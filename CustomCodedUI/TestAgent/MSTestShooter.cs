using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    class MSTestShooter
    {
        private const string TEST_METHOD_NAME = "Run";
        public int CallMsTest(int pId, int tcId, string msTestExeFullName, string testAssemblyFullName, string reportFullName, string testCaseFolder)
        {
            try
            {
                TempRecorder temp = new TempRecorder();
                temp.WriteContent(pId, tcId, testCaseFolder);
                ProcessStartInfo pStartInfo = new ProcessStartInfo(msTestExeFullName);
                pStartInfo.CreateNoWindow = false;
                pStartInfo.UseShellExecute = false;

                pStartInfo.Arguments = string.Format("/nologo /testcontainer:\"{0}\" /test:{1} /resultsfile:\"{2}\"",
                    testAssemblyFullName,
                    TEST_METHOD_NAME,
                    reportFullName);

                ConsoleLogger.LogDebug("Process Arguments: {0}", pStartInfo.Arguments);

                try
                {
                    ConsoleLogger.LogInfo(msTestExeFullName);
                    using (Process p = Process.Start(pStartInfo))
                    {
                        if (p != null)
                        {
                            ConsoleLogger.LogDebug("Current Process ID: {0}", p.Id);
                        }
                        p.WaitForExit();
                        return p.ExitCode;
                    }
                }
                catch (Win32Exception e)
                {
                    ConsoleLogger.LogError("class:MSTestShooter, method:CallMsTest:{0}", e);
                    return ExitCode.ProcessStartException;
                }
            }
            catch (Exception e)
            {
                ConsoleLogger.LogError("unknown exception:{0}", e);
                return ExitCode.UnknownPlaybackException;
            }
        }
    }


    sealed class ExitCode
    {
        public const int Success = 0;
        public const int UnknownException = 2;
        public const int UnknownPlaybackException = 3;
        public const int TCInvalid = 4;
        public const int RegistryException = 5;
        public const int ProcessStartException = 6;
    }
}
