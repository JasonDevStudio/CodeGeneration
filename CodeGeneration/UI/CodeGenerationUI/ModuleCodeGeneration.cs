using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.InteropServices;
using Library.Facade.CodeGenerator;
using Library.Models;
using System.Configuration;
using Library.StringItemDict;
using Library.Common;

namespace CodeGenerationUI
{
    public partial class ModuleCodeGeneration : Form
    {
        public ModuleCodeGeneration()
        {
            InitializeComponent();
            LoadConfig();
        }

        //测试连接按钮
        private void btnConnection_Click(object sender, EventArgs e)
        {
            Connection();
        }
        //数据库下拉列表选择事件
        private void cboSqlDataBase_SelectedIndexChanged(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string strDataBaseType = ConfigurationManager.AppSettings["DataAccess"];
            string strDataBaseName = cboSqlDataBase.Text;
            if (strDataBaseType.Equals(BaseDict.SqlServerData) && (cboSqlDataBase.Text.Equals("Please select") || string.IsNullOrWhiteSpace(cboSqlDataBase.Text)))
            {
                return;
            }

            try
            {
                CodeGenerators gener = new CodeGenerators();
                var list = gener.QueryTablesAll(out resultMsg, strDataBaseType, strDataBaseName);

                list.Insert(0, new ModelGeneration() { TableName = "Please select" });

                cboSqlDataTable.DisplayMember = "TableName";
                cboSqlDataTable.ValueMember = "TableName";
                cboSqlDataTable.DataSource = list;
                cboSqlDataTable.SelectedValue = "Please select";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //Save按钮事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtxtCont.Text.Trim()))
                return;

            if (string.IsNullOrWhiteSpace(cboSqlDataTable.Text.Trim()))
                return;

            var className = CommonMethod.StringToPublicVar(cboSqlDataTable.Text.Trim());
            sfdCode.FileName = className;
            DialogResult dresult = sfdCode.ShowDialog();
            if (dresult == System.Windows.Forms.DialogResult.OK)
            {
                string code = rtxtCont.Text;
                string path = sfdCode.FileName;
                using (Stream fs = new FileStream(path,FileMode.OpenOrCreate,FileAccess.ReadWrite))
                {
                    var codes = Encoding.Default.GetBytes(code);
                    var length = Convert.ToInt32(codes.Length);
                    fs.Write(codes, 0, length);
                    fs.Close();
                }
            }
        }
        //窗体启动加载事件
        private void ModuleCodeGeneration_Load(object sender, EventArgs e)
        {
            Connection();
        }
        //存储过程生成按钮事件
        private void btnSqlGenerate_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            CodeGenerators gen = new CodeGenerators();

            if (cboSqlDataBase.Text == "Please select" || (string.IsNullOrWhiteSpace(cboSqlDataBase.Text) && strDataAccess.Equals(BaseDict.SqlServerData)))
            {
                MessageBox.Show("请选择数据库!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboSqlDataTable.Text == "Please select" || string.IsNullOrWhiteSpace(cboSqlDataTable.Text))
            {
                MessageBox.Show("请选择数据表!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            rtxtCont.Clear();

            rtxtCont.Text = gen.TSqlCodeGeneration(out resultMsg, cboSqlDataTable.Text.Trim(), strDataAccess, cboSqlDataBase.Text.Trim(),
                txtSqlParameterPrefix.Text.Trim(),txtProcedurePrefix.Text.Trim(),cboSqlInsertUpdate.Text.Trim(),cboSqlSelectDetail.Text.Trim(),
                cboSqlUpdateStatus.Text.Trim(),cboSqlSelectPager.Text.Trim(),cboSqlSelectAll.Text.Trim());
            
            SetStyle();
        }
        //C# Model 层代码生成按钮事件
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            CodeGenerators gen = new CodeGenerators();
            try
            {
                if (cboSqlDataBase.Text == "Please select" || (string.IsNullOrWhiteSpace(cboSqlDataBase.Text) && strDataAccess.Equals(BaseDict.SqlServerData)))
                {
                    MessageBox.Show("请选择数据库!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboSqlDataTable.Text == "Please select" || string.IsNullOrWhiteSpace(cboSqlDataTable.Text))
                {
                    MessageBox.Show("请选择数据表!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rtxtCont.Text = gen.ModelCodeGeneration(out resultMsg, cboSqlDataTable.Text.Trim(),
                    strDataAccess, cboSqlDataBase.Text.Trim(), txtModelsNamespace.Text.Trim(), txtModelClassNamePrefix.Text.Trim());
                SetStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }
        //C# DAL 层代码生成按钮事件
        private void btnDalGenerate_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            CodeGenerators gen = new CodeGenerators();
            try
            {
                if (cboSqlDataBase.Text == "Please select" || (string.IsNullOrWhiteSpace(cboSqlDataBase.Text) && strDataAccess.Equals(BaseDict.SqlServerData)))
                {
                    MessageBox.Show("请选择数据库!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboSqlDataTable.Text == "Please select" || string.IsNullOrWhiteSpace(cboSqlDataTable.Text))
                {
                    MessageBox.Show("请选择数据表!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rtxtCont.Text = gen.DalCodeGeneration(out resultMsg, cboSqlDataTable.Text.Trim(), strDataAccess, cboSqlDataBase.Text.Trim(),
                    txtModelsNamespace.Text.Trim(), txtModelClassNamePrefix.Text.Trim(), txtLogicNamespace.Text.Trim(), txtLogicClassNamePrefix.Text.Trim());
                SetStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }

        }
        //IDAL 接口生成
        private void btnIDALGenerate_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            CodeGenerators gen = new CodeGenerators();
            try
            {
                if (cboSqlDataBase.Text == "Please select" || (string.IsNullOrWhiteSpace(cboSqlDataBase.Text) && strDataAccess.Equals(BaseDict.SqlServerData)))
                {
                    MessageBox.Show("请选择数据库!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboSqlDataTable.Text == "Please select" || string.IsNullOrWhiteSpace(cboSqlDataTable.Text))
                {
                    MessageBox.Show("请选择数据表!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rtxtCont.Text = gen.IDalCodeGeneration(out resultMsg, cboSqlDataTable.Text.Trim(), strDataAccess, cboSqlDataBase.Text.Trim(),
                    txtModelsNamespace.Text.Trim(), txtModelClassNamePrefix.Text.Trim(), txtLogicNamespace.Text.Trim(), txtLogicClassNamePrefix.Text.Trim());
                SetStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }

        }
        //Criteria 类代码生成
        private void btnCriteriaGenerate_Click(object sender, EventArgs e)
        {
            string resultMsg = string.Empty;
            string strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            CodeGenerators gen = new CodeGenerators();
            try
            {
                if (cboSqlDataBase.Text == "Please select" || (string.IsNullOrWhiteSpace(cboSqlDataBase.Text) && strDataAccess.Equals(BaseDict.SqlServerData)))
                {
                    MessageBox.Show("请选择数据库!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboSqlDataTable.Text == "Please select" || string.IsNullOrWhiteSpace(cboSqlDataTable.Text))
                {
                    MessageBox.Show("请选择数据表!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                rtxtCont.Text = gen.CriteriaCodeGeneration(out resultMsg, cboSqlDataTable.Text.Trim(), txtLogicNamespace.Text.Trim());
                SetStyle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.ToString());
            }
        }

        //C# HTMLViewGenerate 代码生成
        private void btnViewGenerate_Click(object sender, EventArgs e)
        {
            if (rtxtCont.Text.Trim() == "")
            {
                SetStyle();
            }
            else
            {
                txtStatus.Text = "View";
                string strHtml = rtxtCont.Text;
                string[] strHtmlCont = strHtml.Split('\n');
                string strHtmlCode = null;
                StringBuilder sbCode = new StringBuilder();

                sbCode.AppendLine("StringBuilder sb = new StringBuilder();");
                foreach (string item in strHtmlCont)
                {
                    string str = item.Trim().Replace("\"", "\\\"");
                    sbCode.AppendLine("sb.Append(\"" + str + "\");");
                }
                rtxtCont.Text = sbCode.ToString();
            }
        }

        //设置界面样式
        private void SetStyle()
        {
            SaveConfig();
            #region
            this.gboxProperty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Width = (this.Width + 600) > 1200 ? 1200 : this.Width + 600;
            gboxContent.Width = this.Width - gboxProperty.Width - 10 - 30;
            gboxContent.Location = new Point(gboxProperty.Location.X + gboxProperty.Width + 10, 5);
            this.gboxContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxContent.Visible = true;
            #endregion
        }

        private void btnSetConfig_Click(object sender, EventArgs e)
        {
            Config config = new Config();
            config.ShowDialog();
        }

        /// <summary>
        /// 数据库连接
        /// </summary>
        private void Connection()
        {
            CodeGenerators gen = new CodeGenerators();
            string resultMsg = string.Empty;
            string strDataAccess = ConfigurationManager.AppSettings["DataAccess"];
            if (strDataAccess.Equals(BaseDict.OracleData))
            {
                var list = gen.QueryTablesAll(out resultMsg, strDataAccess);

                if (string.IsNullOrWhiteSpace(resultMsg) || resultMsg.Contains(BaseDict.SuccessPrefix))
                {
                    list.Insert(0, new ModelGeneration() { TableName = "Please select" });

                    cboSqlDataTable.DisplayMember = "TableName";
                    cboSqlDataTable.ValueMember = "TableName";
                    cboSqlDataTable.DataSource = list;
                    cboSqlDataTable.SelectedValue = "Please select";

                    MessageBox.Show("连接成功!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(resultMsg, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (strDataAccess.Equals(BaseDict.SqlServerData))
            {
                var list = gen.QueryDataBaseAll(out resultMsg, strDataAccess);
                cboSqlDataBase.DataSource = list;
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadConfig()
        {
            txtModelsNamespace.Text = ConfigurationManager.AppSettings["ModelsNamespace"];
            txtModelClassNamePrefix.Text = ConfigurationManager.AppSettings["ModelClassNamePrefix"];
            txtLogicNamespace.Text = ConfigurationManager.AppSettings["LogicNamespace"];
            txtLogicClassNamePrefix.Text = ConfigurationManager.AppSettings["LogicClassNamePrefix"];
            txtFacadeNamespace.Text = ConfigurationManager.AppSettings["FacadeNamespace"];
            txtFacadeClassNameSurfix.Text = ConfigurationManager.AppSettings["FacadeClassPrefix"];
            txtProcedurePrefix.Text = ConfigurationManager.AppSettings["SqlProcedurePrefix"];
            txtSqlTablePrefix.Text = ConfigurationManager.AppSettings["SqlTablePrefix"];
            txtSqlParameterPrefix.Text = ConfigurationManager.AppSettings["SqlParameterPrefix"];
            cboSqlInsertUpdate.Text = ConfigurationManager.AppSettings["SqlInsertUpdate"];
            cboSqlSelectDetail.Text = ConfigurationManager.AppSettings["SqlSelectDetail"];
            cboSqlUpdateStatus.Text = ConfigurationManager.AppSettings["SqlUpdateStatus"];
            cboSqlSelectPager.Text = ConfigurationManager.AppSettings["SqlSelectPager"];
            cboSqlSelectAll.Text = ConfigurationManager.AppSettings["SqlSelectAll"]; 

        }

        /// <summary>
        /// 保存节点
        /// </summary>
        private void SaveConfig()
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        
            if (AppSettingsKeyExists("ModelsNamespace", config))
                config.AppSettings.Settings["ModelsNamespace"].Value = txtModelsNamespace.Text.Trim();
            else
                config.AppSettings.Settings.Add("ModelsNamespace", txtModelsNamespace.Text.Trim());

            if (AppSettingsKeyExists("ModelClassNamePrefix", config))
                config.AppSettings.Settings["ModelClassNamePrefix"].Value = txtModelClassNamePrefix.Text.Trim();
            else
                config.AppSettings.Settings.Add("ModelClassNamePrefix", txtModelClassNamePrefix.Text.Trim());

            if (AppSettingsKeyExists("LogicNamespace", config))
                config.AppSettings.Settings["LogicNamespace"].Value = txtLogicNamespace.Text.Trim();
            else
                config.AppSettings.Settings.Add("LogicNamespace", txtLogicNamespace.Text.Trim());

            if (AppSettingsKeyExists("LogicClassNamePrefix", config))
                config.AppSettings.Settings["LogicClassNamePrefix"].Value = txtLogicClassNamePrefix.Text.Trim();
            else
                config.AppSettings.Settings.Add("LogicClassNamePrefix", txtLogicClassNamePrefix.Text.Trim());

            if (AppSettingsKeyExists("FacadeNamespace", config))
                config.AppSettings.Settings["FacadeNamespace"].Value = txtFacadeNamespace.Text.Trim();
            else
                config.AppSettings.Settings.Add("FacadeNamespace", txtFacadeNamespace.Text.Trim());

            if (AppSettingsKeyExists("SqlProcedurePrefix", config))
                config.AppSettings.Settings["SqlProcedurePrefix"].Value = txtProcedurePrefix.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlProcedurePrefix", txtProcedurePrefix.Text.Trim());

            if (AppSettingsKeyExists("FacadeClassPrefix", config))
                config.AppSettings.Settings["FacadeClassPrefix"].Value = txtFacadeClassNameSurfix.Text.Trim();
            else
                config.AppSettings.Settings.Add("FacadeClassPrefix", txtFacadeClassNameSurfix.Text.Trim());

            if (AppSettingsKeyExists("SqlTablePrefix", config))
                config.AppSettings.Settings["SqlTablePrefix"].Value = txtSqlTablePrefix.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlTablePrefix", txtSqlTablePrefix.Text.Trim());

            if (AppSettingsKeyExists("SqlParameterPrefix", config))
                config.AppSettings.Settings["SqlParameterPrefix"].Value = txtSqlParameterPrefix.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlParameterPrefix", txtSqlParameterPrefix.Text.Trim());

            if (AppSettingsKeyExists("SqlInsertUpdate", config))
                config.AppSettings.Settings["SqlInsertUpdate"].Value = cboSqlInsertUpdate.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlInsertUpdate", cboSqlInsertUpdate.Text.Trim());

            if (AppSettingsKeyExists("SqlSelectDetail", config))
                config.AppSettings.Settings["SqlSelectDetail"].Value = cboSqlSelectDetail.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlSelectDetail", cboSqlSelectDetail.Text.Trim());

            if (AppSettingsKeyExists("SqlUpdateStatus", config))
                config.AppSettings.Settings["SqlUpdateStatus"].Value = cboSqlUpdateStatus.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlUpdateStatus", cboSqlUpdateStatus.Text.Trim());

            if (AppSettingsKeyExists("SqlSelectPager", config))
                config.AppSettings.Settings["SqlSelectPager"].Value = cboSqlSelectPager.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlSelectPager", cboSqlSelectPager.Text.Trim());

            if (AppSettingsKeyExists("SqlSelectAll", config))
                config.AppSettings.Settings["SqlSelectAll"].Value = cboSqlSelectAll.Text.Trim();
            else
                config.AppSettings.Settings.Add("SqlSelectAll", cboSqlSelectAll.Text.Trim());

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

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

        
    }
}
