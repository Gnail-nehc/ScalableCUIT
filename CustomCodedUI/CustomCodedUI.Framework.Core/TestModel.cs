using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public abstract class TestModel
    {
        /// <summary>
        /// Current project id
        /// </summary>
        public string PID { get; set; }

        /// <summary>
        /// Current test case id in runtime
        /// </summary>
        public string TCID { get; set; }

        /// <summary>
        /// Gloabl Test Data For Test Cases
        /// </summary>
        public IDataUse GlobalData
        {
            get;
            set;
        }
        /// <summary>
        /// Test Data For Specified Test Cases
        /// </summary>
        public IDataUse TestData
        {
            get;
            set;
        }
        /// <summary>
        /// All Test Cases' report Information
        /// </summary>
        public IReporter Reporter
        {
            get;
            set;
        }

        public IScenarioData ScenarioData
        {
            get;
            set;
        }


        [TestInitialize]
        public abstract void Init();

        [Scenario]
        public abstract void Run();

        [TestCleanup]
        public abstract void End();


    }

}
