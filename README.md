# Win32Operator
a stupid win32 operator base on user32 api


using like following code:


var mainWin = new Win("JpWin32").Active();

            var pn = mainWin.Child("#32770");
            pn.Child("Edit").SetText("喜連瓜破");
            pn.Child("Edit",null,1).SetText("福");
            var NewWinPrt = pn.Child("Button", "平均経路の探索(&X)").Click();

            var newWin1 = mainWin.GetnewWindow(mainWin.allWindowPtrs, "#32770").Print();
            var newWin2 = mainWin.GetnewWindow(mainWin.allWindowPtrs, "#32770").Child("Button", "OK").Click();
            var newWin = mainWin.GetnewWindow(mainWin.allWindowPtrs, "#32770");

            newWin.Child(null, null, 0)
                .Child(null, null, 0)
                .Child(null, null, 0)
                .Child(null, null, 0)
                .Child("Edit", null, 0)
                .SetText("asp.net");
