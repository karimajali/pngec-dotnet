namespace PNGEC.Desktop.Forms
{
    partial class RejectedReason
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
            this.lblRejectedReason = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.bntCancel = new System.Windows.Forms.Button();
            this.txtRejectedReason = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblRejectedReason
            // 
            this.lblRejectedReason.AutoSize = true;
            this.lblRejectedReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRejectedReason.Location = new System.Drawing.Point(12, 9);
            this.lblRejectedReason.Name = "lblRejectedReason";
            this.lblRejectedReason.Size = new System.Drawing.Size(121, 17);
            this.lblRejectedReason.TabIndex = 5;
            this.lblRejectedReason.Text = "Rejected Reason:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.BackColor = System.Drawing.Color.Black;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(140, 140);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 34);
            this.btnSubmit.TabIndex = 7;
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
            this.bntCancel.Location = new System.Drawing.Point(266, 140);
            this.bntCancel.Name = "bntCancel";
            this.bntCancel.Size = new System.Drawing.Size(120, 34);
            this.bntCancel.TabIndex = 8;
            this.bntCancel.Text = "Cancel";
            this.bntCancel.UseVisualStyleBackColor = false;
            this.bntCancel.Click += new System.EventHandler(this.bntCancel_Click);
            // 
            // txtRejectedReason
            // 
            this.txtRejectedReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRejectedReason.Location = new System.Drawing.Point(139, 12);
            this.txtRejectedReason.MaxLength = 255;
            this.txtRejectedReason.Multiline = true;
            this.txtRejectedReason.Name = "txtRejectedReason";
            this.txtRejectedReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRejectedReason.Size = new System.Drawing.Size(270, 120);
            this.txtRejectedReason.TabIndex = 9;
            // 
            // RejectedReason
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 186);
            this.Controls.Add(this.txtRejectedReason);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.bntCancel);
            this.Controls.Add(this.lblRejectedReason);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RejectedReason";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rejected Reason";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRejectedReason;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button bntCancel;
        internal System.Windows.Forms.TextBox txtRejectedReason;
    }
}