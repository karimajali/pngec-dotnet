using System;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ImportBioDetailsStatusType : Form
    {
        public ImportBioDetailsStatusType()
        {
            InitializeComponent();
        }

        #region Event

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
