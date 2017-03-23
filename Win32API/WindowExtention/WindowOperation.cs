using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32.WindowExtention
{
    public static class WindowOperation
    {
        private static int timeOut = 5000;

        public static Win SetText(this Win win, string text)
        {
            win.Success = User32.SendMessage((IntPtr)win.Handle, 0x000C, text.Length, text) > 0;
            return win;
        }

        public static Win Click(this Win win)
        {
            win.Success = User32.SendMessage((IntPtr)win.Handle, 0x00F5, 0, 0) > 0;
            return win;
        }
        public static Win Command(this Win win, int wMsg)
        {
            win.Success = User32.PostMessage((IntPtr)win.Handle, 0x0111, wMsg, 0) > 0;
            return win;
        }
        public static Win Close(this Win win)
        {
            win.Success = User32.SendMessage((IntPtr)win.Handle, 0x0010, 0, 0) > 0;
            return win;
        }

        public static Win Print(this Win win)
        {
            return Command(win, 32946);
        }
        public static Win Active(this Win win)
        {
            win.Success = User32.SetActiveWindow((IntPtr)win.Handle) > 0;
            return win;
        }

       
        public static Win GetnewWindow(this Win win, List<int> oldPtrs, string cls)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.ElapsedMilliseconds < timeOut)
            {
                win.GetAllWindows();
                var diffWindows = win.allWindowPtrs.Where(x => oldPtrs.Contains(x) == false).Select(x => new Win { Handle = x });
                if (string.IsNullOrEmpty(cls) == false)
                {
                    diffWindows = diffWindows.Where(x => x.Class == cls);
                }
                if (diffWindows.Count() > 0)
                {
                    return diffWindows.First();
                }
            }
            return null;
        }
    }
}
