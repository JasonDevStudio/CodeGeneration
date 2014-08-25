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
    }
}
