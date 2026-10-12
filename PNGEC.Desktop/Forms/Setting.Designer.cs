
namespace PNGEC.Desktop.Forms
{
    partial class Setting
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
            this.lblProvisionalCutOffDOB = new System.Windows.Forms.Label();
            this.dtpProvisionalCutOffDOB = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpDCSetting = new System.Windows.Forms.GroupBox();
            this.txtDCVersion = new System.Windows.Forms.TextBox();
            this.txtDCDateReset = new System.Windows.Forms.TextBox();
            this.lblDCVersion = new System.Windows.Forms.Label();
            this.lblDCDateReset = new System.Windows.Forms.Label();
            this.txtDCPassword = new System.Windows.Forms.TextBox();
            this.lblDCPassword = new System.Windows.Forms.Label();
            this.grpGeoSetting = new System.Windows.Forms.GroupBox();
            this.txtGeoVersion = new System.Windows.Forms.TextBox();
            this.txtGeoRefreshDate = new System.Windows.Forms.TextBox();
            this.lblGeoVersion = new System.Windows.Forms.Label();
            this.lblGeoRefreshDate = new System.Windows.Forms.Label();
            this.grpOccupationSetting = new System.Windows.Forms.GroupBox();
            this.txtOccupationVersion = new System.Windows.Forms.TextBox();
            this.txtOccupationRefreshDate = new System.Windows.Forms.TextBox();
            this.lblOccupationVersion = new System.Windows.Forms.Label();
            this.lblOccupationRefreshDate = new System.Windows.Forms.Label();
            this.btnUpdateSetting = new System.Windows.Forms.Button();
            this.grpDCSetting.SuspendLayout();
            this.grpGeoSetting.SuspendLayout();
            this.grpOccupationSetting.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProvisionalCutOffDOB
            // 
            this.lblProvisionalCutOffDOB.AutoSize = true;
            this.lblProvisionalCutOffDOB.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvisionalCutOffDOB.Location = new System.Drawing.Point(39, 34);
            this.lblProvisionalCutOffDOB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProvisionalCutOffDOB.Name = "lblProvisionalCutOffDOB";
            this.lblProvisionalCutOffDOB.Size = new System.Drawing.Size(241, 19);
            this.lblProvisionalCutOffDOB.TabIndex = 0;
            this.lblProvisionalCutOffDOB.Text = "Provisional Cut-Off Date of birth";
            // 
            // dtpProvisionalCutOffDOB
            // 
            this.dtpProvisionalCutOffDOB.CustomFormat = "dd-MM-yyyy";
            this.dtpProvisionalCutOffDOB.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpProvisionalCutOffDOB.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpProvisionalCutOffDOB.Location = new System.Drawing.Point(353, 26);
            this.dtpProvisionalCutOffDOB.Margin = new System.Windows.Forms.Padding(4);
            this.dtpProvisionalCutOffDOB.Name = "dtpProvisionalCutOffDOB";
            this.dtpProvisionalCutOffDOB.Size = new System.Drawing.Size(147, 29);
            this.dtpProvisionalCutOffDOB.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(508, 25);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 37);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Update Date";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpDCSetting
            // 
            this.grpDCSetting.Controls.Add(this.txtDCVersion);
            this.grpDCSetting.Controls.Add(this.txtDCDateReset);
            this.grpDCSetting.Controls.Add(this.lblDCVersion);
            this.grpDCSetting.Controls.Add(this.lblDCDateReset);
            this.grpDCSetting.Controls.Add(this.txtDCPassword);
            this.grpDCSetting.Controls.Add(this.lblDCPassword);
            this.grpDCSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDCSetting.Location = new System.Drawing.Point(43, 73);
            this.grpDCSetting.Name = "grpDCSetting";
            this.grpDCSetting.Size = new System.Drawing.Size(457, 154);
            this.grpDCSetting.TabIndex = 3;
            this.grpDCSetting.TabStop = false;
            this.grpDCSetting.Text = "DC Setting";
            // 
            // txtDCVersion
            // 
            this.txtDCVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDCVersion.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDCVersion.Location = new System.Drawing.Point(133, 98);
            this.txtDCVersion.MaxLength = 5;
            this.txtDCVersion.Name = "txtDCVersion";
            this.txtDCVersion.Size = new System.Drawing.Size(306, 29);
            this.txtDCVersion.TabIndex = 5;
            this.txtDCVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDCVersion_KeyPress);
            // 
            // txtDCDateReset
            // 
            this.txtDCDateReset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDCDateReset.Enabled = false;
            this.txtDCDateReset.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDCDateReset.Location = new System.Drawing.Point(133, 63);
            this.txtDCDateReset.Name = "txtDCDateReset";
            this.txtDCDateReset.Size = new System.Drawing.Size(306, 29);
            this.txtDCDateReset.TabIndex = 3;
            // 
            // lblDCVersion
            // 
            this.lblDCVersion.AutoSize = true;
            this.lblDCVersion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDCVersion.Location = new System.Drawing.Point(7, 103);
            this.lblDCVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDCVersion.Name = "lblDCVersion";
            this.lblDCVersion.Size = new System.Drawing.Size(64, 19);
            this.lblDCVersion.TabIndex = 4;
            this.lblDCVersion.Text = "Version";
            // 
            // lblDCDateReset
            // 
            this.lblDCDateReset.AutoSize = true;
            this.lblDCDateReset.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDCDateReset.Location = new System.Drawing.Point(7, 68);
            this.lblDCDateReset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDCDateReset.Name = "lblDCDateReset";
            this.lblDCDateReset.Size = new System.Drawing.Size(89, 19);
            this.lblDCDateReset.TabIndex = 2;
            this.lblDCDateReset.Text = "Date Reset";
            // 
            // txtDCPassword
            // 
            this.txtDCPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDCPassword.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDCPassword.Location = new System.Drawing.Point(133, 28);
            this.txtDCPassword.MaxLength = 25;
            this.txtDCPassword.Name = "txtDCPassword";
            this.txtDCPassword.PasswordChar = '*';
            this.txtDCPassword.Size = new System.Drawing.Size(306, 29);
            this.txtDCPassword.TabIndex = 1;
            // 
            // lblDCPassword
            // 
            this.lblDCPassword.AutoSize = true;
            this.lblDCPassword.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDCPassword.Location = new System.Drawing.Point(7, 32);
            this.lblDCPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDCPassword.Name = "lblDCPassword";
            this.lblDCPassword.Size = new System.Drawing.Size(109, 19);
            this.lblDCPassword.TabIndex = 0;
            this.lblDCPassword.Text = "DC Password";
            // 
            // grpGeoSetting
            // 
            this.grpGeoSetting.Controls.Add(this.txtGeoVersion);
            this.grpGeoSetting.Controls.Add(this.txtGeoRefreshDate);
            this.grpGeoSetting.Controls.Add(this.lblGeoVersion);
            this.grpGeoSetting.Controls.Add(this.lblGeoRefreshDate);
            this.grpGeoSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGeoSetting.Location = new System.Drawing.Point(43, 345);
            this.grpGeoSetting.Name = "grpGeoSetting";
            this.grpGeoSetting.Size = new System.Drawing.Size(457, 110);
            this.grpGeoSetting.TabIndex = 5;
            this.grpGeoSetting.TabStop = false;
            this.grpGeoSetting.Text = "Geo Setting";
            // 
            // txtGeoVersion
            // 
            this.txtGeoVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGeoVersion.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGeoVersion.Location = new System.Drawing.Point(133, 60);
            this.txtGeoVersion.MaxLength = 5;
            this.txtGeoVersion.Name = "txtGeoVersion";
            this.txtGeoVersion.Size = new System.Drawing.Size(306, 29);
            this.txtGeoVersion.TabIndex = 3;
            this.txtGeoVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGeoVersion_KeyPress);
            // 
            // txtGeoRefreshDate
            // 
            this.txtGeoRefreshDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGeoRefreshDate.Enabled = false;
            this.txtGeoRefreshDate.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGeoRefreshDate.Location = new System.Drawing.Point(133, 25);
            this.txtGeoRefreshDate.Name = "txtGeoRefreshDate";
            this.txtGeoRefreshDate.Size = new System.Drawing.Size(306, 29);
            this.txtGeoRefreshDate.TabIndex = 1;
            // 
            // lblGeoVersion
            // 
            this.lblGeoVersion.AutoSize = true;
            this.lblGeoVersion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeoVersion.Location = new System.Drawing.Point(7, 65);
            this.lblGeoVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGeoVersion.Name = "lblGeoVersion";
            this.lblGeoVersion.Size = new System.Drawing.Size(64, 19);
            this.lblGeoVersion.TabIndex = 2;
            this.lblGeoVersion.Text = "Version";
            // 
            // lblGeoRefreshDate
            // 
            this.lblGeoRefreshDate.AutoSize = true;
            this.lblGeoRefreshDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeoRefreshDate.Location = new System.Drawing.Point(7, 30);
            this.lblGeoRefreshDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGeoRefreshDate.Name = "lblGeoRefreshDate";
            this.lblGeoRefreshDate.Size = new System.Drawing.Size(105, 19);
            this.lblGeoRefreshDate.TabIndex = 0;
            this.lblGeoRefreshDate.Text = "Refresh Date";
            // 
            // grpOccupationSetting
            // 
            this.grpOccupationSetting.Controls.Add(this.txtOccupationVersion);
            this.grpOccupationSetting.Controls.Add(this.txtOccupationRefreshDate);
            this.grpOccupationSetting.Controls.Add(this.lblOccupationVersion);
            this.grpOccupationSetting.Controls.Add(this.lblOccupationRefreshDate);
            this.grpOccupationSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOccupationSetting.Location = new System.Drawing.Point(43, 233);
            this.grpOccupationSetting.Name = "grpOccupationSetting";
            this.grpOccupationSetting.Size = new System.Drawing.Size(457, 106);
            this.grpOccupationSetting.TabIndex = 4;
            this.grpOccupationSetting.TabStop = false;
            this.grpOccupationSetting.Text = "Occupation Setting";
            // 
            // txtOccupationVersion
            // 
            this.txtOccupationVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOccupationVersion.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOccupationVersion.Location = new System.Drawing.Point(133, 60);
            this.txtOccupationVersion.MaxLength = 5;
            this.txtOccupationVersion.Name = "txtOccupationVersion";
            this.txtOccupationVersion.Size = new System.Drawing.Size(306, 29);
            this.txtOccupationVersion.TabIndex = 3;
            this.txtOccupationVersion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOccupationVersion_KeyPress);
            // 
            // txtOccupationRefreshDate
            // 
            this.txtOccupationRefreshDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOccupationRefreshDate.Enabled = false;
            this.txtOccupationRefreshDate.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOccupationRefreshDate.Location = new System.Drawing.Point(133, 25);
            this.txtOccupationRefreshDate.Name = "txtOccupationRefreshDate";
            this.txtOccupationRefreshDate.Size = new System.Drawing.Size(306, 29);
            this.txtOccupationRefreshDate.TabIndex = 1;
            // 
            // lblOccupationVersion
            // 
            this.lblOccupationVersion.AutoSize = true;
            this.lblOccupationVersion.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupationVersion.Location = new System.Drawing.Point(7, 65);
            this.lblOccupationVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOccupationVersion.Name = "lblOccupationVersion";
            this.lblOccupationVersion.Size = new System.Drawing.Size(64, 19);
            this.lblOccupationVersion.TabIndex = 2;
            this.lblOccupationVersion.Text = "Version";
            // 
            // lblOccupationRefreshDate
            // 
            this.lblOccupationRefreshDate.AutoSize = true;
            this.lblOccupationRefreshDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupationRefreshDate.Location = new System.Drawing.Point(7, 30);
            this.lblOccupationRefreshDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOccupationRefreshDate.Name = "lblOccupationRefreshDate";
            this.lblOccupationRefreshDate.Size = new System.Drawing.Size(105, 19);
            this.lblOccupationRefreshDate.TabIndex = 0;
            this.lblOccupationRefreshDate.Text = "Refresh Date";
            // 
            // btnUpdateSetting
            // 
            this.btnUpdateSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateSetting.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateSetting.Location = new System.Drawing.Point(508, 410);
            this.btnUpdateSetting.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateSetting.Name = "btnUpdateSetting";
            this.btnUpdateSetting.Size = new System.Drawing.Size(180, 37);
            this.btnUpdateSetting.TabIndex = 6;
            this.btnUpdateSetting.Text = "Update Setting";
            this.btnUpdateSetting.UseVisualStyleBackColor = true;
            this.btnUpdateSetting.Click += new System.EventHandler(this.btnUpdateSetting_Click);
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.ControlBox = false;
            this.Controls.Add(this.btnUpdateSetting);
            this.Controls.Add(this.grpOccupationSetting);
            this.Controls.Add(this.grpGeoSetting);
            this.Controls.Add(this.grpDCSetting);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dtpProvisionalCutOffDOB);
            this.Controls.Add(this.lblProvisionalCutOffDOB);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setting";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Setting_Load);
            this.grpDCSetting.ResumeLayout(false);
            this.grpDCSetting.PerformLayout();
            this.grpGeoSetting.ResumeLayout(false);
            this.grpGeoSetting.PerformLayout();
            this.grpOccupationSetting.ResumeLayout(false);
            this.grpOccupationSetting.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProvisionalCutOffDOB;
        private System.Windows.Forms.DateTimePicker dtpProvisionalCutOffDOB;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpDCSetting;
        private System.Windows.Forms.GroupBox grpGeoSetting;
        private System.Windows.Forms.GroupBox grpOccupationSetting;
        private System.Windows.Forms.Button btnUpdateSetting;
        private System.Windows.Forms.Label lblDCPassword;
        private System.Windows.Forms.TextBox txtDCPassword;
        private System.Windows.Forms.Label lblDCVersion;
        private System.Windows.Forms.Label lblDCDateReset;
        private System.Windows.Forms.TextBox txtDCDateReset;
        private System.Windows.Forms.TextBox txtDCVersion;
        private System.Windows.Forms.TextBox txtGeoVersion;
        private System.Windows.Forms.TextBox txtGeoRefreshDate;
        private System.Windows.Forms.Label lblGeoVersion;
        private System.Windows.Forms.Label lblGeoRefreshDate;
        private System.Windows.Forms.TextBox txtOccupationVersion;
        private System.Windows.Forms.TextBox txtOccupationRefreshDate;
        private System.Windows.Forms.Label lblOccupationVersion;
        private System.Windows.Forms.Label lblOccupationRefreshDate;
    }
}