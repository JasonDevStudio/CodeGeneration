using Library.Facade.CodeGenerator;
using Library.StringItemDict;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DX.CodeGeneration
{
    public partial class MainForm : BaseClass
    {
        #region 私有函数

        private Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        
        /// <summary>
        /// 判断appSettings中是否有此项
        /// </summary>
        private static bool AppSettingsKeyExists(string strKey, Configuration config)
        {
            foreach (string str in config.AppSettings.Settings.AllKeys)
            {
                if (str == strKey)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="strDataAccess"></param>
        private void LoadConfig(string strDataAccess = null)
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            strDataAccess = string.IsNullOrWhiteSpace(strDataAccess) ? ConfigurationManager.AppSettings["DataAccess"] : strDataAccess;
            string strConnectionString = ConfigurationManager.ConnectionStrings[strDataAccess].ConnectionString;
            var strSection = strConnectionString.Split(';');
            string strDataBaseSource = string.Empty;
            string strDataBaseUserId = string.Empty;
            string strDataBaseUserPwd = string.Empty;
            foreach (var item in strSection)
            {
                if (item.Contains("Data Source="))
                    strDataBaseSource = item.Replace("Data Source=", "");
                else if (item.Contains("User ID="))
                    strDataBaseUserId = item.Replace("User ID=", "");
                else if (item.Contains("Password="))
                    strDataBaseUserPwd = item.Replace("Password=", "");
            }
            rgpDataType.EditValue  = strDataAccess;
            txtDataBaseSource.Text = strDataBaseSource;
            txtDataBaseUserId.Text = strDataBaseUserId;
            txtDataBaseUserPwd.Text = strDataBaseUserPwd;
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        private void DBConnectionTest()
        { 
            var strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            var resultMsg = string.Empty;
            CodeGenerators gen = new CodeGenerators();
            var list = gen.QueryTablesAll(out resultMsg, strDataAccess);

            base.CloseWait();

            if (string.IsNullOrWhiteSpace(resultMsg) || resultMsg.Contains(BaseDict.SuccessPrefix))
            {
                MessageBox.Show("连接成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(resultMsg, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        /// <summary>
        /// 数据库配置保存
        /// </summary>
        private void DBConfigSave()
        {
            string strDataBaseAccess = string.Empty;
            string strDataAccess = rgpDataType.EditValue.ToString();
            string strOracleDataAccess = "Data Source={0};Persist Security Info=True;User ID={1};Password={2}";
            string strSqlServerDataAccess = "Data Source={0};Initial Catalog=master;User ID={1};Password={2}";
            string strDataBaseSource = txtDataBaseSource.Text.Trim();
            string strDataBaseUserId = txtDataBaseUserId.Text.Trim();
            string strDataBaseUserPwd = txtDataBaseUserPwd.Text.Trim();

            if (string.IsNullOrWhiteSpace(strDataAccess))
                return;
             
            if (AppSettingsKeyExists("DataAccess", config))
                config.AppSettings.Settings["DataAccess"].Value = strDataAccess;
            else
                config.AppSettings.Settings.Add("DataAccess", strDataAccess);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            if (strDataAccess.Equals("SqlServerDataAccess"))
                strDataBaseAccess = string.Format(strSqlServerDataAccess, strDataBaseSource, strDataBaseUserId, strDataBaseUserPwd);
            else if (strDataAccess.Equals("OracleDataAccess"))
                strDataBaseAccess = string.Format(strOracleDataAccess, strDataBaseSource, strDataBaseUserId, strDataBaseUserPwd);

            config.ConnectionStrings.ConnectionStrings[strDataAccess].ConnectionString = strDataBaseAccess;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(strDataAccess);

            base.CloseWait();

            MessageBox.Show("配置完成!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        public MainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 主题加载
        /// </summary> 
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
            base.ShowWait();

            Thread thread = new Thread(new ThreadStart(DBConfigSave));
            thread.Start();

            ConfigurationManager.RefreshSection("connectionStrings");
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary> 
        private void btnDBConnection_Click(object sender, EventArgs e)
        {
            base.ShowWait();
             
            Thread thread = new Thread(new ThreadStart(DBConnectionTest));
            thread.Start();
        }
         
        /// <summary>
        /// 窗体加载事件
        /// </summary> 
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoadConfig();
        }

        /// <summary>
        /// 数据库配置文件重新加载
        /// </summary>
        private void btnDBConfigLoad_Click(object sender, EventArgs e)
        {
            this.LoadConfig();
        }
    }
}
