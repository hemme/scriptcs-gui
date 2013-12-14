using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading;

namespace ScriptCS.Gui
{
    public class DrawForm : System.Windows.Forms.Form
    {
        public System.Drawing.Graphics Graphics { get; private set; }

        public DrawForm()
            : base()
        {
            Load += (s, e) =>
            {
                var p = new System.Windows.Forms.Panel();
                p.Dock = System.Windows.Forms.DockStyle.Fill;
                Controls.Add(p);

                typeof(System.Windows.Forms.Panel).InvokeMember("DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null, p, new object[] { true });

                var bmp = new System.Drawing.Bitmap(1024, 1024);
                p.Paint += (_, pe) =>
                {
                    pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    pe.Graphics.DrawImage(bmp, System.Drawing.Point.Empty);
                };
                Graphics = System.Drawing.Graphics.FromImage(bmp);

                var t = new Thread(new ThreadStart(
                    delegate
                    {
                        while (true)
                        {
                            p.Invalidate();
                            p.Update();
                            System.Threading.Thread.Sleep(250);
                        }
                    }));
                t.IsBackground = true;
                t.Start();
            };
        }
    }
}