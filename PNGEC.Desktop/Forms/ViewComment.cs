using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ViewComment : Form
    {
        #region variable declaration
        /// <summary>
        /// The comment
        /// </summary>
        public string Comment = string.Empty;
        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComment"/> class.
        /// </summary>
        public ViewComment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewComment"/> class.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public ViewComment(string comment)
        {
            InitializeComponent();
            this.Comment = comment;
            txtCommentView.Text = this.Comment;
        }

        #endregion
    }
}
