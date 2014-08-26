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
using System.Windows.Forms;

namespace CodeGenerationUI
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        #region 私有函数
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

        private void LoadConfig(string strDataAccess = null)
        {
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
            cboDataBaseType.Text = strDataAccess;
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
            if (string.IsNullOrWhiteSpace(resultMsg) || resultMsg.Contains(BaseDict.SuccessPrefix))
            {
                MessageBox.Show("连接成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(resultMsg, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        /// <summary>
        /// 窗体加载事件
        /// </summary> 
        private void Config_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        /// <summary>
        /// 保存按钮 事件
        /// </summary> 
        private void btnSave_Click(object sender, EventArgs e)
        {
            string strDataBaseAccess = string.Empty;
            string strDataAccess = cboDataBaseType.Text;
            string strOracleDataAccess = "Data Source={0};Persist Security Info=True;User ID={1};Password={2}";
            string strSqlServerDataAccess = "Data Source={0};Initial Catalog=master;User ID={1};Password={2}";
            string strDataBaseSource = txtDataBaseSource.Text.Trim();
            string strDataBaseUserId = txtDataBaseUserId.Text.Trim();
            string strDataBaseUserPwd = txtDataBaseUserPwd.Text.Trim();

            if (string.IsNullOrWhiteSpace(strDataAccess))
                return;

            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

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

            MessageBox.Show("配置完成!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /// <summary>
        /// 测试连接 按钮事件
        /// </summary> 
        private void btnConnection_Click(object sender, EventArgs e)
        {
            DBConnectionTest();
        }

        /// <summary>
        /// 数据类型 下拉菜单事件
        /// </summary> 
        private void cboDataBaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboDataBaseType.Text))
                return;

            var DataBaseType = cboDataBaseType.Text.Trim();
            if (DataBaseType.Equals(BaseDict.OracleData) || DataBaseType.Equals(BaseDict.SqlServerData))
            {
                LoadConfig(DataBaseType);
            }
            else
            {
                MessageBox.Show("数据类型错误!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
