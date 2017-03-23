using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace Win32
{
    public class WindowHelper
    {
        public static void SetText(string path, string text)
        {
            var ptr = FindByXPath(path);
            Console.WriteLine("setText: "+User32.SendMessage((IntPtr)ptr, 0x000C, text.Length, text));
        }
        public static int Click(string path)
        {
            var ptr = FindByXPath(path);
            var result = User32.SendMessage((IntPtr)ptr, 0x00F5, 0, 0);
            Console.WriteLine("click: "+ result);
            return result;
        }
        public static void Command(string path, int wMsg)
        {
            var ptr = FindByXPath(path);
            User32.PostMessage((IntPtr)ptr, 0x0111, wMsg, 0);
        }

        public static void Close(string path)
        {
            var ptr = FindByXPath(path);
            User32.SendMessage((IntPtr)ptr, 0x0010, 0, 0);
        }

        public static int FindByXPath(string path)
        {
            int result = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var pathTree = split(path).Select(x => serilize(x)).ToList();
            for (int i = 0; i < pathTree.Count; i++)
            {
                if (i != pathTree.Count - 1)
                {
                    pathTree[i].NextLevel = pathTree[i + 1];
                }
            }
            while (result == 0 && sw.ElapsedMilliseconds < 15000)
            {

                result = getWindowByParttern(pathTree.First());
                if (result == 0)
                {
                    Console.WriteLine(path + "   not found,time:" + sw.ElapsedMilliseconds);
                    Thread.Sleep(500);
                }
            }
            return result;

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
            var currentPar = User32.FindWindowEx((IntPtr)parentPtr, IntPtr.Zero, parn.Class, parn.Name);
            if (parn.Index > 0)
            {
                for (int idx = 1; idx <= parn.Index; idx++)
                {
                    currentPar = User32.FindWindowEx((IntPtr)parentPtr, (IntPtr)currentPar, parn.Class, parn.Name);
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
}
