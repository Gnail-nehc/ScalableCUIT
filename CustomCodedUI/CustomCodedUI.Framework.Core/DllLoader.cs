
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class DllLoader
    {
        public static string GetDllFullNameContainTCInProject(string prjName, string dllPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dllPath);
            FileInfo[] subFiles = dirInfo.GetFiles();
            if (dllPath[dllPath.Length - 1] != '\\')
            {
                dllPath += "\\";
            }
            foreach (FileInfo file in subFiles)
            {
                string fullName = dllPath + file.ToString();
                if (fullName.ToLower().Contains(prjName.ToLower()) && fullName.ToLower().Contains("TestScripts".ToLower()) && fullName.ToLower().Contains(".dll"))
                {
                    return fullName;
                }
            }
            throw new DllNotFoundException("Not found " + prjName + " related dll under folder" + dllPath + ".");
        }


        public static bool HasFoundTypeByTcid(string dllFullname, string tcID, out Type type)
        {
            type = null;
            try
            {
                Assembly assembly = Assembly.LoadFrom(dllFullname);
                Type[] types = assembly.GetTypes();

                foreach (Type t in types)
                {
                    string target = "_" + tcID;
                    if (IsNumeric(t.Name.Substring(1)))
                    {
                        if (string.Compare(t.Name, target) == 0 && TestCaseDiscover.HasTypeInheritedFromTestBase(t))
                        {
                            type = t;
                            return true;
                        }
                        else
                        {
                            foreach (Attribute attr in t.GetCustomAttributes(false))
                            {
                                if (attr is ReusableTestAttribute)
                                {
                                    if (((ReusableTestAttribute)attr).ContainsTestCase(Convert.ToInt32(tcID)))
                                    {
                                        type = t;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleLogger.LogError(ex.Message);
                throw ex;
            }
            return false;
        }

        private static bool IsNumeric(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }


    }
}
