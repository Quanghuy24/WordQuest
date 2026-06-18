namespace WordQuest.GUI
{
    partial class frmAdmin
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            btnShowAll = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            cboFilterDiff = new ComboBox();
            lblFilterDiff = new Label();
            lblFilterImage = new Label();
            dgvWords = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colTopic = new DataGridViewTextBoxColumn();
            colEnglish = new DataGridViewTextBoxColumn();
            colPhonetic = new DataGridViewTextBoxColumn();
            colVietnamese = new DataGridViewTextBoxColumn();
            colDifficulty = new DataGridViewTextBoxColumn();
            colEmojiIcon = new DataGridViewTextBoxColumn();
            colErase = new DataGridViewButtonColumn();
            colImagePath = new DataGridViewTextBoxColumn();
            pnlInput = new Panel();
            picPreview = new PictureBox();
            btnPickImage = new Button();
            label1 = new Label();
            btnBack = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            txtHint = new TextBox();
            lblInputHint = new Label();
            txtPhonetic = new TextBox();
            txtVietnamese = new TextBox();
            txtEnglish = new TextBox();
            lblInputVI = new Label();
            cboInputDiff = new ComboBox();
            lblInputDiff = new Label();
            lblPhonetic = new Label();
            lblInputEN = new Label();
            cboInputTopic = new ComboBox();
            lblInputTopic = new Label();
            lblThemSua = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            cboFilterImage = new ComboBox();
            panel1 = new Panel();
            panel3 = new Panel();
            cboPageSize = new ComboBox();
            lblNext = new Label();
            lblPage = new Label();
            lblPrev = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblTitle = new Label();
            treeTopics = new TreeView();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvWords).BeginInit();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // btnShowAll
            // 
            btnShowAll.Anchor = AnchorStyles.Left;
            btnShowAll.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnShowAll.Location = new Point(1305, 6);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(87, 51);
            btnShowAll.TabIndex = 7;
            btnShowAll.Text = "Tất cả";
            btnShowAll.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Right;
            btnSearch.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(1204, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(95, 51);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Tìm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Left;
            txtSearch.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearch.Location = new Point(924, 9);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(263, 45);
            txtSearch.TabIndex = 5;
            // 
            // lblSearch
            // 
            lblSearch.Anchor = AnchorStyles.Right;
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearch.Location = new Point(770, 12);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(148, 38);
            lblSearch.TabIndex = 4;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // cboFilterDiff
            // 
            cboFilterDiff.Anchor = AnchorStyles.Left;
            cboFilterDiff.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboFilterDiff.FormattingEnabled = true;
            cboFilterDiff.Location = new Point(609, 8);
            cboFilterDiff.Name = "cboFilterDiff";
            cboFilterDiff.Size = new Size(152, 46);
            cboFilterDiff.TabIndex = 3;
            // 
            // lblFilterDiff
            // 
            lblFilterDiff.Anchor = AnchorStyles.Right;
            lblFilterDiff.AutoSize = true;
            lblFilterDiff.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterDiff.Location = new Point(482, 12);
            lblFilterDiff.Name = "lblFilterDiff";
            lblFilterDiff.Size = new Size(121, 38);
            lblFilterDiff.TabIndex = 2;
            lblFilterDiff.Text = "Độ khó:";
            // 
            // lblFilterImage
            // 
            lblFilterImage.Anchor = AnchorStyles.Right;
            lblFilterImage.AutoSize = true;
            lblFilterImage.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFilterImage.Location = new Point(4, 12);
            lblFilterImage.Name = "lblFilterImage";
            lblFilterImage.Size = new Size(145, 38);
            lblFilterImage.TabIndex = 0;
            lblFilterImage.Text = "Hình ảnh:";
            // 
            // dgvWords
            // 
            dgvWords.AllowUserToDeleteRows = false;
            dgvWords.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = SystemColors.ActiveCaption;
            dgvWords.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvWords.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvWords.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWords.ColumnHeadersHeight = 34;
            dgvWords.Columns.AddRange(new DataGridViewColumn[] { colID, colTopic, colEnglish, colPhonetic, colVietnamese, colDifficulty, colEmojiIcon, colErase, colImagePath });
            dgvWords.EnableHeadersVisualStyles = false;
            dgvWords.Location = new Point(4, 3);
            dgvWords.MultiSelect = false;
            dgvWords.Name = "dgvWords";
            dgvWords.ReadOnly = true;
            dgvWords.RowHeadersVisible = false;
            dgvWords.RowHeadersWidth = 62;
            dgvWords.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvWords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWords.Size = new Size(1417, 537);
            dgvWords.TabIndex = 2;
            // 
            // colID
            // 
            colID.FillWeight = 50F;
            colID.HeaderText = "ID";
            colID.MinimumWidth = 8;
            colID.Name = "colID";
            colID.ReadOnly = true;
            // 
            // colTopic
            // 
            colTopic.HeaderText = "Chủ đề";
            colTopic.MinimumWidth = 8;
            colTopic.Name = "colTopic";
            colTopic.ReadOnly = true;
            // 
            // colEnglish
            // 
            colEnglish.HeaderText = "Tiếng Anh";
            colEnglish.MinimumWidth = 8;
            colEnglish.Name = "colEnglish";
            colEnglish.ReadOnly = true;
            // 
            // colPhonetic
            // 
            colPhonetic.HeaderText = "Phiên âm";
            colPhonetic.MinimumWidth = 8;
            colPhonetic.Name = "colPhonetic";
            colPhonetic.ReadOnly = true;
            // 
            // colVietnamese
            // 
            colVietnamese.HeaderText = "Tiếng Việt";
            colVietnamese.MinimumWidth = 8;
            colVietnamese.Name = "colVietnamese";
            colVietnamese.ReadOnly = true;
            // 
            // colDifficulty
            // 
            colDifficulty.HeaderText = "Độ khó";
            colDifficulty.MinimumWidth = 8;
            colDifficulty.Name = "colDifficulty";
            colDifficulty.ReadOnly = true;
            // 
            // colEmojiIcon
            // 
            colEmojiIcon.HeaderText = "Emoji";
            colEmojiIcon.MinimumWidth = 8;
            colEmojiIcon.Name = "colEmojiIcon";
            colEmojiIcon.ReadOnly = true;
            // 
            // colErase
            // 
            colErase.HeaderText = "Xoá";
            colErase.MinimumWidth = 8;
            colErase.Name = "colErase";
            colErase.ReadOnly = true;
            colErase.Resizable = DataGridViewTriState.True;
            colErase.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // colImagePath
            // 
            colImagePath.DataPropertyName = "ImagePath";
            colImagePath.HeaderText = "Ảnh";
            colImagePath.MinimumWidth = 8;
            colImagePath.Name = "colImagePath";
            colImagePath.ReadOnly = true;
            colImagePath.Visible = false;
            // 
            // pnlInput
            // 
            pnlInput.BackColor = SystemColors.GradientInactiveCaption;
            pnlInput.Controls.Add(picPreview);
            pnlInput.Controls.Add(btnPickImage);
            pnlInput.Controls.Add(label1);
            pnlInput.Controls.Add(btnBack);
            pnlInput.Controls.Add(btnDelete);
            pnlInput.Controls.Add(btnUpdate);
            pnlInput.Controls.Add(btnAdd);
            pnlInput.Controls.Add(txtHint);
            pnlInput.Controls.Add(lblInputHint);
            pnlInput.Controls.Add(txtPhonetic);
            pnlInput.Controls.Add(txtVietnamese);
            pnlInput.Controls.Add(txtEnglish);
            pnlInput.Controls.Add(lblInputVI);
            pnlInput.Controls.Add(cboInputDiff);
            pnlInput.Controls.Add(lblInputDiff);
            pnlInput.Controls.Add(lblPhonetic);
            pnlInput.Controls.Add(lblInputEN);
            pnlInput.Controls.Add(cboInputTopic);
            pnlInput.Controls.Add(lblInputTopic);
            pnlInput.Controls.Add(lblThemSua);
            pnlInput.Dock = DockStyle.Fill;
            pnlInput.Location = new Point(0, 0);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(1405, 224);
            pnlInput.TabIndex = 3;
            // 
            // picPreview
            // 
            picPreview.BorderStyle = BorderStyle.FixedSingle;
            picPreview.Location = new Point(970, 12);
            picPreview.Name = "picPreview";
            picPreview.Size = new Size(230, 204);
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            picPreview.TabIndex = 20;
            picPreview.TabStop = false;
            // 
            // btnPickImage
            // 
            btnPickImage.Location = new Point(464, 169);
            btnPickImage.Name = "btnPickImage";
            btnPickImage.Size = new Size(149, 47);
            btnPickImage.TabIndex = 19;
            btnPickImage.Text = "Chọn ảnh";
            btnPickImage.UseVisualStyleBackColor = true;
            btnPickImage.Click += BtnPickImage_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(8, 141);
            label1.Name = "label1";
            label1.Size = new Size(247, 25);
            label1.TabIndex = 4;
            label1.Text = "Quản lý từ vững | Tổng:     từ\r\n\r\n";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(789, 169);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(135, 47);
            btnBack.TabIndex = 18;
            btnBack.Text = "Quay lại";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(309, 169);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(135, 47);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click_1;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(158, 169);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(135, 47);
            btnUpdate.TabIndex = 16;
            btnUpdate.Text = "Lưu sửa";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(8, 169);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(135, 47);
            btnAdd.TabIndex = 15;
            btnAdd.Text = "+ Thêm mới\r\n";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // txtHint
            // 
            txtHint.Location = new Point(366, 106);
            txtHint.Name = "txtHint";
            txtHint.Size = new Size(558, 31);
            txtHint.TabIndex = 14;
            // 
            // lblInputHint
            // 
            lblInputHint.AutoSize = true;
            lblInputHint.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInputHint.Location = new Point(290, 106);
            lblInputHint.Name = "lblInputHint";
            lblInputHint.Size = new Size(70, 28);
            lblInputHint.TabIndex = 13;
            lblInputHint.Text = "Emoji:";
            // 
            // txtPhonetic
            // 
            txtPhonetic.Location = new Point(595, 54);
            txtPhonetic.Name = "txtPhonetic";
            txtPhonetic.Size = new Size(132, 31);
            txtPhonetic.TabIndex = 12;
            // 
            // txtVietnamese
            // 
            txtVietnamese.Location = new Point(122, 106);
            txtVietnamese.Name = "txtVietnamese";
            txtVietnamese.Size = new Size(135, 31);
            txtVietnamese.TabIndex = 11;
            // 
            // txtEnglish
            // 
            txtEnglish.Location = new Point(352, 56);
            txtEnglish.Name = "txtEnglish";
            txtEnglish.Size = new Size(135, 31);
            txtEnglish.TabIndex = 10;
            // 
            // lblInputVI
            // 
            lblInputVI.AutoSize = true;
            lblInputVI.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInputVI.Location = new Point(8, 106);
            lblInputVI.Name = "lblInputVI";
            lblInputVI.Size = new Size(114, 28);
            lblInputVI.TabIndex = 9;
            lblInputVI.Text = "Tiếng Việt:";
            // 
            // cboInputDiff
            // 
            cboInputDiff.FormattingEnabled = true;
            cboInputDiff.Location = new Point(818, 49);
            cboInputDiff.Name = "cboInputDiff";
            cboInputDiff.Size = new Size(106, 33);
            cboInputDiff.TabIndex = 8;
            // 
            // lblInputDiff
            // 
            lblInputDiff.AutoSize = true;
            lblInputDiff.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInputDiff.Location = new Point(733, 54);
            lblInputDiff.Name = "lblInputDiff";
            lblInputDiff.Size = new Size(85, 28);
            lblInputDiff.TabIndex = 7;
            lblInputDiff.Text = "Độ khó:";
            // 
            // lblPhonetic
            // 
            lblPhonetic.AutoSize = true;
            lblPhonetic.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPhonetic.Location = new Point(493, 54);
            lblPhonetic.Name = "lblPhonetic";
            lblPhonetic.Size = new Size(105, 28);
            lblPhonetic.TabIndex = 5;
            lblPhonetic.Text = "Phiên âm:";
            // 
            // lblInputEN
            // 
            lblInputEN.AutoSize = true;
            lblInputEN.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInputEN.Location = new Point(243, 54);
            lblInputEN.Name = "lblInputEN";
            lblInputEN.Size = new Size(114, 28);
            lblInputEN.TabIndex = 3;
            lblInputEN.Text = "Tiếng Anh:";
            // 
            // cboInputTopic
            // 
            cboInputTopic.FormattingEnabled = true;
            cboInputTopic.Location = new Point(93, 54);
            cboInputTopic.Name = "cboInputTopic";
            cboInputTopic.Size = new Size(135, 33);
            cboInputTopic.TabIndex = 2;
            // 
            // lblInputTopic
            // 
            lblInputTopic.AutoSize = true;
            lblInputTopic.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblInputTopic.Location = new Point(8, 54);
            lblInputTopic.Name = "lblInputTopic";
            lblInputTopic.Size = new Size(83, 28);
            lblInputTopic.TabIndex = 1;
            lblInputTopic.Text = "Chủ đề:";
            // 
            // lblThemSua
            // 
            lblThemSua.AutoSize = true;
            lblThemSua.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblThemSua.Location = new Point(8, 12);
            lblThemSua.Name = "lblThemSua";
            lblThemSua.Size = new Size(249, 30);
            lblThemSua.TabIndex = 0;
            lblThemSua.Text = "THÊM / SỬA TỪ VỮNG";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 11;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.7268877F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.0063515F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.033169F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.1503172F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.0797462F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.9837685F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.94223833F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.870036F));
            tableLayoutPanel1.Controls.Add(cboFilterImage, 3, 0);
            tableLayoutPanel1.Controls.Add(cboFilterDiff, 6, 0);
            tableLayoutPanel1.Controls.Add(txtSearch, 8, 0);
            tableLayoutPanel1.Controls.Add(btnShowAll, 10, 0);
            tableLayoutPanel1.Controls.Add(btnSearch, 9, 0);
            tableLayoutPanel1.Controls.Add(lblFilterDiff, 5, 0);
            tableLayoutPanel1.Controls.Add(lblSearch, 7, 0);
            tableLayoutPanel1.Controls.Add(lblFilterImage, 1, 0);
            tableLayoutPanel1.Location = new Point(352, 91);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1417, 63);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // cboFilterImage
            // 
            cboFilterImage.Anchor = AnchorStyles.Left;
            cboFilterImage.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboFilterImage.FormattingEnabled = true;
            cboFilterImage.IntegralHeight = false;
            cboFilterImage.Location = new Point(155, 8);
            cboFilterImage.Name = "cboFilterImage";
            cboFilterImage.Size = new Size(320, 46);
            cboFilterImage.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(dgvWords);
            panel1.Location = new Point(352, 154);
            panel1.Name = "panel1";
            panel1.Size = new Size(1417, 583);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // panel3
            // 
            panel3.Controls.Add(cboPageSize);
            panel3.Controls.Add(lblNext);
            panel3.Controls.Add(lblPage);
            panel3.Controls.Add(lblPrev);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 541);
            panel3.Name = "panel3";
            panel3.Size = new Size(1417, 42);
            panel3.TabIndex = 3;
            // 
            // cboPageSize
            // 
            cboPageSize.Anchor = AnchorStyles.None;
            cboPageSize.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboPageSize.FormattingEnabled = true;
            cboPageSize.Items.AddRange(new object[] { "\"50\", \"200\", \"300\", \"400\", \"500\"" });
            cboPageSize.Location = new Point(799, 3);
            cboPageSize.Name = "cboPageSize";
            cboPageSize.Size = new Size(109, 29);
            cboPageSize.TabIndex = 3;
            // 
            // lblNext
            // 
            lblNext.Anchor = AnchorStyles.None;
            lblNext.AutoSize = true;
            lblNext.Location = new Point(746, 2);
            lblNext.Name = "lblNext";
            lblNext.Size = new Size(34, 25);
            lblNext.TabIndex = 2;
            lblNext.Text = "[>]";
            lblNext.Click += lblNext_Click;
            // 
            // lblPage
            // 
            lblPage.Anchor = AnchorStyles.None;
            lblPage.AutoSize = true;
            lblPage.Location = new Point(609, 2);
            lblPage.Name = "lblPage";
            lblPage.Size = new Size(107, 25);
            lblPage.TabIndex = 1;
            lblPage.Text = "[Trang 1 / 4]";
            // 
            // lblPrev
            // 
            lblPrev.Anchor = AnchorStyles.None;
            lblPrev.AutoSize = true;
            lblPrev.Location = new Point(545, 2);
            lblPrev.Name = "lblPrev";
            lblPrev.Size = new Size(34, 25);
            lblPrev.TabIndex = 0;
            lblPrev.Text = "[<]";
            lblPrev.Click += lblPrev_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.BackColor = Color.SteelBlue;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(lblTitle, 0, 0);
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(1769, 85);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.None;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(744, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(280, 35);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "QUẢN LÝ TỪ VỮNG";
            // 
            // treeTopics
            // 
            treeTopics.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeTopics.BackColor = Color.White;
            treeTopics.BorderStyle = BorderStyle.None;
            treeTopics.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            treeTopics.ForeColor = Color.SteelBlue;
            treeTopics.FullRowSelect = true;
            treeTopics.HideSelection = false;
            treeTopics.HotTracking = true;
            treeTopics.Indent = 20;
            treeTopics.ItemHeight = 45;
            treeTopics.Location = new Point(2, 91);
            treeTopics.Name = "treeTopics";
            treeTopics.ShowLines = false;
            treeTopics.Size = new Size(339, 864);
            treeTopics.TabIndex = 7;
            treeTopics.AfterSelect += TreeTopics_AfterSelect;
            treeTopics.MouseDown += treeTopics_MouseDown;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(pnlInput);
            panel2.Location = new Point(352, 731);
            panel2.Name = "panel2";
            panel2.Size = new Size(1405, 224);
            panel2.TabIndex = 8;
            // 
            // frmAdmin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1769, 967);
            Controls.Add(panel2);
            Controls.Add(treeTopics);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Name = "frmAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý từ vững";
            Load += frmAdmin_Load;
            ((System.ComponentModel.ISupportInitialize)dgvWords).EndInit();
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picPreview).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtSearch;
        private Label lblSearch;
        private ComboBox cboFilterDiff;
        private Label lblFilterDiff;
        private Label lblFilterImage;
        private Button btnShowAll;
        private Button btnSearch;
        private DataGridView dgvWords;
        private Panel pnlInput;
        private ComboBox cboInputDiff;
        private Label lblInputDiff;
        private Label lblPhonetic;
        private Label lblInputEN;
        private ComboBox cboInputTopic;
        private Label lblInputTopic;
        private Label lblThemSua;
        private TextBox txtPhonetic;
        private TextBox txtVietnamese;
        private TextBox txtEnglish;
        private Label lblInputVI;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private TextBox txtHint;
        private Label lblInputHint;
        private Button btnBack;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label lblTitle;
        private Button btnPickImage;
        private Panel panel2;
        public TreeView treeTopics;
        private ComboBox cboFilterImage;
    private PictureBox picPreview;
        private Panel panel3;
        private Label lblNext;
        private Label lblPage;
        private Label lblPrev;
        private ComboBox cboPageSize;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colTopic;
        private DataGridViewTextBoxColumn colEnglish;
        private DataGridViewTextBoxColumn colPhonetic;
        private DataGridViewTextBoxColumn colVietnamese;
        private DataGridViewTextBoxColumn colDifficulty;
        private DataGridViewTextBoxColumn colEmojiIcon;
        private DataGridViewButtonColumn colErase;
        private DataGridViewTextBoxColumn colImagePath;
    }
}
