using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Framework
{
    public interface IDataUse
    {
        string this[string key] { get; }
    }
}
