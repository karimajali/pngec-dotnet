namespace PNGEC.Desktop.Forms
{
    partial class WardQualityCheck
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.pnlGridMain = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgEnrolmentForms = new System.Windows.Forms.DataGridView();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlPaginationRight = new System.Windows.Forms.Panel();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.pnlPaginationLeft = new System.Windows.Forms.Panel();
            this.pdfDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlSearch.SuspendLayout();
            this.pnlGridMain.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnrolmentForms)).BeginInit();
            this.pnlPagination.SuspendLayout();
            this.pnlPaginationRight.SuspendLayout();
            this.SuspendLayout();
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
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(16, 10);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(279, 29);
            this.txtSearch.TabIndex = 0;
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
            this.pnlSearch.TabIndex = 0;
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.pnlGrid);
            this.pnlGridMain.Controls.Add(this.pnlPagination);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 47);
            this.pnlGridMain.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Size = new System.Drawing.Size(1680, 789);
            this.pnlGridMain.TabIndex = 32;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgEnrolmentForms);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1680, 730);
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
            this.dgEnrolmentForms.Margin = new System.Windows.Forms.Padding(4);
            this.dgEnrolmentForms.MultiSelect = false;
            this.dgEnrolmentForms.Name = "dgEnrolmentForms";
            this.dgEnrolmentForms.ReadOnly = true;
            this.dgEnrolmentForms.RowHeadersWidth = 51;
            this.dgEnrolmentForms.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgEnrolmentForms.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgEnrolmentForms.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgEnrolmentForms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgEnrolmentForms.Size = new System.Drawing.Size(1680, 730);
            this.dgEnrolmentForms.TabIndex = 0;
            this.dgEnrolmentForms.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgEnrolmentForms_CellClick);
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.pnlPaginationRight);
            this.pnlPagination.Controls.Add(this.pnlPaginationLeft);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 730);
            this.pnlPagination.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1680, 59);
            this.pnlPagination.TabIndex = 1;
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
            // WardQualityCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1680, 836);
            this.Controls.Add(this.pnlGridMain);
            this.Controls.Add(this.pnlSearch);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WardQualityCheck";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EnrolmentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnrolmentForm_FormClosing);
            this.Load += new System.EventHandler(this.EnrolmentForm_Load);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlGridMain.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgEnrolmentForms)).EndInit();
            this.pnlPagination.ResumeLayout(false);
            this.pnlPaginationRight.ResumeLayout(false);
            this.pnlPaginationRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Panel pnlGridMain;
        private System.Windows.Forms.SaveFileDialog pdfDialog;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlPaginationLeft;
        private System.Windows.Forms.Panel pnlPaginationRight;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.DataGridView dgEnrolmentForms;
    }
}