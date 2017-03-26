using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Win32;
using Win32.Task;
using Win32.WindowExtention;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManagerHandler handller = new ApisTaskManagerHandller();
            for(int i=0;i<100;i++)
            {
                TaskRunner runner = new TaskRunner();
                runner.Handler = handller;
                runner.FileName = @"C:\Users\BEN.BING.CUI\Desktop\Win32API-master\Win32API\Task\TestOperation.cs";
                runner.Task = new Win32.Model.OperationTask { Data= "{\"Name\":\"wwww\"}" };
                runner.Execute();
            }
           
        }



        public static int FindByXPath(string path)
        {
            var pathTree = split(path).Select(x => serilize(x)).ToList();
            for (int i = 0; i < pathTree.Count; i++)
            {
                if (i != pathTree.Count - 1)
                {
                    pathTree[i].NextLevel = pathTree[i + 1];
                }
            }
            return getWindowByParttern(pathTree.First());

        }

        private static int getWindowByParttern(WindowPartten parn)
        {
            var current = User32.FindWindow(parn.Class, parn.Name);
            if (parn.NextLevel != null)
            {
                return getSubWindow(parn.NextLevel, current);
            }
            else
            {
                return current;
            }
        }

        private static int getSubWindow(WindowPartten parn, int parentPtr)
        {
            var currentPar = User32.FindWindowEx((IntPtr)parentPtr, IntPtr.Zero, parn.Class, null);
            if (parn.Index > 0)
            {
                for (int idx = 1; idx <= parn.Index; idx++)
                {
                    currentPar = User32.FindWindowEx((IntPtr)parentPtr, (IntPtr)currentPar, parn.Class, null);
                }
            }
            if (parn.NextLevel == null)
            {
                return currentPar;
            }
            else
            {
                return getSubWindow(parn.NextLevel, currentPar);
            }
        }

        private static string[] split(string path)
        {
            return path.Split('>');
        }

        private static WindowPartten serilize(string str)
        {
            WindowPartten p = new WindowPartten();
            p.Index = Convert.ToInt32(mathResult(str, @"\[(?<num>\d*?)\]", "0"));
            p.Class = mathResult(str, @"@class='(?<cls>.*?)'", null);
            p.Name = mathResult(str, @"@name='(?<nam>.*?)'", null);
            return p;
        }

        private static string mathResult(string source, string partten, string defaultValue = "")
        {
            var m = new Regex(partten).Match(source);
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            else
            {
                return defaultValue;
            }
        }
    }

    public class WindowPartten
    {
        public int Index { get; set; }

        public string Class { get; set; }

        public string Name { get; set; }

        public WindowPartten NextLevel { get; set; }
    }
}
