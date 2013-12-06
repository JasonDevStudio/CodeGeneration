namespace CodeGenerationUI
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.gboxProperty = new System.Windows.Forms.GroupBox();
            this.cboDataBaseType = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnConnection = new System.Windows.Forms.Button();
            this.txtDataBaseUserPwd = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.txtDataBaseUserId = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtDataBaseSource = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gboxProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxProperty
            // 
            this.gboxProperty.Controls.Add(this.cboDataBaseType);
            this.gboxProperty.Controls.Add(this.textBox2);
            this.gboxProperty.Controls.Add(this.textBox3);
            this.gboxProperty.Controls.Add(this.btnConnection);
            this.gboxProperty.Controls.Add(this.txtDataBaseUserPwd);
            this.gboxProperty.Controls.Add(this.btnSave);
            this.gboxProperty.Controls.Add(this.textBox8);
            this.gboxProperty.Controls.Add(this.txtDataBaseUserId);
            this.gboxProperty.Controls.Add(this.textBox4);
            this.gboxProperty.Controls.Add(this.txtDataBaseSource);
            this.gboxProperty.Controls.Add(this.textBox1);
            this.gboxProperty.Location = new System.Drawing.Point(12, 12);
            this.gboxProperty.Name = "gboxProperty";
            this.gboxProperty.Size = new System.Drawing.Size(403, 224);
            this.gboxProperty.TabIndex = 2;
            this.gboxProperty.TabStop = false;
            // 
            // cboDataBaseType
            // 
            this.cboDataBaseType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDataBaseType.DisplayMember = "name";
            this.cboDataBaseType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDataBaseType.FormattingEnabled = true;
            this.cboDataBaseType.Items.AddRange(new object[] {
            "SqlServerDataAccess",
            "OracleDataAccess"});
            this.cboDataBaseType.Location = new System.Drawing.Point(148, 37);
            this.cboDataBaseType.Name = "cboDataBaseType";
            this.cboDataBaseType.Size = new System.Drawing.Size(233, 20);
            this.cboDataBaseType.TabIndex = 45;
            this.cboDataBaseType.Text = "SqlServerDataAccess";
            this.cboDataBaseType.ValueMember = "id";
            this.cboDataBaseType.SelectedIndexChanged += new System.EventHandler(this.cboDataBaseType_SelectedIndexChanged);
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(146, 35);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(236, 25);
            this.textBox2.TabIndex = 38;
            this.textBox2.Text = "127.0.0.1,2433";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(21, 35);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(126, 25);
            this.textBox3.TabIndex = 37;
            this.textBox3.Text = "DataBaseType";
            // 
            // btnConnection
            // 
            this.btnConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnection.Location = new System.Drawing.Point(21, 141);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(361, 25);
            this.btnConnection.TabIndex = 36;
            this.btnConnection.Text = "Connection";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // txtDataBaseUserPwd
            // 
            this.txtDataBaseUserPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataBaseUserPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataBaseUserPwd.Location = new System.Drawing.Point(146, 107);
            this.txtDataBaseUserPwd.Multiline = true;
            this.txtDataBaseUserPwd.Name = "txtDataBaseUserPwd";
            this.txtDataBaseUserPwd.Size = new System.Drawing.Size(236, 25);
            this.txtDataBaseUserPwd.TabIndex = 21;
            this.txtDataBaseUserPwd.Text = "123456";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(21, 172);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(361, 25);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save AS";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Location = new System.Drawing.Point(21, 107);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(126, 25);
            this.textBox8.TabIndex = 20;
            this.textBox8.Text = "DataBaseUserPwd";
            // 
            // txtDataBaseUserId
            // 
            this.txtDataBaseUserId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataBaseUserId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataBaseUserId.Location = new System.Drawing.Point(146, 83);
            this.txtDataBaseUserId.Multiline = true;
            this.txtDataBaseUserId.Name = "txtDataBaseUserId";
            this.txtDataBaseUserId.Size = new System.Drawing.Size(236, 25);
            this.txtDataBaseUserId.TabIndex = 19;
            this.txtDataBaseUserId.Text = "sa";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(21, 83);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(126, 25);
            this.textBox4.TabIndex = 18;
            this.textBox4.Text = "DataBaseUserId";
            // 
            // txtDataBaseSource
            // 
            this.txtDataBaseSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataBaseSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDataBaseSource.Location = new System.Drawing.Point(146, 59);
            this.txtDataBaseSource.Multiline = true;
            this.txtDataBaseSource.Name = "txtDataBaseSource";
            this.txtDataBaseSource.Size = new System.Drawing.Size(236, 25);
            this.txtDataBaseSource.TabIndex = 17;
            this.txtDataBaseSource.Text = "127.0.0.1,2433";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(21, 59);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(126, 25);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "DataBaseSource";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 254);
            this.Controls.Add(this.gboxProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(448, 297);
            this.MinimumSize = new System.Drawing.Size(448, 297);
            this.Name = "Config";
            this.Text = "数据库配置";
            this.Load += new System.EventHandler(this.Config_Load);
            this.gboxProperty.ResumeLayout(false);
            this.gboxProperty.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxProperty;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.TextBox txtDataBaseUserPwd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox txtDataBaseUserId;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtDataBaseSource;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ComboBox cboDataBaseType;
    }
}