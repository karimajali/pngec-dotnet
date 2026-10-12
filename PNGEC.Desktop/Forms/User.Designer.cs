using System.Drawing;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    partial class User
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdProfileImage = new System.Windows.Forms.OpenFileDialog();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.pnlcontrol = new System.Windows.Forms.Panel();
            this.pnlMKAAssign = new System.Windows.Forms.Panel();
            this.lblMKABio = new System.Windows.Forms.Label();
            this.cmbMKAWardName = new System.Windows.Forms.ComboBox();
            this.lblMKAWardName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMKALLGName = new System.Windows.Forms.ComboBox();
            this.lblMKALLGName = new System.Windows.Forms.Label();
            this.cmbMKADistrictName = new System.Windows.Forms.ComboBox();
            this.pnlPermission = new System.Windows.Forms.Panel();
            this.chkManageClan = new System.Windows.Forms.CheckBox();
            this.chkMKAForm11 = new System.Windows.Forms.CheckBox();
            this.chkMKAWardRollBook = new System.Windows.Forms.CheckBox();
            this.chkMKAGeoStructure = new System.Windows.Forms.CheckBox();
            this.chkMKAFile = new System.Windows.Forms.CheckBox();
            this.chkDevices = new System.Windows.Forms.CheckBox();
            this.chkUsers = new System.Windows.Forms.CheckBox();
            this.chkForm11 = new System.Windows.Forms.CheckBox();
            this.chkRoleStat = new System.Windows.Forms.CheckBox();
            this.chkWardRollBook = new System.Windows.Forms.CheckBox();
            this.lblPermissionModules = new System.Windows.Forms.Label();
            this.chkDatabaseConnect = new System.Windows.Forms.CheckBox();
            this.chkGeoStructure = new System.Windows.Forms.CheckBox();
            this.chkFile = new System.Windows.Forms.CheckBox();
            this.pnlLocation = new System.Windows.Forms.Panel();
            this.cmbLocationName = new System.Windows.Forms.ComboBox();
            this.lblLocationName = new System.Windows.Forms.Label();
            this.pnlLlgWard = new System.Windows.Forms.Panel();
            this.cmbWardName = new System.Windows.Forms.ComboBox();
            this.lblWardName = new System.Windows.Forms.Label();
            this.cmbLLGName = new System.Windows.Forms.ComboBox();
            this.lblLLGName = new System.Windows.Forms.Label();
            this.pnlDistrict = new System.Windows.Forms.Panel();
            this.cmbDistrictName = new System.Windows.Forms.ComboBox();
            this.lblDistrictName = new System.Windows.Forms.Label();
            this.pnlRegionProvince = new System.Windows.Forms.Panel();
            this.lblRegionName = new System.Windows.Forms.Label();
            this.cmbProvinceName = new System.Windows.Forms.ComboBox();
            this.lblProvinceName = new System.Windows.Forms.Label();
            this.cmbRegionName = new System.Windows.Forms.ComboBox();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtOldProfileImageName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pbProfileImage = new System.Windows.Forms.PictureBox();
            this.txtContactNumber = new System.Windows.Forms.TextBox();
            this.lblContactNumber = new System.Windows.Forms.Label();
            this.btnUploadProfileImage = new System.Windows.Forms.Button();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.txtProfileImage = new System.Windows.Forms.TextBox();
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.lblProfileImage = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.pnlcontrol.SuspendLayout();
            this.pnlMKAAssign.SuspendLayout();
            this.pnlPermission.SuspendLayout();
            this.pnlLocation.SuspendLayout();
            this.pnlLlgWard.SuspendLayout();
            this.pnlDistrict.SuspendLayout();
            this.pnlRegionProvince.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfileImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdProfileImage
            // 
            this.ofdProfileImage.FileName = "ProfileImage";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSubmit.BackColor = System.Drawing.Color.Black;
            this.btnSubmit.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(238, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(140, 42);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(385, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 42);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlButton);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 623);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(995, 63);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnSubmit);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Location = new System.Drawing.Point(3, 3);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(888, 48);
            this.pnlButton.TabIndex = 0;
            // 
            // pnlcontrol
            // 
            this.pnlcontrol.AutoScroll = true;
            this.pnlcontrol.Controls.Add(this.pnlMKAAssign);
            this.pnlcontrol.Controls.Add(this.pnlPermission);
            this.pnlcontrol.Controls.Add(this.pnlLocation);
            this.pnlcontrol.Controls.Add(this.pnlLlgWard);
            this.pnlcontrol.Controls.Add(this.pnlDistrict);
            this.pnlcontrol.Controls.Add(this.pnlRegionProvince);
            this.pnlcontrol.Controls.Add(this.pnlUserInfo);
            this.pnlcontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlcontrol.Location = new System.Drawing.Point(0, 0);
            this.pnlcontrol.Name = "pnlcontrol";
            this.pnlcontrol.Size = new System.Drawing.Size(995, 623);
            this.pnlcontrol.TabIndex = 0;
            // 
            // pnlMKAAssign
            // 
            this.pnlMKAAssign.Controls.Add(this.lblMKABio);
            this.pnlMKAAssign.Controls.Add(this.cmbMKAWardName);
            this.pnlMKAAssign.Controls.Add(this.lblMKAWardName);
            this.pnlMKAAssign.Controls.Add(this.label1);
            this.pnlMKAAssign.Controls.Add(this.cmbMKALLGName);
            this.pnlMKAAssign.Controls.Add(this.lblMKALLGName);
            this.pnlMKAAssign.Controls.Add(this.cmbMKADistrictName);
            this.pnlMKAAssign.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMKAAssign.Location = new System.Drawing.Point(0, 943);
            this.pnlMKAAssign.Name = "pnlMKAAssign";
            this.pnlMKAAssign.Size = new System.Drawing.Size(974, 188);
            this.pnlMKAAssign.TabIndex = 6;
            // 
            // lblMKABio
            // 
            this.lblMKABio.AutoSize = true;
            this.lblMKABio.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKABio.Location = new System.Drawing.Point(40, 12);
            this.lblMKABio.Name = "lblMKABio";
            this.lblMKABio.Size = new System.Drawing.Size(75, 19);
            this.lblMKABio.TabIndex = 7;
            this.lblMKABio.Text = "MKA Bio";
            // 
            // cmbMKAWardName
            // 
            this.cmbMKAWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKAWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKAWardName.FormattingEnabled = true;
            this.cmbMKAWardName.Location = new System.Drawing.Point(241, 136);
            this.cmbMKAWardName.Name = "cmbMKAWardName";
            this.cmbMKAWardName.Size = new System.Drawing.Size(403, 28);
            this.cmbMKAWardName.TabIndex = 6;
            // 
            // lblMKAWardName
            // 
            this.lblMKAWardName.AutoSize = true;
            this.lblMKAWardName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKAWardName.Location = new System.Drawing.Point(38, 140);
            this.lblMKAWardName.Name = "lblMKAWardName";
            this.lblMKAWardName.Size = new System.Drawing.Size(135, 19);
            this.lblMKAWardName.TabIndex = 5;
            this.lblMKAWardName.Text = "MKA Ward Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "MKA District Name";
            // 
            // cmbMKALLGName
            // 
            this.cmbMKALLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKALLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKALLGName.FormattingEnabled = true;
            this.cmbMKALLGName.Location = new System.Drawing.Point(241, 88);
            this.cmbMKALLGName.Name = "cmbMKALLGName";
            this.cmbMKALLGName.Size = new System.Drawing.Size(403, 28);
            this.cmbMKALLGName.TabIndex = 3;
            this.cmbMKALLGName.SelectedIndexChanged += new System.EventHandler(this.cmbMKALLGName_SelectedIndexChanged);
            // 
            // lblMKALLGName
            // 
            this.lblMKALLGName.AutoSize = true;
            this.lblMKALLGName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMKALLGName.Location = new System.Drawing.Point(40, 93);
            this.lblMKALLGName.Name = "lblMKALLGName";
            this.lblMKALLGName.Size = new System.Drawing.Size(125, 19);
            this.lblMKALLGName.TabIndex = 2;
            this.lblMKALLGName.Text = "MKA LLG Name";
            // 
            // cmbMKADistrictName
            // 
            this.cmbMKADistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMKADistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMKADistrictName.FormattingEnabled = true;
            this.cmbMKADistrictName.Location = new System.Drawing.Point(241, 43);
            this.cmbMKADistrictName.Name = "cmbMKADistrictName";
            this.cmbMKADistrictName.Size = new System.Drawing.Size(403, 28);
            this.cmbMKADistrictName.TabIndex = 1;
            this.cmbMKADistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbMKADistrictName_SelectedIndexChanged);
            // 
            // pnlPermission
            // 
            this.pnlPermission.Controls.Add(this.chkManageClan);
            this.pnlPermission.Controls.Add(this.chkMKAForm11);
            this.pnlPermission.Controls.Add(this.chkMKAWardRollBook);
            this.pnlPermission.Controls.Add(this.chkMKAGeoStructure);
            this.pnlPermission.Controls.Add(this.chkMKAFile);
            this.pnlPermission.Controls.Add(this.chkDevices);
            this.pnlPermission.Controls.Add(this.chkUsers);
            this.pnlPermission.Controls.Add(this.chkForm11);
            this.pnlPermission.Controls.Add(this.chkRoleStat);
            this.pnlPermission.Controls.Add(this.chkWardRollBook);
            this.pnlPermission.Controls.Add(this.lblPermissionModules);
            this.pnlPermission.Controls.Add(this.chkDatabaseConnect);
            this.pnlPermission.Controls.Add(this.chkGeoStructure);
            this.pnlPermission.Controls.Add(this.chkFile);
            this.pnlPermission.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPermission.Location = new System.Drawing.Point(0, 767);
            this.pnlPermission.Name = "pnlPermission";
            this.pnlPermission.Size = new System.Drawing.Size(974, 176);
            this.pnlPermission.TabIndex = 5;
            // 
            // chkManageClan
            // 
            this.chkManageClan.AutoSize = true;
            this.chkManageClan.Location = new System.Drawing.Point(241, 147);
            this.chkManageClan.Name = "chkManageClan";
            this.chkManageClan.Size = new System.Drawing.Size(127, 23);
            this.chkManageClan.TabIndex = 13;
            this.chkManageClan.Tag = "13";
            this.chkManageClan.Text = "Manage Clan";
            this.chkManageClan.UseVisualStyleBackColor = true;
            // 
            // chkMKAForm11
            // 
            this.chkMKAForm11.AutoSize = true;
            this.chkMKAForm11.Location = new System.Drawing.Point(769, 102);
            this.chkMKAForm11.Name = "chkMKAForm11";
            this.chkMKAForm11.Size = new System.Drawing.Size(125, 23);
            this.chkMKAForm11.TabIndex = 12;
            this.chkMKAForm11.Tag = "12";
            this.chkMKAForm11.Text = "MKA Form11";
            this.chkMKAForm11.UseVisualStyleBackColor = true;
            // 
            // chkMKAWardRollBook
            // 
            this.chkMKAWardRollBook.AutoSize = true;
            this.chkMKAWardRollBook.Location = new System.Drawing.Point(567, 102);
            this.chkMKAWardRollBook.Name = "chkMKAWardRollBook";
            this.chkMKAWardRollBook.Size = new System.Drawing.Size(183, 23);
            this.chkMKAWardRollBook.TabIndex = 11;
            this.chkMKAWardRollBook.Tag = "10";
            this.chkMKAWardRollBook.Text = "MKA Ward Roll Book";
            this.chkMKAWardRollBook.UseVisualStyleBackColor = true;
            // 
            // chkMKAGeoStructure
            // 
            this.chkMKAGeoStructure.AutoSize = true;
            this.chkMKAGeoStructure.Location = new System.Drawing.Point(376, 102);
            this.chkMKAGeoStructure.Name = "chkMKAGeoStructure";
            this.chkMKAGeoStructure.Size = new System.Drawing.Size(172, 23);
            this.chkMKAGeoStructure.TabIndex = 10;
            this.chkMKAGeoStructure.Tag = "10";
            this.chkMKAGeoStructure.Text = "MKA Geo Structure";
            this.chkMKAGeoStructure.UseVisualStyleBackColor = true;
            // 
            // chkMKAFile
            // 
            this.chkMKAFile.AutoSize = true;
            this.chkMKAFile.Location = new System.Drawing.Point(241, 102);
            this.chkMKAFile.Name = "chkMKAFile";
            this.chkMKAFile.Size = new System.Drawing.Size(96, 23);
            this.chkMKAFile.TabIndex = 9;
            this.chkMKAFile.Tag = "9";
            this.chkMKAFile.Text = "MKA File";
            this.chkMKAFile.UseVisualStyleBackColor = true;
            // 
            // chkDevices
            // 
            this.chkDevices.AutoSize = true;
            this.chkDevices.Location = new System.Drawing.Point(769, 57);
            this.chkDevices.Name = "chkDevices";
            this.chkDevices.Size = new System.Drawing.Size(89, 23);
            this.chkDevices.TabIndex = 8;
            this.chkDevices.Tag = "8";
            this.chkDevices.Text = "Devices";
            this.chkDevices.UseVisualStyleBackColor = true;
            // 
            // chkUsers
            // 
            this.chkUsers.AutoSize = true;
            this.chkUsers.Location = new System.Drawing.Point(567, 57);
            this.chkUsers.Name = "chkUsers";
            this.chkUsers.Size = new System.Drawing.Size(73, 23);
            this.chkUsers.TabIndex = 7;
            this.chkUsers.Tag = "7";
            this.chkUsers.Text = "Users";
            this.chkUsers.UseVisualStyleBackColor = true;
            // 
            // chkForm11
            // 
            this.chkForm11.AutoSize = true;
            this.chkForm11.Location = new System.Drawing.Point(376, 57);
            this.chkForm11.Name = "chkForm11";
            this.chkForm11.Size = new System.Drawing.Size(91, 23);
            this.chkForm11.TabIndex = 6;
            this.chkForm11.Tag = "6";
            this.chkForm11.Text = "Form 11";
            this.chkForm11.UseVisualStyleBackColor = true;
            // 
            // chkRoleStat
            // 
            this.chkRoleStat.AutoSize = true;
            this.chkRoleStat.Location = new System.Drawing.Point(241, 57);
            this.chkRoleStat.Name = "chkRoleStat";
            this.chkRoleStat.Size = new System.Drawing.Size(96, 23);
            this.chkRoleStat.TabIndex = 5;
            this.chkRoleStat.Tag = "5";
            this.chkRoleStat.Text = "Role Stat";
            this.chkRoleStat.UseVisualStyleBackColor = true;
            // 
            // chkWardRollBook
            // 
            this.chkWardRollBook.AutoSize = true;
            this.chkWardRollBook.Location = new System.Drawing.Point(769, 11);
            this.chkWardRollBook.Name = "chkWardRollBook";
            this.chkWardRollBook.Size = new System.Drawing.Size(144, 23);
            this.chkWardRollBook.TabIndex = 4;
            this.chkWardRollBook.Tag = "4";
            this.chkWardRollBook.Text = "Ward Roll Book";
            this.chkWardRollBook.UseVisualStyleBackColor = true;
            // 
            // lblPermissionModules
            // 
            this.lblPermissionModules.AutoSize = true;
            this.lblPermissionModules.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPermissionModules.Location = new System.Drawing.Point(38, 10);
            this.lblPermissionModules.Name = "lblPermissionModules";
            this.lblPermissionModules.Size = new System.Drawing.Size(155, 19);
            this.lblPermissionModules.TabIndex = 0;
            this.lblPermissionModules.Text = "Permission Modules";
            // 
            // chkDatabaseConnect
            // 
            this.chkDatabaseConnect.AutoSize = true;
            this.chkDatabaseConnect.Location = new System.Drawing.Point(567, 11);
            this.chkDatabaseConnect.Name = "chkDatabaseConnect";
            this.chkDatabaseConnect.Size = new System.Drawing.Size(166, 23);
            this.chkDatabaseConnect.TabIndex = 3;
            this.chkDatabaseConnect.Tag = "3";
            this.chkDatabaseConnect.Text = "Database Connect";
            this.chkDatabaseConnect.UseVisualStyleBackColor = true;
            // 
            // chkGeoStructure
            // 
            this.chkGeoStructure.AutoSize = true;
            this.chkGeoStructure.Location = new System.Drawing.Point(376, 11);
            this.chkGeoStructure.Name = "chkGeoStructure";
            this.chkGeoStructure.Size = new System.Drawing.Size(133, 23);
            this.chkGeoStructure.TabIndex = 2;
            this.chkGeoStructure.Tag = "2";
            this.chkGeoStructure.Text = "Geo Structure";
            this.chkGeoStructure.UseVisualStyleBackColor = true;
            // 
            // chkFile
            // 
            this.chkFile.AutoSize = true;
            this.chkFile.Location = new System.Drawing.Point(241, 11);
            this.chkFile.Name = "chkFile";
            this.chkFile.Size = new System.Drawing.Size(57, 23);
            this.chkFile.TabIndex = 1;
            this.chkFile.Tag = "1";
            this.chkFile.Text = "File";
            this.chkFile.UseVisualStyleBackColor = true;
            // 
            // pnlLocation
            // 
            this.pnlLocation.Controls.Add(this.cmbLocationName);
            this.pnlLocation.Controls.Add(this.lblLocationName);
            this.pnlLocation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLocation.Location = new System.Drawing.Point(0, 715);
            this.pnlLocation.Name = "pnlLocation";
            this.pnlLocation.Size = new System.Drawing.Size(974, 52);
            this.pnlLocation.TabIndex = 4;
            // 
            // cmbLocationName
            // 
            this.cmbLocationName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLocationName.FormattingEnabled = true;
            this.cmbLocationName.Location = new System.Drawing.Point(241, 15);
            this.cmbLocationName.Name = "cmbLocationName";
            this.cmbLocationName.Size = new System.Drawing.Size(403, 28);
            this.cmbLocationName.TabIndex = 7;
            // 
            // lblLocationName
            // 
            this.lblLocationName.AutoSize = true;
            this.lblLocationName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocationName.Location = new System.Drawing.Point(38, 19);
            this.lblLocationName.Name = "lblLocationName";
            this.lblLocationName.Size = new System.Drawing.Size(118, 19);
            this.lblLocationName.TabIndex = 6;
            this.lblLocationName.Text = "Location Name";
            // 
            // pnlLlgWard
            // 
            this.pnlLlgWard.Controls.Add(this.cmbWardName);
            this.pnlLlgWard.Controls.Add(this.lblWardName);
            this.pnlLlgWard.Controls.Add(this.cmbLLGName);
            this.pnlLlgWard.Controls.Add(this.lblLLGName);
            this.pnlLlgWard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLlgWard.Location = new System.Drawing.Point(0, 600);
            this.pnlLlgWard.Name = "pnlLlgWard";
            this.pnlLlgWard.Size = new System.Drawing.Size(974, 115);
            this.pnlLlgWard.TabIndex = 3;
            // 
            // cmbWardName
            // 
            this.cmbWardName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWardName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWardName.FormattingEnabled = true;
            this.cmbWardName.Location = new System.Drawing.Point(241, 73);
            this.cmbWardName.Name = "cmbWardName";
            this.cmbWardName.Size = new System.Drawing.Size(403, 28);
            this.cmbWardName.TabIndex = 5;
            this.cmbWardName.SelectedIndexChanged += new System.EventHandler(this.cmbWardName_SelectedIndexChanged);
            // 
            // lblWardName
            // 
            this.lblWardName.AutoSize = true;
            this.lblWardName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWardName.Location = new System.Drawing.Point(38, 77);
            this.lblWardName.Name = "lblWardName";
            this.lblWardName.Size = new System.Drawing.Size(96, 19);
            this.lblWardName.TabIndex = 4;
            this.lblWardName.Text = "Ward Name";
            // 
            // cmbLLGName
            // 
            this.cmbLLGName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLLGName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLLGName.FormattingEnabled = true;
            this.cmbLLGName.Location = new System.Drawing.Point(241, 17);
            this.cmbLLGName.Name = "cmbLLGName";
            this.cmbLLGName.Size = new System.Drawing.Size(403, 28);
            this.cmbLLGName.TabIndex = 3;
            this.cmbLLGName.SelectedIndexChanged += new System.EventHandler(this.cmbLLGName_SelectedIndexChanged);
            // 
            // lblLLGName
            // 
            this.lblLLGName.AutoSize = true;
            this.lblLLGName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLLGName.Location = new System.Drawing.Point(38, 20);
            this.lblLLGName.Name = "lblLLGName";
            this.lblLLGName.Size = new System.Drawing.Size(86, 19);
            this.lblLLGName.TabIndex = 2;
            this.lblLLGName.Text = "LLG Name";
            // 
            // pnlDistrict
            // 
            this.pnlDistrict.Controls.Add(this.cmbDistrictName);
            this.pnlDistrict.Controls.Add(this.lblDistrictName);
            this.pnlDistrict.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDistrict.Location = new System.Drawing.Point(0, 548);
            this.pnlDistrict.Name = "pnlDistrict";
            this.pnlDistrict.Size = new System.Drawing.Size(974, 52);
            this.pnlDistrict.TabIndex = 2;
            // 
            // cmbDistrictName
            // 
            this.cmbDistrictName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrictName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrictName.FormattingEnabled = true;
            this.cmbDistrictName.Location = new System.Drawing.Point(241, 15);
            this.cmbDistrictName.Name = "cmbDistrictName";
            this.cmbDistrictName.Size = new System.Drawing.Size(403, 28);
            this.cmbDistrictName.TabIndex = 1;
            this.cmbDistrictName.SelectedIndexChanged += new System.EventHandler(this.cmbDistrictName_SelectedIndexChanged);
            // 
            // lblDistrictName
            // 
            this.lblDistrictName.AutoSize = true;
            this.lblDistrictName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistrictName.Location = new System.Drawing.Point(38, 19);
            this.lblDistrictName.Name = "lblDistrictName";
            this.lblDistrictName.Size = new System.Drawing.Size(107, 19);
            this.lblDistrictName.TabIndex = 0;
            this.lblDistrictName.Text = "District Name";
            // 
            // pnlRegionProvince
            // 
            this.pnlRegionProvince.Controls.Add(this.lblRegionName);
            this.pnlRegionProvince.Controls.Add(this.cmbProvinceName);
            this.pnlRegionProvince.Controls.Add(this.lblProvinceName);
            this.pnlRegionProvince.Controls.Add(this.cmbRegionName);
            this.pnlRegionProvince.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRegionProvince.Location = new System.Drawing.Point(0, 443);
            this.pnlRegionProvince.Name = "pnlRegionProvince";
            this.pnlRegionProvince.Size = new System.Drawing.Size(974, 105);
            this.pnlRegionProvince.TabIndex = 1;
            // 
            // lblRegionName
            // 
            this.lblRegionName.AutoSize = true;
            this.lblRegionName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegionName.Location = new System.Drawing.Point(38, 15);
            this.lblRegionName.Name = "lblRegionName";
            this.lblRegionName.Size = new System.Drawing.Size(107, 19);
            this.lblRegionName.TabIndex = 0;
            this.lblRegionName.Text = "Region Name";
            // 
            // cmbProvinceName
            // 
            this.cmbProvinceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvinceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProvinceName.FormattingEnabled = true;
            this.cmbProvinceName.Location = new System.Drawing.Point(241, 64);
            this.cmbProvinceName.Name = "cmbProvinceName";
            this.cmbProvinceName.Size = new System.Drawing.Size(403, 28);
            this.cmbProvinceName.TabIndex = 3;
            this.cmbProvinceName.SelectedIndexChanged += new System.EventHandler(this.cmbProvinceName_SelectedIndexChanged);
            // 
            // lblProvinceName
            // 
            this.lblProvinceName.AutoSize = true;
            this.lblProvinceName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvinceName.Location = new System.Drawing.Point(38, 71);
            this.lblProvinceName.Name = "lblProvinceName";
            this.lblProvinceName.Size = new System.Drawing.Size(120, 19);
            this.lblProvinceName.TabIndex = 2;
            this.lblProvinceName.Text = "Province Name";
            // 
            // cmbRegionName
            // 
            this.cmbRegionName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRegionName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRegionName.FormattingEnabled = true;
            this.cmbRegionName.Location = new System.Drawing.Point(241, 7);
            this.cmbRegionName.Name = "cmbRegionName";
            this.cmbRegionName.Size = new System.Drawing.Size(403, 28);
            this.cmbRegionName.TabIndex = 1;
            this.cmbRegionName.SelectedIndexChanged += new System.EventHandler(this.cmbRegionName_SelectedIndexChanged);
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.Controls.Add(this.lblFirstName);
            this.pnlUserInfo.Controls.Add(this.txtOldProfileImageName);
            this.pnlUserInfo.Controls.Add(this.lblRole);
            this.pnlUserInfo.Controls.Add(this.lblPassword);
            this.pnlUserInfo.Controls.Add(this.cmbStatus);
            this.pnlUserInfo.Controls.Add(this.txtPassword);
            this.pnlUserInfo.Controls.Add(this.cmbRole);
            this.pnlUserInfo.Controls.Add(this.txtUserId);
            this.pnlUserInfo.Controls.Add(this.lblStatus);
            this.pnlUserInfo.Controls.Add(this.pbProfileImage);
            this.pnlUserInfo.Controls.Add(this.txtContactNumber);
            this.pnlUserInfo.Controls.Add(this.lblContactNumber);
            this.pnlUserInfo.Controls.Add(this.btnUploadProfileImage);
            this.pnlUserInfo.Controls.Add(this.txtEmailAddress);
            this.pnlUserInfo.Controls.Add(this.txtProfileImage);
            this.pnlUserInfo.Controls.Add(this.lblEmailAddress);
            this.pnlUserInfo.Controls.Add(this.lblProfileImage);
            this.pnlUserInfo.Controls.Add(this.txtFirstName);
            this.pnlUserInfo.Controls.Add(this.txtLastName);
            this.pnlUserInfo.Controls.Add(this.lblLastName);
            this.pnlUserInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlUserInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(974, 443);
            this.pnlUserInfo.TabIndex = 0;
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstName.Location = new System.Drawing.Point(38, 23);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(88, 19);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "First Name";
            // 
            // txtOldProfileImageName
            // 
            this.txtOldProfileImageName.Location = new System.Drawing.Point(675, 193);
            this.txtOldProfileImageName.Name = "txtOldProfileImageName";
            this.txtOldProfileImageName.Size = new System.Drawing.Size(116, 27);
            this.txtOldProfileImageName.TabIndex = 18;
            this.txtOldProfileImageName.Visible = false;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(38, 402);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(41, 19);
            this.lblRole.TabIndex = 15;
            this.lblRole.Text = "Role";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(38, 234);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(80, 19);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Password";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(241, 337);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(403, 28);
            this.cmbStatus.TabIndex = 14;
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(241, 230);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(403, 26);
            this.txtPassword.TabIndex = 10;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(241, 394);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(403, 28);
            this.cmbRole.TabIndex = 16;
            this.cmbRole.SelectedIndexChanged += new System.EventHandler(this.cmbRole_SelectedIndexChanged);
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(675, 161);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(116, 27);
            this.txtUserId.TabIndex = 17;
            this.txtUserId.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(38, 346);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(54, 19);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // pbProfileImage
            // 
            this.pbProfileImage.Location = new System.Drawing.Point(675, 19);
            this.pbProfileImage.Name = "pbProfileImage";
            this.pbProfileImage.Size = new System.Drawing.Size(117, 98);
            this.pbProfileImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbProfileImage.TabIndex = 88;
            this.pbProfileImage.TabStop = false;
            // 
            // txtContactNumber
            // 
            this.txtContactNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNumber.Location = new System.Drawing.Point(241, 285);
            this.txtContactNumber.MaxLength = 10;
            this.txtContactNumber.Name = "txtContactNumber";
            this.txtContactNumber.Size = new System.Drawing.Size(403, 26);
            this.txtContactNumber.TabIndex = 12;
            this.txtContactNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactNumber_KeyPress);
            // 
            // lblContactNumber
            // 
            this.lblContactNumber.AutoSize = true;
            this.lblContactNumber.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContactNumber.Location = new System.Drawing.Point(38, 288);
            this.lblContactNumber.Name = "lblContactNumber";
            this.lblContactNumber.Size = new System.Drawing.Size(127, 19);
            this.lblContactNumber.TabIndex = 11;
            this.lblContactNumber.Text = "Contact Number";
            // 
            // btnUploadProfileImage
            // 
            this.btnUploadProfileImage.Location = new System.Drawing.Point(675, 126);
            this.btnUploadProfileImage.Name = "btnUploadProfileImage";
            this.btnUploadProfileImage.Size = new System.Drawing.Size(117, 29);
            this.btnUploadProfileImage.TabIndex = 6;
            this.btnUploadProfileImage.Text = "Upload";
            this.btnUploadProfileImage.UseVisualStyleBackColor = true;
            this.btnUploadProfileImage.Click += new System.EventHandler(this.btnUploadProfileImage_Click);
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailAddress.Location = new System.Drawing.Point(241, 178);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(403, 26);
            this.txtEmailAddress.TabIndex = 8;
            // 
            // txtProfileImage
            // 
            this.txtProfileImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProfileImage.Location = new System.Drawing.Point(241, 126);
            this.txtProfileImage.Name = "txtProfileImage";
            this.txtProfileImage.Size = new System.Drawing.Size(403, 26);
            this.txtProfileImage.TabIndex = 5;
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailAddress.Location = new System.Drawing.Point(38, 182);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(113, 19);
            this.lblEmailAddress.TabIndex = 7;
            this.lblEmailAddress.Text = "Email Address";
            // 
            // lblProfileImage
            // 
            this.lblProfileImage.AutoSize = true;
            this.lblProfileImage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfileImage.Location = new System.Drawing.Point(38, 129);
            this.lblProfileImage.Name = "lblProfileImage";
            this.lblProfileImage.Size = new System.Drawing.Size(106, 19);
            this.lblProfileImage.TabIndex = 0;
            this.lblProfileImage.Text = "Profile Image";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(241, 19);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(403, 26);
            this.txtFirstName.TabIndex = 1;
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(241, 74);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(403, 26);
            this.txtLastName.TabIndex = 3;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastName.Location = new System.Drawing.Point(38, 78);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(86, 19);
            this.lblLastName.TabIndex = 2;
            this.lblLastName.Text = "Last Name";
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(995, 686);
            this.Controls.Add(this.pnlcontrol);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "User";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User";
            this.Load += new System.EventHandler(this.User_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlButton.ResumeLayout(false);
            this.pnlcontrol.ResumeLayout(false);
            this.pnlMKAAssign.ResumeLayout(false);
            this.pnlMKAAssign.PerformLayout();
            this.pnlPermission.ResumeLayout(false);
            this.pnlPermission.PerformLayout();
            this.pnlLocation.ResumeLayout(false);
            this.pnlLocation.PerformLayout();
            this.pnlLlgWard.ResumeLayout(false);
            this.pnlLlgWard.PerformLayout();
            this.pnlDistrict.ResumeLayout(false);
            this.pnlDistrict.PerformLayout();
            this.pnlRegionProvince.ResumeLayout(false);
            this.pnlRegionProvince.PerformLayout();
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfileImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofdProfileImage;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private FlowLayoutPanel pnlBottom;
        private Panel pnlcontrol;
        private Label lblPassword;
        private TextBox txtPassword;
        private TextBox txtUserId;
        private PictureBox pbProfileImage;
        private Panel pnlLlgWard;
        private ComboBox cmbProvinceName;
        private ComboBox cmbRegionName;
        private Label lblProvinceName;
        private Label lblRegionName;
        private Panel pnlPermission;
        private CheckBox chkDevices;
        private CheckBox chkUsers;
        private CheckBox chkForm11;
        private CheckBox chkRoleStat;
        private CheckBox chkWardRollBook;
        private Label lblPermissionModules;
        private CheckBox chkDatabaseConnect;
        private CheckBox chkGeoStructure;
        private CheckBox chkFile;
        private Panel pnlRegionProvince;
        private ComboBox cmbLocationName;
        private Label lblLocationName;
        private ComboBox cmbWardName;
        private Label lblWardName;
        private ComboBox cmbLLGName;
        private Label lblLLGName;
        private ComboBox cmbDistrictName;
        private Label lblDistrictName;
        private Button btnUploadProfileImage;
        private TextBox txtProfileImage;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblProfileImage;
        private Label lblEmailAddress;
        private TextBox txtEmailAddress;
        private Label lblContactNumber;
        private TextBox txtContactNumber;
        private Label lblStatus;
        private ComboBox cmbRole;
        private ComboBox cmbStatus;
        private Label lblRole;
        private TextBox txtOldProfileImageName;
        private Panel pnlUserInfo;
        private Panel pnlButton;
        private Panel pnlLocation;
        private Panel pnlDistrict;
        private Panel pnlMKAAssign;
        private Label label1;
        private ComboBox cmbMKALLGName;
        private Label lblMKALLGName;
        private ComboBox cmbMKADistrictName;
        private Label lblMKAWardName;
        private ComboBox cmbMKAWardName;
        private Label lblMKABio;
        private CheckBox chkMKAForm11;
        private CheckBox chkMKAWardRollBook;
        private CheckBox chkMKAGeoStructure;
        private CheckBox chkMKAFile;
        private CheckBox chkManageClan;
    }
}