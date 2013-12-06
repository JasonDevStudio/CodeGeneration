namespace CodeGenerationUI
{
    partial class ModuleCodeGeneration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleCodeGeneration));
            this.gboxProperty = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnCriteriaGenerate = new System.Windows.Forms.Button();
            this.btnIDALGenerate = new System.Windows.Forms.Button();
            this.cboSqlSelectAll = new System.Windows.Forms.ComboBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.cboSqlSelectPager = new System.Windows.Forms.ComboBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.txtSqlParameterPrefix = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.txtModelClassNamePrefix = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnSetConfig = new System.Windows.Forms.Button();
            this.txtFacadeNamespace = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.txtSqlTablePrefix = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.txtFacadeClassNameSurfix = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.txtLogicNamespace = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.btnSqlGenerate = new System.Windows.Forms.Button();
            this.btnDalGenerate = new System.Windows.Forms.Button();
            this.cboSqlUpdateStatus = new System.Windows.Forms.ComboBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.cboSqlSelectDetail = new System.Windows.Forms.ComboBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.cboSqlInsertUpdate = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.txtProcedurePrefix = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.txtModelsNamespace = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.btnModelGenerate = new System.Windows.Forms.Button();
            this.btnConnection = new System.Windows.Forms.Button();
            this.btnViewGenerate = new System.Windows.Forms.Button();
            this.txtLogicClassNamePrefix = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.cboSqlDataBase = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.cboSqlDataTable = new System.Windows.Forms.ComboBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.gboxContent = new System.Windows.Forms.GroupBox();
            this.rtxtCont = new System.Windows.Forms.RichTextBox();
            this.sfdCode = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cboSqlDelete = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.gboxProperty.SuspendLayout();
            this.gboxContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboxProperty
            // 
            this.gboxProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxProperty.Controls.Add(this.cboSqlDelete);
            this.gboxProperty.Controls.Add(this.textBox1);
            this.gboxProperty.Controls.Add(this.textBox24);
            this.gboxProperty.Controls.Add(this.button1);
            this.gboxProperty.Controls.Add(this.btnCriteriaGenerate);
            this.gboxProperty.Controls.Add(this.btnIDALGenerate);
            this.gboxProperty.Controls.Add(this.cboSqlSelectAll);
            this.gboxProperty.Controls.Add(this.textBox22);
            this.gboxProperty.Controls.Add(this.textBox23);
            this.gboxProperty.Controls.Add(this.cboSqlSelectPager);
            this.gboxProperty.Controls.Add(this.textBox15);
            this.gboxProperty.Controls.Add(this.textBox21);
            this.gboxProperty.Controls.Add(this.txtSqlParameterPrefix);
            this.gboxProperty.Controls.Add(this.textBox8);
            this.gboxProperty.Controls.Add(this.txtModelClassNamePrefix);
            this.gboxProperty.Controls.Add(this.textBox4);
            this.gboxProperty.Controls.Add(this.btnSetConfig);
            this.gboxProperty.Controls.Add(this.txtFacadeNamespace);
            this.gboxProperty.Controls.Add(this.textBox19);
            this.gboxProperty.Controls.Add(this.txtSqlTablePrefix);
            this.gboxProperty.Controls.Add(this.textBox17);
            this.gboxProperty.Controls.Add(this.txtFacadeClassNameSurfix);
            this.gboxProperty.Controls.Add(this.textBox20);
            this.gboxProperty.Controls.Add(this.txtLogicNamespace);
            this.gboxProperty.Controls.Add(this.textBox18);
            this.gboxProperty.Controls.Add(this.btnSqlGenerate);
            this.gboxProperty.Controls.Add(this.btnDalGenerate);
            this.gboxProperty.Controls.Add(this.cboSqlUpdateStatus);
            this.gboxProperty.Controls.Add(this.textBox12);
            this.gboxProperty.Controls.Add(this.textBox13);
            this.gboxProperty.Controls.Add(this.cboSqlSelectDetail);
            this.gboxProperty.Controls.Add(this.textBox10);
            this.gboxProperty.Controls.Add(this.textBox11);
            this.gboxProperty.Controls.Add(this.cboSqlInsertUpdate);
            this.gboxProperty.Controls.Add(this.textBox2);
            this.gboxProperty.Controls.Add(this.textBox7);
            this.gboxProperty.Controls.Add(this.txtProcedurePrefix);
            this.gboxProperty.Controls.Add(this.textBox5);
            this.gboxProperty.Controls.Add(this.txtModelsNamespace);
            this.gboxProperty.Controls.Add(this.textBox3);
            this.gboxProperty.Controls.Add(this.btnModelGenerate);
            this.gboxProperty.Controls.Add(this.btnConnection);
            this.gboxProperty.Controls.Add(this.btnViewGenerate);
            this.gboxProperty.Controls.Add(this.txtLogicClassNamePrefix);
            this.gboxProperty.Controls.Add(this.textBox16);
            this.gboxProperty.Controls.Add(this.textBox6);
            this.gboxProperty.Controls.Add(this.cboSqlDataBase);
            this.gboxProperty.Controls.Add(this.btnSave);
            this.gboxProperty.Controls.Add(this.textBox9);
            this.gboxProperty.Controls.Add(this.textBox14);
            this.gboxProperty.Controls.Add(this.cboSqlDataTable);
            this.gboxProperty.Controls.Add(this.txtStatus);
            this.gboxProperty.Location = new System.Drawing.Point(8, 5);
            this.gboxProperty.Name = "gboxProperty";
            this.gboxProperty.Size = new System.Drawing.Size(403, 617);
            this.gboxProperty.TabIndex = 1;
            this.gboxProperty.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(21, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 25);
            this.button1.TabIndex = 75;
            this.button1.Text = "C#-One-Key-Generate";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnCriteriaGenerate
            // 
            this.btnCriteriaGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCriteriaGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCriteriaGenerate.Location = new System.Drawing.Point(207, 479);
            this.btnCriteriaGenerate.Name = "btnCriteriaGenerate";
            this.btnCriteriaGenerate.Size = new System.Drawing.Size(175, 25);
            this.btnCriteriaGenerate.TabIndex = 74;
            this.btnCriteriaGenerate.Text = "C#-Criteria-Generate";
            this.btnCriteriaGenerate.UseVisualStyleBackColor = true;
            this.btnCriteriaGenerate.Click += new System.EventHandler(this.btnCriteriaGenerate_Click);
            // 
            // btnIDALGenerate
            // 
            this.btnIDALGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIDALGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIDALGenerate.Location = new System.Drawing.Point(21, 541);
            this.btnIDALGenerate.Name = "btnIDALGenerate";
            this.btnIDALGenerate.Size = new System.Drawing.Size(175, 25);
            this.btnIDALGenerate.TabIndex = 73;
            this.btnIDALGenerate.Text = "C#-IDAL-Generate";
            this.btnIDALGenerate.UseVisualStyleBackColor = true;
            this.btnIDALGenerate.Click += new System.EventHandler(this.btnIDALGenerate_Click);
            // 
            // cboSqlSelectAll
            // 
            this.cboSqlSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlSelectAll.DisplayMember = "name";
            this.cboSqlSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlSelectAll.FormattingEnabled = true;
            this.cboSqlSelectAll.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSqlSelectAll.Location = new System.Drawing.Point(147, 190);
            this.cboSqlSelectAll.Name = "cboSqlSelectAll";
            this.cboSqlSelectAll.Size = new System.Drawing.Size(233, 20);
            this.cboSqlSelectAll.TabIndex = 72;
            this.cboSqlSelectAll.Text = "True";
            this.cboSqlSelectAll.ValueMember = "id";
            // 
            // textBox22
            // 
            this.textBox22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox22.Location = new System.Drawing.Point(146, 187);
            this.textBox22.Multiline = true;
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(236, 25);
            this.textBox22.TabIndex = 71;
            // 
            // textBox23
            // 
            this.textBox23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox23.Location = new System.Drawing.Point(21, 187);
            this.textBox23.Multiline = true;
            this.textBox23.Name = "textBox23";
            this.textBox23.ReadOnly = true;
            this.textBox23.Size = new System.Drawing.Size(126, 25);
            this.textBox23.TabIndex = 70;
            this.textBox23.Text = "Sql-SelectAll";
            // 
            // cboSqlSelectPager
            // 
            this.cboSqlSelectPager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlSelectPager.DisplayMember = "name";
            this.cboSqlSelectPager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlSelectPager.FormattingEnabled = true;
            this.cboSqlSelectPager.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSqlSelectPager.Location = new System.Drawing.Point(147, 166);
            this.cboSqlSelectPager.Name = "cboSqlSelectPager";
            this.cboSqlSelectPager.Size = new System.Drawing.Size(233, 20);
            this.cboSqlSelectPager.TabIndex = 69;
            this.cboSqlSelectPager.Text = "True";
            this.cboSqlSelectPager.ValueMember = "id";
            // 
            // textBox15
            // 
            this.textBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox15.Location = new System.Drawing.Point(146, 163);
            this.textBox15.Multiline = true;
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(236, 25);
            this.textBox15.TabIndex = 68;
            // 
            // textBox21
            // 
            this.textBox21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox21.Location = new System.Drawing.Point(21, 163);
            this.textBox21.Multiline = true;
            this.textBox21.Name = "textBox21";
            this.textBox21.ReadOnly = true;
            this.textBox21.Size = new System.Drawing.Size(126, 25);
            this.textBox21.TabIndex = 67;
            this.textBox21.Text = "Sql-SelectPager";
            // 
            // txtSqlParameterPrefix
            // 
            this.txtSqlParameterPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlParameterPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlParameterPrefix.Location = new System.Drawing.Point(146, 403);
            this.txtSqlParameterPrefix.Multiline = true;
            this.txtSqlParameterPrefix.Name = "txtSqlParameterPrefix";
            this.txtSqlParameterPrefix.Size = new System.Drawing.Size(236, 25);
            this.txtSqlParameterPrefix.TabIndex = 66;
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Location = new System.Drawing.Point(21, 403);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(126, 25);
            this.textBox8.TabIndex = 65;
            this.textBox8.Text = "Sql-ParameterPrefix";
            // 
            // txtModelClassNamePrefix
            // 
            this.txtModelClassNamePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModelClassNamePrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelClassNamePrefix.Location = new System.Drawing.Point(146, 259);
            this.txtModelClassNamePrefix.Multiline = true;
            this.txtModelClassNamePrefix.Name = "txtModelClassNamePrefix";
            this.txtModelClassNamePrefix.Size = new System.Drawing.Size(236, 25);
            this.txtModelClassNamePrefix.TabIndex = 64;
            this.txtModelClassNamePrefix.Text = "Model";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Location = new System.Drawing.Point(21, 259);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(126, 25);
            this.textBox4.TabIndex = 63;
            this.textBox4.Text = "ModelClassNamePrefix";
            // 
            // btnSetConfig
            // 
            this.btnSetConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetConfig.Location = new System.Drawing.Point(207, 572);
            this.btnSetConfig.Name = "btnSetConfig";
            this.btnSetConfig.Size = new System.Drawing.Size(175, 25);
            this.btnSetConfig.TabIndex = 62;
            this.btnSetConfig.Text = "Set Config";
            this.btnSetConfig.UseVisualStyleBackColor = true;
            this.btnSetConfig.Click += new System.EventHandler(this.btnSetConfig_Click);
            // 
            // txtFacadeNamespace
            // 
            this.txtFacadeNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFacadeNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFacadeNamespace.Location = new System.Drawing.Point(146, 331);
            this.txtFacadeNamespace.Multiline = true;
            this.txtFacadeNamespace.Name = "txtFacadeNamespace";
            this.txtFacadeNamespace.Size = new System.Drawing.Size(236, 25);
            this.txtFacadeNamespace.TabIndex = 61;
            this.txtFacadeNamespace.Text = "Facade";
            // 
            // textBox19
            // 
            this.textBox19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox19.Location = new System.Drawing.Point(21, 331);
            this.textBox19.Multiline = true;
            this.textBox19.Name = "textBox19";
            this.textBox19.ReadOnly = true;
            this.textBox19.Size = new System.Drawing.Size(126, 25);
            this.textBox19.TabIndex = 60;
            this.textBox19.Text = "FacadeNamespace";
            // 
            // txtSqlTablePrefix
            // 
            this.txtSqlTablePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSqlTablePrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSqlTablePrefix.Location = new System.Drawing.Point(146, 67);
            this.txtSqlTablePrefix.Multiline = true;
            this.txtSqlTablePrefix.Name = "txtSqlTablePrefix";
            this.txtSqlTablePrefix.Size = new System.Drawing.Size(236, 25);
            this.txtSqlTablePrefix.TabIndex = 59;
            // 
            // textBox17
            // 
            this.textBox17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox17.Location = new System.Drawing.Point(21, 67);
            this.textBox17.Multiline = true;
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new System.Drawing.Size(126, 25);
            this.textBox17.TabIndex = 58;
            this.textBox17.Text = "Sql-TablePrefix";
            // 
            // txtFacadeClassNameSurfix
            // 
            this.txtFacadeClassNameSurfix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFacadeClassNameSurfix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFacadeClassNameSurfix.Location = new System.Drawing.Point(146, 355);
            this.txtFacadeClassNameSurfix.Multiline = true;
            this.txtFacadeClassNameSurfix.Name = "txtFacadeClassNameSurfix";
            this.txtFacadeClassNameSurfix.Size = new System.Drawing.Size(236, 25);
            this.txtFacadeClassNameSurfix.TabIndex = 56;
            this.txtFacadeClassNameSurfix.Text = "Facade";
            // 
            // textBox20
            // 
            this.textBox20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox20.Location = new System.Drawing.Point(21, 355);
            this.textBox20.Multiline = true;
            this.textBox20.Name = "textBox20";
            this.textBox20.ReadOnly = true;
            this.textBox20.Size = new System.Drawing.Size(126, 25);
            this.textBox20.TabIndex = 55;
            this.textBox20.Text = "FacadeClassPrefix";
            // 
            // txtLogicNamespace
            // 
            this.txtLogicNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogicNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogicNamespace.Location = new System.Drawing.Point(146, 283);
            this.txtLogicNamespace.Multiline = true;
            this.txtLogicNamespace.Name = "txtLogicNamespace";
            this.txtLogicNamespace.Size = new System.Drawing.Size(236, 25);
            this.txtLogicNamespace.TabIndex = 54;
            this.txtLogicNamespace.Text = "Logic";
            // 
            // textBox18
            // 
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox18.Location = new System.Drawing.Point(21, 283);
            this.textBox18.Multiline = true;
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(126, 25);
            this.textBox18.TabIndex = 53;
            this.textBox18.Text = "LogicNamespace";
            // 
            // btnSqlGenerate
            // 
            this.btnSqlGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSqlGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSqlGenerate.Location = new System.Drawing.Point(207, 447);
            this.btnSqlGenerate.Name = "btnSqlGenerate";
            this.btnSqlGenerate.Size = new System.Drawing.Size(175, 25);
            this.btnSqlGenerate.TabIndex = 52;
            this.btnSqlGenerate.Text = "Sql-Generate";
            this.btnSqlGenerate.UseVisualStyleBackColor = true;
            this.btnSqlGenerate.Click += new System.EventHandler(this.btnSqlGenerate_Click);
            // 
            // btnDalGenerate
            // 
            this.btnDalGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDalGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDalGenerate.Location = new System.Drawing.Point(207, 541);
            this.btnDalGenerate.Name = "btnDalGenerate";
            this.btnDalGenerate.Size = new System.Drawing.Size(175, 25);
            this.btnDalGenerate.TabIndex = 51;
            this.btnDalGenerate.Text = "C#-DAL-Generate";
            this.btnDalGenerate.UseVisualStyleBackColor = true;
            this.btnDalGenerate.Click += new System.EventHandler(this.btnDalGenerate_Click);
            // 
            // cboSqlUpdateStatus
            // 
            this.cboSqlUpdateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlUpdateStatus.DisplayMember = "name";
            this.cboSqlUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlUpdateStatus.FormattingEnabled = true;
            this.cboSqlUpdateStatus.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSqlUpdateStatus.Location = new System.Drawing.Point(148, 141);
            this.cboSqlUpdateStatus.Name = "cboSqlUpdateStatus";
            this.cboSqlUpdateStatus.Size = new System.Drawing.Size(233, 20);
            this.cboSqlUpdateStatus.TabIndex = 50;
            this.cboSqlUpdateStatus.Text = "True";
            this.cboSqlUpdateStatus.ValueMember = "id";
            // 
            // textBox12
            // 
            this.textBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox12.Location = new System.Drawing.Point(146, 139);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(236, 25);
            this.textBox12.TabIndex = 49;
            // 
            // textBox13
            // 
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox13.Location = new System.Drawing.Point(21, 139);
            this.textBox13.Multiline = true;
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(126, 25);
            this.textBox13.TabIndex = 48;
            this.textBox13.Text = "Sql-UpdateStatus";
            // 
            // cboSqlSelectDetail
            // 
            this.cboSqlSelectDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlSelectDetail.DisplayMember = "name";
            this.cboSqlSelectDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlSelectDetail.FormattingEnabled = true;
            this.cboSqlSelectDetail.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSqlSelectDetail.Location = new System.Drawing.Point(148, 117);
            this.cboSqlSelectDetail.Name = "cboSqlSelectDetail";
            this.cboSqlSelectDetail.Size = new System.Drawing.Size(233, 20);
            this.cboSqlSelectDetail.TabIndex = 47;
            this.cboSqlSelectDetail.Text = "True";
            this.cboSqlSelectDetail.ValueMember = "id";
            // 
            // textBox10
            // 
            this.textBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox10.Location = new System.Drawing.Point(146, 115);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(236, 25);
            this.textBox10.TabIndex = 46;
            // 
            // textBox11
            // 
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox11.Location = new System.Drawing.Point(21, 115);
            this.textBox11.Multiline = true;
            this.textBox11.Name = "textBox11";
            this.textBox11.ReadOnly = true;
            this.textBox11.Size = new System.Drawing.Size(126, 25);
            this.textBox11.TabIndex = 45;
            this.textBox11.Text = "Sql-SelectDetail";
            // 
            // cboSqlInsertUpdate
            // 
            this.cboSqlInsertUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlInsertUpdate.DisplayMember = "name";
            this.cboSqlInsertUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlInsertUpdate.FormattingEnabled = true;
            this.cboSqlInsertUpdate.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSqlInsertUpdate.Location = new System.Drawing.Point(148, 93);
            this.cboSqlInsertUpdate.Name = "cboSqlInsertUpdate";
            this.cboSqlInsertUpdate.Size = new System.Drawing.Size(233, 20);
            this.cboSqlInsertUpdate.TabIndex = 44;
            this.cboSqlInsertUpdate.Text = "True";
            this.cboSqlInsertUpdate.ValueMember = "id";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Location = new System.Drawing.Point(146, 91);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(236, 25);
            this.textBox2.TabIndex = 43;
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Location = new System.Drawing.Point(21, 91);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(126, 25);
            this.textBox7.TabIndex = 42;
            this.textBox7.Text = "Sql-InsertUpdate";
            // 
            // txtProcedurePrefix
            // 
            this.txtProcedurePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProcedurePrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProcedurePrefix.Location = new System.Drawing.Point(146, 379);
            this.txtProcedurePrefix.Multiline = true;
            this.txtProcedurePrefix.Name = "txtProcedurePrefix";
            this.txtProcedurePrefix.Size = new System.Drawing.Size(236, 25);
            this.txtProcedurePrefix.TabIndex = 41;
            this.txtProcedurePrefix.Text = "usp_";
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Location = new System.Drawing.Point(21, 379);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(126, 25);
            this.textBox5.TabIndex = 40;
            this.textBox5.Text = "Sql-ProcedurePrefix";
            // 
            // txtModelsNamespace
            // 
            this.txtModelsNamespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtModelsNamespace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtModelsNamespace.Location = new System.Drawing.Point(146, 235);
            this.txtModelsNamespace.Multiline = true;
            this.txtModelsNamespace.Name = "txtModelsNamespace";
            this.txtModelsNamespace.Size = new System.Drawing.Size(236, 25);
            this.txtModelsNamespace.TabIndex = 39;
            this.txtModelsNamespace.Text = "Model";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Location = new System.Drawing.Point(21, 235);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(126, 25);
            this.textBox3.TabIndex = 38;
            this.textBox3.Text = "ModelsNamespace";
            // 
            // btnModelGenerate
            // 
            this.btnModelGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModelGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModelGenerate.Location = new System.Drawing.Point(21, 510);
            this.btnModelGenerate.Name = "btnModelGenerate";
            this.btnModelGenerate.Size = new System.Drawing.Size(175, 25);
            this.btnModelGenerate.TabIndex = 37;
            this.btnModelGenerate.Text = "C#-Model-Generate";
            this.btnModelGenerate.UseVisualStyleBackColor = true;
            this.btnModelGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnConnection
            // 
            this.btnConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnection.Location = new System.Drawing.Point(21, 447);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(175, 25);
            this.btnConnection.TabIndex = 36;
            this.btnConnection.Text = "Connection";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // btnViewGenerate
            // 
            this.btnViewGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewGenerate.Location = new System.Drawing.Point(207, 510);
            this.btnViewGenerate.Name = "btnViewGenerate";
            this.btnViewGenerate.Size = new System.Drawing.Size(175, 25);
            this.btnViewGenerate.TabIndex = 35;
            this.btnViewGenerate.Text = "View-Generate";
            this.btnViewGenerate.UseVisualStyleBackColor = true;
            this.btnViewGenerate.Click += new System.EventHandler(this.btnViewGenerate_Click);
            // 
            // txtLogicClassNamePrefix
            // 
            this.txtLogicClassNamePrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogicClassNamePrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogicClassNamePrefix.Location = new System.Drawing.Point(146, 307);
            this.txtLogicClassNamePrefix.Multiline = true;
            this.txtLogicClassNamePrefix.Name = "txtLogicClassNamePrefix";
            this.txtLogicClassNamePrefix.Size = new System.Drawing.Size(236, 25);
            this.txtLogicClassNamePrefix.TabIndex = 27;
            this.txtLogicClassNamePrefix.Text = "Logic";
            // 
            // textBox16
            // 
            this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox16.Location = new System.Drawing.Point(21, 43);
            this.textBox16.Multiline = true;
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(126, 25);
            this.textBox16.TabIndex = 24;
            this.textBox16.Text = "SqlDataTable";
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Location = new System.Drawing.Point(21, 19);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(126, 25);
            this.textBox6.TabIndex = 22;
            this.textBox6.Text = "SqlDataBase";
            // 
            // cboSqlDataBase
            // 
            this.cboSqlDataBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlDataBase.DisplayMember = "name";
            this.cboSqlDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlDataBase.FormattingEnabled = true;
            this.cboSqlDataBase.Location = new System.Drawing.Point(148, 21);
            this.cboSqlDataBase.Name = "cboSqlDataBase";
            this.cboSqlDataBase.Size = new System.Drawing.Size(233, 20);
            this.cboSqlDataBase.TabIndex = 33;
            this.cboSqlDataBase.ValueMember = "name";
            this.cboSqlDataBase.SelectedIndexChanged += new System.EventHandler(this.cboSqlDataBase_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(21, 572);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 25);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save AS";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox9.Location = new System.Drawing.Point(146, 19);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(236, 25);
            this.textBox9.TabIndex = 31;
            this.textBox9.Text = "Service";
            // 
            // textBox14
            // 
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Location = new System.Drawing.Point(21, 307);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(126, 25);
            this.textBox14.TabIndex = 26;
            this.textBox14.Text = "LogicClassNamePrefix";
            // 
            // cboSqlDataTable
            // 
            this.cboSqlDataTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlDataTable.DisplayMember = "name";
            this.cboSqlDataTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlDataTable.FormattingEnabled = true;
            this.cboSqlDataTable.Location = new System.Drawing.Point(148, 45);
            this.cboSqlDataTable.Name = "cboSqlDataTable";
            this.cboSqlDataTable.Size = new System.Drawing.Size(233, 20);
            this.cboSqlDataTable.TabIndex = 34;
            this.cboSqlDataTable.ValueMember = "id";
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Location = new System.Drawing.Point(146, 43);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(236, 25);
            this.txtStatus.TabIndex = 25;
            this.txtStatus.Text = "NULL";
            // 
            // gboxContent
            // 
            this.gboxContent.Controls.Add(this.rtxtCont);
            this.gboxContent.Location = new System.Drawing.Point(415, 5);
            this.gboxContent.Name = "gboxContent";
            this.gboxContent.Size = new System.Drawing.Size(536, 617);
            this.gboxContent.TabIndex = 36;
            this.gboxContent.TabStop = false;
            this.gboxContent.Visible = false;
            // 
            // rtxtCont
            // 
            this.rtxtCont.BackColor = System.Drawing.Color.White;
            this.rtxtCont.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtCont.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtCont.Location = new System.Drawing.Point(3, 17);
            this.rtxtCont.Name = "rtxtCont";
            this.rtxtCont.Size = new System.Drawing.Size(530, 597);
            this.rtxtCont.TabIndex = 0;
            this.rtxtCont.Text = "";
            // 
            // sfdCode
            // 
            this.sfdCode.Filter = "C#文件|*.cs|Sql文件|*.sql|所有文件|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 626);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(414, 22);
            this.statusStrip1.TabIndex = 37;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cboSqlDelete
            // 
            this.cboSqlDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSqlDelete.DisplayMember = "name";
            this.cboSqlDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSqlDelete.FormattingEnabled = true;
            this.cboSqlDelete.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cboSqlDelete.Location = new System.Drawing.Point(147, 214);
            this.cboSqlDelete.Name = "cboSqlDelete";
            this.cboSqlDelete.Size = new System.Drawing.Size(233, 20);
            this.cboSqlDelete.TabIndex = 78;
            this.cboSqlDelete.Text = "True";
            this.cboSqlDelete.ValueMember = "id";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(146, 211);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(236, 25);
            this.textBox1.TabIndex = 77;
            // 
            // textBox24
            // 
            this.textBox24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox24.Location = new System.Drawing.Point(21, 211);
            this.textBox24.Multiline = true;
            this.textBox24.Name = "textBox24";
            this.textBox24.ReadOnly = true;
            this.textBox24.Size = new System.Drawing.Size(126, 25);
            this.textBox24.TabIndex = 76;
            this.textBox24.Text = "Sql-Delete";
            // 
            // ModuleCodeGeneration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(414, 648);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gboxContent);
            this.Controls.Add(this.gboxProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModuleCodeGeneration";
            this.Text = "CodeGeneration";
            this.Load += new System.EventHandler(this.ModuleCodeGeneration_Load);
            this.gboxProperty.ResumeLayout(false);
            this.gboxProperty.PerformLayout();
            this.gboxContent.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gboxProperty;
        private System.Windows.Forms.Button btnViewGenerate;
        private System.Windows.Forms.TextBox txtLogicClassNamePrefix;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.ComboBox cboSqlDataTable;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.ComboBox cboSqlDataBase;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.GroupBox gboxContent;
        private System.Windows.Forms.Button btnModelGenerate;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.RichTextBox rtxtCont;
        private System.Windows.Forms.TextBox txtModelsNamespace;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.SaveFileDialog sfdCode;
        private System.Windows.Forms.TextBox txtProcedurePrefix;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox cboSqlInsertUpdate;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button btnSqlGenerate;
        private System.Windows.Forms.Button btnDalGenerate;
        private System.Windows.Forms.ComboBox cboSqlUpdateStatus;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.ComboBox cboSqlSelectDetail;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox txtFacadeClassNameSurfix;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox txtLogicNamespace;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox txtSqlTablePrefix;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox txtFacadeNamespace;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnSetConfig;
        private System.Windows.Forms.TextBox txtModelClassNamePrefix;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtSqlParameterPrefix;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.ComboBox cboSqlSelectAll;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.ComboBox cboSqlSelectPager;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.Button btnIDALGenerate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCriteriaGenerate;
        private System.Windows.Forms.ComboBox cboSqlDelete;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox24;


    }
}