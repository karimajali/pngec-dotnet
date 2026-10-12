namespace PNGEC.Desktop.Forms
{
    partial class Clan
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
            this.lblClanName = new System.Windows.Forms.Label();
            this.txtClanName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.lblRegionName = new System.Windows.Forms.Label();
            this.cmbRegionName = new System.Windows.Forms.ComboBox();
            this.cmbProvinceName = new System.Windows.Forms.ComboBox();
            this.lblProvinceName = new System.Windows.Forms.Label();
            this.cmbDistrictName = new System.Windows.Forms.ComboBox();
            this.lblDistrictName = new System.Windows.Forms.Label();
            this.cmbLLGName = new System.Windows.Forms.ComboBox();
            this.lblLLGName = new System.Windows.Forms.Label();
            this.cmbWardName = new System.Windows.Forms.ComboBox();
            this.lblWardName = new System.Windows.Forms.Label();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabClan = new System.Windows.Forms.TabPage();
            this.tabAudit = new System.Windows.Forms.TabPage();
            this.dgAudit = new System.Windows.Forms.DataGridView();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tab.SuspendLayout();
            this.tabClan.SuspendLayout();
            this.tabAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClanName
            // 
            this.lblClanName.AutoSize = true;
            this.lblClanName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClanName.Location = new System.Drawing.Point(65, 330);
            this.lblClanName.Name = "lblClanName";
            this.lblClanName.Size = new System.Drawing.Size(87, 20);
            this.lblClanName.TabIndex = 11;
            this.lblClanName.Text = "Clan Name";
            // 
            // txtClanName
            // 
            this.txtClanName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClanName.Location = new System.Drawing.Point(265, 330);
            this.txtClanName.Name = "txtClanName";
            this.txtClanName.Size = new System.Drawing.Size(481, 26);
            this.txtClanName.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(65, 389);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(265, 389);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(481, 28);
            this.cmbStatus.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Black;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(265, 536);
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
            this.btnCancel.Location = new System.Drawing.Point(470, 536);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 50);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(58, 613);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(0, 16);
            this.lblResponse.TabIndex = 1;
            // 
            // lblRegionName
            // 
            this.lblRegionName.AutoSize = true;
            this.lblRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegionName.Location = new System.Drawing.Point(65, 35);
            this.lblRegionName.Name = "lblRegionName";
            this.lblRegionName.Size = new System.Drawing.Size(106, 20);
            this.lblRegionName.TabIndex = 1;
            this.lblRegionName.Text = "Region Name";
            // 
            // cmbRegionName
            // 
            this.cmbRegionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegionName.FormattingEnabled = true;
            this.cmbRegionName.Location = new System.Drawing.Point(265, 35);
            this.cmbRegionName.Name = "cmbRegionName";
            this.cmbRegionName.Size = new System.Drawing.Size(481, 28);
            this.cmbRegionName.TabIndex = 2;
            this.cmbRegionName.SelectedIndexChanged += new System.EventHandler(this.cmbRegionName_SelectedIndexChanged);
            // 
            // cmbProvinceName
            // 
            this.cmbProvinceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvinceName.FormattingEnabled = true;
            this.cmbProvinceName.Location = new System.Drawing.Point(265, 90);
            this.cmbProvinceName.Name = "cmbProvinceName";
            this.cmbProvinceName.Size = new System.Drawing.Size(481, 28);
            this.cmbProvinceName.TabIndex = 4;
            this.cmbProvinceName.SelectedIndexChanged += new System.EventHandler(this.cmbProvinceName_SelectedIndexChanged);
            // 
            // lblProvinceName
            // 
            this.lblProvinceName.AutoSize = true;
            this.lblProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvinceName.Location = new System.Drawing.Point(65, 94);
            this.lblProvinceName.Name = "lblProvinceName";
            this.lblProvinceName.Size = new System.Drawing.Size(115, 20);
            this.lblProvinceName.TabIndex = 3;
            this.lblProvinceName.Text = "Province Name";
            // 
            // cmbDistrictName
            // 
            this.cmbDistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrictName.FormattingEnabled = true;
            this.cmbDistrictName.Location = new System.Drawing.Point(265, 153);
            this.cmbDistrictName.Name = "cmbDistrictName";
            this.cmbDistrictName.Size = new System.Drawing.Size(481, 28);
            this.cmbDistrictName.TabIndex = 6;
            this.cmbDistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbDistrictName_SelectedIndexChanged);
            // 
            // lblDistrictName
            // 
            this.lblDistrictName.AutoSize = true;
            this.lblDistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrictName.Location = new System.Drawing.Point(65, 153);
            this.lblDistrictName.Name = "lblDistrictName";
            this.lblDistrictName.Size = new System.Drawing.Size(104, 20);
            this.lblDistrictName.TabIndex = 5;
            this.lblDistrictName.Text = "District Name";
            // 
            // cmbLLGName
            // 
            this.cmbLLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLLGName.FormattingEnabled = true;
            this.cmbLLGName.Location = new System.Drawing.Point(265, 212);
            this.cmbLLGName.Name = "cmbLLGName";
            this.cmbLLGName.Size = new System.Drawing.Size(481, 28);
            this.cmbLLGName.TabIndex = 8;
            this.cmbLLGName.SelectedIndexChanged += new System.EventHandler(this.cmbLLGName_SelectedIndexChanged);
            // 
            // lblLLGName
            // 
            this.lblLLGName.AutoSize = true;
            this.lblLLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLLGName.Location = new System.Drawing.Point(65, 212);
            this.lblLLGName.Name = "lblLLGName";
            this.lblLLGName.Size = new System.Drawing.Size(86, 20);
            this.lblLLGName.TabIndex = 7;
            this.lblLLGName.Text = "LLG Name";
            // 
            // cmbWardName
            // 
            this.cmbWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWardName.FormattingEnabled = true;
            this.cmbWardName.Location = new System.Drawing.Point(265, 271);
            this.cmbWardName.Name = "cmbWardName";
            this.cmbWardName.Size = new System.Drawing.Size(481, 28);
            this.cmbWardName.TabIndex = 10;
            // 
            // lblWardName
            // 
            this.lblWardName.AutoSize = true;
            this.lblWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWardName.Location = new System.Drawing.Point(65, 271);
            this.lblWardName.Name = "lblWardName";
            this.lblWardName.Size = new System.Drawing.Size(93, 20);
            this.lblWardName.TabIndex = 9;
            this.lblWardName.Text = "Ward Name";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabClan);
            this.tab.Controls.Add(this.tabAudit);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(914, 643);
            this.tab.TabIndex = 0;
            // 
            // tabClan
            // 
            this.tabClan.Controls.Add(this.txtDescription);
            this.tabClan.Controls.Add(this.lblDescription);
            this.tabClan.Controls.Add(this.cmbWardName);
            this.tabClan.Controls.Add(this.lblWardName);
            this.tabClan.Controls.Add(this.lblClanName);
            this.tabClan.Controls.Add(this.cmbLLGName);
            this.tabClan.Controls.Add(this.txtClanName);
            this.tabClan.Controls.Add(this.lblLLGName);
            this.tabClan.Controls.Add(this.lblStatus);
            this.tabClan.Controls.Add(this.cmbDistrictName);
            this.tabClan.Controls.Add(this.cmbStatus);
            this.tabClan.Controls.Add(this.lblDistrictName);
            this.tabClan.Controls.Add(this.btnSave);
            this.tabClan.Controls.Add(this.cmbProvinceName);
            this.tabClan.Controls.Add(this.btnCancel);
            this.tabClan.Controls.Add(this.lblProvinceName);
            this.tabClan.Controls.Add(this.lblRegionName);
            this.tabClan.Controls.Add(this.cmbRegionName);
            this.tabClan.Location = new System.Drawing.Point(4, 25);
            this.tabClan.Name = "tabClan";
            this.tabClan.Padding = new System.Windows.Forms.Padding(3);
            this.tabClan.Size = new System.Drawing.Size(906, 614);
            this.tabClan.TabIndex = 0;
            this.tabClan.Text = "Clan";
            this.tabClan.UseVisualStyleBackColor = true;
            // 
            // tabAudit
            // 
            this.tabAudit.Controls.Add(this.dgAudit);
            this.tabAudit.Location = new System.Drawing.Point(4, 25);
            this.tabAudit.Name = "tabAudit";
            this.tabAudit.Padding = new System.Windows.Forms.Padding(3);
            this.tabAudit.Size = new System.Drawing.Size(906, 532);
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
            this.dgAudit.Size = new System.Drawing.Size(900, 526);
            this.dgAudit.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(65, 448);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(89, 20);
            this.lblDescription.TabIndex = 17;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(265, 448);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(481, 64);
            this.txtDescription.TabIndex = 18;
            // 
            // Clan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 643);
            this.Controls.Add(this.tab);
            this.Controls.Add(this.lblResponse);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Clan";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clan";
            this.Load += new System.EventHandler(this.Clan_Load);
            this.tab.ResumeLayout(false);
            this.tabClan.ResumeLayout(false);
            this.tabClan.PerformLayout();
            this.tabAudit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAudit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblClanName;
        private System.Windows.Forms.TextBox txtClanName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.Label lblRegionName;
        private System.Windows.Forms.ComboBox cmbRegionName;
        private System.Windows.Forms.ComboBox cmbProvinceName;
        private System.Windows.Forms.Label lblProvinceName;
        private System.Windows.Forms.ComboBox cmbDistrictName;
        private System.Windows.Forms.Label lblDistrictName;
        private System.Windows.Forms.ComboBox cmbLLGName;
        private System.Windows.Forms.Label lblLLGName;
        private System.Windows.Forms.ComboBox cmbWardName;
        private System.Windows.Forms.Label lblWardName;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage tabClan;
        private System.Windows.Forms.TabPage tabAudit;
        private System.Windows.Forms.DataGridView dgAudit;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
    }
}