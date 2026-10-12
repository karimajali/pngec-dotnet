namespace PNGEC.API.Domain.Mobile.Voter
{
    public class FingerPrintModel
    {
        public int ElectorId { get; set; }
        public string ElectorFingerPrintImage { get; set; }

        public byte[] ElectorFingerPrintImageByte { get; set; }

        public string decryptedFingerPrint { get; set; }

        public string ElectorSignatureImage { get; set; }

        /// <summary>
        /// Gets or sets the elector signature image URL.
        /// </summary>
        /// <value>
        /// The elector signature image URL.
        /// </value>
        public string ElectorSignatureImageUrl { get; set; }

        /// <summary>
        /// Sets the image URL.
        /// </summary>
        /// <param name="apiURL">The API URL.</param>
        public void SetSignatureImageUrl(string apiURL)
        {
            if (!string.IsNullOrWhiteSpace(this.ElectorSignatureImage))
            {
                this.ElectorSignatureImageUrl = string.Format("{0}/{1}/{2}/{3}/{4}", apiURL, Constants.UploadFolderPath, Constants.ElectorSignatureImageFolderPath, this.ElectorId.ToString(), this.ElectorSignatureImage);
            }
        }
    }
}
