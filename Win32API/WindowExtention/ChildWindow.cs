using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32.WindowExtention
{
    public static class ChildWindow
    {
        public static Win Child(this Win win, string cls = null, string name = null, int index = 0)
        {
            var currentPtr = User32.FindWindowEx((IntPtr)win.Handle, IntPtr.Zero, cls, name);
            if (index > 0)
            {
                for (int idx = 1; idx <= index; idx++)
                {
                    currentPtr = User32.FindWindowEx((IntPtr)win.Handle, (IntPtr)currentPtr, cls, name);
                }
            }
            return new Win
            {
                Class = cls,
                Index = index,
                Name = name,
                Handle = currentPtr
            };
        }

        public static List<Win> Children(this Win win, string cls = null, string name = null)
        {
            //win.GetAllWindows();
            var result = new List<Win>();
            int currentPtr = -1;
            while (currentPtr != 0)
            {
                if (currentPtr == -1)
                {
                    currentPtr = User32.FindWindowEx((IntPtr)win.Handle, IntPtr.Zero, cls, name);
                }
                else
                {
                    currentPtr = User32.FindWindowEx((IntPtr)win.Handle, (IntPtr)currentPtr, cls, name);
                }
                result.Add(new Win { Handle = currentPtr });
            }
            return result;
        }
    }
}
