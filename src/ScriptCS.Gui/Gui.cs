using ScriptCs.Contracts;
using System;
using System.Threading;

namespace ScriptCS.Gui
{
    public partial class Gui : IScriptPackContext
    {
        /// <summary>
        /// Starts a new thread in STA
        /// </summary>
        /// <param name="action">The action to execute in the new thread</param>
        /// <param name="join">Join the thread</param>
        public void InSTA(Action action, bool join = false)
        {
            var t = new Thread(delegate() { action(); });
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            if (join) t.Join();
        }
    }
}