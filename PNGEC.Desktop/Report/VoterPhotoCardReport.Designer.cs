namespace PNGEC.Desktop.Report
{
    partial class VoterPhotoCardReport
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
            this.rvVoterPhotoCard = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvVoterPhotoCard
            // 
            this.rvVoterPhotoCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvVoterPhotoCard.LocalReport.ReportEmbeddedResource = "PNGEC.Desktop.Report.VoterPhotoCard.rdlc";
            this.rvVoterPhotoCard.Location = new System.Drawing.Point(0, 0);
            this.rvVoterPhotoCard.Name = "rvVoterPhotoCard";
            this.rvVoterPhotoCard.ServerReport.BearerToken = null;
            this.rvVoterPhotoCard.Size = new System.Drawing.Size(800, 450);
            this.rvVoterPhotoCard.TabIndex = 0;
            // 
            // VoterPhotoCardReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvVoterPhotoCard);
            this.Name = "VoterPhotoCardReport";
            this.Text = "VoterPhotoCardReport";
            this.Load += new System.EventHandler(this.VoterPhotoCardReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvVoterPhotoCard;
    }
}