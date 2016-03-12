using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCs.Gui.Watch
{

    internal class ObjectObserver:INotifyPropertyChanged
    {
        public WeakReference Source { get; private set; }
        public ObjectObserver(object src, string propertyName)
        {
            Source = new WeakReference(src);
            var watchThread = new Thread(new ParameterizedThreadStart(Watch));
            watchThread.Start(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void Watch(object propertyName)
        {
            var name = (string)propertyName;
            var t = Source.Target.GetType();
            Func<object> pGet = delegate { return t.GetProperty(name).GetMethod.Invoke(Source.Target, null); };
            var previousValue = pGet();

            while(Source!=null && Source.Target!=null)
            {
                System.Threading.Thread.Sleep(250);

                if (PropertyChanged!=null) {
                    var currentValue = pGet();
                    if (previousValue != currentValue)
                    {
                        previousValue = currentValue;
                        PropertyChanged(this, new PropertyChangedExEventArgs(name, currentValue));
                    }
                }
            }
        }
    }

}
