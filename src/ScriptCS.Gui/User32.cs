using System;
using System.Runtime.InteropServices;

namespace Native
{
    internal static class User32
    {
        //Usage ideas from JohnStewien: http://code.cheesydesign.com/?p=422

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr FindWindow(char[] lpClassName, char[] lpWindowName);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern int SetWindowLongW(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        internal const int GWLP_HWNDPARENT = -8;
    }
}