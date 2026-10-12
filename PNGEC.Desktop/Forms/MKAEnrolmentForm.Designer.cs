namespace PNGEC.Desktop.Forms
{
    partial class MKAEnrolmentForm
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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlGenerate = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.attachedfileName = new System.Windows.Forms.TextBox();
            this.uploadAttachBtn = new System.Windows.Forms.Button();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatchName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.pnlGridTitle = new System.Windows.Forms.Panel();
            this.lblGrid = new System.Windows.Forms.Label();
            this.dgMKAEnrolmentForms = new System.Windows.Forms.DataGridView();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlPaginationRight = new System.Windows.Forms.Panel();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.pnlPaginationLeft = new System.Windows.Forms.Panel();
            this.pdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlSearch.SuspendLayout();
            this.pnlGenerate.SuspendLayout();
            this.pnlGridTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMKAEnrolmentForms)).BeginInit();
            this.pnlPagination.SuspendLayout();
            this.pnlPaginationRight.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.btnRefresh);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(1680, 47);
            this.pnlSearch.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(16, 10);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(279, 29);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(304, 6);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(155, 37);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::PNGEC.Desktop.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(467, 6);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(155, 37);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.pnlGenerate.Location = new System.Drawing.Point(0, 47);
            this.pnlGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGenerate.Name = "pnlGenerate";
            this.pnlGenerate.Size = new System.Drawing.Size(1680, 340);
            this.pnlGenerate.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(11, 94);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 25);
            this.label6.TabIndex = 19;
            this.label6.Text = "Number Of Form";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchNumber.Location = new System.Drawing.Point(201, 90);
            this.txtBatchNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtBatchNumber.MaxLength = 7;
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(549, 30);
            this.txtBatchNumber.TabIndex = 7;
            this.txtBatchNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBatchNumber_KeyPress);
            // 
            // attachedfileName
            // 
            this.attachedfileName.Enabled = false;
            this.attachedfileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attachedfileName.Location = new System.Drawing.Point(349, 249);
            this.attachedfileName.Margin = new System.Windows.Forms.Padding(4);
            this.attachedfileName.MaxLength = 50;
            this.attachedfileName.Name = "attachedfileName";
            this.attachedfileName.Size = new System.Drawing.Size(400, 30);
            this.attachedfileName.TabIndex = 16;
            // 
            // uploadAttachBtn
            // 
            this.uploadAttachBtn.BackColor = System.Drawing.Color.Transparent;
            this.uploadAttachBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uploadAttachBtn.Font = new System.Drawing.Font("Arial", 10F);
            this.uploadAttachBtn.ForeColor = System.Drawing.Color.Black;
            this.uploadAttachBtn.Location = new System.Drawing.Point(201, 247);
            this.uploadAttachBtn.Margin = new System.Windows.Forms.Padding(4);
            this.uploadAttachBtn.Name = "uploadAttachBtn";
            this.uploadAttachBtn.Size = new System.Drawing.Size(140, 37);
            this.uploadAttachBtn.TabIndex = 5;
            this.uploadAttachBtn.Text = "Attachment";
            this.uploadAttachBtn.UseVisualStyleBackColor = false;
            this.uploadAttachBtn.Click += new System.EventHandler(this.uploadAttachBtn_Click);
            // 
            // txtComment
            // 
            this.txtComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Location = new System.Drawing.Point(201, 128);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.MaxLength = 3000;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(549, 114);
            this.txtComment.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Comment";
            // 
            // txtBatchName
            // 
            this.txtBatchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchName.Location = new System.Drawing.Point(201, 52);
            this.txtBatchName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBatchName.MaxLength = 50;
            this.txtBatchName.Name = "txtBatchName";
            this.txtBatchName.Size = new System.Drawing.Size(549, 30);
            this.txtBatchName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 55);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 25);
            this.label4.TabIndex = 17;
            this.label4.Text = "Batch Name";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHeader.Location = new System.Drawing.Point(12, 18);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(240, 19);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Generate MKA Form 11 Batch";
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.Transparent;
            this.btnGenerate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Font = new System.Drawing.Font("Arial", 10F);
            this.btnGenerate.ForeColor = System.Drawing.Color.Black;
            this.btnGenerate.Location = new System.Drawing.Point(565, 288);
            this.btnGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(185, 43);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // pnlGridTitle
            // 
            this.pnlGridTitle.Controls.Add(this.lblGrid);
            this.pnlGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGridTitle.Location = new System.Drawing.Point(0, 387);
            this.pnlGridTitle.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGridTitle.Name = "pnlGridTitle";
            this.pnlGridTitle.Size = new System.Drawing.Size(1680, 59);
            this.pnlGridTitle.TabIndex = 3;
            // 
            // lblGrid
            // 
            this.lblGrid.AutoSize = true;
            this.lblGrid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrid.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblGrid.Location = new System.Drawing.Point(16, 21);
            this.lblGrid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGrid.Name = "lblGrid";
            this.lblGrid.Size = new System.Drawing.Size(245, 19);
            this.lblGrid.TabIndex = 0;
            this.lblGrid.Text = "History of MKA Form 11 Batch";
            // 
            // dgMKAEnrolmentForms
            // 
            this.dgMKAEnrolmentForms.AllowUserToAddRows = false;
            this.dgMKAEnrolmentForms.AllowUserToDeleteRows = false;
            this.dgMKAEnrolmentForms.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgMKAEnrolmentForms.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMKAEnrolmentForms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgMKAEnrolmentForms.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMKAEnrolmentForms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgMKAEnrolmentForms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMKAEnrolmentForms.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgMKAEnrolmentForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMKAEnrolmentForms.EnableHeadersVisualStyles = false;
            this.dgMKAEnrolmentForms.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgMKAEnrolmentForms.Location = new System.Drawing.Point(0, 0);
            this.dgMKAEnrolmentForms.Margin = new System.Windows.Forms.Padding(4);
            this.dgMKAEnrolmentForms.MultiSelect = false;
            this.dgMKAEnrolmentForms.Name = "dgMKAEnrolmentForms";
            this.dgMKAEnrolmentForms.ReadOnly = true;
            this.dgMKAEnrolmentForms.RowHeadersWidth = 51;
            this.dgMKAEnrolmentForms.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgMKAEnrolmentForms.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgMKAEnrolmentForms.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgMKAEnrolmentForms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgMKAEnrolmentForms.Size = new System.Drawing.Size(1680, 331);
            this.dgMKAEnrolmentForms.TabIndex = 0;
            this.dgMKAEnrolmentForms.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMKAEnrolmentForms_CellContentClick);
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.pnlPaginationRight);
            this.pnlPagination.Controls.Add(this.pnlPaginationLeft);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 777);
            this.pnlPagination.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1680, 59);
            this.pnlPagination.TabIndex = 5;
            // 
            // pnlPaginationRight
            // 
            this.pnlPaginationRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPaginationRight.Controls.Add(this.cmbPageSize);
            this.pnlPaginationRight.Controls.Add(this.lblPageSize);
            this.pnlPaginationRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPaginationRight.Location = new System.Drawing.Point(1395, 0);
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
            this.cmbPageSize.TabIndex = 0;
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
            this.pnlPaginationLeft.Location = new System.Drawing.Point(16, 0);
            this.pnlPaginationLeft.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPaginationLeft.Name = "pnlPaginationLeft";
            this.pnlPaginationLeft.Size = new System.Drawing.Size(1159, 59);
            this.pnlPaginationLeft.TabIndex = 0;
            // 
            // pdfDialog
            // 
            this.pdfDialog.CheckPathExists = false;
            this.pdfDialog.CreatePrompt = true;
            this.pdfDialog.DefaultExt = "pdf";
            this.pdfDialog.Filter = "PDF files (*.pdf)|*.pdf";
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgMKAEnrolmentForms);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 446);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1680, 331);
            this.pnlGrid.TabIndex = 6;
            // 
            // MKAEnrolmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1680, 836);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlPagination);
            this.Controls.Add(this.pnlGridTitle);
            this.Controls.Add(this.pnlGenerate);
            this.Controls.Add(this.pnlSearch);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKAEnrolmentForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MKA Enrollment Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MKAEnrolmentForm_FormClosing);
            this.Load += new System.EventHandler(this.MKAEnrolmentForm_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGenerate.ResumeLayout(false);
            this.pnlGenerate.PerformLayout();
            this.pnlGridTitle.ResumeLayout(false);
            this.pnlGridTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMKAEnrolmentForms)).EndInit();
            this.pnlPagination.ResumeLayout(false);
            this.pnlPaginationRight.ResumeLayout(false);
            this.pnlPaginationRight.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlGenerate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBatchNumber;
        private System.Windows.Forms.TextBox attachedfileName;
        private System.Windows.Forms.Button uploadAttachBtn;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatchName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Panel pnlGridTitle;
        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.DataGridView dgMKAEnrolmentForms;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlPaginationRight;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.Panel pnlPaginationLeft;
        private System.Windows.Forms.SaveFileDialog pdfDialog;
        private System.Windows.Forms.Panel pnlGrid;
    }
}