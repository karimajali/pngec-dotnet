namespace PNGEC.Desktop.Forms
{
    partial class MKADistrict
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
            this.tab = new System.Windows.Forms.TabControl();
            this.tabMKADistrict = new System.Windows.Forms.TabPage();
            this.txtMKADistrictName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMKADistrictName = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabAudit = new System.Windows.Forms.TabPage();
            this.dgAudit = new System.Windows.Forms.DataGridView();
            this.tab.SuspendLayout();
            this.tabMKADistrict.SuspendLayout();
            this.tabAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabMKADistrict);
            this.tab.Controls.Add(this.tabAudit);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Arial", 10F);
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(914, 454);
            this.tab.TabIndex = 1;
            // 
            // tabMKADistrict
            // 
            this.tabMKADistrict.Controls.Add(this.txtMKADistrictName);
            this.tabMKADistrict.Controls.Add(this.lblStatus);
            this.tabMKADistrict.Controls.Add(this.lblMKADistrictName);
            this.tabMKADistrict.Controls.Add(this.cmbStatus);
            this.tabMKADistrict.Controls.Add(this.btnSave);
            this.tabMKADistrict.Controls.Add(this.btnCancel);
            this.tabMKADistrict.Font = new System.Drawing.Font("Arial", 10F);
            this.tabMKADistrict.Location = new System.Drawing.Point(4, 28);
            this.tabMKADistrict.Name = "tabMKADistrict";
            this.tabMKADistrict.Padding = new System.Windows.Forms.Padding(3);
            this.tabMKADistrict.Size = new System.Drawing.Size(906, 422);
            this.tabMKADistrict.TabIndex = 0;
            this.tabMKADistrict.Text = "MKA District";
            this.tabMKADistrict.UseVisualStyleBackColor = true;
            // 
            // txtMKADistrictName
            // 
            this.txtMKADistrictName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMKADistrictName.Location = new System.Drawing.Point(265, 35);
            this.txtMKADistrictName.Name = "txtMKADistrictName";
            this.txtMKADistrictName.Size = new System.Drawing.Size(481, 30);
            this.txtMKADistrictName.TabIndex = 8;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(65, 95);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(66, 23);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Status";
            // 
            // lblMKADistrictName
            // 
            this.lblMKADistrictName.AutoSize = true;
            this.lblMKADistrictName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKADistrictName.Location = new System.Drawing.Point(65, 35);
            this.lblMKADistrictName.Name = "lblMKADistrictName";
            this.lblMKADistrictName.Size = new System.Drawing.Size(176, 23);
            this.lblMKADistrictName.TabIndex = 1;
            this.lblMKADistrictName.Text = "MKA District Name";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(265, 95);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(481, 31);
            this.cmbStatus.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Black;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(265, 205);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 50);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(475, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 50);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabAudit
            // 
            this.tabAudit.Controls.Add(this.dgAudit);
            this.tabAudit.Location = new System.Drawing.Point(4, 28);
            this.tabAudit.Name = "tabAudit";
            this.tabAudit.Padding = new System.Windows.Forms.Padding(3);
            this.tabAudit.Size = new System.Drawing.Size(906, 422);
            this.tabAudit.TabIndex = 1;
            this.tabAudit.Text = "Audit Trail";
            this.tabAudit.UseVisualStyleBackColor = true;
            // 
            // dgAudit
            // 
            this.dgAudit.AllowUserToAddRows = false;
            this.dgAudit.AllowUserToDeleteRows = false;
            this.dgAudit.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgAudit.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgAudit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgAudit.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAudit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAudit.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgAudit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAudit.EnableHeadersVisualStyles = false;
            this.dgAudit.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgAudit.Location = new System.Drawing.Point(3, 3);
            this.dgAudit.MultiSelect = false;
            this.dgAudit.Name = "dgAudit";
            this.dgAudit.ReadOnly = true;
            this.dgAudit.RowHeadersWidth = 51;
            this.dgAudit.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgAudit.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgAudit.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgAudit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAudit.Size = new System.Drawing.Size(900, 416);
            this.dgAudit.TabIndex = 4;
            // 
            // MKADistrict
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 454);
            this.Controls.Add(this.tab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKADistrict";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MKA District";
            this.Load += new System.EventHandler(this.MKADistrict_Load);
            this.tab.ResumeLayout(false);
            this.tabMKADistrict.ResumeLayout(false);
            this.tabMKADistrict.PerformLayout();
            this.tabAudit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabMKADistrict;
        private System.Windows.Forms.TextBox txtMKADistrictName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabAudit;
        private System.Windows.Forms.DataGridView dgAudit;
        private System.Windows.Forms.Label lblMKADistrictName;
    }
}