namespace PNGEC.Desktop.Forms
{
    partial class TransferVoter
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
            this.cmbWardName = new System.Windows.Forms.ComboBox();
            this.lblWardName = new System.Windows.Forms.Label();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.cmbLLGName = new System.Windows.Forms.ComboBox();
            this.lblLLGName = new System.Windows.Forms.Label();
            this.cmbDistrictName = new System.Windows.Forms.ComboBox();
            this.lblDistrictName = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbProvinceName = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProvinceName = new System.Windows.Forms.Label();
            this.lblRegionName = new System.Windows.Forms.Label();
            this.cmbRegionName = new System.Windows.Forms.ComboBox();
            this.cmbLocationName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbWardName
            // 
            this.cmbWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWardName.FormattingEnabled = true;
            this.cmbWardName.Location = new System.Drawing.Point(263, 249);
            this.cmbWardName.Name = "cmbWardName";
            this.cmbWardName.Size = new System.Drawing.Size(481, 28);
            this.cmbWardName.TabIndex = 9;
            this.cmbWardName.SelectedIndexChanged += new System.EventHandler(this.cmbWardName_SelectedIndexChanged);
            // 
            // lblWardName
            // 
            this.lblWardName.AutoSize = true;
            this.lblWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWardName.Location = new System.Drawing.Point(63, 252);
            this.lblWardName.Name = "lblWardName";
            this.lblWardName.Size = new System.Drawing.Size(93, 20);
            this.lblWardName.TabIndex = 8;
            this.lblWardName.Text = "Ward Name";
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationName.Location = new System.Drawing.Point(63, 310);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(116, 20);
            this.lblLocationName.TabIndex = 10;
            this.lblLocationName.Text = "Location Name";
            // 
            // cmbLLGName
            // 
            this.cmbLLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLLGName.FormattingEnabled = true;
            this.cmbLLGName.Location = new System.Drawing.Point(263, 191);
            this.cmbLLGName.Name = "cmbLLGName";
            this.cmbLLGName.Size = new System.Drawing.Size(481, 28);
            this.cmbLLGName.TabIndex = 7;
            this.cmbLLGName.SelectedIndexChanged += new System.EventHandler(this.cmbLLGName_SelectedIndexChanged);
            // 
            // lblLLGName
            // 
            this.lblLLGName.AutoSize = true;
            this.lblLLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLLGName.Location = new System.Drawing.Point(63, 194);
            this.lblLLGName.Name = "lblLLGName";
            this.lblLLGName.Size = new System.Drawing.Size(86, 20);
            this.lblLLGName.TabIndex = 6;
            this.lblLLGName.Text = "LLG Name";
            // 
            // cmbDistrictName
            // 
            this.cmbDistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrictName.FormattingEnabled = true;
            this.cmbDistrictName.Location = new System.Drawing.Point(263, 133);
            this.cmbDistrictName.Name = "cmbDistrictName";
            this.cmbDistrictName.Size = new System.Drawing.Size(481, 28);
            this.cmbDistrictName.TabIndex = 5;
            this.cmbDistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbDistrictName_SelectedIndexChanged);
            // 
            // lblDistrictName
            // 
            this.lblDistrictName.AutoSize = true;
            this.lblDistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrictName.Location = new System.Drawing.Point(63, 136);
            this.lblDistrictName.Name = "lblDistrictName";
            this.lblDistrictName.Size = new System.Drawing.Size(104, 20);
            this.lblDistrictName.TabIndex = 4;
            this.lblDistrictName.Text = "District Name";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Black;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Arial", 10F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(263, 397);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 50);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbProvinceName
            // 
            this.cmbProvinceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvinceName.FormattingEnabled = true;
            this.cmbProvinceName.Location = new System.Drawing.Point(263, 75);
            this.cmbProvinceName.Name = "cmbProvinceName";
            this.cmbProvinceName.Size = new System.Drawing.Size(481, 28);
            this.cmbProvinceName.TabIndex = 3;
            this.cmbProvinceName.SelectedIndexChanged += new System.EventHandler(this.cmbProvinceName_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(472, 397);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(170, 50);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblProvinceName
            // 
            this.lblProvinceName.AutoSize = true;
            this.lblProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvinceName.Location = new System.Drawing.Point(63, 78);
            this.lblProvinceName.Name = "lblProvinceName";
            this.lblProvinceName.Size = new System.Drawing.Size(115, 20);
            this.lblProvinceName.TabIndex = 2;
            this.lblProvinceName.Text = "Province Name";
            // 
            // lblRegionName
            // 
            this.lblRegionName.AutoSize = true;
            this.lblRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegionName.Location = new System.Drawing.Point(63, 20);
            this.lblRegionName.Name = "lblRegionName";
            this.lblRegionName.Size = new System.Drawing.Size(106, 20);
            this.lblRegionName.TabIndex = 0;
            this.lblRegionName.Text = "Region Name";
            // 
            // cmbRegionName
            // 
            this.cmbRegionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegionName.FormattingEnabled = true;
            this.cmbRegionName.Location = new System.Drawing.Point(263, 17);
            this.cmbRegionName.Name = "cmbRegionName";
            this.cmbRegionName.Size = new System.Drawing.Size(481, 28);
            this.cmbRegionName.TabIndex = 1;
            this.cmbRegionName.SelectedIndexChanged += new System.EventHandler(this.cmbRegionName_SelectedIndexChanged);
            // 
            // cmbLocationName
            // 
            this.cmbLocationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocationName.FormattingEnabled = true;
            this.cmbLocationName.Location = new System.Drawing.Point(263, 302);
            this.cmbLocationName.Name = "cmbLocationName";
            this.cmbLocationName.Size = new System.Drawing.Size(481, 28);
            this.cmbLocationName.TabIndex = 11;
            // 
            // TransferVoter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 541);
            this.Controls.Add(this.cmbLocationName);
            this.Controls.Add(this.cmbWardName);
            this.Controls.Add(this.lblWardName);
            this.Controls.Add(this.lblLocationName);
            this.Controls.Add(this.cmbLLGName);
            this.Controls.Add(this.lblLLGName);
            this.Controls.Add(this.cmbDistrictName);
            this.Controls.Add(this.lblDistrictName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbProvinceName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblProvinceName);
            this.Controls.Add(this.lblRegionName);
            this.Controls.Add(this.cmbRegionName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferVoter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Voter";
            this.Load += new System.EventHandler(this.TransferVoter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWardName;
        private System.Windows.Forms.Label lblWardName;
        private System.Windows.Forms.Label lblLocationName;
        private System.Windows.Forms.ComboBox cmbLLGName;
        private System.Windows.Forms.Label lblLLGName;
        private System.Windows.Forms.ComboBox cmbDistrictName;
        private System.Windows.Forms.Label lblDistrictName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbProvinceName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProvinceName;
        private System.Windows.Forms.Label lblRegionName;
        private System.Windows.Forms.ComboBox cmbRegionName;
        private System.Windows.Forms.ComboBox cmbLocationName;
    }
}