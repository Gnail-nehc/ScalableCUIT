using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public interface IReporter
    {
        void LogInfo(string message, params object[] args);

        void LogDebug(string message, params object[] args);

        void LogError(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void BeginScenario(int scenarioId, string scenarioTitle);
        void EndScenario();
        void LogDisabledScenario(int sID);
        void LogScenarioPass();
    }
}
