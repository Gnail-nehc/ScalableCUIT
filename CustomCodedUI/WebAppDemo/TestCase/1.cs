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
        private string submissionNo = "";

        [Scenario(1, false)]
        public ETestResult Scenario1()
        {
            if (this.ImportSubmission(out submissionNo))
                return ETestResult.Pass;
            else
            return ETestResult.Fail;
        }

        [Scenario(2, false)]
        public ETestResult Scenario2()
        {
            //if (this.submissionNo != "")
            //{
            //    this.NewQuote(this.submissionNo);
            //    return ETestResult.Pass;
            //}
            //else
            return ETestResult.Fail;
        }

        [Scenario(3, false)]
        public ETestResult Scenario3()
        {
            bool isSuccess = false;
            //isSuccess=this.VerifyQuote(TestData["SIC"].ToString(), Convert.ToInt32(TestData["AggLimit"]), Convert.ToInt32(TestData["SubAggLimit"]));
            return isSuccess ? ETestResult.Pass : ETestResult.Fail;
        }
        
    }
}
