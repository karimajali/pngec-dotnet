namespace PNGEC.Desktop.Forms
{
    partial class ImportBioDetailsStatusType
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
            this.lblSelectStatusType = new System.Windows.Forms.Label();
            this.rdbNew = new System.Windows.Forms.RadioButton();
            this.rdbAll = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSelectStatusType
            // 
            this.lblSelectStatusType.AutoSize = true;
            this.lblSelectStatusType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectStatusType.Location = new System.Drawing.Point(27, 50);
            this.lblSelectStatusType.Name = "lblSelectStatusType";
            this.lblSelectStatusType.Size = new System.Drawing.Size(131, 17);
            this.lblSelectStatusType.TabIndex = 6;
            this.lblSelectStatusType.Text = "Select Status Type:";
            // 
            // rdbNew
            // 
            this.rdbNew.AutoSize = true;
            this.rdbNew.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbNew.Location = new System.Drawing.Point(268, 50);
            this.rdbNew.Name = "rdbNew";
            this.rdbNew.Size = new System.Drawing.Size(113, 19);
            this.rdbNew.TabIndex = 8;
            this.rdbNew.Tag = "New";
            this.rdbNew.Text = "New Enrollment";
            this.rdbNew.UseVisualStyleBackColor = true;
            // 
            // rdbAll
            // 
            this.rdbAll.AutoSize = true;
            this.rdbAll.Checked = true;
            this.rdbAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbAll.Location = new System.Drawing.Point(197, 50);
            this.rdbAll.Name = "rdbAll";
            this.rdbAll.Size = new System.Drawing.Size(38, 19);
            this.rdbAll.TabIndex = 7;
            this.rdbAll.TabStop = true;
            this.rdbAll.Tag = "All";
            this.rdbAll.Text = "All";
            this.rdbAll.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.BackColor = System.Drawing.Color.Black;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(131, 127);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 34);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // ImportBioDetailsStatusType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 186);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.rdbNew);
            this.Controls.Add(this.rdbAll);
            this.Controls.Add(this.lblSelectStatusType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportBioDetailsStatusType";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Bio Details Status Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectStatusType;
        public System.Windows.Forms.RadioButton rdbNew;
        internal System.Windows.Forms.RadioButton rdbAll;
        private System.Windows.Forms.Button btnSubmit;
    }
}