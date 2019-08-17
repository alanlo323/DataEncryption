namespace DataEncryption
{
    partial class frmEncryption
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEncryption));
            this.rtbSource = new System.Windows.Forms.RichTextBox();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.cbEncryptionAlgorithm = new System.Windows.Forms.ComboBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.labEncryptionAlgorithm = new System.Windows.Forms.Label();
            this.labPassword = new System.Windows.Forms.Label();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.labDragDropHints = new System.Windows.Forms.Label();
            this.btnSaveFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbSource
            // 
            this.rtbSource.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbSource.Location = new System.Drawing.Point(12, 12);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.Size = new System.Drawing.Size(776, 195);
            this.rtbSource.TabIndex = 0;
            this.rtbSource.Text = "";
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncrypt.Location = new System.Drawing.Point(12, 213);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(385, 23);
            this.btnEncrypt.TabIndex = 1;
            this.btnEncrypt.Text = "Encrypt";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecrypt.Location = new System.Drawing.Point(403, 213);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(385, 23);
            this.btnDecrypt.TabIndex = 4;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // cbEncryptionAlgorithm
            // 
            this.cbEncryptionAlgorithm.Font = new System.Drawing.Font("Calibri", 8.7F);
            this.cbEncryptionAlgorithm.FormattingEnabled = true;
            this.cbEncryptionAlgorithm.Items.AddRange(new object[] {
            "AESHMA256",
            "AES256-GCM"});
            this.cbEncryptionAlgorithm.Location = new System.Drawing.Point(146, 242);
            this.cbEncryptionAlgorithm.Name = "cbEncryptionAlgorithm";
            this.cbEncryptionAlgorithm.Size = new System.Drawing.Size(95, 22);
            this.cbEncryptionAlgorithm.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Calibri", 8.7F);
            this.tbPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbPassword.Location = new System.Drawing.Point(146, 270);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(95, 22);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.WordWrap = false;
            // 
            // labEncryptionAlgorithm
            // 
            this.labEncryptionAlgorithm.AutoSize = true;
            this.labEncryptionAlgorithm.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.labEncryptionAlgorithm.Location = new System.Drawing.Point(12, 245);
            this.labEncryptionAlgorithm.Name = "labEncryptionAlgorithm";
            this.labEncryptionAlgorithm.Size = new System.Drawing.Size(128, 17);
            this.labEncryptionAlgorithm.TabIndex = 5;
            this.labEncryptionAlgorithm.Text = "EncryptionAlgorithm:";
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.labPassword.Location = new System.Drawing.Point(75, 272);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(65, 17);
            this.labPassword.TabIndex = 6;
            this.labPassword.Text = "Password:";
            // 
            // pbPassword
            // 
            this.pbPassword.Image = global::DataEncryption.Properties.Resources.eye;
            this.pbPassword.Location = new System.Drawing.Point(247, 270);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(22, 22);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPassword.TabIndex = 7;
            this.pbPassword.TabStop = false;
            this.pbPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbPassword_MouseDown);
            this.pbPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbPassword_MouseUp);
            // 
            // labDragDropHints
            // 
            this.labDragDropHints.AllowDrop = true;
            this.labDragDropHints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labDragDropHints.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.labDragDropHints.Location = new System.Drawing.Point(403, 270);
            this.labDragDropHints.Name = "labDragDropHints";
            this.labDragDropHints.Size = new System.Drawing.Size(385, 27);
            this.labDragDropHints.TabIndex = 8;
            this.labDragDropHints.Text = "Drag the file here to import the path";
            this.labDragDropHints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labDragDropHints.DragOver += new System.Windows.Forms.DragEventHandler(this.labDragDropHints_DragOver);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveFile.Location = new System.Drawing.Point(403, 241);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(385, 23);
            this.btnSaveFile.TabIndex = 9;
            this.btnSaveFile.Text = "Save To File";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.BtnSaveFile_Click);
            // 
            // frmEncryption
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 302);
            this.Controls.Add(this.btnSaveFile);
            this.Controls.Add(this.labDragDropHints);
            this.Controls.Add(this.pbPassword);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.labEncryptionAlgorithm);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.cbEncryptionAlgorithm);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.rtbSource);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEncryption";
            this.Text = "Encryption";
            this.Load += new System.EventHandler(this.frmEncryption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbSource;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ComboBox cbEncryptionAlgorithm;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label labEncryptionAlgorithm;
        private System.Windows.Forms.Label labPassword;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.Label labDragDropHints;
        private System.Windows.Forms.Button btnSaveFile;
    }
}

