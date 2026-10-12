namespace PNGEC.Desktop.Forms
{
    partial class EnrolmentForm
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
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblGrid = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlGenerate = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.attachedfileName = new System.Windows.Forms.TextBox();
            this.uploadAttachBtn = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatchName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlGridMain = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgEnrolmentForms = new System.Windows.Forms.DataGridView();
            this.pnlGridTitle = new System.Windows.Forms.Panel();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlPaginationRight = new System.Windows.Forms.Panel();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.pnlPaginationLeft = new System.Windows.Forms.Panel();
            this.pdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlSearch.SuspendLayout();
            this.pnlGenerate.SuspendLayout();
            this.pnlGridMain.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnrolmentForms)).BeginInit();
            this.pnlGridTitle.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            this.pnlPaginationRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("Arial", 10F);
            this.btnGenerate.ForeColor = System.Drawing.Color.Black;
            this.btnGenerate.Location = new System.Drawing.Point(424, 234);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(139, 35);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHeader.Location = new System.Drawing.Point(9, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(161, 16);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Generate Form 11 Batch";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(228, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(116, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(12, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(210, 24);
            this.txtSearch.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::PNGEC.Desktop.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(350, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(116, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblGrid
            // 
            this.lblGrid.AutoSize = true;
            this.lblGrid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrid.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblGrid.Location = new System.Drawing.Point(12, 17);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(161, 16);
            this.lblGrid.TabIndex = 0;
            this.lblGrid.Text = "History of Form 11 Batch";
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
            this.pnlSearch.TabIndex = 0;
            // 
            // pnlGenerate
            // 
            this.pnlGenerate.Controls.Add(this.label6);
            this.pnlGenerate.Controls.Add(this.txtBatchNumber);
            this.pnlGenerate.Controls.Add(this.attachedfileName);
            this.pnlGenerate.Controls.Add(this.uploadAttachBtn);
            this.pnlGenerate.Controls.Add(this.txtComment);
            this.pnlGenerate.Controls.Add(this.label5);
            this.pnlGenerate.Controls.Add(this.txtBatchName);
            this.pnlGenerate.Controls.Add(this.label4);
            this.pnlGenerate.Controls.Add(this.lblHeader);
            this.pnlGenerate.Controls.Add(this.btnGenerate);
            this.pnlGenerate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGenerate.Location = new System.Drawing.Point(0, 38);
            this.pnlGenerate.Name = "pnlGenerate";
            this.pnlGenerate.Size = new System.Drawing.Size(1260, 276);
            this.pnlGenerate.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(127, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Number Of Form";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchNumber.Location = new System.Drawing.Point(151, 73);
            this.txtBatchNumber.MaxLength = 7;
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(413, 26);
            this.txtBatchNumber.TabIndex = 7;
            this.txtBatchNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBatchNumber_KeyPress);
            // 
            // attachedfileName
            // 
            this.attachedfileName.Enabled = false;
            this.attachedfileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attachedfileName.Location = new System.Drawing.Point(262, 202);
            this.attachedfileName.MaxLength = 50;
            this.attachedfileName.Name = "attachedfileName";
            this.attachedfileName.Size = new System.Drawing.Size(301, 26);
            this.attachedfileName.TabIndex = 16;
            // 
            // uploadAttachBtn
            // 
            this.uploadAttachBtn.BackColor = System.Drawing.Color.Transparent;
            this.uploadAttachBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadAttachBtn.Font = new System.Drawing.Font("Arial", 10F);
            this.uploadAttachBtn.ForeColor = System.Drawing.Color.Black;
            this.uploadAttachBtn.Location = new System.Drawing.Point(151, 201);
            this.uploadAttachBtn.Name = "uploadAttachBtn";
            this.uploadAttachBtn.Size = new System.Drawing.Size(105, 30);
            this.uploadAttachBtn.TabIndex = 5;
            this.uploadAttachBtn.Text = "Attachment";
            this.uploadAttachBtn.UseVisualStyleBackColor = false;
            this.uploadAttachBtn.Click += new System.EventHandler(this.uploadAttachBtn_Click);
            // 
            // txtComment
            // 
            this.txtComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Location = new System.Drawing.Point(151, 104);
            this.txtComment.MaxLength = 3000;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(413, 93);
            this.txtComment.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(11, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Comment";
            // 
            // txtBatchName
            // 
            this.txtBatchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchName.Location = new System.Drawing.Point(151, 42);
            this.txtBatchName.MaxLength = 50;
            this.txtBatchName.Name = "txtBatchName";
            this.txtBatchName.Size = new System.Drawing.Size(413, 26);
            this.txtBatchName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Batch Name";
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.pnlGrid);
            this.pnlGridMain.Controls.Add(this.pnlGridTitle);
            this.pnlGridMain.Controls.Add(this.pnlPagination);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 314);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(1260, 365);
            this.pnlGridMain.TabIndex = 32;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgEnrolmentForms);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 48);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1260, 269);
            this.pnlGrid.TabIndex = 32;
            // 
            // dgEnrolmentForms
            // 
            this.dgEnrolmentForms.AllowUserToAddRows = false;
            this.dgEnrolmentForms.AllowUserToDeleteRows = false;
            this.dgEnrolmentForms.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgEnrolmentForms.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgEnrolmentForms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgEnrolmentForms.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgEnrolmentForms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgEnrolmentForms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgEnrolmentForms.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgEnrolmentForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgEnrolmentForms.EnableHeadersVisualStyles = false;
            this.dgEnrolmentForms.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgEnrolmentForms.Location = new System.Drawing.Point(0, 0);
            this.dgEnrolmentForms.MultiSelect = false;
            this.dgEnrolmentForms.Name = "dgEnrolmentForms";
            this.dgEnrolmentForms.ReadOnly = true;
            this.dgEnrolmentForms.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgEnrolmentForms.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgEnrolmentForms.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgEnrolmentForms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEnrolmentForms.Size = new System.Drawing.Size(1260, 269);
            this.dgEnrolmentForms.TabIndex = 0;
            this.dgEnrolmentForms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnrolmentForms_CellContentClick);
            // 
            // pnlGridTitle
            // 
            this.pnlGridTitle.Controls.Add(this.lblGrid);
            this.pnlGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGridTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlGridTitle.Name = "pnlGridTitle";
            this.pnlGridTitle.Size = new System.Drawing.Size(1260, 48);
            this.pnlGridTitle.TabIndex = 0;
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.pnlPaginationRight);
            this.pnlPagination.Controls.Add(this.pnlPaginationLeft);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 317);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1260, 48);
            this.pnlPagination.TabIndex = 1;
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
            this.pnlPaginationRight.TabIndex = 1;
            this.pnlPaginationRight.Visible = false;
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPageSize.FormattingEnabled = true;
            this.cmbPageSize.Location = new System.Drawing.Point(86, 6);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(121, 24);
            this.cmbPageSize.TabIndex = 0;
            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.CmbPageSize_SelectedIndexChanged);
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
            // pnlPaginationLeft
            // 
            this.pnlPaginationLeft.Location = new System.Drawing.Point(12, 0);
            this.pnlPaginationLeft.Name = "pnlPaginationLeft";
            this.pnlPaginationLeft.Size = new System.Drawing.Size(869, 48);
            this.pnlPaginationLeft.TabIndex = 0;
            // 
            // pdfDialog
            // 
            this.pdfDialog.CheckPathExists = false;
            this.pdfDialog.CreatePrompt = true;
            this.pdfDialog.DefaultExt = "pdf";
            this.pdfDialog.Filter = "PDF files (*.pdf)|*.pdf";
            // 
            // EnrolmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 679);
            this.Controls.Add(this.pnlGridMain);
            this.Controls.Add(this.pnlGenerate);
            this.Controls.Add(this.pnlSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnrolmentForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EnrolmentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnrolmentForm_FormClosing);
            this.Load += new System.EventHandler(this.EnrolmentForm_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGenerate.ResumeLayout(false);
            this.pnlGenerate.PerformLayout();
            this.pnlGridMain.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEnrolmentForms)).EndInit();
            this.pnlGridTitle.ResumeLayout(false);
            this.pnlGridTitle.PerformLayout();
            this.pnlPagination.ResumeLayout(false);
            this.pnlPaginationRight.ResumeLayout(false);
            this.pnlPaginationRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlGenerate;
        private System.Windows.Forms.Panel pnlGridMain;
        private System.Windows.Forms.SaveFileDialog pdfDialog;
        private System.Windows.Forms.Panel pnlGridTitle;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlPaginationLeft;
        private System.Windows.Forms.Panel pnlPaginationRight;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.DataGridView dgEnrolmentForms;
        private System.Windows.Forms.TextBox txtBatchName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button uploadAttachBtn;
        private System.Windows.Forms.TextBox attachedfileName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBatchNumber;
    }
}