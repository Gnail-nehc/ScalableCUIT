using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    interface ITestDriver
    {
        void Run(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext, TempReader tr);
    }
}
