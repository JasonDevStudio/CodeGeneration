using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DX.CodeGeneration
{
    public class BaseClass : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        // 等待窗体 声明
        Wait wait = null;
         
        /// <summary>
        /// 显示等待状态窗体
        /// </summary>
        public void ShowWait()
        {
            if (wait == null || wait.IsDisposed)
            {
                wait = new Wait();
                var pointY = this.Location.Y + (this.Height / 2) - wait.Height / 2;
                var pointX = this.Location.X + (this.Width / 2) - wait.Width / 2;
                var point = new Point(pointX, pointY);
                wait.Location = point;
                wait.TopMost = true;

                wait.Show();
            }
        }

        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void CloseWait()
        {
            if (wait != null || !wait.IsDisposed)
            {
                wait.Close();
            } 
        }
         
    }
}
