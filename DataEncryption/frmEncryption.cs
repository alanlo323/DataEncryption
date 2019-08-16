using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DataEncryption
{
    public partial class frmEncryption : Form
    {
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
                Object result = null;
                switch (cbEncryptionAlgorithm.SelectedIndex)
                {
                    case 0:
                        result = Encryption.AESThenHMAC.SimpleDecryptWithPassword(rtbSource.Text, tbPassword.Text);
                        if (result != null)
                        {
                            if (!IsValidImage((byte[])result))
                            {
                                result = Encoding.UTF8.GetString((byte[])result);
                            } else
                            {
                                SaveFileDialog sfd = new SaveFileDialog();
                                sfd.ShowDialog();
                            }
                        }
                        break;
                    case 1:
                        result = Encryption.AESGCM.SimpleDecryptWithPassword(rtbSource.Text, tbPassword.Text);
                        result = (result == null) ? null : Encoding.UTF8.GetString((byte[])result);
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
    }
}
