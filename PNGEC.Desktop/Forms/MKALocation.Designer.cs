namespace PNGEC.Desktop.Forms
{
    partial class MKALocation
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
            this.tabMKALocation = new System.Windows.Forms.TabPage();
            this.cmbMKAWardName = new System.Windows.Forms.ComboBox();
            this.lblMKAWardName = new System.Windows.Forms.Label();
            this.lblMKALocationName = new System.Windows.Forms.Label();
            this.cmbMKALLGName = new System.Windows.Forms.ComboBox();
            this.txtMKALocationName = new System.Windows.Forms.TextBox();
            this.lblMKALLGName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbMKADistrictName = new System.Windows.Forms.ComboBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblMKADistrictName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabAudit = new System.Windows.Forms.TabPage();
            this.dgAudit = new System.Windows.Forms.DataGridView();
            this.tab.SuspendLayout();
            this.tabMKALocation.SuspendLayout();
            this.tabAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabMKALocation);
            this.tab.Controls.Add(this.tabAudit);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(914, 561);
            this.tab.TabIndex = 1;
            // 
            // tabMKALocation
            // 
            this.tabMKALocation.Controls.Add(this.cmbMKAWardName);
            this.tabMKALocation.Controls.Add(this.lblMKAWardName);
            this.tabMKALocation.Controls.Add(this.lblMKALocationName);
            this.tabMKALocation.Controls.Add(this.cmbMKALLGName);
            this.tabMKALocation.Controls.Add(this.txtMKALocationName);
            this.tabMKALocation.Controls.Add(this.lblMKALLGName);
            this.tabMKALocation.Controls.Add(this.lblStatus);
            this.tabMKALocation.Controls.Add(this.cmbMKADistrictName);
            this.tabMKALocation.Controls.Add(this.cmbStatus);
            this.tabMKALocation.Controls.Add(this.lblMKADistrictName);
            this.tabMKALocation.Controls.Add(this.btnSave);
            this.tabMKALocation.Controls.Add(this.btnCancel);
            this.tabMKALocation.Location = new System.Drawing.Point(4, 28);
            this.tabMKALocation.Name = "tabMKALocation";
            this.tabMKALocation.Padding = new System.Windows.Forms.Padding(3);
            this.tabMKALocation.Size = new System.Drawing.Size(906, 529);
            this.tabMKALocation.TabIndex = 0;
            this.tabMKALocation.Text = "MKA Location";
            this.tabMKALocation.UseVisualStyleBackColor = true;
            // 
            // cmbMKAWardName
            // 
            this.cmbMKAWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKAWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKAWardName.FormattingEnabled = true;
            this.cmbMKAWardName.Location = new System.Drawing.Point(267, 157);
            this.cmbMKAWardName.Name = "cmbMKAWardName";
            this.cmbMKAWardName.Size = new System.Drawing.Size(481, 33);
            this.cmbMKAWardName.TabIndex = 10;
            // 
            // lblMKAWardName
            // 
            this.lblMKAWardName.AutoSize = true;
            this.lblMKAWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKAWardName.Location = new System.Drawing.Point(65, 160);
            this.lblMKAWardName.Name = "lblMKAWardName";
            this.lblMKAWardName.Size = new System.Drawing.Size(167, 25);
            this.lblMKAWardName.TabIndex = 9;
            this.lblMKAWardName.Text = "MKA Ward Name";
            // 
            // lblMKALocationName
            // 
            this.lblMKALocationName.AutoSize = true;
            this.lblMKALocationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKALocationName.Location = new System.Drawing.Point(65, 220);
            this.lblMKALocationName.Name = "lblMKALocationName";
            this.lblMKALocationName.Size = new System.Drawing.Size(193, 25);
            this.lblMKALocationName.TabIndex = 11;
            this.lblMKALocationName.Text = "MKA Location Name";
            // 
            // cmbMKALLGName
            // 
            this.cmbMKALLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKALLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKALLGName.FormattingEnabled = true;
            this.cmbMKALLGName.Location = new System.Drawing.Point(267, 96);
            this.cmbMKALLGName.Name = "cmbMKALLGName";
            this.cmbMKALLGName.Size = new System.Drawing.Size(481, 33);
            this.cmbMKALLGName.TabIndex = 8;
            this.cmbMKALLGName.SelectedIndexChanged += new System.EventHandler(this.cmbMKALLGName_SelectedIndexChanged);
            // 
            // txtMKALocationName
            // 
            this.txtMKALocationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMKALocationName.Location = new System.Drawing.Point(267, 217);
            this.txtMKALocationName.Name = "txtMKALocationName";
            this.txtMKALocationName.Size = new System.Drawing.Size(481, 30);
            this.txtMKALocationName.TabIndex = 12;
            // 
            // lblMKALLGName
            // 
            this.lblMKALLGName.AutoSize = true;
            this.lblMKALLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKALLGName.Location = new System.Drawing.Point(65, 99);
            this.lblMKALLGName.Name = "lblMKALLGName";
            this.lblMKALLGName.Size = new System.Drawing.Size(156, 25);
            this.lblMKALLGName.TabIndex = 7;
            this.lblMKALLGName.Text = "MKA LLG Name";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(65, 273);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(68, 25);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // cmbMKADistrictName
            // 
            this.cmbMKADistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKADistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKADistrictName.FormattingEnabled = true;
            this.cmbMKADistrictName.Location = new System.Drawing.Point(267, 33);
            this.cmbMKADistrictName.Name = "cmbMKADistrictName";
            this.cmbMKADistrictName.Size = new System.Drawing.Size(481, 33);
            this.cmbMKADistrictName.TabIndex = 6;
            this.cmbMKADistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbMKADistrictName_SelectedIndexChanged);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(267, 270);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(481, 33);
            this.cmbStatus.TabIndex = 14;
            // 
            // lblMKADistrictName
            // 
            this.lblMKADistrictName.AutoSize = true;
            this.lblMKADistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKADistrictName.Location = new System.Drawing.Point(65, 33);
            this.lblMKADistrictName.Name = "lblMKADistrictName";
            this.lblMKADistrictName.Size = new System.Drawing.Size(177, 25);
            this.lblMKADistrictName.TabIndex = 5;
            this.lblMKADistrictName.Text = "MKA District Name";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Black;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(265, 359);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 50);
            this.btnSave.TabIndex = 15;
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
            this.btnCancel.Location = new System.Drawing.Point(475, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 50);
            this.btnCancel.TabIndex = 16;
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
            this.tabAudit.Size = new System.Drawing.Size(906, 529);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAudit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgAudit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.dgAudit.Size = new System.Drawing.Size(900, 523);
            this.dgAudit.TabIndex = 4;
            // 
            // MKALocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 561);
            this.Controls.Add(this.tab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKALocation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MKA Location";
            this.Load += new System.EventHandler(this.MKALocation_Load);
            this.tab.ResumeLayout(false);
            this.tabMKALocation.ResumeLayout(false);
            this.tabMKALocation.PerformLayout();
            this.tabAudit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabMKALocation;
        private System.Windows.Forms.ComboBox cmbMKAWardName;
        private System.Windows.Forms.Label lblMKAWardName;
        private System.Windows.Forms.Label lblMKALocationName;
        private System.Windows.Forms.ComboBox cmbMKALLGName;
        private System.Windows.Forms.TextBox txtMKALocationName;
        private System.Windows.Forms.Label lblMKALLGName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbMKADistrictName;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblMKADistrictName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabAudit;
        private System.Windows.Forms.DataGridView dgAudit;
    }
}