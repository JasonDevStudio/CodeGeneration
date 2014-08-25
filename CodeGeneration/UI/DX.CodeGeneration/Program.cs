using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DX.CodeGeneration
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 设置程序样式 
            var skinName = "Office 2013";
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
