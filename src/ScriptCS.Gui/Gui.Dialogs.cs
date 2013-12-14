using System.Windows.Forms;

namespace ScriptCS.Gui
{
    public partial class Gui
    {
        public string OpenFileDialog(string path = null, bool join = true)
        {
            var d = new OpenFileDialog();
            DialogResult result = DialogResult.Cancel;

            if (path != null) d.InitialDirectory = path;
            d.CheckFileExists = true;
            InSTA(delegate { result = d.ShowDialog(); }, join);

            return result == DialogResult.OK ? d.FileName : null;
        }

        public string SaveFileDialog(string path = null, bool join = true)
        {
            var d = new SaveFileDialog();
            DialogResult result = DialogResult.Cancel;

            if (path != null) d.InitialDirectory = path;
            InSTA(delegate { result = d.ShowDialog(); }, join);

            return result == DialogResult.OK ? d.FileName : null;
        }
    }
}