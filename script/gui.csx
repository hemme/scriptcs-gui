#r "System.Windows.Forms"
#r "System.Drawing"
#r "bin/native.dll"
#load "sta.csx"

using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Reflection;

public static class Gui {
			
	public static void InSTA(Action action, bool join = false) {
		var t = new Thread(delegate(){ action(); });
		t.IsBackground = true;
		t.SetApartmentState(ApartmentState.STA);
		t.Start();
		if (join) t.Join();
	}	
	
	public static string OpenFileDialog(string path = null, bool join = true)	{
		var d = new OpenFileDialog();
		DialogResult result = DialogResult.Cancel;
		
		if (path!=null) d.InitialDirectory = path;
		d.CheckFileExists = true;
		InSTA(delegate { result = d.ShowDialog(); }, join);
		
		return result == DialogResult.OK ? d.FileName : null;
	}
	
	public static string SaveFileDialog(string path = null, bool join = true)	{
		var d = new SaveFileDialog();
		DialogResult result = DialogResult.Cancel;
		
		if (path!=null) d.InitialDirectory = path;
		InSTA(delegate { result = d.ShowDialog(); }, join);
		
		return result == DialogResult.OK ? d.FileName : null;
	}

	
	private static IntPtr _consoleHWnd;
	public static void GrabConsole()
	{
		_consoleHWnd = Native.User32.FindWindow("ConsoleWindowClass".ToCharArray(), Console.Title.ToCharArray());
	}
	
	public static void AttachToConsole(Form f) 
	{
		if (_consoleHWnd==IntPtr.Zero)
			GrabConsole();
			
		Native.User32.SetWindowLongW(new HandleRef(f, f.Handle), Native.User32.GWLP_HWNDPARENT, _consoleHWnd);
	}
	
	public class StaForm : Form {
		public StaForm():base() {
			Sta =  new StaQueue(this);			
		}
		
		public StaQueue Sta;
	}
	
	public class DrawForm:Form {
	
		public Graphics Graphics {get; private set;}
		public DrawForm():base() {
		
			Load += (s,e) => {
								
				var p = new Panel();
				p.Dock = DockStyle.Fill;
				Controls.Add(p);
				
				typeof(Panel).InvokeMember("DoubleBuffered", 
					BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, 
					null, p, new object[] { true });
			
				var bmp = new Bitmap(1024,1024);
				p.Paint += (_,pe) => { 
					pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;  
					pe.Graphics.DrawImage(bmp, Point.Empty);
				};
				Graphics = Graphics.FromImage(bmp);
			
				var t = new Thread(new ThreadStart(
					delegate{
						while(true) {
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
	
	public static StaForm PopUp(bool modal = false, Action<Form> initForm = null) {
		
		StaForm f = new StaForm(); 
	
		Gui.InSTA(delegate {
			if (initForm!=null)
				initForm(f);
				
			AttachToConsole(f);		
			Application.Run(f);	
		});
		while(f==null) {System.Threading.Thread.Sleep(100);}
								
		return f;
	}
	
	public static Graphics Drawing(bool modal = false) {
		Graphics g = null;
		var f = new DrawForm();
		f.MaximumSize = new Size(1024, 1024);
		
		Gui.InSTA(delegate{
		
			AttachToConsole(f);
			Application.Run(f);
		});
		
		while ((g = f.Graphics) == null) {System.Threading.Thread.Sleep(100);}
		return g;
	}
		
}