using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    class Program
    {
        private const string CORE_LIBRARY = "CustomCodedUI.Framework.Core.dll";
        /// <summary>
        /// TestAgent.exe [PID] [TCID] [TestReportPath]
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string agentFolder = AppDomain.CurrentDomain.BaseDirectory;
            ConsoleLogger.LogDebug("Agent's Folder:" + agentFolder);
            Environment.SetEnvironmentVariable("TestAgentFolder", agentFolder, EnvironmentVariableTarget.User);

            try
            {
                MSTestShooter executioner = new MSTestShooter();

                MSTestDiscoverer mst = new MSTestDiscoverer();
                string strMSTestExeFullName = mst.GetMSTestExeFullName();
                string strTestCoreFileFullName = Path.Combine(agentFolder, CORE_LIBRARY);

                if (args.Length == 0)//in dev mode
                {
                    string strTestReportFolder = ConfigurationManager.AppSettings["LogFolder"];
                    if (!Directory.Exists(strTestReportFolder))
                    {
                        Directory.CreateDirectory(strTestReportFolder);
                    }
                    string prjsInfo = ProjectInfo.PrintAll();
             INPUT: Console.Write("Choose project ID... " + prjsInfo + " :");
                    string prjName, prjId = Console.ReadLine().ToString();
                    int pos = prjsInfo.IndexOf(prjId);
                    if (pos > -1)
                    {
                        int dotpos = prjsInfo.IndexOf('.', pos + 1);
                        prjName = prjsInfo.Substring(dotpos + 1, prjsInfo.IndexOf(" ", pos) - dotpos - 1);
                    }
                    else
                    {
                        ConsoleLogger.LogError("Invalid Project ID [{0}]", prjId);
                        return;
                    }
                    Console.Write("Please input test case ID: ");
                    int pid, tcId;
                    string input = Console.ReadLine().ToString();
                    string strReportFullName = string.Empty;
                    if (int.TryParse(prjId, out pid) && int.TryParse(input, out tcId))
                    {
                        strReportFullName = strTestReportFolder + "\\" + prjName + "_TC" + tcId + "_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".trx";

                        ConsoleLogger.LogDebug("Test case {0} is running ...", tcId);

                        if (executioner.CallMsTest(pid, tcId, strMSTestExeFullName, strTestCoreFileFullName, strReportFullName, agentFolder) == 0)
                        {
                            ConsoleLogger.LogInfo("Test Case {0}: PASSED!", tcId);
                        }
                        else
                        {
                            ConsoleLogger.LogError("Test Case {0}: FAILED!", tcId);
                        }
                    }
                    //Run all of the test cases in project 1 which name prefix is "_" under TestScripts
                    else
                    {
                        if (input == string.Empty)
                        {
                            var ids = TestCaseDiscover.GetAllTcidInProject(prjName, agentFolder);
                            ConsoleLogger.LogDebug("{0} test cases will be running.", ids.Length);
                            foreach (int id in ids)
                            {
                                ConsoleLogger.LogDebug("Test case {0} is running ...", id);
                                strReportFullName = strTestReportFolder + "\\" + prjName + "_TC" + id + "_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".trx";
                                var result = executioner.CallMsTest(pid, id, strMSTestExeFullName, strTestCoreFileFullName, strReportFullName, agentFolder);
                                if (result == 0)
                                {
                                    ConsoleLogger.LogInfo("Test Case {0}: PASSED!", id);
                                }
                                else
                                {
                                    ConsoleLogger.LogError("Test Case {0}: FAILED!", id);
                                }
                            }
                        }
                        else
                            goto INPUT;
                    }
                    Console.Write("Done!");
                    Console.Read();
                }
                else
                {
                    string strReportFullName = args[2];
                    int pId, tcId;
                    if (int.TryParse(args[0], out pId) && int.TryParse(args[1], out tcId))
                    {
                        int exitCode = executioner.CallMsTest(pId, tcId, strMSTestExeFullName, strTestCoreFileFullName, strReportFullName, agentFolder);
                        Environment.Exit(exitCode);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Project Id and/or Test case ID.");
                        Environment.Exit(ExitCode.TCInvalid);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
