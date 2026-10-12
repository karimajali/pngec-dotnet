namespace PNGEC.Desktop.Report
{
    partial class TransferInVoterReport
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rvTransferInVoter = new Microsoft.Reporting.WinForms.ReportViewer();
            this.TransferInVoterDataSet = new PNGEC.Desktop.Report.TransferInVoterDataSet();
            this.tblTransferInVoterBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TransferInVoterDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTransferInVoterBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rvTransferInVoter
            // 
            this.rvTransferInVoter.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ds_TransferInVoter";
            reportDataSource1.Value = this.tblTransferInVoterBindingSource;
            this.rvTransferInVoter.LocalReport.DataSources.Add(reportDataSource1);
            this.rvTransferInVoter.LocalReport.ReportEmbeddedResource = "PNGEC.Desktop.Report.TransferInVoter.rdlc";
            this.rvTransferInVoter.Location = new System.Drawing.Point(0, 0);
            this.rvTransferInVoter.Name = "rvTransferInVoter";
            this.rvTransferInVoter.ServerReport.BearerToken = null;
            this.rvTransferInVoter.Size = new System.Drawing.Size(800, 450);
            this.rvTransferInVoter.TabIndex = 0;
            // 
            // TransferInVoterDataSet
            // 
            this.TransferInVoterDataSet.DataSetName = "TransferInVoterDataSet";
            this.TransferInVoterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblTransferInVoterBindingSource
            // 
            this.tblTransferInVoterBindingSource.DataMember = "tblTransferInVoter";
            this.tblTransferInVoterBindingSource.DataSource = this.TransferInVoterDataSet;
            // 
            // TransferInVoterReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvTransferInVoter);
            this.Name = "TransferInVoterReport";
            this.Text = "TransferInVoterReport";
            this.Load += new System.EventHandler(this.TransferInVoterReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TransferInVoterDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblTransferInVoterBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvTransferInVoter;
        private System.Windows.Forms.BindingSource tblTransferInVoterBindingSource;
        private TransferInVoterDataSet TransferInVoterDataSet;
    }
}