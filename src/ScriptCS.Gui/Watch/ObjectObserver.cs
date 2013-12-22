using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScriptCs.Gui.Watch
{
    public static class ConsoleObserver
    {
        public static void Attach<T>(T src, string propertyName)
        {
            var o = new ObjectObserver(src, propertyName);
            o.PropertyChanged += OnPropertyChanged;
        }

        public static void Detach<T>(T src)
        {
            //TODO
        }

        private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("{0} changed.", e.PropertyName );
            var pc2 = e as PropertyChangedExEventArgs;

            if (pc2 != null)
                Console.WriteLine("New value: {0}.", pc2.Value);
        }
    }

    public class PropertyChangedExEventArgs : PropertyChangedEventArgs
    {
        public PropertyChangedExEventArgs(string name, object value) : base(name)
        {
            Value = value;
        }
        public object Value { get; private set; }
    }

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
                if (PropertyChanged!=null) {
                    var currentValue = pGet();
                    if (currentValue != previousValue)
                    {
                        PropertyChanged(this, new PropertyChangedExEventArgs(name, currentValue));
                        previousValue = currentValue;
                    }
                }

                System.Threading.Thread.Sleep(250);
            }
        }
    }

}
