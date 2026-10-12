namespace PNGEC.Desktop.Forms
{
    partial class DownloadProvincialFile
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgDownloadProvincialFile = new System.Windows.Forms.DataGridView();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnUncheckedAll = new System.Windows.Forms.Button();
            this.btnCheckedAll = new System.Windows.Forms.Button();
            this.btnDownloadAll = new System.Windows.Forms.Button();
            this.tmrDownLoadStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.pnlMain.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDownloadProvincialFile)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlGrid);
            this.pnlMain.Controls.Add(this.pnlFooter);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1079, 721);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlGrid
            // 
            this.pnlGrid.AutoScroll = true;
            this.pnlGrid.Controls.Add(this.dgDownloadProvincialFile);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(1079, 672);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgDownloadProvincialFile
            // 
            this.dgDownloadProvincialFile.AllowUserToAddRows = false;
            this.dgDownloadProvincialFile.AllowUserToDeleteRows = false;
            this.dgDownloadProvincialFile.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDownloadProvincialFile.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDownloadProvincialFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDownloadProvincialFile.BackgroundColor = System.Drawing.Color.White;
            this.dgDownloadProvincialFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDownloadProvincialFile.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDownloadProvincialFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDownloadProvincialFile.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDownloadProvincialFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDownloadProvincialFile.EnableHeadersVisualStyles = false;
            this.dgDownloadProvincialFile.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgDownloadProvincialFile.Location = new System.Drawing.Point(0, 0);
            this.dgDownloadProvincialFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgDownloadProvincialFile.MultiSelect = false;
            this.dgDownloadProvincialFile.Name = "dgDownloadProvincialFile";
            this.dgDownloadProvincialFile.ReadOnly = true;
            this.dgDownloadProvincialFile.RowHeadersWidth = 51;
            this.dgDownloadProvincialFile.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgDownloadProvincialFile.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDownloadProvincialFile.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgDownloadProvincialFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDownloadProvincialFile.Size = new System.Drawing.Size(1079, 672);
            this.dgDownloadProvincialFile.TabIndex = 2;
            this.dgDownloadProvincialFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDownloadProvincialFile_CellClick);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnUncheckedAll);
            this.pnlFooter.Controls.Add(this.btnCheckedAll);
            this.pnlFooter.Controls.Add(this.btnDownloadAll);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 672);
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1079, 49);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnUncheckedAll
            // 
            this.btnUncheckedAll.Location = new System.Drawing.Point(179, 6);
            this.btnUncheckedAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUncheckedAll.Name = "btnUncheckedAll";
            this.btnUncheckedAll.Size = new System.Drawing.Size(143, 37);
            this.btnUncheckedAll.TabIndex = 2;
            this.btnUncheckedAll.Text = "Uncheck All";
            this.btnUncheckedAll.UseVisualStyleBackColor = true;
            this.btnUncheckedAll.Click += new System.EventHandler(this.btnUncheckedAll_Click);
            // 
            // btnCheckedAll
            // 
            this.btnCheckedAll.Location = new System.Drawing.Point(28, 6);
            this.btnCheckedAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCheckedAll.Name = "btnCheckedAll";
            this.btnCheckedAll.Size = new System.Drawing.Size(143, 37);
            this.btnCheckedAll.TabIndex = 1;
            this.btnCheckedAll.Text = "Check All";
            this.btnCheckedAll.UseVisualStyleBackColor = true;
            this.btnCheckedAll.Click += new System.EventHandler(this.btnCheckedAll_Click);
            // 
            // btnDownloadAll
            // 
            this.btnDownloadAll.Location = new System.Drawing.Point(908, 6);
            this.btnDownloadAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDownloadAll.Name = "btnDownloadAll";
            this.btnDownloadAll.Size = new System.Drawing.Size(143, 37);
            this.btnDownloadAll.TabIndex = 0;
            this.btnDownloadAll.Text = "Download";
            this.btnDownloadAll.UseVisualStyleBackColor = true;
            this.btnDownloadAll.Click += new System.EventHandler(this.btnDownloadAll_Click);
            // 
            // tmrDownLoadStatusUpdate
            // 
            this.tmrDownLoadStatusUpdate.Interval = 1000;
            this.tmrDownLoadStatusUpdate.Tick += new System.EventHandler(this.tmrDownLoadStatusUpdate_Tick);
            // 
            // DownloadProvincialFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 721);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadProvincialFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download Provincial File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadProvincialFile_FormClosing);
            this.Load += new System.EventHandler(this.DownloadProvincialFile_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDownloadProvincialFile)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.DataGridView dgDownloadProvincialFile;
        private System.Windows.Forms.Button btnDownloadAll;
        private System.Windows.Forms.Timer tmrDownLoadStatusUpdate;
        private System.Windows.Forms.Button btnUncheckedAll;
        private System.Windows.Forms.Button btnCheckedAll;
    }
}