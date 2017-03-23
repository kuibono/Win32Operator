using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32.WindowExtention;

namespace Win32
{
    public class Win
    {

        public Win(string cls=null,string name=null,int index=0)
        {
            this.Index = index;
            this.Class = cls;
            this.Name = name;

            GetAllWindows();
        }

        
        
        public int Index { get; set; }

        public string Class { get; set; }

        public string Name { get; set; }

        public bool Success { get; set; }

        public int NewWindow { get; set; }

        public int Handle
        {
            get
            {
                if(_Handle==0)
                {
                    _Handle = User32.FindWindow(Class, Name);
                    Name = WinInfoExtention.GetCaption((IntPtr)_Handle);
                    Class = WinInfoExtention.GetClassName((IntPtr)_Handle);

                }
                return _Handle;
            }
            set
            {
                _Handle = value;
                Name = WinInfoExtention.GetCaption((IntPtr)_Handle);
                Class = WinInfoExtention.GetClassName((IntPtr)_Handle);
            }
        }
        private int _Handle { get; set; }


        public List<int> allWindowPtrs { get; set; }

        public List<int> GetAllWindows()
        {
            //this.CurrentWindows = new Win { Handle = 0 }.Children();

            var result = new List<int>();
            //1、获取桌面窗口的句柄
            int desktopPtr = User32.GetDesktopWindow();
            //2、获得一个子窗口（这通常是一个顶层窗口，当前活动的窗口）
            int winPtr = User32.GetWindow((IntPtr)desktopPtr, 5);//GetWindowCmd.GW_CHILD

            //3、循环取得桌面下的所有子窗口
            while (winPtr != 0)
            {
                //4、继续获取下一个子窗口
                winPtr = User32.GetWindow((IntPtr)winPtr, 2);//GW_HWNDNEXT 
                result.Add(winPtr);
            }
            this.allWindowPtrs = result;
            return result;
        }
    }
}
