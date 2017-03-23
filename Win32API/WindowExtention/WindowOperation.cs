using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Win32.WindowExtention
{
    public static class WindowOperation
    {
        private static long timeOut = 5000;

        public static Win SetText(this Win win, string text)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                win.Success = User32.SendMessage((IntPtr)win.Handle, 0x000C, text.Length, text) > 0;
            }
            while (!win.Success && sw.ElapsedMilliseconds < timeOut);
            return win;
        }

        public static Win Click(this Win win)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                win.Success = User32.SendMessage((IntPtr)win.Handle, 0x00F5, 0, 0) == 0;
                
            }
            while (!win.Success && sw.ElapsedMilliseconds < timeOut);
           
            return win;
        }
        public static Win Command(this Win win, int wMsg)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                win.Success = User32.PostMessage((IntPtr)win.Handle, 0x0111, wMsg, 0) > 0;
            }
            while (!win.Success && sw.ElapsedMilliseconds < timeOut);
           
            return win;
        }
        public static Win Close(this Win win)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                win.Success = User32.SendMessage((IntPtr)win.Handle, 0x0010, 0, 0) == 0;
            }
            while (!win.Success && sw.ElapsedMilliseconds < timeOut);

            
            return win;
        }

        public static Win Print(this Win win)
        {
            return Command(win, 32946);
        }
        public static Win Active(this Win win)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                win.Success = User32.SetActiveWindow((IntPtr)win.Handle) > 0;
            }
            while (!win.Success && sw.ElapsedMilliseconds < timeOut);
           
            return win;
        }

        public static void WaitWindowDispose(this Win win)
        {
            var disposed = false;
            while(disposed==false)
            {
                var w = new Win() { Handle = win.Handle };
                if(string.IsNullOrEmpty(w.Name) && string.IsNullOrEmpty(w.Class))
                {
                    disposed = true;
                }
                Thread.Sleep(200);
            }
        }
       
        public static Win GetnewWindow(this Win win, string cls,string name="")
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<int> oldPtrs = win.allWindowPtrs;
            do
            {
                win.GetAllWindows();
                var diffWindows = win.allWindowPtrs.Where(x => oldPtrs.Contains(x) == false).Select(x => new Win { Handle = x });
                Console.WriteLine(string.Format("different windows count:{0}", diffWindows.Count()));
                diffWindows.ToList().ForEach(x =>
                {
                    Console.WriteLine(string.Format("Handle:{0} cls:{1} name:{2}", x.Handle, x.Class, x.Name));
                });
                if (string.IsNullOrEmpty(cls) == false)
                {
                    diffWindows = diffWindows.Where(x => x.Class.Contains(cls) && x.Name.Contains(name));
                }
                if (diffWindows.Count() > 0)
                {
                    return diffWindows.First();
                }
                Thread.Sleep(200);
            }
            while (sw.ElapsedMilliseconds < timeOut);
            return null;
        }
    }
}
