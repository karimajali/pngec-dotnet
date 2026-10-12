using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
//using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Domain.Listing;
using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using PNGEC.Desktop.Properties;
using ZXing;
using PNGEC.API.Domain.MKAEnrolmentForm;
using PNGEC.API.Domain.EnrolmentForm;

namespace PNGEC.Desktop.Forms
{
    public partial class MKAEnrolmentForm : Form
    {
        #region Variable Declaration
        /// <summary>
        /// The page list
        /// </summary>
        private PageList pageList = null;

        /// <summary>
        /// The BTN pager
        /// </summary>
        private Button btnPager = new Button();

        /// <summary>
        /// The MKA geo tree structure
        /// </summary>
        private MKAGeoTreeStructure TreeStructure = null;

        /// <summary>
        /// The page size value
        /// </summary>
        private int pageSizeValue;

        /// <summary>
        /// The page size array
        /// </summary>
        private string[] pageSizeArray = { "15", "50", "100", "500" };

        /// <summary>
        /// The ds registration card
        /// </summary>
        private DataSet dsRegistrationCard = null;

        /// <summary>
        /// The dt registration card
        /// </summary>
        private DataTable dtRegistrationCard = null;

        /// <summary>
        /// The ofd
        /// </summary>
        private OpenFileDialog ofd = null;

        /// <summary>
        /// The list of enrols
        /// </summary>
        private List<MKAEnrolmentFormModel> ListOfEnrols = new List<MKAEnrolmentFormModel>();

        /// <summary>
        /// The ds form11
        /// </summary>
        private DataSet dsMKAForm11 = null;

        /// <summary>
        /// The dt form11, dt SubForm11
        /// </summary>
        private DataTable dtMKAForm11 = null, dtMKASubForm11 = null;
         
        /// <summary>
        /// Gets or sets the district.
        /// </summary>
        /// <value>
        /// The district.
        /// </value>
        private API.Domain.MKAGeoStructure.MKADistrictModel MKADistrict { get; set; }

        /// <summary>
        /// Gets or sets the LLG.
        /// </summary>
        /// <value>
        /// The LLG.
        /// </value>
        private API.Domain.MKAGeoStructure.MKALLGModel MKALLG { get; set; }

        /// <summary>
        /// Gets or sets the ward.
        /// </summary>
        /// <value>
        /// The ward.
        /// </value>
        private API.Domain.MKAGeoStructure.MKAWardModel MKAWard { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MKAEnrolmentForm"/> class.
        /// </summary>
        public MKAEnrolmentForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the MKAEnrolmentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void MKAEnrolmentForm_Load(object sender, EventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
            GlobalsModel.mainMaster.TreeViewSelectedChanged += TreeViewSelectedChanged_AfterSelect;

            if (GlobalsModel.MKATreeViewStructure != null)
            {
                await LoadGridViewSetting();
            }
        }
        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            string messageBoxText = string.Empty;

            try
            {
                if (IsValidForm())
                {
                    if (ofd != null && !string.IsNullOrWhiteSpace(ofd.FileName))
                    {
                        //bytes = System.IO.File.ReadAllBytes(ofd.FileName);
                    }
                    MultipartFormDataContent formContent;
                    if (ofd != null && !string.IsNullOrWhiteSpace(ofd.FileName) && ofd.FileNames.Length >= 1)
                    {
                        formContent = new MultipartFormDataContent()
                        {
                            {new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()),"UserId"},
                            {new StringContent(Convert.ToInt32(this.MKADistrict.DistrictId).ToString()),"MKADistrictId"},
                            {new StringContent(Convert.ToInt32(this.MKALLG.LLGId).ToString()),"MKALLGId"},
                            {new StringContent(Convert.ToInt32(this.MKAWard.WardId).ToString()),"MKAWardId"},
                            {new StringContent(Convert.ToString(txtBatchName.Text)),"BatchName"},
                            {new StringContent(Convert.ToString(txtBatchNumber.Text)),"NumberOfBatch"},
                            {new StringContent(Convert.ToString(txtComment.Text)),"Comment"},
                            //{new StreamContent(new MemoryStream(bytes)),"AttachmnetToUpoad", ofd.SafeFileName},
                        };
                        for (int i = 0; i < ofd.SafeFileNames.Length; i++)
                        {
                            formContent.Add(new StreamContent(new MemoryStream(System.IO.File.ReadAllBytes(ofd.FileNames[i]))),
                                "AttachmnetToUpoad", ofd.SafeFileNames[i]);
                        }
                    }
                    else
                    {
                        formContent = new MultipartFormDataContent()
                        {
                            {new StringContent(GlobalsModel.LoggedUser.User.UserId.ToString()),"UserId"},
                            {new StringContent(Convert.ToInt32(this.MKADistrict.DistrictId).ToString()),"MKADistrictId"},
                            {new StringContent(Convert.ToInt32(this.MKALLG.LLGId).ToString()),"MKALLGId"},
                            {new StringContent(Convert.ToInt32(this.MKAWard.WardId).ToString()),"MKAWardId"},
                            {new StringContent(Convert.ToString(txtBatchName.Text)),"BatchName"},
                            {new StringContent(Convert.ToString(txtBatchNumber.Text)),"NumberOfBatch"},
                            {new StringContent(Convert.ToString(txtComment.Text)),"Comment"},
                            {new StringContent(Convert.ToString(GlobalsModel.LoggedUser.User.UserId)),"ActionBy"}
                        };
                    }
                    Response res = null;
                    using (var client = new HttpClient())
                    {
                        //client.BaseAddress = new Uri(ConfigurationManager.AppSettings["apiURL"]);
                        client.BaseAddress = new Uri(Globals.serverURL);
                        using (HttpResponseMessage response = client.PostAsync("EnrolmentForm/AddMKAEnrolmentForm", formContent).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                res = JsonConvert.DeserializeObject<Response>(responseBody);
                            }
                        }
                    }

                    if (res != null)
                    {
                        if (res.Status)
                        {
                            StartLoader();

                            var enrolData = JsonConvert.DeserializeObject<MKAEnrolmentFormModel>(res.Data.ToString());
                            pdfDialog.FileName = this.MKADistrict.DistrictName.Replace("/", "_") + "_" + this.MKALLG.LLGName.Replace("/", "_") + "_" + this.MKAWard.WardName.Replace("/", "_") + "_" + txtBatchName.Text.Replace("/", "_") + "_mkaform11";
                            if (pdfDialog.ShowDialog() == DialogResult.OK)
                            {
                                if (Path.GetExtension(pdfDialog.FileName) != ".pdf")
                                {
                                    this.DeleteEnrolmentForm(enrolData.EnrolmentFormId);
                                    MessageBox.Show("Only .pdf format is supported.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    await GridViewSetting();
                                }
                                else
                                {
                                    byte[] bytesEnrolmentFormsExport = await EnrolmentFormsExportReportSource(Convert.ToInt32(enrolData.StartRange),
                                    Convert.ToInt32(enrolData.EndRange),
                                    Convert.ToInt32(this.MKADistrict.DistrictId),
                                    Convert.ToInt32(this.MKALLG.LLGId),
                                    Convert.ToInt32(this.MKAWard.WardId),
                                    Convert.ToString(txtBatchName.Text));

                                    if (bytesEnrolmentFormsExport != null)
                                    {
                                        using (FileStream stream = new FileStream(pdfDialog.FileName, FileMode.Create))
                                        {
                                            stream.Write(bytesEnrolmentFormsExport, 0, bytesEnrolmentFormsExport.Length);
                                        }

                                        this.DeleteQRCodeImagesDirectory();
                                        this.DialogResult = DialogResult.OK;

                                        messageBoxText = string.Format("Form 11 is saved at {0}", pdfDialog.FileName);
                                        MessageBox.Show(messageBoxText, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        //MessageBox.Show("Form11 generated successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);                            

                                        this.clearData();
                                        await GridViewSetting();
                                    }
                                    else
                                    {

                                        this.DeleteQRCodeImagesDirectory();
                                        this.DeleteEnrolmentForm(enrolData.EnrolmentFormId);
                                        this.DialogResult = DialogResult.None;
                                        MessageBox.Show("Error in saving the PDF file(s).", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            else
                            {
                                var d = JsonConvert.DeserializeObject<MKAEnrolmentFormModel>(res.Data.ToString());
                                //var d = (MKAEnrolmentFormModel)res.Data;
                                this.DeleteEnrolmentForm(d.EnrolmentFormId);
                                this.DialogResult = DialogResult.None;
                            }

                            StopLoader();

                        }
                        else
                        {
                            this.DialogResult = DialogResult.None;
                            MessageBox.Show(res.Message, "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        clearData();
                    }
                }
            }
            catch (Exception ex)
            {
                StopLoader();
                ErrorLog.Write("EnrolmentForm", "btnGenerate_Click", ex);
            }
            finally
            {
                messageBoxText = string.Empty;
            }
        }

        /// <summary>
        /// Upload Button Click
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void uploadAttachBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.Title = "Upload Attachment";
                //ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    attachedfileName.Text = string.Join(",", ofd.SafeFileNames);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"),
                    "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("Enrolment Form", "uploadAttachBtn_Click", ex);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtStartRange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtStartRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtEndRange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtEndRange_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtBatchNumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtBatchNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            //RefreshData();
            txtSearch.Text = string.Empty;
            await GridViewSetting("Refresh");
        }

        /// <summary>
        /// Handles the Click event of the btnSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            await GridViewSetting("Search");
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the CmbPageSize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void CmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageSizeValue = Convert.ToInt32(cmbPageSize.SelectedValue);
            await GridViewSetting("PageSize");
        }

        /// <summary>
        /// Handles the Click event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Page_Click(object sender, EventArgs e)
        {
            btnPager = (sender as Button);
            await GridViewSetting();
        }

        /// <summary>
        /// Handles the CellContentClick event of the dgMKAEnrolmentForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>       
        private async void dgMKAEnrolmentForms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string messageBoxText = string.Empty;

            if (e.RowIndex >= 0 && dgMKAEnrolmentForms.Columns[e.ColumnIndex].Name == "Print")
            {
                try
                {
                    #region EnrolmentForms

                    var saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = GetFileNameStr(e, "mkaform11").Replace("/", "_");
                    saveFileDialog.DefaultExt = ".pdf";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        GlobalsModel.mainMaster.ShowLoader();

                        if (Path.GetExtension(saveFileDialog.FileName) != ".pdf")
                        {
                            /*Can not delete records on print button so below line comment */
                            //this.DeleteEnrolmentForm(Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EnrolmentFormId"].Value));
                            MessageBox.Show("Only .pdf format is supported.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            byte[] bytes = await EnrolmentFormsExportReportSource(Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["StartRange"].Value),
                            Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EndRange"].Value),
                            //Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["RegionId"].Value),
                            //Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["ProvinceId"].Value),
                            Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["MKADistrictId"].Value),
                            Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["MKALLGId"].Value),
                            Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["MKAWardId"].Value),
                            Convert.ToString(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["BatchName"].Value));

                            if (bytes != null)
                            {
                                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                                {
                                    stream.Write(bytes, 0, bytes.Length);
                                }

                                this.DeleteQRCodeImagesDirectory();

                                messageBoxText = string.Format("Form 11 is saved at {0}", saveFileDialog.FileName);
                                MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //MessageBox.Show("Form11 generated successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                this.DeleteQRCodeImagesDirectory();
                                /*Can not delete records on print button so below line comment */
                                //this.DeleteEnrolmentForm(Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EnrolmentFormId"].Value));
                                MessageBox.Show("Error in saving the PDF file(s).", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        /*Can not delete records on print button so below line comment */
                        //this.DeleteEnrolmentForm(Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EnrolmentFormId"].Value));
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    ErrorLog.Write("EnrolmentForm", "dgEnrolmentForms_CellContentClick - Print", ex);
                    MessageBox.Show("Something went wrong", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (dtMKAForm11 != null)
                    {
                        dtMKAForm11.Clear();
                    }

                    if (dtMKASubForm11 != null)
                    {
                        dtMKASubForm11.Clear();
                    }

                    if (dsMKAForm11 != null)
                    {
                        dsMKAForm11.Clear();
                    }

                    GlobalsModel.mainMaster.HideLoader();
                }
            }
            else if (e.RowIndex >= 0 && dgMKAEnrolmentForms.Columns[e.ColumnIndex].Name == "PrintRegistrationCard")
            {
                pdfDialog.FileName = GetFileNameStr(e, "registrsation_card").Replace("/", "_");
                //pdfDialog.FileName = "enrolmentRegistrationCard_" + dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["ProvinceId"].Value + "_" + dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["StartRange"].Value + "_" + dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EndRange"].Value;
                if (pdfDialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(pdfDialog.FileName) != ".pdf")
                    {
                        MessageBox.Show("Only .pdf format is supported.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        byte[] bytes = null;

                        dsRegistrationCard = new DataSet();
                        DataTable dtMKASubRCard = new DataTable();

                        string geoLocation = string.Format(@"{0}\{1}\{2}", dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["DistrictName"].Value, dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["LLGName"].Value, dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["WardName"].Value);

                        DataTable dtRCard = await GenerateRegistrationCardNo(Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["StartRange"].Value), Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EndRange"].Value), Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["MKADistrictId"].Value), Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["MKAWardId"].Value));

                        #region dtMKASubRCard
                        dtMKASubRCard.Clear();
                        dtMKASubRCard.Columns.Add("RegistrationCardNo");
                        dtMKASubRCard.Columns.Add("Ward");
                        foreach (DataRow DataRow in dtRCard.Rows)
                        {
                            DataRow dtRow = dtMKASubRCard.NewRow();
                            dtRow["RegistrationCardNo"] = DataRow["RegistrationCardNo"];
                            dtRow["Ward"] = geoLocation;

                            dtMKASubRCard.Rows.Add(dtRow);
                        }
                        #endregion

                        dsRegistrationCard.Tables.AddRange(new DataTable[] { dtRCard, dtMKASubRCard });

                        bytes = this.ExportRegistrationCard();
                        using (FileStream stream = new FileStream(pdfDialog.FileName, FileMode.Create))
                        {
                            stream.Write(bytes, 0, bytes.Length);
                        }
                        dsRegistrationCard.Clear();

                        messageBoxText = string.Format("Registration Card is saved at {0}", pdfDialog.FileName);
                        MessageBox.Show(messageBoxText, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //RegistrationCardReport registrationCard = new RegistrationCardReport(dsRegistrationCard);
                        //registrationCard.Show();
                    }
                    StopLoader();
                }
            }
            else if (e.RowIndex >= 0 && dgMKAEnrolmentForms.Columns[e.ColumnIndex].Name == "View")
            {
                var comment = dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["Comment"].Value;
                if (!string.IsNullOrEmpty(Convert.ToString(comment)) && !string.IsNullOrWhiteSpace(Convert.ToString(comment)))
                {
                    ViewComment commentModel = new ViewComment(Convert.ToString(comment));
                    if (commentModel.ShowDialog() == DialogResult.OK)
                    {
                        //await GridViewSetting();
                    }
                }
                else
                {
                    // No comments their.
                    MessageBox.Show("There is no comment to view.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (e.RowIndex >= 0 && dgMKAEnrolmentForms.Columns[e.ColumnIndex].Name == "Download")
            {
                var attachment = dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EnrolmentFormId"].Value;
                var attachmnets = ListOfEnrols.Where(d => d.EnrolmentFormId == Convert.ToInt32(attachment)).SelectMany(f => f.AttachmentUrlList).ToList();
                var fileName = GetFileNameStr(e, "attachment");
                DownloadAttachmnets(attachmnets, fileName);
            }
            else if (e.RowIndex >= 0 && dgMKAEnrolmentForms.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete selected MKA Form11 Batch?", "MKA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.DeleteEnrolmentForm(Convert.ToInt32(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["EnrolmentFormId"].Value), true);
                }
            }

            messageBoxText = string.Empty;
        }
        /// <summary>
        /// Handles the AfterSelect event of the TreeViewSelectedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public async void TreeViewSelectedChanged_AfterSelect(object sender, EventArgs e)
        {
            await LoadGridViewSetting();
        }
        /// <summary>
        /// Handles the FormClosing event of the EnrolmentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void MKAEnrolmentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalsModel.mainMaster.TreeViewSelectedChanged -= TreeViewSelectedChanged_AfterSelect;
        }
        #endregion

        #region Methods    

        #region Get File Name Str
        /// <summary>
        /// Gets the file name string.
        /// </summary>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// <param name="appendStr">The append string.</param>
        /// <returns></returns>
        private string GetFileNameStr(DataGridViewCellEventArgs e, string appendStr)
        {
            //var province = Convert.ToString(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["ProvinceName"].Value);
            var district = Convert.ToString(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["DistrictName"].Value);
            var llg = Convert.ToString(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["LLGName"].Value);
            var ward = Convert.ToString(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["WardName"].Value);
            var batchName = Convert.ToString(dgMKAEnrolmentForms.Rows[e.RowIndex].Cells["BatchName"].Value);
            var str = string.Empty;

            if (!string.IsNullOrEmpty(district))
            {
                //str = province + "_" + district;
            }
            if (!string.IsNullOrEmpty(llg))
            {
                str += district + "_" + llg;
            }
            if (!string.IsNullOrEmpty(ward))
            {
                str += "_" + ward;
            }
            if (!string.IsNullOrEmpty(batchName))
            {
                str += "_" + batchName;
            }
            return str + "_" + appendStr;
        }
        #endregion

        #region Download Attachmnets
        /// <summary>
        /// Downloads the attachmnets.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        private async void DownloadAttachmnets(List<AttachmnetModel> attachment, string fileName)
        {
            try
            {
                if (attachment != null && attachment.Count >= 1)
                {
                    //open list of file dialogs
                    EnrolAttachment commentModel = new EnrolAttachment(attachment);
                    if (commentModel.ShowDialog() == DialogResult.OK)
                    {
                        await GridViewSetting();
                    }

                    ////SaveFileDialog saveFileDialog = new SaveFileDialog();
                    ////saveFileDialog.FileName = string.Format(@"{0}", fileName);
                    ////saveFileDialog.DefaultExt = GetExtension(attachment.ToString());
                    ////if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    ////{
                    ////    using (var client = new WebClient())
                    ////    {
                    ////        client.DownloadFile(attachment.ToString(), saveFileDialog.FileName);
                    ////        MessageBox.Show("Attachment saved successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////    }
                    ////}
                }
                else
                {
                    // No Attachment
                    MessageBox.Show("No attachment available to save.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"),
                    "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("Enrolment Form", "downloadbtn_click", ex);
            }

        }
        #endregion

        #region CreateAddRequestParam
        /// <summary>
        /// Creates the request model
        /// </summary>
        private object CreateAddRequestParam()
        {
            MKAEnrolmentFormModel enrolmentFormModel = new MKAEnrolmentFormModel
            {
                UserId = GlobalsModel.LoggedUser.User.UserId,
                //ProvinceId = Convert.ToInt32(this.Province.ProvinceId),
                //StartRange = Convert.ToInt32(txtStartRange.Text),
                //EndRange = Convert.ToInt32(txtEndRange.Text),
                MKADistrictId = Convert.ToInt32(this.MKADistrict.DistrictId),
                MKALLGId = Convert.ToInt32(this.MKALLG.LLGId),
                MKAWardId = Convert.ToInt32(this.MKAWard.WardId),
                BatchName = Convert.ToString(txtBatchName.Text),
                NumberOfBatch = Convert.ToInt32(txtBatchNumber.Text),
                Comment = Convert.ToString(txtComment.Text)
            };
            return enrolmentFormModel;
        }
        #endregion

        #region ClearForm
        /// <summary>
        /// Clears the form.
        /// </summary>
        private void clearData()
        {
            //txtStartRange.Text = string.Empty;
            //txtEndRange.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtBatchName.Text = string.Empty;
            txtComment.Text = string.Empty;
            attachedfileName.Text = string.Empty;
            txtBatchNumber.Text = string.Empty;
        }
        #endregion

        #region IsValidForm
        /// <summary>
        /// Determines whether [is valid form].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is valid form]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidForm()
        {
            //if (string.IsNullOrWhiteSpace(txtStartRange.Text.Trim()))
            //{
            //    MessageBox.Show("Start Range can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(txtEndRange.Text.Trim()))
            //{
            //    MessageBox.Show("End Range can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            if (string.IsNullOrWhiteSpace(txtBatchName.Text.Trim()))
            {
                MessageBox.Show("Batch Name can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBatchNumber.Text.Trim()))
            {
                MessageBox.Show("Batch Number can't be empty.", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //if (Convert.ToInt32(txtStartRange.Text) > Convert.ToInt32(txtEndRange.Text))
            //{
            //    MessageBox.Show("Start Range can not be greater than End Range", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}


            return true;
        }

        #endregion

        #region RefreshData
        /// <summary>
        /// Refresh Data
        /// </summary>
        private async void RefreshData()
        {
            clearData();
            PageList model = new PageList();
            model.PageNo = 1;
            model.PageSize = 15;
            model.KeyWord = "";
            model.SortField = "EnrolmentFormId";
            model.SortOrder = "DESC";

            Response<MKAEnrolmentFormModel> response = await GenericHttpClient.PostRequest<MKAEnrolmentFormModel>("EnrolmentForm/GetEnrolmentFormList", model);
            if (response.Status)
            {
                BindEnrolmentFormGrid(response.Data.ListItems.ToList());
            }
        }
        #endregion

        #region DeleteEnrolmentForm
        /// <summary>
        /// Deletes the EnrolmentForm if any error in PDF generation
        /// </summary>
        /// <param name="enrolmentFormId">The enrolmentFormId.</param>
        private async void DeleteEnrolmentForm(int enrolmentFormId, bool isDeleteFromUI = false)
        {
            try
            {
                if (isDeleteFromUI)
                {
                    GlobalsModel.mainMaster.ShowLoader();
                }

                string url = string.Format("EnrolmentForm/DeleteMKAEnrolmentForm/{0}", enrolmentFormId);
                //var isSuccess = await GenericHttpClient.DeleteRequest(url);
                var response = await GenericHttpClient.GetResponse(url);
                if (isDeleteFromUI)
                {
                    if (response != null && response.Status)
                    {
                        MessageBox.Show("MKA Form 11 batch is successfully deleted.", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await GridViewSetting();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong", "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("EnrolmentForm", "DeleteEnrolmentForm", ex);
            }

            if (isDeleteFromUI)
            {
                GlobalsModel.mainMaster.HideLoader();
            }
        }
        #endregion

        #region Generate Form Number
        /// <summary>
        /// Generates the form number.
        /// </summary>
        /// <param name="formNumberCount">The form number count.</param>
        /// <returns>return form number</returns>
        private string GenerateFormNumber(int formNumberCount, int startRange, int mkadistrictId, int mkawardId)
        {
            ////string sevenDigitNumber = "0000000".Remove(0, txtStartRange.Text.Length);
            ////string numberWithRange = string.Concat(sevenDigitNumber, Convert.ToInt32(txtStartRange.Text) + formNumberCount);
            ////return string.Format("{0}#{1}", Convert.ToInt32(cmbProvinceName.SelectedValue), numberWithRange);
            var ward = mkawardId.ToString().PadLeft(4, '0');
            var numberWithRange = (startRange + formNumberCount).ToString().PadLeft(7, '0');
            return string.Format("{0}{1}{2}", mkadistrictId, ward, numberWithRange);
        }
        #endregion

        #region Generate QR Code HashNumber
        /// <summary>
        /// Generates the qr code hash number.
        /// </summary>       
        /// <param name="mkadistrictId">The district identifier.</param>
        /// <param name="mkallgId">The LLG identifier.</param>
        /// <param name="mkawardId">The ward identifier.</param>
        /// <param name="batchName">Name of the batch.</param>
        /// <param name="formNumberCount">The form number count.</param>
        /// <param name="startRange">The start range.</param>
        /// <returns></returns>
        private string GenerateQRCodeHashNumber(int mkadistrictId, int mkallgId, int mkawardId, string batchName, int formNumberCount, int startRange)
        {
            var numberWithRange = (startRange + formNumberCount).ToString().PadLeft(7, '0');
            return string.Format
                ("MKA#{0}#{1}#{2}#{3}#{4}",mkadistrictId, mkallgId, mkawardId, batchName, numberWithRange);

        }
        #endregion

        #region Generate QR Code
        /// <summary>
        /// Generates the QR Code.
        /// </summary>
        /// <param name="modeQCTextl">The QCText.</param>
        private string GenerateQRCode(string QCText)
        {
            var QCwriter = new BarcodeWriter();
            QCwriter.Options = new ZXing.Common.EncodingOptions { Margin = 0, Height = 100, Width = 100 };
            QCwriter.Format = BarcodeFormat.QR_CODE;
            var result = QCwriter.Write(QCText);

            string appPath = string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\QRCodeImages");
            string filePath = string.Concat(appPath, "\\", QCText.Replace("#", string.Empty).Replace(" ", string.Empty), ".jpg");
            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }
            var barcodeBitmap = new Bitmap(result);

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(filePath,
                   FileMode.Create, FileAccess.ReadWrite))
                {
                    //byte array to html
                    barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            return filePath;
        }
        #endregion

        #region Generate Registration Card No
        /// <summary>
        /// Generates the registration card no.
        /// </summary>
        /// <param name="startRange">The start range.</param>
        /// <param name="endRange">The end range.</param>
        /// <param name="provinceId">The province identifier.</param>
        /// <returns></returns>
        private async Task<DataTable> GenerateRegistrationCardNo(int startRange, int endRange, int mkadistrictId, int mkawardId)
        {
            StartLoader();
            await Task.Delay(10);

            dtRegistrationCard = new DataTable();

            try
            {
                dtRegistrationCard.Clear();
                dtRegistrationCard.Columns.Add("RegistrationCardNo");


                var numberOfForms = endRange - startRange;
                for (int i = 0; i <= numberOfForms; i++)
                {
                    string formNumber = this.GenerateFormNumber(i, startRange, mkadistrictId, mkawardId);

                    DataRow dtRow = dtRegistrationCard.NewRow();
                    dtRow["RegistrationCardNo"] = string.Format(formNumber.Replace("#", ""));
                    dtRegistrationCard.Rows.Add(dtRow);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Write("EnrolmentForm", "GenerateRegistrationCardNo", ex);
            }

            return dtRegistrationCard;
        }
        #endregion

        #region StartLoader
        /// <summary>
        /// Starts the Loader 
        /// </summary>
        private void StartLoader()
        {
            //this.Cursor = Cursors.WaitCursor;
            this.pnlSearch.Enabled = false;
            this.pnlGenerate.Enabled = false;
            this.pnlGenerate.Enabled = false;
            GlobalsModel.mainMaster.ShowLoader();
        }
        #endregion

        #region StopLoader
        /// <summary>
        /// Stops the Loader 
        /// </summary>
        private void StopLoader()
        {
            this.pnlSearch.Enabled = true;
            this.pnlGenerate.Enabled = true;
            this.pnlGenerate.Enabled = true;
            //this.Cursor = Cursors.Arrow;
            GlobalsModel.mainMaster.HideLoader();
        }
        #endregion

        #region DeleteQRCodeImagesDirectory
        /// <summary>
        /// Deletes the QRCode Images Directory
        /// </summary>
        private void DeleteQRCodeImagesDirectory()
        {
            if (Directory.Exists(string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\QRCodeImages")))
            {
                Directory.Delete(string.Concat(Path.GetDirectoryName(Application.ExecutablePath), "\\QRCodeImages"), true);
            }
        }
        #endregion

        #region Load Grid View Setting
        /// <summary>
        /// Loads the grid view setting.
        /// </summary>
        private async Task LoadGridViewSetting()
        {
            TreeStructure = GlobalsModel.MKATreeViewStructure;

            // isLocation
            if (TreeStructure.MainTagName == "MKAWard" || TreeStructure.MainTagName == "MKALocation")
            {
                btnGenerate.Enabled = true;

                if (TreeStructure.MainTagName == "MKAWard")
                {
                    this.MKAWard = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == TreeStructure.Id).SingleOrDefault();
                    this.MKALLG = Globals.MKAGeoList.MKALLGList.Where(x => x.Status && x.LLGId == this.MKAWard.LLGId).SingleOrDefault();
                    this.MKADistrict = Globals.MKAGeoList.MKADistrictList.Where(x => x.Status && x.DistrictId == this.MKAWard.DistrictId).SingleOrDefault();


                    if (this.MKAWard.WardId > 0)
                    {
                        var ward = Convert.ToString(this.MKAWard.WardId).PadLeft(4, '0');
                        txtBatchName.Text = ward;
                    }
                }
                else if (TreeStructure.MainTagName == "MKALocation")
                {
                    this.MKAWard = Globals.MKAGeoList.MKAWardList.Where(x => x.Status && x.WardId == TreeStructure.ParentId).SingleOrDefault();
                    this.MKALLG = Globals.MKAGeoList.MKALLGList.Where(x => x.Status && x.LLGId == this.MKAWard.LLGId).SingleOrDefault();
                    this.MKADistrict = Globals.MKAGeoList.MKADistrictList.Where(x => x.Status && x.DistrictId == this.MKAWard.DistrictId).SingleOrDefault();


                    if (this.MKAWard.WardId > 0)
                    {
                        var ward = Convert.ToString(this.MKAWard.WardId).PadLeft(4, '0');
                        txtBatchName.Text = ward;
                    }

                    btnGenerate.Enabled = false;
                }
            }
            else
            {
                btnGenerate.Enabled = false;
            }

            await GridViewSetting("AfterSelect");
        }
        #endregion

        #region GridViewSetting
        /// <summary>
        /// Grids the view setting.
        /// </summary>
        private async Task GridViewSetting(string eventName = "")
        {
            GlobalsModel.mainMaster.ShowLoader();
            pageList = new PageList();

            try
            {
                // Current Database Filed 
                pageList.DatabaseName = GlobalsModel.LoggedUser.Database.ToString();

                // Pagination Filed
                string pageIndex = string.Empty;
                if (eventName == "AfterSelect" || eventName == "Refresh" || eventName == "Search" || eventName == "PageSize")
                {
                    pageIndex = "1";
                }
                else
                {
                    pageIndex = btnPager.Name.Equals("") ? "1" : btnPager.Name;
                }
                pageList.PageNo = int.Parse(pageIndex);
                pageList.PageSize = pageSizeValue.Equals(0) ? Convert.ToInt32(pageSizeArray.First()) : pageSizeValue;
                pageList.KeyWord = txtSearch.Text;
                pageList.SortField = "EnrolmentFormId";
                pageList.SortOrder = "DESC";

                // TreeView Filed
                pageList.GeoName = TreeStructure.MainTagName;
                pageList.GeoId = TreeStructure.Id;

                // Login User
                pageList.LoggedUserId = GlobalsModel.LoggedUser.User.UserId;

                if (pageList != null && TreeStructure.MainTagName != "MKALocation")
                {
                    await BindGrid(pageList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resources.ErrorMessage, "MKA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog.Write("MKA Enrolment Form", "GridViewSetting", ex);
            }

            GlobalsModel.mainMaster.HideLoader();

        }
        #endregion

        #region Bind Grid
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <param name="model">The model.</param>
        private async Task BindGrid(PageList model)
        {
            Response<MKAEnrolmentFormModel> response = await GenericHttpClient.PostRequest<MKAEnrolmentFormModel>("EnrolmentForm/GetMKAEnrolmentFormList", model);
            if (response.Status)
            {
                if (response.Data.ListItems.ToList() != null && response.Data.ListItems.ToList().Count > 0)
                {
                    foreach (var item in response.Data.ListItems.ToList())
                    {
                        DateTime utcDate = DateTime.SpecifyKind(item.CreatedDate, DateTimeKind.Utc);
                        DateTime localDate = utcDate.ToLocalTime();
                        item.Date = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
                    }
                }

                BindEnrolmentFormGrid(response.Data.ListItems.ToList());
                PopulatePager(response.Data.TotalRecordCount, model.PageNo, model.PageSize);
            }

            //clearData();
        }
        #endregion

        #region Bind EnrolmentForm Grid
        /// <summary>
        /// Binds the EnrolmentForm gird.
        /// </summary>
        /// <param name="model">The model.</param>
        private void BindEnrolmentFormGrid(List<MKAEnrolmentFormModel> lstEnrolmentForms)
        {
            if (dgMKAEnrolmentForms.Columns.Count > 0)
            {
                dgMKAEnrolmentForms.Columns.Clear();
            }
            this.ListOfEnrols = lstEnrolmentForms;
            dgMKAEnrolmentForms.DataSource = lstEnrolmentForms;


            dgMKAEnrolmentForms.Columns["EnrolmentFormId"].Visible = false;
            dgMKAEnrolmentForms.Columns["UserId"].Visible = false;
            //dgMKAEnrolmentForms.Columns["ProvinceId"].Visible = false;
            //dgMKAEnrolmentForms.Columns["RegionId"].Visible = false;
            dgMKAEnrolmentForms.Columns["MKADistrictId"].Visible = false;
            dgMKAEnrolmentForms.Columns["MKALLGId"].Visible = false;
            dgMKAEnrolmentForms.Columns["MKAWardId"].Visible = false;
            dgMKAEnrolmentForms.Columns["MKAClanId"].Visible = false;
            dgMKAEnrolmentForms.Columns["ResponseCode"].Visible = false;
            dgMKAEnrolmentForms.Columns["CreatedDate"].Visible = false;

            //dgMKAEnrolmentForms.Columns["UsedFromRegistration"].Visible = false;
            //dgMKAEnrolmentForms.Columns["UsedFromTransferIn"].Visible = false;
            //dgMKAEnrolmentForms.Columns["AttachmnetToUpoad"].Visible = false;
            dgMKAEnrolmentForms.Columns["Comment"].Visible = false;
            dgMKAEnrolmentForms.Columns["Attachment"].Visible = false;
            //dgMKAEnrolmentForms.Columns["AttachmentUrlList"].Visible = false;
            //dgMKAEnrolmentForms.Columns["RegionName"].Visible = false;
            dgMKAEnrolmentForms.Columns["NumberOfForms"].Visible = false;

            dgMKAEnrolmentForms.Columns["EmailAddress"].HeaderText = "User Name";
            //dgMKAEnrolmentForms.Columns["ProvinceName"].HeaderText = "Province";
            dgMKAEnrolmentForms.Columns["DistrictName"].HeaderText = "MKA Assembly";
            dgMKAEnrolmentForms.Columns["LLGName"].HeaderText = "MKA Constituency";
            dgMKAEnrolmentForms.Columns["WardName"].HeaderText = "MKA Ward";
            dgMKAEnrolmentForms.Columns["ClanName"].HeaderText = "MKA Section";
            dgMKAEnrolmentForms.Columns["NumberOfBatch"].HeaderText = "Number Of Form";
            dgMKAEnrolmentForms.Columns["UsedFromRegistration"].HeaderText = "Registration";
            dgMKAEnrolmentForms.Columns["UsedFromTransferIn"].HeaderText = "Transfer In";

            //dgMKAEnrolmentForms.Columns["CreatedDate"].DisplayIndex = 1;
            //dgMKAEnrolmentForms.Columns["EmailAddress"].DisplayIndex = 2;
            //dgMKAEnrolmentForms.Columns["ProvinceName"].DisplayIndex = 3;
            //dgMKAEnrolmentForms.Columns["StartRange"].DisplayIndex = 4;
            //dgMKAEnrolmentForms.Columns["EndRange"].DisplayIndex = 5;
            //dgMKAEnrolmentForms.Columns["NumberOfForms"].DisplayIndex = dgMKAEnrolmentForms.ColumnCount - 2;

            DataGridViewButtonColumn viewButton = new DataGridViewButtonColumn();
            viewButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            viewButton.FlatStyle = FlatStyle.Standard;
            viewButton.HeaderText = "Comment";
            viewButton.Name = "View";
            viewButton.Text = "View";
            viewButton.UseColumnTextForButtonValue = true;
            viewButton.Width = 80;
            if (dgMKAEnrolmentForms.Columns["View Comment"] == null)
            {
                dgMKAEnrolmentForms.Columns.Insert(dgMKAEnrolmentForms.ColumnCount, viewButton);
            }

            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteButton.FlatStyle = FlatStyle.Standard;
            deleteButton.HeaderText = "Action";
            deleteButton.Name = "Delete";
            deleteButton.Text = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            deleteButton.Width = 75;
            if (dgMKAEnrolmentForms.Columns["Delete"] == null)
            {
                dgMKAEnrolmentForms.Columns.Insert(dgMKAEnrolmentForms.ColumnCount, deleteButton);
            }

            DataGridViewButtonColumn attachmentButton = new DataGridViewButtonColumn();
            attachmentButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attachmentButton.FlatStyle = FlatStyle.Standard;
            attachmentButton.HeaderText = "Attachment";
            attachmentButton.Name = "Download";
            attachmentButton.Text = "Download";
            attachmentButton.UseColumnTextForButtonValue = true;
            attachmentButton.Width = 80;
            if (dgMKAEnrolmentForms.Columns["Save Attachment"] == null)
            {
                dgMKAEnrolmentForms.Columns.Insert(dgMKAEnrolmentForms.ColumnCount, attachmentButton);
            }

            DataGridViewButtonColumn editButton = new DataGridViewButtonColumn();
            editButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            editButton.FlatStyle = FlatStyle.Standard;
            editButton.HeaderText = "Print";
            editButton.Name = "Print";
            editButton.Text = "Print";
            editButton.UseColumnTextForButtonValue = true;
            editButton.Width = 80;
            if (dgMKAEnrolmentForms.Columns["Print"] == null)
            {
                dgMKAEnrolmentForms.Columns.Insert(dgMKAEnrolmentForms.ColumnCount, editButton);
            }

            DataGridViewButtonColumn printRegistrationCard = new DataGridViewButtonColumn();
            printRegistrationCard.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            printRegistrationCard.FlatStyle = FlatStyle.Standard;
            printRegistrationCard.HeaderText = "Print Registration Card";
            printRegistrationCard.Name = "PrintRegistrationCard";
            printRegistrationCard.Text = "Print Registration Card";
            printRegistrationCard.UseColumnTextForButtonValue = true;
            printRegistrationCard.Width = 125;
            if (dgMKAEnrolmentForms.Columns["PrintRegistrationCard"] == null)
            {
                dgMKAEnrolmentForms.Columns.Insert(dgMKAEnrolmentForms.ColumnCount, printRegistrationCard);
            }

            //dgMKAEnrolmentForms.Columns["Print"].DisplayIndex = dgMKAEnrolmentForms.ColumnCount - 1;
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

            if (dgMKAEnrolmentForms != null && dgMKAEnrolmentForms.Rows.Count > 0)
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
                this.cmbPageSize.SelectedIndexChanged -= this.CmbPageSize_SelectedIndexChanged;
                cmbPageSize.DataSource = pageSizeArray;
                pnlPaginationRight.Visible = true;
                this.cmbPageSize.SelectedIndexChanged += this.CmbPageSize_SelectedIndexChanged;
            }
        }
        #endregion

        #region Export Registration Card
        /// <summary>
        /// Exports the registration card.
        /// </summary>
        /// <returns></returns>
        private byte[] ExportRegistrationCard()
        {
            byte[] bytes = null;

            if (dsRegistrationCard.Tables[0].Rows != null && dsRegistrationCard.Tables[0].Rows.Count > 0)
            {
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                ReportDataSource dataSource = new ReportDataSource("ds_MKARegistrationCard", dsRegistrationCard.Tables[0]);
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();

                System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
                viewer.LocalReport.ReportPath = "Report/MKARegistrationCard.rdlc";
                viewer.LocalReport.DataSources.Add(dataSource);
                viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            }

            return bytes;
        }
        #endregion

        #region SetSubDataSource
        /// <summary>
        /// Sets the sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            string RegistrationCardNo = Convert.ToString(e.Parameters[0].Values[0]);

            if (!string.IsNullOrWhiteSpace(RegistrationCardNo))
            {
                DataTable tblSubRCardFiltered = dsRegistrationCard.Tables[1].AsEnumerable().Where(row => row.Field<String>("RegistrationCardNo") == RegistrationCardNo).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblMKASubRegistrationCard";
                ReportDataSource datasource = new ReportDataSource("ds_MKASubRegistrationCard", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #region GetExtension
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

        #region EnrolmentFormsExportReportSource
        /// <summary>
        /// Enrolments the forms export report source.
        /// </summary>
        /// <param name="startRange">The start range.</param>
        /// <param name="endRange">The end range.</param>
        /// <param name="provinceId">The province identifier.</param>
        /// <param name="mkawardId">The ward identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="mkadistrictId">The district identifier.</param>
        /// <param name="mkallgId">The LLG identifier.</param>
        /// <param name="batchName">Name of the batch.</param>
        /// <returns></returns>
        private async Task<byte[]> EnrolmentFormsExportReportSource(int startRange, int endRange, int mkadistrictId, int mkallgId, int mkawardId, string batchName)
        {
            byte[] bytes = null;

            await Task.Delay(10);

            var numberOfForms = endRange - startRange;
            string[] PDFfilepaths = new string[numberOfForms + 1];
            string FormNo, qrCodeHashNumber, QRCodeUrl, firstFormNo = "", lastFormNo = "";

            // RegionName = Globals.GeoList.RegionList.Where(x => x.RegionId == regionId).Select(y => y.RegionName).FirstOrDefault();
            var DistrictName = Globals.MKAGeoList.MKADistrictList.Where(x => x.DistrictId == mkadistrictId).Select(y => y.DistrictName).FirstOrDefault();
            var LLGName = Globals.MKAGeoList.MKALLGList.Where(x => x.LLGId == mkallgId).Select(y => y.LLGName).FirstOrDefault();
            var WardName = Globals.MKAGeoList.MKAWardList.Where(x => x.WardId == mkawardId).Select(y => y.WardName).FirstOrDefault();
            var WardNumber = Globals.MKAGeoList.MKAWardList.Where(x => x.WardId == mkawardId).Select(y => y.WardNumber).FirstOrDefault();
            var LocationNameList = Globals.MKAGeoList.MKALocationList.Where(x => x.DistrictId == mkadistrictId
                && x.LLGId == mkallgId
                && x.WardId == mkawardId).Select(y => y.LocationName).ToList();
            string LocationStr = string.Join(",", LocationNameList);

            dtMKAForm11 = new DataTable("tblMKAForm11");
            dtMKAForm11.Clear();
            dtMKAForm11.Columns.Add("FormId", typeof(Int32));
            dtMKAForm11.Columns.Add("FormNo");


            dtMKASubForm11 = new DataTable("tblMKASubForm11");
            dtMKASubForm11.Clear();
            dtMKASubForm11.Columns.Add("FormId", typeof(Int32));
            dtMKASubForm11.Columns.Add("FormNo");
            dtMKASubForm11.Columns.Add("QRCodeUrl");
            dtMKASubForm11.Columns.Add("Province");
            dtMKASubForm11.Columns.Add("District");
            dtMKASubForm11.Columns.Add("LLG");
            dtMKASubForm11.Columns.Add("WardNoWardName");
            dtMKASubForm11.Columns.Add("QRCodeNumber");

            for (int i = 0; i <= numberOfForms; i++)
            {
                DataRow drForm11 = dtMKAForm11.NewRow();
                DataRow drSubForm11 = dtMKASubForm11.NewRow();

                drForm11["FormId"] = (i + 1);
                drSubForm11["FormId"] = (i + 1);

                FormNo = this.GenerateFormNumber(i, startRange, mkadistrictId, mkawardId);
                FormNo.Replace("#", "");
                drForm11["FormNo"] = FormNo;
                drSubForm11["FormNo"] = FormNo;

                qrCodeHashNumber = GenerateQRCodeHashNumber(mkadistrictId, mkallgId, mkawardId, batchName, i, startRange);

                QRCodeUrl = this.GenerateQRCode(qrCodeHashNumber);
                drSubForm11["QRCodeUrl"] = @"file:\" + QRCodeUrl;

                drSubForm11["Province"] = "NCD";
                drSubForm11["District"] = DistrictName;
                drSubForm11["LLG"] = LLGName;
                drSubForm11["WardNoWardName"] = WardNumber + " - " + WardName;
                drSubForm11["QRCodeNumber"] = qrCodeHashNumber.Replace("#","-");

                dtMKAForm11.Rows.Add(drForm11);
                dtMKASubForm11.Rows.Add(drSubForm11);

                if (i == 0)
                {
                    firstFormNo = FormNo;
                }
                else if (i == numberOfForms)
                {
                    lastFormNo = FormNo;
                }
            }

            if ((dtMKAForm11 != null && dtMKASubForm11 != null) && (dtMKAForm11.Rows.Count > 0 && dtMKASubForm11.Rows.Count > 0))
            {
                dsMKAForm11 = new DataSet();
                dsMKAForm11.Tables.AddRange(new DataTable[] { dtMKAForm11, dtMKASubForm11 });

                if (dsMKAForm11.Tables[0].Rows != null && dsMKAForm11.Tables[0].Rows.Count > 0)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    dsMKAForm11.Tables[0].TableName = "tblMKAForm11";

                    string formRange = string.Empty;

                    if (!string.IsNullOrWhiteSpace(firstFormNo) && !string.IsNullOrWhiteSpace(lastFormNo))
                    {
                        formRange = firstFormNo + "-" + lastFormNo;
                    }
                    else
                    {
                        formRange = firstFormNo;
                    }

                    ReportParameter[] param = new ReportParameter[8];
                    param[0] = new ReportParameter("Region", "-", true);
                    param[1] = new ReportParameter("Province", "NCD", true);
                    param[2] = new ReportParameter("District", DistrictName, true);
                    param[3] = new ReportParameter("LLG", LLGName, true);
                    param[4] = new ReportParameter("Ward", WardName, true);
                    param[5] = new ReportParameter("Location", LocationStr, true);
                    param[6] = new ReportParameter("FormCount", Convert.ToString(numberOfForms + 1), true);
                    param[7] = new ReportParameter("FormRange", (formRange), true);

                    ReportDataSource dataSource = new ReportDataSource("ds_MKAForm11", dsMKAForm11.Tables[0].Copy());
                    ReportViewer viewer = new ReportViewer();
                    viewer.LocalReport.Refresh();
                    viewer.LocalReport.ReportPath = "Report/MKAForm11.rdlc";
                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.SetParameters(param);
                    viewer.LocalReport.DataSources.Add(dataSource);
                    viewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetEnrolmentFormsSubDataSource);
                    bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                }
            }

            return bytes;
        }

        #endregion

        #region SetEnrolmentFormsSubDataSource
        /// <summary>
        /// Sets the enrolment forms sub data source.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SubreportProcessingEventArgs"/> instance containing the event data.</param>
        private void SetEnrolmentFormsSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            long FormId = Convert.ToInt32(e.Parameters[0].Values[0]);

            if (FormId > 0)
            {
                DataTable tblSubRCardFiltered = dsMKAForm11.Tables[1].AsEnumerable().Where(row => row.Field<int>("FormId") == FormId).CopyToDataTable();
                tblSubRCardFiltered.TableName = "tblSubMKAForm11";
                ReportDataSource datasource = new ReportDataSource("ds_SubMKAForm11", tblSubRCardFiltered);
                e.DataSources.Add(datasource);
            }
        }
        #endregion

        #endregion
    }
}
