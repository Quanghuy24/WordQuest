using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmAdminPlayers : Form
    {
        private readonly Form _topics;
        private readonly PlayerBUS _playerBUS = new();
        private readonly ScoreHistoryBUS _scoreBUS = new();
        private readonly PlayerProgressBUS _progressBUS = new();

        private int _selectedPlayerID = -1;
        private string _selectedUsername = "";
        private bool _showingProgress = true;

        public frmAdminPlayers(Form topics)
        {
            InitializeComponent();
            _topics = topics;
        }

        private async void frmAdminPlayers_Load(object sender, EventArgs e)
        {
            SetupUI();
            await LoadPlayers();
            await LoadStats();
        }

        private void SetupUI()
        {

            dgvPlayers.CellClick += DgvPlayers_CellClick;
            btnBack.Click += BtnBack_Click;
            btnTabProgress.Click += BtnTabProgress_Click;
            btnTabHistory.Click += BtnTabHistory_Click;
            btnResetPP.Click += BtnResetPP_Click;
            btnResetSH.Click += BtnResetSH_Click;
            btnResetAll.Click += BtnResetAll_Click;

            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvPlayers.EnableHeadersVisualStyles = false;

            // Context menu cho dgvPlayers
            var playerMenu = new ContextMenuStrip();
            playerMenu.Items.Add("Xuất danh sách người chơi (CSV)", null, (s, e) => ExportToCSV(dgvPlayers, "Players_Report.csv"));
            dgvPlayers.ContextMenuStrip = playerMenu;

            var detailMenu = new ContextMenuStrip();
            detailMenu.Items.Add("Xuất chi tiết tiến độ/lịch sử (CSV)", null, (s, e) =>
            {
                string type = _showingProgress ? "Progress" : "History";
                ExportToCSV(dgvDetail, $"Player_{_selectedUsername}_{type}.csv");
            });
            dgvDetail.ContextMenuStrip = detailMenu;
        }

        private void ExportToCSV(DataGridView dgv, string defaultFileName)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var sfd = new SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = defaultFileName;
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var sb = new System.Text.StringBuilder();

                // Lấy header
                var headers = dgv.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.Visible && c.Name != "colXoaNguoiChoi")
                    .Select(c => $"\"{c.HeaderText}\"");
                sb.AppendLine(string.Join(",", headers));

                // Lấy data
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    var cells = dgv.Columns.Cast<DataGridViewColumn>()
                        .Where(c => c.Visible && c.Name != "colXoaNguoiChoi")
                        .Select(c =>
                        {
                            string val = row.Cells[c.Index].Value?.ToString() ?? "";
                            return $"\"{val.Replace("\"", "\"\"")}\""; // Thoát nhay kép bằng cách thay " thành ""
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

        private async Task LoadPlayers()
        {
            try
            {
                var dt = await Task.Run(() => _playerBUS.LayTatCaNguoiChoi());

                dgvPlayers.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string lastPlayed = row.Table.Columns.Contains("LastPlayed") && row["LastPlayed"] != DBNull.Value 
                        ? Convert.ToDateTime(row["LastPlayed"]).ToString("dd/MM/yyyy") : "--";
                    string createdAt = row.Table.Columns.Contains("CreatedAt") && row["CreatedAt"] != DBNull.Value 
                        ? Convert.ToDateTime(row["CreatedAt"]).ToString("dd/MM/yyyy") : "--";
                    string dayStreak = row.Table.Columns.Contains("DayStreak") && row["DayStreak"] != DBNull.Value 
                        ? $"{row["DayStreak"]} ngày" : "--";

                    dgvPlayers.Rows.Add(
                        row["PlayerID"],
                        row["Username"],
                        row["TotalScore"],
                        row["TotalStars"],
                        dayStreak,
                        lastPlayed,
                        createdAt,
                        "❌ Xóa"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load người chơi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadStats()
        {
            try
            {
                int totalPlayers = await Task.Run(() => _playerBUS.LayTongSoNguoiChoi());
                int topScore = await Task.Run(() => _playerBUS.LayDiemCaoNhat());
                int topStreak = await Task.Run(() => _playerBUS.LayChuoiNgayCaoNhat());
                int totalGames = await Task.Run(() => _scoreBUS.LayTongSoGame());

                lblTotalPlayers.Text = totalPlayers.ToString();
                lblTotalGames.Text = totalGames.ToString();
                lblTopScore.Text = topScore.ToString();
                lblTopStreak.Text = $"{topStreak} ngày";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPlayers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvPlayers.Columns[e.ColumnIndex].Name == "colXoaNguoiChoi")
            {
                int id = Convert.ToInt32(dgvPlayers.Rows[e.RowIndex].Cells["colPlayerID"].Value);
                string name = dgvPlayers.Rows[e.RowIndex].Cells["colUsername"].Value?.ToString() ?? "";
                XoaNguoiChoi(id, name);
                return;
            }

            _selectedPlayerID = Convert.ToInt32(dgvPlayers.Rows[e.RowIndex].Cells["colPlayerID"].Value);
            _selectedUsername = dgvPlayers.Rows[e.RowIndex].Cells["colUsername"].Value?.ToString() ?? "";
            lblSelectedPlayer.Text = $"Thong tin nguoi choi: {_selectedUsername}";

            dgvPlayers.ClearSelection();
            dgvPlayers.Rows[e.RowIndex].Selected = true;

            if (_showingProgress)
                LoadProgress();
            else
                LoadHistory();
        }

        private async void LoadProgress()
        {
            if (_selectedPlayerID < 0)
            {
                MessageBox.Show("Vui lòng chọn người chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dt = await Task.Run(() => _progressBUS.LayTatCaTienTrinh(_selectedPlayerID));

                colD1.HeaderText = "Chủ đề";
                colD2.HeaderText = "Màn";
                colD3.HeaderText = "Sao";
                colD4.HeaderText = "Điểm cao nhất";
                colD5.HeaderText = "Hoàn thành";
                colD6.HeaderText = "Ngày hoàn thành";

                dgvDetail.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    bool done = Convert.ToBoolean(row["IsCompleted"]);
                    string completedAt = (!done || row["CompletedAt"] == DBNull.Value) ? "--"
                        : Convert.ToDateTime(row["CompletedAt"]).ToString("dd/MM/yyyy");
                    string stars = new string('★', Convert.ToInt32(row["Stars"]))
                                 + new string('☆', 3 - Convert.ToInt32(row["Stars"]));

                    int idx = dgvDetail.Rows.Add(
                        row["TopicName"],
                        "Màn " + row["LevelNum"],
                        stars,
                        row["BestScore"],
                        done ? "✅ Xong" : "❌ Chưa",
                        completedAt
                    );

                    dgvDetail.Rows[idx].Cells["colD5"].Style.ForeColor = done
                        ? Color.FromArgb(27, 94, 32) : Color.FromArgb(183, 28, 28);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load tiến trình: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadHistory()
        {
            if (_selectedPlayerID < 0)
            {
                MessageBox.Show("Vui lòng chọn người chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var dt = await Task.Run(() => _scoreBUS.LayLichSuDiemNguoiChoi(_selectedPlayerID));

                colD1.HeaderText = "Chủ đề";
                colD2.HeaderText = "Màn";
                colD3.HeaderText = "Điểm";
                colD4.HeaderText = "Sao";
                colD5.HeaderText = "Thời gian (s)";
                colD6.HeaderText = "Ngày chơi";

                dgvDetail.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    string playedAt = row["PlayedAt"] == DBNull.Value ? "--"
                        : Convert.ToDateTime(row["PlayedAt"]).ToString("dd/MM/yyyy HH:mm");
                    string stars = new string('★', Convert.ToInt32(row["Stars"]))
                                 + new string('☆', 3 - Convert.ToInt32(row["Stars"]));

                    dgvDetail.Rows.Add(
                        row["TopicName"],
                        "Màn " + row["LevelNum"],
                        row["Score"],
                        stars,
                        row["TimeTaken"],
                        playedAt
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load lịch sử: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTabProgress_Click(object sender, EventArgs e)
        {
            _showingProgress = true;
            btnTabProgress.BackColor = Color.FromArgb(255, 140, 0);
            btnTabHistory.BackColor = Color.FromArgb(100, 65, 30);
            LoadProgress();
        }

        private void BtnTabHistory_Click(object sender, EventArgs e)
        {
            _showingProgress = false;
            btnTabHistory.BackColor = Color.FromArgb(255, 140, 0);
            btnTabProgress.BackColor = Color.FromArgb(100, 65, 30);
            LoadHistory();
        }

        private async void XoaNguoiChoi(int playerID, string username)
        {
            var result = MessageBox.Show($"Xóa người chơi \"{username}\"?\nToàn bộ dữ liệu sẽ bị xóa!",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            try
            {
                await Task.Run(() => _scoreBUS.XoaLichSuDiemNguoiChoi(playerID));
                await Task.Run(() => _progressBUS.XoaTienTrinhNguoiChoi(playerID));
                await Task.Run(() => _playerBUS.XoaNguoiChoi(playerID));

                if (_selectedPlayerID == playerID)
                {
                    _selectedPlayerID = -1;
                    _selectedUsername = "";
                    lblSelectedPlayer.Text = "Chọn người chơi để xem chi tiết";
                    dgvDetail.Rows.Clear();
                }

                await LoadPlayers();
                await LoadStats();
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnResetPP_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerID < 0) { MessageBox.Show("Chọn người chơi!"); return; }

            var result = MessageBox.Show($"Reset tiến độ của \"{_selectedUsername}\"?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            await Task.Run(() => _progressBUS.XoaTienTrinhNguoiChoi(_selectedPlayerID));
            LoadProgress();
            MessageBox.Show("Reset hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnResetSH_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerID < 0) { MessageBox.Show("Chọn người chơi!"); return; }

            var result = MessageBox.Show($"Reset lịch sử của \"{_selectedUsername}\"?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            await Task.Run(() => _scoreBUS.XoaLichSuDiemNguoiChoi(_selectedPlayerID));
            LoadHistory();
            MessageBox.Show("Reset hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnResetAll_Click(object sender, EventArgs e)
        {
            if (_selectedPlayerID < 0) { MessageBox.Show("Chọn người chơi!"); return; }

            var result = MessageBox.Show($"Reset hoàn tất của \"{_selectedUsername}\"?\nKhÔng hoàn tất!",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            await Task.Run(() => _scoreBUS.XoaLichSuDiemNguoiChoi(_selectedPlayerID));
            await Task.Run(() => _progressBUS.XoaTienTrinhNguoiChoi(_selectedPlayerID));

            // Reset hoàn tất cũng sẽ reset streak để tránh tình trạng người chơi bị kẹt ở streak cao nhưng không có tiến trình nào (do đã xóa hết tiến trình)
            await Task.Run(() => _playerBUS.DatLaiChiSoNguoiChoi(_selectedPlayerID));

            await LoadPlayers();
            await LoadStats();
            dgvDetail.Rows.Clear();
            MessageBox.Show("Reset hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            _topics.Show();
            this.Close();
        }
    }
}
