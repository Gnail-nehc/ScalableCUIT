using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RollbackScenarioAttribute : Attribute
    {
        private bool _enabled = true;
        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }

        }
        private int _sid;
        public int SID
        {
            get
            {
                return _sid;
            }

        }

        public RollbackScenarioAttribute(int id)
        {
            _sid = id;
        }
    }
}
