using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DataEncryption
{
    public partial class frmEncryption : Form
    {
        readonly string labDragDropHintsString = "Drag file here to import";
        readonly string labLayerDecryptedString = "Layer decrypted: ";
        readonly string FileSavedMarker = "*fileSavedMarker--@$";

        string[] importPath;
        string firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
        string finalResult;
        ArrayList importFiles = new ArrayList();
        BackgroundWorker bw;
        WorkerType workerType;

        string _rtbSourceText;
        string _tbPasswordText;
        bool _cbAutoDecryptChecked_temp;
        bool _cbAutoDecryptChecked;
        bool _cbRandomAlgorithmChecked;
        int _cbEncryptionAlgorithmSelectedIndex;
        int _nudEncryptTimesValue;

        enum WorkerType
        {
            CountingFiles,
            Encryption,
            Decryption,
            ASCII
        }

        class ReportStates
        {
            public WorkerType workerType { get; set; }
            public int total { get; set; }
            public int current { get; set; }
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
            catch (Exception)
            {
                return false;
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                workerType = WorkerType.Encryption;
                rtbSource.Enabled = false;
                tbPassword.Enabled = false;
                nudEncryptTimes.Enabled = false;
                cbAutoDecrypt.Enabled = false;
                cbRandomAlgorithm.Enabled = false;
                labDragDropHints.AllowDrop = false;
                _cbRandomAlgorithmChecked = cbRandomAlgorithm.Checked;
                _cbEncryptionAlgorithmSelectedIndex = cbEncryptionAlgorithm.SelectedIndex;
                _rtbSourceText = rtbSource.Text;
                _tbPasswordText = tbPassword.Text;
                _nudEncryptTimesValue = (int)nudEncryptTimes.Value;
                pbInfoTotal.Maximum = (int)nudEncryptTimes.Value;
                pbInfoTotal.Value = 0;
                pbInfoTotal.Enabled = true;
                pbInfoSession.Value = 0;
                pbInfoSession.Enabled = true;
                labLayerDecrypted.Visible = false;
                btnStop.Enabled = true;
                bw.RunWorkerAsync();
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy != true)
            {
                workerType = WorkerType.Decryption;
                rtbSource.Enabled = false;
                tbPassword.Enabled = false;
                nudEncryptTimes.Enabled = false;
                cbAutoDecrypt.Enabled = false;
                cbRandomAlgorithm.Enabled = false;
                labDragDropHints.AllowDrop = false;
                _cbRandomAlgorithmChecked = cbRandomAlgorithm.Checked;
                _cbEncryptionAlgorithmSelectedIndex = cbEncryptionAlgorithm.SelectedIndex;
                _rtbSourceText = rtbSource.Text;
                _tbPasswordText = tbPassword.Text;
                _cbAutoDecryptChecked_temp = _cbAutoDecryptChecked = cbAutoDecrypt.Checked;
                _nudEncryptTimesValue = 1;
                pbInfoTotal.Value = 0;
                pbInfoTotal.Enabled = true;
                pbInfoSession.Value = 0;
                pbInfoSession.Enabled = false;
                labLayerDecrypted.Text = labLayerDecryptedString + 0;
                labLayerDecrypted.Visible = true;
                btnStop.Enabled = true;
                bw.RunWorkerAsync();
            }
        }

        private void btnAscii_Click(object sender, EventArgs e)
        {

            if (bw.IsBusy != true)
            {
                workerType = WorkerType.ASCII;
                rtbSource.Enabled = false;
                tbPassword.Enabled = false;
                nudEncryptTimes.Enabled = false;
                cbAutoDecrypt.Enabled = false;
                cbRandomAlgorithm.Enabled = false;
                labDragDropHints.AllowDrop = false;
                _cbRandomAlgorithmChecked = cbRandomAlgorithm.Checked;
                _cbEncryptionAlgorithmSelectedIndex = cbEncryptionAlgorithm.SelectedIndex;
                _rtbSourceText = rtbSource.Text;
                _tbPasswordText = tbPassword.Text;
                _nudEncryptTimesValue = (int)nudEncryptTimes.Value;
                pbInfoTotal.Maximum = (int)nudEncryptTimes.Value;
                pbInfoTotal.Value = 0;
                pbInfoTotal.Enabled = true;
                pbInfoSession.Value = 0;
                pbInfoSession.Enabled = true;
                labLayerDecrypted.Visible = false;
                btnStop.Enabled = true;
                bw.RunWorkerAsync();
            }
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy)
            {
                bw.CancelAsync();
            }
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
            pbInfoTotal.Value = 0;
            pbInfoTotal.Enabled = false;
            pbInfoSession.Value = 0;
            pbInfoSession.Enabled = false;
            labLayerDecrypted.Visible = false;
            labLayerDecrypted.Text = labLayerDecryptedString;
            btnStop.Enabled = false;
            if (bw != null || bw.IsBusy)
            {
                bw.CancelAsync();
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
        }

        private void labDragDropHints_DragDrop(object sender, DragEventArgs e)
        {
            rtbSource.Text = null;
            importFiles.Clear();
            importPath = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (importPath == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            updateDrapInfoToLabel();
        }

        private void updateDrapInfoToLabel()
        {
            labDragDropHints.Text = labDragDropHintsString;
            if (importPath != null)
            {
                for (int i = 0; i < importPath.Length; i++)
                {
                    labDragDropHints.Text += "\n" + importPath[i];
                }
            }
        }

        private void labDragDropHints_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
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

        private string Encrypt(string filePath)
        {
            try
            {
                bool isBytesData = false;
                byte[] secretMessage = null;
                if (filePath != null)
                {
                    try
                    {
                        if (filePath.Length > 260)
                        {

                        }
                        using (FileStream fs = File.Open(Path.GetFullPath(filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
                    ByteArrayToFile(filePath, result);
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

        private string Decrypt(string filePath)
        {
            try
            {
                bool isBytesData = false;
                byte[] secretMessage = null;
                if (filePath != null)
                {
                    try
                    {
                        using (FileStream fs = File.Open(Path.GetFullPath(filePath), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
                            ByteArrayToFile(filePath, result);
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
                        ByteArrayToFile(filePath, result);
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

        private void HandleData(string[] path, DoWorkEventArgs e)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if ((bw.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                Thread.Sleep(20);
                string filePath = path[i];
                bool isDirectory = false;
                try
                {
                    FileAttributes attr = File.GetAttributes(filePath);
                    if (attr.HasFlag(FileAttributes.Directory))
                    {
                        isDirectory = true;
                    }
                }
                catch (Exception) { }

                if (isDirectory)
                {
                    DirectoryInfo d = new DirectoryInfo(filePath);
                    FileInfo[] Files = d.GetFiles();
                    for (int j = 0; j < Files.Length; j++)
                    {
                        HandleData(new string[] { Files[j].FullName }, e);
                    }
                    DirectoryInfo[] directories = d.GetDirectories();
                    for (int j = 0; j < directories.Length; j++)
                    {
                        HandleData(new string[] { directories[j].FullName }, e);
                    }
                }
                else
                {
                    if (workerType == WorkerType.Encryption)
                    {
                        finalResult = "";
                        for (int j = 0; j < _nudEncryptTimesValue; j++)
                        {
                            if ((bw.CancellationPending == true))
                            {
                                e.Cancel = true;
                                break;
                            }
                            bw.ReportProgress(0, new ReportStates
                            {
                                workerType = WorkerType.Encryption,
                                total = _nudEncryptTimesValue,
                                current = j
                            });
                            finalResult = Encrypt(filePath);
                            _rtbSourceText = finalResult;
                            bw.ReportProgress(1, new ReportStates
                            {
                                workerType = WorkerType.Encryption,
                                total = _nudEncryptTimesValue,
                                current = j + 1
                            });
                        }
                    }
                    else if (workerType == WorkerType.Decryption)
                    {
                        finalResult = "";
                        do
                        {
                            try
                            {
                                if ((bw.CancellationPending == true))
                                {
                                    e.Cancel = true;
                                    break;
                                }
                                bw.ReportProgress(0, new ReportStates { workerType = WorkerType.Decryption });
                                finalResult = Decrypt(filePath);
                                _rtbSourceText = finalResult;
                                if (finalResult != null || finalResult == FileSavedMarker)
                                {
                                    bw.ReportProgress(1, new ReportStates { workerType = WorkerType.Decryption });
                                }
                            }
                            catch (FormatException)
                            {
                                return;
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        } while (_cbAutoDecryptChecked);
                    }
                }
            }
        }

        //背景執行
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (workerType == WorkerType.ASCII)
            {
                string source = _rtbSourceText;
                string se = "";
                string sd = "";
                int j;
                Random random = new Random(-1);
                _nudEncryptTimesValue = 2;
                bw.ReportProgress(source.Length, new ReportStates { workerType = WorkerType.CountingFiles });

                int v = char.MaxValue;

                j = 0;
                foreach (char c in source)
                {
                    if (new Random().NextDouble() > 0.5)
                    {
                        Thread.Sleep(1);
                    }
                    if ((bw.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    int x = Convert.ToInt32(c);
                    int n = (int)Math.Floor(random.NextDouble() * 100 * v);
                    int i = (x + n) % v;
                    se += Convert.ToChar(i);
                    bw.ReportProgress(1, new ReportStates
                    {
                        workerType = WorkerType.ASCII,
                        total = source.Length,
                        current = ++j
                    });
                }
                random = new Random(-1);
                j = 0;
                foreach (char c in se)
                {
                    if (new Random().NextDouble() > 0.5)
                    {
                        Thread.Sleep(1);
                    }
                    if ((bw.CancellationPending == true))
                    {
                        e.Cancel = true;
                        break;
                    }
                    int x = Convert.ToInt32(c);
                    int n = (int)Math.Floor(random.NextDouble() * 100 * v);
                    int i = (x - n) % v;
                    if (i < 0)
                    {
                        i += v;
                    }
                    sd += Convert.ToChar(i);
                    bw.ReportProgress(1, new ReportStates
                    {
                        workerType = WorkerType.ASCII,
                        total = source.Length,
                        current = ++j
                    });
                }
                finalResult = se + "\n" + sd;
                return;
            }
            try
            {
                //Count files
                int filesCount = 0;
                if (importPath != null)
                {
                    for (int i = 0; i < importPath.Length; i++)
                    {
                        string filePath = importPath[i];
                        try
                        {
                            FileAttributes attr = File.GetAttributes(filePath);
                            if (attr.HasFlag(FileAttributes.Directory))
                            {
                                filesCount += Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories).Length;
                            }
                            else
                            {
                                filesCount++;
                            }
                            bw.ReportProgress(filesCount, new ReportStates { workerType = WorkerType.CountingFiles });
                        }
                        catch (Exception) { }
                    }
                    // Encrypt or decrypt the files
                    HandleData(importPath, e);
                }
                else
                {
                    // Encrypt or decrypt the files
                    HandleData(new string[1] { _rtbSourceText }, e);
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
            ReportStates reportStates = (ReportStates)e.UserState;
            WorkerType workerType = reportStates.workerType;
            int progressPercentage = e.ProgressPercentage;
            switch (workerType)
            {
                case WorkerType.CountingFiles:
                    pbInfoSession.Maximum = _nudEncryptTimesValue;
                    pbInfoTotal.Maximum = progressPercentage * _nudEncryptTimesValue;
                    labDragDropHints.Text = "Counting files: " + progressPercentage + " files found.";
                    break;
                case WorkerType.Encryption:
                    pbInfoSession.Maximum = reportStates.total;
                    pbInfoSession.Value = reportStates.current;
                    pbInfoTotal.Value += progressPercentage;
                    labDragDropHints.Text = pbInfoTotal.Value + " files Encrypted...";
                    break;
                case WorkerType.Decryption:
                    pbInfoTotal.Value += progressPercentage;
                    labLayerDecrypted.Text = labLayerDecryptedString + pbInfoTotal.Value;
                    break;
                case WorkerType.ASCII:
                    pbInfoSession.Maximum = reportStates.total;
                    pbInfoSession.Value = reportStates.current;
                    pbInfoTotal.Value += progressPercentage;
                    double d1 = ((double)pbInfoTotal.Value / pbInfoTotal.Maximum);
                    int pec = (int)Math.Floor(d1 * 100);
                    labDragDropHints.Text = pec + "% finished...";
                    break;
                default:
                    break;
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
            updateDrapInfoToLabel();
            labDragDropHints.AllowDrop = true;
            rtbSource.Enabled = true;
            tbPassword.Enabled = true;
            nudEncryptTimes.Enabled = true;
            cbAutoDecrypt.Enabled = true;
            cbRandomAlgorithm.Enabled = true;
            btnStop.Enabled = false;
        }
    }
}
