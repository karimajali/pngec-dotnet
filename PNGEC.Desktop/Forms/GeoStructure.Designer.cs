namespace PNGEC.Desktop.Forms
{
    partial class GeoStructure
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtSequence = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSequence = new System.Windows.Forms.Label();
            this.txtGeoStructureName = new System.Windows.Forms.TextBox();
            this.lblGeoStructureName = new System.Windows.Forms.Label();
            this.btnGeoStructureList = new System.Windows.Forms.Button();
            this.btnAddNew = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Arial", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(470, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(146, 41);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.Black;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("Arial", 10F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(290, 227);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(139, 41);
            this.btnAdd.TabIndex = 22;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(290, 162);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(413, 28);
            this.cmbStatus.TabIndex = 20;
            // 
            // txtSequence
            // 
            this.txtSequence.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSequence.Location = new System.Drawing.Point(290, 124);
            this.txtSequence.Name = "txtSequence";
            this.txtSequence.Size = new System.Drawing.Size(413, 26);
            this.txtSequence.TabIndex = 19;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(12, 168);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 20);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "Status";
            // 
            // lblSequence
            // 
            this.lblSequence.AutoSize = true;
            this.lblSequence.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSequence.Location = new System.Drawing.Point(12, 131);
            this.lblSequence.Name = "lblSequence";
            this.lblSequence.Size = new System.Drawing.Size(82, 20);
            this.lblSequence.TabIndex = 16;
            this.lblSequence.Text = "Sequence";
            // 
            // txtGeoStructureName
            // 
            this.txtGeoStructureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGeoStructureName.Location = new System.Drawing.Point(290, 87);
            this.txtGeoStructureName.Name = "txtGeoStructureName";
            this.txtGeoStructureName.Size = new System.Drawing.Size(413, 26);
            this.txtGeoStructureName.TabIndex = 15;
            // 
            // lblGeoStructureName
            // 
            this.lblGeoStructureName.AutoSize = true;
            this.lblGeoStructureName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeoStructureName.Location = new System.Drawing.Point(12, 93);
            this.lblGeoStructureName.Name = "lblGeoStructureName";
            this.lblGeoStructureName.Size = new System.Drawing.Size(156, 40);
            this.lblGeoStructureName.TabIndex = 14;
            this.lblGeoStructureName.Text = "Geo Structure Name\r\n\r\n";
            // 
            // btnGeoStructureList
            // 
            this.btnGeoStructureList.Font = new System.Drawing.Font("Arial", 10F);
            this.btnGeoStructureList.Location = new System.Drawing.Point(208, 27);
            this.btnGeoStructureList.Name = "btnGeoStructureList";
            this.btnGeoStructureList.Size = new System.Drawing.Size(165, 34);
            this.btnGeoStructureList.TabIndex = 13;
            this.btnGeoStructureList.Text = "GeoStructure List";
            this.btnGeoStructureList.UseVisualStyleBackColor = true;
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackColor = System.Drawing.Color.Black;
            this.btnAddNew.Font = new System.Drawing.Font("Arial", 10F);
            this.btnAddNew.ForeColor = System.Drawing.Color.White;
            this.btnAddNew.Location = new System.Drawing.Point(12, 27);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(168, 34);
            this.btnAddNew.TabIndex = 12;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = false;
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Location = new System.Drawing.Point(93, 336);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(0, 13);
            this.lblResponse.TabIndex = 24;
            // 
            // GeoStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblResponse);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.txtSequence);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblSequence);
            this.Controls.Add(this.txtGeoStructureName);
            this.Controls.Add(this.lblGeoStructureName);
            this.Controls.Add(this.btnGeoStructureList);
            this.Controls.Add(this.btnAddNew);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeoStructure";
            this.Text = "GeoStructure";
            this.Load += new System.EventHandler(this.GeoStructure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtSequence;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSequence;
        private System.Windows.Forms.TextBox txtGeoStructureName;
        private System.Windows.Forms.Label lblGeoStructureName;
        private System.Windows.Forms.Button btnGeoStructureList;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Label lblResponse;
    }
}