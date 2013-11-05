using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class Reporter : IReporter
    {
        private Microsoft.VisualStudio.TestTools.UnitTesting.TestContext _testContext;
        private int _scenarioId;
        private string _scenarioTitle;

        public Reporter(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            try
            {
                _testContext = testContext;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            if (null != _testContext)
                _testContext.WriteLine("{0}", string.Format(message, args));
            try
            {
                ConsoleLogger.LogInfo(message, args);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void LogDebug(string message, params object[] args)
        {
            if (null != _testContext)
                _testContext.WriteLine("{0}", string.Format(message, args));
            try
            {
                ConsoleLogger.LogDebug(message, args);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void LogError(string message, params object[] args)
        {
            if (null != _testContext)
                _testContext.WriteLine("{0}", string.Format(message, args));
            try
            {
                ConsoleLogger.LogError(message, args);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void LogWarning(string message, params object[] args)
        {
            if (null != _testContext)
                _testContext.WriteLine("{0}", string.Format(message, args));
            try
            {
                ConsoleLogger.LogWarning(message, args);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }


        public void BeginScenario(int scenarioId, string scenarioTitle)
        {
            _scenarioId = scenarioId;
            _scenarioTitle = scenarioTitle;
            LogDebug("---------------------------------------------------");
            LogDebug("Beginning Scenario: [{0}] - {1}", scenarioId, scenarioTitle);
        }

        public void LogDisabledScenario(int sID)
        {
            LogDebug("-----------------------------------------------------");
            LogDebug("Scenario [{0}] is not enable.", sID);
        }

        public void EndScenario()
        {
            LogDebug("Scenario {0} End: {1}", _scenarioId, _scenarioTitle);
            LogDebug("---------------------------------------------------");
        }

        public void LogScenarioPass()
        {
            LogInfo("Scenario {0} PASS: {1}", _scenarioId, _scenarioTitle);
        }
    }
}
