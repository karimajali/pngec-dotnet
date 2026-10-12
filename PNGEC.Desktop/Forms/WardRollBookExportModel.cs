using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.Text;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class WardRollBookExportModel : Form
    {
        #region variable
        /// <summary>
        /// The file
        /// </summary>
        File file = null;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is export to CSV.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is export to CSV; otherwise, <c>false</c>.
        /// </value>
        public bool IsExportToCsv { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WardRollBookExportModel"/> class.
        /// </summary>
        public WardRollBookExportModel()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the WardRollBookExportModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WardRollBookExportModel_Load(object sender, EventArgs e)
        {
            dtpFromDate.MaxDate = DateTime.Now.Date;
            dtpToDate.MaxDate = DateTime.Now.Date;

            if (GlobalsModel.TreeViewStructure.MainTagName == "Location")
            {
                rdbWardRollBook.Text = "Location Roll Book";
                rdbWardRollBook.Tag = "Location Roll Book";

                rdbWardPhotoRollBook.Text = "Location Photo Roll Book";
                rdbWardPhotoRollBook.Tag = "Location Photo Roll Book";

                rdbVoterIdCard.Text = "Location Voter Id Card";
                rdbVoterIdCard.Tag = "Location Voter Id Card";

                rdbVoterPhotoCard.Text = "Location Voter Photo Card";
                rdbVoterPhotoCard.Tag = "Location Voter Photo Card";

                rdbVoterEnvelope.Text = "Location Roll Envelope";
                rdbVoterEnvelope.Tag = "Location Roll Envelope";
                chkWardRollEnvelope.Text = "Location Roll Envelope";
                chkWardRollEnvelope.Tag = "Location Roll Envelope";

                rdbWardRollDeletion.Text = "Location Roll Deletion";
                rdbWardRollDeletion.Tag = "Location Roll Deletion";

                //rdbPreFieldWorkSummary.Visible = false;
                rdbPreFieldWorkSummary.Text = "Location Pre-Field Work Summary";
                rdbPreFieldWorkSummary.Tag = "Location Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Text = "Location Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Tag = "Location Pre-Field Work Summary";

                rdbWardClanRoll.Visible = false;
            }
            else
            {
                rdbWardClanRoll.Visible = true;

                rdbWardRollBook.Text = "Ward Roll Book";
                rdbWardRollBook.Tag = "Ward Roll Book";

                rdbWardPhotoRollBook.Text = "Ward Photo Roll Book";
                rdbWardPhotoRollBook.Tag = "Ward Photo Roll Book";

                rdbVoterIdCard.Text = "Ward Voter Id Card";
                rdbVoterIdCard.Tag = "Ward Voter Id Card";

                rdbVoterPhotoCard.Text = "Ward Voter Photo Card";
                rdbVoterPhotoCard.Tag = "Ward Voter Photo Card";

                rdbVoterEnvelope.Text = "Ward Roll Envelope";
                rdbVoterEnvelope.Tag = "Ward Roll Envelope";
                chkWardRollEnvelope.Text = "Ward Roll Envelope";
                chkWardRollEnvelope.Tag = "Ward Roll Envelope";

                rdbWardRollDeletion.Text = "Ward Roll Deletion";
                rdbWardRollDeletion.Tag = "Ward Roll Deletion";

                //rdbPreFieldWorkSummary.Visible = true;
                rdbPreFieldWorkSummary.Text = "Ward Pre-Field Work Summary";
                rdbPreFieldWorkSummary.Tag = "Ward Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Text = "Ward Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Tag = "Ward Pre-Field Work Summary";
            }

            ManageFilterPanel();
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                chkWardRollBookLandscap.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            bool isValidForm = true;

            // Date Range
            if (rdbDateRange.Checked == true && (dtpFromDate.Value != null && dtpToDate.Value != null) && (dtpFromDate.Value > dtpToDate.Value))
            {
                isValidForm = false;
                MessageBox.Show("The To date must be greater than the From date.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Alpha Split
            if (rdbAlphaSplit.Checked == true && string.IsNullOrWhiteSpace(txtAlphaSplitStart.Text))
            {
                isValidForm = false;
                MessageBox.Show("Please enter valid start alphabet.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdbAlphaSplit.Checked == true && string.IsNullOrWhiteSpace(txtAlphaSplitEnd.Text))
            {
                isValidForm = false;
                MessageBox.Show("Please enter valid end alphabet.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdbAlphaSplit.Checked == true && !string.IsNullOrWhiteSpace(txtAlphaSplitStart.Text) && !string.IsNullOrWhiteSpace(txtAlphaSplitEnd.Text))
            {
                byte[] asciiBytesStart = Encoding.ASCII.GetBytes(txtAlphaSplitStart.Text);
                byte[] asciiBytesEnd = Encoding.ASCII.GetBytes(txtAlphaSplitEnd.Text);

                if (asciiBytesStart[0] > asciiBytesEnd[0])
                {
                    isValidForm = false;
                    MessageBox.Show("Please enter valid alpha split range.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (isValidForm)
            {
                IsExportToCsv = false;
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExportCsv control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            IsExportToCsv = true;
            this.DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// Handles the CheckedChanged event of the rdbWardClanRoll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbWardClanRoll_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbWardRollBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbWardRollBook_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
            if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
            {
                chkWardRollBookLandscap.Visible = true;
            }           
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbWardPhotoRollBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbWardPhotoRollBook_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbVoterIdCard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbVoterIdCard_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbVoterPhotoCard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbVoterPhotoCard_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbWardRollDeletion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbWardRollDeletion_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbPreFieldWorkSummary control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbPreFieldWorkSummary_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbExisitngVoterDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbExisitngVoterDetails_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbDateAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbDateAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = false;
            dtpToDate.Enabled = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbDateRange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbDateRange_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = true;
            dtpToDate.Enabled = true;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbElectorByIDsAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbElectorByIDsAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbExisitngVoterDetails.Checked == true)
            {
                // Gender
                rdbGenderAll.Checked = true;
                gbGender.Enabled = false;

                // Disability
                rdbDisabilityAll.Checked = true;
                gbDisability.Enabled = false;

                // Photo
                rdbPhotoAll.Checked = true;
                gbPhoto.Enabled = false;

                // Date
                rdbDateAll.Checked = true;
                gbDateRange.Enabled = false;

                // ExportCsv
                btnExportCsv.Visible = false;
            }
            else
            {
                // Gender
                rdbGenderAll.Checked = true;
                gbGender.Enabled = true;

                // Disability
                rdbDisabilityAll.Checked = true;
                gbDisability.Enabled = true;

                // Photo
                rdbPhotoAll.Checked = true;
                gbPhoto.Enabled = true;

                // Date
                rdbDateAll.Checked = true;
                gbDateRange.Enabled = true;

                // ElectorByIDsAll
                //rdbElectorByIDsAll.Checked = true;
                //gbElectorByIDs.Enabled = true;

                // ExportCsv
                btnExportCsv.Visible = false;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbElectorByIDsSelected control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbElectorByIDsSelected_CheckedChanged(object sender, EventArgs e)
        {
            // Gender
            rdbGenderAll.Checked = true;
            gbGender.Enabled = false;

            // Disability
            rdbDisabilityAll.Checked = true;
            gbDisability.Enabled = false;

            // Photo
            rdbPhotoAll.Checked = true;
            gbPhoto.Enabled = false;

            // Date
            rdbDateAll.Checked = true;
            gbDateRange.Enabled = false;

            // ElectorByIDsAll
            //rdbElectorByIDsSelected.Checked = true;
            //gbElectorByIDs.Enabled = true;

            // ExportCsv
            btnExportCsv.Visible = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbAlphaSplitAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbAlphaSplitAll_CheckedChanged(object sender, EventArgs e)
        {
            txtAlphaSplitStart.Enabled = false;
            txtAlphaSplitEnd.Enabled = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rdbAlphaSplit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbAlphaSplit_CheckedChanged(object sender, EventArgs e)
        {
            txtAlphaSplitStart.Enabled = true;
            txtAlphaSplitEnd.Enabled = true;
        }
        #endregion

        #region Methods

        #region ManageFilterPanel
        /// <summary>
        /// Manages the filter panel.
        /// </summary>
        private void ManageFilterPanel()
        {
            chkWardRollBookLandscap.Visible = false;
            if (rdbWardClanRoll.Checked == true || rdbWardRollBook.Checked == true || rdbVoterIdCard.Checked == true || rdbWardRollDeletion.Checked == true)
            {
                // Gender
                rdbGenderAll.Checked = true;
                gbGender.Enabled = true;

                // Disability
                rdbDisabilityAll.Checked = true;
                gbDisability.Enabled = true;

                // Photo
                rdbPhotoAll.Checked = true;
                gbPhoto.Enabled = false;

                // Date
                rdbDateAll.Checked = true;
                gbDateRange.Enabled = false;

                // ElectorByIDsAll
                rdbElectorByIDsAll.Checked = true;
                gbElectorByIDs.Enabled = false;

                // ExportCsv
                if (rdbWardRollBook.Checked == true || rdbWardClanRoll.Checked == true)
                {
                    btnExportCsv.Visible = true;

                    //AlphaSplit
                    rdbAlphaSplitAll.Checked = true;
                    gbAlphaSplit.Enabled = true;
                }
                else
                {
                    btnExportCsv.Visible = false;

                    //AlphaSplit
                    rdbAlphaSplitAll.Checked = true;
                    gbAlphaSplit.Enabled = false;
                }
            }
            else if (rdbWardPhotoRollBook.Checked == true || rdbVoterPhotoCard.Checked == true)
            {
                // Gender
                rdbGenderAll.Checked = true;
                gbGender.Enabled = true;

                // Disability
                rdbDisabilityAll.Checked = true;
                gbDisability.Enabled = true;

                // Photo
                rdbPhotoAll.Checked = true;
                gbPhoto.Enabled = true;

                // Date
                rdbDateAll.Checked = true;
                gbDateRange.Enabled = true;

                // ElectorByIDsAll
                rdbElectorByIDsAll.Checked = true;
                gbElectorByIDs.Enabled = true;

                // Date Range & ElectorByIDsAll
                if (rdbVoterPhotoCard.Checked == true)
                {
                    // Date
                    rdbDateAll.Checked = true;
                    gbDateRange.Enabled = true;

                    // ElectorByIDsAll
                    rdbElectorByIDsAll.Checked = true;
                    gbElectorByIDs.Enabled = true;

                    //AlphaSplit
                    rdbAlphaSplitAll.Checked = true;
                    gbAlphaSplit.Enabled = false;
                }
                else
                {
                    // Date
                    rdbDateAll.Checked = true;
                    gbDateRange.Enabled = false;

                    // ElectorByIDsAll
                    rdbElectorByIDsAll.Checked = true;
                    gbElectorByIDs.Enabled = false;

                    //AlphaSplit
                    rdbAlphaSplitAll.Checked = true;
                    gbAlphaSplit.Enabled = true;
                }

                // ExportCsv
                btnExportCsv.Visible = false;
            }
            else if (rdbExisitngVoterDetails.Checked == true)
            {
                // Gender
                rdbGenderAll.Checked = true;
                gbGender.Enabled = false;

                // Disability
                rdbDisabilityAll.Checked = true;
                gbDisability.Enabled = false;

                // Photo
                rdbPhotoAll.Checked = true;
                gbPhoto.Enabled = false;

                // Date
                rdbDateAll.Checked = true;
                gbDateRange.Enabled = false;

                // ElectorByIDsAll
                rdbElectorByIDsAll.Checked = true;
                gbElectorByIDs.Enabled = true;

                //AlphaSplit
                rdbAlphaSplitAll.Checked = true;
                gbAlphaSplit.Enabled = false;

                // ExportCsv
                btnExportCsv.Visible = false;
            }
            else
            {
                // Gender
                rdbGenderAll.Checked = true;
                gbGender.Enabled = false;

                // Disability
                rdbDisabilityAll.Checked = true;
                gbDisability.Enabled = false;

                // Photo
                rdbPhotoAll.Checked = true;
                gbPhoto.Enabled = false;

                // Date
                rdbDateAll.Checked = true;
                gbDateRange.Enabled = false;

                // ElectorByIDsAll
                rdbElectorByIDsAll.Checked = true;
                gbElectorByIDs.Enabled = false;

                //AlphaSplit
                rdbAlphaSplitAll.Checked = true;
                gbAlphaSplit.Enabled = false;

                // ExportCsv
                btnExportCsv.Visible = false;
            }

            // Include Possible Deletion Report checkbox 
            if (rdbWardRollBook.Checked == true || rdbWardPhotoRollBook.Checked == true || rdbWardClanRoll.Checked == true)
            {
                chkIncludePossibleDeletionReport.Enabled = true;
                chkWardRollEnvelope.Enabled = true;
                chkWardPreFieldWorkSummary.Enabled = true;

                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                {
                    chkIncludePossibleDeletionReport.Checked = false;
                    chkIncludePossibleDeletionReport.Enabled = false;
                }

                //if(rdbWardRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                //{
                //    txtReportTitle.Enabled = true;
                //    txtReportTitle.Text = "ELECTORAL ROLL 2022";
                //}
                //else if (rdbWardPhotoRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                //{
                //    txtReportTitle.Enabled = true;
                //    txtReportTitle.Text = "ELECTORAL ROLL 2022";
                //}
                //else
                //{
                //    txtReportTitle.Enabled = false;
                //    txtReportTitle.Text = string.Empty;
                //}

                if (rdbWardClanRoll.Checked == true || rdbWardRollBook.Checked == true || rdbWardPhotoRollBook.Checked == true)
                {
                    txtReportTitle.Enabled = true;
                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        txtReportTitle.Text = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        txtReportTitle.Text = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    {
                        txtReportTitle.Text = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                    }

                }
                else
                {
                    txtReportTitle.Enabled = false;
                    txtReportTitle.Text = string.Empty;
                }
            }
            else
            {
                chkIncludePossibleDeletionReport.Enabled = false;
                chkWardRollEnvelope.Enabled = false;
                chkWardPreFieldWorkSummary.Enabled = false;
                txtReportTitle.Enabled = false;
                txtReportTitle.Text = string.Empty;
            }
        }
        #endregion

        #endregion

    }
}
