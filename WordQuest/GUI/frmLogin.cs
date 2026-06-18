using Microsoft.Data.SqlClient;
using System;
using System.Windows.Forms;
using WordQuest.BUS;

namespace WordQuest.GUI
{
    public partial class frmLogin : Form
    {
        private readonly PlayerBUS _playerBUS = new();

        public frmLogin()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            txtUsername.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    btnStart_Click(null, EventArgs.Empty);
            };
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();

            if (username.ToLower() == "admin")
            {
                //string inputPwd = PromptPassword();
                //if (inputPwd != AppConfig.AdminPassword)
                //{
                //    lblError.Text = "❌ Sai mật khẩu Admin!";
                //    return;
                //}

                var frmTopics = new frmTopics(0, "Admin", true);
                frmTopics.Show();
                this.Hide();
                return;
            }

            if (string.IsNullOrEmpty(username))
            {
                lblError.Text = "Lỗi¸ Vui lòng nhập tên của bạn!";
                txtUsername.Focus();
                return;
            }

            if (username.Length < 2)
            {
                lblError.Text = "Lỗi¸ Tên phải có ít nhất 2 ký tự!";
                txtUsername.Focus();
                return;
            }

            try
            {
                int playerID = await Task.Run(() => _playerBUS.LayHoacTaoNguoiChoi(username));
                if (playerID > 0)
                {
                    //Cập nhật streak theo ngày
                    var (newStreak, isNewDay) = await Task.Run(()
                        => _playerBUS.CapNhatChuoiNgay(playerID));

                    if (isNewDay)
                    {
                        string streakMsg;
                        if (newStreak == 1)
                        {
                            streakMsg = "Streak của bạn đã bị reset vì bỏ lỡ hôm qua 😢\nHãy quay lại mỗi ngày để giữ streak nhé!";
                        }
                        else if (newStreak >= 7)
                        {
                            streakMsg = $"🔥 TUYỆT VỜI! Streak {newStreak} ngày liên tiếp!\nBạn đang trên đà rất tốt, tiếp tục nhé! 🏆";
                        }
                        else
                        {
                            streakMsg = $"🔥 Streak: {newStreak} ngày liên tiếp!\nChào mừng trở lại, {username}!";
                        }

                        MessageBox.Show(streakMsg, "Streak hôm nay",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //Mở màn hình chính

                    var frmTopics = new frmTopics(playerID, username);
                    frmTopics.Show();
                    this.Hide();
                }
                else
                {
                    lblError.Text = "❌ Không thể tạo/tìm người chơi!";
                }
            }
            catch (Exception ex)
            {
                lblError.Text = $"❌ Lỗi: {ex.Message}";
            }
        }
        
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }
        
    }
}
