using System;
using System.Windows.Forms;

namespace PNGEC.Desktop
{
    public partial class SplashScreen : Form
    {
        #region variable declaration
        /// <summary>
        /// The TMR
        /// </summary>
        private Timer tmr;
        #endregion

        #region constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="SplashScreen"/> class from being created.
        /// </summary>
        public SplashScreen()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        /// <summary>
        /// Handles the Shown event of the SplashScreen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            tmr = new Timer();
            tmr.Interval = 3000;
            tmr.Start();
            tmr.Tick += tmr_Tick;
        }

        /// <summary>
        /// Handles the Tick event of the tmr control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void tmr_Tick(object sender, EventArgs e)

        {
            tmr.Stop();
            Login login = new Login();
            login.Show();
            this.Hide();
        }
        #endregion
    }
}
