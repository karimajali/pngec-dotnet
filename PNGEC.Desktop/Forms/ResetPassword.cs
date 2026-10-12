using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using System;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ResetPassword : Form
    {
        #region variable declaration
        /// <summary>
        /// The model
        /// </summary>
        private UserModel model;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPassword"/> class.
        /// </summary>
        public ResetPassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPassword"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public ResetPassword(UserModel model)
        {
            InitializeComponent();
            this.model = model;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the ResetPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ResetPassword_Load(object sender, EventArgs e)
        {
            if (this.model != null && this.model.UserId > 0)
            {
                BindControlData(model);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsValidForm())
            {
                this.Cursor = Cursors.WaitCursor;
                model.Password = txtPassword.Text;
                Response response = await GenericHttpClient.PostRequest("User/ResetPassword", model);
                if (response.Status)
                {
                    DialogResult dialogResult = MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        this.Hide();
                    }
                    ClearForm();
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            this.Hide();
        }
        #endregion

        #region Methods

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void ClearForm()
        {
            txtPassword.Text = string.Empty;
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
            if(this.model.UserId <= 0)
            {
                MessageBox.Show("Something went wrong, try again later", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Password can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region BindControlData
        /// <summary>
        /// Binds the control data.
        /// </summary>
        /// <param name="user">The user.</param>
        private void BindControlData(UserModel user)
        {
            txtUserName.Text = user.EmailAddress;
        }
        #endregion

        #endregion
    }
}
