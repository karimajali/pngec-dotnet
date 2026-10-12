using System;
using System.Text;
using System.Windows.Forms;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Forms
{
    public partial class MKAWardRollBookExportModel : Form
    {
       
        #region variable
        /// <summary>
        /// The file
        /// </summary>
        MKAFile mkaFile = null;

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
        /// Initializes a new instance of the <see cref="MKAWardRollBookExportModel"/> class.
        /// </summary>
        public MKAWardRollBookExportModel()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the MKAWardRollBookExportModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>       
        private void MKAWardRollBookExportModel_Load(object sender, EventArgs e)
        {
            dtpFromDate.MaxDate = DateTime.Now.Date;
            dtpToDate.MaxDate = DateTime.Now.Date;

            if (GlobalsModel.MKATreeViewStructure.MainTagName == "MKALocation")
            {
                rdbMKAWardRollBook.Text = "MKA Location Roll Book";
                rdbMKAWardRollBook.Tag = "MKA Location Roll Book";

                rdbWardPhotoRollBook.Text = "MKA Location Photo Roll Book";
                rdbWardPhotoRollBook.Tag = "MKA Location Photo Roll Book";

                rdbVoterIdCard.Text = "MKA Location Voter Id Card";
                rdbVoterIdCard.Tag = "MKA Location Voter Id Card";

                rdbVoterPhotoCard.Text = "MKA Location Voter Photo Card";
                rdbVoterPhotoCard.Tag = "MKA Location Voter Photo Card";

                rdbVoterEnvelope.Text = "MKA Location Roll Envelope";
                rdbVoterEnvelope.Tag = "MKA Location Roll Envelope";
                chkWardRollEnvelope.Text = "MKA Location Roll Envelope";
                chkWardRollEnvelope.Tag = "MKA Location Roll Envelope";

                rdbWardRollDeletion.Text = "MKA Location Roll Deletion";
                rdbWardRollDeletion.Tag = "MKA Location Roll Deletion";

                rdbPreFieldWorkSummary.Visible = false;
                rdbPreFieldWorkSummary.Text = "Location Pre-Field Work Summary";
                rdbPreFieldWorkSummary.Tag = "Location Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Text = "Location Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Tag = "Location Pre-Field Work Summary";
            }
            else
            {
                rdbMKAWardRollBook.Text = "MKA Ward Roll Book";
                rdbMKAWardRollBook.Tag = "MKA Ward Roll Book";

                rdbWardPhotoRollBook.Text = "MKA Ward Photo Roll Book";
                rdbWardPhotoRollBook.Tag = "MKA Ward Photo Roll Book";

                rdbVoterIdCard.Text = "MKA Ward Voter Id Card";
                rdbVoterIdCard.Tag = "MKA Ward Voter Id Card";

                rdbVoterPhotoCard.Text = "MKA Ward Voter Photo Card";
                rdbVoterPhotoCard.Tag = "MKA Ward Voter Photo Card";

                rdbVoterEnvelope.Text = "MKA Ward Roll Envelope";
                rdbVoterEnvelope.Tag = "MKA Ward Roll Envelope";
                chkWardRollEnvelope.Text = "MKA Ward Roll Envelope";
                chkWardRollEnvelope.Tag = "MKA Ward Roll Envelope";

                rdbWardRollDeletion.Text = "MKA Ward Roll Deletion";
                rdbWardRollDeletion.Tag = "MKA Ward Roll Deletion";

                rdbPreFieldWorkSummary.Visible = false;
                rdbPreFieldWorkSummary.Text = "Ward Pre-Field Work Summary";
                rdbPreFieldWorkSummary.Tag = "Ward Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Text = "Ward Pre-Field Work Summary";
                chkWardPreFieldWorkSummary.Tag = "Ward Pre-Field Work Summary";
            }

            ManageFilterPanel();
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
                MessageBox.Show("The To date must be greater than the From date.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Alpha Split
            if (rdbAlphaSplit.Checked == true && string.IsNullOrWhiteSpace(txtAlphaSplitStart.Text))
            {
                isValidForm = false;
                MessageBox.Show("Please enter valid start alphabet.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdbAlphaSplit.Checked == true && string.IsNullOrWhiteSpace(txtAlphaSplitEnd.Text))
            {
                isValidForm = false;
                MessageBox.Show("Please enter valid end alphabet.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rdbAlphaSplit.Checked == true && !string.IsNullOrWhiteSpace(txtAlphaSplitStart.Text) && !string.IsNullOrWhiteSpace(txtAlphaSplitEnd.Text))
            {
                byte[] asciiBytesStart = Encoding.ASCII.GetBytes(txtAlphaSplitStart.Text);
                byte[] asciiBytesEnd = Encoding.ASCII.GetBytes(txtAlphaSplitEnd.Text);

                if (asciiBytesStart[0] > asciiBytesEnd[0])
                {
                    isValidForm = false;
                    MessageBox.Show("Please enter valid alpha split range.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// Handles the CheckedChanged event of the rdbMKAWardRollBook control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rdbWardRollBook_CheckedChanged(object sender, EventArgs e)
        {
            ManageFilterPanel();
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

        //#region Methods

        #region ManageFilterPanel
        /// <summary>
        /// Manages the filter panel.
        /// </summary>
        private void ManageFilterPanel()
        {
            if (rdbMKAWardRollBook.Checked == true || rdbVoterIdCard.Checked == true || rdbWardRollDeletion.Checked == true)
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
                if (rdbMKAWardRollBook.Checked == true)
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
            if (rdbMKAWardRollBook.Checked == true || rdbWardPhotoRollBook.Checked == true)
            {
                chkIncludePossibleDeletionReport.Enabled = true;
                chkWardRollEnvelope.Enabled = true;
                chkWardPreFieldWorkSummary.Enabled = true;

                if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                {
                    chkIncludePossibleDeletionReport.Checked = false;
                    chkIncludePossibleDeletionReport.Enabled = false;
                }

                //if(rdbMKAWardRollBook.Checked == true && GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
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

                if (rdbMKAWardRollBook.Checked == true || rdbWardPhotoRollBook.Checked == true)
                {
                    txtReportTitle.Enabled = true;
                    if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final)
                    {
                        txtReportTitle.Text = $"ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        txtReportTitle.Text = String.Empty;
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                    {
                        txtReportTitle.Text = $"PRELIMINARY ELECTORAL ROLL {DateTime.Now.ToString("yyyy")}";
                        txtReportTitle.Text = String.Empty;
                    }
                    else if (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Staging)
                    {
                        txtReportTitle.Text = $"WORKING ROLL {DateTime.Now.ToString("yyyy")}";
                        txtReportTitle.Text = String.Empty;
                    }

                }
                else
                {
                    txtReportTitle.Enabled = false;
                    txtReportTitle.Text = string.Empty;
                }

                #region This condition set for only MKA Preliminary working roll report with Adding Deletion report
                if (rdbMKAWardRollBook.Checked == true && (GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary || GlobalsModel.LoggedUser.Database == CommonEnum.Database.Final))
                {
                    if(GlobalsModel.LoggedUser.Database == CommonEnum.Database.Preliminary)
                        chkIncludePossibleDeletionReport.Enabled = true;
                    
                    gbGrouping.Visible = true;
                }
                else
                {
                    chkIncludePossibleDeletionReport.Enabled = false;
                    chkIncludePossibleDeletionReport.Checked = false;
                    gbGrouping.Visible = false;
                }
                #endregion
            }
            else
            {
                chkIncludePossibleDeletionReport.Enabled = false;
                chkWardRollEnvelope.Enabled = false;
                chkWardPreFieldWorkSummary.Enabled = false;
                txtReportTitle.Enabled = false;
                txtReportTitle.Text = string.Empty;

                gbGrouping.Visible = false;
            }
        }
        #endregion

        //#endregion
    }
}
