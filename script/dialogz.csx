#r "System.Windows.Forms";
using System.Windows.Forms;
using System.Threading;

public static class Win {
	public static void InSTA(ThreadStart action, bool join = false) {
		var t = new Thread(action);
		t.IsBackground = true;
		t.SetApartmentState(ApartmentState.STA);
		t.Start();
		if (join) t.Join();
	}	
	public static string OpenFileDialog(string path = null)	{
		var d = new OpenFileDialog();
		DialogResult result = DialogResult.Cancel;
		
		if (path!=null) d.InitialDirectory = path;
		d.CheckFileExists = true;
		InSTA(delegate { result = d.ShowDialog(); }, join:true);
		
		return result == DialogResult.OK ? d.FileName : null;
	}
	public static string SaveFileDialog(string path = null)	{
		var d = new SaveFileDialog();
		DialogResult result = DialogResult.Cancel;
		
		if (path!=null) d.InitialDirectory = path;
		InSTA(delegate { result = d.ShowDialog(); }, join: true);
		
		return result == DialogResult.OK ? d.FileName : null;
	}		
	public static Form PopUp(bool modal = false, bool topMost = false) {
		var f = new Form {
			TopMost = topMost
		};
		
		InSTA(delegate { Application.Run(f); }, join:modal);
				
		return f;
	}
}