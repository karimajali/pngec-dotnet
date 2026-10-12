namespace PNGEC.Desktop.Forms
{
    partial class Map
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
            this.vbMap = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // vbMap
            // 
            this.vbMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vbMap.Location = new System.Drawing.Point(0, 0);
            this.vbMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.vbMap.Name = "vbMap";
            this.vbMap.Size = new System.Drawing.Size(800, 450);
            this.vbMap.TabIndex = 0;
            this.vbMap.Url = new System.Uri("https://www.google.co.in/maps", System.UriKind.Absolute);
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.vbMap);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Map";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Map";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser vbMap;
    }
}