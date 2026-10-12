namespace PNGEC.Desktop
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.pbSplashBackGround = new System.Windows.Forms.PictureBox();
            this.pbSplashLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSplashBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSplashLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSplashBackGround
            // 
            this.pbSplashBackGround.BackColor = System.Drawing.SystemColors.Desktop;
            this.pbSplashBackGround.Image = global::PNGEC.Desktop.Properties.Resources.Splash;
            this.pbSplashBackGround.Location = new System.Drawing.Point(0, 0);
            this.pbSplashBackGround.Name = "pbSplashBackGround";
            this.pbSplashBackGround.Size = new System.Drawing.Size(600, 300);
            this.pbSplashBackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSplashBackGround.TabIndex = 0;
            this.pbSplashBackGround.TabStop = false;
            // 
            // pbSplashLogo
            // 
            this.pbSplashLogo.BackColor = System.Drawing.SystemColors.Desktop;
            this.pbSplashLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbSplashLogo.Image = global::PNGEC.Desktop.Properties.Resources.splash_logo;
            this.pbSplashLogo.Location = new System.Drawing.Point(136, 110);
            this.pbSplashLogo.Name = "pbSplashLogo";
            this.pbSplashLogo.Size = new System.Drawing.Size(328, 80);
            this.pbSplashLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSplashLogo.TabIndex = 1;
            this.pbSplashLogo.TabStop = false;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.pbSplashLogo);
            this.Controls.Add(this.pbSplashBackGround);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.Shown += new System.EventHandler(this.SplashScreen_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbSplashBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSplashLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSplashBackGround;
        private System.Windows.Forms.PictureBox pbSplashLogo;
    }
}