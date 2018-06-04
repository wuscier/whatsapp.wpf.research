using System;
using System.Runtime.InteropServices;

namespace whatsapp.wpf._2.Helper
{
    public class WinAPIs
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int FindWindowEx(IntPtr hWndParent, IntPtr hWndChildAfter, string wndClass, string wndTitle);




    }
}
