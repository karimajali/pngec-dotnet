using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PNGEC.API.Domain.District;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Location;
using PNGEC.API.Domain.Province;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.Desktop.Helper;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Forms
{
    public partial class tvGeoStructureList : Form
    {
        #region Variables

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private GeoTreeStructure TreeStructure = null;

        /// <summary>
        /// The location Model
        /// </summary>
        //private LocationModel locationModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="tvGeoStructureList"/> class.
        /// </summary>
        public tvGeoStructureList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// //Main Geo List Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeoStructureList_Load(object sender, EventArgs e)
        {
            geoStructureTree.Visible = false;
            //Task.Run(() => InitTreeData());

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            if (GlobalsModel.TreeViewStructure != null)
            {
                LoadGridViewSetting();
            }
        }

        /// <summary>
        /// Handles the AfterSelect event of the TreeViewSelectedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void TreeViewSelectedChanged_AfterSelect(object sender, EventArgs e)
        {
            LoadGridViewSetting();
        }

        /// <summary>
        /// Loads the grid view setting.
        /// </summary>
        private void LoadGridViewSetting()
        {
            TreeStructure = GlobalsModel.TreeViewStructure;
            GridViewSetting(TreeStructure);
        }

        /// <summary>
        ///Initialize Tree Data
        /// </summary>
        private void InitTreeData()
        {
            var loc = Globals.GeoList.LocationList.Select(l => new GeoTreeStructure()
            {
                Id = l.LocationId,
                Name = l.LocationName,
                MainTagName = "Location",
                ParentId = l.WardId,
                Children = null
            });
            var ward = Globals.GeoList.WardList.Select(w => new GeoTreeStructure()
            {
                Id = w.WardId,
                Name = w.WardName,
                MainTagName = "Ward",
                ParentId = w.LLGId,
                Children = loc.Where(l => l.ParentId == w.WardId)
            });
            var llg = Globals.GeoList.LLGList.Select(w => new GeoTreeStructure()
            {
                Id = w.LLGId,
                Name = w.LLGName,
                MainTagName = "LLG",
                ParentId = w.DistrictId,
                Children = ward.Where(l => l.ParentId == w.LLGId)
            });
            var dis = Globals.GeoList.DistrictList.Select(w => new GeoTreeStructure()
            {
                Id = w.DistrictId,
                Name = w.DistrictName,
                MainTagName = "District",
                ParentId = w.ProvinceId,
                Children = llg.Where(l => l.ParentId == w.DistrictId)
            });
            var pro = Globals.GeoList.ProvinceList.Select(w => new GeoTreeStructure()
            {
                Id = w.ProvinceId,
                Name = w.ProvinceName,
                MainTagName = "Province",
                ParentId = w.RegionId,
                Children = dis.Where(l => l.ParentId == w.ProvinceId)
            });
            var reg = Globals.GeoList.RegionList.Select(w => new GeoTreeStructure()
            {
                Id = w.RegionId,
                Name = w.RegionName,
                MainTagName = "Region",
                ParentId = default(int),
                Children = pro.Where(l => l.ParentId == w.RegionId)
            });
            InitializeActualTreeLogic(reg);
        }

        /// <summary>
        /// recusive logic to set data in tree node.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentNode"></param>
        private void PopulateTreeView(IEnumerable<GeoTreeStructure> data, TreeNode parentNode)
        {
            TreeNode childNode;
            foreach (GeoTreeStructure row in data)
            {
                TreeNode t = new TreeNode();
                t.Text = row.Name.ToString();
                t.Tag = row;
                t.Name = row.MainTagName;
                t.ImageKey = Convert.ToString(row.Id);
                if (parentNode == null)
                {
                    geoStructureTree.Nodes.Add(t);
                    childNode = t;
                }
                else
                {
                    parentNode.Nodes.Add(t);
                    childNode = t;
                }
                if (row.Children != null)
                    PopulateTreeView(row.Children, childNode);
            }
        }

        /// <summary>
        ///Tree Logic Init 
        /// </summary>
        /// <param name="region"></param>
        private async void InitializeActualTreeLogic(IEnumerable<GeoTreeStructure> region)
        {
            //this.Cursor = Cursors.WaitCursor;
            GeoTreeStructure geoTreeStructure = new GeoTreeStructure();
            geoTreeStructure.Id = 0;
            geoTreeStructure.MainTagName = "Geo";
            //TreeNode t = new TreeNode();
            //t.Text = "PNGEC";
            //t.Tag = region;
            //t.Name = "Region";
            //t.ImageKey = "PNGEC";
            //t.Tag = geoTreeStructure;
            //geoStructureTree.Nodes.Add(t);

            //PopulateTreeView(region, t);
            //geoStructureTree.SelectedNode = t;
            await Task.Delay(50);

            geoStructureTree.Invoke((Action)delegate ()
            {
                this.Cursor = Cursors.WaitCursor;
                this.pnlGeoButtons.Enabled = false;
                TreeNode t = new TreeNode();
                t.Text = "PNGEC";
                t.Tag = region;
                t.Name = "Region";
                t.ImageKey = "PNGEC";
                t.Tag = geoTreeStructure;
                geoStructureTree.BeginUpdate(); // No visual updates until we say 
                geoStructureTree.Nodes.Clear(); // Remove existing nodes
                geoStructureTree.Nodes.Add(t);
                PopulateTreeView(region, t);
                geoStructureTree.SelectedNode = t;
                geoStructureTree.SelectedNode.Expand();
                geoStructureTree.EndUpdate(); // Allow the treeview to update visually
                this.Cursor = Cursors.Default;
                this.pnlGeoButtons.Enabled = true;
            });


        }

        /// <summary>
        /// //Grid Configurations..
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchText"></param>
        /// <param name="isFromSearch"></param>
        private void GridViewSetting(GeoTreeStructure data, string searchText = null, bool isFromSearch = false)
        {
            if (geoStructureListgrid.Columns.Count > 0)
            {
                geoStructureListgrid.Columns.Clear();
            }

            ////geoStructureListgrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Gray;
            ////using (Font font = new Font(geoStructureListgrid.DefaultCellStyle.Font.FontFamily, 7, FontStyle.Bold))
            ////{
            ////    geoStructureListgrid.ColumnHeadersDefaultCellStyle.Font = font;
            ////}
            ////geoStructureListgrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////geoStructureListgrid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////geoStructureListgrid.EnableHeadersVisualStyles = false;
            if (data.MainTagName == "Geo")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoStructureListgrid.DataSource = Globals.GeoList.RegionList.Where(f =>
                    f.RegionName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    geoStructureListgrid.DataSource = Globals.GeoList.RegionList.ToList();
                }
                geoStructureListgrid.Refresh();
                //geoStructureListgrid.Columns["RegionId"].Visible = false;
                //geoStructureListgrid.Columns["Status"].Visible = false;
                //geoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //geoStructureListgrid.Columns["RegionName"].Visible = true;

                //geoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //geoStructureListgrid.Columns["ModifiedDate"].Visible = false;
                //geoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //geoStructureListgrid.Columns["Comment"].Visible = false;
                //geoStructureListgrid.Columns["ActionBy"].Visible = false;

                geoStructureListgrid.Columns["RegionName"].HeaderText = "Region Name";
                geoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                geoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoStructureListgrid.Columns["RegionName"].Visible = true;
                geoStructureListgrid.Columns["StatusName"].Visible = true;
                geoStructureListgrid.Columns["LastChangeDate"].Visible = true;
            }
            else if (data.MainTagName == "Region")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoStructureListgrid.DataSource = Globals.GeoList.ProvinceList.Where(f =>
                    f.RegionId == data.Id &&
                    f.ProvinceName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    geoStructureListgrid.DataSource = Globals.GeoList.ProvinceList.Where(p => p.RegionId == data.Id).ToList();
                }
                geoStructureListgrid.Refresh();
                //geoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //geoStructureListgrid.Columns["Status"].Visible = false;
                //geoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //geoStructureListgrid.Columns["RegionName"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceName"].Visible = true;
                //geoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //geoStructureListgrid.Columns["ModifiedDate"].Visible = false;
                //geoStructureListgrid.Columns["RegionId"].Visible = false;

                //geoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //geoStructureListgrid.Columns["Comment"].Visible = false;
                //geoStructureListgrid.Columns["ActionBy"].Visible = false;

                geoStructureListgrid.Columns["ProvinceName"].HeaderText = "Province Name";
                geoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                geoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoStructureListgrid.Columns["ProvinceName"].Visible = true;
                geoStructureListgrid.Columns["StatusName"].Visible = true;
                geoStructureListgrid.Columns["LastChangeDate"].Visible = true;
            }
            else if (data.MainTagName == "Province")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoStructureListgrid.DataSource = Globals.GeoList.DistrictList.Where(f =>
                    f.ProvinceId == data.Id &&
                    f.DistrictName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.GeoList.DistrictList.Where(p => p.ProvinceId == data.Id).ToList();
                    geoStructureListgrid.DataSource = list;
                }

                geoStructureListgrid.Refresh();
                //geoStructureListgrid.Columns["DistrictId"].Visible = false;
                //geoStructureListgrid.Columns["Status"].Visible = false;
                //geoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //geoStructureListgrid.Columns["DistrictName"].Visible = true;
                //geoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //geoStructureListgrid.Columns["ModifiedDate"].Visible = false;
                //geoStructureListgrid.Columns["BatchID"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //geoStructureListgrid.Columns["RegionId"].Visible = false;

                //geoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //geoStructureListgrid.Columns["Comment"].Visible = false;
                //geoStructureListgrid.Columns["RegionName"].Visible = false;
                //geoStructureListgrid.Columns["ActionBy"].Visible = false;

                geoStructureListgrid.Columns["DistrictName"].HeaderText = "District Name";
                geoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                geoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoStructureListgrid.Columns["DistrictName"].Visible = true;
                geoStructureListgrid.Columns["StatusName"].Visible = true;
                geoStructureListgrid.Columns["LastChangeDate"].Visible = true;
            }
            else if (data.MainTagName == "District")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoStructureListgrid.DataSource = Globals.GeoList.LLGList.Where(f =>
                    f.DistrictId == data.Id &&
                    f.LLGName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.GeoList.LLGList.Where(p => p.DistrictId == data.Id).ToList();
                    geoStructureListgrid.DataSource = list;
                }

                geoStructureListgrid.Refresh();
                //geoStructureListgrid.Columns["LLGId"].Visible = false;
                //geoStructureListgrid.Columns["Status"].Visible = false;
                //geoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //geoStructureListgrid.Columns["DistrictName"].Visible = false;
                //geoStructureListgrid.Columns["LLGName"].Visible = true;
                //geoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //geoStructureListgrid.Columns["ModifiedDate"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //geoStructureListgrid.Columns["RegionId"].Visible = false;
                //geoStructureListgrid.Columns["DistrictId"].Visible = false;

                //geoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //geoStructureListgrid.Columns["Comment"].Visible = false;
                //geoStructureListgrid.Columns["RegionName"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //geoStructureListgrid.Columns["ActionBy"].Visible = false;

                geoStructureListgrid.Columns["LLGName"].HeaderText = "LLG Name";
                geoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                geoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoStructureListgrid.Columns["LLGName"].Visible = true;
                geoStructureListgrid.Columns["StatusName"].Visible = true;
                geoStructureListgrid.Columns["LastChangeDate"].Visible = true;

                DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                transferButton.FlatStyle = FlatStyle.Standard;
                transferButton.HeaderText = "Action";
                transferButton.Name = "Transfer";
                transferButton.Text = "Transfer";
                transferButton.UseColumnTextForButtonValue = true;
                transferButton.Width = 100;
                if (geoStructureListgrid.Columns["Transfer"] == null)
                {
                    geoStructureListgrid.Columns.Insert(geoStructureListgrid.Columns.Count, transferButton);
                }
            }
            else if (data.MainTagName == "LLG")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoStructureListgrid.DataSource = Globals.GeoList.WardList.Where(f =>
                    f.LLGId == data.Id &&
                    f.WardName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.GeoList.WardList.Where(p => p.LLGId == data.Id).ToList();
                    geoStructureListgrid.DataSource = list;
                }

                geoStructureListgrid.Refresh();
                //geoStructureListgrid.Columns["WardId"].Visible = false;
                //geoStructureListgrid.Columns["Status"].Visible = false;
                //geoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //geoStructureListgrid.Columns["LLGName"].Visible = false;
                //geoStructureListgrid.Columns["WardName"].Visible = true;
                //geoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //geoStructureListgrid.Columns["ModifiedDate"].Visible = false;

                //geoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //geoStructureListgrid.Columns["RegionId"].Visible = false;
                //geoStructureListgrid.Columns["DistrictId"].Visible = false;
                //geoStructureListgrid.Columns["LLGId"].Visible = false;

                //geoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //geoStructureListgrid.Columns["Comment"].Visible = false;
                //geoStructureListgrid.Columns["RegionName"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //geoStructureListgrid.Columns["DistrictName"].Visible = false;
                //geoStructureListgrid.Columns["WardNoWardName"].Visible = false;
                //geoStructureListgrid.Columns["ActionBy"].Visible = false;

                geoStructureListgrid.Columns["WardName"].HeaderText = "Ward Name";
                geoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                geoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoStructureListgrid.Columns["WardName"].Visible = true;
                geoStructureListgrid.Columns["StatusName"].Visible = true;
                geoStructureListgrid.Columns["LastChangeDate"].Visible = true;

                DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                transferButton.FlatStyle = FlatStyle.Standard;
                transferButton.HeaderText = "Action";
                transferButton.Name = "Transfer";
                transferButton.Text = "Transfer";
                transferButton.UseColumnTextForButtonValue = true;
                transferButton.Width = 100;
                if (geoStructureListgrid.Columns["Transfer"] == null)
                {
                    geoStructureListgrid.Columns.Insert(geoStructureListgrid.Columns.Count, transferButton);
                }
            }
            else if (data.MainTagName == "Ward")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoStructureListgrid.DataSource = Globals.GeoList.LocationList.Where(f =>
                    f.WardId == data.Id &&
                    f.LocationName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.GeoList.LocationList.Where(p => p.WardId == data.Id).ToList();
                    geoStructureListgrid.DataSource = list;
                }
                geoStructureListgrid.Refresh();
                //geoStructureListgrid.Columns["LocationId"].Visible = false;
                //geoStructureListgrid.Columns["Status"].Visible = false;
                //geoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //geoStructureListgrid.Columns["WardName"].Visible = false;
                //geoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //geoStructureListgrid.Columns["ModifiedDate"].Visible = false;

                //geoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //geoStructureListgrid.Columns["RegionId"].Visible = false;
                //geoStructureListgrid.Columns["DistrictId"].Visible = false;
                //geoStructureListgrid.Columns["LLGId"].Visible = false;
                //geoStructureListgrid.Columns["WardId"].Visible = false;

                //geoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //geoStructureListgrid.Columns["Comment"].Visible = false;
                //geoStructureListgrid.Columns["LLGName"].Visible = false;
                //geoStructureListgrid.Columns["RegionName"].Visible = false;
                //geoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //geoStructureListgrid.Columns["DistrictName"].Visible = false;
                //geoStructureListgrid.Columns["ActionBy"].Visible = false;

                geoStructureListgrid.Columns["LocationName"].HeaderText = "Location Name";
                geoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                geoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoStructureListgrid.Columns["LocationName"].Visible = true;
                geoStructureListgrid.Columns["StatusName"].Visible = true;
                geoStructureListgrid.Columns["LastChangeDate"].Visible = true;

                DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                transferButton.FlatStyle = FlatStyle.Standard;
                transferButton.HeaderText = "Action";
                transferButton.Name = "Transfer";
                transferButton.Text = "Transfer";
                transferButton.UseColumnTextForButtonValue = true;
                transferButton.Width = 100;
                if (geoStructureListgrid.Columns["Transfer"] == null)
                {
                    geoStructureListgrid.Columns.Insert(geoStructureListgrid.Columns.Count, transferButton);
                }
            }
            else
            {

            }

            if (data.MainTagName != "Location")
            {
                DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
                editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //editButton.CellTemplate = new DataGridViewButtonCell();
                editButton.FlatStyle = FlatStyle.Standard;
                editButton.HeaderText = "Action";
                editButton.Name = "Edit";
                editButton.Text = "Edit";
                editButton.UseColumnTextForButtonValue = true;
                editButton.Width = 100;
                if (geoStructureListgrid.Columns["Edit"] == null)
                {
                    geoStructureListgrid.Columns.Insert(geoStructureListgrid.Columns.Count, editButton);
                }
            }
        }

        /// <summary>
        /// //Referesh grid after Update geo model
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="data"></param>
        private void RefereshGlobleAfterUpdate(string entity, dynamic data)
        {
            if (entity == "Region")
            {
                var list = Globals.GeoList.RegionList.ToList();
                list.ForEach(r =>
                {
                    if (r.RegionId == data.RegionId)
                    {
                        r.RegionName = data.RegionName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtRegion.Clear();
                Globals.GeoList.RegionList = list;
                Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
            }
            else if (entity == "Province")
            {
                var list = Globals.GeoList.ProvinceList.ToList();
                list.ForEach(r =>
                {
                    if (r.ProvinceId == data.ProvinceId)
                    {
                        r.ProvinceName = data.ProvinceName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtProvince.Clear();
                Globals.GeoList.ProvinceList = list;
                Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
            }
            else if (entity == "District")
            {
                var list = Globals.GeoList.DistrictList.ToList();
                list.ForEach(r =>
                {
                    if (r.DistrictId == data.DistrictId)
                    {
                        r.BatchID = data.BatchID;
                        r.DistrictName = data.DistrictName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtDistrict.Clear();
                Globals.GeoList.DistrictList = list;
                Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
            }
            else if (entity == "LLG")
            {
                var list = Globals.GeoList.LLGList.ToList();
                list.ForEach(r =>
                {
                    if (r.LLGId == data.LLGId)
                    {
                        r.LLGName = data.LLGName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtLLG.Clear();
                Globals.GeoList.LLGList = list;
                Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
            }
            else if (entity == "Ward")
            {
                var list = Globals.GeoList.WardList.ToList();
                list.ForEach(r =>
                {
                    if (r.WardId == data.WardId)
                    {
                        r.WardName = data.WardName;
                        r.WardNumber = data.WardNumber;
                        r.Status = data.Status;
                    }
                });
                Globals.dtWard.Clear();
                list = list.OrderBy(o => o.WardNumber).ToList();
                Globals.GeoList.WardList = list;
                Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
            }
            else if (entity == "Location")
            {
                var list = Globals.GeoList.LocationList.ToList();
                list.ForEach(r =>
                {
                    if (r.LocationId == data.LocationId)
                    {
                        r.LocationName = data.LocationName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtLocation.Clear();
                Globals.GeoList.LocationList = list;
                Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());
            }

            Globals.GeoList.Version = Globals.GeoList.Version + 1;
            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");

            GlobalsModel.mainMaster.InitTreeData(false);
        }

        /// <summary>
        /// //Referesh grid after Adding geo model
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addedData"></param>
        private void RefereshGlobalDataAfterAdd(string entity, dynamic addedData)
        {
            if (entity == "Region")
            {
                var list = Globals.GeoList.RegionList.ToList();
                list.Add(addedData);
                Globals.dtRegion.Clear();
                Globals.GeoList.RegionList = list;
                Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
            }
            else if (entity == "Province")
            {
                var list = Globals.GeoList.ProvinceList.ToList();
                list.Add(addedData);
                Globals.dtProvince.Clear();
                Globals.GeoList.ProvinceList = list;
                Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
            }
            else if (entity == "District")
            {
                var list = Globals.GeoList.DistrictList.ToList();
                list.Add(addedData);
                Globals.dtDistrict.Clear();
                Globals.GeoList.DistrictList = list;
                Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
            }
            else if (entity == "LLG")
            {
                var list = Globals.GeoList.LLGList.ToList();
                list.Add(addedData);
                Globals.dtLLG.Clear();
                Globals.GeoList.LLGList = list;
                Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
            }
            else if (entity == "Ward")
            {
                var list = Globals.GeoList.WardList.ToList();
                list.Add(addedData);
                Globals.dtWard.Clear();
                list = list.OrderBy(o => o.WardNumber).ToList();
                Globals.GeoList.WardList = list;
                Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
            }
            else if (entity == "Location")
            {
                var list = Globals.GeoList.LocationList.ToList();
                list.Add(addedData);
                Globals.dtLocation.Clear();
                Globals.GeoList.LocationList = list;
                Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());
            }

            Globals.GeoList.Version = Globals.GeoList.Version + 1;
            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");

            GlobalsModel.mainMaster.InitTreeData(false);
        }

        /// <summary>
        /// Searches the filter records.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        private void SearchFilterRecords(string searchText)
        {
            GridViewSetting(TreeStructure, searchText, true);
            //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
            //GridViewSetting(selectedItem, searchText, true);
        }

        /// <summary>
        /// Refereshes the grid view data.
        /// </summary>
        private void RefereshGridViewData()
        {
            GridViewSetting(TreeStructure);
            //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
            //GridViewSetting(selectedItem);
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Handles the CellContentClick event of the geoStructureListgrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void geoStructureListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var geoModel = geoStructureListgrid.Rows[e.RowIndex].DataBoundItem;

            try
            {
                if (geoStructureListgrid.Columns[e.ColumnIndex].Name == "Edit")
                {
                    if (geoModel as RegionModel != null)
                    {
                        InitRegionFormAsync(geoModel as RegionModel);
                    }
                    else if (geoModel as ProvinceModel != null)
                    {
                        InitProvinceFormAsync(geoModel as ProvinceModel);
                    }
                    else if (geoModel as DistrictModel != null)
                    {
                        InitDistrictFormAsync(geoModel as DistrictModel);
                    }
                    else if (geoModel as LLGModel != null)
                    {
                        InitLLGFormAsync(geoModel as LLGModel);
                    }
                    else if (geoModel as WardModel != null)
                    {
                        InitWardFormAsync(geoModel as WardModel);
                    }
                    else if (geoModel as LocationModel != null)
                    {
                        InitLocationFormAsync(geoModel as LocationModel);
                    }

                }
                else if (geoStructureListgrid.Columns[e.ColumnIndex].Name == "Transfer")
                {
                    if (geoModel as LLGModel != null)
                    {
                        var llg = (geoModel as LLGModel);
                        LLGTransfer llgTransfer = new LLGTransfer(llg);
                        if (llgTransfer.ShowDialog() == DialogResult.OK)
                        {
                            //Update binded location list data from Globals.GeoList
                            llg.RegionId = llgTransfer.request.RegionId;
                            llg.ProvinceId = llgTransfer.request.ProvinceId;
                            llg.DistrictId = llgTransfer.request.DistrictId;
                            llg.LLGId = llgTransfer.request.LLGId;

                            var wardsOfLLG = Globals.GeoList.WardList.Where(x => x.LLGId == llg.LLGId).ToList();
                            wardsOfLLG.ForEach(ward =>
                            {
                                ward.RegionId = llg.RegionId;
                                ward.ProvinceId = llg.ProvinceId;
                                ward.DistrictId = llg.DistrictId;
                                ward.LLGId = llg.LLGId;
                            });

                            var locationsOfLLG = Globals.GeoList.LocationList.Where(x => x.LLGId == llg.LLGId).ToList();
                            locationsOfLLG.ForEach(loc =>
                            {
                                loc.RegionId = llg.RegionId;
                                loc.ProvinceId = llg.ProvinceId;
                                loc.DistrictId = llg.DistrictId;
                                loc.LLGId = llg.LLGId;
                            });

                            LoadGridViewSetting();

                            //Update geo datatables from New Globals.GeoList
                            Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
                            Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
                            Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
                            Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
                            Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
                            Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());

                            Globals.GeoList.Version = Globals.GeoList.Version + 1;

                            //Update json file from New Globals.GeoList
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");
                            GlobalsModel.mainMaster.InitTreeData(false);
                        }
                    }
                    else if (geoModel as WardModel != null)
                    {
                        var ward = (geoModel as WardModel);
                        WardTransfer wardTransfer = new WardTransfer(ward);
                        if (wardTransfer.ShowDialog() == DialogResult.OK)
                        {
                            //Update binded location list data from Globals.GeoList
                            ward.RegionId = wardTransfer.request.RegionId;
                            ward.ProvinceId = wardTransfer.request.ProvinceId;
                            ward.DistrictId = wardTransfer.request.DistrictId;
                            ward.LLGId = wardTransfer.request.LLGId;
                            ward.WardId = wardTransfer.request.WardId;

                            var locationsOfWard = Globals.GeoList.LocationList.Where(x => x.WardId == ward.WardId).ToList();
                            locationsOfWard.ForEach(loc =>
                            {
                                loc.RegionId = ward.RegionId;
                                loc.ProvinceId = ward.ProvinceId;
                                loc.DistrictId = ward.DistrictId;
                                loc.LLGId = ward.LLGId;
                                loc.WardId = ward.WardId;
                            });

                            LoadGridViewSetting();

                            //Update geo datatables from New Globals.GeoList
                            Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
                            Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
                            Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
                            Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
                            Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
                            Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());

                            Globals.GeoList.Version = Globals.GeoList.Version + 1;

                            //Update json file from New Globals.GeoList
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");
                            GlobalsModel.mainMaster.InitTreeData(false);
                        }
                    }
                    else if (geoModel as LocationModel != null)
                    {
                        //locationModel = new LocationModel();
                        LocationModel location = (geoModel as LocationModel);

                        //locationModel.RegionId = location.RegionId;
                        //locationModel.ProvinceId = location.ProvinceId;
                        //locationModel.DistrictId = location.DistrictId;
                        //locationModel.LLGId = location.LLGId;
                        //locationModel.WardId = location.WardId;
                        //locationModel.LocationId = location.LocationId;

                        //LocationTransfer locationTransfer = new LocationTransfer(locationModel);
                        LocationTransfer locationTransfer = new LocationTransfer(location);
                        if (locationTransfer.ShowDialog() == DialogResult.OK)
                        {
                            //Update binded location list data from Globals.GeoList
                            location.RegionId = locationTransfer.request.RegionId;
                            location.ProvinceId = locationTransfer.request.ProvinceId;
                            location.DistrictId = locationTransfer.request.DistrictId;
                            location.LLGId = locationTransfer.request.LLGId;
                            location.WardId = locationTransfer.request.WardId;
                            location.LocationId = locationTransfer.request.LocationId;
                            LoadGridViewSetting();

                            //Update geo datatables from New Globals.GeoList
                            Globals.dtRegion = CommonFunction.ToDataTable(Globals.GeoList.RegionList.ToList());
                            Globals.dtProvince = CommonFunction.ToDataTable(Globals.GeoList.ProvinceList.ToList());
                            Globals.dtDistrict = CommonFunction.ToDataTable(Globals.GeoList.DistrictList.ToList());
                            Globals.dtLLG = CommonFunction.ToDataTable(Globals.GeoList.LLGList.ToList());
                            Globals.dtWard = CommonFunction.ToDataTable(Globals.GeoList.WardList.ToList());
                            Globals.dtLocation = CommonFunction.ToDataTable(Globals.GeoList.LocationList.ToList());

                            Globals.GeoList.Version = Globals.GeoList.Version + 1;

                            //Update json file from New Globals.GeoList
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");
                            GlobalsModel.mainMaster.InitTreeData(false);
                        }
                    }

                }

                geoStructureListgrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                ErrorLog.Write("tvGeoStructureList", "geoStructureListgrid_CellContentClick", ex);
            }
        }

        /// <summary>
        /// Initializes the location form asynchronous.
        /// </summary>
        /// <param name="locationModel">The location model.</param>
        private void InitLocationFormAsync(LocationModel locationModel)
        {
            Location location = new Location(locationModel);
            if (location.ShowDialog() == DialogResult.OK)
            {
                var updatedData = location.ReturnLocationModel;
                RefereshGlobleAfterUpdate("Location", updatedData);
                GridViewSetting(new GeoTreeStructure() { MainTagName = "Ward", Id = locationModel.WardId });
            }
        }

        /// <summary>
        /// Initializes the ward form asynchronous.
        /// </summary>
        /// <param name="wardModel">The ward model.</param>
        private void InitWardFormAsync(WardModel wardModel)
        {
            Ward ward = new Ward(wardModel);
            if (ward.ShowDialog() == DialogResult.OK)
            {
                var updatedData = ward.ReturnWardModel;
                RefereshGlobleAfterUpdate("Ward", updatedData);
                GridViewSetting(new GeoTreeStructure() { MainTagName = "LLG", Id = wardModel.LLGId });
            }
        }

        /// <summary>
        /// Initializes the LLG form asynchronous.
        /// </summary>
        /// <param name="lLGModel">The l lg model.</param>
        private void InitLLGFormAsync(LLGModel lLGModel)
        {
            LLG llg = new LLG(lLGModel);
            if (llg.ShowDialog() == DialogResult.OK)
            {
                var updatedData = llg.ReturnLLGModel;
                RefereshGlobleAfterUpdate("LLG", updatedData);
                GridViewSetting(new GeoTreeStructure() { MainTagName = "District", Id = lLGModel.DistrictId });
            }
        }

        /// <summary>
        /// Initializes the district form asynchronous.
        /// </summary>
        /// <param name="districtModel">The district model.</param>
        private void InitDistrictFormAsync(DistrictModel districtModel)
        {
            District district = new District(districtModel);
            if (district.ShowDialog() == DialogResult.OK)
            {
                var updatedData = district.ReturnDistrictModel;
                RefereshGlobleAfterUpdate("District", updatedData);
                GridViewSetting(new GeoTreeStructure() { MainTagName = "Province", Id = districtModel.ProvinceId });
            }
        }

        /// <summary>
        /// Initializes the province form asynchronous.
        /// </summary>
        /// <param name="provinceModel">The province model.</param>
        private void InitProvinceFormAsync(ProvinceModel provinceModel)
        {
            Province province = new Province(provinceModel);
            if (province.ShowDialog() == DialogResult.OK)
            {
                var updatedData = province.ReturnProvinceModel;
                RefereshGlobleAfterUpdate("Province", updatedData);
                GridViewSetting(new GeoTreeStructure() { MainTagName = "Region", Id = provinceModel.RegionId });
            }
        }

        /// <summary>
        /// Initializes the region form asynchronous.
        /// </summary>
        /// <param name="regionModel">The region model.</param>
        private void InitRegionFormAsync(RegionModel regionModel)
        {
            Region region = new Region(regionModel);
            if (region.ShowDialog() == DialogResult.OK)
            {
                var updatedData = region.ReturnRegionModel;
                RefereshGlobleAfterUpdate("Region", updatedData);
                GridViewSetting(new GeoTreeStructure() { MainTagName = "Geo" });
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the geoStructureListgrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void geoStructureListgrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (geoStructureListgrid.Columns[e.ColumnIndex].Name == "Status")
            {
                var value = geoStructureListgrid.Rows[e.RowIndex].Cells["Id"].Value;

            }
        }

        /// <summary>
        /// Handles the AfterSelect event of the geoStructureTree control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void geoStructureTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GridViewSetting((GeoTreeStructure)e.Node.Tag);
            //if (e.Node.Tag != null)
            //{
            //    if (e.Node.Text == "PNGEC")
            //    {
            //        //GridViewSetting((List<GeoTreeStructure>)e.Node.Tag, "Region Name", "Region");
            //    }
            //    else
            //    {
            //        var data = (GeoTreeStructure)e.Node.Tag;
            //        //GridViewSetting(data.Children, data.ChildNodeName, data.MainTagName);

            //    }

            //}
        }

        /// <summary>
        /// Handles the Click event of the btnAddRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            Region mdiChildUser = new Region();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnRegionModel;
                RefereshGlobalDataAfterAdd("Region", addedData);
                GridViewSetting(TreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != null && !string.IsNullOrEmpty(txtSearch.Text))
            {
                SearchFilterRecords(txtSearch.Text);
            }
            else
            {
                RefereshGridViewData();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            RefereshGridViewData();
        }

        /// <summary>
        /// Handles the Click event of the btnAddProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddProvince_Click(object sender, EventArgs e)
        {
            Province mdiChildUser = new Province();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnProvinceModel;
                RefereshGlobalDataAfterAdd("Province", addedData);
                GridViewSetting(TreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }

        }

        /// <summary>
        /// Handles the Click event of the btnAddDistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddDistrict_Click(object sender, EventArgs e)
        {
            District mdiChildUser = new District();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnDistrictModel;
                RefereshGlobalDataAfterAdd("District", addedData);
                GridViewSetting(TreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }

        }

        /// <summary>
        /// Handles the Click event of the btnAddLLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddLLG_Click(object sender, EventArgs e)
        {
            LLG mdiChildUser = new LLG();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnLLGModel;
                RefereshGlobalDataAfterAdd("LLG", addedData);
                GridViewSetting(TreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }

        }

        /// <summary>
        /// Handles the Click event of the btnAddWard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddWard_Click(object sender, EventArgs e)
        {
            Ward mdiChildUser = new Ward();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnWardModel;
                RefereshGlobalDataAfterAdd("Ward", addedData);
                GridViewSetting(TreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }

        }

        /// <summary>
        /// Handles the Click event of the btnAddLocation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            Location mdiChildUser = new Location();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnLocationModel;
                RefereshGlobalDataAfterAdd("Location", addedData);
                GridViewSetting(TreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }

        }

        /// <summary>
        /// Handles the FormClosing event of the tvGeoStructureList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void tvGeoStructureList_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }
        #endregion
    }
}










































//locations = (from DataRow dr in Globals.dtLocation.Rows
//             select new GeoTreeStructure()
//             {
//                 Id = Convert.ToInt32(dr["LocationId"]),
//                 Name = dr["LocationName"].ToString(),
//                 MainTagName = "Location",
//                 ParentId = Convert.ToInt32(dr["WardId"]),
//                 Children = null
//             }).ToList();
//wards = (from DataRow dr in Globals.dtWard.Rows
//         select new GeoTreeStructure()
//         {
//             Id = Convert.ToInt32(dr["WardId"]),
//             Name = dr["WardName"].ToString(),
//             MainTagName = "Ward",
//             ParentId = Convert.ToInt32(dr["LLGId"]),
//             Children = locations.Where(l => l.ParentId == Convert.ToInt32(dr["WardId"])).ToList()
//         }).ToList();
//llgs = (from DataRow dr in Globals.dtLLG.Rows
//        select new GeoTreeStructure()
//        {
//            Name = dr["LLGName"].ToString(),
//            Id = Convert.ToInt32(dr["LLGId"]),
//            ParentId = Convert.ToInt32(dr["DistrictId"]),
//            MainTagName = "LLG",
//            Children = wards.Where(w => w.ParentId == Convert.ToInt32(dr["LLGId"])).ToList()
//        }).ToList();
//districts = (from DataRow dr in Globals.dtDistrict.Rows
//             select new GeoTreeStructure()
//             {
//                 Name = dr["DistrictName"].ToString(),
//                 ParentId = Convert.ToInt32(dr["ProvinceId"]),
//                 Id = Convert.ToInt32(dr["DistrictId"]),
//                 MainTagName = "District",
//                 Children = llgs.Where(w => w.ParentId == Convert.ToInt32(dr["DistrictId"])).ToList()
//             }).ToList();
//provinces = (from DataRow dr in Globals.dtProvince.Rows
//             select new GeoTreeStructure()
//             {
//                 Name = dr["ProvinceName"].ToString(),
//                 Id = Convert.ToInt32(dr["ProvinceId"]),
//                 ParentId = Convert.ToInt32(dr["RegionId"]),
//                 MainTagName = "Province",
//                 Children = districts.Where(w => w.ParentId == Convert.ToInt32(dr["ProvinceId"])).ToList()
//             }).ToList();
//regionList = (from DataRow dr in Globals.dtRegion.Rows
//              select new GeoTreeStructure()
//              {
//                  Id = Convert.ToInt32(dr["RegionId"]),
//                  Name = dr["RegionName"].ToString(),
//                  ParentId = default(int),
//                  MainTagName = "Region",
//                  Children = provinces.Where(p => p.ParentId == Convert.ToInt32(dr["RegionId"])).ToList()
//              }).ToList();