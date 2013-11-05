using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public class DataUse : IDataUse
    {
        private DataRow _dr;
        public DataUse(DataRow dr)
        {
            _dr = dr;
        }
        public string this[string key]
        {
            get
            {
                return _dr[key] as string;
            }
        }
    }
}
