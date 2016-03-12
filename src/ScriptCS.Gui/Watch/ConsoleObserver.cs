using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Gui.Watch
{
    public class ConsoleObserver : ObserversCollectionBase
    {
        public static readonly ObserversCollectionBase Instance = new ConsoleObserver();

        public static new void Attach<T>(T src, string propertyName)
            where T : class
        {
            Instance.Attach(src, propertyName);
        }

        public static new void Detach<T>(T src)
            where T : class
        {
            Instance.Detach(src);
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("{0} changed.", e.PropertyName);
            var pc2 = e as PropertyChangedExEventArgs;

            if (pc2 != null)
                Console.WriteLine("New value: {0}.", pc2.Value);
        }
    }
}
