using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class TestCaseDiscover
    {
        public static int[] GetAllTcidInProject(string prjName, string agentFolder)
        {
            string dllName = DllLoader.GetDllFullNameContainTCInProject(prjName, agentFolder);
            IList<int> tcIds = new List<int>(0);
            Assembly testAssembly = null;
            try
            {
                testAssembly = Assembly.LoadFrom(dllName);
            }
            catch (FileNotFoundException e)
            {
                ConsoleLogger.LogError("{0} not found!", e.FileName);
                throw e;
            }
            catch (BadImageFormatException e)
            {
                ConsoleLogger.LogError("{0}", e.Message);
                throw e;
            }
            catch (Exception e)
            {
                ConsoleLogger.LogError("Exception loading log and assembly: {0}", e);
                throw e;
            }

            Type[] assemblyTypes = testAssembly.GetExportedTypes();
            foreach (Type t in assemblyTypes)
            {
                int tcId;
                if (IsTypeTestCase(t.Name, out tcId) && HasTypeInheritedFromTestBase(t))
                {
                    tcIds.Add(tcId);
                }
            }

            return tcIds.ToArray<int>();
        }


        public static bool HasTypeInheritedFromTestBase(Type t)
        {
            bool flag = false;
            while (t.BaseType != null && t.BaseType.Name != "Object")
            {
                t = t.BaseType;
                if ("TestBase".Equals(t.Name))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private static bool IsTypeTestCase(string typeName, out int tcId)
        {
            tcId = 0;
            if ('_'.Equals(typeName.Substring(0, 1)))
            {
                try
                {
                    tcId = int.Parse(typeName.Replace("_", ""));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
