using CustomCodedUI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Application.WebAppDemo
{
    [ReusableTest(2)]
    class _1 : TestBase
    {
        [Scenario(1, false)]
        public ETestResult Scenario1()
        {
            if (this.SubmitForm())
                return ETestResult.Pass;
            else
            return ETestResult.Fail;
        }

        [Scenario(2, false)]
        public ETestResult Scenario2()
        {
           
            return ETestResult.Fail;
        }

        [Scenario(3, false)]
        public ETestResult Scenario3()
        {
            bool isSuccess = false;
            
            return isSuccess ? ETestResult.Pass : ETestResult.Fail;
        }
        
    }
}
