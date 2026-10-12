using System;

namespace PNGEC.Desktop.Forms
{
    partial class ImportBioDataDetails
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.Searchbox = new System.Windows.Forms.TextBox();
            this.biocheckBox1 = new System.Windows.Forms.CheckBox();
            this.Backbtn = new System.Windows.Forms.Button();
            this.Nextbtn = new System.Windows.Forms.Button();
            this.tbRead = new System.Windows.Forms.Button();
            this.Browse = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.exportButton = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.Resetbtn = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnLoadToStagingServer = new System.Windows.Forms.Button();
            this.pnlcontrol = new System.Windows.Forms.Panel();
            this.cmbLocationName = new System.Windows.Forms.ComboBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWardName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbLLGName = new System.Windows.Forms.ComboBox();
            this.cmbDistrictName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProvinceName = new System.Windows.Forms.Label();
            this.cmbProvinceName = new System.Windows.Forms.ComboBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.lblRegionName = new System.Windows.Forms.Label();
            this.cmbRegionName = new System.Windows.Forms.ComboBox();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlRecordNumber = new System.Windows.Forms.Panel();
            this.pnlgrid = new System.Windows.Forms.Panel();
            this.dgImportBioDataDetails = new System.Windows.Forms.DataGridView();
            this.btnCLear = new System.Windows.Forms.Button();
            this.pnlcontrol.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            this.pnlRecordNumber.SuspendLayout();
            this.pnlgrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgImportBioDataDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // Searchbtn
            // 
            this.Searchbtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Searchbtn.Location = new System.Drawing.Point(22, 42);
            this.Searchbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(10, 20);
            this.Searchbtn.TabIndex = 30;
            this.Searchbtn.Text = "Search";
            this.Searchbtn.UseVisualStyleBackColor = false;
            this.Searchbtn.Visible = false;
            this.Searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // Searchbox
            // 
            this.Searchbox.BackColor = System.Drawing.SystemColors.Window;
            this.Searchbox.Location = new System.Drawing.Point(13, 42);
            this.Searchbox.Margin = new System.Windows.Forms.Padding(4);
            this.Searchbox.Name = "Searchbox";
            this.Searchbox.Size = new System.Drawing.Size(10, 20);
            this.Searchbox.TabIndex = 29;
            this.toolTip1.SetToolTip(this.Searchbox, "\rEnter : First Name/Last Name/ National Id/Electrol Id/Biometric Id");
            this.Searchbox.Visible = false;
            this.Searchbox.TextChanged += new System.EventHandler(this.Searchbox_TextChanged);
            // 
            // biocheckBox1
            // 
            this.biocheckBox1.AutoSize = true;
            this.biocheckBox1.Location = new System.Drawing.Point(42, 45);
            this.biocheckBox1.Margin = new System.Windows.Forms.Padding(4);
            this.biocheckBox1.Name = "biocheckBox1";
            this.biocheckBox1.Size = new System.Drawing.Size(15, 14);
            this.biocheckBox1.TabIndex = 27;
            this.biocheckBox1.UseVisualStyleBackColor = true;
            this.biocheckBox1.Visible = false;
            this.biocheckBox1.CheckedChanged += new System.EventHandler(this.biocheckBox1_CheckedChanged);
            // 
            // Backbtn
            // 
            this.Backbtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Backbtn.Location = new System.Drawing.Point(13, 4);
            this.Backbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Backbtn.Name = "Backbtn";
            this.Backbtn.Size = new System.Drawing.Size(179, 32);
            this.Backbtn.TabIndex = 26;
            this.Backbtn.Text = "Previous";
            this.Backbtn.UseVisualStyleBackColor = false;
            this.Backbtn.Click += new System.EventHandler(this.Backbtn_Click);
            // 
            // Nextbtn
            // 
            this.Nextbtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Nextbtn.Location = new System.Drawing.Point(217, 4);
            this.Nextbtn.Margin = new System.Windows.Forms.Padding(4);
            this.Nextbtn.Name = "Nextbtn";
            this.Nextbtn.Size = new System.Drawing.Size(171, 32);
            this.Nextbtn.TabIndex = 25;
            this.Nextbtn.Text = "Next";
            this.Nextbtn.UseVisualStyleBackColor = false;
            this.Nextbtn.Click += new System.EventHandler(this.Nextbtn_Click);
            // 
            // tbRead
            // 
            this.tbRead.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tbRead.Location = new System.Drawing.Point(993, 45);
            this.tbRead.Margin = new System.Windows.Forms.Padding(4);
            this.tbRead.Name = "tbRead";
            this.tbRead.Size = new System.Drawing.Size(142, 34);
            this.tbRead.TabIndex = 23;
            this.tbRead.Text = "Load Data";
            this.tbRead.UseVisualStyleBackColor = false;
            this.tbRead.Click += new System.EventHandler(this.tbRead_Click);
            // 
            // Browse
            // 
            this.Browse.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Browse.Location = new System.Drawing.Point(843, 45);
            this.Browse.Margin = new System.Windows.Forms.Padding(4);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(142, 34);
            this.Browse.TabIndex = 22;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = false;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.exportButton.Location = new System.Drawing.Point(843, 128);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(142, 34);
            this.exportButton.TabIndex = 34;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(83, 9);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(743, 69);
            this.fileListBox.TabIndex = 33;
            this.fileListBox.SelectedIndexChanged += new System.EventHandler(this.fileListBox_SelectedIndexChanged);
            // 
            // Resetbtn
            // 
            this.Resetbtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Resetbtn.Location = new System.Drawing.Point(32, 42);
            this.Resetbtn.Name = "Resetbtn";
            this.Resetbtn.Size = new System.Drawing.Size(10, 20);
            this.Resetbtn.TabIndex = 32;
            this.Resetbtn.Text = "Reset";
            this.Resetbtn.UseVisualStyleBackColor = false;
            this.Resetbtn.Visible = false;
            this.Resetbtn.Click += new System.EventHandler(this.Resetbtn_Click);
            // 
            // lblPage
            // 
            this.lblPage.BackColor = System.Drawing.Color.Transparent;
            this.lblPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage.Location = new System.Drawing.Point(27, 8);
            this.lblPage.Margin = new System.Windows.Forms.Padding(3, 0, 10, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(235, 13);
            this.lblPage.TabIndex = 31;
            this.lblPage.Text = "Records 0 of 0";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPage.Click += new System.EventHandler(this.lblPage_Click);
            // 
            // btnLoadToStagingServer
            // 
            this.btnLoadToStagingServer.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnLoadToStagingServer.Location = new System.Drawing.Point(993, 128);
            this.btnLoadToStagingServer.Name = "btnLoadToStagingServer";
            this.btnLoadToStagingServer.Size = new System.Drawing.Size(142, 34);
            this.btnLoadToStagingServer.TabIndex = 35;
            this.btnLoadToStagingServer.Text = "Load to Staging Server";
            this.btnLoadToStagingServer.UseVisualStyleBackColor = false;
            this.btnLoadToStagingServer.Click += new System.EventHandler(this.btnLoadToStagingServer_Click);
            // 
            // pnlcontrol
            // 
            this.pnlcontrol.Controls.Add(this.btnCLear);
            this.pnlcontrol.Controls.Add(this.cmbLocationName);
            this.pnlcontrol.Controls.Add(this.lblLocation);
            this.pnlcontrol.Controls.Add(this.label3);
            this.pnlcontrol.Controls.Add(this.cmbWardName);
            this.pnlcontrol.Controls.Add(this.label2);
            this.pnlcontrol.Controls.Add(this.cmbLLGName);
            this.pnlcontrol.Controls.Add(this.cmbDistrictName);
            this.pnlcontrol.Controls.Add(this.label1);
            this.pnlcontrol.Controls.Add(this.lblProvinceName);
            this.pnlcontrol.Controls.Add(this.cmbProvinceName);
            this.pnlcontrol.Controls.Add(this.lblFilePath);
            this.pnlcontrol.Controls.Add(this.lblRegionName);
            this.pnlcontrol.Controls.Add(this.cmbRegionName);
            this.pnlcontrol.Controls.Add(this.btnLoadToStagingServer);
            this.pnlcontrol.Controls.Add(this.Searchbtn);
            this.pnlcontrol.Controls.Add(this.Resetbtn);
            this.pnlcontrol.Controls.Add(this.Searchbox);
            this.pnlcontrol.Controls.Add(this.fileListBox);
            this.pnlcontrol.Controls.Add(this.exportButton);
            this.pnlcontrol.Controls.Add(this.biocheckBox1);
            this.pnlcontrol.Controls.Add(this.Browse);
            this.pnlcontrol.Controls.Add(this.tbRead);
            this.pnlcontrol.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlcontrol.Location = new System.Drawing.Point(0, 0);
            this.pnlcontrol.Name = "pnlcontrol";
            this.pnlcontrol.Size = new System.Drawing.Size(1148, 171);
            this.pnlcontrol.TabIndex = 36;
            // 
            // cmbLocationName
            // 
            this.cmbLocationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationName.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbLocationName.FormattingEnabled = true;
            this.cmbLocationName.Location = new System.Drawing.Point(637, 136);
            this.cmbLocationName.Name = "cmbLocationName";
            this.cmbLocationName.Size = new System.Drawing.Size(189, 24);
            this.cmbLocationName.TabIndex = 47;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(563, 139);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(68, 18);
            this.lblLocation.TabIndex = 48;
            this.lblLocation.Text = "Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(286, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 18);
            this.label3.TabIndex = 46;
            this.label3.Text = "Ward";
            // 
            // cmbWardName
            // 
            this.cmbWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWardName.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbWardName.FormattingEnabled = true;
            this.cmbWardName.Location = new System.Drawing.Point(361, 136);
            this.cmbWardName.Name = "cmbWardName";
            this.cmbWardName.Size = new System.Drawing.Size(196, 24);
            this.cmbWardName.TabIndex = 45;
            this.cmbWardName.SelectedIndexChanged += new System.EventHandler(this.cmbWardName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 18);
            this.label2.TabIndex = 44;
            this.label2.Text = "LLG";
            // 
            // cmbLLGName
            // 
            this.cmbLLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLLGName.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbLLGName.FormattingEnabled = true;
            this.cmbLLGName.Location = new System.Drawing.Point(83, 136);
            this.cmbLLGName.Name = "cmbLLGName";
            this.cmbLLGName.Size = new System.Drawing.Size(197, 24);
            this.cmbLLGName.TabIndex = 43;
            this.cmbLLGName.SelectedIndexChanged += new System.EventHandler(this.cmbLLGName_SelectedIndexChanged);
            // 
            // cmbDistrictName
            // 
            this.cmbDistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrictName.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbDistrictName.FormattingEnabled = true;
            this.cmbDistrictName.Location = new System.Drawing.Point(637, 95);
            this.cmbDistrictName.Name = "cmbDistrictName";
            this.cmbDistrictName.Size = new System.Drawing.Size(189, 24);
            this.cmbDistrictName.TabIndex = 41;
            this.cmbDistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbDistrictName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(563, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 42;
            this.label1.Text = "District";
            // 
            // lblProvinceName
            // 
            this.lblProvinceName.AutoSize = true;
            this.lblProvinceName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvinceName.Location = new System.Drawing.Point(286, 98);
            this.lblProvinceName.Name = "lblProvinceName";
            this.lblProvinceName.Size = new System.Drawing.Size(69, 18);
            this.lblProvinceName.TabIndex = 40;
            this.lblProvinceName.Text = "Province";
            // 
            // cmbProvinceName
            // 
            this.cmbProvinceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvinceName.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbProvinceName.FormattingEnabled = true;
            this.cmbProvinceName.Location = new System.Drawing.Point(361, 95);
            this.cmbProvinceName.Name = "cmbProvinceName";
            this.cmbProvinceName.Size = new System.Drawing.Size(196, 24);
            this.cmbProvinceName.TabIndex = 39;
            this.cmbProvinceName.SelectedIndexChanged += new System.EventHandler(this.cmbProvinceName_SelectedIndexChanged);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilePath.Location = new System.Drawing.Point(9, 8);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(71, 20);
            this.lblFilePath.TabIndex = 38;
            this.lblFilePath.Text = "File Path";
            // 
            // lblRegionName
            // 
            this.lblRegionName.AutoSize = true;
            this.lblRegionName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegionName.Location = new System.Drawing.Point(9, 98);
            this.lblRegionName.Name = "lblRegionName";
            this.lblRegionName.Size = new System.Drawing.Size(58, 18);
            this.lblRegionName.TabIndex = 37;
            this.lblRegionName.Text = "Region";
            // 
            // cmbRegionName
            // 
            this.cmbRegionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegionName.Font = new System.Drawing.Font("Arial", 10F);
            this.cmbRegionName.FormattingEnabled = true;
            this.cmbRegionName.Location = new System.Drawing.Point(83, 95);
            this.cmbRegionName.Name = "cmbRegionName";
            this.cmbRegionName.Size = new System.Drawing.Size(197, 24);
            this.cmbRegionName.TabIndex = 36;
            this.cmbRegionName.SelectedIndexChanged += new System.EventHandler(this.cmbRegionName_SelectedIndexChanged);
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.Nextbtn);
            this.pnlPagination.Controls.Add(this.Backbtn);
            this.pnlPagination.Controls.Add(this.pnlRecordNumber);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 607);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1148, 40);
            this.pnlPagination.TabIndex = 38;
            // 
            // pnlRecordNumber
            // 
            this.pnlRecordNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlRecordNumber.Controls.Add(this.lblPage);
            this.pnlRecordNumber.Location = new System.Drawing.Point(876, 6);
            this.pnlRecordNumber.Name = "pnlRecordNumber";
            this.pnlRecordNumber.Size = new System.Drawing.Size(269, 30);
            this.pnlRecordNumber.TabIndex = 32;
            // 
            // pnlgrid
            // 
            this.pnlgrid.AutoScroll = true;
            this.pnlgrid.AutoSize = true;
            this.pnlgrid.Controls.Add(this.dgImportBioDataDetails);
            this.pnlgrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlgrid.Location = new System.Drawing.Point(0, 171);
            this.pnlgrid.Name = "pnlgrid";
            this.pnlgrid.Size = new System.Drawing.Size(1148, 436);
            this.pnlgrid.TabIndex = 37;
            // 
            // dgImportBioDataDetails
            // 
            this.dgImportBioDataDetails.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgImportBioDataDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgImportBioDataDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgImportBioDataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgImportBioDataDetails.GridColor = System.Drawing.SystemColors.Control;
            this.dgImportBioDataDetails.Location = new System.Drawing.Point(0, 0);
            this.dgImportBioDataDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dgImportBioDataDetails.Name = "dgImportBioDataDetails";
            this.dgImportBioDataDetails.RowHeadersWidth = 51;
            this.dgImportBioDataDetails.Size = new System.Drawing.Size(1148, 436);
            this.dgImportBioDataDetails.TabIndex = 9;
            this.dgImportBioDataDetails.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgImportBioDataDetails_CellClick);
            this.dgImportBioDataDetails.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgImportBioDataDetails_CellPainting);
            this.dgImportBioDataDetails.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgImportBioDataDetails_Scroll);
            // 
            // btnCLear
            // 
            this.btnCLear.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnCLear.Location = new System.Drawing.Point(843, 9);
            this.btnCLear.Margin = new System.Windows.Forms.Padding(4);
            this.btnCLear.Name = "btnCLear";
            this.btnCLear.Size = new System.Drawing.Size(142, 34);
            this.btnCLear.TabIndex = 49;
            this.btnCLear.Text = "Clear";
            this.btnCLear.UseVisualStyleBackColor = false;
            this.btnCLear.Click += new System.EventHandler(this.btnCLear_Click);
            // 
            // ImportBioDataDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 647);
            this.Controls.Add(this.pnlgrid);
            this.Controls.Add(this.pnlPagination);
            this.Controls.Add(this.pnlcontrol);
            this.Name = "ImportBioDataDetails";
            this.Text = "Import Bio Data Details";
            this.Load += new System.EventHandler(this.ImportBioDataDetails_Load);
            this.pnlcontrol.ResumeLayout(false);
            this.pnlcontrol.PerformLayout();
            this.pnlPagination.ResumeLayout(false);
            this.pnlRecordNumber.ResumeLayout(false);
            this.pnlgrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgImportBioDataDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void lblPage_Click(object sender, EventArgs e)
        {
            
        }

        private void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void FilePath_Click(object sender, EventArgs e)
        {
            
        }

        private void biocheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        private System.Windows.Forms.Button Searchbtn;
        private System.Windows.Forms.TextBox Searchbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox biocheckBox1;
        private System.Windows.Forms.Button Backbtn;
        private System.Windows.Forms.Button Nextbtn;
        private System.Windows.Forms.Button tbRead;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button Resetbtn;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Button btnLoadToStagingServer;
        private System.Windows.Forms.Panel pnlcontrol;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlgrid;
        private System.Windows.Forms.Label lblRegionName;
        private System.Windows.Forms.ComboBox cmbRegionName;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Label lblProvinceName;
        private System.Windows.Forms.ComboBox cmbProvinceName;
        private System.Windows.Forms.ComboBox cmbDistrictName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbLLGName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWardName;
        private System.Windows.Forms.ComboBox cmbLocationName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.DataGridView dgImportBioDataDetails;
        private System.Windows.Forms.Panel pnlRecordNumber;
        private System.Windows.Forms.Button btnCLear;
    }
}