namespace PNGEC.Desktop.Report
{
    partial class WardRollBookReport
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
            this.rvWardRollBook = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvWardRollBook
            // 
            this.rvWardRollBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvWardRollBook.LocalReport.ReportEmbeddedResource = "PNGEC.Desktop.Report.WardRollBook.rdlc";
            this.rvWardRollBook.Location = new System.Drawing.Point(0, 0);
            this.rvWardRollBook.Name = "rvWardRollBook";
            this.rvWardRollBook.ServerReport.BearerToken = null;
            this.rvWardRollBook.Size = new System.Drawing.Size(800, 450);
            this.rvWardRollBook.TabIndex = 0;
            // 
            // WardRollBookReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvWardRollBook);
            this.Name = "WardRollBookReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ward Roll Book Report";
            this.Load += new System.EventHandler(this.WardRollBookReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvWardRollBook;
    }
}