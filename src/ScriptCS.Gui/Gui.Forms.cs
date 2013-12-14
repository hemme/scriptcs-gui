using System;

namespace ScriptCS.Gui
{
    public partial class Gui
    {
        public StaForm PopUp(bool modal = false, Action<System.Windows.Forms.Form> initForm = null)
        {
            StaForm f = new StaForm();

            InSTA(delegate
            {
                if (initForm != null)
                    initForm(f);

                ConsoleHelper.AttachToConsole(f);
                System.Windows.Forms.Application.Run(f);
            });
            while (f == null) { System.Threading.Thread.Sleep(100); }

            return f;
        }

        public System.Drawing.Graphics Drawing(bool modal = false)
        {
            System.Drawing.Graphics g = null;
            var f = new DrawForm();
            f.MaximumSize = new System.Drawing.Size(1024, 1024);

            InSTA(delegate
            {
                ConsoleHelper.AttachToConsole(f);
                System.Windows.Forms.Application.Run(f);
            });

            while ((g = f.Graphics) == null) { System.Threading.Thread.Sleep(100); }
            return g;
        }
    }
}