using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomCodedUI.Framework
{
    class TestDriver : ITestDriver
    {
        private IReporter _reporter = null;

        private void LoadData(string projectId, string testCaseId, out DataRow globalData, out DataRow localData)
        {
            string sql = "select " + RetData.Name + "," + RetData.Value + "," + RetData.IsGlobal + " from " + RetData.FuncInDB + "(" + projectId + "," + testCaseId + ")";
            DataTable dt = SqlHelper.GetDataTableBySql(sql);
            globalData = TransformDataRow(dt, true);
            localData = TransformDataRow(dt, false);
        }

        private DataRow TransformDataRow(DataTable table, bool isGlobal)
        {
            try
            {
                IEnumerable<DataRow> query = from row in table.AsEnumerable()
                                             where row.Field<bool>(RetData.IsGlobal) == isGlobal
                                             select row;
                if (query.Count() != 0)
                {
                    DataTable dt = query.CopyToDataTable<DataRow>();
                    DataRow dr = dt.NewRow();
                    foreach (DataRow row in dt.Rows)
                    {
                        string colName=row.Field<string>(RetData.Name);
                        string colValue=row.Field<string>(RetData.Value);
                        dt.Columns.Add(colName, typeof(String));
                        dr.SetField<string>(colName,colValue);
                    }
                    return dr;
                }
                else
                    return table.NewRow();
            }
            catch (Exception ex)
            {
                _reporter.LogError(ex.Message);
                throw ex;
            }
        }

        public void Run(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext, TempReader temp)
        {
            _reporter = new Reporter(testContext);
            string dllFullname = DllLoader.GetDllFullNameContainTCInProject(temp.ProjectName, temp.TcFolder);
            Type t;
            if (DllLoader.HasFoundTypeByTcid(dllFullname, temp.TcID.ToString(), out t))
            {
                DataRow globalData, testData;
                this.LoadData(temp.PID.ToString(), temp.TcID.ToString(), out globalData, out testData);
                Execute(new DataUse(globalData), new DataUse(testData), t, temp.PID.ToString(), temp.TcID.ToString());
            }
            else
                throw new NotImplementedException(string.Format("Test case#{0} in Project {1} does not exist!", temp.TcID, t, temp.PID));
        }


        private void Execute(IDataUse globalData, IDataUse testData, Type t, string pid, string tcid)
        {
            try
            {
                //Binding properties that test script used
                var obj = t.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);
                t.InvokeMember(ETestUsage.TestData.ToString(), BindingFlags.SetProperty, null, obj, new object[] { testData });
                t.InvokeMember(ETestUsage.GlobalData.ToString(), BindingFlags.SetProperty, null, obj, new object[] { globalData });
                t.InvokeMember(ETestUsage.Reporter.ToString(), BindingFlags.SetProperty, null, obj, new object[] { _reporter });
                t.InvokeMember(ETestUsage.PID.ToString(), BindingFlags.SetProperty, null, obj, new object[] { pid });
                t.InvokeMember(ETestUsage.TCID.ToString(), BindingFlags.SetProperty, null, obj, new object[] { tcid });
                InvokeSteps(t, obj);
            }
            catch (Exception e)
            {
                ConsoleLogger.LogError("Error occured in class {0},{1}", t.Name, e);
                throw e;
            }
        }

        private void InvokeSteps(Type type, Object instance)
        {
            MethodInfo[] mInfos = type.GetMethods();
            IScenarioData scenarioData = new ScenarioData();
            List<MethodInfo> listMI = new List<MethodInfo>();
            IDictionary<int, MethodInfo> dictRollbackScenario = new Dictionary<int, MethodInfo>();
            Type scenraioAttrType = typeof(ScenarioAttribute);
            Type rollbackAttrType = typeof(RollbackScenarioAttribute);
            ScenarioAttribute lastRunScenario = null;
            List<ScenarioAttribute> rollBackScenariosCompareList = new List<ScenarioAttribute>();
            #region Get all methods set ScenarioAttribute and RollbackScenario
            foreach (MethodInfo mi in mInfos)
            {
                foreach (ScenarioAttribute sa in mi.GetCustomAttributes(typeof(ScenarioAttribute), true))
                {
                    if (sa.Enabled)
                    {
                        listMI.Add(mi);
                    }
                    scenarioData.Add(sa.SID, ETestResult.NotRun);
                }
                foreach (RollbackScenarioAttribute rbsa in mi.GetCustomAttributes(typeof(RollbackScenarioAttribute), true))
                {
                    if (rbsa.Enabled)
                    {
                        dictRollbackScenario.Add(rbsa.SID, mi);
                    }
                }
            }
            if (type.GetProperty(ETestUsage.ScenarioData.ToString()) != null)
            {
                type.InvokeMember(ETestUsage.ScenarioData.ToString(), BindingFlags.SetProperty, null, instance, new object[] { scenarioData });
            }
            #endregion
            try
            {
                #region Invoke Init()
                InvokeTestInitialize(type, instance);
                #endregion

                #region Invoke enabled scenario methods and rollback methods if failed or bloced
                if (listMI.Count > 0)
                {
                    listMI.Sort(
                    delegate(MethodInfo m1, MethodInfo m2)
                    {
                        return ((ScenarioAttribute)(m1.GetCustomAttributes(scenraioAttrType, true))[0]).SID.CompareTo(((ScenarioAttribute)(m2.GetCustomAttributes(scenraioAttrType, true))[0]).SID);
                    }
                    );
                    foreach (MethodInfo m in listMI)
                    {
                        ScenarioAttribute scenarioAttr = (ScenarioAttribute)m.GetCustomAttributes(true)[0];
                        if (scenarioAttr.Enabled)
                        {
                            _reporter.BeginScenario(scenarioAttr.SID, string.IsNullOrEmpty(scenarioAttr.Description) ? m.Name : scenarioAttr.Description);

                            ETestResult retVal = ETestResult.Fail;
                            try
                            {
                                var result = type.InvokeMember(m.Name, BindingFlags.InvokeMethod, null, instance, new Object[0]);

                                if (result == null)
                                {
                                    _reporter.LogWarning("Scenario [{0}] returned void .", scenarioAttr.SID);
                                    LogEndScenario(scenarioAttr.SID, ETestResult.Warning);
                                    continue;
                                }
                                else if (result is ETestResult)
                                {
                                    retVal = (ETestResult)result;
                                    scenarioData[scenarioAttr.SID] = retVal;
                                    LogEndScenario(scenarioAttr.SID, retVal);
                                    if (retVal == ETestResult.Skipped || retVal == ETestResult.NotRun)
                                    {
                                        continue;
                                    }
                                    if (retVal == ETestResult.Fail)
                                    {
                                        lastRunScenario = scenarioAttr;
                                        throw new TestFailException(scenarioAttr.SID);
                                    }
                                    if (retVal == ETestResult.Blocked)
                                    {
                                        lastRunScenario = scenarioAttr;
                                        throw new TestBlockedException(scenarioAttr.SID);
                                    }
                                }
                                else
                                {
                                    _reporter.LogWarning("Scenario [{0}] retuned value was not 'ETestResult' type.", scenarioAttr.SID);
                                    LogEndScenario(scenarioAttr.SID, ETestResult.Warning);
                                }
                                lastRunScenario = scenarioAttr;
                            }
                            catch (TestFailException failEx)
                            {
                                _reporter.LogError("{0}", failEx);
                                Rollback(type, instance, dictRollbackScenario, lastRunScenario.SID);
                                throw failEx;
                            }
                            catch (TestBlockedException blockedEx)
                            {
                                _reporter.LogError("{0}", blockedEx);
                                Rollback(type, instance, dictRollbackScenario, lastRunScenario.SID);
                                throw blockedEx;
                            }
                            catch (Exception e)
                            {
                                _reporter.LogError("{0}", e);
                                if (e.InnerException != null)
                                {
                                    _reporter.LogError("{0}", e.InnerException);
                                    if (e.InnerException.InnerException != null)
                                    {
                                        _reporter.LogError("{0}", e.InnerException.InnerException);
                                    }
                                }
                                LogEndScenario(scenarioAttr.SID, ETestResult.Fail);
                                scenarioData[scenarioAttr.SID] = ETestResult.Fail;
                                Rollback(type, instance, dictRollbackScenario, scenarioAttr.SID);
                                throw e;
                            }
                            #region Rollback required scenarios when all test scenario has executed.
                            finally
                            {
                                ScenarioAttribute lastExpectedScenario = (ScenarioAttribute)listMI.Last().GetCustomAttributes(true)[0];
                                if (null != lastRunScenario && lastRunScenario.SID.Equals(lastExpectedScenario.SID))
                                {
                                    Rollback(type, instance, dictRollbackScenario, lastExpectedScenario.SID);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            _reporter.LogDisabledScenario(scenarioAttr.SID);
                            LogEndScenario(scenarioAttr.SID, ETestResult.Skipped);
                        }
                    }
                }
                #endregion
            }
            finally
            {
                #region Invoke End()
                InvokeTestCleanup(type, instance);
                #endregion
            }
        }
        private void LogEndScenario(int sID, ETestResult testResult)
        {
            switch (testResult)
            {
                case ETestResult.Warning:
                case ETestResult.Skipped:
                case ETestResult.NotRun:
                    _reporter.LogWarning("End Scenario: [{0}] Test result: [{1}]", sID, testResult.ToString().ToUpper());
                    break;
                case ETestResult.Pass:
                    _reporter.LogInfo("End Scenario: [{0}] Test result: [{1}]", sID, testResult.ToString().ToUpper());
                    break;
                case ETestResult.Blocked:
                case ETestResult.Fail:
                    _reporter.LogError("End Scenario: [{0}] Test result: [{1}]", sID, testResult.ToString().ToUpper());
                    break;
            }
        }

        /// <summary>
        /// Run all methods set RollbackScenario attribute from minimum to maximum.
        /// RollbackScenario ID is as same as Scenario ID which need roll back.
        /// </summary>
        private void Rollback(Type type, Object instance, IDictionary<int, MethodInfo> dictRollbackScenario, int sidNeedRollback)
        {
            try
            {
                if (dictRollbackScenario.Count > 0 && dictRollbackScenario.ContainsKey(sidNeedRollback))
                {
                    try
                    {
                        _reporter.LogDebug("---------------------------------------------------");
                        _reporter.LogDebug("Rollback Scenario: [{0}] - {1} START.", sidNeedRollback, dictRollbackScenario[sidNeedRollback].Name);
                        var result = type.InvokeMember(dictRollbackScenario[sidNeedRollback].Name, BindingFlags.InvokeMethod, null, instance, new object[0]);
                        if (result == null)
                        {
                            _reporter.LogInfo("Rollback Scenario [{0}] - DONE", sidNeedRollback);
                        }
                        if (result is ETestResult)
                        {
                            ETestResult returnResult = (ETestResult)result;
                            if (returnResult == ETestResult.Fail || returnResult == ETestResult.Blocked)
                            {
                                _reporter.LogWarning("Rollback Scenario [{0}] - [{1}]", sidNeedRollback, returnResult);
                            }
                            else
                            {
                                _reporter.LogInfo("Rollback Scenario [{0}] - [{1}]", sidNeedRollback, returnResult);
                            }
                            _reporter.LogInfo("Rollback Scenario [{0}] - DONE", sidNeedRollback);
                        }
                        else
                        {
                            _reporter.LogInfo("Rollback Scenarios [{0}] returned value: {1}", sidNeedRollback, result);
                        }
                    }
                    catch (Exception rollScErr)
                    {
                        if (rollScErr.InnerException != null)
                        {
                            _reporter.LogError("RollbackScenario [{0}] thrown an exception - {1}", sidNeedRollback, rollScErr.InnerException.Message);
                        }
                        else
                        {
                            _reporter.LogError("RollbackScenario [{0}] thrown an exception - {1}", sidNeedRollback, rollScErr.Message);
                        }
                    }
                }
            }
            catch (Exception rollErr)
            {
                _reporter.LogWarning(rollErr.Message);
            }
        }

        private void InvokeTestInitialize(Type type, Object instance)
        {
            try
            {
                MemberInfo[] memberInfos = type.GetMembers();
                Type initializeType = typeof(TestInitializeAttribute);
                MemberInfo initialize = null;
                foreach (MemberInfo m in memberInfos)
                {
                    var attributes = m.GetCustomAttributes(initializeType, true);
                    if (attributes.Length > 0)
                    {
                        initialize = m;
                        _reporter.LogInfo("====================================================");
                        _reporter.LogInfo("Initializing TC#{0}", type.Name.Replace("_", ""));
                        type.InvokeMember(initialize.Name, BindingFlags.InvokeMethod, null, instance, new Object[0]);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _reporter.LogError("Initialize Exception in TC#{0}:{1}", (type.Name + "").Replace("_", ""), e.Message);
            }
        }

        private void InvokeTestCleanup(Type type, Object instance)
        {
            try
            {
                MemberInfo[] memInfos = type.GetMembers();
                Type cleanupType = typeof(TestCleanupAttribute);
                MemberInfo cleanup = null;
                foreach (MemberInfo meminfo in memInfos)
                {
                    var attributes = meminfo.GetCustomAttributes(cleanupType, true);
                    if (attributes.Length > 0)
                    {
                        cleanup = meminfo;
                        _reporter.LogInfo("====================================================");
                        _reporter.LogInfo(string.Format("Cleanup TC#{0}", (type.Name + "").Replace("_", "")));
                        type.InvokeMember(cleanup.Name, BindingFlags.InvokeMethod, null, instance, new Object[0]);
                        _reporter.LogInfo("====================================================");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _reporter.LogError("Cleanup Exception in TC#{0}:{1}", (type.Name + "").Replace("_", ""), e.Message);
            }
        }


    }

    public class TestBlockedException : Exception
    {
        public TestBlockedException(int sID)
            : base(string.Format("Scenario [{0}] was marked as [{1}] by user.", sID, ETestResult.Blocked.ToString().ToUpper()))
        {

        }
    }

    public class TestFailException : Exception
    {
        public TestFailException(int sID)
            : base(string.Format("Scenario [{0}] was marked as [{1}] by user.", sID, ETestResult.Fail.ToString().ToUpper()))
        {

        }
    }

    //public class LoadDataFailException : Exception
    //{
    //    public LoadDataFailException(string projectId, string testCaseId, bool isGlobal)
    //        : base(string.Format("Fail to Retrieve" + (isGlobal ? "Global" : "Test") + " Data in Test Case [{0}] in Project [{1}].Please check DB first.", testCaseId, projectId))
    //    {

    //    }
    //}

}
