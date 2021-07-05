using DeponaHR1.FileHelper;
using DeponaHR1.Zip;
using Squirrel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeponaHR1
{
    public partial class Form1 : Form
    {
        private TreeNode _selectedNode, _parentNode;
        private string _newFolderName;
        private bool _formConfigured = false;
        private Dictionary<string, string> _dictMapp;
        private Dictionary<string, string> _dictProcess;

        public Form1()
        {
            InitializeComponent();

            //this.Text = "DeponaHR1 " + this.Tag.ToString();   DEPRICATED! Is managed by System Diagnostics
            txtSource.TextChanged += new EventHandler(Text_Changed);
            txtDone.TextChanged += new EventHandler(Text_Changed);
            txtLog.TextChanged += new EventHandler(Text_Changed);
            txtTemp.TextChanged += new EventHandler(Text_Changed);
            btnPlus.Click += new EventHandler(Button_AddMinusClicked);
            btnMinus.Click += new EventHandler(Button_AddMinusClicked);

            AddVersionNumber();
            CheckForUpdates();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        }

        private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            // todo: implement version handling
            this.Text += $" v. {versionInfo.FileVersion}";
        }

        private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager(@"G:\Lit\MLT_Förvaltning\DeponaHR\Releases"))
            {
                await manager.UpdateApp();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadConfiguration();
            initializeTreeView();
            showMappConfiguration();
            _formConfigured = true;
        }

        private void loadConfiguration()
        {
            _dictMapp = DeponaConfig.Configuration.GetMappSettingsDictionary();
            _dictProcess = DeponaConfig.Configuration.GetProcessSettingsDictionay();
        }

        private void showMappConfiguration()
        {
            txtDestination.Text = _dictMapp["Destination"];
            txtSource.Text = _dictMapp["Source"];
            txtDone.Text = _dictMapp["Done"];
            txtLog.Text = _dictMapp["Log"];
            txtTemp.Text = _dictMapp["Temp"];

            int batchNo = int.Parse(_dictProcess["BatchNo"]);
            txtBatchNo.Text = _dictProcess["DeponaFakt"] + batchNo.ToString("000");
        }

        private void initializeTreeView()
        {
            string rootDir = _dictProcess["rootDir"];

            treeView1.Nodes.Clear();

            treeView1.Nodes.AddRange(getFolders(rootDir, chkTreeViewExpanded.Checked).ToArray());
        }

        List<TreeNode> getFolders(string dir, bool expanded)
        {
            var dirs = Directory.GetDirectories(dir).ToArray();
            var nodes = new List<TreeNode>();
            foreach (string d in dirs)
            {
                DirectoryInfo di = new DirectoryInfo(d);
                TreeNode tn = new TreeNode(di.Name);
                tn.Tag = di;
                int subCount = 0;
                try
                {
                    subCount = Directory.GetDirectories(d).Count();
                }
                catch { /* ignore accessdenied */ }

                if (subCount > 0)
                {
                    var subNodes = getFolders(di.FullName, expanded);
                    tn.Nodes.AddRange(subNodes.ToArray());
                }
                if (expanded)
                    tn.Expand();

                nodes.Add(tn);
            }
            return nodes;
        }

        private void getDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;

            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name);

                subSubDirs = subDir.GetDirectories();

                if (subSubDirs.Length != 0)
                {
                    getDirectories(subSubDirs, aNode);
                }
            }
            treeView1.Nodes.Add(nodeToAddTo);
        }

        private void chkTreeViewExpanded_CheckedChanged(object sender, EventArgs e)
        {
            initializeTreeView();
        }


        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string nodeName = Path.GetFileName(e.Node.Tag.ToString());
            btnMyChoice.Text = nodeName;
            btnMyChoice.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            btnMyChoice.ForeColor = Color.Blue;

            _selectedNode = new TreeNode(e.Node.FullPath);
            _selectedNode.Tag = e.Node.Tag;

            string button = e.Button.ToString();
            if (button == "Right")
            {
                ShowFolderControls(true);
                txtNewFolder.Focus();
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            string selectedNode = _selectedNode.Tag.ToString();
            string newFolderName;

            if (txtNewFolder.TextLength > 0)
            {
                newFolderName = Path.Combine("c:\\temp", selectedNode, _newFolderName);

                if (!Directory.Exists(newFolderName))
                {
                    Directory.CreateDirectory(Path.Combine("c:\\temp", selectedNode, _newFolderName));

                    TreeNode aNode = new TreeNode(txtNewFolder.Text);
                    _selectedNode.Nodes.Add(aNode);
                    initializeTreeView();
                    ShowFolderControls(false);

                    updateFolderConfiguration(newFolderName);
                }
            }
        }

        private void updateFolderConfiguration(string folderName)
        {
            if (rbDestination.Checked)
            {
                txtDestination.Text = folderName;
            }
            else if(rbSource.Checked)
            {
                txtSource.Text = folderName;
            }
            else if(rbDone.Checked)
            {
                txtDone.Text = folderName;
            }
            else if (rbLog.Checked)
            {
                txtLog.Text = folderName;
            }
            else
            {
                txtTemp.Text = folderName;
            }
        }

        private void txtNewFolder_Leave(object sender, EventArgs e)
        {
            _newFolderName = txtNewFolder.Text;
        }

        private void ShowFolderControls(bool show)
        {
            txtNewFolder.Text = String.Empty;
            txtNewFolder.Visible = show;
            btnAddFolder.Visible = show;
            btnRemoveFolder.Visible = show;
        }

        private void btnRemoveFolder_Click(object sender, EventArgs e)
        {
            if (_selectedNode != null)
            {
                string selectedNode = Path.Combine("c:\\temp", _selectedNode.Tag.ToString());
                string fileName = Path.GetFileName(selectedNode);
                txtNewFolder.Text = fileName;

                string message = $"Radera folder \"{fileName}\"?\nAlla underliggande foldrar + filer kommer att raderas!!";
                DialogResult answer = MessageBox.Show(message, _selectedNode.Tag.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (answer.Equals(DialogResult.Yes))
                {
                    Directory.Delete(selectedNode, true);
                    treeView1.Nodes.Remove(_selectedNode);
                    updateFolderConfiguration(_parentNode.Tag.ToString());
                    initializeTreeView();
                }
                ShowFolderControls(false);
            }
        }

        private void Text_Changed(object sender, EventArgs e) 
        {
            if (_formConfigured)
            {
                //_dictMapp["Destination"] = txtDestination.Text;
                _dictMapp["Source"] = txtSource.Text;
                _dictMapp["Done"] = txtDone.Text;
                _dictMapp["Log"] = txtLog.Text;
                _dictMapp["Temp"] = txtTemp.Text;
                SaveConfiguraitons();

                if(!VerifyMapConfiguration())
                {
                    Font f = new Font("Segoe UI", 9, FontStyle.Regular);
                    txtDestination.Font = f;
                    txtSource.Font = f;
                    txtDone.Font = f;
                    txtLog.Font = f;
                    txtTemp.Font = f;
                }
            }
        }

        private bool VerifyMapConfiguration()
        {
            Font f = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            bool pathVerification = true;

            // Do NOT verify txtDestination!

            if (!Directory.Exists(txtSource.Text)) 
            {
                txtSource.Text = "Re-enter valid path!";
                txtSource.Font = f;
                pathVerification = false;
            }
            
            if (!Directory.Exists(txtDone.Text))
            {
                txtDone.Text = "Re-enter valid path!";
                txtDone.Font = f;
                pathVerification = false;
            }

            if (!Directory.Exists(txtLog.Text))
            {
                txtLog.Text = "Re-enter valid path!";
                txtLog.Font = f;
                pathVerification = false;
            }

            if (!Directory.Exists(txtTemp.Text))
            {
                txtTemp.Text = "Re-enter valid path!";
                txtTemp.Font = f;
                pathVerification = false;
            }
            return pathVerification;
        }

        private void btnMyChoice_Click(object sender, EventArgs e)
        {
            if(_selectedNode != null)
            {
                updateFolderConfiguration(_selectedNode.Tag.ToString());
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CountPDFFilesInKallaDir();
        }

        private int CountPDFFilesInKallaDir()
        {
            if (VerifyMapConfiguration())
            {
                var filecount = (from file in Directory.EnumerateFiles(_dictMapp["Source"], "*.pdf", SearchOption.TopDirectoryOnly)
                                    select file).Count();

                lblFilesInSource.Text = lblFilesInSource.Tag.ToString();
                lblFilesInSource.Text += filecount;

                DeponaConfig.Configuration.SetProcessControlFlowInstance("NumPDFFilesInSourceDir", filecount);
                return filecount;
            }
            else
            {
                return -2;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (this.btnStartBatch.Enabled == true)
            {
                if (!VerifyMapConfiguration())
                {
                    string message = "Do you REALLY want to close this form? - Configuration is not syncronized - your configuration will not be saved!";
                    string caption = "Filesystem and configuration settings are out of sync - due to removing of folders!";
                    DialogResult dr = MessageBox.Show(message, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    if (dr.Equals(DialogResult.Yes))
                    {
                        //SaveConfiguraitons();     //moved to Form Closing Event
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                // Configuration will be saved in Closing Event
                this.Close();
            }
        }

        private void SaveConfiguraitons()
        {
            DeponaConfig.Configuration.SaveMappSettingsDictionary(_dictMapp);
            DeponaConfig.Configuration.SaveProcessSettingsDictionary(_dictProcess);
        }

        private void btnStartBatch_Click(object sender, EventArgs e)
        {
            if (CountPDFFilesInKallaDir() == 0)
            {
                MessageBox.Show("'Source' map is empty!", "Source empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // FileParameterContextVerification
            var fileNameImport = new Batch.FileNameImport(DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
            List<string> fileNamesCollectionDAT = new List<string>(fileNameImport.getFileNamesCollectionDAT());
            List<string> fileNamesCollectionPDF = new List<string>(fileNameImport.getFileNamesCollectionPDF());

            var fileParameterContextVerification = new Batch.FileParameterContentVerification(fileNamesCollectionPDF, fileNamesCollectionDAT);
            if (!fileParameterContextVerification.verifyContents())
            {
                String caption = "FileParameter Verification Error";
                String message = "One (or more) of the following conditions have been violated:\n";
                message += "1. Only 1 '.dat' file must be present in the delivery package.\n";
                message += "2. The amount of parameter indices in the '.dat' file must be equal to the amount of delivered '.pdf' files in the delivery package.";

                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool allFileNamesOk = fileParameterContextVerification.verifyDATattributeCorrespondsForPDFFileName();
            if (!allFileNamesOk)
            {
                string caption = "Inconsistency found in delivery metadata!";
                string message = "Non applicable reference(s) to .pdf filename(s) found in .dat file.\n";
                message += "You must abort this process! Contact delivery advisor.";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Batch start
            btnClose.Enabled = false;
            grpBatchParams.Enabled = false;
            grpTreeView.Enabled = false;
            this.btnStartBatch.Enabled = false;
            Directory.SetCurrentDirectory(_dictMapp["Source"]);

            // CreateProcessInformation
            _dictProcess["SystemVersion"] = this.Tag.ToString();
            DeponaConfig.Configuration.SaveProcessSettingsDictionary(_dictProcess);

            BatchStart bs = new BatchStart(_dictMapp, _dictProcess);
            var a = bs.StartPocessAsync();

            DeponaConfig.Configuration.SetProcessControlFlowInstance("BatchInProgress", 1);         // batch in Progress = 1, Zip in progress = 2
            progressBar1.Minimum = 0;
            progressBar1.Maximum = DeponaConfig.Configuration.GetProcessControlFlowInstance("NumPDFFilesInSourceDir");

            progressBarCopyDelete.Minimum = 0;
            progressBarCopyDelete.Maximum = DeponaConfig.Configuration.GetProcessControlFlowInstance("NumPDFFilesInSourceDir") * 3; // was * 4

            timer1.Start();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            // for development usage only
            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> item in _dictProcess)
            {
                sb.Append(item.Key);
                sb.Append(" " + item.Value + "\n");
            }
            MessageBox.Show("Dictionary: \n" + sb.ToString());
        }


        private void Button_AddMinusClicked(object sender, EventArgs e)
        {
            int batchNo = int.Parse(_dictProcess["BatchNo"]);
            Button button = sender as Button;

            if (button.Tag.Equals("Plus"))
            {
                if(batchNo < 999)
                {
                    batchNo++;
                    _dictProcess["BatchNo"] = batchNo.ToString("000");
                }
                else
                {
                    batchNo = 1;
                    _dictProcess["BatchNo"] = batchNo.ToString("000");
                }
            }
            else
            {
                if (batchNo > 1)
                {
                    batchNo--;
                    _dictProcess["BatchNo"] = batchNo.ToString("000");
                }
            }
            txtBatchNo.Text = _dictProcess["DeponaFakt"] + batchNo.ToString("000");
        }

        private async void cmdUnpack_Click(object sender, EventArgs e)
        {
            bool errorState = false;

            if(!Directory.Exists(_dictMapp["Temp"]))
            {
                return;
            }

            if(!Directory.Exists(_dictMapp["Source"]))
            {
                return;
            }

            var filecountDat = (from file in Directory.EnumerateFiles(_dictMapp["Temp"], "*.dat", SearchOption.TopDirectoryOnly)
                             select file).Count();

            var filecountZip = (from file in Directory.EnumerateFiles(_dictMapp["Temp"], "*.zip", SearchOption.TopDirectoryOnly)
                                select file).Count();

            var filecountSourcemap = (from file in Directory.EnumerateFiles(_dictMapp["Source"], "*.*", SearchOption.TopDirectoryOnly)
                                select file).Count();

            var filecountTempmap = (from file in Directory.EnumerateFiles(_dictMapp["Temp"], "*.*", SearchOption.TopDirectoryOnly)
                                   select file).Count();

            Cursor.Current = Cursors.WaitCursor;

            StringBuilder sb = new StringBuilder();
            
            if(filecountZip > 1)
            {
                sb.AppendLine("FEL: flera zip filer! (max = 1)");
                errorState = true;
            }

            if(filecountDat > 0 && filecountZip > 0)
            {
                sb.AppendLine("FEL: mix av .dat + .zip filer! (antingen .dat / eller .zip)");
                errorState = true;
            }

            if(filecountSourcemap > 0)
            {
                sb.AppendLine("FEL: Source mappen är inte tom! (måste vara tom)");
                errorState = true;
            }

            if(filecountTempmap == 0)
            {
                sb.AppendLine("FEL: Tempmappen är tom.");
                errorState = true;
            }

            if (errorState)
            {
                MessageBox.Show(sb.ToString(), "Fel hittade!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!errorState)
            {
                if(filecountZip == 1)
                {
                    var myZip = new ZipUnpacker(DeponaConfig.Configuration.GetMappSettingsInstance("Temp"),
                                                DeponaConfig.Configuration.GetMappSettingsInstance("Source"));

                    string[] zipfileName = Directory.GetFiles(_dictMapp["Temp"], "*.zip");
                    var fileInfo = new FileInfo(zipfileName[0]);

                    long byteSize = fileInfo.Length;
                    float compLevel = myZip.GetZipCompression(zipfileName[0]);
                    long totBytes = byteSize * (long)(compLevel);
                    totBytes = totBytes / 120000;

                    int zipCount = myZip.GetZipFileCount(zipfileName[0]);
                    progressBarZip.Maximum = zipCount + (int)totBytes;

                    grpFooter.Enabled = false;
                    grpUnzip.Visible = true;
                    timerZip.Start();

                    /*
                    await Task.Run(() =>
                    {
                       var t = startUnzipAsync();
                    });
                    */
                    await Task.Run(StartUnzipAsync);

                    timerZip.Stop();
                    grpUnzip.Enabled = false;
                    
                    grpFooter.Enabled = true;
                    grpUnzip.Visible = false;
                    this.Show();
                    this.Focus();

                    string caption = "Zip archive extraction";
                    MessageBox.Show("Zip archive unpacked and files moved to 'Source'", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(filecountDat > 0)
                {
                    // copy all .dat files to 'Source' folder
                    var copyfiles = new CopyFiles(DeponaConfig.Configuration.GetMappSettingsInstance("Temp"),
                                                    DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
                    int res = copyfiles.copyFiles(DeponaConfig.Configuration.GetMappSettingsInstance("Temp"),
                                                         DeponaConfig.Configuration.GetMappSettingsInstance("Source"));
                    if(res == -2)
                    {
                        MessageBox.Show("Error: 'Source folder is NOT empty!", "Copy error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    // delete all files from 'Temp' folder
                    var files = Directory.EnumerateFiles(DeponaConfig.Configuration.GetMappSettingsInstance("Temp"));

                    int deleteCount = 0;
                    foreach (var fileitem in files)
                    {
                        File.Delete(fileitem);
                        deleteCount++;
                    }
                    DeponaConfig.Configuration.WriteLogMessage($"Deleted files in 'Temp': {deleteCount}");
                    string message = "Copy / move success!";
                    MessageBox.Show($"{deleteCount} files (.dat + .pdf) moved to 'Source'", message, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private async Task<int> StartUnzipAsync()
        {
            var myZip = new ZipUnpacker(DeponaConfig.Configuration.GetMappSettingsInstance("Temp"),
                                        DeponaConfig.Configuration.GetMappSettingsInstance("Source"));

            string[] zipfileName = Directory.GetFiles(_dictMapp["Temp"], "*.zip");
            var t = await myZip.UnpackZipAndCopyToSource(zipfileName[0]);
        
            return 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblProcessedFiles.Text = DeponaConfig.Configuration.GetProcessControlFlowInstance("NumProcessedFiles").ToString();
            progressBar1.Value = DeponaConfig.Configuration.GetProcessControlFlowInstance("NumProcessedFiles");

            try
            {
                progressBarCopyDelete.Value = DeponaConfig.Configuration.GetProcessControlFlowInstance("FileManipCount");
            }
            catch(System.ArgumentOutOfRangeException)
            {
                progressBarCopyDelete.Value = progressBarCopyDelete.Maximum;
            }

            if(DeponaConfig.Configuration.GetProcessControlFlowInstance("BatchInProgress") <= 0)
            {
                timer1.Stop();
                string caption = "Process is finished!";
                progressBarCopyDelete.Value = progressBarCopyDelete.Maximum;
                MessageBox.Show($"Klar!\nInlästa filer (.pdf): {DeponaConfig.Configuration.GetProcessControlFlowInstance("NumProcessedFiles")}", caption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnClose.Enabled = true;
            }
        }

        private void timerZip_Tick(object sender, EventArgs e)
        {
            try
            {
                progressBarZip.Value = DeponaConfig.Configuration.GetProcessControlFlowInstance("ZipExtractCount");
            }
            catch (System.ArgumentOutOfRangeException err)
            {
                string errMessage = $"ERROR: timerZip\n";
                errMessage += $"e.Message = {err.Message}\n";
                errMessage += $"e.ActualValue = {err.ActualValue}\n";
                DeponaConfig.Configuration.WriteLogMessage(errMessage);
                timerZip.Stop();
            }
        }

        private void txtDestination_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to change the Destination string?", "Change Destination", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (dr.Equals(DialogResult.Yes))
            {
                txtDestination.ReadOnly = false;
                grpTreeView.Enabled = false;
                grpBatchParams.Enabled = false;

                btnDestinationOK.Visible = true;
            }
        }

        private void btnDestinationOK_Click(object sender, EventArgs e)
        {
            txtDestination.ReadOnly = true;
            btnDestinationOK.Visible = false;
            grpTreeView.Enabled = true;
            grpBatchParams.Enabled = true;
            _dictMapp["Destination"] = txtDestination.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfiguraitons();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                _parentNode = e.Node.Parent;
            }
        }
    }
}
