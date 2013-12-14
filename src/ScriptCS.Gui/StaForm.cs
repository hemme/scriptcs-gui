using ScriptCs.Gui;
namespace ScriptCS.Gui
{
    public class StaForm : System.Windows.Forms.Form
    {
        public StaForm()
            : base()
        {
            Sta = new StaQueue(this);
        }

        public StaQueue Sta;
    }
}