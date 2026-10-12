namespace PNGEC.Desktop.Forms
{
    partial class TransferInVoter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.pnlPaginationRight = new System.Windows.Forms.Panel();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlPaginationLeft = new System.Windows.Forms.Panel();
            this.dgTransferInVoter = new System.Windows.Forms.DataGridView();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlGridMain = new System.Windows.Forms.Panel();
            this.pnlGenerate = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbWardName = new System.Windows.Forms.ComboBox();
            this.cmbLLGName = new System.Windows.Forms.ComboBox();
            this.cmbDistrictName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRegionName = new System.Windows.Forms.Label();
            this.cmbRegionName = new System.Windows.Forms.ComboBox();
            this.lblProvinceName = new System.Windows.Forms.Label();
            this.cmbProvinceName = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlPaginationRight.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransferInVoter)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.pnlGridMain.SuspendLayout();
            this.pnlGenerate.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // pdfDialog
            // 
            this.pdfDialog.CheckPathExists = false;
            this.pdfDialog.CreatePrompt = true;
            this.pdfDialog.DefaultExt = "pdf";
            this.pdfDialog.Filter = "PDF files (*.pdf)|*.pdf";
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Location = new System.Drawing.Point(86, 6);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(121, 24);
            this.cmbPageSize.TabIndex = 1;
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageSize.Location = new System.Drawing.Point(9, 9);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(71, 16);
            this.lblPageSize.TabIndex = 0;
            this.lblPageSize.Text = "Page Size";
            // 
            // pnlPaginationRight
            // 
            this.pnlPaginationRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPaginationRight.Controls.Add(this.cmbPageSize);
            this.pnlPaginationRight.Controls.Add(this.lblPageSize);
            this.pnlPaginationRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPaginationRight.Location = new System.Drawing.Point(1046, 0);
            this.pnlPaginationRight.Name = "pnlPaginationRight";
            this.pnlPaginationRight.Size = new System.Drawing.Size(214, 48);
            this.pnlPaginationRight.TabIndex = 3;
            this.pnlPaginationRight.Visible = false;
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.pnlPaginationRight);
            this.pnlPagination.Controls.Add(this.pnlPaginationLeft);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 458);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1260, 48);
            this.pnlPagination.TabIndex = 30;
            // 
            // pnlPaginationLeft
            // 
            this.pnlPaginationLeft.Location = new System.Drawing.Point(12, 0);
            this.pnlPaginationLeft.Name = "pnlPaginationLeft";
            this.pnlPaginationLeft.Size = new System.Drawing.Size(869, 48);
            this.pnlPaginationLeft.TabIndex = 2;
            // 
            // dgTransferInVoter
            // 
            this.dgTransferInVoter.AllowUserToAddRows = false;
            this.dgTransferInVoter.AllowUserToDeleteRows = false;
            this.dgTransferInVoter.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgTransferInVoter.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgTransferInVoter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgTransferInVoter.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgTransferInVoter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgTransferInVoter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgTransferInVoter.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgTransferInVoter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTransferInVoter.EnableHeadersVisualStyles = false;
            this.dgTransferInVoter.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgTransferInVoter.Location = new System.Drawing.Point(0, 0);
            this.dgTransferInVoter.MultiSelect = false;
            this.dgTransferInVoter.Name = "dgTransferInVoter";
            this.dgTransferInVoter.ReadOnly = true;
            this.dgTransferInVoter.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgTransferInVoter.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgTransferInVoter.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgTransferInVoter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTransferInVoter.Size = new System.Drawing.Size(1260, 458);
            this.dgTransferInVoter.TabIndex = 3;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgTransferInVoter);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1260, 458);
            this.pnlGrid.TabIndex = 32;
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.pnlGrid);
            this.pnlGridMain.Controls.Add(this.pnlPagination);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 173);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(1260, 506);
            this.pnlGridMain.TabIndex = 35;
            // 
            // pnlGenerate
            // 
            this.pnlGenerate.Controls.Add(this.btnExport);
            this.pnlGenerate.Controls.Add(this.label3);
            this.pnlGenerate.Controls.Add(this.label2);
            this.pnlGenerate.Controls.Add(this.cmbWardName);
            this.pnlGenerate.Controls.Add(this.cmbLLGName);
            this.pnlGenerate.Controls.Add(this.cmbDistrictName);
            this.pnlGenerate.Controls.Add(this.label1);
            this.pnlGenerate.Controls.Add(this.lblRegionName);
            this.pnlGenerate.Controls.Add(this.cmbRegionName);
            this.pnlGenerate.Controls.Add(this.lblProvinceName);
            this.pnlGenerate.Controls.Add(this.cmbProvinceName);
            this.pnlGenerate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGenerate.Location = new System.Drawing.Point(0, 38);
            this.pnlGenerate.Name = "pnlGenerate";
            this.pnlGenerate.Size = new System.Drawing.Size(1260, 135);
            this.pnlGenerate.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(598, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "Ward Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(598, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "LLG Name";
            // 
            // cmbWardName
            // 
            this.cmbWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWardName.FormattingEnabled = true;
            this.cmbWardName.Location = new System.Drawing.Point(713, 53);
            this.cmbWardName.Name = "cmbWardName";
            this.cmbWardName.Size = new System.Drawing.Size(413, 28);
            this.cmbWardName.TabIndex = 28;
            this.cmbWardName.SelectedIndexChanged += new System.EventHandler(this.cmbWardName_SelectedIndexChanged);
            // 
            // cmbLLGName
            // 
            this.cmbLLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLLGName.FormattingEnabled = true;
            this.cmbLLGName.Location = new System.Drawing.Point(713, 18);
            this.cmbLLGName.Name = "cmbLLGName";
            this.cmbLLGName.Size = new System.Drawing.Size(413, 28);
            this.cmbLLGName.TabIndex = 27;
            this.cmbLLGName.SelectedIndexChanged += new System.EventHandler(this.cmbLLGName_SelectedIndexChanged);
            // 
            // cmbDistrictName
            // 
            this.cmbDistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrictName.FormattingEnabled = true;
            this.cmbDistrictName.Location = new System.Drawing.Point(150, 88);
            this.cmbDistrictName.Name = "cmbDistrictName";
            this.cmbDistrictName.Size = new System.Drawing.Size(413, 28);
            this.cmbDistrictName.TabIndex = 26;
            this.cmbDistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbDistrictName_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 25;
            this.label1.Text = "District Name";
            // 
            // lblRegionName
            // 
            this.lblRegionName.AutoSize = true;
            this.lblRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegionName.Location = new System.Drawing.Point(11, 21);
            this.lblRegionName.Name = "lblRegionName";
            this.lblRegionName.Size = new System.Drawing.Size(106, 20);
            this.lblRegionName.TabIndex = 13;
            this.lblRegionName.Text = "Region Name";
            // 
            // cmbRegionName
            // 
            this.cmbRegionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegionName.FormattingEnabled = true;
            this.cmbRegionName.Location = new System.Drawing.Point(150, 20);
            this.cmbRegionName.Name = "cmbRegionName";
            this.cmbRegionName.Size = new System.Drawing.Size(413, 28);
            this.cmbRegionName.TabIndex = 14;
            this.cmbRegionName.SelectedIndexChanged += new System.EventHandler(this.cmbRegionName_SelectedIndexChanged);
            // 
            // lblProvinceName
            // 
            this.lblProvinceName.AutoSize = true;
            this.lblProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvinceName.Location = new System.Drawing.Point(11, 56);
            this.lblProvinceName.Name = "lblProvinceName";
            this.lblProvinceName.Size = new System.Drawing.Size(115, 20);
            this.lblProvinceName.TabIndex = 15;
            this.lblProvinceName.Text = "Province Name";
            // 
            // cmbProvinceName
            // 
            this.cmbProvinceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvinceName.FormattingEnabled = true;
            this.cmbProvinceName.Location = new System.Drawing.Point(150, 54);
            this.cmbProvinceName.Name = "cmbProvinceName";
            this.cmbProvinceName.Size = new System.Drawing.Size(413, 28);
            this.cmbProvinceName.TabIndex = 16;
            this.cmbProvinceName.SelectedIndexChanged += new System.EventHandler(this.cmbProvinceName_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::PNGEC.Desktop.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(353, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(116, 30);
            this.btnRefresh.TabIndex = 27;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnRefresh);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1260, 38);
            this.pnlSearch.TabIndex = 33;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(15, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(210, 24);
            this.txtSearch.TabIndex = 25;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(231, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(116, 30);
            this.btnSearch.TabIndex = 26;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.Font = new System.Drawing.Font("Arial", 10F);
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(713, 87);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(139, 41);
            this.btnExport.TabIndex = 31;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // TransferInVoter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 679);
            this.Controls.Add(this.pnlGridMain);
            this.Controls.Add(this.pnlGenerate);
            this.Controls.Add(this.pnlSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferInVoter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer In Voter";
            this.Load += new System.EventHandler(this.TransferInVoter_Load);
            this.pnlPaginationRight.ResumeLayout(false);
            this.pnlPaginationRight.PerformLayout();
            this.pnlPagination.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTransferInVoter)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGridMain.ResumeLayout(false);
            this.pnlGenerate.ResumeLayout(false);
            this.pnlGenerate.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog pdfDialog;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.Panel pnlPaginationRight;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlPaginationLeft;
        private System.Windows.Forms.DataGridView dgTransferInVoter;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlGridMain;
        private System.Windows.Forms.Panel pnlGenerate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbWardName;
        private System.Windows.Forms.ComboBox cmbLLGName;
        private System.Windows.Forms.ComboBox cmbDistrictName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRegionName;
        private System.Windows.Forms.ComboBox cmbRegionName;
        private System.Windows.Forms.Label lblProvinceName;
        private System.Windows.Forms.ComboBox cmbProvinceName;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExport;
    }
}