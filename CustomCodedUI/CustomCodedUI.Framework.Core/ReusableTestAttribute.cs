using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class ReusableTestAttribute : Attribute
    {
        public int[] TestCases { get; set; }

        public string Description { get; set; }

        public ReusableTestAttribute(params int[] _testCases)
        {
            this.TestCases = _testCases;
        }

        public ReusableTestAttribute(int[] _testCases,string description)
        {
            this.Description = description;
            this.TestCases = _testCases;
        }

        public bool ContainsTestCase(int testCaseId)
        {
            bool flag = false;
            if (this.TestCases != null)
            {
                flag = this.TestCases.Contains(testCaseId);
            }
            return flag;
        } 
    }
}
