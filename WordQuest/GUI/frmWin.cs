using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordQuest.GUI
{
    public partial class frmWin : System.Windows.Forms.Form
    {
        private readonly int _playerID;
        private readonly string _username;
        private readonly int _topicID;
        private readonly string _topicName;
        private readonly string _topicEmoji;
        private readonly int _levelNum;
        private readonly int _levelID;
        private readonly int _score;
        private readonly int _stars;
        private readonly int _correctCount;
        private readonly int _timeTaken;
        private readonly int _totalQuestions;
        private readonly string _gameMode;
        private readonly bool _isAdmin;

        public frmWin(int playerID, string username, int topicID, string topicName, string topicEmoji,
                      int levelNum, int levelID, int score, int stars, int correctCount, int timeTaken,
                      int totalQuestions, string gameMode, bool isAdmin = false)
        {
            InitializeComponent();
            _playerID = playerID;
            _username = username;
            _topicID = topicID;
            _topicName = topicName;
            _topicEmoji = topicEmoji;
            _levelNum = levelNum;
            _levelID = levelID;
            _score = score;
            _stars = stars;
            _correctCount = correctCount;
            _timeTaken = timeTaken;
            _totalQuestions = totalQuestions;
            _gameMode = gameMode;
            _isAdmin = isAdmin;
        }

        private void frmWin_Load(object sender, EventArgs e)
        {
            SetupUI();

            // Gán sự kiện
            btnReplay.Click += BtnReplay_Click;
            btnLevels.Click += BtnLevels_Click;
            btnHome.Click += BtnHome_Click;
        }

        private void SetupUI()
        {
            lblTitle.Text = _stars switch
            {
                3 => "🏆 Xuất sắc!",
                2 => "🎉 Tốt lắm!",
                1 => "👍 Cố gắng hơn!",
                _ => "😢 Thử lại nào!"
            };

            lblStars.Text = _stars switch
            {
                3 => "🌟 🌟 🌟",
                2 => "🌟 🌟 ☆",
                1 => "🌟 ☆ ☆",
                _ => "☆ ☆ ☆"
            };

            string rank = _stars switch
            {
                3 => "S - Hoàn hảo",
                2 => "A - Tốt",
                1 => "B - Trung bình",
                _ => "C - Cần cố gắng"
            };

            lblTopicInfo.Text = $"{_topicEmoji}  Chủ đề:       {_topicName} - Màn {_levelNum}";
            lblCorrectInfo.Text = $"✅  Số câu đúng:  {_correctCount}/{_totalQuestions}";
            lblScoreInfo.Text = $"🎯  Điểm số:       {_score} điểm";
            lblRankInfo.Text = $"🏅  Xếp loại:      {rank}";
            lblRankInfo.ForeColor = Color.FromArgb(255, 180, 50);

        }

        private void BtnReplay_Click(object sender, EventArgs e)
        {
            var frmGame = new frmGame(_playerID, _username, _topicID, _topicName, _topicEmoji, _levelNum, _gameMode, _levelID, _isAdmin);
            frmGame.Owner = this.Owner;
            frmGame.Show();
            this.Close();
        }

        private void BtnLevels_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Show();
            this.Close();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            var frmTopics = new frmTopics(_playerID, _username, _isAdmin);
            frmTopics.Show();
            this.Close();
        }
    }
}
