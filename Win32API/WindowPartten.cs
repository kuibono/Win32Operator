using System;
using System.Collections.Generic;
using System.Text;

namespace Win32
{
    public class WindowPartten
    {
        public int Index { get; set; }

        public string Class { get; set; }

        public string Name { get; set; }

        public WindowPartten NextLevel { get; set; }
    }
}
