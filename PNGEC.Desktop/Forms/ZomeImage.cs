using System;
using System.Drawing;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class ZomeImage : Form
    {
        #region variable declaration
        /// <summary>
        /// The image
        /// </summary>
        private Image image;
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZomeImage"/> class.
        /// </summary>
        /// <param name="image">The image.</param>
        public ZomeImage(Image image)
        {
            InitializeComponent();
            this.image = image;
        }
        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the ZomeImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ZomeImage_Load(object sender, EventArgs e)
        {
            trackBar.Minimum = 1;
            trackBar.Maximum = 6;
            trackBar.SmallChange = 1;
            trackBar.LargeChange = 1;
            trackBar.UseWaitCursor = false;

            //minimizes the strutter
            this.DoubleBuffered = true;

            pbZoomImage.Image = this.image;

            // TrackBar set location
            trackBar.Width = 265;
            trackBar.Location = new Point((pnlControl.Width / 2) - (trackBar.Width / 2), trackBar.Location.Y);

            // TrackBar set location
            pbZoomImage.Width = (pnlImage.Width - 20);
            pbZoomImage.Height = (pnlImage.Height - 20);
            pbZoomImage.Location = new Point(10, 10);
        }

        /// <summary>
        /// Handles the Scroll event of the trackBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void trackBar_Scroll(object sender, EventArgs e)
        {
            if (trackBar.Value != 0)
            {
                pbZoomImage.Image = null;
                pbZoomImage.Image = ZoomPicture(this.image, new Size(trackBar.Value, trackBar.Value));
            }
        }

        #endregion

        #region Methods

        #region Zoom Picture
        /// <summary>
        /// Zooms the picture.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        Image ZoomPicture(Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width * size.Width),
                Convert.ToInt32(img.Height * size.Height));

            if(size.Width == 1 && size.Height == 1)
            {
                pbZoomImage.Size = new Size(875,375);

                // TrackBar set location
                pbZoomImage.Width = (pnlImage.Width - 20);
                pbZoomImage.Height = (pnlImage.Height - 20);
                pbZoomImage.Location = new Point(10, 10);
            }
            else
            {
                pbZoomImage.Size = new Size(Convert.ToInt32(img.Width * size.Width),
                Convert.ToInt32(img.Height * size.Height));
            }

            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }
        #endregion

        #endregion
    }
}
