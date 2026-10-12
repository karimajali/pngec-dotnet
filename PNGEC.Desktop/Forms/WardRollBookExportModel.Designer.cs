namespace PNGEC.Desktop.Forms
{
    partial class WardRollBookExportModel
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
            this.rdbVoterIdCard = new System.Windows.Forms.RadioButton();
            this.rdbVoterPhotoCard = new System.Windows.Forms.RadioButton();
            this.rdbWardRollBook = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.rdbWardPhotoRollBook = new System.Windows.Forms.RadioButton();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.gbDisability = new System.Windows.Forms.GroupBox();
            this.rdbDisabilityWith = new System.Windows.Forms.RadioButton();
            this.rdbDisabilityAll = new System.Windows.Forms.RadioButton();
            this.gbGender = new System.Windows.Forms.GroupBox();
            this.rdbGenderOther = new System.Windows.Forms.RadioButton();
            this.rdbGenderAll = new System.Windows.Forms.RadioButton();
            this.rdbGenderMale = new System.Windows.Forms.RadioButton();
            this.rdbGenderFemale = new System.Windows.Forms.RadioButton();
            this.rdbVoterEnvelope = new System.Windows.Forms.RadioButton();
            this.gbPhoto = new System.Windows.Forms.GroupBox();
            this.rdbPhotoWithout = new System.Windows.Forms.RadioButton();
            this.rdbPhotoWith = new System.Windows.Forms.RadioButton();
            this.rdbPhotoAll = new System.Windows.Forms.RadioButton();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.rdbWardRollDeletion = new System.Windows.Forms.RadioButton();
            this.gbDateRange = new System.Windows.Forms.GroupBox();
            this.pnlDateRange = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.rdbDateRange = new System.Windows.Forms.RadioButton();
            this.rdbDateAll = new System.Windows.Forms.RadioButton();
            this.gbElectorByIDs = new System.Windows.Forms.GroupBox();
            this.rdbElectorByIDsSelected = new System.Windows.Forms.RadioButton();
            this.rdbElectorByIDsAll = new System.Windows.Forms.RadioButton();
            this.rdbPreFieldWorkSummary = new System.Windows.Forms.RadioButton();
            this.rdbExisitngVoterDetails = new System.Windows.Forms.RadioButton();
            this.chkIncludePossibleDeletionReport = new System.Windows.Forms.CheckBox();
            this.gbAlphaSplit = new System.Windows.Forms.GroupBox();
            this.pnlAlphaSplit = new System.Windows.Forms.Panel();
            this.txtAlphaSplitEnd = new System.Windows.Forms.TextBox();
            this.txtAlphaSplitStart = new System.Windows.Forms.TextBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.rdbAlphaSplit = new System.Windows.Forms.RadioButton();
            this.rdbAlphaSplitAll = new System.Windows.Forms.RadioButton();
            this.txtReportTitle = new System.Windows.Forms.TextBox();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.chkWardRollEnvelope = new System.Windows.Forms.CheckBox();
            this.chkWardPreFieldWorkSummary = new System.Windows.Forms.CheckBox();
            this.rdbWardClanRoll = new System.Windows.Forms.RadioButton();
            this.chkWardRollBookLandscap = new System.Windows.Forms.CheckBox();
            this.pnlFilter.SuspendLayout();
            this.gbDisability.SuspendLayout();
            this.gbGender.SuspendLayout();
            this.gbPhoto.SuspendLayout();
            this.gbDateRange.SuspendLayout();
            this.pnlDateRange.SuspendLayout();
            this.gbElectorByIDs.SuspendLayout();
            this.gbAlphaSplit.SuspendLayout();
            this.pnlAlphaSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdbVoterIdCard
            // 
            this.rdbVoterIdCard.AutoSize = true;
            this.rdbVoterIdCard.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbVoterIdCard.Location = new System.Drawing.Point(39, 116);
            this.rdbVoterIdCard.Margin = new System.Windows.Forms.Padding(4);
            this.rdbVoterIdCard.Name = "rdbVoterIdCard";
            this.rdbVoterIdCard.Size = new System.Drawing.Size(152, 21);
            this.rdbVoterIdCard.TabIndex = 0;
            this.rdbVoterIdCard.Tag = "Ward Voter Id Card";
            this.rdbVoterIdCard.Text = "Ward Voter Id Card";
            this.rdbVoterIdCard.UseVisualStyleBackColor = true;
            this.rdbVoterIdCard.CheckedChanged += new System.EventHandler(this.rdbVoterIdCard_CheckedChanged);
            // 
            // rdbVoterPhotoCard
            // 
            this.rdbVoterPhotoCard.AutoSize = true;
            this.rdbVoterPhotoCard.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbVoterPhotoCard.Location = new System.Drawing.Point(39, 145);
            this.rdbVoterPhotoCard.Margin = new System.Windows.Forms.Padding(4);
            this.rdbVoterPhotoCard.Name = "rdbVoterPhotoCard";
            this.rdbVoterPhotoCard.Size = new System.Drawing.Size(179, 21);
            this.rdbVoterPhotoCard.TabIndex = 1;
            this.rdbVoterPhotoCard.Tag = "Ward Voter Photo Card";
            this.rdbVoterPhotoCard.Text = "Ward Voter Photo Card";
            this.rdbVoterPhotoCard.UseVisualStyleBackColor = true;
            this.rdbVoterPhotoCard.CheckedChanged += new System.EventHandler(this.rdbVoterPhotoCard_CheckedChanged);
            // 
            // rdbWardRollBook
            // 
            this.rdbWardRollBook.AutoSize = true;
            this.rdbWardRollBook.Checked = true;
            this.rdbWardRollBook.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbWardRollBook.Location = new System.Drawing.Point(39, 31);
            this.rdbWardRollBook.Margin = new System.Windows.Forms.Padding(4);
            this.rdbWardRollBook.Name = "rdbWardRollBook";
            this.rdbWardRollBook.Size = new System.Drawing.Size(130, 21);
            this.rdbWardRollBook.TabIndex = 2;
            this.rdbWardRollBook.TabStop = true;
            this.rdbWardRollBook.Tag = "Ward Roll Book";
            this.rdbWardRollBook.Text = "Ward Roll Book";
            this.rdbWardRollBook.UseVisualStyleBackColor = true;
            this.rdbWardRollBook.CheckedChanged += new System.EventHandler(this.rdbWardRollBook_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(39, 601);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(168, 37);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // rdbWardPhotoRollBook
            // 
            this.rdbWardPhotoRollBook.AutoSize = true;
            this.rdbWardPhotoRollBook.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbWardPhotoRollBook.Location = new System.Drawing.Point(39, 86);
            this.rdbWardPhotoRollBook.Margin = new System.Windows.Forms.Padding(4);
            this.rdbWardPhotoRollBook.Name = "rdbWardPhotoRollBook";
            this.rdbWardPhotoRollBook.Size = new System.Drawing.Size(172, 21);
            this.rdbWardPhotoRollBook.TabIndex = 5;
            this.rdbWardPhotoRollBook.Tag = "Ward Photo Roll Book";
            this.rdbWardPhotoRollBook.Text = "Ward Photo Roll Book";
            this.rdbWardPhotoRollBook.UseVisualStyleBackColor = true;
            this.rdbWardPhotoRollBook.CheckedChanged += new System.EventHandler(this.rdbWardPhotoRollBook_CheckedChanged);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.gbDisability);
            this.pnlFilter.Controls.Add(this.gbGender);
            this.pnlFilter.Location = new System.Drawing.Point(269, 16);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(4);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(459, 127);
            this.pnlFilter.TabIndex = 4;
            // 
            // gbDisability
            // 
            this.gbDisability.Controls.Add(this.rdbDisabilityWith);
            this.gbDisability.Controls.Add(this.rdbDisabilityAll);
            this.gbDisability.Location = new System.Drawing.Point(4, 73);
            this.gbDisability.Margin = new System.Windows.Forms.Padding(4);
            this.gbDisability.Name = "gbDisability";
            this.gbDisability.Padding = new System.Windows.Forms.Padding(4);
            this.gbDisability.Size = new System.Drawing.Size(443, 50);
            this.gbDisability.TabIndex = 9;
            this.gbDisability.TabStop = false;
            this.gbDisability.Text = "Disability";
            // 
            // rdbDisabilityWith
            // 
            this.rdbDisabilityWith.AutoSize = true;
            this.rdbDisabilityWith.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbDisabilityWith.Location = new System.Drawing.Point(81, 20);
            this.rdbDisabilityWith.Margin = new System.Windows.Forms.Padding(4);
            this.rdbDisabilityWith.Name = "rdbDisabilityWith";
            this.rdbDisabilityWith.Size = new System.Drawing.Size(121, 21);
            this.rdbDisabilityWith.TabIndex = 6;
            this.rdbDisabilityWith.Text = "With Disability";
            this.rdbDisabilityWith.UseVisualStyleBackColor = true;
            // 
            // rdbDisabilityAll
            // 
            this.rdbDisabilityAll.AutoSize = true;
            this.rdbDisabilityAll.Checked = true;
            this.rdbDisabilityAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbDisabilityAll.Location = new System.Drawing.Point(15, 20);
            this.rdbDisabilityAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbDisabilityAll.Name = "rdbDisabilityAll";
            this.rdbDisabilityAll.Size = new System.Drawing.Size(44, 21);
            this.rdbDisabilityAll.TabIndex = 5;
            this.rdbDisabilityAll.TabStop = true;
            this.rdbDisabilityAll.Text = "All";
            this.rdbDisabilityAll.UseVisualStyleBackColor = true;
            // 
            // gbGender
            // 
            this.gbGender.Controls.Add(this.rdbGenderOther);
            this.gbGender.Controls.Add(this.rdbGenderAll);
            this.gbGender.Controls.Add(this.rdbGenderMale);
            this.gbGender.Controls.Add(this.rdbGenderFemale);
            this.gbGender.Location = new System.Drawing.Point(4, 15);
            this.gbGender.Margin = new System.Windows.Forms.Padding(4);
            this.gbGender.Name = "gbGender";
            this.gbGender.Padding = new System.Windows.Forms.Padding(4);
            this.gbGender.Size = new System.Drawing.Size(443, 50);
            this.gbGender.TabIndex = 8;
            this.gbGender.TabStop = false;
            this.gbGender.Text = "Gender";
            // 
            // rdbGenderOther
            // 
            this.rdbGenderOther.AutoSize = true;
            this.rdbGenderOther.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbGenderOther.Location = new System.Drawing.Point(245, 20);
            this.rdbGenderOther.Margin = new System.Windows.Forms.Padding(4);
            this.rdbGenderOther.Name = "rdbGenderOther";
            this.rdbGenderOther.Size = new System.Drawing.Size(66, 21);
            this.rdbGenderOther.TabIndex = 7;
            this.rdbGenderOther.Tag = "Other";
            this.rdbGenderOther.Text = "Other";
            this.rdbGenderOther.UseVisualStyleBackColor = true;
            // 
            // rdbGenderAll
            // 
            this.rdbGenderAll.AutoSize = true;
            this.rdbGenderAll.Checked = true;
            this.rdbGenderAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbGenderAll.Location = new System.Drawing.Point(15, 20);
            this.rdbGenderAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbGenderAll.Name = "rdbGenderAll";
            this.rdbGenderAll.Size = new System.Drawing.Size(44, 21);
            this.rdbGenderAll.TabIndex = 4;
            this.rdbGenderAll.TabStop = true;
            this.rdbGenderAll.Text = "All";
            this.rdbGenderAll.UseVisualStyleBackColor = true;
            // 
            // rdbGenderMale
            // 
            this.rdbGenderMale.AutoSize = true;
            this.rdbGenderMale.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbGenderMale.Location = new System.Drawing.Point(81, 20);
            this.rdbGenderMale.Margin = new System.Windows.Forms.Padding(4);
            this.rdbGenderMale.Name = "rdbGenderMale";
            this.rdbGenderMale.Size = new System.Drawing.Size(59, 21);
            this.rdbGenderMale.TabIndex = 5;
            this.rdbGenderMale.Tag = "Male";
            this.rdbGenderMale.Text = "Male";
            this.rdbGenderMale.UseVisualStyleBackColor = true;
            // 
            // rdbGenderFemale
            // 
            this.rdbGenderFemale.AutoSize = true;
            this.rdbGenderFemale.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbGenderFemale.Location = new System.Drawing.Point(157, 20);
            this.rdbGenderFemale.Margin = new System.Windows.Forms.Padding(4);
            this.rdbGenderFemale.Name = "rdbGenderFemale";
            this.rdbGenderFemale.Size = new System.Drawing.Size(78, 21);
            this.rdbGenderFemale.TabIndex = 6;
            this.rdbGenderFemale.Tag = "Female";
            this.rdbGenderFemale.Text = "Female";
            this.rdbGenderFemale.UseVisualStyleBackColor = true;
            // 
            // rdbVoterEnvelope
            // 
            this.rdbVoterEnvelope.AutoSize = true;
            this.rdbVoterEnvelope.Location = new System.Drawing.Point(39, 256);
            this.rdbVoterEnvelope.Margin = new System.Windows.Forms.Padding(4);
            this.rdbVoterEnvelope.Name = "rdbVoterEnvelope";
            this.rdbVoterEnvelope.Size = new System.Drawing.Size(149, 20);
            this.rdbVoterEnvelope.TabIndex = 6;
            this.rdbVoterEnvelope.Text = "Ward Roll Envelope";
            this.rdbVoterEnvelope.UseVisualStyleBackColor = true;
            this.rdbVoterEnvelope.Visible = false;
            // 
            // gbPhoto
            // 
            this.gbPhoto.Controls.Add(this.rdbPhotoWithout);
            this.gbPhoto.Controls.Add(this.rdbPhotoWith);
            this.gbPhoto.Controls.Add(this.rdbPhotoAll);
            this.gbPhoto.Location = new System.Drawing.Point(273, 146);
            this.gbPhoto.Margin = new System.Windows.Forms.Padding(4);
            this.gbPhoto.Name = "gbPhoto";
            this.gbPhoto.Padding = new System.Windows.Forms.Padding(4);
            this.gbPhoto.Size = new System.Drawing.Size(443, 50);
            this.gbPhoto.TabIndex = 10;
            this.gbPhoto.TabStop = false;
            this.gbPhoto.Text = "Photo";
            // 
            // rdbPhotoWithout
            // 
            this.rdbPhotoWithout.AutoSize = true;
            this.rdbPhotoWithout.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbPhotoWithout.Location = new System.Drawing.Point(201, 20);
            this.rdbPhotoWithout.Margin = new System.Windows.Forms.Padding(4);
            this.rdbPhotoWithout.Name = "rdbPhotoWithout";
            this.rdbPhotoWithout.Size = new System.Drawing.Size(121, 21);
            this.rdbPhotoWithout.TabIndex = 7;
            this.rdbPhotoWithout.Text = "Without Photo";
            this.rdbPhotoWithout.UseVisualStyleBackColor = true;
            // 
            // rdbPhotoWith
            // 
            this.rdbPhotoWith.AutoSize = true;
            this.rdbPhotoWith.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbPhotoWith.Location = new System.Drawing.Point(81, 20);
            this.rdbPhotoWith.Margin = new System.Windows.Forms.Padding(4);
            this.rdbPhotoWith.Name = "rdbPhotoWith";
            this.rdbPhotoWith.Size = new System.Drawing.Size(101, 21);
            this.rdbPhotoWith.TabIndex = 6;
            this.rdbPhotoWith.Text = "With Photo";
            this.rdbPhotoWith.UseVisualStyleBackColor = true;
            // 
            // rdbPhotoAll
            // 
            this.rdbPhotoAll.AutoSize = true;
            this.rdbPhotoAll.Checked = true;
            this.rdbPhotoAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbPhotoAll.Location = new System.Drawing.Point(15, 20);
            this.rdbPhotoAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbPhotoAll.Name = "rdbPhotoAll";
            this.rdbPhotoAll.Size = new System.Drawing.Size(44, 21);
            this.rdbPhotoAll.TabIndex = 5;
            this.rdbPhotoAll.TabStop = true;
            this.rdbPhotoAll.Text = "All";
            this.rdbPhotoAll.UseVisualStyleBackColor = true;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.Location = new System.Drawing.Point(215, 601);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(168, 37);
            this.btnExportCsv.TabIndex = 11;
            this.btnExportCsv.Text = "Export to CSV";
            this.btnExportCsv.UseVisualStyleBackColor = true;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // rdbWardRollDeletion
            // 
            this.rdbWardRollDeletion.AutoSize = true;
            this.rdbWardRollDeletion.Location = new System.Drawing.Point(39, 175);
            this.rdbWardRollDeletion.Margin = new System.Windows.Forms.Padding(4);
            this.rdbWardRollDeletion.Name = "rdbWardRollDeletion";
            this.rdbWardRollDeletion.Size = new System.Drawing.Size(141, 20);
            this.rdbWardRollDeletion.TabIndex = 12;
            this.rdbWardRollDeletion.Text = "Ward Roll Deletion";
            this.rdbWardRollDeletion.UseVisualStyleBackColor = true;
            this.rdbWardRollDeletion.CheckedChanged += new System.EventHandler(this.rdbWardRollDeletion_CheckedChanged);
            // 
            // gbDateRange
            // 
            this.gbDateRange.Controls.Add(this.pnlDateRange);
            this.gbDateRange.Controls.Add(this.rdbDateRange);
            this.gbDateRange.Controls.Add(this.rdbDateAll);
            this.gbDateRange.Location = new System.Drawing.Point(273, 204);
            this.gbDateRange.Margin = new System.Windows.Forms.Padding(4);
            this.gbDateRange.Name = "gbDateRange";
            this.gbDateRange.Padding = new System.Windows.Forms.Padding(4);
            this.gbDateRange.Size = new System.Drawing.Size(443, 90);
            this.gbDateRange.TabIndex = 11;
            this.gbDateRange.TabStop = false;
            this.gbDateRange.Text = "Date Range";
            // 
            // pnlDateRange
            // 
            this.pnlDateRange.Controls.Add(this.dtpToDate);
            this.pnlDateRange.Controls.Add(this.dtpFromDate);
            this.pnlDateRange.Controls.Add(this.lblToDate);
            this.pnlDateRange.Controls.Add(this.lblFromDate);
            this.pnlDateRange.Location = new System.Drawing.Point(8, 47);
            this.pnlDateRange.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDateRange.Name = "pnlDateRange";
            this.pnlDateRange.Size = new System.Drawing.Size(427, 38);
            this.pnlDateRange.TabIndex = 14;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MM-yyyy";
            this.dtpToDate.Enabled = false;
            this.dtpToDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(261, 4);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(147, 27);
            this.dtpToDate.TabIndex = 10;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MM-yyyy";
            this.dtpFromDate.Enabled = false;
            this.dtpFromDate.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(67, 4);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFromDate.MaxDate = new System.DateTime(2021, 5, 12, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(147, 27);
            this.dtpFromDate.TabIndex = 9;
            this.dtpFromDate.Value = new System.DateTime(2021, 5, 12, 0, 0, 0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Arial", 10F);
            this.lblToDate.Location = new System.Drawing.Point(223, 10);
            this.lblToDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(25, 19);
            this.lblToDate.TabIndex = 8;
            this.lblToDate.Text = "To";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Arial", 10F);
            this.lblFromDate.Location = new System.Drawing.Point(4, 10);
            this.lblFromDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(47, 19);
            this.lblFromDate.TabIndex = 7;
            this.lblFromDate.Text = "From";
            // 
            // rdbDateRange
            // 
            this.rdbDateRange.AutoSize = true;
            this.rdbDateRange.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbDateRange.Location = new System.Drawing.Point(81, 20);
            this.rdbDateRange.Margin = new System.Windows.Forms.Padding(4);
            this.rdbDateRange.Name = "rdbDateRange";
            this.rdbDateRange.Size = new System.Drawing.Size(107, 21);
            this.rdbDateRange.TabIndex = 6;
            this.rdbDateRange.Text = "Date Range";
            this.rdbDateRange.UseVisualStyleBackColor = true;
            this.rdbDateRange.CheckedChanged += new System.EventHandler(this.rdbDateRange_CheckedChanged);
            // 
            // rdbDateAll
            // 
            this.rdbDateAll.AutoSize = true;
            this.rdbDateAll.Checked = true;
            this.rdbDateAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbDateAll.Location = new System.Drawing.Point(15, 20);
            this.rdbDateAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbDateAll.Name = "rdbDateAll";
            this.rdbDateAll.Size = new System.Drawing.Size(44, 21);
            this.rdbDateAll.TabIndex = 5;
            this.rdbDateAll.TabStop = true;
            this.rdbDateAll.Text = "All";
            this.rdbDateAll.UseVisualStyleBackColor = true;
            this.rdbDateAll.CheckedChanged += new System.EventHandler(this.rdbDateAll_CheckedChanged);
            // 
            // gbElectorByIDs
            // 
            this.gbElectorByIDs.Controls.Add(this.rdbElectorByIDsSelected);
            this.gbElectorByIDs.Controls.Add(this.rdbElectorByIDsAll);
            this.gbElectorByIDs.Location = new System.Drawing.Point(273, 302);
            this.gbElectorByIDs.Margin = new System.Windows.Forms.Padding(4);
            this.gbElectorByIDs.Name = "gbElectorByIDs";
            this.gbElectorByIDs.Padding = new System.Windows.Forms.Padding(4);
            this.gbElectorByIDs.Size = new System.Drawing.Size(443, 50);
            this.gbElectorByIDs.TabIndex = 11;
            this.gbElectorByIDs.TabStop = false;
            this.gbElectorByIDs.Text = "Elector By IDs";
            // 
            // rdbElectorByIDsSelected
            // 
            this.rdbElectorByIDsSelected.AutoSize = true;
            this.rdbElectorByIDsSelected.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbElectorByIDsSelected.Location = new System.Drawing.Point(81, 20);
            this.rdbElectorByIDsSelected.Margin = new System.Windows.Forms.Padding(4);
            this.rdbElectorByIDsSelected.Name = "rdbElectorByIDsSelected";
            this.rdbElectorByIDsSelected.Size = new System.Drawing.Size(86, 21);
            this.rdbElectorByIDsSelected.TabIndex = 6;
            this.rdbElectorByIDsSelected.Text = "Selected";
            this.rdbElectorByIDsSelected.UseVisualStyleBackColor = true;
            this.rdbElectorByIDsSelected.CheckedChanged += new System.EventHandler(this.rdbElectorByIDsSelected_CheckedChanged);
            // 
            // rdbElectorByIDsAll
            // 
            this.rdbElectorByIDsAll.AutoSize = true;
            this.rdbElectorByIDsAll.Checked = true;
            this.rdbElectorByIDsAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbElectorByIDsAll.Location = new System.Drawing.Point(15, 20);
            this.rdbElectorByIDsAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbElectorByIDsAll.Name = "rdbElectorByIDsAll";
            this.rdbElectorByIDsAll.Size = new System.Drawing.Size(44, 21);
            this.rdbElectorByIDsAll.TabIndex = 5;
            this.rdbElectorByIDsAll.TabStop = true;
            this.rdbElectorByIDsAll.Text = "All";
            this.rdbElectorByIDsAll.UseVisualStyleBackColor = true;
            this.rdbElectorByIDsAll.CheckedChanged += new System.EventHandler(this.rdbElectorByIDsAll_CheckedChanged);
            // 
            // rdbPreFieldWorkSummary
            // 
            this.rdbPreFieldWorkSummary.AutoSize = true;
            this.rdbPreFieldWorkSummary.Location = new System.Drawing.Point(39, 229);
            this.rdbPreFieldWorkSummary.Margin = new System.Windows.Forms.Padding(4);
            this.rdbPreFieldWorkSummary.Name = "rdbPreFieldWorkSummary";
            this.rdbPreFieldWorkSummary.Size = new System.Drawing.Size(214, 20);
            this.rdbPreFieldWorkSummary.TabIndex = 13;
            this.rdbPreFieldWorkSummary.Text = "Ward Pre-Field Work Summary";
            this.rdbPreFieldWorkSummary.UseVisualStyleBackColor = true;
            this.rdbPreFieldWorkSummary.Visible = false;
            this.rdbPreFieldWorkSummary.CheckedChanged += new System.EventHandler(this.rdbPreFieldWorkSummary_CheckedChanged);
            // 
            // rdbExisitngVoterDetails
            // 
            this.rdbExisitngVoterDetails.AutoSize = true;
            this.rdbExisitngVoterDetails.Location = new System.Drawing.Point(39, 202);
            this.rdbExisitngVoterDetails.Margin = new System.Windows.Forms.Padding(4);
            this.rdbExisitngVoterDetails.Name = "rdbExisitngVoterDetails";
            this.rdbExisitngVoterDetails.Size = new System.Drawing.Size(154, 20);
            this.rdbExisitngVoterDetails.TabIndex = 14;
            this.rdbExisitngVoterDetails.Text = "Exisitng Voter Details";
            this.rdbExisitngVoterDetails.UseVisualStyleBackColor = true;
            this.rdbExisitngVoterDetails.CheckedChanged += new System.EventHandler(this.rdbExisitngVoterDetails_CheckedChanged);
            // 
            // chkIncludePossibleDeletionReport
            // 
            this.chkIncludePossibleDeletionReport.AutoSize = true;
            this.chkIncludePossibleDeletionReport.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludePossibleDeletionReport.Location = new System.Drawing.Point(289, 452);
            this.chkIncludePossibleDeletionReport.Margin = new System.Windows.Forms.Padding(4);
            this.chkIncludePossibleDeletionReport.Name = "chkIncludePossibleDeletionReport";
            this.chkIncludePossibleDeletionReport.Size = new System.Drawing.Size(267, 23);
            this.chkIncludePossibleDeletionReport.TabIndex = 15;
            this.chkIncludePossibleDeletionReport.Text = "Include Possible Deletion Report";
            this.chkIncludePossibleDeletionReport.UseVisualStyleBackColor = true;
            // 
            // gbAlphaSplit
            // 
            this.gbAlphaSplit.Controls.Add(this.pnlAlphaSplit);
            this.gbAlphaSplit.Controls.Add(this.rdbAlphaSplit);
            this.gbAlphaSplit.Controls.Add(this.rdbAlphaSplitAll);
            this.gbAlphaSplit.Location = new System.Drawing.Point(273, 359);
            this.gbAlphaSplit.Margin = new System.Windows.Forms.Padding(4);
            this.gbAlphaSplit.Name = "gbAlphaSplit";
            this.gbAlphaSplit.Padding = new System.Windows.Forms.Padding(4);
            this.gbAlphaSplit.Size = new System.Drawing.Size(443, 90);
            this.gbAlphaSplit.TabIndex = 15;
            this.gbAlphaSplit.TabStop = false;
            this.gbAlphaSplit.Text = "Alpha Split";
            // 
            // pnlAlphaSplit
            // 
            this.pnlAlphaSplit.Controls.Add(this.txtAlphaSplitEnd);
            this.pnlAlphaSplit.Controls.Add(this.txtAlphaSplitStart);
            this.pnlAlphaSplit.Controls.Add(this.lblEnd);
            this.pnlAlphaSplit.Controls.Add(this.lblStart);
            this.pnlAlphaSplit.Location = new System.Drawing.Point(8, 47);
            this.pnlAlphaSplit.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAlphaSplit.Name = "pnlAlphaSplit";
            this.pnlAlphaSplit.Size = new System.Drawing.Size(427, 38);
            this.pnlAlphaSplit.TabIndex = 14;
            // 
            // txtAlphaSplitEnd
            // 
            this.txtAlphaSplitEnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAlphaSplitEnd.Enabled = false;
            this.txtAlphaSplitEnd.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlphaSplitEnd.Location = new System.Drawing.Point(223, 6);
            this.txtAlphaSplitEnd.Margin = new System.Windows.Forms.Padding(4);
            this.txtAlphaSplitEnd.MaxLength = 1;
            this.txtAlphaSplitEnd.Name = "txtAlphaSplitEnd";
            this.txtAlphaSplitEnd.Size = new System.Drawing.Size(92, 27);
            this.txtAlphaSplitEnd.TabIndex = 17;
            // 
            // txtAlphaSplitStart
            // 
            this.txtAlphaSplitStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAlphaSplitStart.Enabled = false;
            this.txtAlphaSplitStart.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlphaSplitStart.Location = new System.Drawing.Point(63, 6);
            this.txtAlphaSplitStart.Margin = new System.Windows.Forms.Padding(4);
            this.txtAlphaSplitStart.MaxLength = 1;
            this.txtAlphaSplitStart.Name = "txtAlphaSplitStart";
            this.txtAlphaSplitStart.Size = new System.Drawing.Size(92, 27);
            this.txtAlphaSplitStart.TabIndex = 16;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Arial", 10F);
            this.lblEnd.Location = new System.Drawing.Point(171, 10);
            this.lblEnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(38, 19);
            this.lblEnd.TabIndex = 8;
            this.lblEnd.Text = "End";
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Arial", 10F);
            this.lblStart.Location = new System.Drawing.Point(4, 10);
            this.lblStart.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(43, 19);
            this.lblStart.TabIndex = 7;
            this.lblStart.Text = "Start";
            // 
            // rdbAlphaSplit
            // 
            this.rdbAlphaSplit.AutoSize = true;
            this.rdbAlphaSplit.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbAlphaSplit.Location = new System.Drawing.Point(81, 20);
            this.rdbAlphaSplit.Margin = new System.Windows.Forms.Padding(4);
            this.rdbAlphaSplit.Name = "rdbAlphaSplit";
            this.rdbAlphaSplit.Size = new System.Drawing.Size(97, 21);
            this.rdbAlphaSplit.TabIndex = 6;
            this.rdbAlphaSplit.Text = "Alpha Split";
            this.rdbAlphaSplit.UseVisualStyleBackColor = true;
            this.rdbAlphaSplit.CheckedChanged += new System.EventHandler(this.rdbAlphaSplit_CheckedChanged);
            // 
            // rdbAlphaSplitAll
            // 
            this.rdbAlphaSplitAll.AutoSize = true;
            this.rdbAlphaSplitAll.Checked = true;
            this.rdbAlphaSplitAll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbAlphaSplitAll.Location = new System.Drawing.Point(15, 20);
            this.rdbAlphaSplitAll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbAlphaSplitAll.Name = "rdbAlphaSplitAll";
            this.rdbAlphaSplitAll.Size = new System.Drawing.Size(44, 21);
            this.rdbAlphaSplitAll.TabIndex = 5;
            this.rdbAlphaSplitAll.TabStop = true;
            this.rdbAlphaSplitAll.Text = "All";
            this.rdbAlphaSplitAll.UseVisualStyleBackColor = true;
            this.rdbAlphaSplitAll.CheckedChanged += new System.EventHandler(this.rdbAlphaSplitAll_CheckedChanged);
            // 
            // txtReportTitle
            // 
            this.txtReportTitle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReportTitle.Enabled = false;
            this.txtReportTitle.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportTitle.Location = new System.Drawing.Point(401, 548);
            this.txtReportTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtReportTitle.Name = "txtReportTitle";
            this.txtReportTitle.Size = new System.Drawing.Size(305, 27);
            this.txtReportTitle.TabIndex = 19;
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Font = new System.Drawing.Font("Arial", 10F);
            this.lblReportTitle.Location = new System.Drawing.Point(285, 551);
            this.lblReportTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(90, 19);
            this.lblReportTitle.TabIndex = 18;
            this.lblReportTitle.Text = "Report Title";
            // 
            // chkWardRollEnvelope
            // 
            this.chkWardRollEnvelope.AutoSize = true;
            this.chkWardRollEnvelope.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWardRollEnvelope.Location = new System.Drawing.Point(289, 484);
            this.chkWardRollEnvelope.Margin = new System.Windows.Forms.Padding(4);
            this.chkWardRollEnvelope.Name = "chkWardRollEnvelope";
            this.chkWardRollEnvelope.Size = new System.Drawing.Size(173, 23);
            this.chkWardRollEnvelope.TabIndex = 20;
            this.chkWardRollEnvelope.Text = "Ward Roll Envelope";
            this.chkWardRollEnvelope.UseVisualStyleBackColor = true;
            // 
            // chkWardPreFieldWorkSummary
            // 
            this.chkWardPreFieldWorkSummary.AutoSize = true;
            this.chkWardPreFieldWorkSummary.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWardPreFieldWorkSummary.Location = new System.Drawing.Point(289, 516);
            this.chkWardPreFieldWorkSummary.Margin = new System.Windows.Forms.Padding(4);
            this.chkWardPreFieldWorkSummary.Name = "chkWardPreFieldWorkSummary";
            this.chkWardPreFieldWorkSummary.Size = new System.Drawing.Size(263, 23);
            this.chkWardPreFieldWorkSummary.TabIndex = 21;
            this.chkWardPreFieldWorkSummary.Text = "Ward Pre-Field Work Summary";
            this.chkWardPreFieldWorkSummary.UseVisualStyleBackColor = true;
            // 
            // rdbWardClanRoll
            // 
            this.rdbWardClanRoll.AutoSize = true;
            this.rdbWardClanRoll.Font = new System.Drawing.Font("Arial", 9F);
            this.rdbWardClanRoll.Location = new System.Drawing.Point(39, 58);
            this.rdbWardClanRoll.Margin = new System.Windows.Forms.Padding(4);
            this.rdbWardClanRoll.Name = "rdbWardClanRoll";
            this.rdbWardClanRoll.Size = new System.Drawing.Size(164, 21);
            this.rdbWardClanRoll.TabIndex = 22;
            this.rdbWardClanRoll.Tag = "Ward Clan Roll Book";
            this.rdbWardClanRoll.Text = "Ward Clan Roll Book";
            this.rdbWardClanRoll.UseVisualStyleBackColor = true;
            this.rdbWardClanRoll.CheckedChanged += new System.EventHandler(this.rdbWardClanRoll_CheckedChanged);
            // 
            // chkWardRollBookLandscap
            // 
            this.chkWardRollBookLandscap.AutoSize = true;
            this.chkWardRollBookLandscap.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWardRollBookLandscap.Location = new System.Drawing.Point(533, 608);
            this.chkWardRollBookLandscap.Margin = new System.Windows.Forms.Padding(4);
            this.chkWardRollBookLandscap.Name = "chkWardRollBookLandscap";
            this.chkWardRollBookLandscap.Size = new System.Drawing.Size(187, 23);
            this.chkWardRollBookLandscap.TabIndex = 23;
            this.chkWardRollBookLandscap.Text = "Ward Roll Landscape";
            this.chkWardRollBookLandscap.UseVisualStyleBackColor = true;
            // 
            // WardRollBookExportModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 647);
            this.Controls.Add(this.chkWardRollBookLandscap);
            this.Controls.Add(this.rdbWardClanRoll);
            this.Controls.Add(this.chkWardPreFieldWorkSummary);
            this.Controls.Add(this.chkWardRollEnvelope);
            this.Controls.Add(this.txtReportTitle);
            this.Controls.Add(this.lblReportTitle);
            this.Controls.Add(this.gbAlphaSplit);
            this.Controls.Add(this.chkIncludePossibleDeletionReport);
            this.Controls.Add(this.rdbExisitngVoterDetails);
            this.Controls.Add(this.rdbPreFieldWorkSummary);
            this.Controls.Add(this.gbElectorByIDs);
            this.Controls.Add(this.gbDateRange);
            this.Controls.Add(this.rdbWardRollDeletion);
            this.Controls.Add(this.btnExportCsv);
            this.Controls.Add(this.gbPhoto);
            this.Controls.Add(this.rdbVoterEnvelope);
            this.Controls.Add(this.rdbWardPhotoRollBook);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rdbWardRollBook);
            this.Controls.Add(this.rdbVoterPhotoCard);
            this.Controls.Add(this.rdbVoterIdCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WardRollBookExportModel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export";
            this.Load += new System.EventHandler(this.WardRollBookExportModel_Load);
            this.pnlFilter.ResumeLayout(false);
            this.gbDisability.ResumeLayout(false);
            this.gbDisability.PerformLayout();
            this.gbGender.ResumeLayout(false);
            this.gbGender.PerformLayout();
            this.gbPhoto.ResumeLayout(false);
            this.gbPhoto.PerformLayout();
            this.gbDateRange.ResumeLayout(false);
            this.gbDateRange.PerformLayout();
            this.pnlDateRange.ResumeLayout(false);
            this.pnlDateRange.PerformLayout();
            this.gbElectorByIDs.ResumeLayout(false);
            this.gbElectorByIDs.PerformLayout();
            this.gbAlphaSplit.ResumeLayout(false);
            this.gbAlphaSplit.PerformLayout();
            this.pnlAlphaSplit.ResumeLayout(false);
            this.pnlAlphaSplit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnExport;
        internal System.Windows.Forms.RadioButton rdbVoterIdCard;
        internal System.Windows.Forms.RadioButton rdbVoterPhotoCard;
        internal System.Windows.Forms.RadioButton rdbWardRollBook;
        public System.Windows.Forms.RadioButton rdbWardPhotoRollBook;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.GroupBox gbDisability;
        internal System.Windows.Forms.RadioButton rdbDisabilityWith;
        internal System.Windows.Forms.RadioButton rdbDisabilityAll;
        private System.Windows.Forms.GroupBox gbGender;
        internal System.Windows.Forms.RadioButton rdbGenderAll;
        internal System.Windows.Forms.RadioButton rdbGenderMale;
        internal System.Windows.Forms.RadioButton rdbGenderFemale;
        private System.Windows.Forms.GroupBox gbPhoto;
        internal System.Windows.Forms.RadioButton rdbPhotoWith;
        internal System.Windows.Forms.RadioButton rdbPhotoAll;
        internal System.Windows.Forms.RadioButton rdbPhotoWithout;
        internal System.Windows.Forms.RadioButton rdbVoterEnvelope;
        private System.Windows.Forms.Button btnExportCsv;
        internal System.Windows.Forms.RadioButton rdbWardRollDeletion;
        private System.Windows.Forms.GroupBox gbDateRange;
        internal System.Windows.Forms.RadioButton rdbDateRange;
        internal System.Windows.Forms.RadioButton rdbDateAll;
        private System.Windows.Forms.Panel pnlDateRange;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.GroupBox gbElectorByIDs;
        internal System.Windows.Forms.RadioButton rdbElectorByIDsSelected;
        internal System.Windows.Forms.RadioButton rdbElectorByIDsAll;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        internal System.Windows.Forms.RadioButton rdbPreFieldWorkSummary;
        internal System.Windows.Forms.RadioButton rdbExisitngVoterDetails;
        internal System.Windows.Forms.CheckBox chkIncludePossibleDeletionReport;
        private System.Windows.Forms.GroupBox gbAlphaSplit;
        private System.Windows.Forms.Panel pnlAlphaSplit;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblStart;
        internal System.Windows.Forms.RadioButton rdbAlphaSplit;
        internal System.Windows.Forms.RadioButton rdbAlphaSplitAll;
        internal System.Windows.Forms.TextBox txtAlphaSplitEnd;
        internal System.Windows.Forms.TextBox txtAlphaSplitStart;
        internal System.Windows.Forms.TextBox txtReportTitle;
        private System.Windows.Forms.Label lblReportTitle;
        internal System.Windows.Forms.CheckBox chkWardRollEnvelope;
        internal System.Windows.Forms.CheckBox chkWardPreFieldWorkSummary;
        internal System.Windows.Forms.RadioButton rdbGenderOther;
        internal System.Windows.Forms.RadioButton rdbWardClanRoll;
        internal System.Windows.Forms.CheckBox chkWardRollBookLandscap;
    }
}