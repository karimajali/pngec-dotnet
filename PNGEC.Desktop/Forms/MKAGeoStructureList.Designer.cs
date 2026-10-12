namespace PNGEC.Desktop.Forms
{
    partial class tvMKAGeoStructureList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mkaGeoStructureTree = new System.Windows.Forms.TreeView();
            this.mkaGeoStructureListgrid = new System.Windows.Forms.DataGridView();
            this.pnlGeoButtons = new System.Windows.Forms.Panel();
            this.btnMKASearch = new System.Windows.Forms.Button();
            this.btnMKARefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddMKALocation = new System.Windows.Forms.Button();
            this.btnAddMKAWard = new System.Windows.Forms.Button();
            this.btnAddMKALLG = new System.Windows.Forms.Button();
            this.btnAddMKADistrict = new System.Windows.Forms.Button();
            this.pnlMKAGeoGrid = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mkaGeoStructureListgrid)).BeginInit();
            this.pnlGeoButtons.SuspendLayout();
            this.pnlMKAGeoGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // mkaGeoStructureTree
            // 
            this.mkaGeoStructureTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.mkaGeoStructureTree.Location = new System.Drawing.Point(0, 0);
            this.mkaGeoStructureTree.Name = "mkaGeoStructureTree";
            this.mkaGeoStructureTree.Size = new System.Drawing.Size(355, 814);
            this.mkaGeoStructureTree.TabIndex = 0;
            // 
            // mkaGeoStructureListgrid
            // 
            this.mkaGeoStructureListgrid.AllowUserToAddRows = false;
            this.mkaGeoStructureListgrid.AllowUserToDeleteRows = false;
            this.mkaGeoStructureListgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mkaGeoStructureListgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.mkaGeoStructureListgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mkaGeoStructureListgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mkaGeoStructureListgrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mkaGeoStructureListgrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.mkaGeoStructureListgrid.ColumnHeadersHeight = 28;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mkaGeoStructureListgrid.DefaultCellStyle = dataGridViewCellStyle7;
            this.mkaGeoStructureListgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.mkaGeoStructureListgrid.EnableHeadersVisualStyles = false;
            this.mkaGeoStructureListgrid.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mkaGeoStructureListgrid.Location = new System.Drawing.Point(0, 0);
            this.mkaGeoStructureListgrid.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.mkaGeoStructureListgrid.MultiSelect = false;
            this.mkaGeoStructureListgrid.Name = "mkaGeoStructureListgrid";
            this.mkaGeoStructureListgrid.ReadOnly = true;
            this.mkaGeoStructureListgrid.RowHeadersWidth = 40;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.mkaGeoStructureListgrid.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.mkaGeoStructureListgrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.mkaGeoStructureListgrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.mkaGeoStructureListgrid.RowTemplate.Height = 24;
            this.mkaGeoStructureListgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mkaGeoStructureListgrid.Size = new System.Drawing.Size(1274, 756);
            this.mkaGeoStructureListgrid.TabIndex = 0;
            this.mkaGeoStructureListgrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mkaGeoStructureListgrid_CellContentClick);
            // 
            // pnlGeoButtons
            // 
            this.pnlGeoButtons.Controls.Add(this.btnMKASearch);
            this.pnlGeoButtons.Controls.Add(this.btnMKARefresh);
            this.pnlGeoButtons.Controls.Add(this.txtSearch);
            this.pnlGeoButtons.Controls.Add(this.btnAddMKALocation);
            this.pnlGeoButtons.Controls.Add(this.btnAddMKAWard);
            this.pnlGeoButtons.Controls.Add(this.btnAddMKALLG);
            this.pnlGeoButtons.Controls.Add(this.btnAddMKADistrict);
            this.pnlGeoButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGeoButtons.Location = new System.Drawing.Point(355, 0);
            this.pnlGeoButtons.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGeoButtons.Name = "pnlGeoButtons";
            this.pnlGeoButtons.Size = new System.Drawing.Size(1274, 58);
            this.pnlGeoButtons.TabIndex = 2;
            // 
            // btnMKASearch
            // 
            this.btnMKASearch.BackColor = System.Drawing.Color.Transparent;
            this.btnMKASearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMKASearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnMKASearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMKASearch.Location = new System.Drawing.Point(921, 10);
            this.btnMKASearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnMKASearch.Name = "btnMKASearch";
            this.btnMKASearch.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnMKASearch.Size = new System.Drawing.Size(133, 37);
            this.btnMKASearch.TabIndex = 7;
            this.btnMKASearch.Text = "Search";
            this.btnMKASearch.UseVisualStyleBackColor = false;
            this.btnMKASearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnMKARefresh
            // 
            this.btnMKARefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnMKARefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMKARefresh.Image = global::PNGEC.Desktop.Properties.Resources.refresh;
            this.btnMKARefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMKARefresh.Location = new System.Drawing.Point(1081, 10);
            this.btnMKARefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnMKARefresh.Name = "btnMKARefresh";
            this.btnMKARefresh.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnMKARefresh.Size = new System.Drawing.Size(133, 37);
            this.btnMKARefresh.TabIndex = 8;
            this.btnMKARefresh.Text = "Refresh";
            this.btnMKARefresh.UseVisualStyleBackColor = false;
            this.btnMKARefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(665, 15);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(223, 27);
            this.txtSearch.TabIndex = 6;
            // 
            // btnAddMKALocation
            // 
            this.btnAddMKALocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMKALocation.Location = new System.Drawing.Point(477, 10);
            this.btnAddMKALocation.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddMKALocation.Name = "btnAddMKALocation";
            this.btnAddMKALocation.Size = new System.Drawing.Size(158, 37);
            this.btnAddMKALocation.TabIndex = 5;
            this.btnAddMKALocation.Text = "Add MKA Location";
            this.btnAddMKALocation.UseVisualStyleBackColor = true;
            this.btnAddMKALocation.Click += new System.EventHandler(this.btnAddMKALocation_Click);
            // 
            // btnAddMKAWard
            // 
            this.btnAddMKAWard.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMKAWard.Location = new System.Drawing.Point(318, 10);
            this.btnAddMKAWard.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddMKAWard.Name = "btnAddMKAWard";
            this.btnAddMKAWard.Size = new System.Drawing.Size(135, 37);
            this.btnAddMKAWard.TabIndex = 4;
            this.btnAddMKAWard.Text = "Add MKA Ward";
            this.btnAddMKAWard.UseVisualStyleBackColor = true;
            this.btnAddMKAWard.Click += new System.EventHandler(this.btnAddMKAWard_Click);
            // 
            // btnAddMKALLG
            // 
            this.btnAddMKALLG.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMKALLG.Location = new System.Drawing.Point(165, 10);
            this.btnAddMKALLG.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddMKALLG.Name = "btnAddMKALLG";
            this.btnAddMKALLG.Size = new System.Drawing.Size(132, 37);
            this.btnAddMKALLG.TabIndex = 3;
            this.btnAddMKALLG.Text = "Add MKA LLG";
            this.btnAddMKALLG.UseVisualStyleBackColor = true;
            this.btnAddMKALLG.Click += new System.EventHandler(this.btnAddMKALLG_Click);
            // 
            // btnAddMKADistrict
            // 
            this.btnAddMKADistrict.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddMKADistrict.Location = new System.Drawing.Point(11, 10);
            this.btnAddMKADistrict.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddMKADistrict.Name = "btnAddMKADistrict";
            this.btnAddMKADistrict.Size = new System.Drawing.Size(140, 37);
            this.btnAddMKADistrict.TabIndex = 2;
            this.btnAddMKADistrict.Text = "Add MKA District";
            this.btnAddMKADistrict.UseVisualStyleBackColor = true;
            this.btnAddMKADistrict.Click += new System.EventHandler(this.btnAddMKADistrict_Click);
            // 
            // pnlMKAGeoGrid
            // 
            this.pnlMKAGeoGrid.Controls.Add(this.mkaGeoStructureListgrid);
            this.pnlMKAGeoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMKAGeoGrid.Location = new System.Drawing.Point(355, 58);
            this.pnlMKAGeoGrid.Name = "pnlMKAGeoGrid";
            this.pnlMKAGeoGrid.Size = new System.Drawing.Size(1274, 756);
            this.pnlMKAGeoGrid.TabIndex = 3;
            // 
            // tvMKAGeoStructureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1629, 814);
            this.Controls.Add(this.pnlMKAGeoGrid);
            this.Controls.Add(this.pnlGeoButtons);
            this.Controls.Add(this.mkaGeoStructureTree);
            this.Name = "tvMKAGeoStructureList";
            this.Text = "MKAGeoStructureList";
            this.Load += new System.EventHandler(this.MKAGeoStructureList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mkaGeoStructureListgrid)).EndInit();
            this.pnlGeoButtons.ResumeLayout(false);
            this.pnlGeoButtons.PerformLayout();
            this.pnlMKAGeoGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView mkaGeoStructureTree;
        private System.Windows.Forms.DataGridView mkaGeoStructureListgrid;
        private System.Windows.Forms.Panel pnlGeoButtons;
        private System.Windows.Forms.Button btnMKASearch;
        private System.Windows.Forms.Button btnMKARefresh;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAddMKALocation;
        private System.Windows.Forms.Button btnAddMKAWard;
        private System.Windows.Forms.Button btnAddMKALLG;
        private System.Windows.Forms.Button btnAddMKADistrict;
        private System.Windows.Forms.Panel pnlMKAGeoGrid;
    }
}