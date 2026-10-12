using Newtonsoft.Json;
using PNGEC.API.Domain.Document;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class DocumentList : Form
    {
        #region variable declaration
        /// <summary>
        /// The ofd
        /// </summary>
        private OpenFileDialog ofd = null;

        /// <summary>
        /// The add document
        /// </summary>
        private AddDocument addDocument = null;

        /// <summary>
        /// The model
        /// </summary>
        private DocumentModel model;

        /// <summary>
        /// The page list
        /// </summary>
        private PageList pageList = null;

        /// <summary>
        /// The BTN pager
        /// </summary>
        private Button btnPager = new Button();

        /// <summary>
        /// The page size value
        /// </summary>
        private int pageSizeValue;

        /// <summary>
        /// The page size array
        /// </summary>
        private string[] pageSizeArray = { "15", "50", "100", "500" };

        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentList"/> class.
        /// </summary>
        public DocumentList()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the DocumentList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DocumentList_Load(object sender, System.EventArgs e)
        {
            dgDocuments.CellContentClick -= new DataGridViewCellEventHandler(dgDocuments_CellContentClick);
            dgDocuments.CellContentClick += new DataGridViewCellEventHandler(dgDocuments_CellContentClick);

            FillDocumentTypeCombobox();

            LoadData();
        }

        /// <summary>
        /// Handles the Click event of the btnUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                ofd = new OpenFileDialog();
                ofd.Title = "Upload Document";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.txt;*.pdf;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    addDocument = new AddDocument();
                    DialogResult dialogResult = addDocument.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        this.AddRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "btnUploadProfileImage_Click", ex);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the dgDocuments control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgDocuments.Columns[e.ColumnIndex].Name == "Delete")
            {
                DocumentModel model = (DocumentModel)dgDocuments.Rows[e.RowIndex].DataBoundItem;
                if (model != null && (model.Type == 3 || model.Type == 4))
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the record?", "PNGEC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.DeleteDocument(model);
                    }

                }
                else
                {
                    MessageBox.Show("You can delete only Training & Manuals and Others Type Documents", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (e.RowIndex >= 0 && dgDocuments.Columns[e.ColumnIndex].Name == "Download")
            {
                DocumentModel model = (DocumentModel)dgDocuments.Rows[e.RowIndex].DataBoundItem;
                if (model != null)
                {
                    var file = dgDocuments.Rows[e.RowIndex].Cells["FileUrl"].Value;
                    var fileName = dgDocuments.Rows[e.RowIndex].Cells["FileName"].Value;
                    DownloadFile(file, fileName.ToString());
                }
                else
                {
                    MessageBox.Show("You can download only Training & Manuals and Others Type Documents", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData("Search");
            //if (documentLists != null && documentLists.Count > 0)
            //{
            //    List<DocumentModel> objDocumentList = new List<DocumentModel>();
            //    objDocumentList = documentLists;

            //    string fileName = txtDocumentName.Text;
            //    int documentType = Convert.ToInt16(cmbDocumentType.SelectedValue);

            //    if (!string.IsNullOrWhiteSpace(fileName) && documentType > 0)
            //    {
            //        objDocumentList = documentLists.Where(c => c.FileName.ToLower().Contains(fileName.ToLower()) && c.Type == documentType).ToList();
            //    }
            //    else if (!string.IsNullOrWhiteSpace(fileName))
            //    {
            //        objDocumentList = documentLists.Where(c => c.FileName.ToLower().Contains(fileName.ToLower())).ToList();
            //    }
            //    else if (documentType > 0)
            //    {
            //        objDocumentList = documentLists.Where(c => c.Type == documentType).ToList();
            //    }
            //    BindDocumentGird(objDocumentList);
            //}
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageSizeValue = Convert.ToInt32(cmbPageSize.SelectedValue);
            LoadData("PageSize");
        }

        /// <summary>
        /// Handles the Click event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Page_Click(object sender, EventArgs e)
        {
            btnPager = (sender as Button);
            LoadData();
        }

        #endregion

        #region Methods
        #region Load Data

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <returns></returns>
        private async void LoadData(string eventName = "")
        {
            pageList = new PageList();

            // Pagination Filed
            string pageIndex = string.Empty;
            if (eventName == "Search" || eventName == "PageSize")
            {
                pageIndex = "1";
            }
            else
            {
                pageIndex = btnPager.Name.Equals("") ? "1" : btnPager.Name;
            }
            pageList.PageNo = int.Parse(pageIndex);
            pageList.PageSize = pageSizeValue.Equals(0) ? Convert.ToInt32(pageSizeArray.First()) : pageSizeValue;
            pageList.KeyWord = txtDocumentName.Text;
            pageList.DocumentType = Convert.ToInt16(cmbDocumentType.SelectedValue);
            //pageList.SortField = "DocumentId";
            //pageList.SortOrder = "DESC";
            if (pageList != null)
            {
                Response<DocumentModel> response = await GenericHttpClient.PostRequest<DocumentModel>("Document/GetDocumentList", pageList);
                if (response.Status)
                {
                    if (response.Data.ListItems.ToList() != null)
                    {
                        foreach (var item in response.Data.ListItems.ToList())
                        {
                            DateTime utcDate = DateTime.SpecifyKind(item.CreatedDate, DateTimeKind.Utc);
                            DateTime localDate = utcDate.ToLocalTime();
                            item.CreatedDateString = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
                        }
                        BindDocumentGird(response.Data.ListItems.ToList());
                        PopulatePager(response.Data.TotalRecordCount, pageList.PageNo, pageList.PageSize);
                    }
                }
            }
        }
        #endregion

        #region Bind Document Gird

        /// <summary>
        /// Binds the document gird.
        /// </summary>
        /// <param name="lstDocument">The LST document.</param>
        private void BindDocumentGird(List<DocumentModel> lstDocument)
        {
            try
            {
                if (this.dgDocuments.Columns.Count > 0)
                {
                    this.dgDocuments.Columns.Clear();
                }

                this.dgDocuments.DataSource = null;
                this.dgDocuments.DataSource = lstDocument;
                this.dgDocuments.Columns["DocumentId"].Visible = false;
                this.dgDocuments.Columns["Type"].Visible = false;
                this.dgDocuments.Columns["CreatedDateString"].HeaderText = "Date Created";
                this.dgDocuments.Columns["DocumentTypeName"].HeaderText = "Type";
                this.dgDocuments.Columns["FileName"].HeaderText = "File Name";
                this.dgDocuments.Columns["Description"].HeaderText = "Description";
                this.dgDocuments.Columns["ActionBy"].Visible = false;
                this.dgDocuments.Columns["ResponseCode"].Visible = false;
                this.dgDocuments.Columns["FileImage"].Visible = false;
                this.dgDocuments.Columns["FileUrl"].Visible = false;
                this.dgDocuments.Columns["CreatedDate"].Visible = false;
                this.dgDocuments.Columns["CreatedBy"].Visible = false;
                this.dgDocuments.Columns["ModifiedDate"].Visible = false;
                this.dgDocuments.Columns["ModifiedDateString"].Visible = false;
                this.dgDocuments.Columns["ModifiedBy"].Visible = false;

                #region Delete Button
                DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
                deleteButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                deleteButton.FlatStyle = FlatStyle.Standard;
                deleteButton.HeaderText = "Delete";
                deleteButton.Name = "Delete";
                deleteButton.Text = "Delete";
                deleteButton.UseColumnTextForButtonValue = true;
                deleteButton.Width = 60;
                if (dgDocuments.Columns["Delete"] == null)
                {
                    dgDocuments.Columns.Insert(dgDocuments.ColumnCount, deleteButton);
                }
                #endregion

                #region Download Button
                DataGridViewButtonColumn downloadButton = new DataGridViewButtonColumn();
                downloadButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                downloadButton.FlatStyle = FlatStyle.Standard;
                downloadButton.HeaderText = "Download";
                downloadButton.Name = "Download";
                downloadButton.Text = "Download";
                downloadButton.UseColumnTextForButtonValue = true;
                downloadButton.Width = 80;
                if (dgDocuments.Columns["Download"] == null)
                {
                    dgDocuments.Columns.Insert(dgDocuments.ColumnCount, downloadButton);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("List Documents", "BindDocumentGird", ex);
            }
        }
        #endregion

        #region Add Document
        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <returns></returns>
        private async void AddRecord()
        {
            try
            {
                //this.Cursor = Cursors.WaitCursor;

                model = new DocumentModel();
                model.Type = Convert.ToInt16(addDocument.cmbType.SelectedValue);
                model.Description = addDocument.txtDescription.Text;
                if (model != null)
                {
                    Response responseData = null;

                    byte[] bytes = null;

                    if (ofd != null && !string.IsNullOrWhiteSpace(ofd.FileName))
                    {
                        bytes = System.IO.File.ReadAllBytes(ofd.FileName);
                    }

                    MultipartFormDataContent formContent;

                    //if (bytes != null && bytes.Length > 0)
                    //{
                    formContent = new MultipartFormDataContent
                    {
                        {new StringContent(model.Type.ToString()),"Type"},
                        {new StringContent(model.Description), "Description"},
                        {new StreamContent(new MemoryStream(bytes)),"FileImage", ofd.SafeFileName},
                        {new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()), "CreatedBy"}
                    };
                    //}

                    GlobalsModel.mainMaster.ShowLoader();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Globals.serverURL);
                        using (HttpResponseMessage response = client.PostAsync("Document/AddDeleteDocument", formContent).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                responseData = JsonConvert.DeserializeObject<Response>(responseBody);
                            }
                        }
                    }

                    GlobalsModel.mainMaster.HideLoader();
                    if (responseData.Data != null && responseData.Status)
                    {
                        MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        ////this.DialogResult = DialogResult.None;
                        MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "AddUpdateRecord", ex);
            }
        }
        #endregion

        #region Delete Document

        /// <summary>
        /// Deletes the document.
        /// </summary>
        private async void DeleteDocument(DocumentModel model)
        {
            try
            {
                if (model != null)
                {
                    Response responseData = null;

                    MultipartFormDataContent formContent;

                    formContent = new MultipartFormDataContent
                    {
                        {new StringContent(model.DocumentId.ToString()),"DocumentId"},
                        {new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()), "ModifiedBy"}
                    };

                    GlobalsModel.mainMaster.ShowLoader();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Globals.serverURL);
                        using (HttpResponseMessage response = client.PostAsync("Document/AddDeleteDocument", formContent).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                responseData = JsonConvert.DeserializeObject<Response>(responseBody);
                            }
                        }
                    }

                    GlobalsModel.mainMaster.HideLoader();
                    if (responseData.Data != null && responseData.Status)
                    {
                        MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        ////this.DialogResult = DialogResult.None;
                        MessageBox.Show(responseData.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalsModel.mainMaster.HideLoader();
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("User Form", "AddUpdateRecord", ex);
            }
        }
        #endregion

        #region download file


        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isSHowMesage">if set to <c>true</c> [is s how mesage].</param>
        private void DownloadFile(object file, string fileName = "downloaded_file", bool isSHowMesage = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(file)) && !string.IsNullOrWhiteSpace(Convert.ToString(file)))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = string.Format(@"{0}", fileName);
                    saveFileDialog.DefaultExt = GetExtension(fileName.ToString());
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(file.ToString(), saveFileDialog.FileName);
                            if (isSHowMesage)
                            {
                                MessageBox.Show("File saved successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    // No File
                    MessageBox.Show("No file available to save.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"),
                    "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("Document list", "downloadbtn_click", ex);
            }
        }
        #endregion

        #region get extension
        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private string GetExtension(string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                return (Path.GetExtension(filename));
            }
            return (".png");
        }

        #endregion

        #region Fill Document Type Combobox Bind
        /// <summary>
        /// Fills the document type combobox.
        /// </summary>
        private void FillDocumentTypeCombobox()
        {
            try
            {
                cmbDocumentType.DataSource = Enum.GetValues(typeof(CommonEnum.DocumentManagement))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();

                cmbDocumentType.DisplayMember = "Description";
                cmbDocumentType.ValueMember = "value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"), "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("Document list form", "FillDocumentTypeCombobox", ex);
            }
        }

        #endregion
        #region Populate Pager
        /// <summary>
        /// Populates the pager.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        /// <param name="currentPage">The current page.</param>
        private void PopulatePager(int recordCount, int currentPage, int pageSize)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)(recordCount / Convert.ToDecimal(pageList.PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                //var bitmap = new Bitmap(global::PNGEC.Desktop.Properties.Resources.first);
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 5)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 5).ToString() });
            }

            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">", Value = (currentPage + 1).ToString() });
            }

            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 5).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPaginationLeft.Controls.Clear();

            if (dgDocuments != null && dgDocuments.Rows.Count > 0)
            {
                //Loop and add Buttons for Pager.
                int count = 0;
                foreach (Page page in pages)
                {
                    Button btnPage = new Button();
                    btnPage.Location = new System.Drawing.Point(65 * count, 5);
                    btnPage.Size = new System.Drawing.Size(65, 30);
                    btnPage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
                    btnPage.Name = page.Value;
                    btnPage.Text = page.Text;
                    btnPage.Enabled = !page.Selected;
                    btnPage.Click += new System.EventHandler(this.Page_Click);
                    pnlPaginationLeft.Controls.Add(btnPage);
                    count++;
                }
                this.cmbPageSize.SelectedIndexChanged -= this.cmbPageSize_SelectedIndexChanged;
                cmbPageSize.DataSource = pageSizeArray;
                pnlPaginationRight.Visible = true;
                this.cmbPageSize.SelectedIndexChanged += this.cmbPageSize_SelectedIndexChanged;
            }
        }

        #endregion

        #endregion


    }
}
