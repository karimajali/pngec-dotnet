using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGEC.Desktop.Report
{
    public partial class TransferInVoterReport : Form
    {
        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TransferInVoterReport"/> class.
        /// </summary>
        public TransferInVoterReport()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the TransferInVoterReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TransferInVoterReport_Load(object sender, EventArgs e)
        {
            this.rvTransferInVoter.RefreshReport();
        }

        #endregion
    }
}
