using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PNGEC.API.Domain.Mobile.Voter;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class TransferVoter : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The dt
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// The voters model
        /// </summary>
        private List<VoterModel> votersModel = null;

        /// <summary>
        /// The LST same location voters
        /// </summary>
        private List<VoterModel> lstSameLocationVoters = null;

        /// <summary>
        /// The LST same ward voters
        /// </summary>
        private List<VoterModel> lstSameWardVoters = null;

        /// <summary>
        /// The LST different ward voters
        /// </summary>
        private List<VoterModel> lstDifferentWardVoters = null;

        /// <summary>
        /// The not synced and transfer voter list
        /// </summary>
        private List<VoterModel> notSyncedAndTransferVoterList = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferVoter"/> class.
        /// </summary>
        public TransferVoter(List<VoterModel> votersModel, string pageName)
        {
            InitializeComponent();
            this.votersModel = votersModel;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Transfer Voter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TransferVoter_Load(object sender, EventArgs e)
        {
            this.BindRegionName();

            cmbRegionName.SelectedValue = GlobalsModel.LoggedUser.User.Region;
            cmbProvinceName.SelectedValue = GlobalsModel.LoggedUser.User.Province;
            cmbDistrictName.SelectedValue = GlobalsModel.LoggedUser.User.District;
            cmbLLGName.SelectedValue = GlobalsModel.LoggedUser.User.LLG;
            cmbWardName.SelectedValue = GlobalsModel.LoggedUser.User.Ward;
            cmbLocationName.SelectedValue = GlobalsModel.LoggedUser.User.Location;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRegion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbRegionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegionName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbRegionName.SelectedValue);
                var list = Globals.GeoList.ProvinceList.Where(x => x.Status && x.RegionId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Province.ProvinceModel { ProvinceId = 0, ProvinceName = "Select Province" });
                cmbProvinceName.DataSource = list;
                cmbProvinceName.ValueMember = "ProvinceId";
                cmbProvinceName.DisplayMember = "ProvinceName";
            }
            else
            {
                cmbProvinceName.SelectedIndex = -1;
                cmbProvinceName.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProvince control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProvinceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvinceName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbProvinceName.SelectedValue);
                var list = Globals.GeoList.DistrictList.Where(x => x.Status && x.ProvinceId == selectedValue).ToList();
                list.Insert(0, new API.Domain.District.DistrictModel { DistrictId = 0, DistrictName = "Select District" });
                cmbDistrictName.DataSource = list;
                cmbDistrictName.ValueMember = "DistrictId";
                cmbDistrictName.DisplayMember = "DistrictName";
            }
            else
            {
                cmbDistrictName.SelectedIndex = -1;
                cmbDistrictName.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDistrict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbDistrictName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDistrictName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbDistrictName.SelectedValue);
                var list = Globals.GeoList.LLGList.Where(x => x.Status && x.DistrictId == selectedValue).ToList();
                list.Insert(0, new API.Domain.LLG.LLGModel { LLGId = 0, LLGName = "Select LLG" });
                cmbLLGName.DataSource = list;
                cmbLLGName.ValueMember = "LLGId";
                cmbLLGName.DisplayMember = "LLGName";
            }
            else
            {
                cmbLLGName.SelectedIndex = -1;
                cmbLLGName.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbLLG control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbLLGName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLLGName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbLLGName.SelectedValue);
                var list = Globals.GeoList.WardList.Where(x => x.Status && x.LLGId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Ward.WardModel { WardId = 0, WardName = "Select Ward", WardNoWardName = "Select Ward" });
                cmbWardName.DataSource = list;
                cmbWardName.ValueMember = "WardId";
                cmbWardName.DisplayMember = "WardNoWardName";
            }
            else
            {
                cmbWardName.SelectedIndex = -1;
                cmbWardName.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbWardName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbWardName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWardName.SelectedIndex > 0)
            {
                var selectedValue = Convert.ToInt32(cmbWardName.SelectedValue);
                var list = Globals.GeoList.LocationList.Where(x => x.Status && x.WardId == selectedValue).ToList();
                list.Insert(0, new API.Domain.Location.LocationModel { LocationId = 0, LocationName = "Select Location" });
                cmbLocationName.DataSource = list;
                cmbLocationName.ValueMember = "LocationId";
                cmbLocationName.DisplayMember = "LocationName";
            }
            else
            {
                cmbLocationName.SelectedIndex = -1;
                cmbLocationName.DataSource = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                notSyncedAndTransferVoterList = new List<VoterModel>();

                if (IsValidForm())
                {
                    GlobalsModel.mainMaster.ShowLoader();

                    lstSameLocationVoters = new List<VoterModel>();
                    lstSameWardVoters = new List<VoterModel>();
                    lstDifferentWardVoters = new List<VoterModel>();

                    lstSameLocationVoters = this.votersModel
                        .Where(x => x.RegionId == (int)cmbRegionName.SelectedValue &&
                        x.ProvinceId == (int)cmbProvinceName.SelectedValue &&
                        x.DistrictId == (int)cmbDistrictName.SelectedValue &&
                        x.LLGId == (int)cmbLLGName.SelectedValue &&
                        x.WardId == (int)cmbWardName.SelectedValue &&
                        x.LocationId == (int)cmbLocationName.SelectedValue).ToList();

                    lstSameWardVoters = this.votersModel
                        .Where(x => x.RegionId == (int)cmbRegionName.SelectedValue &&
                        x.ProvinceId == (int)cmbProvinceName.SelectedValue &&
                        x.DistrictId == (int)cmbDistrictName.SelectedValue &&
                        x.LLGId == (int)cmbLLGName.SelectedValue &&
                        x.WardId == (int)cmbWardName.SelectedValue &&
                        x.LocationId != (int)cmbLocationName.SelectedValue).ToList();

                    lstDifferentWardVoters = this.votersModel
                        .Where(x => (x.WardId != (int)cmbWardName.SelectedValue && x.LocationId != (int)cmbLocationName.SelectedValue)).ToList();

                    // if db is final then update 
                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        lstDifferentWardVoters = votersModel;

                        if (lstDifferentWardVoters != null && lstDifferentWardVoters.Count > 0)
                        {
                            foreach (var item in lstDifferentWardVoters)
                            {
                                item.TransferOutRegionId = item.RegionId;
                                item.TransferOutProvinceId = item.ProvinceId;
                                item.TransferOutDistrictId = item.DistrictId;
                                item.TransferOutLLGId = item.LLGId;
                                item.TransferOutWardId = item.WardId;
                                item.TransferOutLocationId = item.LocationId;
                                item.LocationId = (int)cmbLocationName.SelectedValue;
                                item.Database = Convert.ToString(GlobalsModel.LoggedUser.Database);
                                item.RegionId = (int)cmbRegionName.SelectedValue;
                                item.ProvinceId = (int)cmbProvinceName.SelectedValue;
                                item.DistrictId = (int)cmbDistrictName.SelectedValue;
                                item.LLGId = (int)cmbLLGName.SelectedValue;
                                item.WardId = (int)cmbWardName.SelectedValue;
                                item.LocationId = (int)cmbLocationName.SelectedValue;

                            }

                            Response responseTransferVoter = await GenericHttpClient.PostRequest("mobile/Voter/TransferVoterByRO", lstDifferentWardVoters);
                            if (responseTransferVoter.Status)
                            {
                                if (responseTransferVoter != null && responseTransferVoter.Data != null)
                                {
                                    foreach (VoterModel item in (List<VoterModel>)responseTransferVoter.Data)
                                    {
                                        notSyncedAndTransferVoterList.Add(item);
                                    }
                                }
                            }
                        }

                    }
                    else
                    {

                        if (lstSameWardVoters != null && lstSameWardVoters.Count > 0)
                        {
                            foreach (var item in lstSameWardVoters)
                            {
                                item.TransferOutRegionId = item.RegionId;
                                item.TransferOutProvinceId = item.ProvinceId;
                                item.TransferOutDistrictId = item.DistrictId;
                                item.TransferOutLLGId = item.LLGId;
                                item.TransferOutWardId = item.WardId;
                                item.TransferOutLocationId = item.LocationId;
                                item.RegionId = (int)cmbRegionName.SelectedValue;
                                item.ProvinceId = (int)cmbProvinceName.SelectedValue;
                                item.DistrictId = (int)cmbDistrictName.SelectedValue;
                                item.LLGId = (int)cmbLLGName.SelectedValue;
                                item.WardId = (int)cmbWardName.SelectedValue;
                                item.LocationId = (int)cmbLocationName.SelectedValue;

                            }

                            Response responseTransferVoter = await GenericHttpClient.PostRequest("mobile/Voter/TransferVoterByRO", lstSameWardVoters);
                            if (responseTransferVoter.Status)
                            {
                                if (responseTransferVoter != null && responseTransferVoter.Data != null)
                                {
                                    foreach (VoterModel item in (List<VoterModel>)responseTransferVoter.Data)
                                    {
                                        notSyncedAndTransferVoterList.Add(item);
                                    }
                                }
                            }
                        }

                        if (lstDifferentWardVoters != null && lstDifferentWardVoters.Count > 0)
                        {
                            foreach (var item in lstDifferentWardVoters)
                            {
                                item.TransferOutRegionId = item.RegionId;
                                item.TransferOutProvinceId = item.ProvinceId;
                                item.TransferOutDistrictId = item.DistrictId;
                                item.TransferOutLLGId = item.LLGId;
                                item.TransferOutWardId = item.WardId;
                                item.TransferOutLocationId = item.LocationId;

                                item.RegionId = (int)cmbRegionName.SelectedValue;
                                item.ProvinceId = (int)cmbProvinceName.SelectedValue;
                                item.DistrictId = (int)cmbDistrictName.SelectedValue;
                                item.LLGId = (int)cmbLLGName.SelectedValue;
                                item.WardId = (int)cmbWardName.SelectedValue;
                                item.LocationId = (int)cmbLocationName.SelectedValue;

                                item.Status = "TRANSFERRED IN";
                            }

                            Response responseSyncElectors = await GenericHttpClient.PostRequest("mobile/Voter/SyncElectors", lstDifferentWardVoters);
                            if (responseSyncElectors.Status)
                            {
                                if (responseSyncElectors != null && responseSyncElectors.Data != null)
                                {
                                    foreach (VoterModel item in (List<VoterModel>)responseSyncElectors.Data)
                                    {
                                        notSyncedAndTransferVoterList.Add(item);
                                    }
                                }
                            }
                        }
                    }

                    if ((lstSameLocationVoters != null && lstSameLocationVoters.Count > 0) && (this.votersModel.Count == lstSameLocationVoters.Count))
                    {
                        string SameLocationVotersElectorIds = string.Join(",", lstSameLocationVoters.Select(x => x.ElectorId.ToString()));
                        MessageBox.Show($"The new location you have selected is the same as that of the current location of the voter. Please reselect required location in this records {SameLocationVotersElectorIds}", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if ((notSyncedAndTransferVoterList != null && notSyncedAndTransferVoterList.Count > 0) && (this.votersModel.Count == notSyncedAndTransferVoterList.Count))
                    {
                        string SyncedAndTransferVoterElectorIds = string.Join(",", notSyncedAndTransferVoterList.Select(x => x.ElectorId.ToString()));
                        MessageBox.Show($"This {SyncedAndTransferVoterElectorIds} are record(s) are not Transfer.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if ((lstSameLocationVoters != null && lstSameLocationVoters.Count > 0) && (notSyncedAndTransferVoterList != null && notSyncedAndTransferVoterList.Count > 0))
                    {
                        string SameLocationVotersElectorIds = string.Join(",", lstSameLocationVoters.Select(x => x.ElectorId.ToString()));
                        string SyncedAndTransferVoterElectorIds = string.Join(",", notSyncedAndTransferVoterList.Select(x => x.ElectorId.ToString()));
                        string Message = string.Format("{0}" + Environment.NewLine + "{1}", $"The new location you have selected is the same as that of the current location of the voter. Please reselect required location in this records {SameLocationVotersElectorIds}", $"This {SyncedAndTransferVoterElectorIds} are record(s) are not Transfer.");
                        MessageBox.Show(Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if ((lstSameLocationVoters != null && lstSameLocationVoters.Count > 0) && (notSyncedAndTransferVoterList != null && notSyncedAndTransferVoterList.Count == 0))
                    {
                        string SameLocationVotersElectorIds = string.Join(",", lstSameLocationVoters.Select(x => x.ElectorId.ToString()));
                        string Message = string.Format("{0}" + Environment.NewLine + "{1}", $"The new location you have selected is the same as that of the current location of the voter. Please reselect required location in this records {SameLocationVotersElectorIds}", string.Format(ResponseMessage.RecordUpdateSuccess, "Transfer Voter By RO"));
                        MessageBox.Show(Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if ((lstSameLocationVoters == null || lstSameLocationVoters.Count == 0) && (notSyncedAndTransferVoterList == null || notSyncedAndTransferVoterList.Count == 0))
                    {
                        MessageBox.Show(string.Format(ResponseMessage.RecordUpdateSuccess, "Transfer Voter By RO"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.ErrorMessage, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
            finally
            {
                GlobalsModel.mainMaster.HideLoader();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Methods

        #region BindRegionName
        /// <summary>
        /// Binds the name of the region.
        /// </summary>
        private void BindRegionName()
        {
            var list = Globals.GeoList.RegionList.Where(x => x.Status).ToList();
            list.Insert(0, new API.Domain.Region.RegionModel { RegionId = 0, RegionName = "Select Region" });
            cmbRegionName.DataSource = list;
            cmbRegionName.ValueMember = "RegionId";
            cmbRegionName.DisplayMember = "RegionName";
        }

        #endregion

        #region IsValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsValidForm()
        {
            if (Convert.ToInt32(cmbRegionName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Region Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbProvinceName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Province Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbDistrictName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select District Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbLLGName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select LLG Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbWardName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Ward Name", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Convert.ToInt32(cmbLocationName.SelectedIndex) <= 0)
            {
                MessageBox.Show("Please select Location Name.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }


            return true;
        }
        #endregion

        #endregion
    }
}
