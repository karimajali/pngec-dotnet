namespace PNGEC.Desktop.Forms
{
    partial class tvGeoClanList
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
            this.geoClanListgrid = new System.Windows.Forms.DataGridView();
            this.pnlGeoButtons = new System.Windows.Forms.Panel();
            this.btnExportToCSV = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAddClan = new System.Windows.Forms.Button();
            this.pnlGeoGrid = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.geoClanListgrid)).BeginInit();
            this.pnlGeoButtons.SuspendLayout();
            this.pnlGeoGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // geoStructureTree
            // 
            this.geoStructureTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.geoStructureTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geoStructureTree.Location = new System.Drawing.Point(0, 0);
            this.geoStructureTree.Margin = new System.Windows.Forms.Padding(4);
            this.geoStructureTree.Name = "geoStructureTree";
            this.geoStructureTree.Size = new System.Drawing.Size(355, 814);
            this.geoStructureTree.TabIndex = 0;
            this.geoStructureTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.geoStructureTree_AfterSelect);
            // 
            // geoClanListgrid
            // 
            this.geoClanListgrid.AllowUserToAddRows = false;
            this.geoClanListgrid.AllowUserToDeleteRows = false;
            this.geoClanListgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.geoClanListgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.geoClanListgrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.geoClanListgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.geoClanListgrid.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.geoClanListgrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.geoClanListgrid.ColumnHeadersHeight = 28;
            this.geoClanListgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.geoClanListgrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.geoClanListgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.geoClanListgrid.EnableHeadersVisualStyles = false;
            this.geoClanListgrid.GridColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.geoClanListgrid.Location = new System.Drawing.Point(0, 0);
            this.geoClanListgrid.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.geoClanListgrid.MultiSelect = false;
            this.geoClanListgrid.Name = "geoClanListgrid";
            this.geoClanListgrid.ReadOnly = true;
            this.geoClanListgrid.RowHeadersWidth = 40;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.geoClanListgrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.geoClanListgrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.geoClanListgrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geoClanListgrid.RowTemplate.Height = 25;
            this.geoClanListgrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.geoClanListgrid.Size = new System.Drawing.Size(1274, 756);
            this.geoClanListgrid.TabIndex = 0;
            this.geoClanListgrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.geoStructureListgrid_CellContentClick);
            this.geoClanListgrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.geoStructureListgrid_CellValueChanged);
            // 
            // pnlGeoButtons
            // 
            this.pnlGeoButtons.Controls.Add(this.btnExportToCSV);
            this.pnlGeoButtons.Controls.Add(this.btnSearch);
            this.pnlGeoButtons.Controls.Add(this.btnRefresh);
            this.pnlGeoButtons.Controls.Add(this.txtSearch);
            this.pnlGeoButtons.Controls.Add(this.btnAddClan);
            this.pnlGeoButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGeoButtons.Location = new System.Drawing.Point(355, 0);
            this.pnlGeoButtons.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGeoButtons.Name = "pnlGeoButtons";
            this.pnlGeoButtons.Size = new System.Drawing.Size(1274, 58);
            this.pnlGeoButtons.TabIndex = 1;
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportToCSV.Location = new System.Drawing.Point(1101, 10);
            this.btnExportToCSV.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(140, 37);
            this.btnExportToCSV.TabIndex = 15;
            this.btnExportToCSV.Text = "Export to CSV";
            this.btnExportToCSV.UseVisualStyleBackColor = true;
            this.btnExportToCSV.Visible = false;
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Image = global::PNGEC.Desktop.Properties.Resources.search_image;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(819, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnSearch.Size = new System.Drawing.Size(133, 37);
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
            this.btnRefresh.Location = new System.Drawing.Point(960, 10);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnRefresh.Size = new System.Drawing.Size(133, 37);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(587, 15);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(223, 27);
            this.txtSearch.TabIndex = 6;
            // 
            // btnAddClan
            // 
            this.btnAddClan.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddClan.Location = new System.Drawing.Point(12, 10);
            this.btnAddClan.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddClan.Name = "btnAddClan";
            this.btnAddClan.Size = new System.Drawing.Size(115, 37);
            this.btnAddClan.TabIndex = 5;
            this.btnAddClan.Text = "Add Clan";
            this.btnAddClan.UseVisualStyleBackColor = true;
            this.btnAddClan.Click += new System.EventHandler(this.btnAddClan_Click);
            // 
            // pnlGeoGrid
            // 
            this.pnlGeoGrid.Controls.Add(this.geoClanListgrid);
            this.pnlGeoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeoGrid.Location = new System.Drawing.Point(355, 58);
            this.pnlGeoGrid.Margin = new System.Windows.Forms.Padding(4);
            this.pnlGeoGrid.Name = "pnlGeoGrid";
            this.pnlGeoGrid.Size = new System.Drawing.Size(1274, 756);
            this.pnlGeoGrid.TabIndex = 3;
            // 
            // tvGeoClanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1629, 814);
            this.Controls.Add(this.pnlGeoGrid);
            this.Controls.Add(this.pnlGeoButtons);
            this.Controls.Add(this.geoStructureTree);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "tvGeoClanList";
            this.Text = "GeoClanList";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.tvGeoClanList_FormClosing);
            this.Load += new System.EventHandler(this.tvGeoClanList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.geoClanListgrid)).EndInit();
            this.pnlGeoButtons.ResumeLayout(false);
            this.pnlGeoButtons.PerformLayout();
            this.pnlGeoGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView geoStructureTree;
        private System.Windows.Forms.DataGridView geoClanListgrid;
        private System.Windows.Forms.Panel pnlGeoButtons;
        private System.Windows.Forms.Button btnAddClan;
        private System.Windows.Forms.Panel pnlGeoGrid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnExportToCSV;
    }
}