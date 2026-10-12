using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PNGEC.API.Domain.EnrolmentForm;

namespace PNGEC.API.Domain.MKAEnrolmentForm
{
    public class MKAEnrolmentFormModel
    {
        /// <summary>
        /// Gets or sets the createddate.
        /// </summary>
        /// <value>
        /// The createddate.
        /// </value>
        private DateTime createddate { get; set; }

        /// <summary>
        /// Gets or sets the Enrolment Form identifier.
        /// </summary>
        /// <value>
        /// The Enrolment Form identifier.
        /// </value>
        public int EnrolmentFormId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the district identifier.
        /// </summary>
        /// <value>
        /// The district identifier.
        /// </value>
        public int MKADistrictId { get; set; }

        /// <summary>
        /// Gets or sets the LLG identifier.
        /// </summary>
        /// <value>
        /// The LLG identifier.
        /// </value>
        public int MKALLGId { get; set; }

        /// <summary>
        /// Gets or sets the ward identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int MKAWardId { get; set; }
        /// <summary>
        /// Gets or sets the clan identifier.
        /// </summary>
        /// <value>
        /// The ward identifier.
        /// </value>
        public int MKAClanId { get; set; }

        /// <summary>
        /// Gets or sets the Created Date
        /// </summary>
        /// <value>
        /// The Created Date.
        /// </value>
        public DateTime CreatedDate
        {
            get { return createddate; }
            set
            {
                createddate = value;
                if (value != null)
                {
                    //DateTime utcDate = DateTime.SpecifyKind(value, DateTimeKind.Utc);
                    //DateTime localDate = utcDate.ToLocalTime();
                    //Date = String.Format("{0:dd-MM-yyyy HH:mm}", localDate);
                    Date = String.Format("{0:dd-MM-yyyy HH:mm}", value);
                }
                else
                {
                    Date = string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Email Address.
        /// </summary>
        /// <value>
        /// The Email Address.
        /// </value>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the name of the district.
        /// </summary>
        /// <value>
        /// The name of the district.
        /// </value>
        public string DistrictName { get; set; }

        /// <summary>
        /// Gets or sets the name of the LLG.
        /// </summary>
        /// <value>
        /// The name of the LLG.
        /// </value>
        public string LLGName { get; set; }

        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string WardName { get; set; }
        /// <summary>
        /// Gets or sets the name of the ward.
        /// </summary>
        /// <value>
        /// The name of the ward.
        /// </value>
        public string ClanName { get; set; }
        /// <summary>
        /// Gets or sets the start range.
        /// </summary>
        /// <value>
        /// The start range.
        /// </value>
        public int StartRange { get; set; }

        /// <summary>
        /// Gets or sets the end range.
        /// </summary>
        /// <value>
        /// The end range.
        /// </value>
        public int EndRange { get; set; }

        /// <summary>
        /// Gets or sets the name of the batch.
        /// </summary>
        /// <value>
        /// The name of the batch.
        /// </value>
        public string BatchName { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the number of forms.
        /// </summary>
        /// <value>
        /// The number of forms.
        /// </value>
        public int NumberOfForms { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public string Attachment { get; set; }

        /// <summary>
        /// Gets or sets the attachment URL.
        /// </summary>
        /// <value>
        /// The attachment URL.
        /// </value>
        public List<AttachmnetModel> AttachmentUrlList { get; set; } = new List<AttachmnetModel>();

        /// <summary>
        /// Sets the attachment URL.
        /// </summary>
        /// <param name="apiURL">The API URL.</param>
        public void SetAttachmentUrl(string apiURL)
        {
            if (!string.IsNullOrWhiteSpace(this.Attachment))
            {
                foreach (var attch in this.Attachment.ToString().Split(','))
                {
                    this.AttachmentUrlList.Add(new AttachmnetModel()
                    {
                        AttachmnetUrl = string.Format("{0}/{1}/{2}/{3}", apiURL, Constants.UploadFolderPath,
                        Constants.ElectorAttachmentFolderPath, attch),
                        AttachmnetName = attch
                    });
                }
                //this.AttachmentUrl = string.Format("{0}/{1}/{2}/{3}", apiURL, Constants.UploadFolderPath,
                //    Constants.ElectorAttachmentFolderPath, this.Attachment);
            }
        }

        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        /// <value>
        /// The registration.
        /// </value>
        public int UsedFromRegistration { get; set; }

        /// <summary>
        /// Gets or sets the transfer in.
        /// </summary>
        /// <value>
        /// The transfer in.
        /// </value>
        public int UsedFromTransferIn { get; set; }

        /// <summary>
        /// Gets or sets the attachmnet to upoad.
        /// </summary>
        /// <value>
        /// The attachmnet to upoad.
        /// </value>
        public List<IFormFile> AttachmnetToUpoad { get; set; }

        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <value>
        /// The batch number.
        /// </value>
        public int NumberOfBatch { get; set; }
    }
}

