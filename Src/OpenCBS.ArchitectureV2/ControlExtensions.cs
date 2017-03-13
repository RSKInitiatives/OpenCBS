using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OpenCBS.ArchitectureV2
{
    public static class ControlExtensions
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public static void SetHint(this TextBox textBox, string hintText)
        {
            SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, hintText);
        }
    }
}
