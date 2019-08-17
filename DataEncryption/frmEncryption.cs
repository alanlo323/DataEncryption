using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DataEncryption
{
    public partial class frmEncryption : Form
    {
        byte[] decryptedData;
        public frmEncryption()
        {
            InitializeComponent();
        }

        public static bool IsValidImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            try
            {
                Object result = null;
                switch (cbEncryptionAlgorithm.SelectedIndex)
                {
                    case 0:
                        result = Encryption.AESThenHMAC.SimpleEncryptWithPassword(rtbSource.Text, tbPassword.Text);
                        break;
                    case 1:
                        result = Encryption.AESGCM.SimpleEncryptWithPassword(rtbSource.Text, tbPassword.Text);
                        break;
                    default:
                        MessageBox.Show("Please select an encryption algorithm!");
                        return;
                }
                rtbSource.Text = result == null ? "" : result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {
                object result = null;
                switch (cbEncryptionAlgorithm.SelectedIndex)
                {
                    case 0:
                        result = Encryption.AESThenHMAC.SimpleDecryptWithPassword(rtbSource.Text, tbPassword.Text);
                        break;
                    case 1:
                        result = Encryption.AESGCM.SimpleDecryptWithPassword(rtbSource.Text, tbPassword.Text);
                        break;
                    default:
                        MessageBox.Show("Please select an encryption algorithm!");
                        return;
                }
                if (result != null)
                {
                    decryptedData = (byte[])result;
                    result = Encoding.UTF8.GetString((byte[])result);
                }
                rtbSource.Text = result == null ? "" : result.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmEncryption_Load(object sender, EventArgs e)
        {
            MaximumSize = MinimumSize = Size;
            cbEncryptionAlgorithm.SelectedItem = null;
            cbEncryptionAlgorithm.SelectedText = "--select--";
        }

        private void pbPassword_MouseDown(object sender, MouseEventArgs e)
        {
            tbPassword.UseSystemPasswordChar = false;
        }

        private void pbPassword_MouseUp(object sender, MouseEventArgs e)
        {
            tbPassword.UseSystemPasswordChar = true;
        }

        private void labDragDropHints_DragOver(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            rtbSource.Text = fileList[0];
        }

        private void BtnSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "Save as"
            };
            if (decryptedData == null)
            {
                sfd.AddExtension = true;
                sfd.DefaultExt = "txt|txt";
            }
            sfd.ShowDialog();
            string path = sfd.FileName;
            if (decryptedData == null)
            {
                decryptedData = Encoding.ASCII.GetBytes(rtbSource.Text);
            }
            ByteArrayToFile(path, decryptedData);
        }
    }
}
