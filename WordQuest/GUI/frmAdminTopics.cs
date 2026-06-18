using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmAdminTopics : Form
    {
        private readonly Form _topics;
        private readonly TopicBUS _topicBUS = new();
        private readonly LevelBUS _levelBUS = new();
        private readonly WordBUS _wordBUS = new();

        private int _selectedTopicID = -1;
        private int _selectedLevelID = -1;
        private bool _isRandomMode = true;
        private bool _isEditingLevel = false;

        public frmAdminTopics(Form topics)
        {
            InitializeComponent();
            _topics = topics;
        }

        private async void frmAdminTopics_Load(object sender, EventArgs e)
        {
            SetupUI();
            await LoadTopics();
            await LoadComboBoxes();
            SetLevelEditVisible(false);
        }

        private void SetupUI()
        {
            trvTopics.AfterSelect += TrvTopics_AfterSelect;

            btnThemChuDe.Click += BtnThemChuDe_Click;
            btnXoaChuDe.Click += BtnXoaChuDe_Click;
            btnSaveTopic.Click += BtnSaveTopic_Click;
            btnThemMucDo.Click += BtnThemMucDo_Click;
            btnApplyFilter.Click += BtnApplyFilter_Click;
            btnSaveLevel.Click += BtnSaveLevel_Click;
            btnCancelLevel.Click += BtnCancelLevel_Click;
            btnBack.Click += BtnBack_Click;
            rdoRandom.Click += RdoRandom_Click;
            rdoFixed.Click += RdoFixed_Click;
            dgvLevels.CellClick += DgvLevels_CellClick;
            clbWords.ItemCheck += ClbWords_ItemCheck;

        }

        private async Task LoadComboBoxes()
        {
            cboLevelDiff.Items.Clear();
            cboLevelDiff.Items.AddRange(new[] { "Dễ", "Trung bình", "Khó" });
            if (cboLevelDiff.Items.Count > 0) cboLevelDiff.SelectedIndex = 0;
        }

        //LOAD TOPIC TREE
        /// Nạp tất cả topic từ DB, dựng cây cha-con vào TreeView đệ quy.
        /// Sau khi load xong, chọn lại node có _selectedTopicID (nếu có).
        private async Task LoadTopics()
        {
            try
            {
                var dt = await Task.Run(() => _topicBUS.LayTatCaChuDe());

                trvTopics.AfterSelect -= TrvTopics_AfterSelect;
                trvTopics.BeginUpdate();
                trvTopics.Nodes.Clear();

                // Chuyển DataTable sang list ẩn danh để dễ thao tác
                var allTopics = dt.AsEnumerable().Select(row => new
                {
                    TopicID  = Convert.ToInt32(row["TopicID"]),
                    Name     = row["TopicName"].ToString() ?? "",
                    Icon     = row["TopicIcon"].ToString() ?? "",
                    ParentID = row["ParentID"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["ParentID"])
                }).ToList();

                foreach (var t in allTopics.Where(x => x.ParentID == null).OrderBy(x => x.Name))
                {
                    string pIcon = string.IsNullOrEmpty(t.Icon) ? "📁" : t.Icon;
                    var parentNode = new TreeNode($"{pIcon} {t.Name}") { Tag = t.TopicID };
                    trvTopics.Nodes.Add(parentNode);

                    foreach (var child in allTopics.Where(x => x.ParentID == t.TopicID).OrderBy(x => x.Name))
                    {
                        string cIcon = string.IsNullOrEmpty(child.Icon) ? "└" : child.Icon;
                        var childNode = new TreeNode($"  {cIcon} {child.Name}") { Tag = child.TopicID };
                        parentNode.Nodes.Add(childNode);
                    }
                }

                trvTopics.ExpandAll();
                trvTopics.EndUpdate();
                trvTopics.AfterSelect += TrvTopics_AfterSelect;

                await LoadParentComboBox(dt);

                // Khôi phục selection nếu đang chỉnh sửa
                if (_selectedTopicID > 0)
                    SelectTopicNodeByID(_selectedTopicID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// Điền vào cboParentTopic: mục đầu là "-- Không có (chủ đề gốc) --",
        /// sau đó là toàn bộ topic cha (ParentID IS NULL).
        private async Task LoadParentComboBox(DataTable dt = null)
        {
            if (dt == null)
                dt = await Task.Run(() => _topicBUS.LayTatCaChuDe());

            cboParentTopic.Items.Clear();
            cboParentTopic.Items.Add(new ComboItemTopic(0, "-- Không có (chủ đề gốc) --"));

            // Chỉ thêm topic cha (ParentID == null) để tránh đệ quy vòng
            foreach (DataRow row in dt.Rows)
            {
                if (row["ParentID"] == DBNull.Value)
                {
                    cboParentTopic.Items.Add(new ComboItemTopic(
                        Convert.ToInt32(row["TopicID"]),
                        row["TopicName"].ToString() ?? ""
                    ));
                }
            }
            cboParentTopic.SelectedIndex = 0;
        }

        //TREE VIEW SELECTIO
        private async void TrvTopics_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;

            _selectedTopicID = Convert.ToInt32(e.Node.Tag);
            await LoadTopicInfo(_selectedTopicID);
            await LoadLevels(_selectedTopicID);
            SetLevelEditVisible(false);
        }

        private async Task LoadTopicInfo(int topicID)
        {
            try
            {
                var topic = await Task.Run(() => _topicBUS.LayChuDeTheoID(topicID));
                if (topic != null)
                {
                    txtTopicName.Text = topic.TopicName;
                    txtTopicIcon.Text = topic.TopicIcon;
                    nudStarsToUnlock.Value = topic.StarsToUnlock;
                    lblTopicInfoTitle.Text = $"THÔNG TIN CHỦ ĐỀ: {topic.TopicName}";

                    // Chọn đúng ParentID trong ComboBox
                    int parentIDValue = topic.ParentID ?? 0;
                    foreach (ComboItemTopic item in cboParentTopic.Items)
                    {
                        if (item.ID == parentIDValue)
                        {
                            cboParentTopic.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load thông tin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadLevels(int topicID)
        {
            try
            {
                var dt = await Task.Run(() => _levelBUS.LayMucDoTheoChuDe(topicID));

                dgvLevels.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvLevels.Rows.Add(
                        row["LevelID"],
                        row["LevelName"],
                        row["DifficultyLevel"],
                        row["Mode"],
                        row["QuestionCount"],
                        "✏ Sửa",
                        "🗑 Xóa"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load màn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DgvLevels_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = dgvLevels.Columns[e.ColumnIndex].Name;
            int levelID = Convert.ToInt32(dgvLevels.Rows[e.RowIndex].Cells["colLevelID"].Value);

            if (colName == "btnEditLevel")
            {
                _selectedLevelID = levelID;
                _isEditingLevel = true;
                await LoadLevelToEdit(levelID);
                SetLevelEditVisible(true);
            }
            else if (colName == "btnXoaMucDo")
            {
                await XoaMucDo(levelID);
            }
        }

        private async Task LoadLevelToEdit(int levelID)
        {
            try
            {
                var level = await Task.Run(() => _levelBUS.LayMucDoTheoID(levelID));
                if (level != null)
                {
                    txtLevelName.Text = level.LevelName;
                    nudLevelCount.Value = level.QuestionCount;

                    int diffIndex = cboLevelDiff.Items.IndexOf(level.DifficultyLevel);
                    if (diffIndex >= 0) cboLevelDiff.SelectedIndex = diffIndex;

                    _isRandomMode = level.Mode == "Random";
                    SetModeStyle(_isRandomMode);
                    lblEditLevelTitle.Text = $"SỬA MỨC ĐỘ: {level.LevelName}";
                }

                if (!_isRandomMode)
                {
                    var fixedWords = await Task.Run(() => _levelBUS.LayTuCoDinhMucDo(levelID));
                    var checkedIDs = fixedWords.AsEnumerable().Select(r => Convert.ToInt32(r["WordID"])).ToHashSet();
                    await LoadWords(checkedIDs);
                }
                else
                {
                    await LoadWords(null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load màn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadWords(System.Collections.Generic.HashSet<int> checkedIDs = null)
        {
            if (_selectedTopicID < 0) return;

            try
            {
                var dt = await Task.Run(() => _wordBUS.LayTuTheoChuDe(_selectedTopicID));

                clbWords.Items.Clear();

                var diffs = new System.Collections.Generic.List<string>();
                if (chkFilterEasy.Checked) diffs.Add("1");
                if (chkFilterMedium.Checked) diffs.Add("2");
                if (chkFilterHard.Checked) diffs.Add("3");

                foreach (DataRow row in dt.Rows)
                {
                    string difficulty = row["DifficultyLevel"].ToString() ?? "";
                    if (diffs.Count > 0 && !diffs.Contains(difficulty)) continue;

                    string displayText = $"{row["EnglishWord"]} — {row["VietnameseMeaning"]} (Độ khó: {difficulty})";
                    int wordID = Convert.ToInt32(row["WordID"]);
                    bool isChecked = checkedIDs != null && checkedIDs.Contains(wordID);

                    clbWords.Items.Add(new ComboItemWord(wordID, displayText), isChecked);
                }

                CapNhatTuInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load từ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatTuInfo()
        {
            int total = clbWords.Items.Count;
            int checked_ = clbWords.CheckedItems.Count;
            lblEditLevelTitle.Text = _isEditingLevel
                ? $"SỬA MỨC ĐỘ  |  Từ khả dụng: {total}  |  Đã chọn: {checked_}"
                : $"THÊM MỨC ĐỘ  |  Từ khả dụng: {total}  |  Đã chọn: {checked_}";
        }

        private void ClbWords_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke(() => CapNhatTuInfo());
        }

        private void BtnThemMucDo_Click(object sender, EventArgs e)
        {
            if (_selectedTopicID < 0)
            {
                MessageBox.Show("Vui lòng chọn chủ đề trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedLevelID = -1;
            _isEditingLevel = false;
            ClearLevelForm();
            SetLevelEditVisible(true);
            lblEditLevelTitle.Text = "THÊM MỨC ĐỘ MỚI";

            chkFilterEasy.Checked = true;
            chkFilterMedium.Checked = true;
            chkFilterHard.Checked = true;
            _ = LoadWords(null);
        }

        private async void BtnApplyFilter_Click(object sender, EventArgs e)
        {
            if (_isRandomMode)
                await LoadWords(null);
            else
            {
                var fixedWords = _selectedLevelID > 0
                    ? await Task.Run(() => _levelBUS.LayTuCoDinhMucDo(_selectedLevelID))
                    : null;

                var checkedIDs = fixedWords?.AsEnumerable()
                    .Select(r => Convert.ToInt32(r["WordID"]))
                    .ToHashSet();

                await LoadWords(checkedIDs);
            }
        }

        private async void BtnSaveLevel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLevelName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên màn!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboLevelDiff.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn độ khó!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var level = new LevelDTO
                {
                    TopicID = _selectedTopicID,
                    LevelName = txtLevelName.Text.Trim(),
                    DifficultyLevel = cboLevelDiff.SelectedItem.ToString() ?? "Trung bình",
                    Mode = _isRandomMode ? "Random" : "Fixed",
                    QuestionCount = (int)nudLevelCount.Value
                };

                if (!_isEditingLevel)
                {
                    int maxLevelNum = dgvLevels.Rows.Count;
                    level.LevelNum = maxLevelNum + 1;
                    int newID = await Task.Run(() => _levelBUS.ThemMucDo(level));
                    _selectedLevelID = newID;
                }
                else
                {
                    level.LevelID = _selectedLevelID;
                    level.LevelNum = 0;
                    await Task.Run(() => _levelBUS.CapNhatMucDo(level));
                }

                if (!_isRandomMode)
                {
                    await Task.Run(() => _levelBUS.XoaTatCaTuMucDo(_selectedLevelID));
                    foreach (ComboItemWord item in clbWords.CheckedItems)
                    {
                        await Task.Run(() => _levelBUS.ThemTuVaoMucDo(_selectedLevelID, item.ID));
                    }
                }

                await LoadLevels(_selectedTopicID);
                SetLevelEditVisible(false);
                MessageBox.Show("Lưu màn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu màn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task XoaMucDo(int levelID)
        {
            var result = MessageBox.Show("Xóa màn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try
            {
                await Task.Run(() => _levelBUS.XoaMucDo(levelID));
                await LoadLevels(_selectedTopicID);
                SetLevelEditVisible(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa màn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TOPIC CRUD
        private void BtnThemChuDe_Click(object sender, EventArgs e)
        {
            txtTopicName.Text = "";
            txtTopicIcon.Text = "";
            nudStarsToUnlock.Value = 0;
            _selectedTopicID = -1;

            trvTopics.AfterSelect -= TrvTopics_AfterSelect;
            trvTopics.SelectedNode = null;
            trvTopics.AfterSelect += TrvTopics_AfterSelect;

            if (cboParentTopic.Items.Count > 0) cboParentTopic.SelectedIndex = 0; // parent

            lblTopicInfoTitle.Text = "THÊM CHỦ ĐỀ MỚI";
            txtTopicName.Focus();
        }

        private async void BtnXoaChuDe_Click(object sender, EventArgs e)
        {
            if (_selectedTopicID < 0)
            {
                MessageBox.Show("Vui lòng chọn chủ đề!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Xóa chủ đề \"{txtTopicName.Text}\"?\nTất cả dữ liệu liên quan sẽ bị xóa!",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                await Task.Run(() => _topicBUS.XoaChuDe(_selectedTopicID));
                _selectedTopicID = -1;
                await LoadTopics();
                dgvLevels.Rows.Clear();
                clbWords.Items.Clear();
                txtTopicName.Text = "";
                txtTopicIcon.Text = "";
                lblTopicInfoTitle.Text = "THÔNG TIN CHỦ ĐỀ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSaveTopic_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTopicName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chủ đề!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đọc ParentID từ ComboBox
            int? parentID = null;
            if (cboParentTopic.SelectedItem is ComboItemTopic parentItem && parentItem.ID != 0)
                parentID = parentItem.ID;

            if (_selectedTopicID > 0 && parentID == _selectedTopicID)
            {
                MessageBox.Show("Chủ đề không thể làm con của chính nó!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var topic = new TopicDTO
            {
                TopicName     = txtTopicName.Text.Trim(),
                TopicIcon     = txtTopicIcon.Text.Trim(),
                StarsToUnlock = (int)nudStarsToUnlock.Value,
                ParentID      = parentID
            };

            try
            {
                int savedTopicID;

                if (_selectedTopicID < 0)
                {
                    savedTopicID = await Task.Run(() => _topicBUS.ThemChuDe(topic));
                    MessageBox.Show("Lưu chủ đề thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    topic.TopicID = _selectedTopicID;
                    savedTopicID = _selectedTopicID;
                    await Task.Run(() => _topicBUS.CapNhatChuDe(topic));
                    MessageBox.Show("Cập nhật chủ đề thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Reload cây và chọn lại node vừa lưu
                _selectedTopicID = savedTopicID;
                await LoadTopics();
                SelectTopicNodeByID(savedTopicID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //HELPER: Chọn node trên TreeView theo TopicID
        /// Duyệt đệ quy toàn bộ TreeView để tìm và select node có Tag == topicID dễ
        private void SelectTopicNodeByID(int topicID)
        {
            TreeNode found = FindNodeByID(trvTopics.Nodes, topicID);
            if (found == null) return;

            trvTopics.AfterSelect -= TrvTopics_AfterSelect;
            trvTopics.SelectedNode = found;
            found.EnsureVisible();
            trvTopics.AfterSelect += TrvTopics_AfterSelect;
        }

        private TreeNode FindNodeByID(TreeNodeCollection nodes, int topicID)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Tag is int id && id == topicID)
                    return node;

                var found = FindNodeByID(node.Nodes, topicID);
                if (found != null) return found;
            }
            return null;
        }

        //MODE / UI HELPERS
        private void RdoRandom_Click(object sender, EventArgs e)
        {
            _isRandomMode = true;
            SetModeStyle(true);
        }

        private void RdoFixed_Click(object sender, EventArgs e)
        {
            _isRandomMode = false;
            SetModeStyle(false);
        }

        private void SetModeStyle(bool isRandom)
        {
            rdoRandom.BackColor = isRandom ? Color.FromArgb(255, 140, 0) : Color.FromArgb(225, 225, 225);
            rdoFixed.BackColor  = isRandom ? Color.FromArgb(225, 225, 225) : Color.FromArgb(255, 140, 0);
            clbWords.Enabled    = !isRandom;
        }

        private void SetLevelEditVisible(bool visible)
        {
            pnlEditLevel.Visible = visible;
        }

        private void ClearLevelForm()
        {
            txtLevelName.Text = "";
            if (cboLevelDiff.Items.Count > 0) cboLevelDiff.SelectedIndex = 0;
            nudLevelCount.Value = 15;
            _isRandomMode = true;
            SetModeStyle(true);
            chkFilterEasy.Checked = false;
            chkFilterMedium.Checked = false;
            chkFilterHard.Checked = false;
            clbWords.Items.Clear();
        }

        private void BtnCancelLevel_Click(object sender, EventArgs e)
        {
            SetLevelEditVisible(false);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            _topics.Show();
            this.Close();
        }

        //HELPER CLASSES
        private class ComboItemTopic
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public ComboItemTopic(int id, string name) { ID = id; Name = name; }
            public override string ToString() => Name;
        }

        private class ComboItemWord
        {
            public int ID { get; set; }
            public string Text { get; set; }
            public ComboItemWord(int id, string text) { ID = id; Text = text; }
            public override string ToString() => Text;
        }
    }
}
