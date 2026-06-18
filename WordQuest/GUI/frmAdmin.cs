using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmAdmin : Form
    {
        private readonly Form _topics;
        private readonly WordBUS _wordBUS = new();
        private readonly TopicBUS _topicBUS = new();

        private int _selectedWordID = -1;
        private int _selectedImageID = -1;
        private readonly ImageBUS _imageBUS = new();

        private int _currentTopicFilter = -1;
        private string _currentFilterImage = "Tất cả";
        private string _currentFilterDiff = "Tất cả";
        private string _currentSearchKeyword = "";

        // Phân trang
        private List<WordDTO> _allWords = new();
        private int _currentPage = 1;
        private int _pageSize = 150;

        public frmAdmin(Form topics)
        {
            InitializeComponent();
            _topics = topics;
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            SetupControls();
            LoadTopicsToComboBox();
            LoadTopicTree();
            LoadWords();
        }

        private void SetupControls()
        {
            cboPageSize.Items.AddRange(new[] { "50", "200", "300", "400", "500" });
            cboPageSize.SelectedItem = "10";
            cboPageSize.SelectedIndexChanged += CboPageSize_SelectedIndexChanged;

            cboFilterImage.Items.Clear();
            cboFilterImage.Items.AddRange(new[] { "Tất cả", "Có ảnh", "Chưa có ảnh" });
            cboFilterImage.SelectedIndex = 0;
            cboFilterImage.SelectedIndexChanged += CboFilterImage_SelectedIndexChanged;

            cboFilterDiff.Items.Clear();
            cboFilterDiff.Items.AddRange(new[] { "Tất cả", "1", "2", "3" });
            cboFilterDiff.SelectedIndex = 0;
            cboFilterDiff.SelectedIndexChanged += CboFilterDiff_SelectedIndexChanged;

            cboInputDiff.Items.Clear();
            cboInputDiff.Items.AddRange(new[] { "1", "2", "3" });
            cboInputDiff.SelectedIndex = 0;

            dgvWords.CellClick += DgvWords_CellClick;

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSearch.Click += BtnSearch_Click;
            btnShowAll.Click += BtnShowAll_Click;
            btnBack.Click += BtnBack_Click;

            txtSearch.KeyDown += TxtSearch_KeyDown;

            var menu = new ContextMenuStrip();
            menu.Items.Add("Xuất danh sách từ vựng (CSV)", null, (s, e) => XuatCSV(dgvWords, "Words_Report.csv"));
            dgvWords.ContextMenuStrip = menu;
        }

        private void XuatCSV(DataGridView dgv, string defaultFileName)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = defaultFileName;
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new System.Text.StringBuilder();

                var headers = dgv.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.Visible && c.Name != "colDelete" && c.Name != "colImage")
                    .Select(c => $"\"{c.HeaderText}\"");
                sb.AppendLine(string.Join(",", headers));

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    var cells = dgv.Columns.Cast<DataGridViewColumn>()
                        .Where(c => c.Visible && c.Name != "colDelete" && c.Name != "colImage")
                        .Select(c =>
                        {
                            string val = row.Cells[c.Index].Value?.ToString() ?? "";
                            return $"\"{val.Replace("\"", "\"\"")}\"";
                        });
                    sb.AppendLine(string.Join(",", cells));
                }

                System.IO.File.WriteAllText(sfd.FileName, sb.ToString(), System.Text.Encoding.UTF8);
                MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Load Data

        private void LoadTopicsToComboBox()
        {
            try
            {
                var dt = _topicBUS.LayTatCaChuDe();
                cboInputTopic.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    cboInputTopic.Items.Add(new ComboItem(
                        Convert.ToInt32(row["TopicID"]),
                        row["TopicName"].ToString() ?? ""
                    ));
                }
                if (cboInputTopic.Items.Count > 0) cboInputTopic.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTopicTree()
        {
            try
            {
                treeTopics.Nodes.Clear();
                treeTopics.AfterSelect -= TreeTopics_AfterSelect;

                var allNode = new TreeNode("📚 Tất cả từ vựng") { Tag = -1 };
                treeTopics.Nodes.Add(allNode);

                var dt = _topicBUS.LayTatCaChuDe();

                // Nhóm các topic theo ParentID
                var topics = dt.AsEnumerable().Select(row => new
                {
                    TopicID = Convert.ToInt32(row["TopicID"]),
                    TopicName = row["TopicName"].ToString() ?? "",
                    TopicIcon = row["TopicIcon"].ToString() ?? "",
                    ParentID = row["ParentID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ParentID"])
                }).ToList();

                foreach (var topic in topics.Where(t => t.ParentID == 0))
                {
                    var parentNode = new TreeNode($"{topic.TopicIcon} {topic.TopicName}") { Tag = topic.TopicID };
                    treeTopics.Nodes.Add(parentNode);

                    // Thêm topic con
                    foreach (var child in topics.Where(t => t.ParentID == topic.TopicID))
                    {
                        var childNode = new TreeNode($"{child.TopicIcon} {child.TopicName}") { Tag = child.TopicID };
                        parentNode.Nodes.Add(childNode);
                    }
                }

                treeTopics.ExpandAll();
                treeTopics.AfterSelect += TreeTopics_AfterSelect;
                treeTopics.SelectedNode = allNode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load cây chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWords()
        {
            var filter = new WordFilterDTO
            {
                TopicID = _currentTopicFilter,
                Difficulty = _currentFilterDiff,
                ImageFilter = _currentFilterImage,
                Keyword = _currentSearchKeyword
            };

            _allWords = _wordBUS.LayDanhSachTu(filter);
            _currentPage = 1;
            RenderPage();
        }

        private void LoadCapNhat1Dong(int wordID)
        {
            // Tìm dòng có WordID tương ứng
            foreach (DataGridViewRow row in dgvWords.Rows)
            {
                if (row.IsNewRow) continue;

                if (Convert.ToInt32(row.Cells["colID"].Value) == wordID)
                {
                    var updatedWord = _wordBUS.LayTuTheoID(wordID);

                    if (updatedWord != null)
                    {
                        row.Cells["colTopic"].Value = updatedWord.TopicName;
                        row.Cells["colEnglish"].Value = updatedWord.EnglishWord;
                        row.Cells["colPhonetic"].Value = updatedWord.Phonetic;
                        row.Cells["colVietnamese"].Value = updatedWord.VietnameseMeaning;
                        row.Cells["colDifficulty"].Value = updatedWord.DifficultyLevel.ToString();
                        row.Cells["colEmojiIcon"].Value = updatedWord.EmojiIcon;
                        row.Cells["colImagePath"].Value = updatedWord.ImagePath ?? "";

                        ToMauDoKho(row, updatedWord.DifficultyLevel.ToString());
                    }
                    return;
                }
            }
            LoadWords();
        }

        private void LoadThem1Dong(int newWordID)
        {
            var newWord = _wordBUS.LayTuTheoID(newWordID);
            if (newWord == null)
            {
                LoadWords();
                return;
            }

            int rowIndex = dgvWords.Rows.Add(
                newWord.WordID,
                newWord.TopicName,
                newWord.EnglishWord,
                newWord.Phonetic,
                newWord.VietnameseMeaning,
                newWord.DifficultyLevel.ToString(),
                newWord.EmojiIcon,
                "🗑 Xóa",
                newWord.ImagePath ?? ""
            );
            ToMauDoKho(dgvWords.Rows[rowIndex], newWord.DifficultyLevel.ToString());
            // Đồng bộ danh sách _allWords
            _allWords.Add(newWord);
        }

        private void LoadXoa1Dong(int wordID)
        {
            for (int i = 0; i < dgvWords.Rows.Count; i++)
            {
                var row = dgvWords.Rows[i];
                if (row.IsNewRow) continue;

                if (Convert.ToInt32(row.Cells["colID"].Value) == wordID)
                {
                    dgvWords.Rows.RemoveAt(i);
                    break;
                }
            }

            // Đồng bộ danh sách _allWords
            var wordToRemove = _allWords.FirstOrDefault(w => w.WordID == wordID);
            if (wordToRemove != null)
                _allWords.Remove(wordToRemove);
        }

        private void RenderPage()
        {
            dgvWords.Rows.Clear();

            int totalPages = Math.Max(1, (int)Math.Ceiling(_allWords.Count / (double)_pageSize));
            _currentPage = Math.Clamp(_currentPage, 1, totalPages);

            var pageData = _allWords
                .Skip((_currentPage - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            foreach (var word in pageData)
            {
                int rowIndex = dgvWords.Rows.Add(
                    word.WordID, word.TopicName, word.EnglishWord,
                    word.Phonetic, word.VietnameseMeaning,
                    word.DifficultyLevel.ToString(), word.EmojiIcon,
                    "🗑 Xóa", word.ImagePath
                );
                ToMauDoKho(dgvWords.Rows[rowIndex], word.DifficultyLevel.ToString());
            }

            lblPage.Text = $"[Trang {_currentPage} / {totalPages}]";
            lblPrev.Enabled = _currentPage > 1;
            lblNext.Enabled = _currentPage < totalPages;

            label1.Text = $"Quản lý từ vựng | {_allWords.Count} từ | Trang {_currentPage}/{totalPages}";
        }

        private void ToMauDoKho(DataGridViewRow row, string diff)
        {
            var cell = row.Cells["colDifficulty"];
            switch (diff)
            {
                case "1":
                    cell.Style.BackColor = Color.FromArgb(200, 230, 201);
                    cell.Style.ForeColor = Color.FromArgb(27, 94, 32);
                    break;
                case "2":
                    cell.Style.BackColor = Color.FromArgb(255, 249, 196);
                    cell.Style.ForeColor = Color.FromArgb(230, 81, 0);
                    break;
                case "3":
                    cell.Style.BackColor = Color.FromArgb(255, 205, 210);
                    cell.Style.ForeColor = Color.FromArgb(183, 28, 28);
                    break;
            }
            cell.Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            cell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        #endregion

        #region Filter Events

        private void TreeTopics_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node?.Tag == null) return;
            _currentTopicFilter = Convert.ToInt32(e.Node.Tag);
            LoadWords();
        }

        private void CboFilterImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentFilterImage = cboFilterImage.SelectedItem?.ToString() ?? "Tất cả";
            LoadWords();
        }

        private void CboFilterDiff_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentFilterDiff = cboFilterDiff.SelectedItem?.ToString() ?? "Tất cả";
            LoadWords();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _currentSearchKeyword = txtSearch.Text.Trim();
                LoadWords();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            _currentSearchKeyword = txtSearch.Text.Trim();
            LoadWords();
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            // Reset tất cả filter
            cboFilterImage.SelectedIndex = 0;
            cboFilterDiff.SelectedIndex = 0;
            txtSearch.Text = "";
            _currentSearchKeyword = "";
            _currentFilterImage = "Tất cả";
            _currentFilterDiff = "Tất cả";
            _currentTopicFilter = -1;

            // Reset TreeView về node "Tất cả"
            foreach (TreeNode node in treeTopics.Nodes)
            {
                if (Convert.ToInt32(node.Tag) == -1)
                {
                    treeTopics.SelectedNode = node;
                    break;
                }
            }

            LoadWords();
            ClearInput();
        }

        #endregion

        #region Grid Events

        private void DgvWords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Xóa từ
            if (dgvWords.Columns[e.ColumnIndex].Name == "colErase")
            {
                int wordID = Convert.ToInt32(dgvWords.Rows[e.RowIndex].Cells["colID"].Value);
                XoaTu(wordID);
                return;
            }

            // Load lên form sửa
            LoadFormSua(e.RowIndex);
        }

        private void LoadFormSua(int rowIndex)
        {
            var row = dgvWords.Rows[rowIndex];
            _selectedWordID = Convert.ToInt32(row.Cells["colID"].Value);

            // Chọn chủ đề
            string topicName = row.Cells["colTopic"].Value?.ToString() ?? "";
            foreach (ComboItem item in cboInputTopic.Items)
            {
                if (item.Name == topicName)
                {
                    cboInputTopic.SelectedItem = item;
                    break;
                }
            }

            txtEnglish.Text = row.Cells["colEnglish"].Value?.ToString() ?? "";
            txtPhonetic.Text = row.Cells["colPhonetic"].Value?.ToString() ?? "";
            txtVietnamese.Text = row.Cells["colVietnamese"].Value?.ToString() ?? "";
            txtHint.Text = row.Cells["colEmojiIcon"].Value?.ToString() ?? "";

            string diff = row.Cells["colDifficulty"].Value?.ToString() ?? "1";
            int diffIdx = cboInputDiff.Items.IndexOf(diff);
            if (diffIdx >= 0) cboInputDiff.SelectedIndex = diffIdx;

            var word = _allWords.FirstOrDefault(w => w.WordID == _selectedWordID);
            _selectedImageID = word?.ImageID ?? -1;

            if (_selectedImageID > 0)
            {
                LoadAnhPic(_imageBUS.LayHinh(_selectedImageID));
            }
            else
            {
                LoadAnhPic(null);
            }

            // Highlight dòng đang chọn
            dgvWords.ClearSelection();
            dgvWords.Rows[rowIndex].Selected = true;

            SuaXoaKhiChon(true);
        }

        private void SuaXoaKhiChon(bool enabled)
        {
            btnUpdate.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }

        #endregion

        #region CRUD Operations

        private bool KiemTraDuLieu()
        {
            if (cboInputTopic.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn chủ đề!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEnglish.Text))
            {
                MessageBox.Show("Vui lòng nhập từ tiếng Anh!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtVietnamese.Text))
            {
                MessageBox.Show("Vui lòng nhập nghĩa tiếng Việt!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!KiemTraDuLieu()) return;

            int topicID = ((ComboItem)cboInputTopic.SelectedItem).ID;
            string englishWord = txtEnglish.Text.Trim();

            // Kiểm tra trùng lặp
            bool isDuplicate = _wordBUS.KiemTraTuTiengAnhTrung(topicID, englishWord, 0);
            if (isDuplicate)
            {
                MessageBox.Show($"Từ '{englishWord}' đã tồn tại trong chủ đề này!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var word = new WordDTO
            {
                TopicID = topicID,
                EnglishWord = englishWord,
                Phonetic = txtPhonetic.Text.Trim(),
                VietnameseMeaning = txtVietnamese.Text.Trim(),
                DifficultyLevel = int.Parse(cboInputDiff.SelectedItem.ToString()!),
                EmojiIcon = txtHint.Text.Trim(),
                ImageID = _selectedImageID > 0 ? _selectedImageID : null
            };

            try
            {
                int newWordID = _wordBUS.ThemTu(word);
                LoadThem1Dong(newWordID);

                ClearInput();
                ConTroDong(newWordID);   // ← Nhảy đến dòng vừa thêm
                MessageBox.Show("✅ Thêm từ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedWordID < 0)
            {
                MessageBox.Show("Vui lòng chọn từ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!KiemTraDuLieu()) return;

            int topicID = ((ComboItem)cboInputTopic.SelectedItem).ID;
            string englishWord = txtEnglish.Text.Trim();

            // Kiểm tra trùng lặp (trừ chính nó)
            bool isDuplicate = _wordBUS.KiemTraTuTiengAnhTrung(topicID, englishWord, _selectedWordID);
            if (isDuplicate)
            {
                MessageBox.Show($"Từ '{englishWord}' đã tồn tại trong chủ đề này!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var word = new WordDTO
            {
                WordID = _selectedWordID,
                TopicID = topicID,
                EnglishWord = englishWord,
                Phonetic = txtPhonetic.Text.Trim(),
                VietnameseMeaning = txtVietnamese.Text.Trim(),
                DifficultyLevel = int.Parse(cboInputDiff.SelectedItem.ToString()!),
                EmojiIcon = txtHint.Text.Trim(),
                ImageID = _selectedImageID > 0 ? _selectedImageID : null
            };

            try
            {
                int updatedID = _selectedWordID;
                _wordBUS.CapNhatTu(word);
                LoadCapNhat1Dong(_selectedWordID);

                ConTroDong(updatedID);
                MessageBox.Show("✅ Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1) { _currentPage--; RenderPage(); }
        }

        private void lblNext_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling(_allWords.Count / (double)_pageSize);
            if (_currentPage < totalPages) { _currentPage++; RenderPage(); }
        }
        private void XoaTu(int wordID)
        {
            var confirm = MessageBox.Show("Xóa từ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _wordBUS.XoaTu(wordID);
                LoadXoa1Dong(wordID);

                if (_selectedWordID == wordID) ClearInput();
                MessageBox.Show("✅ Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cboPageSize.SelectedItem?.ToString(), out int size))
            {
                _pageSize = size;
                _currentPage = 1;
                RenderPage();
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e) => XoaTu(_selectedWordID);

        private void ClearInput()
        {
            _selectedWordID = -1;
            if (cboInputTopic.Items.Count > 0) cboInputTopic.SelectedIndex = 0;
            txtEnglish.Text = "";
            txtPhonetic.Text = "";
            txtVietnamese.Text = "";
            txtHint.Text = "";
            if (cboInputDiff.Items.Count > 0) cboInputDiff.SelectedIndex = 0;
            _selectedImageID = -1;
            LoadAnhPic(null);
            SuaXoaKhiChon(false);
            dgvWords.ClearSelection();
        }

        #endregion

        #region Active Row Helper

        /// Sau khi LoadWords() / RenderPage(), tìm dòng có WordID tương ứng và
        /// scroll + highlight đúng dòng đó. Nếu dòng ở trang khác thì tự nhảy trang.
        private void ConTroDong(int wordID)
        {
            if (wordID <= 0) return;

            // --- Bước 1: Kiểm tra dòng có trên trang hiện tại không ---
            foreach (DataGridViewRow row in dgvWords.Rows)
            {
                if (Convert.ToInt32(row.Cells["colID"].Value) == wordID)
                {
                    HighlightRow(row);
                    return;
                }
            }

            // --- Bước 2: Tìm trong _allWords để biết dòng ở trang nào ---
            int globalIndex = _allWords.FindIndex(w => w.WordID == wordID);
            if (globalIndex < 0) return;   // không tìm thấy trong data

            int targetPage = globalIndex / _pageSize + 1;
            if (targetPage != _currentPage)
            {
                _currentPage = targetPage;
                RenderPage();
            }

            // --- Bước 3: Tìm lại trên trang mới ---
            foreach (DataGridViewRow row in dgvWords.Rows)
            {
                if (Convert.ToInt32(row.Cells["colID"].Value) == wordID)
                {
                    HighlightRow(row);
                    return;
                }
            }
        }

        /// Bôi đậm dòng, cuộn màn hình đến đúng vị trí và load thông tin lên form sửa.
        private void HighlightRow(DataGridViewRow row)
        {
            dgvWords.ClearSelection();
            row.Selected = true;

            // Scroll đến dòng (tránh IndexOutOfRange)
            if (row.Index >= 0 && row.Index < dgvWords.Rows.Count)
                dgvWords.FirstDisplayedScrollingRowIndex = row.Index;

            // Đặt CurrentCell để bàn phím cũng hoạt động đúng
            var targetCell = row.Cells["colEnglish"];
            if (targetCell != null)
                dgvWords.CurrentCell = targetCell;

            // Load thông tin lên form nhập liệu
            LoadFormSua(row.Index);
        }

        #endregion

        #region Image Handling

        private void LoadAnhPic(Image newImage)
        {
            var old = picPreview.Image;
            picPreview.Image = newImage;
            picPreview.SizeMode = PictureBoxSizeMode.Zoom;
            old?.Dispose();
        }

        private void BtnPickImage_Click(object sender, EventArgs e)
        {
            using var frm = new frmImage();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                _selectedImageID = frm.SelectedImageID;
                if (_selectedImageID > 0)
                {
                    LoadAnhPic(_imageBUS.LayHinh(_selectedImageID));
                }
                else
                {
                    LoadAnhPic(null);
                }
            }
        }

        #endregion

        #region Navigation

        private void BtnBack_Click(object sender, EventArgs e)
        {
            _topics.Show();
            this.Close();
        }

        private void treeTopics_MouseDown(object sender, MouseEventArgs e)
        {
            var node = treeTopics.GetNodeAt(e.X, e.Y);
            if (node != null) treeTopics.SelectedNode = node;
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }
    }

    public class ComboItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ComboItem(int id, string name) { ID = id; Name = name; }
        public override string ToString() => Name;
    }
}
