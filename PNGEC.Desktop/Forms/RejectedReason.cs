using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PNGEC.API.Domain.Mobile.Voter;

namespace PNGEC.Desktop.Forms
{
    public partial class RejectedReason : Form
    {
        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RejectedReason"/> class.
        /// </summary>
        public RejectedReason()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to reject this record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsValidForm()
        {
            if (string.IsNullOrWhiteSpace(txtRejectedReason.Text.Trim()))
            {
                MessageBox.Show("Rejected reason can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRejectedReason.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handles the Click event of the bntCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void bntCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
