using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WordQuest.BUS;

namespace WordQuest.GUI
{
    public partial class frmLeaderboard : Form
    {
        private readonly int _playerID;
        private readonly string _username;
        private readonly bool _isAdmin;
        private readonly ScoreHistoryBUS _scoreBUS = new();

        public frmLeaderboard(int playerID, string username, bool isAdmin)
        {
            InitializeComponent();
            _playerID = playerID;
            _username = username;
            _isAdmin = isAdmin;
        }

        private async void frmLeaderboard_Load(object sender, EventArgs e)
        {
            SetupUI();
            await LoadLeaderboard();
        }

        private void SetupUI()
        {
            btnBack.Click += (s, ev) =>
            {
                if (this.Owner != null)
                    this.Owner.Show();
                this.Close();
            };
        }

        private async System.Threading.Tasks.Task LoadLeaderboard()
        {
            try
            {
                var dt = await System.Threading.Tasks.Task.Run(() => _scoreBUS.LayBangXepHang());

                dgvScore.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int hang = Convert.ToInt32(row["Hang"]);
                    string medal = hang switch { 1 => "🥇", 2 => "🥈", 3 => "🥉", _ => $"#{hang}" };
                    string name = row["Username"].ToString() ?? "";
                    bool isMe = name.Equals(_username, StringComparison.OrdinalIgnoreCase);

                    if (isMe)
                        name = $"▶ {name} (Bạn)";

                    int idx = dgvScore.Rows.Add(medal, name, $"{row["TotalScore"]} điểm", $"⭐ {row["TotalStars"]}");

                    if (isMe)
                    {
                        dgvScore.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
                        dgvScore.Rows[idx].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bảng xếp hạng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
