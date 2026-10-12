namespace PNGEC.Desktop.Forms
{
    partial class tvGeoStructureList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.geoStructureTree = new System.Windows.Forms.TreeView();
            this.geoStructureListgrid = new System.Windows.Forms.DataGridView();
            this.pnlGeoButtons = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddLocation = new System.Windows.Forms.Button();
            this.btnAddWard = new System.Windows.Forms.Button();
            this.btnAddLLG = new System.Windows.Forms.Button();
            this.btnAddDistrict = new System.Windows.Forms.Button();
            this.btnAddProvince = new System.Windows.Forms.Button();
            this.btnAddRegion = new System.Windows.Forms.Button();
            this.pnlGeoGrid = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.geoStructureListgrid)).BeginInit();
            this.pnlGeoButtons.SuspendLayout();
            this.pnlGeoGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // geoStructureTree
            // 
            this.geoStructureTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.geoStructureTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geoStructureTree.Location = new System.Drawing.Point(0, 0);
            this.geoStructureTree.Name = "geoStructureTree";
            this.geoStructureTree.Size = new System.Drawing.Size(267, 661);
            this.geoStructureTree.TabIndex = 0;
            this.geoStructureTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.geoStructureTree_AfterSelect);
            // 
            // geoStructureListgrid
            // 
            this.geoStructureListgrid.AllowUserToAddRows = false;
            this.geoStructureListgrid.AllowUserToDeleteRows = false;
            this.geoStructureListgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.geoStructureListgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.geoStructureListgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.geoStructureListgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.geoStructureListgrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.geoStructureListgrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.geoStructureListgrid.ColumnHeadersHeight = 28;
            this.geoStructureListgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.geoStructureListgrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.geoStructureListgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.geoStructureListgrid.EnableHeadersVisualStyles = false;
            this.geoStructureListgrid.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.geoStructureListgrid.Location = new System.Drawing.Point(0, 0);
            this.geoStructureListgrid.Margin = new System.Windows.Forms.Padding(10);
            this.geoStructureListgrid.MultiSelect = false;
            this.geoStructureListgrid.Name = "geoStructureListgrid";
            this.geoStructureListgrid.ReadOnly = true;
            this.geoStructureListgrid.RowHeadersWidth = 40;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.geoStructureListgrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.geoStructureListgrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.geoStructureListgrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geoStructureListgrid.RowTemplate.Height = 25;
            this.geoStructureListgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.geoStructureListgrid.Size = new System.Drawing.Size(955, 614);
            this.geoStructureListgrid.TabIndex = 0;
            this.geoStructureListgrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.geoStructureListgrid_CellContentClick);
            this.geoStructureListgrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.geoStructureListgrid_CellValueChanged);
            // 
            // pnlGeoButtons
            // 
            this.pnlGeoButtons.Controls.Add(this.btnSearch);
            this.pnlGeoButtons.Controls.Add(this.btnRefresh);
            this.pnlGeoButtons.Controls.Add(this.txtSearch);
            this.pnlGeoButtons.Controls.Add(this.btnAddLocation);
            this.pnlGeoButtons.Controls.Add(this.btnAddWard);
            this.pnlGeoButtons.Controls.Add(this.btnAddLLG);
            this.pnlGeoButtons.Controls.Add(this.btnAddDistrict);
            this.pnlGeoButtons.Controls.Add(this.btnAddProvince);
            this.pnlGeoButtons.Controls.Add(this.btnAddRegion);
            this.pnlGeoButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGeoButtons.Location = new System.Drawing.Point(267, 0);
            this.pnlGeoButtons.Name = "pnlGeoButtons";
            this.pnlGeoButtons.Size = new System.Drawing.Size(955, 47);
            this.pnlGeoButtons.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(732, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Image = global::PNGEC.Desktop.Properties.Resources.refresh;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefresh.Location = new System.Drawing.Point(838, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(558, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(168, 23);
            this.txtSearch.TabIndex = 6;
            // 
            // btnAddLocation
            // 
            this.btnAddLocation.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLocation.Location = new System.Drawing.Point(466, 8);
            this.btnAddLocation.Name = "btnAddLocation";
            this.btnAddLocation.Size = new System.Drawing.Size(86, 30);
            this.btnAddLocation.TabIndex = 5;
            this.btnAddLocation.Text = "Add Location";
            this.btnAddLocation.UseVisualStyleBackColor = true;
            this.btnAddLocation.Click += new System.EventHandler(this.btnAddLocation_Click);
            // 
            // btnAddWard
            // 
            this.btnAddWard.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddWard.Location = new System.Drawing.Point(374, 8);
            this.btnAddWard.Name = "btnAddWard";
            this.btnAddWard.Size = new System.Drawing.Size(86, 30);
            this.btnAddWard.TabIndex = 4;
            this.btnAddWard.Text = "Add Ward";
            this.btnAddWard.UseVisualStyleBackColor = true;
            this.btnAddWard.Click += new System.EventHandler(this.btnAddWard_Click);
            // 
            // btnAddLLG
            // 
            this.btnAddLLG.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddLLG.Location = new System.Drawing.Point(282, 8);
            this.btnAddLLG.Name = "btnAddLLG";
            this.btnAddLLG.Size = new System.Drawing.Size(86, 30);
            this.btnAddLLG.TabIndex = 3;
            this.btnAddLLG.Text = "Add LLG";
            this.btnAddLLG.UseVisualStyleBackColor = true;
            this.btnAddLLG.Click += new System.EventHandler(this.btnAddLLG_Click);
            // 
            // btnAddDistrict
            // 
            this.btnAddDistrict.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddDistrict.Location = new System.Drawing.Point(190, 8);
            this.btnAddDistrict.Name = "btnAddDistrict";
            this.btnAddDistrict.Size = new System.Drawing.Size(86, 30);
            this.btnAddDistrict.TabIndex = 2;
            this.btnAddDistrict.Text = "Add District";
            this.btnAddDistrict.UseVisualStyleBackColor = true;
            this.btnAddDistrict.Click += new System.EventHandler(this.btnAddDistrict_Click);
            // 
            // btnAddProvince
            // 
            this.btnAddProvince.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProvince.Location = new System.Drawing.Point(98, 8);
            this.btnAddProvince.Name = "btnAddProvince";
            this.btnAddProvince.Size = new System.Drawing.Size(86, 30);
            this.btnAddProvince.TabIndex = 1;
            this.btnAddProvince.Text = "Add Province";
            this.btnAddProvince.UseVisualStyleBackColor = true;
            this.btnAddProvince.Click += new System.EventHandler(this.btnAddProvince_Click);
            // 
            // btnAddRegion
            // 
            this.btnAddRegion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRegion.Location = new System.Drawing.Point(6, 8);
            this.btnAddRegion.Name = "btnAddRegion";
            this.btnAddRegion.Size = new System.Drawing.Size(86, 30);
            this.btnAddRegion.TabIndex = 0;
            this.btnAddRegion.Text = "Add Region";
            this.btnAddRegion.UseVisualStyleBackColor = true;
            this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegion_Click);
            // 
            // pnlGeoGrid
            // 
            this.pnlGeoGrid.Controls.Add(this.geoStructureListgrid);
            this.pnlGeoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeoGrid.Location = new System.Drawing.Point(267, 47);
            this.pnlGeoGrid.Name = "pnlGeoGrid";
            this.pnlGeoGrid.Size = new System.Drawing.Size(955, 614);
            this.pnlGeoGrid.TabIndex = 3;
            // 
            // tvGeoStructureList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 661);
            this.Controls.Add(this.pnlGeoGrid);
            this.Controls.Add(this.pnlGeoButtons);
            this.Controls.Add(this.geoStructureTree);
            this.Name = "tvGeoStructureList";
            this.Text = "GeoStructureList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tvGeoStructureList_FormClosing);
            this.Load += new System.EventHandler(this.GeoStructureList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.geoStructureListgrid)).EndInit();
            this.pnlGeoButtons.ResumeLayout(false);
            this.pnlGeoButtons.PerformLayout();
            this.pnlGeoGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView geoStructureTree;
        private System.Windows.Forms.DataGridView geoStructureListgrid;
        private System.Windows.Forms.Panel pnlGeoButtons;
        private System.Windows.Forms.Button btnAddLocation;
        private System.Windows.Forms.Button btnAddWard;
        private System.Windows.Forms.Button btnAddLLG;
        private System.Windows.Forms.Button btnAddDistrict;
        private System.Windows.Forms.Button btnAddProvince;
        private System.Windows.Forms.Button btnAddRegion;
        private System.Windows.Forms.Panel pnlGeoGrid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
    }
}