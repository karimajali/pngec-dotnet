using PNGEC.API.Domain.EnrolmentForm;
using PNGEC.API.Helper;
using PNGEC.Desktop.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class EnrolAttachment : Form
    {
        #region variable declaration
        /// <summary>
        /// The attachmnets lists
        /// </summary>
        private List<AttachmnetModel> attachmnetsLists = new List<AttachmnetModel>();
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolAttachment"/> class.
        /// </summary>
        public EnrolAttachment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnrolAttachment"/> class.
        /// </summary>
        /// <param name="attachmnets">The attachmnets.</param>
        public EnrolAttachment(List<AttachmnetModel> attachmnets)
        {
            InitializeComponent();
            this.attachmnetsLists = attachmnets;
            BindEnrolmentFormGrid(attachmnets);
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the btnDownloadAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDownloadAll_Click(object sender, EventArgs e)
        {
            if (this.attachmnetsLists.Any() && this.attachmnetsLists.Count >= 1)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        var path = fbd.SelectedPath;
                        foreach (var attach in this.attachmnetsLists)
                        {
                            var attachment = attach.AttachmnetUrl;
                            var fileName = attach.AttachmnetName;
                            if (!string.IsNullOrEmpty(Convert.ToString(attachment)) &&
                                !string.IsNullOrWhiteSpace(Convert.ToString(attachment)))
                            {
                                try
                                {
                                    using (var client = new WebClient())
                                    {
                                        var pat = System.IO.Path.Combine(new Uri(path).LocalPath, fileName);
                                        client.DownloadFile(attachment.ToString(), pat);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(Resources.ResourceManager.GetString("ErrorMessage"),
                                        "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    ErrorLog.Write("Enrolment Form", "downloadbtn_click", ex);
                                }
                            }
                        }
                        MessageBox.Show("Attachment's saved successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the attachmnetGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void attachmnetGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && attachmnetGrid.Columns[e.ColumnIndex].Name == "Download")
            {
                var attachment = attachmnetGrid.Rows[e.RowIndex].Cells["AttachmnetUrl"].Value;
                var fileName = attachmnetGrid.Rows[e.RowIndex].Cells["AttachmnetName"].Value;
                DownloadAttachmnets(attachment, fileName.ToString());
            }
        }

        #endregion

        #region Methods

        #region bind enrolment form grid
        /// <summary>
        /// Binds the enrolment form grid.
        /// </summary>
        /// <param name="attachmnetLists">The attachmnet lists.</param>
        private void BindEnrolmentFormGrid(List<AttachmnetModel> attachmnetLists)
        {
            if (attachmnetGrid.Columns.Count > 0)
            {
                attachmnetGrid.Columns.Clear();
            }
            attachmnetGrid.DataSource = attachmnetLists;
            attachmnetGrid.Columns["AttachmnetUrl"].Visible = false;

            attachmnetGrid.Columns["AttachmnetName"].HeaderText = "Attachment name";

            DataGridViewButtonColumn attachmentButton = new DataGridViewButtonColumn();
            attachmentButton.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            attachmentButton.FlatStyle = FlatStyle.Standard;
            attachmentButton.HeaderText = "Attachment";
            attachmentButton.Name = "Download";
            attachmentButton.Text = "Download";
            attachmentButton.UseColumnTextForButtonValue = true;
            attachmentButton.Width = 80;
            if (attachmnetGrid.Columns["Save Attachment"] == null)
            {
                attachmnetGrid.Columns.Insert(attachmnetGrid.ColumnCount, attachmentButton);
            }
        }
        #endregion

        #region download attachmnets
        /// <summary>
        /// Downloads the attachmnets.
        /// </summary>
        /// <param name="attachmnet">The attachmnet.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="isSHowMesage">if set to <c>true</c> [is s how mesage].</param>
        private void DownloadAttachmnets(object attachmnet, string fileName = "downloaded_attachment", bool isSHowMesage = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(attachmnet)) && !string.IsNullOrWhiteSpace(Convert.ToString(attachmnet)))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = string.Format(@"{0}", fileName);
                    saveFileDialog.DefaultExt = GetExtension(fileName.ToString());
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(attachmnet.ToString(), saveFileDialog.FileName);
                            if (isSHowMesage)
                            {
                                MessageBox.Show("Attachment saved successfully ", "PNGEC", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
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

        #endregion
    }
}
