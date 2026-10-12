namespace PNGEC.Desktop.Forms
{
    partial class MKAClan
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.bntCancel = new System.Windows.Forms.Button();
            this.txtClanName = new System.Windows.Forms.TextBox();
            this.lblMKAClanName = new System.Windows.Forms.Label();
            this.lblClanDescription = new System.Windows.Forms.Label();
            this.txtClanDescription = new System.Windows.Forms.TextBox();
            this.cmbMKADistrictName = new System.Windows.Forms.ComboBox();
            this.cmbMKALLGName = new System.Windows.Forms.ComboBox();
            this.cmbMKAWardName = new System.Windows.Forms.ComboBox();
            this.lblMKADistrictName = new System.Windows.Forms.Label();
            this.lblMKALLGName = new System.Windows.Forms.Label();
            this.lblMKAWardName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.BackColor = System.Drawing.Color.Black;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(208, 358);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(180, 50);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // bntCancel
            // 
            this.bntCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bntCancel.BackColor = System.Drawing.Color.Black;
            this.bntCancel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntCancel.ForeColor = System.Drawing.Color.White;
            this.bntCancel.Location = new System.Drawing.Point(476, 358);
            this.bntCancel.Margin = new System.Windows.Forms.Padding(5);
            this.bntCancel.Name = "bntCancel";
            this.bntCancel.Size = new System.Drawing.Size(180, 50);
            this.bntCancel.TabIndex = 4;
            this.bntCancel.Text = "Cancel";
            this.bntCancel.UseVisualStyleBackColor = false;
            this.bntCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtClanName
            // 
            this.txtClanName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClanName.Location = new System.Drawing.Point(208, 241);
            this.txtClanName.Margin = new System.Windows.Forms.Padding(5);
            this.txtClanName.MaxLength = 50;
            this.txtClanName.Name = "txtClanName";
            this.txtClanName.Size = new System.Drawing.Size(502, 30);
            this.txtClanName.TabIndex = 5;
            // 
            // lblMKAClanName
            // 
            this.lblMKAClanName.AutoSize = true;
            this.lblMKAClanName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKAClanName.Location = new System.Drawing.Point(27, 247);
            this.lblMKAClanName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblMKAClanName.Name = "lblMKAClanName";
            this.lblMKAClanName.Size = new System.Drawing.Size(92, 20);
            this.lblMKAClanName.TabIndex = 6;
            this.lblMKAClanName.Text = "Clan Name";
            // 
            // lblClanDescription
            // 
            this.lblClanDescription.AutoSize = true;
            this.lblClanDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClanDescription.Location = new System.Drawing.Point(27, 309);
            this.lblClanDescription.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblClanDescription.Name = "lblClanDescription";
            this.lblClanDescription.Size = new System.Drawing.Size(134, 20);
            this.lblClanDescription.TabIndex = 7;
            this.lblClanDescription.Text = "Clan Description";
            // 
            // txtClanDescription
            // 
            this.txtClanDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClanDescription.Location = new System.Drawing.Point(208, 303);
            this.txtClanDescription.Margin = new System.Windows.Forms.Padding(5);
            this.txtClanDescription.MaxLength = 50;
            this.txtClanDescription.Name = "txtClanDescription";
            this.txtClanDescription.Size = new System.Drawing.Size(502, 30);
            this.txtClanDescription.TabIndex = 8;
            // 
            // cmbMKADistrictName
            // 
            this.cmbMKADistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKADistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKADistrictName.FormattingEnabled = true;
            this.cmbMKADistrictName.Location = new System.Drawing.Point(208, 54);
            this.cmbMKADistrictName.Name = "cmbMKADistrictName";
            this.cmbMKADistrictName.Size = new System.Drawing.Size(502, 33);
            this.cmbMKADistrictName.TabIndex = 9;
            this.cmbMKADistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbMKADistrictName_SelectedIndexChanged);
            // 
            // cmbMKALLGName
            // 
            this.cmbMKALLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKALLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKALLGName.FormattingEnabled = true;
            this.cmbMKALLGName.Location = new System.Drawing.Point(208, 121);
            this.cmbMKALLGName.Name = "cmbMKALLGName";
            this.cmbMKALLGName.Size = new System.Drawing.Size(502, 33);
            this.cmbMKALLGName.TabIndex = 10;
            this.cmbMKALLGName.SelectedIndexChanged += new System.EventHandler(this.cmbMKALLGName_SelectedIndexChanged);
            // 
            // cmbMKAWardName
            // 
            this.cmbMKAWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKAWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKAWardName.FormattingEnabled = true;
            this.cmbMKAWardName.Location = new System.Drawing.Point(208, 182);
            this.cmbMKAWardName.Name = "cmbMKAWardName";
            this.cmbMKAWardName.Size = new System.Drawing.Size(502, 33);
            this.cmbMKAWardName.TabIndex = 11;
            // 
            // lblMKADistrictName
            // 
            this.lblMKADistrictName.AutoSize = true;
            this.lblMKADistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKADistrictName.Location = new System.Drawing.Point(26, 57);
            this.lblMKADistrictName.Name = "lblMKADistrictName";
            this.lblMKADistrictName.Size = new System.Drawing.Size(154, 20);
            this.lblMKADistrictName.TabIndex = 12;
            this.lblMKADistrictName.Text = "MKA District Name";
            // 
            // lblMKALLGName
            // 
            this.lblMKALLGName.AutoSize = true;
            this.lblMKALLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKALLGName.Location = new System.Drawing.Point(26, 124);
            this.lblMKALLGName.Name = "lblMKALLGName";
            this.lblMKALLGName.Size = new System.Drawing.Size(132, 20);
            this.lblMKALLGName.TabIndex = 13;
            this.lblMKALLGName.Text = "MKA LLG Name";
            // 
            // lblMKAWardName
            // 
            this.lblMKAWardName.AutoSize = true;
            this.lblMKAWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKAWardName.Location = new System.Drawing.Point(26, 182);
            this.lblMKAWardName.Name = "lblMKAWardName";
            this.lblMKAWardName.Size = new System.Drawing.Size(139, 20);
            this.lblMKAWardName.TabIndex = 14;
            this.lblMKAWardName.Text = "MKA Ward Name";
            // 
            // MKAClan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMKAWardName);
            this.Controls.Add(this.lblMKALLGName);
            this.Controls.Add(this.lblMKADistrictName);
            this.Controls.Add(this.cmbMKAWardName);
            this.Controls.Add(this.cmbMKALLGName);
            this.Controls.Add(this.cmbMKADistrictName);
            this.Controls.Add(this.txtClanDescription);
            this.Controls.Add(this.lblClanDescription);
            this.Controls.Add(this.lblMKAClanName);
            this.Controls.Add(this.txtClanName);
            this.Controls.Add(this.bntCancel);
            this.Controls.Add(this.btnSubmit);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKAClan";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MKA Clan";
            this.Load += new System.EventHandler(this.MKAClan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button bntCancel;
        private System.Windows.Forms.TextBox txtClanName;
        private System.Windows.Forms.Label lblMKAClanName;
        private System.Windows.Forms.Label lblClanDescription;
        private System.Windows.Forms.TextBox txtClanDescription;
        private System.Windows.Forms.ComboBox cmbMKADistrictName;
        private System.Windows.Forms.ComboBox cmbMKALLGName;
        private System.Windows.Forms.ComboBox cmbMKAWardName;
        private System.Windows.Forms.Label lblMKADistrictName;
        private System.Windows.Forms.Label lblMKALLGName;
        private System.Windows.Forms.Label lblMKAWardName;
    }
}