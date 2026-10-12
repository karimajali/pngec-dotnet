namespace PNGEC.Desktop.Forms
{
    partial class ProductionToTraining
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblSelectedDatabase = new System.Windows.Forms.Label();
            this.cmbDatabaseOperations = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.tmrProductionToTraining = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(533, 167);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(195, 50);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Black;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.Font = new System.Drawing.Font("Arial", 10F);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(335, 167);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(185, 50);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblSelectedDatabase
            // 
            this.lblSelectedDatabase.AutoSize = true;
            this.lblSelectedDatabase.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDatabase.Location = new System.Drawing.Point(40, 26);
            this.lblSelectedDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedDatabase.Name = "lblSelectedDatabase";
            this.lblSelectedDatabase.Size = new System.Drawing.Size(277, 23);
            this.lblSelectedDatabase.TabIndex = 16;
            this.lblSelectedDatabase.Text = "Selected Database Operation:";
            // 
            // cmbDatabaseOperations
            // 
            this.cmbDatabaseOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabaseOperations.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDatabaseOperations.FormattingEnabled = true;
            this.cmbDatabaseOperations.Location = new System.Drawing.Point(319, 26);
            this.cmbDatabaseOperations.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDatabaseOperations.Name = "cmbDatabaseOperations";
            this.cmbDatabaseOperations.Size = new System.Drawing.Size(409, 29);
            this.cmbDatabaseOperations.TabIndex = 17;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(44, 117);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(684, 23);
            this.progressBar1.TabIndex = 20;
           
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMessage.Location = new System.Drawing.Point(41, 86);
            this.lblStatusMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(11, 16);
            this.lblStatusMessage.TabIndex = 21;
            this.lblStatusMessage.Text = ":";
            // 
            // tmrProductionToTraining
            // 
            this.tmrProductionToTraining.Interval = 1000;
            this.tmrProductionToTraining.Tick += new System.EventHandler(this.TmrProductionToTraining_Tick);
            // 
            // ProductionToTraining
            // 
            this.ClientSize = new System.Drawing.Size(744, 232);
            this.Controls.Add(this.lblStatusMessage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblSelectedDatabase);
            this.Controls.Add(this.cmbDatabaseOperations);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductionToTraining";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Move Production Data to Training ";
            this.Load += new System.EventHandler(this.ProductionToTraining_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }       

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblSelectedDatabase;
        private System.Windows.Forms.ComboBox cmbDatabaseOperations;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.Timer tmrProductionToTraining;
    }
}