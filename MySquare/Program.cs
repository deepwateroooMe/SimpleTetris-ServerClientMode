using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MySquare {

    static class Program {

        // 应用程序的主入口点。
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainPad());
        }
    }
}
