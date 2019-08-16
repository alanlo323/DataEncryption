using System;
using System.Windows.Forms;

namespace DataEncryption
{
    public partial class frmEncryption : Form
    {
        public frmEncryption()
        {
            InitializeComponent();
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
                        break;
                    case 1:
                        result = Encryption.AESGCM.SimpleDecryptWithPassword(rtbSource.Text, tbPassword.Text);
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
