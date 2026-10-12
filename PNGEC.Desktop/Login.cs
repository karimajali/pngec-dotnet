using PNGEC.API.Domain.User;
using PNGEC.API.Helper;
using PNGEC.Desktop.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Response = PNGEC.API.Helper.Response;

namespace PNGEC.Desktop
{
    public partial class Login : Form
    {
        #region variable declaration
        /// <summary>
        /// The request model
        /// </summary>
        private CommonRequest requestModel = null;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the Login control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Login_Load(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DefaultProvisionalCutOffDOB))
                {
                    Properties.Settings.Default.DefaultProvisionalCutOffDOB = Helper.AppSettings.DefaultProvisionalCutOffDOB;
                    Properties.Settings.Default.Save();
                }
            }
            catch { }

            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblversion.Text = $"Version : {API.Helper.Globals.DesktopVersionYear}.{version.Major}.{version.Minor}";
            BindDatabase();
            BindServer();
        }

        /// <summary>
        /// Handles the Click event of the btnlogin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                #region Only for Developer

                //if (string.IsNullOrWhiteSpace(txtUserName.Text))
                //{
                //    txtUserName.Text = "em@pnge.com";
                //    txtPassword.Text = "em123";
                //    cmbDatabase.SelectedValue = CommonEnum.Database.Staging;
                //}
                //if (txtUserName.Text == "2")
                //{
                //    txtUserName.Text = "bhavesh.ghul@hiddenbrains.in";
                //    txtPassword.Text = "bhavesh77";
                //    cmbDatabase.SelectedValue = CommonEnum.Database.Staging;
                //}
                //if (txtUserName.Text == "3")
                //{
                //    txtUserName.Text = "ro@png.com";
                //    txtPassword.Text = "123456";
                //    cmbDatabase.SelectedValue = CommonEnum.Database.Staging;
                //}
                //if (txtUserName.Text == "4")
                //{
                //    txtUserName.Text = "rd@pnge.com";
                //    txtPassword.Text = "rd123";
                //    cmbDatabase.SelectedValue = CommonEnum.Database.Staging;
                //}
                //if (txtUserName.Text == "5")
                //{
                //    txtUserName.Text = "l.wanusim@gmail.com";
                //    txtPassword.Text = "123456";
                //    cmbDatabase.SelectedValue = CommonEnum.Database.Staging;
                //}

                // string MD5 = CommonFunction.MD5Hash(txtPassword.Text.Trim());

                #endregion

                if (IsValidForm())
                {
                    requestModel = new CommonRequest();
                    requestModel.username = txtUserName.Text.Trim();
                    requestModel.password = txtPassword.Text.Trim();
                    requestModel.database = cmbDatabase.Text;
                    requestModel.server = cmbServer.Text;

                    GlobalsModel.LoggedUser.Server = (CommonEnum.Server)cmbServer.SelectedValue;
                    Globals.serverURL = "";
                    if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 1)//Development
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["DevelopmentApiURL"].ToString();
                    }
                    else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 2)//Training
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
                    }
                    else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)//Production
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
                    }
                    else // Development
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["DevelopmentApiURL"].ToString();
                    }

                    Response<UserModel> response = await GenericHttpClient.PostRequest<UserModel>("User/UserLogin", requestModel);

                    if (response.Status)
                    {
                        if (response.Data != null && response.Data.ListItems != null && response.Data.ListItems.Count<UserModel>() > 0)
                        {
                            GlobalsModel.LoggedUser.User = response.Data.ListItems.FirstOrDefault();
                            GlobalsModel.LoggedUser.Database = (CommonEnum.Database)cmbDatabase.SelectedValue;
                            await CommonFunction.AddActivityLogForDesktop(GlobalsModel.LoggedUser.User.UserId, "Login", "Login Success " + GlobalsModel.LoggedUser.User.EmailAddress);

                            try
                            {
                                //set location 
                                LocationDetails.GetLocationDetails();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.Write("User Login", "GetLocationDetails", ex);
                            }
                            
                        }

                        //this.Cursor = Cursors.WaitCursor;
                        //this.pnlLogin.Enabled = false;
                        //await GetList();
                        //this.pnlLogin.Enabled = true;
                        //this.Cursor = Cursors.Arrow;

                        this.Hide();
                        GlobalsModel.mainMaster = new MainMaster();
                        GlobalsModel.mainMaster.Closed += (s, args) => this.Close();
                        GlobalsModel.mainMaster.Show();
                    }
                    else
                    {

                        await CommonFunction.AddActivityLogForDesktop(0, "Login", "Login - " + response.Message + " --- " + requestModel.username);
                        MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    #region oldCode
                    //if (txtUserName.Text.Trim() == "admin" && txtPassword.Text.Trim() == "admin" && (int)cmbDatabase.SelectedValue > 0)
                    //{
                    //    this.Cursor = Cursors.WaitCursor;
                    //    await GetList();
                    //    this.Cursor = Cursors.Arrow;

                    //    this.Hide();
                    //    var mm = new MainMaster();
                    //    mm.Closed += (s, args) => this.Close();
                    //    mm.Show();
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;
                await CommonFunction.AddActivityLogForDesktop(0, "Login", "Login - " + Resources.ResourceManager.GetString("ErrorMessage") + " --- " + requestModel.username);
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Login", "btnlogin_Click", ex);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the Login control.
        /// </summary> 
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.pnlForgotPassword.Visible)
            {
                this.HideForgetPassword();
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the lbForgotPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lbForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Text = "Forgot Password";
            this.pnlForgotPassword.Visible = true;
            this.pnlLogin.Visible = false;
            this.txtUserNameForgotPassword.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.HideForgetPassword();
        }

        /// <summary>
        /// Handles the Click event of the btnReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnReset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserNameForgotPassword.Text))
            {
                bool isUsername = Regex.IsMatch(txtUserNameForgotPassword.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isUsername)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Globals.serverURL = "";
                    if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 1)//Development
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["DevelopmentApiURL"].ToString();
                    }
                    else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 2)//Training
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["TrainingApiURL"].ToString();
                    }
                    else if (Convert.ToInt16(GlobalsModel.LoggedUser.Server) == 3)//Production
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["ProductionApiURL"].ToString();
                    }
                    else//Development
                    {
                        Globals.serverURL = ConfigurationManager.AppSettings["DevelopmentApiURL"].ToString();
                    }

                    this.pnlForgotPassword.Enabled = false;
                    requestModel = new CommonRequest();
                    requestModel.username = txtUserNameForgotPassword.Text.Trim();
                    try
                    {
                        Response response = await GenericHttpClient.PostRequest("User/ForgotPassword", requestModel);
                        this.Cursor = Cursors.Arrow;
                        this.pnlForgotPassword.Enabled = true;
                        if (response.Status)
                        {
                            await CommonFunction.AddActivityLogForDesktop(0, "Login", "Reset Password " + requestModel.username);

                            DialogResult dialogResult = MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (dialogResult == DialogResult.OK)
                            {
                                this.Text = "Login";
                                this.pnlLogin.Visible = true;
                                this.pnlForgotPassword.Visible = false;
                                this.txtUserNameForgotPassword.Text = string.Empty;
                                this.txtUserName.Focus();
                            }
                        }
                        else
                        {
                            await CommonFunction.AddActivityLogForDesktop(0, "Login", "Reset Password - " + response.Message + " --- " + requestModel.username);
                            this.txtUserNameForgotPassword.Text = string.Empty;
                            MessageBox.Show(response.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Arrow;
                        MessageBox.Show("Something went wrong", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ErrorLog.Write("Forgot Password", "btnReset_Click", ex);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid username.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show(Resources.ResourceManager.GetString("UserName_EmptyOrNull"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Method
        #region IsValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        private Boolean IsValidForm()
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()) && string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show(Resources.ResourceManager.GetString("UsernameAndPassword_EmptyOrNull"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
                MessageBox.Show(Resources.ResourceManager.GetString("UserName_EmptyOrNull"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show(Resources.ResourceManager.GetString("Password_EmptyOrNull"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // valid username/email address checking with regex
            if (!string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
                bool isUsername = Regex.IsMatch(txtUserName.Text.Trim(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                if (isUsername == false)
                {
                    MessageBox.Show("Please enter a valid username.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if ((int)cmbDatabase.SelectedValue == 0)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("Database_Select"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region BindDatabase
        /// <summary>
        /// Binds the database.
        /// </summary>
        private void BindDatabase()
        {
            try
            {
                cmbDatabase.DataSource = Enum.GetValues(typeof(CommonEnum.Database))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
                cmbDatabase.DisplayMember = "Description";
                cmbDatabase.ValueMember = "value";

                //cmbDatabase.DataSource = Enum.GetValues(typeof(CommonEnum.Database));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Login", "BindDatabase", ex);
            }
        }
        #endregion

        #region BindServer
        /// <summary>
        /// Binds the server.
        /// </summary> 
        private void BindServer()
        {
            try
            {
                cmbServer.DataSource = Enum.GetValues(typeof(CommonEnum.Server))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
                cmbServer.DisplayMember = "Description";
                cmbServer.ValueMember = "value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Login", "BindServer", ex);
            }
        }
        #endregion

        #region Hide Forget Password
        /// <summary>
        /// Hides the forget password.
        /// </summary>
        private void HideForgetPassword()
        {
            this.Text = "Login";
            this.pnlLogin.Visible = true;
            this.pnlForgotPassword.Visible = false;
            this.txtUserNameForgotPassword.Text = string.Empty;
            this.txtUserName.Focus();
        }
        #endregion
        #endregion
    }
}
