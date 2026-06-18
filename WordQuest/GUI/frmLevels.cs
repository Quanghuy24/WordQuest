using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmLevels : Form
    {
        private readonly int _playerID;
        private readonly string _username;
        private readonly int _topicID;
        private readonly string _topicName;
        private readonly string _topicEmoji;
        private readonly bool _isAdmin;

        private readonly LevelBUS _levelBUS = new();
        private readonly PlayerProgressBUS _progressBUS = new();

        private DataTable _levelsTable;
        private int _currentLevelPage = 0;
        private const int LEVELS_PER_PAGE = 3;

        private Panel[] _levelPanels;
        private Button[] _btnPlayVI;
        private Button[] _btnPlayEN;

        public frmLevels(int playerID, string username, int topicID, string topicName, string topicEmoji, bool isAdmin)
        {
            InitializeComponent();
            _playerID = playerID;
            _username = username;
            _topicID = topicID;
            _topicName = topicName;
            _topicEmoji = topicEmoji;
            _isAdmin = isAdmin;

            _levelPanels = new Panel[] { pnlLevel1, pnlLevel2, pnlLevel3 };
            _btnPlayVI = new Button[] { btnPlayVI1, btnPlayVI2, btnPlayVI3 };
            _btnPlayEN = new Button[] { btnPlayEN1, btnPlayEN2, btnPlayEN3 };

            SetupUI();
            LoadLevels();
        }

        private void SetupUI()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = $"WordQuest - {_topicName}";

            label1.Text = _topicEmoji + " " + _topicName;

            btnBack.Click += BtnBack_Click;
            btnPrevLevel.Click += BtnPrevLevel_Click;
            btnNextLevel.Click += BtnNextLevel_Click;

            for (int i = 0; i < _btnPlayVI.Length; i++)
            {
                int index = i;
                _btnPlayVI[i].Click += (s, e) => StartGame(index, "VI");
                _btnPlayEN[i].Click += (s, e) => StartGame(index, "EN");
            }
        }

        private async void LoadLevels()
        {
            try
            {
                _levelsTable = await Task.Run(() => _levelBUS.LayMucDoTheoChuDe(_topicID));

                // Load tiến trình người chơi nếu có (không phải admin)
                DataTable progressTable = null;
                if (_playerID > 0 && !_isAdmin)
                {
                    progressTable = await Task.Run(() => _progressBUS.LayTienTrinhNguoiChoi(_playerID, _topicID));
                }

                RenderLevelPage(progressTable);
                CapNhatMucDoPageButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải màn chơi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderLevelPage(DataTable progressTable)
        {
            int totalLevels = _levelsTable.Rows.Count;
            int startIdx = _currentLevelPage * LEVELS_PER_PAGE;

            for (int i = 0; i < LEVELS_PER_PAGE; i++)
            {
                int levelIdx = startIdx + i;
                if (levelIdx >= totalLevels)
                {
                    _levelPanels[i].Visible = false;
                    continue;
                }

                _levelPanels[i].Visible = true;

                var row = _levelsTable.Rows[levelIdx];
                int levelID = Convert.ToInt32(row["LevelID"]);
                int levelNum = Convert.ToInt32(row["LevelNum"]);
                string levelName = row["LevelName"].ToString() ?? "";
                string difficulty = row["DifficultyLevel"].ToString() ?? "";
                int questionCount = Convert.ToInt32(row["QuestionCount"]);

                // Lấy sao và trạng thái hoàn thành từ progress
                int stars = 0;
                bool completed = false;
                if (progressTable != null)
                {
                    var progressRow = progressTable.AsEnumerable()
                        .FirstOrDefault(r => Convert.ToInt32(r["LevelNum"]) == levelNum);
                    if (progressRow != null)
                    {
                        stars = Convert.ToInt32(progressRow["Stars"]);
                        completed = Convert.ToBoolean(progressRow["IsCompleted"]);
                    }
                }

                // Kiểm tra mở khóa: màn 1 luôn mở, màn sau cần hoàn thành màn trước
                bool unlocked = _isAdmin || levelNum == 1;
                if (!unlocked && levelNum > 1 && progressTable != null)
                {
                    var prevProgress = progressTable.AsEnumerable()
                        .FirstOrDefault(r => Convert.ToInt32(r["LevelNum"]) == levelNum - 1);
                    unlocked = prevProgress != null && Convert.ToBoolean(prevProgress["IsCompleted"]);
                }

                // Cập nhật UI cho từng panel
                CapNhatMucDoPanelUI(_levelPanels[i], i, levelNum, levelName, difficulty, questionCount, stars, unlocked, completed);
            }
        }

        private void CapNhatMucDoPanelUI(Panel panel, int index, int levelNum, string levelName, string difficulty, int questionCount, int stars, bool unlocked, bool completed)
        {
            // Tìm label sao trong panel
            foreach (Control c in panel.Controls)
            {
                if (c is Label lbl)
                {
                    if (lbl.Name == $"lblStar{index + 1}")
                    {
                        lbl.Text = stars switch { 1 => "🌟☆☆", 2 => "🌟🌟☆", 3 => "🌟🌟🌟", _ => "☆☆☆" };
                    }
                    if (lbl.Name == $"lblStar{index + 1}" && panel.Controls.ContainsKey($"lblLock{index + 1}"))
                    {
                        var lockLabel = panel.Controls[$"lblLock{index + 1}"] as Label;
                        if (lockLabel != null)
                        {
                            lockLabel.Visible = !unlocked;
                            lockLabel.Text = levelNum == 1 ? "" : $"🔒 Hoàn thành Màn {levelNum - 1} trước";
                        }
                    }
                    // Tìm label tên màn (label3, label8, label11)
                    if (lbl.Name == $"label{GetLabelIndex(index)}")
                    {
                        lbl.Text = levelName;
                    }
                }
            }

            _btnPlayVI[index].Enabled = unlocked;
            _btnPlayEN[index].Enabled = unlocked;
            _btnPlayVI[index].BackColor = unlocked ? Color.FromArgb(255, 128, 0) : Color.FromArgb(150, 150, 150);
            _btnPlayEN[index].BackColor = unlocked ? Color.FromArgb(0, 150, 80) : Color.FromArgb(150, 150, 150);

            // Lưu levelNum vào tag của nút để StartGame biết
            _btnPlayVI[index].Tag = levelNum;
            _btnPlayEN[index].Tag = levelNum;
        }

        private int GetLabelIndex(int index)
        {
            return index switch { 0 => 3, 1 => 8, 2 => 11, _ => 3 };
        }

        private void CapNhatMucDoPageButtons()
        {
            int totalLevels = _levelsTable.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalLevels / LEVELS_PER_PAGE);
            btnPrevLevel.Enabled = _currentLevelPage > 0;
            btnNextLevel.Enabled = _currentLevelPage < totalPages - 1;
            lblLevelPage.Text = $"Trang {_currentLevelPage + 1} / {Math.Max(1, totalPages)}";
        }

        private void BtnPrevLevel_Click(object sender, EventArgs e)
        {
            if (_currentLevelPage > 0)
            {
                _currentLevelPage--;
                LoadLevels();
            }
        }

        private void BtnNextLevel_Click(object sender, EventArgs e)
        {
            int totalLevels = _levelsTable.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalLevels / LEVELS_PER_PAGE);
            if (_currentLevelPage < totalPages - 1)
            {
                _currentLevelPage++;
                LoadLevels();
            }
        }

        private void StartGame(int panelIndex, string mode)
        {
            int levelNum = (int)(_btnPlayVI[panelIndex].Tag ?? 0);
            if (levelNum <= 0) return;

            var levelRow = _levelsTable.AsEnumerable()
                .FirstOrDefault(r => Convert.ToInt32(r["LevelNum"]) == levelNum);
            if (levelRow == null) return;

            int levelID = Convert.ToInt32(levelRow["LevelID"]);

            var frmGame = new frmGame(_playerID, _username, _topicID, _topicName, _topicEmoji, levelNum, mode, levelID, _isAdmin);
            frmGame.Owner = this;
            frmGame.Show();
            this.Hide();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Show();
            this.Close();
        }
    }
}
