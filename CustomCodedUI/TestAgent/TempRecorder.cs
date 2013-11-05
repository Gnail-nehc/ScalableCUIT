using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    class TempRecorder
    {
        public void WriteContent(int pId, int tcId, string testCaseFullName)
        {
            if (string.IsNullOrEmpty(testCaseFullName))
            {
                throw new Exception("Test Case's path is null or empty!!!!");
            }
            string temp = Path.GetTempPath();
            FileStream s = null;
            StreamWriter writer = null;
            try
            {
                s = File.Open(temp + "customcodedui.tmp", FileMode.Create);
                writer = new StreamWriter(s);
                writer.WriteLine(pId);
                writer.WriteLine(tcId);
                writer.WriteLine(testCaseFullName);
            }
            catch (Exception e)
            {
                throw new Exception("Create customcodedui.tmp failed ---->" + e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (s != null)
                {
                    s.Close();
                }
            }
        }
    }
}
