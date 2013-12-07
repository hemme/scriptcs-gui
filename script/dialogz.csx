#r "System.Windows.Forms";
using System.Windows.Forms;
using System.Threading;

public string OpenFileDialog(string path = null){
	
	var d = new OpenFileDialog();
	DialogResult result = DialogResult.Cancel;
	
	if (path!=null) d.InitialDirectory = path;
	d.CheckFileExists = true;
	var t = new Thread(_ => result = d.ShowDialog());
	t.SetApartmentState(ApartmentState.STA);
	t.Start();
	t.Join();
	
	return result == DialogResult.OK 
		? d.FileName
		: null;
}

public string SaveFileDialog(string path = null)
{
	var d = new SaveFileDialog();
	DialogResult result = DialogResult.Cancel;
	
	if (path!=null) d.InitialDirectory = path;
	var t = new Thread(_ => result = d.ShowDialog());
	t.SetApartmentState(ApartmentState.STA);
	t.Start();
	t.Join();
	
	return result == DialogResult.OK
		? d.FileName
		: null;
}
