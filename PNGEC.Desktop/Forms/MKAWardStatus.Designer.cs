namespace PNGEC.Desktop.Forms
{
    partial class MKAWardStatus
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
            this.pnlControl = new System.Windows.Forms.Panel();
            this.pnlExport = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.pnlPagination = new System.Windows.Forms.Panel();
            this.pnlPaginationRight = new System.Windows.Forms.Panel();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.pnlPaginationLeft = new System.Windows.Forms.Panel();
            this.dgWardStatus = new System.Windows.Forms.DataGridView();
            this.pnlControl.SuspendLayout();
            this.pnlExport.SuspendLayout();
            this.pnlPagination.SuspendLayout();
            this.pnlPaginationRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWardStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.pnlExport);
            this.pnlControl.Controls.Add(this.dtpFromDate);
            this.pnlControl.Controls.Add(this.dtpToDate);
            this.pnlControl.Controls.Add(this.btnSearch);
            this.pnlControl.Controls.Add(this.btnRefresh);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControl.Location = new System.Drawing.Point(0, 0);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(4);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(1471, 60);
            this.pnlControl.TabIndex = 1;
            // 
            // pnlExport
            // 
            this.pnlExport.Controls.Add(this.btnExport);
            this.pnlExport.Location = new System.Drawing.Point(767, 4);
            this.pnlExport.Margin = new System.Windows.Forms.Padding(4);
            this.pnlExport.Name = "pnlExport";
            this.pnlExport.Size = new System.Drawing.Size(163, 53);
            this.pnlExport.TabIndex = 14;
            this.pnlExport.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(0, 9);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(155, 37);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Checked = false;
            this.dtpFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(16, 15);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.ShowCheckBox = true;
            this.dtpFromDate.Size = new System.Drawing.Size(185, 27);
            this.dtpFromDate.TabIndex = 1;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Checked = false;
            this.dtpToDate.CustomFormat = "dd-MM-yyyy";
            this.dtpToDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(225, 15);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.ShowCheckBox = true;
            this.dtpToDate.Size = new System.Drawing.Size(185, 27);
            this.dtpToDate.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(441, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(155, 37);
            this.btnSearch.TabIndex = 2;
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
            this.btnRefresh.Location = new System.Drawing.Point(604, 12);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(155, 37);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // pnlPagination
            // 
            this.pnlPagination.Controls.Add(this.pnlPaginationRight);
            this.pnlPagination.Controls.Add(this.pnlPaginationLeft);
            this.pnlPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPagination.Location = new System.Drawing.Point(0, 495);
            this.pnlPagination.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPagination.Name = "pnlPagination";
            this.pnlPagination.Size = new System.Drawing.Size(1471, 59);
            this.pnlPagination.TabIndex = 3;
            // 
            // pnlPaginationRight
            // 
            this.pnlPaginationRight.BackColor = System.Drawing.SystemColors.Control;
            this.pnlPaginationRight.Controls.Add(this.cmbPageSize);
            this.pnlPaginationRight.Controls.Add(this.lblPageSize);
            this.pnlPaginationRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlPaginationRight.Location = new System.Drawing.Point(1186, 0);
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
            // dgWardStatus
            // 
            this.dgWardStatus.AllowUserToAddRows = false;
            this.dgWardStatus.AllowUserToDeleteRows = false;
            this.dgWardStatus.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgWardStatus.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgWardStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgWardStatus.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgWardStatus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgWardStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgWardStatus.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgWardStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgWardStatus.EnableHeadersVisualStyles = false;
            this.dgWardStatus.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgWardStatus.Location = new System.Drawing.Point(0, 60);
            this.dgWardStatus.Margin = new System.Windows.Forms.Padding(4);
            this.dgWardStatus.MultiSelect = false;
            this.dgWardStatus.Name = "dgWardStatus";
            this.dgWardStatus.ReadOnly = true;
            this.dgWardStatus.RowHeadersWidth = 51;
            this.dgWardStatus.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgWardStatus.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgWardStatus.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgWardStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWardStatus.Size = new System.Drawing.Size(1471, 435);
            this.dgWardStatus.TabIndex = 4;
            // 
            // MKAWardStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 554);
            this.Controls.Add(this.dgWardStatus);
            this.Controls.Add(this.pnlPagination);
            this.Controls.Add(this.pnlControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKAWardStatus";
            this.ShowIcon = false;
            this.Text = "MKA Summary Field";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MKAWardStatus_FormClosing);
            this.Load += new System.EventHandler(this.MKAWardStatus_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlExport.ResumeLayout(false);
            this.pnlPagination.ResumeLayout(false);
            this.pnlPaginationRight.ResumeLayout(false);
            this.pnlPaginationRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWardStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Panel pnlExport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Panel pnlPaginationRight;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.Panel pnlPaginationLeft;
        private System.Windows.Forms.DataGridView dgWardStatus;
    }
}