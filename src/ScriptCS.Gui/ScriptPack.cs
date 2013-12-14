using ScriptCs.Contracts;

namespace ScriptCs.Gui
{
    public class ScriptPack : IScriptPack
    {
        IScriptPackContext IScriptPack.GetContext()
        {
            return new Gui();
        }

        void IScriptPack.Initialize(IScriptPackSession session)
        {
            session.ImportNamespace("ScriptCS.Gui");
            
            session.AddReference("System.Windows.Forms");
            session.AddReference("System.Drawing");
        }

        void IScriptPack.Terminate()
        {
        }
    }
}