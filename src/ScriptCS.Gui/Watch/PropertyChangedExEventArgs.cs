using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Gui.Watch
{
    public class PropertyChangedExEventArgs : PropertyChangedEventArgs
    {
        public PropertyChangedExEventArgs(string name, object value)
            : base(name)
        {
            Value = value;
        }
        public object Value { get; private set; }
    }
}
