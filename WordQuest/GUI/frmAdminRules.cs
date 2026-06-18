using System;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmAdminRules : Form
    {
        private readonly Form _topics;
        private readonly GameRuleBUS _ruleBUS = new();

        public frmAdminRules(Form topics)
        {
            InitializeComponent();
            _topics = topics;
        }

        private async void frmAdminRules_Load(object sender, EventArgs e)
        {
            SetupUI();
            await LoadRules();
        }

        private void SetupUI()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            btnSave.Click += BtnSave_Click;
            btnDefault.Click += BtnDefault_Click;
            btnBack.Click += BtnBack_Click;
        }

        private async Task LoadRules()
        {
            try
            {
                var rule = await Task.Run(() => _ruleBUS.LayLuatGame());

                nudQuestionCount.Value = rule.QuestionCount;
                nudTimeLđiểmit.Value = rule.TimeLđiểmit;
                nudLives.Value = rule.Lives;
                nudStreakBonus.Value = rule.StreakBonus;
                nudStar1.Value = rule.Star1Threshold;
                nudStar2.Value = rule.Star2Threshold;
                nudStar3.Value = rule.Star3Threshold;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải quy tắc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetDefaultValues();
            }
        }

        private void SetDefaultValues()
        {
            nudQuestionCount.Value = 15;
            nudTimeLđiểmit.Value = 60;
            nudLives.Value = 3;
            nudStreakBonus.Value = 5;
            nudStar1.Value = 5;
            nudStar2.Value = 10;
            nudStar3.Value = 15;
        }

        private bool ValidateRules()
        {
            if (nudStar1.Value >= nudStar2.Value)
            {
                MessageBox.Show("Ngưỡng sao 1 phải nhỏ hơn ngưỡng sao 2!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudStar2.Value >= nudStar3.Value)
            {
                MessageBox.Show("Ngưỡng sao 2 phải nhỏ hơn ngưỡng sao 3!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateRules()) return;

            var rule = new GameRuleDTO
            {
                QuestionCount = (int)nudQuestionCount.Value,
                TimeLđiểmit = (int)nudTimeLđiểmit.Value,
                Lives = (int)nudLives.Value,
                StreakBonus = (int)nudStreakBonus.Value,
                Star1Threshold = (int)nudStar1.Value,
                Star2Threshold = (int)nudStar2.Value,
                Star3Threshold = (int)nudStar3.Value
            };

            try
            {
                await Task.Run(() => _ruleBUS.LuuLuatGame(rule));
                MessageBox.Show("Lưu quy tắc game thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            SetDefaultValues();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            _topics.Show();
            this.Close();
        }

        private void lblQuestionCount_Click(object sender, EventArgs e)
        {

        }
    }
}
