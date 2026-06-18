namespace WordQuest.GUI
{
    partial class frmAdminTopics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            pnlTopicList = new Panel();
            btnXoaChuDe = new Button();
            btnThemChuDe = new Button();
            trvTopics = new TreeView();
            lblTopicListTitle = new Label();
            pnlTopicInfo = new Panel();
            btnBack = new Button();
            btnThemMucDo = new Button();
            pnlLevelList = new Panel();
            dgvLevels = new DataGridView();
            colLevelID = new DataGridViewTextBoxColumn();
            colLevelName = new DataGridViewTextBoxColumn();
            colLevelDiff = new DataGridViewTextBoxColumn();
            colLevelMode = new DataGridViewTextBoxColumn();
            colLevelCount = new DataGridViewTextBoxColumn();
            btnEditLevel = new DataGridViewButtonColumn();
            btnXoaMucDo = new DataGridViewButtonColumn();
            lblLevelListTitle = new Label();
            btnSaveTopic = new Button();
            nudStarsToUnlock = new NumericUpDown();
            lblStarsToUnlock = new Label();
            txtTopicIcon = new TextBox();
            lblTopicIcon = new Label();
            txtTopicName = new TextBox();
            lblTopicName = new Label();
            cboParentTopic = new ComboBox();
            lblParentTopic = new Label();
            lblTopicInfoTitle = new Label();
            pnlEditLevel = new Panel();
            btnCancelLevel = new Button();
            btnSaveLevel = new Button();
            clbWords = new CheckedListBox();
            btnApplyFilter = new Button();
            chkFilterHard = new CheckBox();
            chkFilterMedium = new CheckBox();
            chkFilterEasy = new CheckBox();
            lblFilterDiff = new Label();
            rdoFixed = new Button();
            rdoRandom = new Button();
            lblMode = new Label();
            nudLevelCount = new NumericUpDown();
            lblLevelCount = new Label();
            cboLevelDiff = new ComboBox();
            lblLevelDiff = new Label();
            txtLevelName = new TextBox();
            lblLevelName = new Label();
            lblEditLevelTitle = new Label();
            pnlTopicList.SuspendLayout();
            pnlTopicInfo.SuspendLayout();
            pnlLevelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLevels).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStarsToUnlock).BeginInit();
            pnlEditLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevelCount).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(312, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(354, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ CHỦ ĐỀ/MÀN CHƠI\r\n";
            // 
            // pnlTopicList
            // 
            pnlTopicList.Controls.Add(btnXoaChuDe);
            pnlTopicList.Controls.Add(btnThemChuDe);
            pnlTopicList.Controls.Add(trvTopics);
            pnlTopicList.Controls.Add(lblTopicListTitle);
            pnlTopicList.Location = new Point(12, 74);
            pnlTopicList.Name = "pnlTopicList";
            pnlTopicList.Size = new Size(316, 688);
            pnlTopicList.TabIndex = 1;
            // 
            // btnXoaChuDe
            // 
            btnXoaChuDe.Location = new Point(172, 645);
            btnXoaChuDe.Name = "btnXoaChuDe";
            btnXoaChuDe.Size = new Size(141, 40);
            btnXoaChuDe.TabIndex = 3;
            btnXoaChuDe.Text = "Xoá chủ đề";
            btnXoaChuDe.UseVisualStyleBackColor = true;
            // 
            // btnThemChuDe
            // 
            btnThemChuDe.Location = new Point(3, 645);
            btnThemChuDe.Name = "btnThemChuDe";
            btnThemChuDe.Size = new Size(141, 40);
            btnThemChuDe.TabIndex = 2;
            btnThemChuDe.Text = "+ Thêm chủ đề";
            btnThemChuDe.UseVisualStyleBackColor = true;
            // 
            // trvTopics
            // 
            trvTopics.Font = new Font("Segoe UI", 10F);
            trvTopics.FullRowSelect = true;
            trvTopics.HideSelection = false;
            trvTopics.Location = new Point(3, 53);
            trvTopics.Name = "trvTopics";
            trvTopics.Size = new Size(310, 586);
            trvTopics.TabIndex = 1;
            // 
            // lblTopicListTitle
            // 
            lblTopicListTitle.AutoSize = true;
            lblTopicListTitle.Location = new Point(71, 16);
            lblTopicListTitle.Name = "lblTopicListTitle";
            lblTopicListTitle.Size = new Size(182, 25);
            lblTopicListTitle.TabIndex = 0;
            lblTopicListTitle.Text = "DANH SÁCH CHỦ ĐỀ";
            // 
            // pnlTopicInfo
            // 
            pnlTopicInfo.Controls.Add(btnBack);
            pnlTopicInfo.Controls.Add(btnThemMucDo);
            pnlTopicInfo.Controls.Add(pnlLevelList);
            pnlTopicInfo.Controls.Add(btnSaveTopic);
            pnlTopicInfo.Controls.Add(nudStarsToUnlock);
            pnlTopicInfo.Controls.Add(lblStarsToUnlock);
            pnlTopicInfo.Controls.Add(txtTopicIcon);
            pnlTopicInfo.Controls.Add(lblTopicIcon);
            pnlTopicInfo.Controls.Add(txtTopicName);
            pnlTopicInfo.Controls.Add(lblTopicName);
            pnlTopicInfo.Controls.Add(cboParentTopic);
            pnlTopicInfo.Controls.Add(lblParentTopic);
            pnlTopicInfo.Controls.Add(lblTopicInfoTitle);
            pnlTopicInfo.Location = new Point(334, 74);
            pnlTopicInfo.Name = "pnlTopicInfo";
            pnlTopicInfo.Size = new Size(600, 392);
            pnlTopicInfo.TabIndex = 2;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(454, 346);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(112, 34);
            btnBack.TabIndex = 18;
            btnBack.Text = "Quay lại";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnThemMucDo
            // 
            btnThemMucDo.Location = new Point(17, 348);
            btnThemMucDo.Name = "btnThemMucDo";
            btnThemMucDo.Size = new Size(125, 34);
            btnThemMucDo.TabIndex = 12;
            btnThemMucDo.Text = "+ Thêm màn";
            btnThemMucDo.UseVisualStyleBackColor = true;
            // 
            // pnlLevelList
            // 
            pnlLevelList.Controls.Add(dgvLevels);
            pnlLevelList.Controls.Add(lblLevelListTitle);
            pnlLevelList.Location = new Point(8, 109);
            pnlLevelList.Name = "pnlLevelList";
            pnlLevelList.Size = new Size(566, 236);
            pnlLevelList.TabIndex = 11;
            // 
            // dgvLevels
            // 
            dgvLevels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLevels.Columns.AddRange(new DataGridViewColumn[] { colLevelID, colLevelName, colLevelDiff, colLevelMode, colLevelCount, btnEditLevel, btnXoaMucDo });
            dgvLevels.EnableHeadersVisualStyles = false;
            dgvLevels.Location = new Point(8, 37);
            dgvLevels.Name = "dgvLevels";
            dgvLevels.RowHeadersVisible = false;
            dgvLevels.RowHeadersWidth = 62;
            dgvLevels.Size = new Size(550, 195);
            dgvLevels.TabIndex = 1;
            // 
            // colLevelID
            // 
            colLevelID.HeaderText = "ID";
            colLevelID.MinimumWidth = 8;
            colLevelID.Name = "colLevelID";
            colLevelID.Width = 50;
            // 
            // colLevelName
            // 
            colLevelName.HeaderText = "Tên màn";
            colLevelName.MinimumWidth = 8;
            colLevelName.Name = "colLevelName";
            colLevelName.Width = 120;
            // 
            // colLevelDiff
            // 
            colLevelDiff.HeaderText = "Độ khó";
            colLevelDiff.MinimumWidth = 8;
            colLevelDiff.Name = "colLevelDiff";
            colLevelDiff.Width = 110;
            // 
            // colLevelMode
            // 
            colLevelMode.HeaderText = "Chế độ";
            colLevelMode.MinimumWidth = 8;
            colLevelMode.Name = "colLevelMode";
            colLevelMode.Width = 110;
            // 
            // colLevelCount
            // 
            colLevelCount.HeaderText = "Số câu";
            colLevelCount.MinimumWidth = 8;
            colLevelCount.Name = "colLevelCount";
            colLevelCount.Width = 55;
            // 
            // btnEditLevel
            // 
            btnEditLevel.HeaderText = "Sửa";
            btnEditLevel.MinimumWidth = 8;
            btnEditLevel.Name = "btnEditLevel";
            btnEditLevel.Resizable = DataGridViewTriState.True;
            btnEditLevel.Width = 50;
            // 
            // btnXoaMucDo
            // 
            btnXoaMucDo.HeaderText = "Xoá";
            btnXoaMucDo.MinimumWidth = 8;
            btnXoaMucDo.Name = "btnXoaMucDo";
            btnXoaMucDo.Width = 50;
            // 
            // lblLevelListTitle
            // 
            lblLevelListTitle.AutoSize = true;
            lblLevelListTitle.Location = new Point(8, 9);
            lblLevelListTitle.Name = "lblLevelListTitle";
            lblLevelListTitle.Size = new Size(200, 25);
            lblLevelListTitle.TabIndex = 0;
            lblLevelListTitle.Text = "CÁC MÀN CỦA CHỦ ĐỀ";
            // 
            // btnSaveTopic
            // 
            btnSaveTopic.Location = new Point(486, 33);
            btnSaveTopic.Name = "btnSaveTopic";
            btnSaveTopic.Size = new Size(88, 37);
            btnSaveTopic.TabIndex = 8;
            btnSaveTopic.Text = "Lưu";
            btnSaveTopic.UseVisualStyleBackColor = true;
            // 
            // nudStarsToUnlock
            // 
            nudStarsToUnlock.Location = new Point(406, 36);
            nudStarsToUnlock.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudStarsToUnlock.Name = "nudStarsToUnlock";
            nudStarsToUnlock.Size = new Size(61, 31);
            nudStarsToUnlock.TabIndex = 7;
            // 
            // lblStarsToUnlock
            // 
            lblStarsToUnlock.AutoSize = true;
            lblStarsToUnlock.Location = new Point(364, 36);
            lblStarsToUnlock.Name = "lblStarsToUnlock";
            lblStarsToUnlock.Size = new Size(46, 25);
            lblStarsToUnlock.TabIndex = 6;
            lblStarsToUnlock.Text = "Sao:";
            // 
            // txtTopicIcon
            // 
            txtTopicIcon.Location = new Point(258, 33);
            txtTopicIcon.Name = "txtTopicIcon";
            txtTopicIcon.Size = new Size(100, 31);
            txtTopicIcon.TabIndex = 4;
            // 
            // lblTopicIcon
            // 
            lblTopicIcon.AutoSize = true;
            lblTopicIcon.Location = new Point(213, 36);
            lblTopicIcon.Name = "lblTopicIcon";
            lblTopicIcon.Size = new Size(50, 25);
            lblTopicIcon.TabIndex = 3;
            lblTopicIcon.Text = "Icon:";
            // 
            // txtTopicName
            // 
            txtTopicName.Location = new Point(105, 33);
            txtTopicName.Name = "txtTopicName";
            txtTopicName.Size = new Size(102, 31);
            txtTopicName.TabIndex = 2;
            // 
            // lblTopicName
            // 
            lblTopicName.AutoSize = true;
            lblTopicName.Location = new Point(8, 36);
            lblTopicName.Name = "lblTopicName";
            lblTopicName.Size = new Size(100, 25);
            lblTopicName.TabIndex = 1;
            lblTopicName.Text = "Tên chủ đề:";
            // 
            // cboParentTopic
            // 
            cboParentTopic.DropDownStyle = ComboBoxStyle.DropDownList;
            cboParentTopic.FormattingEnabled = true;
            cboParentTopic.Location = new Point(105, 70);
            cboParentTopic.Name = "cboParentTopic";
            cboParentTopic.Size = new Size(200, 33);
            cboParentTopic.TabIndex = 20;
            // 
            // lblParentTopic
            // 
            lblParentTopic.AutoSize = true;
            lblParentTopic.Location = new Point(8, 73);
            lblParentTopic.Name = "lblParentTopic";
            lblParentTopic.Size = new Size(104, 25);
            lblParentTopic.TabIndex = 19;
            lblParentTopic.Text = "Chủ đề cha:";
            // 
            // lblTopicInfoTitle
            // 
            lblTopicInfoTitle.AutoSize = true;
            lblTopicInfoTitle.Location = new Point(17, 7);
            lblTopicInfoTitle.Name = "lblTopicInfoTitle";
            lblTopicInfoTitle.Size = new Size(173, 25);
            lblTopicInfoTitle.TabIndex = 0;
            lblTopicInfoTitle.Text = "THÔNG TIN CHỦ ĐỀ";
            // 
            // pnlEditLevel
            // 
            pnlEditLevel.Controls.Add(btnCancelLevel);
            pnlEditLevel.Controls.Add(btnSaveLevel);
            pnlEditLevel.Controls.Add(clbWords);
            pnlEditLevel.Controls.Add(btnApplyFilter);
            pnlEditLevel.Controls.Add(chkFilterHard);
            pnlEditLevel.Controls.Add(chkFilterMedium);
            pnlEditLevel.Controls.Add(chkFilterEasy);
            pnlEditLevel.Controls.Add(lblFilterDiff);
            pnlEditLevel.Controls.Add(rdoFixed);
            pnlEditLevel.Controls.Add(rdoRandom);
            pnlEditLevel.Controls.Add(lblMode);
            pnlEditLevel.Controls.Add(nudLevelCount);
            pnlEditLevel.Controls.Add(lblLevelCount);
            pnlEditLevel.Controls.Add(cboLevelDiff);
            pnlEditLevel.Controls.Add(lblLevelDiff);
            pnlEditLevel.Controls.Add(txtLevelName);
            pnlEditLevel.Controls.Add(lblLevelName);
            pnlEditLevel.Controls.Add(lblEditLevelTitle);
            pnlEditLevel.Location = new Point(334, 472);
            pnlEditLevel.Name = "pnlEditLevel";
            pnlEditLevel.Size = new Size(600, 290);
            pnlEditLevel.TabIndex = 3;
            // 
            // btnCancelLevel
            // 
            btnCancelLevel.Location = new Point(127, 256);
            btnCancelLevel.Name = "btnCancelLevel";
            btnCancelLevel.Size = new Size(112, 34);
            btnCancelLevel.TabIndex = 17;
            btnCancelLevel.Text = "Huỷ";
            btnCancelLevel.UseVisualStyleBackColor = true;
            // 
            // btnSaveLevel
            // 
            btnSaveLevel.Location = new Point(9, 256);
            btnSaveLevel.Name = "btnSaveLevel";
            btnSaveLevel.Size = new Size(112, 34);
            btnSaveLevel.TabIndex = 16;
            btnSaveLevel.Text = "Lưu màn";
            btnSaveLevel.UseVisualStyleBackColor = true;
            // 
            // clbWords
            // 
            clbWords.FormattingEnabled = true;
            clbWords.Location = new Point(9, 112);
            clbWords.Name = "clbWords";
            clbWords.Size = new Size(557, 144);
            clbWords.TabIndex = 15;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.Location = new Point(515, 74);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(54, 34);
            btnApplyFilter.TabIndex = 14;
            btnApplyFilter.Text = "Lưu";
            btnApplyFilter.UseVisualStyleBackColor = true;
            // 
            // chkFilterHard
            // 
            chkFilterHard.AutoSize = true;
            chkFilterHard.Location = new Point(443, 77);
            chkFilterHard.Name = "chkFilterHard";
            chkFilterHard.Size = new Size(69, 29);
            chkFilterHard.TabIndex = 13;
            chkFilterHard.Text = "Khó";
            chkFilterHard.UseVisualStyleBackColor = true;
            // 
            // chkFilterMedium
            // 
            chkFilterMedium.AutoSize = true;
            chkFilterMedium.Location = new Point(389, 77);
            chkFilterMedium.Name = "chkFilterMedium";
            chkFilterMedium.Size = new Size(57, 29);
            chkFilterMedium.TabIndex = 12;
            chkFilterMedium.Text = "TB";
            chkFilterMedium.UseVisualStyleBackColor = true;
            // 
            // chkFilterEasy
            // 
            chkFilterEasy.AutoSize = true;
            chkFilterEasy.Location = new Point(337, 77);
            chkFilterEasy.Name = "chkFilterEasy";
            chkFilterEasy.Size = new Size(60, 29);
            chkFilterEasy.TabIndex = 11;
            chkFilterEasy.Text = "Dễ";
            chkFilterEasy.UseVisualStyleBackColor = true;
            // 
            // lblFilterDiff
            // 
            lblFilterDiff.AutoSize = true;
            lblFilterDiff.Location = new Point(273, 78);
            lblFilterDiff.Name = "lblFilterDiff";
            lblFilterDiff.Size = new Size(65, 25);
            lblFilterDiff.TabIndex = 10;
            lblFilterDiff.Text = "Lọc từ:";
            // 
            // rdoFixed
            // 
            rdoFixed.BackColor = Color.White;
            rdoFixed.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rdoFixed.Location = new Point(180, 74);
            rdoFixed.Name = "rdoFixed";
            rdoFixed.Size = new Size(87, 34);
            rdoFixed.TabIndex = 9;
            rdoFixed.Text = "Tuỳ chọn";
            rdoFixed.UseVisualStyleBackColor = false;
            // 
            // rdoRandom
            // 
            rdoRandom.BackColor = Color.White;
            rdoRandom.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rdoRandom.Location = new Point(87, 73);
            rdoRandom.Name = "rdoRandom";
            rdoRandom.Size = new Size(87, 34);
            rdoRandom.TabIndex = 8;
            rdoRandom.Text = "Random";
            rdoRandom.UseVisualStyleBackColor = false;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(9, 78);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(73, 25);
            lblMode.TabIndex = 7;
            lblMode.Text = "Chế độ:";
            // 
            // nudLevelCount
            // 
            nudLevelCount.Location = new Point(459, 34);
            nudLevelCount.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudLevelCount.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            nudLevelCount.Name = "nudLevelCount";
            nudLevelCount.Size = new Size(96, 31);
            nudLevelCount.TabIndex = 6;
            nudLevelCount.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblLevelCount
            // 
            lblLevelCount.AutoSize = true;
            lblLevelCount.Location = new Point(393, 37);
            lblLevelCount.Name = "lblLevelCount";
            lblLevelCount.Size = new Size(69, 25);
            lblLevelCount.TabIndex = 5;
            lblLevelCount.Text = "Số câu:";
            // 
            // cboLevelDiff
            // 
            cboLevelDiff.FormattingEnabled = true;
            cboLevelDiff.Location = new Point(289, 31);
            cboLevelDiff.Name = "cboLevelDiff";
            cboLevelDiff.Size = new Size(81, 33);
            cboLevelDiff.TabIndex = 4;
            cboLevelDiff.Text = "Dễ";
            // 
            // lblLevelDiff
            // 
            lblLevelDiff.AutoSize = true;
            lblLevelDiff.Location = new Point(217, 34);
            lblLevelDiff.Name = "lblLevelDiff";
            lblLevelDiff.Size = new Size(75, 25);
            lblLevelDiff.TabIndex = 3;
            lblLevelDiff.Text = "Độ khó:";
            // 
            // txtLevelName
            // 
            txtLevelName.Location = new Point(87, 34);
            txtLevelName.Name = "txtLevelName";
            txtLevelName.Size = new Size(115, 31);
            txtLevelName.TabIndex = 2;
            // 
            // lblLevelName
            // 
            lblLevelName.AutoSize = true;
            lblLevelName.Location = new Point(9, 34);
            lblLevelName.Name = "lblLevelName";
            lblLevelName.Size = new Size(82, 25);
            lblLevelName.TabIndex = 1;
            lblLevelName.Text = "Tên màn:";
            // 
            // lblEditLevelTitle
            // 
            lblEditLevelTitle.AutoSize = true;
            lblEditLevelTitle.Location = new Point(17, 9);
            lblEditLevelTitle.Name = "lblEditLevelTitle";
            lblEditLevelTitle.Size = new Size(152, 25);
            lblEditLevelTitle.TabIndex = 0;
            lblEditLevelTitle.Text = "THÊM /SỬA MÀN";
            // 
            // frmAdminTopics
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(958, 774);
            Controls.Add(pnlEditLevel);
            Controls.Add(pnlTopicInfo);
            Controls.Add(pnlTopicList);
            Controls.Add(lblTitle);
            Name = "frmAdminTopics";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += frmAdminTopics_Load;
            pnlTopicList.ResumeLayout(false);
            pnlTopicList.PerformLayout();
            pnlTopicInfo.ResumeLayout(false);
            pnlTopicInfo.PerformLayout();
            pnlLevelList.ResumeLayout(false);
            pnlLevelList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLevels).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStarsToUnlock).EndInit();
            pnlEditLevel.ResumeLayout(false);
            pnlEditLevel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLevelCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlTopicList;
        private Button btnXoaChuDe;
        private Button btnThemChuDe;
        private TreeView trvTopics;
        private Label lblTopicListTitle;
        private Panel pnlTopicInfo;
        private TextBox txtTopicIcon;
        private Label lblTopicIcon;
        private TextBox txtTopicName;
        private Label lblTopicName;
        private ComboBox cboParentTopic;
        private Label lblParentTopic;
        private Label lblTopicInfoTitle;
        private NumericUpDown nudStarsToUnlock;
        private Label lblStarsToUnlock;
        private Button btnSaveTopic;
        private Panel pnlLevelList;
        private DataGridView dgvLevels;
        private Label lblLevelListTitle;
        private Panel pnlEditLevel;
        private NumericUpDown nudLevelCount;
        private Label lblLevelCount;
        private ComboBox cboLevelDiff;
        private Label lblLevelDiff;
        private TextBox txtLevelName;
        private Label lblLevelName;
        private Label lblEditLevelTitle;
        private CheckBox chkFilterHard;
        private CheckBox chkFilterMedium;
        private CheckBox chkFilterEasy;
        private Label lblFilterDiff;
        private Button rdoFixed;
        private Button rdoRandom;
        private Label lblMode;
        private Button btnApplyFilter;
        private Button btnThemMucDo;
        private Button btnCancelLevel;
        private Button btnSaveLevel;
        private CheckedListBox clbWords;
        private Button btnBack;
        private DataGridViewTextBoxColumn colLevelID;
        private DataGridViewTextBoxColumn colLevelName;
        private DataGridViewTextBoxColumn colLevelDiff;
        private DataGridViewTextBoxColumn colLevelMode;
        private DataGridViewTextBoxColumn colLevelCount;
        private DataGridViewButtonColumn btnEditLevel;
        private DataGridViewButtonColumn btnXoaMucDo;
    }
}
