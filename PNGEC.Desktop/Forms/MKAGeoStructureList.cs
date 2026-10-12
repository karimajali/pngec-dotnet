using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PNGEC.API.Helper;
using PNGEC.Desktop.Helper;
using PNGEC.Desktop.Model;
using PNGEC.API.Domain.MKAGeoStructure;

namespace PNGEC.Desktop.Forms
{
    public partial class tvMKAGeoStructureList : Form
    {
        #region Variables

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private MKAGeoTreeStructure MKATreeStructure = null;

        /// <summary>
        /// The location Model
        /// </summary>
        //private LocationModel locationModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="tvMKAGeoStructureList"/> class.
        /// </summary>
        public tvMKAGeoStructureList()
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
        private void MKAGeoStructureList_Load(object sender, EventArgs e)
        {
            mkaGeoStructureTree.Visible = false;
            //Task.Run(() => InitTreeData());

            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            if (GlobalsModel.MKATreeViewStructure != null)
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
            MKATreeStructure = GlobalsModel.MKATreeViewStructure;
            GridViewSetting(MKATreeStructure);
        }

        /// <summary>
        ///Initialize Tree Data
        /// </summary>
        private void InitTreeData()
        {
            var mkaLocs = Globals.MKAGeoList.MKALocationList.Select(l => new MKAGeoTreeStructure()
            {
                Id = l.LocationId,
                Name = l.LocationName,
                MainTagName = "Location",
                ParentId = l.WardId,
                Children = null
            });
            var mkaWards = Globals.MKAGeoList.MKAWardList.OrderBy(o => o.WardNumber).Select(w => new MKAGeoTreeStructure()
            {
                Id = w.WardId,
                Name = $"{w.WardNumber} - {w.WardName}",
                MainTagName = "Ward",
                ParentId = w.LLGId,
                Children = mkaLocs.Where(l => l.ParentId == w.WardId)
            });
            var mkaLLGs = Globals.MKAGeoList.MKALLGList.Select(w => new MKAGeoTreeStructure()
            {
                Id = w.LLGId,
                Name = w.LLGName,
                MainTagName = "LLG",
                ParentId = w.DistrictId,
                Children = mkaWards.Where(l => l.ParentId == w.LLGId)
            });
            var mkaDists = Globals.MKAGeoList.MKADistrictList.Select(w => new MKAGeoTreeStructure()
            {
                Id = w.DistrictId,
                Name = w.DistrictName,
                MainTagName = "District",
                ParentId = default(int),
                Children = mkaLLGs.Where(l => l.ParentId == w.DistrictId)
            });
            InitializeMKAActualTreeLogic(mkaDists);
        }

        /// <summary>
        /// recusive logic to set data in tree node.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentNode"></param>
        private void PopulateTreeView(IEnumerable<MKAGeoTreeStructure> data, TreeNode parentNode)
        {
            TreeNode childNode;
            foreach (MKAGeoTreeStructure row in data)
            {
                TreeNode t = new TreeNode();
                t.Text = row.Name.ToString();
                t.Tag = row;
                t.Name = row.MainTagName;
                t.ImageKey = Convert.ToString(row.Id);
                if (parentNode == null)
                {
                    mkaGeoStructureTree.Nodes.Add(t);
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
        private async void InitializeMKAActualTreeLogic(IEnumerable<MKAGeoTreeStructure> mkaDistrict)
        {
            //this.Cursor = Cursors.WaitCursor;
            MKAGeoTreeStructure mkaGeoTreeStructure = new MKAGeoTreeStructure();
            mkaGeoTreeStructure.Id = 0;
            mkaGeoTreeStructure.MainTagName = "MKAGeo";
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

            mkaGeoStructureTree.Invoke((Action)delegate ()
            {
                this.Cursor = Cursors.WaitCursor;
                this.pnlGeoButtons.Enabled = false;
                TreeNode t = new TreeNode();
                t.Text = "MKA PNGEC";
                t.Tag = mkaDistrict;
                t.Name = "MKAGeo";
                t.ImageKey = "PNGEC";
                t.Tag = mkaGeoTreeStructure;
                mkaGeoStructureTree.BeginUpdate(); // No visual updates until we say 
                mkaGeoStructureTree.Nodes.Clear(); // Remove existing nodes
                mkaGeoStructureTree.Nodes.Add(t);
                PopulateTreeView(mkaDistrict, t);
                mkaGeoStructureTree.SelectedNode = t;
                mkaGeoStructureTree.SelectedNode.Expand();
                mkaGeoStructureTree.EndUpdate(); // Allow the treeview to update visually
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
        private void GridViewSetting(MKAGeoTreeStructure data, string searchText = null, bool isFromSearch = false)
        {
            if (mkaGeoStructureListgrid.Columns.Count > 0)
            {
                mkaGeoStructureListgrid.Columns.Clear();
            }
           
            if (data.MainTagName == "MKAGeo")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    mkaGeoStructureListgrid.DataSource = Globals.MKAGeoList.MKADistrictList.Where(f =>
                    f.DistrictName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    mkaGeoStructureListgrid.DataSource = Globals.MKAGeoList.MKADistrictList.ToList();
                }
                mkaGeoStructureListgrid.Refresh();
                //mkaGeoStructureListgrid.Columns["RegionId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Status"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionName"].Visible = true;

                //mkaGeoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ModifiedDate"].Visible = false;
                //mkaGeoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Comment"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ActionBy"].Visible = false;

                mkaGeoStructureListgrid.Columns["DistrictName"].HeaderText = "MKA District Name";
                mkaGeoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                mkaGeoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in mkaGeoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                mkaGeoStructureListgrid.Columns["DistrictName"].Visible = true;
                mkaGeoStructureListgrid.Columns["StatusName"].Visible = true;
                mkaGeoStructureListgrid.Columns["LastChangeDate"].Visible = true;
            }
            else if (data.MainTagName == "MKADistrict")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    mkaGeoStructureListgrid.DataSource = Globals.MKAGeoList.MKALLGList.Where(f =>
                    f.DistrictId == data.Id &&
                    f.LLGName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.MKAGeoList.MKALLGList.Where(p => p.DistrictId == data.Id).ToList();
                    mkaGeoStructureListgrid.DataSource = list;
                }

                mkaGeoStructureListgrid.Refresh();
                //mkaGeoStructureListgrid.Columns["LLGId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Status"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //mkaGeoStructureListgrid.Columns["DistrictName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["LLGName"].Visible = true;
                //mkaGeoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ModifiedDate"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["DistrictId"].Visible = false;

                //mkaGeoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Comment"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ActionBy"].Visible = false;

                mkaGeoStructureListgrid.Columns["LLGName"].HeaderText = "MKA LLG Name";
                mkaGeoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                mkaGeoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in mkaGeoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                mkaGeoStructureListgrid.Columns["LLGName"].Visible = true;
                mkaGeoStructureListgrid.Columns["StatusName"].Visible = true;
                mkaGeoStructureListgrid.Columns["LastChangeDate"].Visible = true;

                DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                transferButton.FlatStyle = FlatStyle.Standard;
                transferButton.HeaderText = "Action";
                transferButton.Name = "Transfer";
                transferButton.Text = "Transfer";
                transferButton.UseColumnTextForButtonValue = true;
                transferButton.Width = 100;
                if (mkaGeoStructureListgrid.Columns["Transfer"] == null)
                {
                   // mkaGeoStructureListgrid.Columns.Insert(mkaGeoStructureListgrid.Columns.Count, transferButton);
                }
            }
            else if (data.MainTagName == "MKALLG")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    mkaGeoStructureListgrid.DataSource = Globals.MKAGeoList.MKAWardList.Where(f =>
                    f.LLGId == data.Id &&
                    f.WardName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.MKAGeoList.MKAWardList.Where(p => p.LLGId == data.Id).ToList();
                    mkaGeoStructureListgrid.DataSource = list;
                }

                mkaGeoStructureListgrid.Refresh();
                //mkaGeoStructureListgrid.Columns["WardId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Status"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //mkaGeoStructureListgrid.Columns["LLGName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["WardName"].Visible = true;
                //mkaGeoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ModifiedDate"].Visible = false;

                //mkaGeoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["DistrictId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["LLGId"].Visible = false;

                //mkaGeoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Comment"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["DistrictName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["WardNoWardName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ActionBy"].Visible = false;

                mkaGeoStructureListgrid.Columns["WardName"].HeaderText = "MKA Ward Name";
                mkaGeoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                mkaGeoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in mkaGeoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                mkaGeoStructureListgrid.Columns["WardName"].Visible = true;
                mkaGeoStructureListgrid.Columns["StatusName"].Visible = true;
                mkaGeoStructureListgrid.Columns["LastChangeDate"].Visible = true;

                DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                transferButton.FlatStyle = FlatStyle.Standard;
                transferButton.HeaderText = "Action";
                transferButton.Name = "Transfer";
                transferButton.Text = "Transfer";
                transferButton.UseColumnTextForButtonValue = true;
                transferButton.Width = 100;
                if (mkaGeoStructureListgrid.Columns["Transfer"] == null)
                {
                    //mkaGeoStructureListgrid.Columns.Insert(mkaGeoStructureListgrid.Columns.Count, transferButton);
                }
            }
            else if (data.MainTagName == "MKAWard")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    mkaGeoStructureListgrid.DataSource = Globals.MKAGeoList.MKALocationList.Where(f =>
                    f.WardId == data.Id &&
                    f.LocationName.ToLower().Contains(searchText.ToLower())).ToList();
                }
                else
                {
                    var list = Globals.MKAGeoList.MKALocationList.Where(p => p.WardId == data.Id).ToList();
                    mkaGeoStructureListgrid.DataSource = list;
                }
                mkaGeoStructureListgrid.Refresh();
                //mkaGeoStructureListgrid.Columns["LocationId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Status"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ResponseCode"].Visible = false;
                //mkaGeoStructureListgrid.Columns["WardName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["CreatedDate"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ModifiedDate"].Visible = false;

                //mkaGeoStructureListgrid.Columns["ProvinceId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["DistrictId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["LLGId"].Visible = false;
                //mkaGeoStructureListgrid.Columns["WardId"].Visible = false;

                //mkaGeoStructureListgrid.Columns["UpdateByUserName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["Comment"].Visible = false;
                //mkaGeoStructureListgrid.Columns["LLGName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["RegionName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ProvinceName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["DistrictName"].Visible = false;
                //mkaGeoStructureListgrid.Columns["ActionBy"].Visible = false;

                mkaGeoStructureListgrid.Columns["LocationName"].HeaderText = "MKA Location Name";
                mkaGeoStructureListgrid.Columns["StatusName"].HeaderText = "Status";
                mkaGeoStructureListgrid.Columns["LastChangeDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in mkaGeoStructureListgrid.Columns)
                {
                    item.Visible = false;
                }

                mkaGeoStructureListgrid.Columns["LocationName"].Visible = true;
                mkaGeoStructureListgrid.Columns["StatusName"].Visible = true;
                mkaGeoStructureListgrid.Columns["LastChangeDate"].Visible = true;

                DataGridViewButtonColumn transferButton = new DataGridViewButtonColumn();
                transferButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                transferButton.FlatStyle = FlatStyle.Standard;
                transferButton.HeaderText = "Action";
                transferButton.Name = "Transfer";
                transferButton.Text = "Transfer";
                transferButton.UseColumnTextForButtonValue = true;
                transferButton.Width = 100;
                if (mkaGeoStructureListgrid.Columns["Transfer"] == null)
                {
                    //mkaGeoStructureListgrid.Columns.Insert(mkaGeoStructureListgrid.Columns.Count, transferButton);
                }
            }
            else
            {

            }

            if (data.MainTagName != "MKALocation")
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
                if (mkaGeoStructureListgrid.Columns["Edit"] == null)
                {
                    mkaGeoStructureListgrid.Columns.Insert(mkaGeoStructureListgrid.Columns.Count, editButton);
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
            if (entity == "MKADistrict")
            {
                var list = Globals.MKAGeoList.MKADistrictList.ToList();
                list.ForEach(r =>
                {
                    if (r.DistrictId == data.DistrictId)
                    {
                        //r.BatchID = data.BatchID;
                        r.DistrictName = data.DistrictName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtMKADistricts.Clear();
                Globals.MKAGeoList.MKADistrictList = list;
                Globals.dtMKADistricts = CommonFunction.ToDataTable(Globals.MKAGeoList.MKADistrictList.ToList());
            }
            else if (entity == "MKALLG")
            {
                var list = Globals.MKAGeoList.MKALLGList.ToList();
                list.ForEach(r =>
                {
                    if (r.LLGId == data.LLGId)
                    {
                        r.LLGName = data.LLGName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtLLG.Clear();
                Globals.MKAGeoList.MKALLGList = list;
                Globals.dtLLG = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALLGList.ToList());
            }
            else if (entity == "MKAWard")
            {
                var list = Globals.MKAGeoList.MKAWardList.ToList();
                list.ForEach(r =>
                {
                    if (r.WardId == data.WardId)
                    {
                        r.WardName = data.WardName;
                        r.WardNumber = data.WardNumber;
                        r.Status = data.Status;
                    }
                });
                Globals.dtMKAWards.Clear();
                list = list.OrderBy(o => o.WardNumber).ToList();
                Globals.MKAGeoList.MKAWardList = list;
                Globals.dtMKAWards = CommonFunction.ToDataTable(Globals.MKAGeoList.MKAWardList.ToList());
            }
            else if (entity == "MKALocation")
            {
                var list = Globals.MKAGeoList.MKALocationList.ToList();
                list.ForEach(r =>
                {
                    if (r.LocationId == data.LocationId)
                    {
                        r.LocationName = data.LocationName;
                        r.Status = data.Status;
                    }
                });
                Globals.dtMKALocations.Clear();
                Globals.MKAGeoList.MKALocationList = list;
                Globals.dtMKALocations = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALocationList.ToList());
            }

            Globals.MKAGeoList.Version = Globals.MKAGeoList.Version + 1;
            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.MKAGeoList), "mkageolist");

            GlobalsModel.mainMaster.InitTreeData(false);
        }

        /// <summary>
        /// //Referesh grid after Adding geo model
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="addedData"></param>
        private void RefereshGlobalDataAfterAdd(string entity, dynamic addedData)
        {            
            if (entity == "MKADistrict")
            {
                var list = Globals.MKAGeoList.MKADistrictList.ToList();
                list.Add(addedData);
                Globals.dtMKADistricts.Clear();
                Globals.MKAGeoList.MKADistrictList = list;
                Globals.dtMKADistricts = CommonFunction.ToDataTable(Globals.MKAGeoList.MKADistrictList.ToList());
            }
            else if (entity == "MKALLG")
            {
                var list = Globals.MKAGeoList.MKALLGList.ToList();
                list.Add(addedData);
                Globals.dtMKALLGs.Clear();
                Globals.MKAGeoList.MKALLGList = list;
                Globals.dtMKALLGs = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALLGList.ToList());
            }
            else if (entity == "MKAWard")
            {
                var list = Globals.MKAGeoList.MKAWardList.ToList();
                list.Add(addedData);
                Globals.dtMKAWards.Clear();
                list = list.OrderBy(o => o.WardNumber).ToList();
                Globals.MKAGeoList.MKAWardList = list;
                Globals.dtMKAWards = CommonFunction.ToDataTable(Globals.MKAGeoList.MKAWardList.ToList());
            }
            else if (entity == "MKALocation")
            {
                var list = Globals.MKAGeoList.MKALocationList.ToList();
                list.Add(addedData);
                Globals.dtMKALocations.Clear();
                Globals.MKAGeoList.MKALocationList = list;
                Globals.dtMKALocations = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALocationList.ToList());
            }

            Globals.MKAGeoList.Version = Globals.MKAGeoList.Version + 1;
            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.MKAGeoList), "mkageolist");

            GlobalsModel.mainMaster.InitTreeData(false);
        }

        /// <summary>
        /// Searches the filter records.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        private void SearchFilterRecords(string searchText)
        {
            GridViewSetting(MKATreeStructure, searchText, true);
            //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
            //GridViewSetting(selectedItem, searchText, true);
        }

        /// <summary>
        /// Refereshes the grid view data.
        /// </summary>
        private void RefereshGridViewData()
        {
            GridViewSetting(MKATreeStructure);
            //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
            //GridViewSetting(selectedItem);
        }
        #endregion

        #region Methods        
        /// <summary>
        /// Handles the CellContentClick event of the mkaGeoStructureListgrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void mkaGeoStructureListgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var geoModel = mkaGeoStructureListgrid.Rows[e.RowIndex].DataBoundItem;

            try
            {
                if (mkaGeoStructureListgrid.Columns[e.ColumnIndex].Name == "Edit")
                {                    
                    if (geoModel as MKADistrictModel != null)
                    {
                        InitMKADistrictFormAsync(geoModel as MKADistrictModel);
                    }
                    else if (geoModel as MKALLGModel != null)
                    {
                        InitMKALLGFormAsync(geoModel as MKALLGModel);
                    }
                    else if (geoModel as MKAWardModel != null)
                    {
                        InitMKAWardFormAsync(geoModel as MKAWardModel);
                    }
                    else if (geoModel as MKALocationModel != null)
                    {
                        InitMKALocationFormAsync(geoModel as MKALocationModel);
                    }

                }

                #region Transfer
                else if (mkaGeoStructureListgrid.Columns[e.ColumnIndex].Name == "Transfer")
                {
                    if (geoModel as MKALLGModel != null)
                    {
                        var mkaLLG = (geoModel as MKALLGModel);
                        MKALLGTransfer mkaLLGTransfer = new MKALLGTransfer(mkaLLG);
                        if (mkaLLGTransfer.ShowDialog() == DialogResult.OK)
                        {
                            //Update binded location list data from Globals.GeoList                           
                            mkaLLG.DistrictId = mkaLLGTransfer.request.DistrictId;
                            mkaLLG.LLGId = mkaLLGTransfer.request.LLGId;

                            var wardsOfLLG = Globals.MKAGeoList.MKAWardList.Where(x => x.LLGId == mkaLLG.LLGId).ToList();
                            wardsOfLLG.ForEach(ward =>
                            {                                
                                ward.DistrictId = mkaLLG.DistrictId;
                                ward.LLGId = mkaLLG.LLGId;
                            });

                            var locationsOfLLG = Globals.MKAGeoList.MKALocationList.Where(x => x.LLGId == mkaLLG.LLGId).ToList();
                            locationsOfLLG.ForEach(loc =>
                            {                               
                                loc.DistrictId = mkaLLG.DistrictId;
                                loc.LLGId = mkaLLG.LLGId;
                            });

                            LoadGridViewSetting();

                            //Update geo datatables from New Globals.GeoList
                           
                            Globals.dtMKADistricts  = CommonFunction.ToDataTable(Globals.MKAGeoList.MKADistrictList.ToList());
                            Globals.dtMKALLGs = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALLGList.ToList());
                            Globals.dtMKAWards = CommonFunction.ToDataTable(Globals.MKAGeoList.MKAWardList.ToList());
                            Globals.dtMKALocations = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALocationList.ToList());

                            Globals.MKAGeoList.Version = Globals.MKAGeoList.Version + 1;

                            //Update json file from New Globals.GeoList
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.MKAGeoList), "mkageolist");
                            GlobalsModel.mainMaster.InitTreeData(false);
                        }
                    }
                    else if (geoModel as MKAWardModel != null)
                    {
                        var mkaWard = (geoModel as MKAWardModel);
                        MKAWardTransfer mkaWardTransfer = new MKAWardTransfer(mkaWard);
                        if (mkaWardTransfer.ShowDialog() == DialogResult.OK)
                        {
                            //Update binded location list data from Globals.GeoList                           
                            mkaWard.DistrictId = mkaWardTransfer.request.DistrictId;
                            mkaWard.LLGId = mkaWardTransfer.request.LLGId;
                            mkaWard.WardId = mkaWardTransfer.request.WardId;

                            var locationsOfWard = Globals.MKAGeoList.MKALocationList.Where(x => x.WardId == mkaWard.WardId).ToList();
                            locationsOfWard.ForEach(loc =>
                            {
                                loc.DistrictId = mkaWard.DistrictId;
                                loc.LLGId = mkaWard.LLGId;
                                loc.WardId = mkaWard.WardId;
                            });

                            LoadGridViewSetting();

                            //Update geo datatables from New Globals.GeoList                           
                            Globals.dtMKADistricts = CommonFunction.ToDataTable(Globals.MKAGeoList.MKADistrictList.ToList());
                            Globals.dtMKALLGs = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALLGList.ToList());
                            Globals.dtMKAWards = CommonFunction.ToDataTable(Globals.MKAGeoList.MKAWardList.ToList());
                            Globals.dtMKALocations = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALocationList.ToList());

                            Globals.MKAGeoList.Version = Globals.MKAGeoList.Version + 1;

                            //Update json file from New Globals.GeoList
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.MKAGeoList), "mkageolist");
                            GlobalsModel.mainMaster.InitTreeData(false);
                        }
                    }
                    else if (geoModel as MKALocationModel != null)
                    {
                        //locationModel = new LocationModel();
                        MKALocationModel mkaLocation = (geoModel as MKALocationModel);                     
                        MKALocationTransfer mkaLocationTransfer = new MKALocationTransfer(mkaLocation);

                        if (mkaLocationTransfer.ShowDialog() == DialogResult.OK)
                        {
                            //Update binded location list data from Globals.GeoList

                            mkaLocation.DistrictId = mkaLocationTransfer.request.DistrictId;
                            mkaLocation.LLGId = mkaLocationTransfer.request.LLGId;
                            mkaLocation.WardId = mkaLocationTransfer.request.WardId;
                            mkaLocation.LocationId = mkaLocationTransfer.request.LocationId;
                            LoadGridViewSetting();

                            //Update geo datatables from New Globals.GeoList

                            Globals.dtMKADistricts = CommonFunction.ToDataTable(Globals.MKAGeoList.MKADistrictList.ToList());
                            Globals.dtMKALLGs = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALLGList.ToList());
                            Globals.dtMKAWards = CommonFunction.ToDataTable(Globals.MKAGeoList.MKAWardList.ToList());
                            Globals.dtMKALocations = CommonFunction.ToDataTable(Globals.MKAGeoList.MKALocationList.ToList());

                            Globals.MKAGeoList.Version = Globals.MKAGeoList.Version + 1;

                            //Update json file from New Globals.GeoList
                            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.MKAGeoList), "mkageolist");
                            GlobalsModel.mainMaster.InitTreeData(false);
                        }
                    }

                }
                #endregion

                mkaGeoStructureListgrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                ErrorLog.Write("tvGeoStructureList", "mkaGeoStructureListgrid_CellContentClick", ex);
            }
        }

        /// <summary>
        /// Initializes the location form asynchronous.
        /// </summary>
        /// <param name="mkaLocationModel">The location model.</param>
        private void InitMKALocationFormAsync(MKALocationModel mkaLocationModel)
        {
            MKALocation mkaLocation = new MKALocation(mkaLocationModel);
            if (mkaLocation.ShowDialog() == DialogResult.OK)
            {
                var updatedData = mkaLocation.ReturnMKALocationModel;
                RefereshGlobleAfterUpdate("MKALocation", updatedData);
                GridViewSetting(new MKAGeoTreeStructure() { MainTagName = "MKAWard", Id = mkaLocationModel.WardId });
            }
        }

        /// <summary>
        /// Initializes the ward form asynchronous.
        /// </summary>
        /// <param name="wardModel">The ward model.</param>
        private void InitMKAWardFormAsync(MKAWardModel mkaWardModel)
        {
            MKAWard ward = new MKAWard(mkaWardModel);
            if (ward.ShowDialog() == DialogResult.OK)
            {
                var updatedData = ward.ReturnMKAWardModel;
                RefereshGlobleAfterUpdate("MKAWard", updatedData);
                GridViewSetting(new MKAGeoTreeStructure() { MainTagName = "MKALLG", Id = mkaWardModel.LLGId });
            }
        }

        /// <summary>
        /// Initializes the LLG form asynchronous.
        /// </summary>
        /// <param name="lLGModel">The l lg model.</param>
        private void InitMKALLGFormAsync(MKALLGModel mkaLLGModel)
        {
            MKALLG mkaLLG = new MKALLG(mkaLLGModel);
            if (mkaLLG.ShowDialog() == DialogResult.OK)
            {
                var updatedData = mkaLLG.ReturnMKALLGModel;
                RefereshGlobleAfterUpdate("MKALLG", updatedData);
                GridViewSetting(new MKAGeoTreeStructure() { MainTagName = "District", Id = mkaLLGModel.DistrictId });
            }
        }

        /// <summary>
        /// Initializes the district form asynchronous.
        /// </summary>
        /// <param name="districtModel">The district model.</param>
        private void InitMKADistrictFormAsync(MKADistrictModel mkaDistrictModel)
        {
            MKADistrict mkaDistrict = new MKADistrict(mkaDistrictModel);
            if (mkaDistrict.ShowDialog() == DialogResult.OK)
            {
                var updatedData = mkaDistrict.ReturnMKADistrictModel;
                RefereshGlobleAfterUpdate("MKADistrict", updatedData);
                GridViewSetting(new MKAGeoTreeStructure() { MainTagName = "MKAGeo" });
            }
        }

        /// <summary>
        /// Initializes the province form asynchronous.
        /// </summary>
        /// <param name="provinceModel">The province model.</param>
        //private void InitProvinceFormAsync(ProvinceModel provinceModel)
        //{
            //Province province = new Province(provinceModel);
            //if (province.ShowDialog() == DialogResult.OK)
            //{
            //    var updatedData = province.ReturnProvinceModel;
            //    RefereshGlobleAfterUpdate("Province", updatedData);
            //    GridViewSetting(new GeoTreeStructure() { MainTagName = "Region", Id = provinceModel.RegionId });
            //}
        //}

        /// <summary>
        /// Initializes the region form asynchronous.
        /// </summary>
        /// <param name="regionModel">The region model.</param>
        //private void InitRegionFormAsync(RegionModel regionModel)
        //{
        //    //Region region = new Region(regionModel);
        //    //if (region.ShowDialog() == DialogResult.OK)
        //    //{
        //    //    var updatedData = region.ReturnRegionModel;
        //    //    RefereshGlobleAfterUpdate("Region", updatedData);
        //    //    GridViewSetting(new GeoTreeStructure() { MainTagName = "Geo" });
        //    //}
        //}

        /// <summary>
        /// Handles the CellValueChanged event of the mkaGeoStructureListgrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void mkaGeoStructureListgrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (mkaGeoStructureListgrid.Columns[e.ColumnIndex].Name == "Status")
            {
                var value = mkaGeoStructureListgrid.Rows[e.RowIndex].Cells["Id"].Value;

            }
        }

        /// <summary>
        /// Handles the AfterSelect event of the geoStructureTree control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TreeViewEventArgs"/> instance containing the event data.</param>
        private void geoStructureTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            GridViewSetting((MKAGeoTreeStructure)e.Node.Tag);
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
        /// Handles the Click event of the btnAddMKADistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       
        private void btnAddMKADistrict_Click(object sender, EventArgs e)
        {
            MKADistrict mdiChildUser = new MKADistrict();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnMKADistrictModel;
                RefereshGlobalDataAfterAdd("MKADistrict", addedData);
                GridViewSetting(MKATreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddMKALLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       
        private void btnAddMKALLG_Click(object sender, EventArgs e)
        {
            MKALLG mdiChildUser = new MKALLG();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnMKALLGModel;
                RefereshGlobalDataAfterAdd("MKALLG", addedData);
                GridViewSetting(MKATreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }
        }


        /// <summary>
        /// Handles the Click event of the btnAddMKAWard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>      
        private void btnAddMKAWard_Click(object sender, EventArgs e)
        {
            MKAWard mdiChildUser = new MKAWard();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnMKAWardModel;
                RefereshGlobalDataAfterAdd("MKAWard", addedData);
                GridViewSetting(MKATreeStructure);
                //var selectedItem = (GeoTreeStructure)geoStructureTree.SelectedNode.Tag;
                //GridViewSetting(selectedItem);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAddMKALocation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddMKALocation_Click(object sender, EventArgs e)
        {

            MKALocation mdiChildUser = new MKALocation();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                var addedData = mdiChildUser.ReturnMKALocationModel;
                RefereshGlobalDataAfterAdd("MKALocation", addedData);
                GridViewSetting(MKATreeStructure);
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
