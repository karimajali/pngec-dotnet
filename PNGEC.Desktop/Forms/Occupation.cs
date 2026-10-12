using System;
using System.Windows.Forms;
using PNGEC.API.Domain.Mobile.General;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;

namespace PNGEC.Desktop.Forms
{
    public partial class Occupation : Form
    {
        #region Variable declaration
        /// <summary>
        /// The model
        /// </summary>
        private OccupationModel model;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Occupation"/> class.
        /// </summary>
        public Occupation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Occupation"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public Occupation(OccupationModel model)
        {
            InitializeComponent();
            this.model = model;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Occupation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Occupation_Load(object sender, EventArgs e)
        {
            if (this.model != null)
            {
                this.txtOccupation.Text = this.model.OccupationName;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtOccupation.Text.Trim()))
            {
                MessageBox.Show("occupation can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string processtext = string.Empty;
            var deviceId = this.model != null ? this.model.OccupationPK : 0;
            if (deviceId > 0)
                processtext = "update";
            else
                processtext = "add";

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to " + processtext + " the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                OccupationModel request = new OccupationModel
                {
                    OccupationPK = this.model != null ? this.model.OccupationPK : 0,
                    OccupationName = this.txtOccupation.Text,
                    ActionBy = GlobalsModel.LoggedUser.User.UserId
                };

                var response = GenericHttpClient.PostRequest<OccupationModel>("Occupation/AddUpdateOccupation", request);
                if (response.Result != null && response.Result.Status)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                    MessageBox.Show(response.Result.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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

        #region Methods

        #endregion

    }
}
