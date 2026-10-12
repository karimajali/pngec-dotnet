namespace PNGEC.Desktop.Forms
{
    partial class MKAFile
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tvMKAGeoStructure = new System.Windows.Forms.TreeView();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgFile = new System.Windows.Forms.DataGridView();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlPaginationRight = new System.Windows.Forms.Panel();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.pnlPaginationLeft = new System.Windows.Forms.Panel();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.chkLookUp = new System.Windows.Forms.CheckBox();
            this.pnlClan = new System.Windows.Forms.Panel();
            this.lblClan = new System.Windows.Forms.Label();
            this.cmbClan = new System.Windows.Forms.ComboBox();
            this.pnlExport = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlApproval = new System.Windows.Forms.Panel();
            this.cmbApproval = new System.Windows.Forms.ComboBox();
            this.lblApproval = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnExportToCSV = new System.Windows.Forms.Button();
            this.cmbGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.lblLName = new System.Windows.Forms.Label();
            this.txtLName = new System.Windows.Forms.TextBox();
            this.lblFName = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this.pnlApproveReject = new System.Windows.Forms.Panel();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbFiled = new System.Windows.Forms.ComboBox();
            this.pnlMain.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFile)).BeginInit();
            this.pnlPagination.SuspendLayout();
            this.pnlPaginationRight.SuspendLayout();
            this.pnlControl.SuspendLayout();
            this.pnlClan.SuspendLayout();
            this.pnlExport.SuspendLayout();
            this.pnlApproval.SuspendLayout();
            this.pnlApproveReject.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvMKAGeoStructure
            // 
            this.tvMKAGeoStructure.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvMKAGeoStructure.Location = new System.Drawing.Point(0, 0);
            this.tvMKAGeoStructure.Margin = new System.Windows.Forms.Padding(4);
            this.tvMKAGeoStructure.Name = "tvMKAGeoStructure";
            this.tvMKAGeoStructure.Size = new System.Drawing.Size(289, 554);
            this.tvMKAGeoStructure.TabIndex = 0;
            this.tvMKAGeoStructure.Visible = false;
            this.tvMKAGeoStructure.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvMKAGeoStructure_AfterCollapse);
            this.tvMKAGeoStructure.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMKAGeoStructure_AfterSelect);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlGrid);
            this.pnlMain.Controls.Add(this.pnlPagination);
            this.pnlMain.Controls.Add(this.pnlControl);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(289, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1383, 554);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgFile);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 146);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1383, 349);
            this.pnlGrid.TabIndex = 2;
            // 
            // dgFile
            // 
            this.dgFile.AllowUserToAddRows = false;
            this.dgFile.AllowUserToDeleteRows = false;
            this.dgFile.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgFile.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgFile.BackgroundColor = System.Drawing.Color.White;
            //dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;

            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgFile.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFile.EnableHeadersVisualStyles = false;
            this.dgFile.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgFile.Location = new System.Drawing.Point(0, 0);
            this.dgFile.Margin = new System.Windows.Forms.Padding(4);
            this.dgFile.MultiSelect = false;
            this.dgFile.Name = "dgFile";
            this.dgFile.ReadOnly = true;
            this.dgFile.RowHeadersWidth = 51;
            this.dgFile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgFile.RowsDefaultCellStyle = dataGridViewCellStyle4;
            //this.dgFile.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFile.Size = new System.Drawing.Size(1383, 349);
            this.dgFile.TabIndex = 0;
            this.dgFile.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgFile_CellPainting);
            this.dgFile.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgFile_Scroll);
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.pnlPaginationRight);
            this.pnlPagination.Controls.Add(this.pnlPaginationLeft);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 495);
            this.pnlPagination.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1383, 59);
            this.pnlPagination.TabIndex = 1;
            // 
            // pnlPaginationRight
            // 
            this.pnlPaginationRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPaginationRight.Controls.Add(this.cmbPageSize);
            this.pnlPaginationRight.Controls.Add(this.lblPageSize);
            this.pnlPaginationRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPaginationRight.Location = new System.Drawing.Point(1098, 0);
            this.pnlPaginationRight.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPaginationRight.Name = "pnlPaginationRight";
            this.pnlPaginationRight.Size = new System.Drawing.Size(285, 59);
            this.pnlPaginationRight.TabIndex = 1;
            this.pnlPaginationRight.Visible = false;
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Location = new System.Drawing.Point(115, 7);
            this.cmbPageSize.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(160, 27);
            this.cmbPageSize.TabIndex = 1;
            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.CmbPageSize_SelectedIndexChanged);
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageSize.Location = new System.Drawing.Point(12, 11);
            this.lblPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(84, 19);
            this.lblPageSize.TabIndex = 0;
            this.lblPageSize.Text = "Page Size";
            // 
            // pnlPaginationLeft
            // 
            this.pnlPaginationLeft.Location = new System.Drawing.Point(8, 0);
            this.pnlPaginationLeft.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPaginationLeft.Name = "pnlPaginationLeft";
            this.pnlPaginationLeft.Size = new System.Drawing.Size(1068, 59);
            this.pnlPaginationLeft.TabIndex = 0;
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.chkLookUp);
            this.pnlControl.Controls.Add(this.pnlClan);
            this.pnlControl.Controls.Add(this.pnlExport);
            this.pnlControl.Controls.Add(this.pnlApproval);
            this.pnlControl.Controls.Add(this.btnCancel);
            this.pnlControl.Controls.Add(this.btnTransfer);
            this.pnlControl.Controls.Add(this.btnExportToCSV);
            this.pnlControl.Controls.Add(this.cmbGender);
            this.pnlControl.Controls.Add(this.lblGender);
            this.pnlControl.Controls.Add(this.lblLName);
            this.pnlControl.Controls.Add(this.txtLName);
            this.pnlControl.Controls.Add(this.lblFName);
            this.pnlControl.Controls.Add(this.txtFName);
            this.pnlControl.Controls.Add(this.pnlApproveReject);
            this.pnlControl.Controls.Add(this.btnSearch);
            this.pnlControl.Controls.Add(this.cmbStatus);
            this.pnlControl.Controls.Add(this.btnRefresh);
            this.pnlControl.Controls.Add(this.cmbFiled);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(0, 0);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(4);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(1383, 146);
            this.pnlControl.TabIndex = 0;
            // 
            // chkLookUp
            // 
            this.chkLookUp.AutoSize = true;
            this.chkLookUp.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkLookUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLookUp.Location = new System.Drawing.Point(1047, 16);
            this.chkLookUp.Name = "chkLookUp";
            this.chkLookUp.Size = new System.Drawing.Size(85, 24);
            this.chkLookUp.TabIndex = 19;
            this.chkLookUp.Text = "Lookup";
            this.chkLookUp.UseVisualStyleBackColor = true;
            // 
            // pnlClan
            // 
            this.pnlClan.Controls.Add(this.lblClan);
            this.pnlClan.Controls.Add(this.cmbClan);
            this.pnlClan.Location = new System.Drawing.Point(1150, 9);
            this.pnlClan.Name = "pnlClan";
            this.pnlClan.Size = new System.Drawing.Size(177, 38);
            this.pnlClan.TabIndex = 18;
            this.pnlClan.Visible = false;
            // 
            // lblClan
            // 
            this.lblClan.AutoSize = true;
            this.lblClan.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClan.Location = new System.Drawing.Point(4, 10);
            this.lblClan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClan.Name = "lblClan";
            this.lblClan.Size = new System.Drawing.Size(42, 19);
            this.lblClan.TabIndex = 16;
            this.lblClan.Text = "Clan";
            // 
            // cmbClan
            // 
            this.cmbClan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClan.DropDownWidth = 180;
            this.cmbClan.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbClan.FormattingEnabled = true;
            this.cmbClan.Location = new System.Drawing.Point(54, 6);
            this.cmbClan.Margin = new System.Windows.Forms.Padding(4);
            this.cmbClan.Name = "cmbClan";
            this.cmbClan.Size = new System.Drawing.Size(112, 27);
            this.cmbClan.TabIndex = 17;
            // 
            // pnlExport
            // 
            this.pnlExport.Controls.Add(this.btnExport);
            this.pnlExport.Location = new System.Drawing.Point(927, 47);
            this.pnlExport.Margin = new System.Windows.Forms.Padding(4);
            this.pnlExport.Name = "pnlExport";
            this.pnlExport.Size = new System.Drawing.Size(149, 46);
            this.pnlExport.TabIndex = 9;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(5, 5);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 37);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // pnlApproval
            // 
            this.pnlApproval.Controls.Add(this.cmbApproval);
            this.pnlApproval.Controls.Add(this.lblApproval);
            this.pnlApproval.Location = new System.Drawing.Point(452, 52);
            this.pnlApproval.Name = "pnlApproval";
            this.pnlApproval.Size = new System.Drawing.Size(326, 37);
            this.pnlApproval.TabIndex = 15;
            // 
            // cmbApproval
            // 
            this.cmbApproval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbApproval.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbApproval.FormattingEnabled = true;
            this.cmbApproval.Location = new System.Drawing.Point(90, 5);
            this.cmbApproval.Margin = new System.Windows.Forms.Padding(4);
            this.cmbApproval.Name = "cmbApproval";
            this.cmbApproval.Size = new System.Drawing.Size(226, 27);
            this.cmbApproval.TabIndex = 9;
            // 
            // lblApproval
            // 
            this.lblApproval.AutoSize = true;
            this.lblApproval.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApproval.Location = new System.Drawing.Point(6, 9);
            this.lblApproval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApproval.Name = "lblApproval";
            this.lblApproval.Size = new System.Drawing.Size(72, 19);
            this.lblApproval.TabIndex = 8;
            this.lblApproval.Text = "Approval";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(304, 52);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnCancel.Size = new System.Drawing.Size(140, 37);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.Enabled = false;
            this.btnTransfer.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.Location = new System.Drawing.Point(8, 97);
            this.btnTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(140, 37);
            this.btnTransfer.TabIndex = 13;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToCSV.Location = new System.Drawing.Point(785, 52);
            this.btnExportToCSV.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(140, 37);
            this.btnExportToCSV.TabIndex = 14;
            this.btnExportToCSV.Text = "Export to CSV";
            this.btnExportToCSV.UseVisualStyleBackColor = true;
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Location = new System.Drawing.Point(922, 14);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(112, 27);
            this.cmbGender.TabIndex = 7;
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.Location = new System.Drawing.Point(849, 18);
            this.lblGender.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(63, 19);
            this.lblGender.TabIndex = 6;
            this.lblGender.Text = "Gender";
            // 
            // lblLName
            // 
            this.lblLName.AutoSize = true;
            this.lblLName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLName.Location = new System.Drawing.Point(424, 18);
            this.lblLName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLName.Name = "lblLName";
            this.lblLName.Size = new System.Drawing.Size(64, 19);
            this.lblLName.TabIndex = 2;
            this.lblLName.Text = "L Name";
            // 
            // txtLName
            // 
            this.txtLName.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLName.Location = new System.Drawing.Point(505, 14);
            this.txtLName.Margin = new System.Windows.Forms.Padding(4);
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(117, 29);
            this.txtLName.TabIndex = 3;
            // 
            // lblFName
            // 
            this.lblFName.AutoSize = true;
            this.lblFName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFName.Location = new System.Drawing.Point(632, 18);
            this.lblFName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFName.Name = "lblFName";
            this.lblFName.Size = new System.Drawing.Size(66, 19);
            this.lblFName.TabIndex = 4;
            this.lblFName.Text = "F Name";
            // 
            // txtFName
            // 
            this.txtFName.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFName.Location = new System.Drawing.Point(716, 14);
            this.txtFName.Margin = new System.Windows.Forms.Padding(4);
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(115, 29);
            this.txtFName.TabIndex = 5;
            // 
            // pnlApproveReject
            // 
            this.pnlApproveReject.Controls.Add(this.btnReject);
            this.pnlApproveReject.Controls.Add(this.btnApprove);
            this.pnlApproveReject.Location = new System.Drawing.Point(156, 92);
            this.pnlApproveReject.Margin = new System.Windows.Forms.Padding(4);
            this.pnlApproveReject.Name = "pnlApproveReject";
            this.pnlApproveReject.Size = new System.Drawing.Size(297, 47);
            this.pnlApproveReject.TabIndex = 8;
            // 
            // btnReject
            // 
            this.btnReject.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReject.Location = new System.Drawing.Point(148, 4);
            this.btnReject.Margin = new System.Windows.Forms.Padding(4);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(140, 37);
            this.btnReject.TabIndex = 1;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApprove.Location = new System.Drawing.Point(0, 5);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(140, 37);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(8, 52);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(140, 37);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Arial", 9F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.ItemHeight = 17;
            this.cmbStatus.Location = new System.Drawing.Point(156, 15);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(260, 25);
            this.cmbStatus.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::PNGEC.Desktop.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(156, 52);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(140, 37);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbFiled
            // 
            this.cmbFiled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiled.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbFiled.FormattingEnabled = true;
            this.cmbFiled.Location = new System.Drawing.Point(8, 15);
            this.cmbFiled.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFiled.Name = "cmbFiled";
            this.cmbFiled.Size = new System.Drawing.Size(139, 27);
            this.cmbFiled.TabIndex = 0;
            this.cmbFiled.SelectedIndexChanged += new System.EventHandler(this.cmbFiled_SelectedIndexChanged);
            // 
            // MKAFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 554);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tvMKAGeoStructure);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MKAFile";
            this.Text = "File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.File_FormClosing);
            this.Load += new System.EventHandler(this.MKAFile_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFile)).EndInit();
            this.pnlPagination.ResumeLayout(false);
            this.pnlPaginationRight.ResumeLayout(false);
            this.pnlPaginationRight.PerformLayout();
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.pnlClan.ResumeLayout(false);
            this.pnlClan.PerformLayout();
            this.pnlExport.ResumeLayout(false);
            this.pnlApproval.ResumeLayout(false);
            this.pnlApproval.PerformLayout();
            this.pnlApproveReject.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMKAGeoStructure;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.ComboBox cmbFiled;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgFile;
        private System.Windows.Forms.Panel pnlApproveReject;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Panel pnlExport;
        private System.Windows.Forms.Panel pnlPaginationLeft;
        private System.Windows.Forms.Panel pnlPaginationRight;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblFName;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label lblLName;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Button btnExportToCSV;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlApproval;
        private System.Windows.Forms.ComboBox cmbApproval;
        private System.Windows.Forms.Label lblApproval;
        private System.Windows.Forms.Label lblClan;
        private System.Windows.Forms.ComboBox cmbClan;
        private System.Windows.Forms.Panel pnlClan;
        private System.Windows.Forms.CheckBox chkLookUp;
    }
}