using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Application.WebAppDemo
{
    public static class CustomVerification
    {
        public static void VerifyEqual(this TestBase @this, double expectedResult, double actualResult)
        {
            if (expectedResult == actualResult)
                @this.Reporter.LogInfo("The actual result {0} is equal to expected result {1}.", actualResult, expectedResult);
            else
                Assert.Fail("The actual result {0} isn't equal to expected result {1}.", actualResult, expectedResult);
        }

        public static void VerifyMatch(this TestBase @this, string expectedResult, string actualResult)
        {
            if (actualResult != string.Empty && expectedResult.Equals(actualResult, StringComparison.CurrentCulture))
                @this.Reporter.LogInfo("The actual result {0} match the expected result {1}.", actualResult, expectedResult);
            else
                Assert.Fail("The actual result {0} doesn't match expected result {1}.", actualResult, expectedResult);
        }

        public static void VerifyContain(this TestBase @this, string expectedResult, string actualResult)
        {
            if (actualResult != string.Empty && expectedResult.IndexOf(actualResult) > 0)
                @this.Reporter.LogInfo("The expected result {0} contain actual result {1}.", expectedResult, actualResult);
            else
                Assert.Fail("The expected result {0} doesn't contain actual result {1}.", expectedResult, actualResult);
        }
    }
}
