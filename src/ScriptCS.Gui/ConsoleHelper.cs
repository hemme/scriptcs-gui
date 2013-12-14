using System;
using System.Runtime.InteropServices;

namespace ScriptCs.Gui
{
    internal static class ConsoleHelper
    {
        private static IntPtr _consoleHWnd;

        public static void GrabConsole()
        {
            _consoleHWnd = Native.User32.FindWindow("ConsoleWindowClass".ToCharArray(), Console.Title.ToCharArray());
        }

        public static void AttachToConsole(System.Windows.Forms.Form f)
        {
            if (_consoleHWnd == IntPtr.Zero)
                GrabConsole();

            Native.User32.SetWindowLongW(new HandleRef(f, f.Handle), Native.User32.GWLP_HWNDPARENT, _consoleHWnd);
        }
    }
}