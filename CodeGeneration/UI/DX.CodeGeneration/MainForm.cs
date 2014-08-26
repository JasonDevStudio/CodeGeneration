using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DX.CodeGeneration
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void rgbiTheme_GalleryItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e)
        {
            var skinName = e.Item.Value.ToString();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
        }

        /// <summary>
        /// 关闭按钮
        /// </summary> 
        private void bvbiClose_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 数据库配置保存
        /// </summary> 
        private void btnDBConfigSave_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary> 
        private void btnDBConnection_Click(object sender, EventArgs e)
        {

        }
    }
}
