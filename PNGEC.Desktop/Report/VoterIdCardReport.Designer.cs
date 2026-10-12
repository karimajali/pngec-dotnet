namespace PNGEC.Desktop.Report
{
    partial class VoterIdCardReport
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
            this.rvVoterIdCard = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvVoterIdCard
            // 
            this.rvVoterIdCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvVoterIdCard.LocalReport.ReportEmbeddedResource = "PNGEC.Desktop.Report.VoterIdCard.rdlc";
            this.rvVoterIdCard.Location = new System.Drawing.Point(0, 0);
            this.rvVoterIdCard.Name = "rvVoterIdCard";
            this.rvVoterIdCard.ServerReport.BearerToken = null;
            this.rvVoterIdCard.Size = new System.Drawing.Size(800, 450);
            this.rvVoterIdCard.TabIndex = 0;
            // 
            // VoterIdCardReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvVoterIdCard);
            this.Name = "VoterIdCardReport";
            this.Text = "VoterIdCardReport";
            this.Load += new System.EventHandler(this.VoterIdCardReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvVoterIdCard;
    }
}