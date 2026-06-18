using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmTopics : System.Windows.Forms.Form
    {
        private readonly bool _isAdmin;
        private readonly int _playerID;
        private readonly string _username;
        private int _currentPage = 0;
        private const int TOPICS_PER_PAGE = 9;

        private readonly TopicBUS _topicBUS = new();
        private readonly PlayerBUS _playerBUS = new();

        private DataTable _topicsTable;
        private Button[] _topicButtons;

        public frmTopics(int playerID, string username, bool isAdmin = false)
        {
            InitializeComponent();
            _playerID = playerID;
            _username = username;
            _isAdmin = isAdmin;

            _topicButtons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            SetupUI();
            RefreshChuDe();
        }

        private void SetupUI()
        {
            lblName.Text = $"Xin chào, {_username}";
            btnCaiDat.Visible = _isAdmin;

            foreach (var btn in _topicButtons)
            {
                btn.Click -= TopicCard_Click;
                btn.Click += TopicCard_Click;
            }

            btnReset.Click += BtnReset_Click;
            btnNext.Click += BtnNext_Click;
            btnXepHang.Click += BtnXepHang_Click;
            btnCaiDat.Click += BtnCaiDat_Click;
        }

        private async void RefreshChuDe()
        {
            try
            {
                _topicsTable = await Task.Run(() => _topicBUS.LayTatCaChuDe());

                int totalStars = 0;
                int dayStreak = 0;
                if (_playerID > 0 && !_isAdmin)
                {
                    var player = await Task.Run(() => _playerBUS.LayNguoiChoiTheoID(_playerID));
                    totalStars = player?.TotalStars ?? 0;
                    dayStreak = player?.DayStreak ?? 0;
                }

                lblStrat.Text = _isAdmin ? "⭐ Admin" : $"{totalStars} ⭐";

                // Hiển thị streak trên label tên: thêm ngọn lửa nếu có streak
                if (!_isAdmin && _playerID > 0)
                {
                    string streakBadge = dayStreak >= 1 ? $"  |  🔥 {dayStreak} ngày" : "";
                    lblName.Text = $"Xin chào, {_username}{streakBadge}";
                }

                RenderChuDePage(totalStars);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chủ đề: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderChuDePage(int currentStars)
        {
            int totalTopics = _topicsTable.Rows.Count;
            int startIdx = _currentPage * TOPICS_PER_PAGE;
            int totalPages = (int)Math.Ceiling((double)totalTopics / TOPICS_PER_PAGE);

            btnReset.Enabled = _currentPage > 0;
            btnNext.Enabled = _currentPage < totalPages - 1;

            for (int i = 0; i < _topicButtons.Length; i++)
            {
                int topicIdx = startIdx + i;
                if (topicIdx < totalTopics)
                {
                    var row = _topicsTable.Rows[topicIdx];
                    int topicID = Convert.ToInt32(row["TopicID"]);
                    string topicName = row["TopicName"].ToString() ?? "";
                    string topicIcon = row["TopicIcon"].ToString() ?? "";
                    int starsNeeded = Convert.ToInt32(row["StarsToUnlock"]);

                    bool unlocked = _isAdmin || currentStars >= starsNeeded;

                    _topicButtons[i].Visible = true;
                    _topicButtons[i].Tag = topicID;
                    _topicButtons[i].Text = unlocked
                        ? $"{topicIcon}\n{topicName}"
                        : $"🔒\n{topicName}\nCần {starsNeeded}⭐";
                    _topicButtons[i].ForeColor = unlocked ? Color.FromArgb(100, 40, 0) : Color.FromArgb(150, 150, 150);         
                    _topicButtons[i].BackColor = unlocked ? Color.FromArgb(255, 200, 120) : Color.FromArgb(200, 200, 200);
                    _topicButtons[i].FlatAppearance.BorderColor = unlocked ? Color.FromArgb(200, 100, 0) : Color.FromArgb(150, 150, 150);
                }
                else
                {
                    _topicButtons[i].Visible = false;
                }
            }
        }

        private void TopicCard_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag == null) return;

            int topicID = (int)btn.Tag;

            var row = _topicsTable.AsEnumerable()
                .FirstOrDefault(r => Convert.ToInt32(r["TopicID"]) == topicID);

            if (row == null) return;

            string topicName = row["TopicName"].ToString() ?? "";
            string topicIcon = row["TopicIcon"].ToString() ?? "";
            int starsNeeded = Convert.ToInt32(row["StarsToUnlock"]);

            int currentStars = 0;
            if (!_isAdmin && _playerID > 0)
            {
                var player = _playerBUS.LayNguoiChoiTheoID(_playerID);
                currentStars = player?.TotalStars ?? 0;
            }

            if (!_isAdmin && currentStars < starsNeeded)
            {
                MessageBox.Show($"🔒 Cần {starsNeeded}⭐ để mở chủ đề này!\nBạn có: {currentStars}⭐",
                    "Chưa mở khóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frmLevels = new frmLevels(_playerID, _username, topicID, topicName, topicIcon, _isAdmin);
            frmLevels.Owner = this;
            frmLevels.Show();
            this.Hide();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                RefreshChuDe();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            int totalTopics = _topicsTable.Rows.Count;
            int totalPages = (int)Math.Ceiling((double)totalTopics / TOPICS_PER_PAGE);
            if (_currentPage < totalPages - 1)
            {
                _currentPage++;
                RefreshChuDe();
            }
        }

        private void BtnXepHang_Click(object sender, EventArgs e)
        {
            var frm = new frmLeaderboard(_playerID, _username, _isAdmin);
            frm.Owner = this;
            frm.Show();
            this.Hide();
        }

        private void BtnCaiDat_Click(object sender, EventArgs e)
        {
            var menu = new frmAdminMenu(_playerID, _username, this);
            menu.ShowDialog();
            RefreshChuDe();
        }

    }
}
