using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MySquare.Utilities {

    public class MyEventArgs : EventArgs {
        public IPEndPoint EndPoint { set; get; }
    }
 // 这个类： 自动检测是否有用户上线,但是它是用在两个端的
    public class OnLineDetector {

        public IPEndPoint RemoteEndPoint { set; get; }
        private DateTime keyDateTime;
        private System.Timers.Timer timer = new System.Timers.Timer();
        private int timeout = 0;
        public event EventHandler OffLineEvent;

        public OnLineDetector(int t = 2) {
            timeout = t;
            timer.Interval = 10;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
        }

        public void Mark() { // 开始计时 
            // if (GameEngine.IsLanGameStarted)
            keyDateTime = DateTime.Now;
        }
        public void Start() {
            timer.Enabled = true;
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (keyDateTime == default(DateTime))
                return;
            double t = DateTime.Now.Subtract(keyDateTime).TotalSeconds;
            if (t > timeout) {
                timer.Enabled = false;
                if (OffLineEvent != null) // 自动触发下线通知 
                    OffLineEvent(this, new MyEventArgs() { EndPoint = RemoteEndPoint });
            }
        }
    }
}
