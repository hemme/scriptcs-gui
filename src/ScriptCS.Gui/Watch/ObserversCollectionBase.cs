using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Gui.Watch
{
    public abstract class ObserversCollectionBase
    {
        private List<ObjectObserver> _observers = new List<ObjectObserver>();

        public void Attach<T>(T src, string propertyName)
            where T : class
        {
            var o = new ObjectObserver(src, propertyName);
            o.PropertyChanged += OnPropertyChanged;

            _observers.Add(o);
        }

        public void Detach<T>(T src)
            where T : class
        {
            var srcObservers = _observers.Where(o => o.Source.Target == src).ToList();

            foreach (var so in srcObservers)
            {
                so.PropertyChanged -= OnPropertyChanged;
                _observers.Remove(so);
            }
        }

        protected abstract void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
    }
}
