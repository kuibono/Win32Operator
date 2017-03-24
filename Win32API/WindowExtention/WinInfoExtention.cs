using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32.WindowExtention
{
    public static class WinInfoExtention
    {
        public static string GetClassName(IntPtr handle)
        {
            StringBuilder a = new StringBuilder();
            var r = User32.GetClassName(handle, a, 255);
            return a.ToString();
        }

        public static string GetCaption(IntPtr handle)
        {
            StringBuilder a = new StringBuilder(1024);
            var r = User32.GetWindowText(handle, a, 255);
            if(a.Length==0)
            {
                User32.SendMessage(handle, User32.WM_GETTEXT, 1024,a);
            }
            return a.ToString();
        }
        
    }
}
