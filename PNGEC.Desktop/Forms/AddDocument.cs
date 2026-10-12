using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using PNGEC.API.Helper;
using PNGEC.Desktop.Properties;

namespace PNGEC.Desktop.Forms
{
    public partial class AddDocument : Form
    {

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AddDocument"/> class.
        /// </summary>
        public AddDocument()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the AddDocument control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AddDocument_Load(object sender, System.EventArgs e)
        {
            this.BindDocumentType();
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to add document?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
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
        #endregion

        #region Methods

        #region BindDocumentType
        /// <summary>
        /// Binds the role.
        /// </summary>
        private void BindDocumentType()
        {
            try
            {
                cmbType.DataSource = Enum.GetValues(typeof(CommonEnum.DocumentManagement))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .Where(c => Convert.ToInt32(c.value) == 0 || Convert.ToInt32(c.value) == 3 || Convert.ToInt32(c.value) == 4)
                .OrderBy(item => item.value)
                .ToList();

                cmbType.DisplayMember = "Description";
                cmbType.ValueMember = "value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("Add Document Form", "BindDocumentType", ex);
            }

        }
        #endregion

        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        /// <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsValidForm()
        {
            if (cmbType.Text == "Select Type")
            {
                MessageBox.Show("Please select document type.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text.Trim()))
            {
                MessageBox.Show("Description can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }
            return true;
        }

        #endregion


    }
}
