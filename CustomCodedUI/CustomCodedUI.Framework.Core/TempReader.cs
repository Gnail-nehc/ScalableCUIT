using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class TempReader
    {
        public int PID
        {
            get;
            set;
        }
        public string ProjectName
        {
            get;
            set;
        }
        public int TcID
        {
            get;
            set;
        }
        public string TcFolder
        {
            get;
            set;
        }

        public TempReader ReadContent()
        {
            int pId, tcId;
            string prjName, tcFolder;
            StreamReader reader = null;
            try
            {
                string temp = Path.GetTempPath();
                reader = File.OpenText(temp + "customcodedui.tmp");
                pId = int.Parse(reader.ReadLine().Trim());
                tcId = int.Parse(reader.ReadLine().Trim());
                tcFolder = reader.ReadLine().Trim();
                reader.Close();
                prjName = ProjectInfo.FindPrjNameById(pId.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                }
            }
            if (string.IsNullOrEmpty(tcFolder))
            {
                throw new Exception("TCLibPath is null or empty!!!");
            }
            return new TempReader { PID = pId, ProjectName = prjName, TcID = tcId, TcFolder = tcFolder };
        }

    }
}
