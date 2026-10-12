namespace PNGEC.Desktop.Model
{
    /// <summary>
    /// Download province model class
    /// </summary>
    public class DownloadProvince
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is checked; otherwise, <c>false</c>.
        /// </value>
        public bool IsChecked { get; set; }

        /// <summary>
        /// The file download status
        /// </summary>
        private DownloadStatus fileDownloadStatus;

        /// <summary>
        /// Gets or sets the province identifier.
        /// </summary>
        /// <value>
        /// The province identifier.
        /// </value>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the province.
        /// </summary>
        /// <value>
        /// The name of the province.
        /// </value>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public string Percentage { get; set; }

        /// <summary>
        /// Gets or sets the file download status.
        /// </summary>
        /// <value>
        /// The file download status.
        /// </value>
        public DownloadStatus FileDownloadStatus
        {
            get { return fileDownloadStatus; }
            set
            {
                fileDownloadStatus = value;
                if (fileDownloadStatus == DownloadStatus.Downloaded)
                {
                    FileDownloadStatusText = "Downloaded";
                }
                else if (fileDownloadStatus == DownloadStatus.Downloading)
                {
                    FileDownloadStatusText = "Downloading";
                }
                else if (fileDownloadStatus == DownloadStatus.Fail)
                {
                    FileDownloadStatusText = "Fail";
                }
                else if (fileDownloadStatus == DownloadStatus.NotStarted)
                {
                    FileDownloadStatusText = "Not started";
                }
                else if (fileDownloadStatus == DownloadStatus.Pause)
                {
                    FileDownloadStatusText = "Pause";
                }
                else
                {
                    FileDownloadStatusText = "Not started";
                }

            }
        }

        /// <summary>
        /// Gets or sets the file download status text.
        /// </summary>
        /// <value>
        /// The file download status text.
        /// </value>
        public string FileDownloadStatusText { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }
    }

    /// <summary>
    /// Download status enum
    /// </summary>
    public enum DownloadStatus : int
    {
        Downloaded,
        Downloading,
        Fail,
        NotStarted,
        Pause,
        InQueue
    }
}
