using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class ScenarioData : IScenarioData
    {
        private IDictionary<int, ETestResult> _scenarioData;
        public ScenarioData()
        {
            _scenarioData = new Dictionary<int, ETestResult>();

        }

        public void Add(int id, ETestResult result)
        {
            if (!_scenarioData.ContainsKey(id))
            {
                _scenarioData.Add(id, result);
            }
        }

        public ETestResult this[int id]
        {
            get
            {
                return _scenarioData[id];
            }
            set
            {
                _scenarioData[id] = value;
            }
        }
    }
}
