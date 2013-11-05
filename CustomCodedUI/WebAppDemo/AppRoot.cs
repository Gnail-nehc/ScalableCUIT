using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Application.WebAppDemo
{
    public class AppRoot : BrowserWindow
    {
        public AppRoot()
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
            #endregion
        }

        public void CloseBrowser()
        {
            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process proc in procs)
            {
                if (proc.ProcessName.ToUpper() == "IEXPLORE")
                {
                    proc.Kill();
                }
            }
        }

        public void LaunchUrl(System.Uri uri)
        {
            this.CopyFrom(BrowserWindow.Launch(uri));
            this.WaitForControlReady(15 * 1000);
        }

    }
}
