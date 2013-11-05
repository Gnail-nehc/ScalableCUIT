using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.Configuration;


namespace CustomCodedUI.Framework
{
    [CodedUITest]
    public class TestEntry
    {
        public TestContext TestContext
        {
            get;
            set;
        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void Run()
        {
            ITestDriver td = new TestDriver();
            TempReader temp = new TempReader().ReadContent();
            this.TestContext.WriteLine("Current Project: {0}", temp.ProjectName);
            this.TestContext.WriteLine("Current Test Case ID: {0}", temp.TcID);
            this.TestContext.WriteLine("Current Test Case Folder: {0}", temp.TcFolder);
            td.Run(TestContext, temp);
        }


    }
}
