namespace feederdikti_importer
{
    partial class formImporter
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
            labelDbPort = new Label();
            txtDbPort = new TextBox();
            txtDbUserName = new TextBox();
            labelDbUserName = new Label();
            txtDbPassword = new TextBox();
            label3 = new Label();
            txtDBName = new TextBox();
            label4 = new Label();
            txtApiAddress = new TextBox();
            label5 = new Label();
            txtApiUserName = new TextBox();
            label6 = new Label();
            txtApiPassword = new TextBox();
            label7 = new Label();
            label8 = new Label();
            txtApiAdditionalFilter = new TextBox();
            label1 = new Label();
            txtDestTableName = new TextBox();
            label2 = new Label();
            btnRun = new Button();
            btnCancel = new Button();
            progressBar = new ProgressBar();
            txtLog = new TextBox();
            txtDbAddress = new TextBox();
            label9 = new Label();
            cbxDropAndCreate = new CheckBox();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            cbxSeparateDatabase = new CheckBox();
            txtAdditionalLimit = new TextBox();
            label13 = new Label();
            cmbProdi = new ComboBox();
            cmbApiAction = new ComboBox();
            label14 = new Label();
            label15 = new Label();
            lblFileName = new Label();
            btnExportFile = new Button();
            btnOpenFile = new Button();
            SuspendLayout();
            // 
            // labelDbPort
            // 
            labelDbPort.AutoSize = true;
            labelDbPort.Location = new Point(51, 136);
            labelDbPort.Margin = new Padding(6, 0, 6, 0);
            labelDbPort.Name = "labelDbPort";
            labelDbPort.Size = new Size(106, 37);
            labelDbPort.TabIndex = 0;
            labelDbPort.Text = "DB Port";
            // 
            // txtDbPort
            // 
            txtDbPort.Location = new Point(266, 128);
            txtDbPort.Margin = new Padding(6, 7, 6, 7);
            txtDbPort.Name = "txtDbPort";
            txtDbPort.Size = new Size(973, 43);
            txtDbPort.TabIndex = 20;
            txtDbPort.Text = "3306";
            // 
            // txtDbUserName
            // 
            txtDbUserName.Location = new Point(266, 188);
            txtDbUserName.Margin = new Padding(6, 7, 6, 7);
            txtDbUserName.Name = "txtDbUserName";
            txtDbUserName.Size = new Size(973, 43);
            txtDbUserName.TabIndex = 30;
            txtDbUserName.Text = "root";
            // 
            // labelDbUserName
            // 
            labelDbUserName.AutoSize = true;
            labelDbUserName.Location = new Point(51, 195);
            labelDbUserName.Margin = new Padding(6, 0, 6, 0);
            labelDbUserName.Name = "labelDbUserName";
            labelDbUserName.Size = new Size(177, 37);
            labelDbUserName.TabIndex = 2;
            labelDbUserName.Text = "DB Username";
            // 
            // txtDbPassword
            // 
            txtDbPassword.Location = new Point(266, 247);
            txtDbPassword.Margin = new Padding(6, 7, 6, 7);
            txtDbPassword.Name = "txtDbPassword";
            txtDbPassword.PasswordChar = '*';
            txtDbPassword.Size = new Size(973, 43);
            txtDbPassword.TabIndex = 40;
            txtDbPassword.Text = "root";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(51, 255);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(169, 37);
            label3.TabIndex = 4;
            label3.Text = "DB Password";
            // 
            // txtDBName
            // 
            txtDBName.Location = new Point(266, 307);
            txtDBName.Margin = new Padding(6, 7, 6, 7);
            txtDBName.Name = "txtDBName";
            txtDBName.Size = new Size(973, 43);
            txtDBName.TabIndex = 50;
            txtDBName.Text = "db_importer_feeder";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(51, 314);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(129, 37);
            label4.TabIndex = 6;
            label4.Text = "DB Name";
            // 
            // txtApiAddress
            // 
            txtApiAddress.Location = new Point(266, 417);
            txtApiAddress.Margin = new Padding(6, 7, 6, 7);
            txtApiAddress.Name = "txtApiAddress";
            txtApiAddress.Size = new Size(973, 43);
            txtApiAddress.TabIndex = 60;
            txtApiAddress.Text = "http://103.149.179.178:3003/ws/live2.php";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(51, 424);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(157, 37);
            label5.TabIndex = 8;
            label5.Text = "API Address";
            // 
            // txtApiUserName
            // 
            txtApiUserName.Location = new Point(266, 476);
            txtApiUserName.Margin = new Padding(6, 7, 6, 7);
            txtApiUserName.Name = "txtApiUserName";
            txtApiUserName.Size = new Size(973, 43);
            txtApiUserName.TabIndex = 70;
            txtApiUserName.Text = "081020";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(51, 484);
            label6.Margin = new Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new Size(182, 37);
            label6.TabIndex = 10;
            label6.Text = "API Username";
            // 
            // txtApiPassword
            // 
            txtApiPassword.Location = new Point(266, 536);
            txtApiPassword.Margin = new Padding(6, 7, 6, 7);
            txtApiPassword.Name = "txtApiPassword";
            txtApiPassword.PasswordChar = '*';
            txtApiPassword.Size = new Size(973, 43);
            txtApiPassword.TabIndex = 80;
            txtApiPassword.Text = "@Eliazaredwardadminukawkupang1510034985";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(51, 543);
            label7.Margin = new Padding(6, 0, 6, 0);
            label7.Name = "label7";
            label7.Size = new Size(174, 37);
            label7.TabIndex = 12;
            label7.Text = "API Password";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(60, 661);
            label8.Margin = new Padding(6, 0, 6, 0);
            label8.Name = "label8";
            label8.Size = new Size(139, 37);
            label8.TabIndex = 14;
            label8.Text = "API Action";
            // 
            // txtApiAdditionalFilter
            // 
            txtApiAdditionalFilter.Enabled = false;
            txtApiAdditionalFilter.Location = new Point(269, 767);
            txtApiAdditionalFilter.Margin = new Padding(6, 7, 6, 7);
            txtApiAdditionalFilter.Name = "txtApiAdditionalFilter";
            txtApiAdditionalFilter.Size = new Size(970, 43);
            txtApiAdditionalFilter.TabIndex = 100;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(55, 770);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(202, 37);
            label1.TabIndex = 19;
            label1.Text = "Additional filter";
            // 
            // txtDestTableName
            // 
            txtDestTableName.Enabled = false;
            txtDestTableName.Location = new Point(266, 879);
            txtDestTableName.Margin = new Padding(6, 7, 6, 7);
            txtDestTableName.Name = "txtDestTableName";
            txtDestTableName.Size = new Size(715, 43);
            txtDestTableName.TabIndex = 110;
            txtDestTableName.Text = "FeederRawData_ProfilPT";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(51, 882);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(217, 37);
            label2.TabIndex = 21;
            label2.Text = "Dest Table Name";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(909, 956);
            btnRun.Margin = new Padding(6, 7, 6, 7);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(161, 57);
            btnRun.TabIndex = 130;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(1082, 956);
            btnCancel.Margin = new Padding(6, 7, 6, 7);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(161, 57);
            btnCancel.TabIndex = 140;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Bottom;
            progressBar.Location = new Point(0, 1279);
            progressBar.Margin = new Padding(6, 7, 6, 7);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(2104, 57);
            progressBar.TabIndex = 25;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.BackColor = SystemColors.ControlLightLight;
            txtLog.Location = new Point(1290, 34);
            txtLog.Margin = new Padding(6, 7, 6, 7);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Both;
            txtLog.Size = new Size(779, 1207);
            txtLog.TabIndex = 26;
            txtLog.TabStop = false;
            txtLog.WordWrap = false;
            // 
            // txtDbAddress
            // 
            txtDbAddress.Location = new Point(266, 69);
            txtDbAddress.Margin = new Padding(6, 7, 6, 7);
            txtDbAddress.Name = "txtDbAddress";
            txtDbAddress.Size = new Size(973, 43);
            txtDbAddress.TabIndex = 10;
            txtDbAddress.Text = "localhost";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(51, 76);
            label9.Margin = new Padding(6, 0, 6, 0);
            label9.Name = "label9";
            label9.Size = new Size(152, 37);
            label9.TabIndex = 27;
            label9.Text = "DB Address";
            // 
            // cbxDropAndCreate
            // 
            cbxDropAndCreate.AutoSize = true;
            cbxDropAndCreate.Checked = true;
            cbxDropAndCreate.CheckState = CheckState.Checked;
            cbxDropAndCreate.Location = new Point(1003, 884);
            cbxDropAndCreate.Margin = new Padding(6, 7, 6, 7);
            cbxDropAndCreate.Name = "cbxDropAndCreate";
            cbxDropAndCreate.Size = new Size(245, 41);
            cbxDropAndCreate.TabIndex = 120;
            cbxDropAndCreate.Text = "Drop and Create";
            cbxDropAndCreate.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(28, 22);
            label10.Margin = new Padding(6, 0, 6, 0);
            label10.Name = "label10";
            label10.Size = new Size(319, 37);
            label10.TabIndex = 141;
            label10.Text = "Target Database Config";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(28, 367);
            label11.Margin = new Padding(6, 0, 6, 0);
            label11.Name = "label11";
            label11.Size = new Size(342, 37);
            label11.TabIndex = 142;
            label11.Text = "Source Feeder API Config";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(28, 601);
            label12.Margin = new Padding(6, 0, 6, 0);
            label12.Name = "label12";
            label12.Size = new Size(171, 37);
            label12.TabIndex = 143;
            label12.Text = "API Method";
            // 
            // cbxSeparateDatabase
            // 
            cbxSeparateDatabase.AutoSize = true;
            cbxSeparateDatabase.Location = new Point(608, 965);
            cbxSeparateDatabase.Margin = new Padding(6, 7, 6, 7);
            cbxSeparateDatabase.Name = "cbxSeparateDatabase";
            cbxSeparateDatabase.Size = new Size(271, 41);
            cbxSeparateDatabase.TabIndex = 144;
            cbxSeparateDatabase.Text = "Separate Database";
            cbxSeparateDatabase.UseVisualStyleBackColor = true;
            cbxSeparateDatabase.CheckedChanged += cbxSeparateDatabase_CheckedChanged;
            // 
            // txtAdditionalLimit
            // 
            txtAdditionalLimit.Location = new Point(266, 822);
            txtAdditionalLimit.Margin = new Padding(6, 7, 6, 7);
            txtAdditionalLimit.Name = "txtAdditionalLimit";
            txtAdditionalLimit.Size = new Size(973, 43);
            txtAdditionalLimit.TabIndex = 146;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(51, 825);
            label13.Margin = new Padding(6, 0, 6, 0);
            label13.Name = "label13";
            label13.Size = new Size(201, 37);
            label13.TabIndex = 145;
            label13.Text = "Additional limit";
            // 
            // cmbProdi
            // 
            cmbProdi.FormattingEnabled = true;
            cmbProdi.Location = new Point(266, 709);
            cmbProdi.Name = "cmbProdi";
            cmbProdi.Size = new Size(977, 45);
            cmbProdi.TabIndex = 147;
            cmbProdi.SelectedIndexChanged += cmbProdi_SelectedIndexChanged;
            // 
            // cmbApiAction
            // 
            cmbApiAction.FormattingEnabled = true;
            cmbApiAction.Location = new Point(266, 658);
            cmbApiAction.Name = "cmbApiAction";
            cmbApiAction.Size = new Size(974, 45);
            cmbApiAction.TabIndex = 148;
            cmbApiAction.Text = "ProfilPT";
            cmbApiAction.SelectedIndexChanged += cmbApiAction_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(51, 712);
            label14.Margin = new Padding(6, 0, 6, 0);
            label14.Name = "label14";
            label14.Size = new Size(192, 37);
            label14.TabIndex = 149;
            label14.Text = "Program Study";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(28, 1056);
            label15.Margin = new Padding(6, 0, 6, 0);
            label15.Name = "label15";
            label15.Size = new Size(286, 37);
            label15.TabIndex = 150;
            label15.Text = "Export Excel to Table";
            // 
            // lblFileName
            // 
            lblFileName.AutoSize = true;
            lblFileName.Location = new Point(31, 1171);
            lblFileName.Margin = new Padding(6, 0, 6, 0);
            lblFileName.Name = "lblFileName";
            lblFileName.Size = new Size(124, 37);
            lblFileName.TabIndex = 151;
            lblFileName.Text = "Filename";
            // 
            // btnExportFile
            // 
            btnExportFile.Location = new Point(398, 1107);
            btnExportFile.Margin = new Padding(6, 7, 6, 7);
            btnExportFile.Name = "btnExportFile";
            btnExportFile.Size = new Size(339, 57);
            btnExportFile.TabIndex = 152;
            btnExportFile.Text = "Run Export";
            btnExportFile.UseVisualStyleBackColor = true;
            btnExportFile.Click += btnExportFile_Click;
            // 
            // btnOpenFile
            // 
            btnOpenFile.Location = new Point(31, 1107);
            btnOpenFile.Margin = new Padding(6, 7, 6, 7);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(339, 57);
            btnOpenFile.TabIndex = 153;
            btnOpenFile.Text = "Open File";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // formImporter
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2104, 1336);
            Controls.Add(btnOpenFile);
            Controls.Add(btnExportFile);
            Controls.Add(lblFileName);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(cmbApiAction);
            Controls.Add(cmbProdi);
            Controls.Add(txtAdditionalLimit);
            Controls.Add(label13);
            Controls.Add(cbxSeparateDatabase);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(cbxDropAndCreate);
            Controls.Add(txtDbAddress);
            Controls.Add(label9);
            Controls.Add(txtLog);
            Controls.Add(progressBar);
            Controls.Add(btnCancel);
            Controls.Add(btnRun);
            Controls.Add(txtDestTableName);
            Controls.Add(label2);
            Controls.Add(txtApiAdditionalFilter);
            Controls.Add(label1);
            Controls.Add(label8);
            Controls.Add(txtApiPassword);
            Controls.Add(label7);
            Controls.Add(txtApiUserName);
            Controls.Add(label6);
            Controls.Add(txtApiAddress);
            Controls.Add(label5);
            Controls.Add(txtDBName);
            Controls.Add(label4);
            Controls.Add(txtDbPassword);
            Controls.Add(label3);
            Controls.Add(txtDbUserName);
            Controls.Add(labelDbUserName);
            Controls.Add(txtDbPort);
            Controls.Add(labelDbPort);
            Margin = new Padding(6, 7, 6, 7);
            Name = "formImporter";
            Text = "Dikti Feeder Importer";
            Load += formImporter_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelDbAddress;
        private TextBox txtDbAddress;
        private TextBox txtDbUserName;
        private Label labelDbUserName;
        private TextBox txtDbPassword;
        private Label label3;
        private TextBox txtDBName;
        private Label label4;
        private TextBox txtApiAddress;
        private Label label5;
        private TextBox txtApiUserName;
        private Label label6;
        private TextBox txtApiPassword;
        private Label label7;
        private Label label8;
        private TextBox txtApiAdditionalFilter;
        private Label label1;
        private TextBox txtDestTableName;
        private Label label2;
        private Button btnRun;
        private Button btnCancel;
        private ProgressBar progressBar;
        private TextBox txtLog;
        private Label label9;
        private Label labelDbPort;
        private TextBox txtDbPort;
        private CheckBox cbxDropAndCreate;
        private Label label10;
        private Label label11;
        private Label label12;
        private CheckBox cbxSeparateDatabase;
        private TextBox txtAdditionalLimit;
        private Label label13;
        private ComboBox cmbProdi;
        private ComboBox cmbApiAction;
        private Label label14;
        private Label label15;
        private Label lblFileName;
        private Button btnExportFile;
        private Button btnOpenFile;
    }
}
