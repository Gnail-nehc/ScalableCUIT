using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public interface IScenarioData
    {
        ETestResult this[int scenarioID] { get; set; }

        void Add(int scenarioId, ETestResult result);
    }
}
