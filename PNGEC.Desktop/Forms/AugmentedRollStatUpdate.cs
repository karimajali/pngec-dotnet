using PNGEC.API.Domain.AugmentedRollStat;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class AugmentedRollStatUpdate : Form
    {
        #region variable declaration
        /// <summary>
        /// The augmented roll stat model
        /// </summary>
        internal AugmentedRollStatModel augmentedRollStatModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AugmentedRollStatUpdate"/> class.
        /// </summary>
        /// <param name="augmentedRollStatModel">The augmented roll stat model.</param>
        public AugmentedRollStatUpdate(AugmentedRollStatModel augmentedRollStatModel)
        {
            InitializeComponent();

            this.augmentedRollStatModel = augmentedRollStatModel;
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the AugmentedRollStatUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AugmentedRollStatUpdate_Load(object sender, EventArgs e)
        {
            this.BindDataAugmentedRollStat();
        }

        /// <summary>
        /// Handles the KeyPress event of the txtOld control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtOld_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtUpdated control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtUpdated_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtTransferIn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtTransferIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtTransferOut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtTransferOut_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtDeceased control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtDeceased_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtDuplicte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtDuplicte_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtGhost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtGhost_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                augmentedRollStatModel = new AugmentedRollStatModel();

                augmentedRollStatModel.RegionId = Convert.ToInt32(txtRegionId.Text);
                augmentedRollStatModel.ProvinceId = Convert.ToInt32(txtProvinceId.Text);
                augmentedRollStatModel.DistrictId = Convert.ToInt32(txtDistrictId.Text);
                augmentedRollStatModel.LLGId = Convert.ToInt32(txtLLGId.Text);
                augmentedRollStatModel.WardId = Convert.ToInt32(txtWardId.Text);
                augmentedRollStatModel.LocationId = Convert.ToInt32(txtLocationId.Text);

                augmentedRollStatModel.OLD = Convert.ToInt32(txtOld.Text);
                augmentedRollStatModel.NEW = Convert.ToInt32(txtNew.Text);
                augmentedRollStatModel.UPDATED = Convert.ToInt32(txtUpdated.Text);
                augmentedRollStatModel.TRANSFERIN = Convert.ToInt32(txtTransferIn.Text);
                augmentedRollStatModel.TRANSFEROUT = Convert.ToInt32(txtTransferOut.Text);
                augmentedRollStatModel.DECEASED = Convert.ToInt32(txtDeceased.Text);
                augmentedRollStatModel.DUPLICATE = Convert.ToInt32(txtDuplicte.Text);
                augmentedRollStatModel.GHOST = Convert.ToInt32(txtGhost.Text);
                augmentedRollStatModel.UNDERAGE = Convert.ToInt32(txtUnderAge.Text);

                this.AddUpdateAugmentedRollStat(augmentedRollStatModel);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        #endregion

        #region Methos

        #region Numeric Allow
        /// <summary>
        /// Numerics the allow.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void NumericAllow(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #endregion

        #region Bind Data AugmentedRollStat
        /// <summary>
        /// Binds the data augmented roll stat.
        /// </summary>
        private void BindDataAugmentedRollStat()
        {
            txtRegionId.Text = augmentedRollStatModel.RegionId.ToString();
            txtProvinceId.Text = augmentedRollStatModel.ProvinceId.ToString();
            txtDistrictId.Text = augmentedRollStatModel.DistrictId.ToString();
            txtLLGId.Text = augmentedRollStatModel.LLGId.ToString();
            txtWardId.Text = augmentedRollStatModel.WardId.ToString();
            txtLocationId.Text = augmentedRollStatModel.LocationId.ToString();

            txtOld.Text = augmentedRollStatModel.OLD.ToString();
            txtNew.Text = augmentedRollStatModel.NEW.ToString();
            txtUpdated.Text = augmentedRollStatModel.UPDATED.ToString();
            txtTransferIn.Text = augmentedRollStatModel.TRANSFERIN.ToString();
            txtTransferOut.Text = augmentedRollStatModel.TRANSFEROUT.ToString();
            txtDeceased.Text = augmentedRollStatModel.DECEASED.ToString();
            txtDuplicte.Text = augmentedRollStatModel.DUPLICATE.ToString();
            txtGhost.Text = augmentedRollStatModel.GHOST.ToString();
            txtUnderAge.Text = augmentedRollStatModel.UNDERAGE.ToString();
        }
        #endregion

        #region Add Update Augmented Roll Stat
        /// <summary>
        /// Adds the update augmented roll stat.
        /// </summary>
        /// <param name="augmentedRollStatModel">The augmented roll stat model.</param>
        /// <exception cref="NotImplementedException"></exception>
        private async void AddUpdateAugmentedRollStat(AugmentedRollStatModel model)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to Update the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                // DatabaseName
                augmentedRollStatModel.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

                var response = await GenericHttpClient.PostRequestCommon<AugmentedRollStatModel>("AugmentedRollStat/AddUpdateAugmentedRollStat", model);
                if (response.Data != null && response.Status)
                {
                    augmentedRollStatModel = new AugmentedRollStatModel();
                    augmentedRollStatModel = response.Data;

                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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
            if (string.IsNullOrWhiteSpace(txtOld.Text.Trim()))
            {
                MessageBox.Show("OLD can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOld.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNew.Text.Trim()))
            {
                MessageBox.Show("NEW can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNew.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUpdated.Text.Trim()))
            {
                MessageBox.Show("UPDATED can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUpdated.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTransferIn.Text.Trim()))
            {
                MessageBox.Show("TRANSFERIN can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTransferIn.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTransferOut.Text.Trim()))
            {
                MessageBox.Show("TRANSFEROUT can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTransferOut.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDeceased.Text.Trim()))
            {
                MessageBox.Show("DECEASED can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDeceased.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDuplicte.Text.Trim()))
            {
                MessageBox.Show("DUPLICTE can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuplicte.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGhost.Text.Trim()))
            {
                MessageBox.Show("GHOST can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGhost.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUnderAge.Text.Trim()))
            {
                MessageBox.Show("UNDERAGE can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnderAge.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        /// <summary>
        /// Handles the KeyPress event of the txtUnderAge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtUnderAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.NumericAllow(sender, e);
        }
    }
}
