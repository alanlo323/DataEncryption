using System;
using System.ComponentModel;
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
        readonly string labLayerDecryptedString = "Layer decrypted: ";
        readonly string FileSavedMarker = "*fileSavedMarker--@$";

        string importPath;
        string firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
        BackgroundWorker bw;
        string finalResult;
        WorkerType workerType;

        string _rtbSourceText;
        string _tbPasswordText;
        bool _cbAutoDecryptChecked_temp;
        bool _cbAutoDecryptChecked;
        bool _cbRandomAlgorithmChecked;
        int _cbEncryptionAlgorithmSelectedIndex;

        enum WorkerType
        {
            Encryption,
            Decryption
        }

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
            bw = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
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
            if (bw.IsBusy != true)
            {
                _cbRandomAlgorithmChecked = cbRandomAlgorithm.Checked;
                _cbEncryptionAlgorithmSelectedIndex = cbEncryptionAlgorithm.SelectedIndex;
                _rtbSourceText = rtbSource.Text;
                _tbPasswordText = tbPassword.Text;
                workerType = WorkerType.Encryption;
                pbInfo.Maximum = (int)nudEncryptTimes.Value;
                pbInfo.Value = 0;
                pbInfo.Visible = true;
                labLayerDecrypted.Visible = false;
                bw.RunWorkerAsync();
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                _cbRandomAlgorithmChecked = cbRandomAlgorithm.Checked;
                _cbEncryptionAlgorithmSelectedIndex = cbEncryptionAlgorithm.SelectedIndex;
                _rtbSourceText = rtbSource.Text;
                _tbPasswordText = tbPassword.Text;
                _cbAutoDecryptChecked_temp = _cbAutoDecryptChecked = cbAutoDecrypt.Checked;
                workerType = WorkerType.Decryption;
                pbInfo.Value = 0;
                pbInfo.Visible = false;
                labLayerDecrypted.Text = labLayerDecryptedString + 0;
                labLayerDecrypted.Visible = true;
                bw.RunWorkerAsync();
            }
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
            cbAutoDecrypt.Checked = false;
            cbRandomAlgorithm.Checked = false;
            labDragDropHints.Text = labDragDropHintsString;
            cbEncryptionAlgorithm.SelectedItem = null;
            cbEncryptionAlgorithm.Text = "--select--";
            nudEncryptTimes.Value = 1;
            tbPassword.Text = firstMacAddress;
            pbInfo.Value = 0;
            pbInfo.Visible = false;
            labLayerDecrypted.Visible = false;
            labLayerDecrypted.Text = labLayerDecryptedString;
            if (bw != null || bw.IsBusy)
            {
                bw.CancelAsync();
            }
        }

        private void rtbSource_TextChanged(object sender, EventArgs e)
        {
            labDragDropHints.Text = labDragDropHintsString;
            importPath = null;
        }

        private void cbRandom_CheckedChanged(object sender, EventArgs e)
        {
            cbEncryptionAlgorithm.Enabled = !cbRandomAlgorithm.Checked;
            if (!cbRandomAlgorithm.Checked)
            {
                cbAutoDecrypt.Checked = false;
            }
        }

        private void CbAutoDecrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoDecrypt.Checked)
            {
                cbRandomAlgorithm.Checked = true;
            }
        }

        private string Encrypt()
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
                    secretMessage = Encoding.UTF8.GetBytes(_rtbSourceText);
                }
                byte[] result = null;
                int encryptionAlgorithm = (_cbRandomAlgorithmChecked) ? ((new Random().NextDouble() >= 0.5) ? 0 : 1) : _cbEncryptionAlgorithmSelectedIndex;
                switch (encryptionAlgorithm)
                {
                    case 0:
                        result = Encryption.AESGCM.SimpleEncryptWithPassword(secretMessage, _tbPasswordText);
                        break;
                    case 1:
                        result = Encryption.AESThenHMAC.SimpleEncryptWithPassword(secretMessage, _tbPasswordText);
                        break;
                    default:
                        throw new Exception("Please select an encryption algorithm!");
                }
                if (isBytesData)
                {
                    ByteArrayToFile(importPath, result);
                    return null;
                }
                else
                {
                    return (result == null) ? "" : Convert.ToBase64String(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string Decrypt()
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
                    secretMessage = Convert.FromBase64String(_rtbSourceText);
                }
                byte[] result = null;

                if (!_cbAutoDecryptChecked)
                {
                    int decryptionAlgorithm = (_cbRandomAlgorithmChecked) ? ((new Random().NextDouble() >= 0.5) ? 0 : 1) : _cbEncryptionAlgorithmSelectedIndex;
                    switch (decryptionAlgorithm)
                    {
                        case 0:
                            result = Encryption.AESGCM.SimpleDecryptWithPassword(secretMessage, _tbPasswordText);
                            break;
                        case 1:
                            result = Encryption.AESThenHMAC.SimpleDecryptWithPassword(secretMessage, _tbPasswordText);
                            break;
                        default:
                            throw new Exception("Please select an encryption algorithm!");
                    }
                }
                else
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                result = Encryption.AESGCM.SimpleDecryptWithPassword(secretMessage, _tbPasswordText);
                                break;
                            case 1:
                                result = Encryption.AESThenHMAC.SimpleDecryptWithPassword(secretMessage, _tbPasswordText);
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
                if (_cbAutoDecryptChecked)
                {
                    if (result != null)
                    {
                        if (isBytesData)
                        {
                            ByteArrayToFile(importPath, result);
                            return FileSavedMarker;
                        }
                        else
                        {
                            return (result == null) ? "" : Encoding.UTF8.GetString(result);
                        }
                    }
                    else
                    {
                        _cbAutoDecryptChecked = false;
                        return null;
                    }
                }
                else
                {
                    if (isBytesData)
                    {
                        ByteArrayToFile(importPath, result);
                        return FileSavedMarker;
                    }
                    else
                    {
                        return (result == null) ? "" : Encoding.UTF8.GetString(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //背景執行
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (workerType == WorkerType.Encryption)
                {
                    finalResult = "";
                    for (int i = 0; i < nudEncryptTimes.Value; i++)
                    {
                        if ((bw.CancellationPending == true))
                        {
                            e.Cancel = true;
                            break;
                        }
                        else
                        {
                            finalResult = Encrypt();
                            _rtbSourceText = finalResult;
                            bw.ReportProgress(i + 1);
                        }
                    }
                }
                else if (workerType == WorkerType.Decryption)
                {
                    finalResult = "";
                    int i = 0;
                    do
                    {
                        try
                        {
                            finalResult = Decrypt();
                            _rtbSourceText = finalResult;
                            if (finalResult != null || finalResult == FileSavedMarker)
                            {
                                bw.ReportProgress(++i);
                            }
                        }
                        catch (FormatException)
                        {
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    } while (_cbAutoDecryptChecked);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                e.Cancel = true;
                return;
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progressPercentage = e.ProgressPercentage;
            if (workerType == WorkerType.Encryption)
            {
                pbInfo.Value = progressPercentage;
            }
            else if (workerType == WorkerType.Decryption)
            {
                labLayerDecrypted.Text = labLayerDecryptedString + progressPercentage;
            }
        }

        //執行完成
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((e.Cancelled == true))
            {
                //MessageBox.Show("取消!");
            }
            else if (!(e.Error == null))
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }
            else
            {
                if (finalResult != FileSavedMarker)
                {
                    rtbSource.Text = finalResult;
                }
            }
            if (workerType == WorkerType.Decryption)
            {
                cbAutoDecrypt.Checked = _cbAutoDecryptChecked_temp;
            }
        }
    }
}
