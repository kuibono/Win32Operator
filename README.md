# Win32Operator
a stupid win32 operator base on user32 api


using like following code:

            for(int i=0;i<100;i++)
            {
                var mainWin = new Win("JpWin32").Active();

                var pn = mainWin.Child("#32770");
                pn.Child("Edit").SetText("喜連瓜破");
                pn.Child("Edit", null, 1).SetText("福");
                var NewWinPrt = pn.Child("Button", "平均経路の探索(&X)").Click();

                var newWin1 = mainWin.GetnewWindow("#32770").Print();
                var newWin2 = mainWin.GetnewWindow("#32770", "経路の印刷").Child("Button", "OK").Click();

                Console.WriteLine("start find new window");
                var newWin = mainWin.GetnewWindow("#32770", "Save Print Output As");
                Console.WriteLine(newWin.Name);

                //Thread.Sleep(5000);
                var edit = newWin
                    .Child("DUIViewWndClassName")
                    .Child("DirectUIHWND")
                    .Child("FloatNotifySink")
                    .Child("ComboBox")
                    .Child("Edit")
                    .SetText(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                newWin.Child("Button", "&Save").Click();

                new Win("#32770", "印刷中...").WaitWindowDispose();
                newWin1.Close();
            }
           