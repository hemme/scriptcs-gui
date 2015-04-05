using System.Windows.Forms;

namespace ScriptCs.Gui
{
    public partial class Gui
    {
        public void SetClipboardText(string text, bool join = true)
        {
            InSTA(() => Clipboard.SetText(text), join);
        }

        public string GetClipboardText(bool join = true)
        {
            var text = string.Empty;

            InSTA(() => { text = Clipboard.GetText(); }, join);

            return text;
        }
    }
}