using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ScriptCs.Gui
{
    public class StaQueue
    {
        public static StaQueue operator +(StaQueue s, Action a)
        {
            s.Enqueue(a);
            return s;
        }

        public StaQueue(System.Windows.Forms.Control c)
        {
            _control = c;
        }

        private System.Windows.Forms.Control _control;

        private Queue<Action> _staActions = new Queue<Action>();

        private bool _run = false;

        [STAThread]
        private void Consume()
        {
            while (_run)
            {
                if (_staActions.Any())
                {
                    var todo = _staActions.Dequeue();

                    if (todo != null)
                    {
                        if (_control.InvokeRequired)
                            _control.Invoke(todo);
                        else
                            todo();
                    }
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        public void Enqueue(Action a)
        {
            if (a != null)
            {
                _staActions.Enqueue(a);
            }
        }

        public int Length { get { return _staActions.Count(); } }

        private Thread _staThread = null;

        public void Start()
        {
            _run = true;

            if (_staThread == null)
            {
                _staThread = new Thread(new ThreadStart(Consume));
                _staThread.IsBackground = true;
                _staThread.SetApartmentState(ApartmentState.STA);
                _staThread.Start();
            }
        }

        public void Stop()
        {
            _run = false;
            _staThread = null;
        }
    }
}