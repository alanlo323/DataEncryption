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
            this.btnReset = new System.Windows.Forms.Button();
            this.cbRandomAlgorithm = new System.Windows.Forms.CheckBox();
            this.nudEncryptTimes = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutoDecrypt = new System.Windows.Forms.CheckBox();
            this.pbInfoTotal = new System.Windows.Forms.ProgressBar();
            this.labLayerDecrypted = new System.Windows.Forms.Label();
            this.pbInfoSession = new System.Windows.Forms.ProgressBar();
            this.btnStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEncryptTimes)).BeginInit();
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
            this.rtbSource.TextChanged += new System.EventHandler(this.rtbSource_TextChanged);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncrypt.Location = new System.Drawing.Point(12, 408);
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
            this.btnDecrypt.Location = new System.Drawing.Point(403, 408);
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
            "AES256-GCM",
            "AESHMA256"});
            this.cbEncryptionAlgorithm.Location = new System.Drawing.Point(146, 466);
            this.cbEncryptionAlgorithm.Name = "cbEncryptionAlgorithm";
            this.cbEncryptionAlgorithm.Size = new System.Drawing.Size(95, 22);
            this.cbEncryptionAlgorithm.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("Calibri", 8.7F);
            this.tbPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tbPassword.Location = new System.Drawing.Point(146, 494);
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
            this.labEncryptionAlgorithm.Location = new System.Drawing.Point(12, 469);
            this.labEncryptionAlgorithm.Name = "labEncryptionAlgorithm";
            this.labEncryptionAlgorithm.Size = new System.Drawing.Size(128, 17);
            this.labEncryptionAlgorithm.TabIndex = 5;
            this.labEncryptionAlgorithm.Text = "EncryptionAlgorithm:";
            this.labEncryptionAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.labPassword.Location = new System.Drawing.Point(75, 496);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(65, 17);
            this.labPassword.TabIndex = 6;
            this.labPassword.Text = "Password:";
            this.labPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbPassword
            // 
            this.pbPassword.Image = global::DataEncryption.Properties.Resources.eye;
            this.pbPassword.Location = new System.Drawing.Point(247, 494);
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
            this.labDragDropHints.Location = new System.Drawing.Point(12, 210);
            this.labDragDropHints.Name = "labDragDropHints";
            this.labDragDropHints.Size = new System.Drawing.Size(776, 195);
            this.labDragDropHints.TabIndex = 8;
            this.labDragDropHints.Tag = "";
            this.labDragDropHints.Text = "Drag file here to import";
            this.labDragDropHints.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labDragDropHints.DragDrop += new System.Windows.Forms.DragEventHandler(this.labDragDropHints_DragDrop);
            this.labDragDropHints.DragEnter += new System.Windows.Forms.DragEventHandler(this.labDragDropHints_DragEnter);
            this.labDragDropHints.DragOver += new System.Windows.Forms.DragEventHandler(this.labDragDropHints_DragOver);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(403, 492);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(385, 23);
            this.btnReset.TabIndex = 10;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cbRandomAlgorithm
            // 
            this.cbRandomAlgorithm.AutoSize = true;
            this.cbRandomAlgorithm.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.cbRandomAlgorithm.Location = new System.Drawing.Point(247, 467);
            this.cbRandomAlgorithm.Name = "cbRandomAlgorithm";
            this.cbRandomAlgorithm.Size = new System.Drawing.Size(111, 21);
            this.cbRandomAlgorithm.TabIndex = 11;
            this.cbRandomAlgorithm.Text = "Random Select";
            this.cbRandomAlgorithm.UseVisualStyleBackColor = true;
            this.cbRandomAlgorithm.CheckedChanged += new System.EventHandler(this.cbRandom_CheckedChanged);
            // 
            // nudEncryptTimes
            // 
            this.nudEncryptTimes.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.nudEncryptTimes.Location = new System.Drawing.Point(146, 437);
            this.nudEncryptTimes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEncryptTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEncryptTimes.Name = "nudEncryptTimes";
            this.nudEncryptTimes.Size = new System.Drawing.Size(95, 23);
            this.nudEncryptTimes.TabIndex = 15;
            this.nudEncryptTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudEncryptTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.label2.Location = new System.Drawing.Point(41, 439);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Encrytion times:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbAutoDecrypt
            // 
            this.cbAutoDecrypt.AutoSize = true;
            this.cbAutoDecrypt.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.cbAutoDecrypt.Location = new System.Drawing.Point(403, 439);
            this.cbAutoDecrypt.Name = "cbAutoDecrypt";
            this.cbAutoDecrypt.Size = new System.Drawing.Size(141, 21);
            this.cbAutoDecrypt.TabIndex = 17;
            this.cbAutoDecrypt.Text = "Decrypt to last layer";
            this.cbAutoDecrypt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cbAutoDecrypt.UseVisualStyleBackColor = true;
            this.cbAutoDecrypt.CheckedChanged += new System.EventHandler(this.CbAutoDecrypt_CheckedChanged);
            // 
            // pbInfoTotal
            // 
            this.pbInfoTotal.Enabled = false;
            this.pbInfoTotal.Location = new System.Drawing.Point(12, 521);
            this.pbInfoTotal.Name = "pbInfoTotal";
            this.pbInfoTotal.Size = new System.Drawing.Size(776, 23);
            this.pbInfoTotal.TabIndex = 18;
            // 
            // labLayerDecrypted
            // 
            this.labLayerDecrypted.Font = new System.Drawing.Font("Calibri", 9.8F);
            this.labLayerDecrypted.Location = new System.Drawing.Point(550, 439);
            this.labLayerDecrypted.Name = "labLayerDecrypted";
            this.labLayerDecrypted.Size = new System.Drawing.Size(238, 17);
            this.labLayerDecrypted.TabIndex = 19;
            this.labLayerDecrypted.Text = "Layer decrypted: ";
            this.labLayerDecrypted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labLayerDecrypted.Visible = false;
            // 
            // pbInfoSession
            // 
            this.pbInfoSession.Enabled = false;
            this.pbInfoSession.Location = new System.Drawing.Point(12, 550);
            this.pbInfoSession.Name = "pbInfoSession";
            this.pbInfoSession.Size = new System.Drawing.Size(776, 23);
            this.pbInfoSession.TabIndex = 20;
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(403, 463);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(385, 23);
            this.btnStop.TabIndex = 21;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // frmEncryption
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.pbInfoSession);
            this.Controls.Add(this.labLayerDecrypted);
            this.Controls.Add(this.pbInfoTotal);
            this.Controls.Add(this.cbAutoDecrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudEncryptTimes);
            this.Controls.Add(this.cbRandomAlgorithm);
            this.Controls.Add(this.btnReset);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Encryption";
            this.Load += new System.EventHandler(this.frmEncryption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEncryptTimes)).EndInit();
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
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox cbRandomAlgorithm;
        private System.Windows.Forms.NumericUpDown nudEncryptTimes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbAutoDecrypt;
        private System.Windows.Forms.ProgressBar pbInfoTotal;
        private System.Windows.Forms.Label labLayerDecrypted;
        private System.Windows.Forms.ProgressBar pbInfoSession;
        private System.Windows.Forms.Button btnStop;
    }
}

