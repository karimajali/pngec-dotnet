using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PNGEC.API.Domain.District;
using PNGEC.API.Domain.LLG;
using PNGEC.API.Domain.Clan;
using PNGEC.API.Domain.Province;
using PNGEC.API.Domain.Region;
using PNGEC.API.Domain.Ward;
using PNGEC.API.Helper;
using PNGEC.Desktop.Helper;
using PNGEC.Desktop.Model;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using PNGEC.Desktop.Properties;
using System.IO;

namespace PNGEC.Desktop.Forms
{
    public partial class tvGeoClanList : Form
    {
        #region Variables

        /// <summary>
        /// The geo tree structure
        /// </summary>
        private GeoTreeStructure TreeStructure = null;


        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dtClanReport = null;

        /// <summary>
        /// The Clan Model
        /// </summary>
        //private ClanModel ClanModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="tvGeoClanList"/> class.
        /// </summary>
        public tvGeoClanList()
        {
            InitializeComponent();
            dtClanReport = new DataTable();
        }
        #endregion

        #region Events
        /// <summary>
        /// //Main Geo List Page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvGeoClanList_Load(object sender, EventArgs e)
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
            var loc = Globals.GeoList.ClanList.Select(l => new GeoTreeStructure()
            {
                Id = l.ClanId,
                Name = l.ClanName,
                MainTagName = "Clan",
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
            if (geoClanListgrid.Columns.Count > 0)
            {
                geoClanListgrid.Columns.Clear();
            }

            if (data.MainTagName == "Geo")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(f =>
                    f.ClanName.ToLower().Contains(searchText.ToLower())).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                else
                {
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                geoClanListgrid.Refresh();

                geoClanListgrid.Columns["ClanName"].HeaderText = "Clan Name";
                geoClanListgrid.Columns["Status"].HeaderText = "Status";
                geoClanListgrid.Columns["ModifiedDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoClanListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoClanListgrid.Columns["ClanName"].Visible = true;
                geoClanListgrid.Columns["Status"].Visible = true;
                geoClanListgrid.Columns["ModifiedDate"].Visible = true;
            }
            else if (data.MainTagName == "Region")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(f =>
                    f.RegionId == data.Id &&
                    f.ClanName.ToLower().Contains(searchText.ToLower())).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                else
                {
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(p => p.RegionId == data.Id).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                geoClanListgrid.Refresh();
                geoClanListgrid.Columns["ClanName"].HeaderText = "Clan Name";
                geoClanListgrid.Columns["Status"].HeaderText = "Status";
                geoClanListgrid.Columns["ModifiedDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoClanListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoClanListgrid.Columns["ClanName"].Visible = true;
                geoClanListgrid.Columns["Status"].Visible = true;
                geoClanListgrid.Columns["ModifiedDate"].Visible = true;
            }
            else if (data.MainTagName == "Province")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(f =>
                    f.ProvinceId == data.Id &&
                    f.ClanName.ToLower().Contains(searchText.ToLower())).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                else
                {
                    var list = Globals.GeoList.ClanList.Where(p => p.ProvinceId == data.Id).ToList().OrderBy(x => x.ClanName).ToList(); ;
                    geoClanListgrid.DataSource = list;
                }

                geoClanListgrid.Refresh();
                geoClanListgrid.Columns["ClanName"].HeaderText = "Clan Name";
                geoClanListgrid.Columns["Status"].HeaderText = "Status";
                geoClanListgrid.Columns["ModifiedDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoClanListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoClanListgrid.Columns["ClanName"].Visible = true;
                geoClanListgrid.Columns["Status"].Visible = true;
                geoClanListgrid.Columns["ModifiedDate"].Visible = true;
            }
            else if (data.MainTagName == "District")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(f =>
                    f.DistrictId == data.Id &&
                    f.ClanName.ToLower().Contains(searchText.ToLower())).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                else
                {
                    var list = Globals.GeoList.ClanList.Where(p => p.DistrictId == data.Id).ToList().OrderBy(x => x.ClanName).ToList(); ;
                    geoClanListgrid.DataSource = list;
                }

                geoClanListgrid.Refresh();

                geoClanListgrid.Columns["ClanName"].HeaderText = "Clan Name";
                geoClanListgrid.Columns["Status"].HeaderText = "Status";
                geoClanListgrid.Columns["ModifiedDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoClanListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoClanListgrid.Columns["ClanName"].Visible = true;
                geoClanListgrid.Columns["Status"].Visible = true;
                geoClanListgrid.Columns["ModifiedDate"].Visible = true;

            }
            else if (data.MainTagName == "LLG")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(f =>
                    f.LLGId == data.Id &&
                    f.ClanName.ToLower().Contains(searchText.ToLower())).ToList().OrderBy(x => x.ClanName).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                else
                {
                    var list = Globals.GeoList.ClanList.Where(p => p.LLGId == data.Id).ToList().OrderBy(x => x.ClanName).ToList().OrderBy(x => x.ClanName).ToList(); ; ;
                    geoClanListgrid.DataSource = list;
                }

                geoClanListgrid.Refresh();
                geoClanListgrid.Columns["ClanName"].HeaderText = "Clan Name";
                geoClanListgrid.Columns["Status"].HeaderText = "Status";
                geoClanListgrid.Columns["ModifiedDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoClanListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoClanListgrid.Columns["ClanName"].Visible = true;
                geoClanListgrid.Columns["Status"].Visible = true;
                geoClanListgrid.Columns["ModifiedDate"].Visible = true;

            }
            else if (data.MainTagName == "Ward")
            {
                if (isFromSearch || (!string.IsNullOrEmpty(txtSearch.Text) && !string.IsNullOrWhiteSpace(txtSearch.Text)))
                {
                    searchText = txtSearch.Text;
                    geoClanListgrid.DataSource = Globals.GeoList.ClanList.Where(f =>
                    f.WardId == data.Id &&
                    f.ClanName.ToLower().Contains(searchText.ToLower())).ToList().OrderBy(x => x.ClanName).ToList(); ;
                }
                else
                {
                    var list = Globals.GeoList.ClanList.Where(p => p.WardId == data.Id).ToList().OrderBy(x => x.ClanName).ToList(); ;
                    geoClanListgrid.DataSource = list;
                }
                geoClanListgrid.Refresh();

                geoClanListgrid.Columns["ClanName"].HeaderText = "Clan Name";
                geoClanListgrid.Columns["Status"].HeaderText = "Status";
                geoClanListgrid.Columns["ModifiedDate"].HeaderText = "Last Change Date";

                foreach (DataGridViewColumn item in geoClanListgrid.Columns)
                {
                    item.Visible = false;
                }

                geoClanListgrid.Columns["ClanName"].Visible = true;
                geoClanListgrid.Columns["Status"].Visible = true;
                geoClanListgrid.Columns["ModifiedDate"].Visible = true;

            }
            else
            {

            }

            if (data.MainTagName != "Clan")
            {
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
                    if (geoClanListgrid.Columns["Edit"] == null)
                    {
                        geoClanListgrid.Columns.Insert(geoClanListgrid.Columns.Count, editButton);
                    }
                }
            }

            if (data.MainTagName != "Clan")
            {
                if (data.MainTagName != "Location")
                {
                    DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                    deleteButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    //deleteButton.CellTemplate = new DataGridViewButtonCell();
                    deleteButton.FlatStyle = FlatStyle.Standard;
                    deleteButton.HeaderText = "Action";
                    deleteButton.Name = "Delete";
                    deleteButton.Text = "Delete";
                    deleteButton.UseColumnTextForButtonValue = true;
                    deleteButton.Width = 100;
                    if (geoClanListgrid.Columns["Delete"] == null)
                    {
                        geoClanListgrid.Columns.Insert(geoClanListgrid.Columns.Count, deleteButton);
                    }
                }
            }

            if (GlobalsModel.TreeViewStructure.MainTagName == "Ward" || GlobalsModel.TreeViewStructure.MainTagName == "Location")
            {
                btnExportToCSV.Visible = true;
            }
            else
            {
                btnExportToCSV.Visible = false;
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
            else if (entity == "Clan")
            {
                var list = Globals.GeoList.ClanList.ToList();
                list.ForEach(r =>
                {
                    if (r.ClanId == data.ClanId)
                    {
                        r.ClanName = data.ClanName;
                        r.Description = data.Description;
                        r.Status = data.Status;
                    }
                });
                if (Globals.dtClan != null)
                    Globals.dtClan.Clear();
                Globals.GeoList.ClanList = list;
                Globals.dtClan = CommonFunction.ToDataTable(Globals.GeoList.ClanList.ToList());
            }

            Globals.GeoList.Version = Globals.GeoList.Version + 1;
            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");

            // GlobalsModel.mainMaster.InitTreeData(false);
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
            else if (entity == "Clan")
            {
                var list = Globals.GeoList.ClanList.ToList();
                list.Add(addedData);
                if (Globals.dtClan != null)
                    Globals.dtClan.Clear();
                Globals.GeoList.ClanList = list;
                Globals.dtClan = CommonFunction.ToDataTable(Globals.GeoList.ClanList.ToList());
            }

            Globals.GeoList.Version = Globals.GeoList.Version + 1;
            JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");

            //GlobalsModel.mainMaster.InitTreeData(false);
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
            var geoModel = geoClanListgrid.Rows[e.RowIndex].DataBoundItem;
            ClanModel clanmodel = geoModel as ClanModel;
            try
            {

                if (clanmodel.ClanName == "Unknown")
                    return;

                if (geoClanListgrid.Columns[e.ColumnIndex].Name == "Edit")
                {
                    Clan frmClan = new Clan(clanmodel);
                    if (frmClan.ShowDialog() == DialogResult.OK)
                    {
                        var updatedData = frmClan.ReturnClanModel;
                        RefereshGlobleAfterUpdate("Clan", updatedData);
                        GridViewSetting(new GeoTreeStructure() { MainTagName = "Ward", Id = clanmodel.WardId });
                    }

                }

                if (geoClanListgrid.Columns[e.ColumnIndex].Name == "Delete")
                {
                    DeleteClan(clanmodel);
                }


                geoClanListgrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                ErrorLog.Write("tvGeoStructureList", "geoStructureListgrid_CellContentClick", ex);
            }
        }

        private async void DeleteClan(ClanModel clanmodel)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure? like to delete " + clanmodel.ClanName + "?", "Delet Clan", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var response = GenericHttpClient.PostRequestCommon<int>("Clan/DeleteClan", clanmodel.ClanId);
                if (response.Result.Message != null)
                {
                    MessageBox.Show(response.Result.Message, "Delete Clan", MessageBoxButtons.OK);
                }

                var list = Globals.GeoList.ClanList.ToList();
                list.Remove(clanmodel);
                if (Globals.dtClan != null)
                {
                    Globals.dtClan.Clear();
                }
                Globals.GeoList.ClanList = list;
                Globals.dtClan = CommonFunction.ToDataTable(Globals.GeoList.ClanList.ToList());
                Globals.GeoList.Version = Globals.GeoList.Version + 1;
                JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");
                RefereshGridViewData();
            }
        }


        /// <summary>
        /// Handles the CellValueChanged event of the geoStructureListgrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void geoStructureListgrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (geoClanListgrid.Columns[e.ColumnIndex].Name == "Status" && e.RowIndex != -1)
            {
                var value = geoClanListgrid.Rows[e.RowIndex].Cells["Id"].Value;
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
        /// Handles the FormClosing event of the tvGeoStructureList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void tvGeoClanList_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }
        #endregion
        /// <summary>
        /// Handles the Click event of the btnAddClan control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddClan_Click(object sender, EventArgs e)
        {
            ClanAdd mdiChildUser = new ClanAdd();
            if (mdiChildUser.ShowDialog() == DialogResult.OK)
            {
                if (mdiChildUser.ReturnClanModel != null && mdiChildUser.ReturnClanModel.Count > 0)
                {
                    mdiChildUser.ReturnClanModel.Reverse();
                    foreach (ClanModel clanModel in mdiChildUser.ReturnClanModel)
                    {

                        var list = Globals.GeoList.ClanList.ToList();
                        list.Add(clanModel);
                        if (Globals.dtClan != null)
                            Globals.dtClan.Clear();
                        Globals.GeoList.ClanList = list;
                        Globals.dtClan = CommonFunction.ToDataTable(Globals.GeoList.ClanList.ToList());
                    }

                    Globals.GeoList.Version = Globals.GeoList.Version + 1;
                    JsonFileHelper.WriteJsonContent(Newtonsoft.Json.JsonConvert.SerializeObject(Globals.GeoList), "geolist");

                    MessageBox.Show("Clans added successfully", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                GridViewSetting(TreeStructure);

            }
        }

        private void btnExportToCSV_Click(object sender, EventArgs e)
        {
            try
            {
                string exportReportName = string.Empty;
                string messageBoxText = string.Empty;

                exportReportName = $"{GlobalsModel.LoggedUser.Server}-{GlobalsModel.LoggedUser.Database}-{GlobalsModel.TreeViewStructure.Name}-{"ClanList"}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";


                //geoClanListgrid.DataSource

                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = exportReportName;
                saveFileDialog.Filter = "CSV (*.csv)|*.csv";
                saveFileDialog.DefaultExt = ".csv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    #region write
                    StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false);
                    //headers    
                    sw.Write(string.Format("{0},{1},{2},{3},{4},{5}", "District Name", "LLG Name", "Ward Number", "Ward Name", "Clan Name", "Status"));
                    sw.Write(sw.NewLine);
                    //data
                    foreach (ClanModel objModel in geoClanListgrid.DataSource as List<ClanModel>)
                    {
                        sw.Write(string.Format("{0},{1},{2},{3},{4},{5}",
                            Globals.GeoList.DistrictList.Where(f => f.DistrictId == objModel.DistrictId).FirstOrDefault().DistrictName,
                            Globals.GeoList.LLGList.Where(f => f.LLGId == objModel.LLGId).FirstOrDefault().LLGName,
                            Globals.GeoList.WardList.Where(f => f.WardId == objModel.WardId).FirstOrDefault().WardNumber,
                            Globals.GeoList.WardList.Where(f => f.WardId == objModel.WardId).FirstOrDefault().WardName,
                            objModel.ClanName,
                            objModel.Status == true ? "Active" : "Inactive"
                           ));

                        sw.Write(sw.NewLine);
                    }
                    sw.Close();
                    #endregion

                    messageBoxText = $"{TreeStructure.MainTagName} {"Clans"} CSV is exported sucessfully at {saveFileDialog.FileName}";
                    MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else
                //{
                //    MessageBox.Show("No record founds.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                GlobalsModel.mainMaster.HideLoader();
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ErrorLog.Write("File", "btnExportToCSV_Click", ex);
            }
            finally
            {
            }
        }
    }
}