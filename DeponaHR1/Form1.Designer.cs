
namespace DeponaHR1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.chkTreeViewExpanded = new System.Windows.Forms.CheckBox();
            this.txtNewFolder = new System.Windows.Forms.TextBox();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnRemoveFolder = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.grpConfig = new System.Windows.Forms.GroupBox();
            this.btnDestinationOK = new System.Windows.Forms.Button();
            this.cmdUnpack = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.rbTemp = new System.Windows.Forms.RadioButton();
            this.txtTemp = new System.Windows.Forms.TextBox();
            this.rbLog = new System.Windows.Forms.RadioButton();
            this.rbDone = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDone = new System.Windows.Forms.TextBox();
            this.rbSource = new System.Windows.Forms.RadioButton();
            this.rbDestination = new System.Windows.Forms.RadioButton();
            this.btnFilesInKallaDir = new System.Windows.Forms.Button();
            this.btnMyChoice = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.grpFooter = new System.Windows.Forms.GroupBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnStartBatch = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpTreeView = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.btnPlus = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.grpBatchParams = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblFilesInSource = new System.Windows.Forms.Label();
            this.lblProcessedFiles = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBarCopyDelete = new System.Windows.Forms.ProgressBar();
            this.label8 = new System.Windows.Forms.Label();
            this.lblZipProgress = new System.Windows.Forms.Label();
            this.grpUnzip = new System.Windows.Forms.GroupBox();
            this.progressBarZip = new System.Windows.Forms.ProgressBar();
            this.timerZip = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.grpConfig.SuspendLayout();
            this.grpFooter.SuspendLayout();
            this.grpTreeView.SuspendLayout();
            this.grpBatchParams.SuspendLayout();
            this.grpUnzip.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(16, 48);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(358, 165);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // chkTreeViewExpanded
            // 
            this.chkTreeViewExpanded.AutoSize = true;
            this.chkTreeViewExpanded.Location = new System.Drawing.Point(16, 22);
            this.chkTreeViewExpanded.Name = "chkTreeViewExpanded";
            this.chkTreeViewExpanded.Size = new System.Drawing.Size(165, 19);
            this.chkTreeViewExpanded.TabIndex = 1;
            this.chkTreeViewExpanded.Text = "Expand treeview (c:\\temp)";
            this.chkTreeViewExpanded.UseVisualStyleBackColor = true;
            this.chkTreeViewExpanded.CheckedChanged += new System.EventHandler(this.chkTreeViewExpanded_CheckedChanged);
            // 
            // txtNewFolder
            // 
            this.txtNewFolder.Location = new System.Drawing.Point(18, 221);
            this.txtNewFolder.Name = "txtNewFolder";
            this.txtNewFolder.PlaceholderText = "new Foldername";
            this.txtNewFolder.Size = new System.Drawing.Size(156, 23);
            this.txtNewFolder.TabIndex = 2;
            this.txtNewFolder.Visible = false;
            this.txtNewFolder.Leave += new System.EventHandler(this.txtNewFolder_Leave);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.Location = new System.Drawing.Point(180, 220);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(75, 23);
            this.btnAddFolder.TabIndex = 3;
            this.btnAddFolder.Text = "Add Folder";
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Visible = false;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // btnRemoveFolder
            // 
            this.btnRemoveFolder.Location = new System.Drawing.Point(261, 221);
            this.btnRemoveFolder.Name = "btnRemoveFolder";
            this.btnRemoveFolder.Size = new System.Drawing.Size(99, 23);
            this.btnRemoveFolder.TabIndex = 4;
            this.btnRemoveFolder.Tag = "";
            this.btnRemoveFolder.Text = "Remove Folder";
            this.btnRemoveFolder.UseVisualStyleBackColor = true;
            this.btnRemoveFolder.Visible = false;
            this.btnRemoveFolder.Click += new System.EventHandler(this.btnRemoveFolder_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(27, 35);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.ReadOnly = true;
            this.txtDestination.Size = new System.Drawing.Size(366, 23);
            this.txtDestination.TabIndex = 5;
            this.txtDestination.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtDestination_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Destination";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Source";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(27, 122);
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(366, 23);
            this.txtSource.TabIndex = 7;
            // 
            // grpConfig
            // 
            this.grpConfig.Controls.Add(this.btnDestinationOK);
            this.grpConfig.Controls.Add(this.cmdUnpack);
            this.grpConfig.Controls.Add(this.label7);
            this.grpConfig.Controls.Add(this.rbTemp);
            this.grpConfig.Controls.Add(this.txtTemp);
            this.grpConfig.Controls.Add(this.rbLog);
            this.grpConfig.Controls.Add(this.rbDone);
            this.grpConfig.Controls.Add(this.label3);
            this.grpConfig.Controls.Add(this.txtLog);
            this.grpConfig.Controls.Add(this.label4);
            this.grpConfig.Controls.Add(this.txtDone);
            this.grpConfig.Controls.Add(this.rbSource);
            this.grpConfig.Controls.Add(this.rbDestination);
            this.grpConfig.Controls.Add(this.label2);
            this.grpConfig.Controls.Add(this.txtSource);
            this.grpConfig.Controls.Add(this.label1);
            this.grpConfig.Controls.Add(this.txtDestination);
            this.grpConfig.Location = new System.Drawing.Point(393, 40);
            this.grpConfig.Name = "grpConfig";
            this.grpConfig.Size = new System.Drawing.Size(399, 243);
            this.grpConfig.TabIndex = 9;
            this.grpConfig.TabStop = false;
            this.grpConfig.Text = "Mapp Config";
            // 
            // btnDestinationOK
            // 
            this.btnDestinationOK.Location = new System.Drawing.Point(318, 61);
            this.btnDestinationOK.Name = "btnDestinationOK";
            this.btnDestinationOK.Size = new System.Drawing.Size(75, 23);
            this.btnDestinationOK.TabIndex = 24;
            this.btnDestinationOK.Text = "Ok";
            this.btnDestinationOK.UseVisualStyleBackColor = true;
            this.btnDestinationOK.Visible = false;
            this.btnDestinationOK.Click += new System.EventHandler(this.btnDestinationOK_Click);
            // 
            // cmdUnpack
            // 
            this.cmdUnpack.Location = new System.Drawing.Point(324, 214);
            this.cmdUnpack.Name = "cmdUnpack";
            this.cmdUnpack.Size = new System.Drawing.Size(69, 23);
            this.cmdUnpack.TabIndex = 20;
            this.cmdUnpack.Text = "Unp./cp";
            this.cmdUnpack.UseVisualStyleBackColor = true;
            this.cmdUnpack.Click += new System.EventHandler(this.cmdUnpack_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 15);
            this.label7.TabIndex = 19;
            this.label7.Text = "Temp";
            // 
            // rbTemp
            // 
            this.rbTemp.AutoSize = true;
            this.rbTemp.Location = new System.Drawing.Point(9, 219);
            this.rbTemp.Name = "rbTemp";
            this.rbTemp.Size = new System.Drawing.Size(14, 13);
            this.rbTemp.TabIndex = 18;
            this.rbTemp.TabStop = true;
            this.rbTemp.UseVisualStyleBackColor = true;
            // 
            // txtTemp
            // 
            this.txtTemp.Location = new System.Drawing.Point(27, 215);
            this.txtTemp.Name = "txtTemp";
            this.txtTemp.ReadOnly = true;
            this.txtTemp.Size = new System.Drawing.Size(291, 23);
            this.txtTemp.TabIndex = 17;
            // 
            // rbLog
            // 
            this.rbLog.AutoSize = true;
            this.rbLog.Location = new System.Drawing.Point(8, 170);
            this.rbLog.Name = "rbLog";
            this.rbLog.Size = new System.Drawing.Size(14, 13);
            this.rbLog.TabIndex = 16;
            this.rbLog.TabStop = true;
            this.rbLog.UseVisualStyleBackColor = true;
            // 
            // rbDone
            // 
            this.rbDone.AutoSize = true;
            this.rbDone.Location = new System.Drawing.Point(8, 82);
            this.rbDone.Name = "rbDone";
            this.rbDone.Size = new System.Drawing.Size(14, 13);
            this.rbDone.TabIndex = 15;
            this.rbDone.TabStop = true;
            this.rbDone.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(27, 166);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(366, 23);
            this.txtLog.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Done";
            // 
            // txtDone
            // 
            this.txtDone.Location = new System.Drawing.Point(27, 78);
            this.txtDone.Name = "txtDone";
            this.txtDone.ReadOnly = true;
            this.txtDone.Size = new System.Drawing.Size(366, 23);
            this.txtDone.TabIndex = 11;
            // 
            // rbSource
            // 
            this.rbSource.AutoSize = true;
            this.rbSource.Location = new System.Drawing.Point(9, 126);
            this.rbSource.Name = "rbSource";
            this.rbSource.Size = new System.Drawing.Size(14, 13);
            this.rbSource.TabIndex = 10;
            this.rbSource.UseVisualStyleBackColor = true;
            // 
            // rbDestination
            // 
            this.rbDestination.AutoSize = true;
            this.rbDestination.Checked = true;
            this.rbDestination.Location = new System.Drawing.Point(6, 39);
            this.rbDestination.Name = "rbDestination";
            this.rbDestination.Size = new System.Drawing.Size(14, 13);
            this.rbDestination.TabIndex = 9;
            this.rbDestination.TabStop = true;
            this.rbDestination.UseVisualStyleBackColor = true;
            // 
            // btnFilesInKallaDir
            // 
            this.btnFilesInKallaDir.Location = new System.Drawing.Point(239, 15);
            this.btnFilesInKallaDir.Name = "btnFilesInKallaDir";
            this.btnFilesInKallaDir.Size = new System.Drawing.Size(91, 26);
            this.btnFilesInKallaDir.TabIndex = 10;
            this.btnFilesInKallaDir.Text = "Files count ->";
            this.btnFilesInKallaDir.UseVisualStyleBackColor = true;
            this.btnFilesInKallaDir.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnMyChoice
            // 
            this.btnMyChoice.Location = new System.Drawing.Point(259, 15);
            this.btnMyChoice.Name = "btnMyChoice";
            this.btnMyChoice.Size = new System.Drawing.Size(115, 26);
            this.btnMyChoice.TabIndex = 11;
            this.btnMyChoice.UseVisualStyleBackColor = true;
            this.btnMyChoice.Click += new System.EventHandler(this.btnMyChoice_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Choose:";
            // 
            // grpFooter
            // 
            this.grpFooter.Controls.Add(this.btnTest);
            this.grpFooter.Controls.Add(this.btnStartBatch);
            this.grpFooter.Controls.Add(this.btnClose);
            this.grpFooter.Location = new System.Drawing.Point(2, 401);
            this.grpFooter.Name = "grpFooter";
            this.grpFooter.Size = new System.Drawing.Size(796, 47);
            this.grpFooter.TabIndex = 13;
            this.grpFooter.TabStop = false;
            this.grpFooter.Text = "Batch start / finish";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(634, 18);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 15;
            this.btnTest.Text = "Dict. Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnStartBatch
            // 
            this.btnStartBatch.Location = new System.Drawing.Point(6, 18);
            this.btnStartBatch.Name = "btnStartBatch";
            this.btnStartBatch.Size = new System.Drawing.Size(75, 23);
            this.btnStartBatch.TabIndex = 14;
            this.btnStartBatch.Text = "Start Batch";
            this.btnStartBatch.UseVisualStyleBackColor = true;
            this.btnStartBatch.Click += new System.EventHandler(this.btnStartBatch_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(715, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpTreeView
            // 
            this.grpTreeView.Controls.Add(this.treeView1);
            this.grpTreeView.Controls.Add(this.label5);
            this.grpTreeView.Controls.Add(this.chkTreeViewExpanded);
            this.grpTreeView.Controls.Add(this.btnMyChoice);
            this.grpTreeView.Location = new System.Drawing.Point(2, 2);
            this.grpTreeView.Name = "grpTreeView";
            this.grpTreeView.Size = new System.Drawing.Size(385, 281);
            this.grpTreeView.TabIndex = 14;
            this.grpTreeView.TabStop = false;
            this.grpTreeView.Text = "Work Configuration";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "Aktuell batch:";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(94, 304);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.ReadOnly = true;
            this.txtBatchNo.Size = new System.Drawing.Size(78, 23);
            this.txtBatchNo.TabIndex = 16;
            // 
            // btnPlus
            // 
            this.btnPlus.Location = new System.Drawing.Point(178, 304);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(21, 23);
            this.btnPlus.TabIndex = 17;
            this.btnPlus.Tag = "Plus";
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            // 
            // btnMinus
            // 
            this.btnMinus.Location = new System.Drawing.Point(205, 304);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(21, 23);
            this.btnMinus.TabIndex = 18;
            this.btnMinus.Tag = "Minus";
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            // 
            // grpBatchParams
            // 
            this.grpBatchParams.Controls.Add(this.progressBar1);
            this.grpBatchParams.Controls.Add(this.lblFilesInSource);
            this.grpBatchParams.Controls.Add(this.label6);
            this.grpBatchParams.Controls.Add(this.btnFilesInKallaDir);
            this.grpBatchParams.Location = new System.Drawing.Point(2, 289);
            this.grpBatchParams.Name = "grpBatchParams";
            this.grpBatchParams.Size = new System.Drawing.Size(612, 47);
            this.grpBatchParams.TabIndex = 19;
            this.grpBatchParams.TabStop = false;
            this.grpBatchParams.Text = "Batch params";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(470, 15);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(132, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // lblFilesInSource
            // 
            this.lblFilesInSource.AutoSize = true;
            this.lblFilesInSource.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFilesInSource.Location = new System.Drawing.Point(336, 19);
            this.lblFilesInSource.Name = "lblFilesInSource";
            this.lblFilesInSource.Size = new System.Drawing.Size(131, 15);
            this.lblFilesInSource.TabIndex = 16;
            this.lblFilesInSource.Tag = "\".dat\" Files in Source:  ";
            this.lblFilesInSource.Text = "\".dat\" Files in Source:  ";
            // 
            // lblProcessedFiles
            // 
            this.lblProcessedFiles.AutoSize = true;
            this.lblProcessedFiles.Location = new System.Drawing.Point(711, 310);
            this.lblProcessedFiles.Name = "lblProcessedFiles";
            this.lblProcessedFiles.Size = new System.Drawing.Size(13, 15);
            this.lblProcessedFiles.TabIndex = 21;
            this.lblProcessedFiles.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBarCopyDelete
            // 
            this.progressBarCopyDelete.Location = new System.Drawing.Point(472, 342);
            this.progressBarCopyDelete.Name = "progressBarCopyDelete";
            this.progressBarCopyDelete.Size = new System.Drawing.Size(132, 23);
            this.progressBarCopyDelete.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(353, 342);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "Kopierar, rensar upp:";
            // 
            // lblZipProgress
            // 
            this.lblZipProgress.AutoSize = true;
            this.lblZipProgress.Location = new System.Drawing.Point(6, 19);
            this.lblZipProgress.Name = "lblZipProgress";
            this.lblZipProgress.Size = new System.Drawing.Size(36, 15);
            this.lblZipProgress.TabIndex = 22;
            this.lblZipProgress.Text = "unzip";
            // 
            // grpUnzip
            // 
            this.grpUnzip.Controls.Add(this.progressBarZip);
            this.grpUnzip.Controls.Add(this.lblZipProgress);
            this.grpUnzip.Location = new System.Drawing.Point(612, 337);
            this.grpUnzip.Name = "grpUnzip";
            this.grpUnzip.Size = new System.Drawing.Size(176, 38);
            this.grpUnzip.TabIndex = 23;
            this.grpUnzip.TabStop = false;
            this.grpUnzip.Text = "Zip";
            this.grpUnzip.Visible = false;
            // 
            // progressBarZip
            // 
            this.progressBarZip.Location = new System.Drawing.Point(55, 17);
            this.progressBarZip.Name = "progressBarZip";
            this.progressBarZip.Size = new System.Drawing.Size(115, 15);
            this.progressBarZip.TabIndex = 23;
            // 
            // timerZip
            // 
            this.timerZip.Interval = 500;
            this.timerZip.Tick += new System.EventHandler(this.timerZip_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(620, 310);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 24;
            this.label9.Text = "Processed files:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblProcessedFiles);
            this.Controls.Add(this.grpUnzip);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.progressBarCopyDelete);
            this.Controls.Add(this.txtBatchNo);
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.grpFooter);
            this.Controls.Add(this.grpConfig);
            this.Controls.Add(this.btnRemoveFolder);
            this.Controls.Add(this.btnAddFolder);
            this.Controls.Add(this.txtNewFolder);
            this.Controls.Add(this.grpTreeView);
            this.Controls.Add(this.grpBatchParams);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(816, 489);
            this.MinimumSize = new System.Drawing.Size(816, 489);
            this.Name = "Form1";
            this.Tag = "v. 1.0.5";
            this.Text = "DeponaHR1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpConfig.ResumeLayout(false);
            this.grpConfig.PerformLayout();
            this.grpFooter.ResumeLayout(false);
            this.grpTreeView.ResumeLayout(false);
            this.grpTreeView.PerformLayout();
            this.grpBatchParams.ResumeLayout(false);
            this.grpBatchParams.PerformLayout();
            this.grpUnzip.ResumeLayout(false);
            this.grpUnzip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox chkTreeViewExpanded;
        private System.Windows.Forms.TextBox txtNewFolder;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnRemoveFolder;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.GroupBox grpConfig;
        private System.Windows.Forms.RadioButton rbSource;
        private System.Windows.Forms.RadioButton rbDestination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDone;
        private System.Windows.Forms.RadioButton rbLog;
        private System.Windows.Forms.RadioButton rbDone;
        private System.Windows.Forms.Button btnFilesInKallaDir;
        private System.Windows.Forms.Button btnMyChoice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStartBatch;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.GroupBox grpTreeView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.GroupBox grpBatchParams;
        private System.Windows.Forms.Label lblFilesInSource;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbTemp;
        private System.Windows.Forms.TextBox txtTemp;
        private System.Windows.Forms.Button cmdUnpack;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblProcessedFiles;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBarCopyDelete;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblZipProgress;
        private System.Windows.Forms.GroupBox grpUnzip;
        private System.Windows.Forms.ProgressBar progressBarZip;
        private System.Windows.Forms.Timer timerZip;
        private System.Windows.Forms.Button btnDestinationOK;
        private System.Windows.Forms.Label label9;
    }
}

