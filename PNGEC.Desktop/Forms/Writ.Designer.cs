namespace PNGEC.Desktop.Forms
{
    partial class Writ
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblWriteTitle = new System.Windows.Forms.Label();
            this.tabStep2 = new System.Windows.Forms.TabPage();
            this.cmbScop = new System.Windows.Forms.ComboBox();
            this.lblScop = new System.Windows.Forms.Label();
            this.cmbWard = new System.Windows.Forms.ComboBox();
            this.lblWard = new System.Windows.Forms.Label();
            this.cmbLLG = new System.Windows.Forms.ComboBox();
            this.lblLLG = new System.Windows.Forms.Label();
            this.cmbDistrict = new System.Windows.Forms.ComboBox();
            this.lblDistrict = new System.Windows.Forms.Label();
            this.cmbProvince = new System.Windows.Forms.ComboBox();
            this.lblProvince = new System.Windows.Forms.Label();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.lblRegion = new System.Windows.Forms.Label();
            this.tabStep1 = new System.Windows.Forms.TabPage();
            this.dtpLastDayOfPolling = new System.Windows.Forms.DateTimePicker();
            this.dtpFirstDayofPolling = new System.Windows.Forms.DateTimePicker();
            this.txtIssuerTitle = new System.Windows.Forms.TextBox();
            this.txtIssuerName = new System.Windows.Forms.TextBox();
            this.dtpIssueDate = new System.Windows.Forms.DateTimePicker();
            this.lblIssueDate = new System.Windows.Forms.Label();
            this.dtpReturnofWritDate = new System.Windows.Forms.DateTimePicker();
            this.lblReturnofWritDate = new System.Windows.Forms.Label();
            this.lblLastDayOfPolling = new System.Windows.Forms.Label();
            this.lblFirstDayOfPolling = new System.Windows.Forms.Label();
            this.dtpNomiEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpNomiStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblNomiEndDate = new System.Windows.Forms.Label();
            this.cmbElectionType = new System.Windows.Forms.ComboBox();
            this.txtElectionName = new System.Windows.Forms.TextBox();
            this.lblElectionName = new System.Windows.Forms.Label();
            this.lblNominStartDate = new System.Windows.Forms.Label();
            this.lblIssuerTitle = new System.Windows.Forms.Label();
            this.lblIssuerName = new System.Windows.Forms.Label();
            this.lblElectionType = new System.Windows.Forms.Label();
            this.tabWrit = new System.Windows.Forms.TabControl();
            this.tabStep3 = new System.Windows.Forms.TabPage();
            this.pnlNameOfRO = new System.Windows.Forms.Panel();
            this.txtNameOfRO = new System.Windows.Forms.TextBox();
            this.lblNameOfRO = new System.Windows.Forms.Label();
            this.tabStep4 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkLockWrit = new System.Windows.Forms.CheckBox();
            this.lblMesssage = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.tabStep2.SuspendLayout();
            this.tabStep1.SuspendLayout();
            this.tabWrit.SuspendLayout();
            this.tabStep3.SuspendLayout();
            this.tabStep4.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.AutoSize = true;
            this.pnlBottom.Controls.Add(this.btnNext);
            this.pnlBottom.Controls.Add(this.btnPrevious);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 586);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(878, 61);
            this.pnlBottom.TabIndex = 14;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Black;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Font = new System.Drawing.Font("Arial", 10F);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(675, 7);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(195, 50);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Black;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.Font = new System.Drawing.Font("Arial", 10F);
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.Location = new System.Drawing.Point(482, 7);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(185, 50);
            this.btnPrevious.TabIndex = 13;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblWriteTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(878, 65);
            this.pnlTop.TabIndex = 15;
            // 
            // lblWriteTitle
            // 
            this.lblWriteTitle.AutoSize = true;
            this.lblWriteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWriteTitle.Location = new System.Drawing.Point(21, 21);
            this.lblWriteTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWriteTitle.Name = "lblWriteTitle";
            this.lblWriteTitle.Size = new System.Drawing.Size(183, 29);
            this.lblWriteTitle.TabIndex = 1;
            this.lblWriteTitle.Text = "Writ Generator";
            // 
            // tabStep2
            // 
            this.tabStep2.Controls.Add(this.cmbScop);
            this.tabStep2.Controls.Add(this.lblScop);
            this.tabStep2.Controls.Add(this.cmbWard);
            this.tabStep2.Controls.Add(this.lblWard);
            this.tabStep2.Controls.Add(this.cmbLLG);
            this.tabStep2.Controls.Add(this.lblLLG);
            this.tabStep2.Controls.Add(this.cmbDistrict);
            this.tabStep2.Controls.Add(this.lblDistrict);
            this.tabStep2.Controls.Add(this.cmbProvince);
            this.tabStep2.Controls.Add(this.lblProvince);
            this.tabStep2.Controls.Add(this.cmbRegion);
            this.tabStep2.Controls.Add(this.lblRegion);
            this.tabStep2.Location = new System.Drawing.Point(4, 31);
            this.tabStep2.Name = "tabStep2";
            this.tabStep2.Padding = new System.Windows.Forms.Padding(3);
            this.tabStep2.Size = new System.Drawing.Size(870, 486);
            this.tabStep2.TabIndex = 1;
            this.tabStep2.Text = "Step 2";
            this.tabStep2.UseVisualStyleBackColor = true;
            // 
            // cmbScop
            // 
            this.cmbScop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbScop.FormattingEnabled = true;
            this.cmbScop.Location = new System.Drawing.Point(249, 25);
            this.cmbScop.Margin = new System.Windows.Forms.Padding(4);
            this.cmbScop.Name = "cmbScop";
            this.cmbScop.Size = new System.Drawing.Size(593, 33);
            this.cmbScop.TabIndex = 36;
            // 
            // lblScop
            // 
            this.lblScop.AutoSize = true;
            this.lblScop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScop.Location = new System.Drawing.Point(36, 25);
            this.lblScop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScop.Name = "lblScop";
            this.lblScop.Size = new System.Drawing.Size(69, 25);
            this.lblScop.TabIndex = 35;
            this.lblScop.Text = "Scope";
            // 
            // cmbWard
            // 
            this.cmbWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWard.FormattingEnabled = true;
            this.cmbWard.Location = new System.Drawing.Point(249, 308);
            this.cmbWard.Margin = new System.Windows.Forms.Padding(4);
            this.cmbWard.Name = "cmbWard";
            this.cmbWard.Size = new System.Drawing.Size(593, 33);
            this.cmbWard.TabIndex = 34;
            // 
            // lblWard
            // 
            this.lblWard.AutoSize = true;
            this.lblWard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWard.Location = new System.Drawing.Point(36, 308);
            this.lblWard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWard.Name = "lblWard";
            this.lblWard.Size = new System.Drawing.Size(60, 25);
            this.lblWard.TabIndex = 33;
            this.lblWard.Text = "Ward";
            // 
            // cmbLLG
            // 
            this.cmbLLG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLLG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLLG.FormattingEnabled = true;
            this.cmbLLG.Location = new System.Drawing.Point(249, 251);
            this.cmbLLG.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLLG.Name = "cmbLLG";
            this.cmbLLG.Size = new System.Drawing.Size(593, 33);
            this.cmbLLG.TabIndex = 32;
            this.cmbLLG.SelectedIndexChanged += new System.EventHandler(this.cmbLLG_SelectedIndexChanged);
            // 
            // lblLLG
            // 
            this.lblLLG.AutoSize = true;
            this.lblLLG.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLLG.Location = new System.Drawing.Point(36, 251);
            this.lblLLG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLLG.Name = "lblLLG";
            this.lblLLG.Size = new System.Drawing.Size(49, 25);
            this.lblLLG.TabIndex = 31;
            this.lblLLG.Text = "LLG";
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrict.FormattingEnabled = true;
            this.cmbDistrict.Location = new System.Drawing.Point(249, 194);
            this.cmbDistrict.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.Size = new System.Drawing.Size(593, 33);
            this.cmbDistrict.TabIndex = 30;
            this.cmbDistrict.SelectedIndexChanged += new System.EventHandler(this.cmbDistrictName_SelectedIndexChanged);
            // 
            // lblDistrict
            // 
            this.lblDistrict.AutoSize = true;
            this.lblDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrict.Location = new System.Drawing.Point(36, 194);
            this.lblDistrict.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDistrict.Name = "lblDistrict";
            this.lblDistrict.Size = new System.Drawing.Size(70, 25);
            this.lblDistrict.TabIndex = 29;
            this.lblDistrict.Text = "District";
            // 
            // cmbProvince
            // 
            this.cmbProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Location = new System.Drawing.Point(249, 137);
            this.cmbProvince.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(593, 33);
            this.cmbProvince.TabIndex = 28;
            this.cmbProvince.SelectedIndexChanged += new System.EventHandler(this.cmbProvinceName_SelectedIndexChanged);
            // 
            // lblProvince
            // 
            this.lblProvince.AutoSize = true;
            this.lblProvince.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvince.Location = new System.Drawing.Point(36, 137);
            this.lblProvince.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProvince.Name = "lblProvince";
            this.lblProvince.Size = new System.Drawing.Size(88, 25);
            this.lblProvince.TabIndex = 27;
            this.lblProvince.Text = "Province";
            // 
            // cmbRegion
            // 
            this.cmbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Location = new System.Drawing.Point(249, 80);
            this.cmbRegion.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(593, 33);
            this.cmbRegion.TabIndex = 26;
            this.cmbRegion.SelectedIndexChanged += new System.EventHandler(this.cmbRegion_SelectedIndexChanged);
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegion.Location = new System.Drawing.Point(36, 80);
            this.lblRegion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(73, 25);
            this.lblRegion.TabIndex = 25;
            this.lblRegion.Text = "Region";
            // 
            // tabStep1
            // 
            this.tabStep1.Controls.Add(this.dtpLastDayOfPolling);
            this.tabStep1.Controls.Add(this.dtpFirstDayofPolling);
            this.tabStep1.Controls.Add(this.txtIssuerTitle);
            this.tabStep1.Controls.Add(this.txtIssuerName);
            this.tabStep1.Controls.Add(this.dtpIssueDate);
            this.tabStep1.Controls.Add(this.lblIssueDate);
            this.tabStep1.Controls.Add(this.dtpReturnofWritDate);
            this.tabStep1.Controls.Add(this.lblReturnofWritDate);
            this.tabStep1.Controls.Add(this.lblLastDayOfPolling);
            this.tabStep1.Controls.Add(this.lblFirstDayOfPolling);
            this.tabStep1.Controls.Add(this.dtpNomiEndDate);
            this.tabStep1.Controls.Add(this.dtpNomiStartDate);
            this.tabStep1.Controls.Add(this.lblNomiEndDate);
            this.tabStep1.Controls.Add(this.cmbElectionType);
            this.tabStep1.Controls.Add(this.txtElectionName);
            this.tabStep1.Controls.Add(this.lblElectionName);
            this.tabStep1.Controls.Add(this.lblNominStartDate);
            this.tabStep1.Controls.Add(this.lblIssuerTitle);
            this.tabStep1.Controls.Add(this.lblIssuerName);
            this.tabStep1.Controls.Add(this.lblElectionType);
            this.tabStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabStep1.Location = new System.Drawing.Point(4, 31);
            this.tabStep1.Name = "tabStep1";
            this.tabStep1.Padding = new System.Windows.Forms.Padding(3);
            this.tabStep1.Size = new System.Drawing.Size(870, 486);
            this.tabStep1.TabIndex = 0;
            this.tabStep1.Text = "Step 1";
            this.tabStep1.UseVisualStyleBackColor = true;
            // 
            // dtpLastDayOfPolling
            // 
            this.dtpLastDayOfPolling.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLastDayOfPolling.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpLastDayOfPolling.Location = new System.Drawing.Point(667, 196);
            this.dtpLastDayOfPolling.Name = "dtpLastDayOfPolling";
            this.dtpLastDayOfPolling.Size = new System.Drawing.Size(182, 28);
            this.dtpLastDayOfPolling.TabIndex = 6;
            this.dtpLastDayOfPolling.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // dtpFirstDayofPolling
            // 
            this.dtpFirstDayofPolling.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFirstDayofPolling.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFirstDayofPolling.Location = new System.Drawing.Point(249, 196);
            this.dtpFirstDayofPolling.Name = "dtpFirstDayofPolling";
            this.dtpFirstDayofPolling.Size = new System.Drawing.Size(182, 28);
            this.dtpFirstDayofPolling.TabIndex = 5;
            this.dtpFirstDayofPolling.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // txtIssuerTitle
            // 
            this.txtIssuerTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssuerTitle.Location = new System.Drawing.Point(249, 424);
            this.txtIssuerTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtIssuerTitle.Name = "txtIssuerTitle";
            this.txtIssuerTitle.Size = new System.Drawing.Size(600, 30);
            this.txtIssuerTitle.TabIndex = 10;
            // 
            // txtIssuerName
            // 
            this.txtIssuerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIssuerName.Location = new System.Drawing.Point(249, 367);
            this.txtIssuerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtIssuerName.Name = "txtIssuerName";
            this.txtIssuerName.Size = new System.Drawing.Size(600, 30);
            this.txtIssuerName.TabIndex = 9;
            // 
            // dtpIssueDate
            // 
            this.dtpIssueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssueDate.Location = new System.Drawing.Point(248, 310);
            this.dtpIssueDate.Name = "dtpIssueDate";
            this.dtpIssueDate.Size = new System.Drawing.Size(182, 28);
            this.dtpIssueDate.TabIndex = 8;
            this.dtpIssueDate.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // lblIssueDate
            // 
            this.lblIssueDate.AutoSize = true;
            this.lblIssueDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssueDate.Location = new System.Drawing.Point(29, 310);
            this.lblIssueDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIssueDate.Name = "lblIssueDate";
            this.lblIssueDate.Size = new System.Drawing.Size(105, 25);
            this.lblIssueDate.TabIndex = 38;
            this.lblIssueDate.Text = "Issue Date";
            // 
            // dtpReturnofWritDate
            // 
            this.dtpReturnofWritDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnofWritDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReturnofWritDate.Location = new System.Drawing.Point(249, 253);
            this.dtpReturnofWritDate.Name = "dtpReturnofWritDate";
            this.dtpReturnofWritDate.Size = new System.Drawing.Size(182, 28);
            this.dtpReturnofWritDate.TabIndex = 7;
            this.dtpReturnofWritDate.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // lblReturnofWritDate
            // 
            this.lblReturnofWritDate.AutoSize = true;
            this.lblReturnofWritDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnofWritDate.Location = new System.Drawing.Point(29, 253);
            this.lblReturnofWritDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReturnofWritDate.Name = "lblReturnofWritDate";
            this.lblReturnofWritDate.Size = new System.Drawing.Size(176, 25);
            this.lblReturnofWritDate.TabIndex = 34;
            this.lblReturnofWritDate.Text = "Return of Writ Date";
            // 
            // lblLastDayOfPolling
            // 
            this.lblLastDayOfPolling.AutoSize = true;
            this.lblLastDayOfPolling.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastDayOfPolling.Location = new System.Drawing.Point(454, 196);
            this.lblLastDayOfPolling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastDayOfPolling.Name = "lblLastDayOfPolling";
            this.lblLastDayOfPolling.Size = new System.Drawing.Size(173, 25);
            this.lblLastDayOfPolling.TabIndex = 29;
            this.lblLastDayOfPolling.Text = "Last Day of Polling";
            // 
            // lblFirstDayOfPolling
            // 
            this.lblFirstDayOfPolling.AutoSize = true;
            this.lblFirstDayOfPolling.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstDayOfPolling.Location = new System.Drawing.Point(29, 196);
            this.lblFirstDayOfPolling.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFirstDayOfPolling.Name = "lblFirstDayOfPolling";
            this.lblFirstDayOfPolling.Size = new System.Drawing.Size(173, 25);
            this.lblFirstDayOfPolling.TabIndex = 28;
            this.lblFirstDayOfPolling.Text = "First Day of Polling";
            // 
            // dtpNomiEndDate
            // 
            this.dtpNomiEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNomiEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNomiEndDate.Location = new System.Drawing.Point(667, 139);
            this.dtpNomiEndDate.Name = "dtpNomiEndDate";
            this.dtpNomiEndDate.Size = new System.Drawing.Size(182, 28);
            this.dtpNomiEndDate.TabIndex = 4;
            this.dtpNomiEndDate.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // dtpNomiStartDate
            // 
            this.dtpNomiStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNomiStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNomiStartDate.Location = new System.Drawing.Point(249, 139);
            this.dtpNomiStartDate.Name = "dtpNomiStartDate";
            this.dtpNomiStartDate.Size = new System.Drawing.Size(182, 28);
            this.dtpNomiStartDate.TabIndex = 3;
            this.dtpNomiStartDate.Value = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            // 
            // lblNomiEndDate
            // 
            this.lblNomiEndDate.AutoSize = true;
            this.lblNomiEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomiEndDate.Location = new System.Drawing.Point(454, 139);
            this.lblNomiEndDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNomiEndDate.Name = "lblNomiEndDate";
            this.lblNomiEndDate.Size = new System.Drawing.Size(196, 25);
            this.lblNomiEndDate.TabIndex = 25;
            this.lblNomiEndDate.Text = "Nomination End Date";
            // 
            // cmbElectionType
            // 
            this.cmbElectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbElectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbElectionType.FormattingEnabled = true;
            this.cmbElectionType.Location = new System.Drawing.Point(249, 25);
            this.cmbElectionType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbElectionType.Name = "cmbElectionType";
            this.cmbElectionType.Size = new System.Drawing.Size(600, 28);
            this.cmbElectionType.TabIndex = 1;
            // 
            // txtElectionName
            // 
            this.txtElectionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtElectionName.Location = new System.Drawing.Point(249, 82);
            this.txtElectionName.Margin = new System.Windows.Forms.Padding(4);
            this.txtElectionName.Name = "txtElectionName";
            this.txtElectionName.Size = new System.Drawing.Size(600, 30);
            this.txtElectionName.TabIndex = 2;
            // 
            // lblElectionName
            // 
            this.lblElectionName.AutoSize = true;
            this.lblElectionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElectionName.Location = new System.Drawing.Point(29, 82);
            this.lblElectionName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElectionName.Name = "lblElectionName";
            this.lblElectionName.Size = new System.Drawing.Size(138, 25);
            this.lblElectionName.TabIndex = 14;
            this.lblElectionName.Text = "Election Name";
            // 
            // lblNominStartDate
            // 
            this.lblNominStartDate.AutoSize = true;
            this.lblNominStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNominStartDate.Location = new System.Drawing.Point(29, 139);
            this.lblNominStartDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNominStartDate.Name = "lblNominStartDate";
            this.lblNominStartDate.Size = new System.Drawing.Size(202, 25);
            this.lblNominStartDate.TabIndex = 16;
            this.lblNominStartDate.Text = "Nomination Start Date";
            // 
            // lblIssuerTitle
            // 
            this.lblIssuerTitle.AutoSize = true;
            this.lblIssuerTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssuerTitle.Location = new System.Drawing.Point(29, 424);
            this.lblIssuerTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIssuerTitle.Name = "lblIssuerTitle";
            this.lblIssuerTitle.Size = new System.Drawing.Size(107, 25);
            this.lblIssuerTitle.TabIndex = 22;
            this.lblIssuerTitle.Text = "Issuer Title";
            // 
            // lblIssuerName
            // 
            this.lblIssuerName.AutoSize = true;
            this.lblIssuerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssuerName.Location = new System.Drawing.Point(29, 367);
            this.lblIssuerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIssuerName.Name = "lblIssuerName";
            this.lblIssuerName.Size = new System.Drawing.Size(122, 25);
            this.lblIssuerName.TabIndex = 20;
            this.lblIssuerName.Text = "Issuer Name";
            // 
            // lblElectionType
            // 
            this.lblElectionType.AutoSize = true;
            this.lblElectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElectionType.Location = new System.Drawing.Point(29, 25);
            this.lblElectionType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElectionType.Name = "lblElectionType";
            this.lblElectionType.Size = new System.Drawing.Size(131, 25);
            this.lblElectionType.TabIndex = 12;
            this.lblElectionType.Text = "Election Type";
            // 
            // tabWrit
            // 
            this.tabWrit.Controls.Add(this.tabStep1);
            this.tabWrit.Controls.Add(this.tabStep2);
            this.tabWrit.Controls.Add(this.tabStep3);
            this.tabWrit.Controls.Add(this.tabStep4);
            this.tabWrit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWrit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabWrit.Location = new System.Drawing.Point(0, 65);
            this.tabWrit.Name = "tabWrit";
            this.tabWrit.SelectedIndex = 0;
            this.tabWrit.Size = new System.Drawing.Size(878, 521);
            this.tabWrit.TabIndex = 16;
            this.tabWrit.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabWrit_Selecting);
            // 
            // tabStep3
            // 
            this.tabStep3.Controls.Add(this.pnlNameOfRO);
            this.tabStep3.Controls.Add(this.txtNameOfRO);
            this.tabStep3.Controls.Add(this.lblNameOfRO);
            this.tabStep3.Location = new System.Drawing.Point(4, 31);
            this.tabStep3.Name = "tabStep3";
            this.tabStep3.Size = new System.Drawing.Size(870, 486);
            this.tabStep3.TabIndex = 2;
            this.tabStep3.Text = "Step 3";
            this.tabStep3.UseVisualStyleBackColor = true;
            // 
            // pnlNameOfRO
            // 
            this.pnlNameOfRO.AutoScroll = true;
            this.pnlNameOfRO.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNameOfRO.Location = new System.Drawing.Point(0, 71);
            this.pnlNameOfRO.Name = "pnlNameOfRO";
            this.pnlNameOfRO.Size = new System.Drawing.Size(870, 415);
            this.pnlNameOfRO.TabIndex = 18;
            // 
            // txtNameOfRO
            // 
            this.txtNameOfRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameOfRO.Location = new System.Drawing.Point(249, 25);
            this.txtNameOfRO.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameOfRO.Name = "txtNameOfRO";
            this.txtNameOfRO.Size = new System.Drawing.Size(593, 30);
            this.txtNameOfRO.TabIndex = 17;
            // 
            // lblNameOfRO
            // 
            this.lblNameOfRO.AutoSize = true;
            this.lblNameOfRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfRO.Location = new System.Drawing.Point(36, 25);
            this.lblNameOfRO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameOfRO.Name = "lblNameOfRO";
            this.lblNameOfRO.Size = new System.Drawing.Size(119, 25);
            this.lblNameOfRO.TabIndex = 16;
            this.lblNameOfRO.Text = "Name of RO";
            // 
            // tabStep4
            // 
            this.tabStep4.Controls.Add(this.btnSave);
            this.tabStep4.Controls.Add(this.chkLockWrit);
            this.tabStep4.Controls.Add(this.lblMesssage);
            this.tabStep4.Location = new System.Drawing.Point(4, 31);
            this.tabStep4.Name = "tabStep4";
            this.tabStep4.Size = new System.Drawing.Size(870, 486);
            this.tabStep4.TabIndex = 3;
            this.tabStep4.Text = "Stap 4";
            this.tabStep4.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Black;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(41, 204);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(195, 50);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkLockWrit
            // 
            this.chkLockWrit.AutoSize = true;
            this.chkLockWrit.Location = new System.Drawing.Point(41, 124);
            this.chkLockWrit.Name = "chkLockWrit";
            this.chkLockWrit.Size = new System.Drawing.Size(384, 26);
            this.chkLockWrit.TabIndex = 18;
            this.chkLockWrit.Text = "Do you wish to lock this writ after generation";
            this.chkLockWrit.UseVisualStyleBackColor = true;
            // 
            // lblMesssage
            // 
            this.lblMesssage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesssage.Location = new System.Drawing.Point(36, 25);
            this.lblMesssage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMesssage.Name = "lblMesssage";
            this.lblMesssage.Size = new System.Drawing.Size(780, 63);
            this.lblMesssage.TabIndex = 17;
            this.lblMesssage.Text = "You will only be able to generate for a by-election writ. For any other types use" +
    " the Batch Report Server.";
            // 
            // Writ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 647);
            this.Controls.Add(this.tabWrit);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Writ";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Writ";
            this.Load += new System.EventHandler(this.Writ_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabStep2.ResumeLayout(false);
            this.tabStep2.PerformLayout();
            this.tabStep1.ResumeLayout(false);
            this.tabStep1.PerformLayout();
            this.tabWrit.ResumeLayout(false);
            this.tabStep3.ResumeLayout(false);
            this.tabStep3.PerformLayout();
            this.tabStep4.ResumeLayout(false);
            this.tabStep4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblWriteTitle;
        private System.Windows.Forms.TabPage tabStep2;
        private System.Windows.Forms.TabPage tabStep1;
        private System.Windows.Forms.TabControl tabWrit;
        private System.Windows.Forms.TabPage tabStep3;
        private System.Windows.Forms.TabPage tabStep4;
        private System.Windows.Forms.TextBox txtElectionName;
        private System.Windows.Forms.Label lblElectionName;
        private System.Windows.Forms.Label lblNominStartDate;
        private System.Windows.Forms.Label lblIssuerTitle;
        private System.Windows.Forms.Label lblIssuerName;
        private System.Windows.Forms.Label lblElectionType;
        private System.Windows.Forms.ComboBox cmbElectionType;
        private System.Windows.Forms.Label lblNomiEndDate;
        private System.Windows.Forms.DateTimePicker dtpNomiStartDate;
        private System.Windows.Forms.DateTimePicker dtpNomiEndDate;
        private System.Windows.Forms.Label lblLastDayOfPolling;
        private System.Windows.Forms.Label lblFirstDayOfPolling;
        private System.Windows.Forms.DateTimePicker dtpReturnofWritDate;
        private System.Windows.Forms.Label lblReturnofWritDate;
        private System.Windows.Forms.DateTimePicker dtpIssueDate;
        private System.Windows.Forms.Label lblIssueDate;
        private System.Windows.Forms.TextBox txtIssuerName;
        private System.Windows.Forms.TextBox txtIssuerTitle;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.ComboBox cmbProvince;
        private System.Windows.Forms.Label lblProvince;
        private System.Windows.Forms.ComboBox cmbDistrict;
        private System.Windows.Forms.Label lblDistrict;
        private System.Windows.Forms.ComboBox cmbLLG;
        private System.Windows.Forms.Label lblLLG;
        private System.Windows.Forms.ComboBox cmbWard;
        private System.Windows.Forms.Label lblWard;
        private System.Windows.Forms.TextBox txtNameOfRO;
        private System.Windows.Forms.Label lblNameOfRO;
        private System.Windows.Forms.Panel pnlNameOfRO;
        private System.Windows.Forms.Label lblMesssage;
        private System.Windows.Forms.CheckBox chkLockWrit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpLastDayOfPolling;
        private System.Windows.Forms.DateTimePicker dtpFirstDayofPolling;
        private System.Windows.Forms.ComboBox cmbScop;
        private System.Windows.Forms.Label lblScop;
    }
}