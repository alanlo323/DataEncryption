using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace DataEncryption
{
    public partial class frmEncryption : Form
    {
        readonly string labDragDropHintsString = "Drag file here to import";

        string importPath;
        string firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
        private static object syncObj = new object();

        public frmEncryption()
        {
            InitializeComponent();
        }

        private void frmEncryption_Load(object sender, EventArgs e)
        {
            MaximumSize = MinimumSize = Size;
            cbEncryptionAlgorithm.SelectedItem = null;
            cbEncryptionAlgorithm.Text = "--select--";
            tbPassword.Text = firstMacAddress;
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
                for (int i = 0; i < nudEncryptTimes.Value; i++)
                {
                    Encrypt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            bool temp = cbAutoDecrypt.Checked;
            do
            {
                try
                {
                    Decrypt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            } while (cbAutoDecrypt.Checked);
            cbAutoDecrypt.Checked = temp;
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
            rtbSource.Text = null;
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            importPath = fileList[0];
            labDragDropHints.Text = labDragDropHintsString + "\n" + importPath;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            importPath = null;
            rtbSource.Text = "";
            labDragDropHints.Text = labDragDropHintsString;
            cbEncryptionAlgorithm.SelectedItem = null;
            cbEncryptionAlgorithm.Text = "--select--";
            nudEncryptTimes.Value = 1;
            tbPassword.Text = firstMacAddress;
        }

        private void rtbSource_TextChanged(object sender, EventArgs e)
        {
            labDragDropHints.Text = labDragDropHintsString;
            importPath = null;
        }

        private void cbRandom_CheckedChanged(object sender, EventArgs e)
        {
            cbEncryptionAlgorithm.Enabled = !cbRandomAlgorithm.Checked;
        }

        private void Encrypt()
        {
            try
            {
                bool isBytesData = false;
                byte[] secretMessage = null;
                if (importPath != null)
                {
                    try
                    {
                        using (FileStream fs = File.Open(Path.GetFullPath(importPath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (BufferedStream bs = new BufferedStream(fs))
                        using (StreamReader sr = new StreamReader(bs))
                        {
                            using (var memstream = new MemoryStream())
                            {
                                var buffer = new byte[512];
                                var bytesRead = default(int);
                                while ((bytesRead = sr.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    memstream.Write(buffer, 0, bytesRead);
                                }
                                secretMessage = memstream.ToArray();
                                isBytesData = true;
                            }
                        }

                    }
                    catch (Exception) { }
                }
                if (!isBytesData)
                {
                    secretMessage = Encoding.UTF8.GetBytes(rtbSource.Text);
                }
                byte[] result = null;
                int encryptionAlgorithm = (cbRandomAlgorithm.Checked) ? ((new Random().NextDouble() >= 0.5) ? 0 : 1) : cbEncryptionAlgorithm.SelectedIndex;
                switch (encryptionAlgorithm)
                {
                    case 0:
                        result = Encryption.AESGCM.SimpleEncryptWithPassword(secretMessage, tbPassword.Text);
                        break;
                    case 1:
                        result = Encryption.AESThenHMAC.SimpleEncryptWithPassword(secretMessage, tbPassword.Text);
                        break;
                    default:
                        throw new Exception("Please select an encryption algorithm!");
                }
                if (isBytesData)
                {
                    ByteArrayToFile(importPath, result);
                    labDragDropHints.Text = labDragDropHintsString + "\n" + importPath;
                }
                else
                {
                    rtbSource.Text = (result == null) ? "" : Convert.ToBase64String(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Decrypt()
        {
            try
            {
                bool isBytesData = false;
                byte[] secretMessage = null;
                if (importPath != null)
                {
                    try
                    {
                        using (FileStream fs = File.Open(Path.GetFullPath(importPath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (BufferedStream bs = new BufferedStream(fs))
                        using (StreamReader sr = new StreamReader(bs))
                        {
                            using (var memstream = new MemoryStream())
                            {
                                var buffer = new byte[512];
                                var bytesRead = default(int);
                                while ((bytesRead = sr.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    memstream.Write(buffer, 0, bytesRead);
                                }
                                secretMessage = memstream.ToArray();
                                isBytesData = true;
                            }
                        }

                    }
                    catch (Exception) { }
                }
                if (!isBytesData)
                {
                    secretMessage = Convert.FromBase64String(rtbSource.Text);
                }
                byte[] result = null;

                if (!cbAutoDecrypt.Checked)
                {
                    switch (cbEncryptionAlgorithm.SelectedIndex)
                    {
                        case 0:
                            result = Encryption.AESGCM.SimpleDecryptWithPassword(secretMessage, tbPassword.Text);
                            break;
                        case 1:
                            result = Encryption.AESThenHMAC.SimpleDecryptWithPassword(secretMessage, tbPassword.Text);
                            break;
                        default:
                            throw new Exception("Please select an encryption algorithm!");
                    }
                }
                else
                {
                    for (int i = 0; i < 1; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                result = Encryption.AESGCM.SimpleDecryptWithPassword(secretMessage, tbPassword.Text);
                                break;
                            case 1:
                                result = Encryption.AESThenHMAC.SimpleDecryptWithPassword(secretMessage, tbPassword.Text);
                                break;
                            default:
                                throw new Exception("Please select an encryption algorithm!");
                        }
                        if (result != null)
                        {
                            break;
                        }
                    }
                }
                if (cbAutoDecrypt.Checked)
                {
                    if (result != null)
                    {
                        if (isBytesData)
                        {
                            ByteArrayToFile(importPath, result);
                            labDragDropHints.Text = labDragDropHintsString + "\n" + importPath;
                        }
                        else
                        {
                            rtbSource.Text = (result == null) ? "" : Encoding.UTF8.GetString(result);
                        }
                    }
                    else
                    {
                        cbAutoDecrypt.Checked = false;
                    }
                }
                else
                {
                    if (isBytesData)
                    {
                        ByteArrayToFile(importPath, result);
                        labDragDropHints.Text = labDragDropHintsString + "\n" + importPath;
                    }
                    else
                    {
                        rtbSource.Text = (result == null) ? "" : Encoding.UTF8.GetString(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
