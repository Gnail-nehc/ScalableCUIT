using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScenarioAttribute : Attribute
    {
        private int _sid = 0;
        private bool _enabled = true;

        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
            }
        }
        public int SID
        {
            get { return _sid; }
        }

        public string Description
        {
            get;
            set;
        }
        internal ScenarioAttribute()
        {
            this._sid = 0;
        }

        public ScenarioAttribute(bool enabled)
        {
            this._enabled = enabled;
        }

        public ScenarioAttribute(int id)
        {
            this._sid = id;
        }

        public ScenarioAttribute(int id, bool enabled)
        {
            this._sid = id;
            this._enabled = enabled;
        }
        public ScenarioAttribute(int id, string desc)
        {
            this._sid = id;
            this.Description = desc;
        }
        public ScenarioAttribute(int id, bool enabled, string desc)
        {
            this._sid = id;
            this._enabled = enabled;
            this.Description = desc;
        }
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestInitializeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestCleanupAttribute : Attribute
    {
    }
}
