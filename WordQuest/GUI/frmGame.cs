using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;
using System.Windows.Forms;
using WordQuest.BUS;
using WordQuest.DTO;

namespace WordQuest.GUI
{
    public partial class frmGame : Form
    {
        #region ==================== Constants ====================

        private const int SLOT_WIDTH = 45;
        private const int SLOT_HEIGHT = 50;
        private const int LETTER_BTN_WIDTH = 55;
        private const int LETTER_BTN_HEIGHT = 55;
        private const int GAP = 6;
     
        private static readonly Color COLOR_SLOT_DEFAULT = Color.FromArgb(30, 30, 60);
        private static readonly Color COLOR_SLOT_FILLED = Color.FromArgb(50, 50, 100);
        private static readonly Color COLOR_CORRECT = Color.Lime;
        private static readonly Color COLOR_WRONG = Color.FromArgb(255, 100, 100);
        private static readonly Color COLOR_HINT = Color.FromArgb(255, 200, 0); 

        #endregion

        #region ==================== Fields ====================

        private readonly int _playerID;
        private readonly int _topicID;
        private readonly string _topicName;
        private readonly string _topicEmoji;
        private readonly int _levelNum;
        private readonly string _gameMode;
        private readonly int _levelID;
        private readonly bool _isAdmin;

        private readonly GameBUS _gameBUS;
        private SpeechSynthesizer _speechSynthesizer;
        private List<Button> _answerSlots;
        private List<Button> _letterButtons;
        private CurrentQuestionDTO _currentQuestion;

        // Timer để đếm thời gian chơi và cập nhật UI, chạy liên tục mỗi 50ms
        private System.Windows.Forms.Timer _nextWordTimer;

        #endregion

        #region ==================== Constructor ====================

        public frmGame(int playerID, string username, int topicID, string topicName, string topicEmoji,
                       int levelNum, string gameMode, int levelID, bool isAdmin = false)
        {
            InitializeComponent();

            _playerID = playerID;
            _topicID = topicID;
            _topicName = topicName;
            _topicEmoji = topicEmoji;
            _levelNum = levelNum;
            _gameMode = gameMode;
            _levelID = levelID;
            _isAdmin = isAdmin;

            _gameBUS = new GameBUS();
            _answerSlots = new List<Button>();
            _letterButtons = new List<Button>();

            _gameBUS.OnStateChanged += CapNhatUI;
            _gameBUS.OnAnswerCompleted += (outcome) => KiemTraDapAn(outcome.KiemTraDung, outcome.CorrectAnswer);
            _gameBUS.OnTimeOut += (timeout) => HetThoiGian();
            _gameBUS.OnGameEnded += KhiGameKetThuc;
            _gameBUS.OnScoreChanged += KhiDiemThayDoi; 
        }

        #endregion

        #region ==================== Form Events ====================

        private async void frmGame_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                KhoiTaoGiongNoi();
                await _gameBUS.KhoiTaoGameAsync(_levelID, _gameMode);

                // Load UI chủ đề, level, câu hỏi đầu tiên
                await LoadCurrentWordUIAsync();

                gameTimer.Interval = 50;
                gameTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo game: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Owner?.Show();
                Close();
            }
            finally { Cursor = Cursors.Default; }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            try { CapNhatUI(_gameBUS.LayTrangThaiHienTai()); } catch { }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?\nTiến độ sẽ không được lưu!",
                "Thoát game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gameTimer.Stop();
                _nextWordTimer?.Stop();
                _gameBUS.HuyGame();
                DonDepTaiNguyen();
                Owner?.Show();
                Close();
            }
        }

        private void BtnHint_Click(object sender, EventArgs e)
        {
            var hint = _gameBUS.SuDungGoiY();
            if (hint == null) return;

            var btn = _letterButtons.Find(b => b.Visible && b.Text.Length > 0
                      && char.ToUpper(b.Text[0]) == char.ToUpper(hint.Letter));
            if (btn != null) btn.Visible = false;

            int hintIndex = _gameBUS.LayNhapLieuHienTai().Count - 1;
            if (hintIndex >= 0 && hintIndex < _answerSlots.Count)
                _answerSlots[hintIndex].BackColor = COLOR_HINT;
        }

        private void AnswerSlot_Click(object sender, EventArgs e)
        {
            if (sender is not Button slot) return;
            int index = _answerSlots.IndexOf(slot);
            if (index < 0) return;

            // Lưu lại các chữ sẽ bị xóa để hiện lại button
            var inputBefore = _gameBUS.LayNhapLieuHienTai();
            var charsToReturn = new List<char>();
            for (int i = index; i < inputBefore.Count; i++)
                charsToReturn.Add(inputBefore[i]);

            _gameBUS.XoaKyTuTu(index);

            // Hiện lại các button tương ứng với chữ bị xóa
            foreach (char c in charsToReturn)
            {
                var btnToShow = _letterButtons.Find(b => !b.Visible && b.Text.Length > 0
                                && char.ToUpper(b.Text[0]) == char.ToUpper(c));
                if (btnToShow != null) btnToShow.Visible = true;
            }
        }

        private void LetterButton_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn || !btn.Visible) return;

            var result = _gameBUS.ThemKyTu(btn.Text[0]);

            if (result == AnswerResult.Added || result == AnswerResult.Correct)
                btn.Visible = false;

            if (result == AnswerResult.Correct)
            {
                AmThanhTrLoiDung();
                DocTiengAnh();
            }
            else if (result == AnswerResult.Wrong)
            {
                AmThanhBaoLoi();
            }
        }

        #endregion

        #region ==================== BUS Event Handlers ====================

        private void CapNhatUI(GameStateDTO state)
        {
            if (this.IsDisposed || state == null) return;
            if (this.InvokeRequired)
            {
                try { this.BeginInvoke(new Action(() => CapNhatUI(state))); } catch { }
                return;
            }

            // Cập nhật header
            lblTopic.Text = $"{_topicEmoji} {_topicName} - Màn {state.LevelNum}";
            lblQuestion.Text = $"Câu {Math.Min(state.CurrentWordIndex + 1, state.TotalQuestions)}/{state.TotalQuestions}";
            lblLives.Text = GetLivesDisplay(state.Lives, state.MaxLives);
            lblStreak.Text = $"🔥 {state.Streak}";
            lblTimer.Text = $"{state.RemainingSeconds}s ⏱️";
            CapNhatTimerColor(state.RemainingSeconds);
            pbTime.Value = Math.Clamp(state.ProgressPercent, 0, 100);

            bool canInteract = !state.IsWaitingForNext && !state.IsAnswered && !state.IsGameOver;
            pnlLetters.Enabled = canInteract;
            btnHint.Enabled = canInteract;

            CapNhatMauOTrLoi(state.CurrentInput);
        }

        /// BUS sẽ gọi hàm này khi người chơi hoàn thành trả lời một câu hỏi, với KiemTraDung = true nếu trả lời đúng, false nếu sai, và correctAnswer là đáp án đúng để hiển thị nếu sai hoặc hết thời gian.
        private void KiemTraDapAn(bool KiemTraDung, string correctAnswer)
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => KiemTraDapAn(KiemTraDung, correctAnswer))); return; }

            if (KiemTraDung)
            {
                ThayDoiMauOTrLoi(COLOR_CORRECT);
                AmThanhTrLoiDung();
                DocTiengAnh();
            }
            else
            {
                ThayDoiMauOTrLoi(COLOR_WRONG);
                ShowDapAnDung(correctAnswer);
                AmThanhBaoLoi();
            }

            // Đợi 2 giây rồi sang câu tiếp theo
            ChuyenCauSauNGiay();
        }

        private void HetThoiGian()
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(HetThoiGian)); return; }

            ThayDoiMauOTrLoi(COLOR_WRONG);
            ShowDapAnDung(_gameBUS.LayDapAnDung());
            AmThanhBaoLoi();

            // Đợi 2 giây rồi sang câu tiếp theo
            ChuyenCauSauNGiay();
        }

        private void KhiGameKetThuc(GameResultDTO result)
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => KhiGameKetThuc(result))); return; }

            _nextWordTimer?.Stop();
            gameTimer.Stop();
            _ = _gameBUS.LuuKetQuaGameAsync(_playerID, _topicID, _levelNum);

            var resultForm = new frmWin(
                _playerID, "",
                _topicID, _topicName, _topicEmoji,
                _levelNum, _levelID, result.Score, result.Stars,
                result.CorrectCount, result.TimeTaken,
                result.TotalQuestions, _gameMode, _isAdmin)
            { Owner = this.Owner };

            resultForm.Show();
            DonDepTaiNguyen();
            Close();
        }

        private void KhiDiemThayDoi(int newScore, int bonus)
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => KhiDiemThayDoi(newScore, bonus))); return; }
            if (bonus <= 0) return;
            lblStreak.BackColor = Color.Gold;
            var t = new System.Windows.Forms.Timer { Interval = 300 };
            t.Tick += (s, e) => { lblStreak.BackColor = Color.Transparent; lblStreak.ForeColor = Color.White; t.Stop(); t.Dispose(); };
            t.Start();
        }

        #endregion

        #region ==================== Next Word Timer ====================

        /// <summary>
        /// Bắt đầu một timer đếm ngược 2 giây trước khi gọi BUS để chuyển sang câu tiếp theo và load UI mới. Nếu đã có timer đang chạy thì dừng và tạo lại timer mới.
        /// </summary>
        private void ChuyenCauSauNGiay()
        {
            _nextWordTimer?.Stop();
            _nextWordTimer?.Dispose();

            _nextWordTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            _nextWordTimer.Tick += async (s, e) =>
            {
                _nextWordTimer.Stop();
                _nextWordTimer.Dispose();
                _nextWordTimer = null;

                // Gọi BUS chuyển câu
                _gameBUS.TuTiepTheo();

                // Load UI câu mới
                await LoadCurrentWordUIAsync();
            };
            _nextWordTimer.Start();
        }

        #endregion

        #region ==================== UI Building ====================

        private async Task LoadCurrentWordUIAsync() 
        {
            _currentQuestion = _gameBUS.LayCauHoiHienTai();
            if (_currentQuestion == null) return;

            // Cập nhật hình ảnh trước, nếu không có thì dùng EmojiIcon của từ
            bool imageLoaded = await TryLoadImageAsync(_currentQuestion);
            if (!imageLoaded)
                RenderEmoji(_currentQuestion.EmojiIcon ?? "🔥");

            TaoCacNutDapAn(_currentQuestion.CorrectAnswer.Length);
            TaoCacNutChuCai(_gameBUS.LayKyTuCoSan());
            lblEnglish.Visible = false;
        }

        private async Task<bool> TryLoadImageAsync(CurrentQuestionDTO question)
        {
            if (question.ImageID.HasValue && question.ImageID.Value > 0)
            {
                var imageBUS = new WordQuest.BUS.ImageBUS();
                var điểmg = await imageBUS.LayHinhAsync(question.ImageID.Value);
                if (điểmg != null)
                {
                    var old = picWord.Image;
                    picWord.Image = điểmg;
                    picWord.SizeMode = PictureBoxSizeMode.Zoom;
                    old?.Dispose();
                    return true;
                }
            }

            if (string.IsNullOrEmpty(question.ImagePath)) return false;

            string[] paths = {
                Path.Combine(Application.StartupPath, question.ImagePath),
                Path.Combine(Application.StartupPath, "Images", question.ImagePath),
                question.ImagePath
            };

            foreach (var fullPath in paths)
            {
                if (!File.Exists(fullPath)) continue;
                try
                {
                    var old = picWord.Image;
                    picWord.Image = await Task.Run(() => Image.FromFile(fullPath));
                    picWord.SizeMode = PictureBoxSizeMode.Zoom;
                    old?.Dispose();
                    return true; 
                }
                catch { }
            }

            return false; 
        }

        private void TaoCacNutDapAn(int slotCount)
        {
            if (slotCount <= 0) return;

            pnlànswer.SuspendLayout();

            // dùng Dispose để giải phóng tài nguyên của các button cũ trước khi xóa khỏi panel, tránh rò rỉ bộ nhớ
            foreach (var btn in _answerSlots)
                btn.Dispose();
                
            pnlànswer.Controls.Clear();
            _answerSlots.Clear();

            int totalWidth = slotCount * (SLOT_WIDTH + GAP);
            int startX = Math.Max(5, (pnlànswer.Width - totalWidth) / 2);
            int startY = (pnlànswer.Height - SLOT_HEIGHT) / 2;

            for (int i = 0; i < slotCount; i++)
            {
                var slot = new Button
                {
                    Size = new Size(SLOT_WIDTH, SLOT_HEIGHT),
                    Location = new Point(startX + i * (SLOT_WIDTH + GAP), startY),
                    Text = "",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = COLOR_SLOT_DEFAULT,
                    FlatStyle = FlatStyle.Flat,
                    Tag = i
                };
                slot.FlatAppearance.BorderSize = 0;
                slot.Click += AnswerSlot_Click;
                pnlànswer.Controls.Add(slot);
                _answerSlots.Add(slot);
            }

            pnlànswer.ResumeLayout();
        }

        private void TaoCacNutChuCai(List<char> letters)
        {
            if (letters == null || letters.Count == 0) return;

            pnlLetters.SuspendLayout();

            // dùng Dispose để giải phóng tài nguyên của các button cũ trước khi xóa khỏi panel, tránh rò rỉ bộ nhớ
            foreach (var btn in _letterButtons)
                btn.Dispose();
                
            pnlLetters.Controls.Clear();
            _letterButtons.Clear();

            int totalWidth = letters.Count * (LETTER_BTN_WIDTH + GAP);
            int startX = Math.Max(5, (pnlLetters.Width - totalWidth) / 2);
            int startY = (pnlLetters.Height - LETTER_BTN_HEIGHT) / 2;

            for (int i = 0; i < letters.Count; i++)
            {
                var btn = new Button
                {
                    Size = new Size(LETTER_BTN_WIDTH, LETTER_BTN_HEIGHT),
                    Location = new Point(startX + i * (LETTER_BTN_WIDTH + GAP), startY),
                    Text = letters[i].ToString().ToUpper(),
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    ForeColor = Color.FromArgb(100, 40, 0),
                    BackColor = Color.FromArgb(255, 224, 192),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += LetterButton_Click;
                pnlLetters.Controls.Add(btn);
                _letterButtons.Add(btn);
            }

            pnlLetters.ResumeLayout();
        }
        
        private void CapNhatMauOTrLoi(List<char> currentInput)
        {
            for (int i = 0; i < _answerSlots.Count; i++)
            {
                bool hasChar = currentInput != null && i < currentInput.Count && currentInput[i] != '\0';
                string newText = hasChar ? currentInput[i].ToString().ToUpper() : "";
                if (_answerSlots[i].Text != newText)
                    _answerSlots[i].Text = newText;
                    
                if (!hasChar && _answerSlots[i].BackColor != COLOR_SLOT_DEFAULT)
                    _answerSlots[i].BackColor = COLOR_SLOT_DEFAULT;
                else if (hasChar && _answerSlots[i].BackColor == COLOR_SLOT_DEFAULT)
                    _answerSlots[i].BackColor = COLOR_SLOT_FILLED;
            }
        }

        private void ThayDoiMauOTrLoi(Color color)
        { 
            foreach (var slot in _answerSlots)
                slot.BackColor = color;
        }

        private void ShowDapAnDung(string answer)
        {
            if (string.IsNullOrEmpty(answer)) return;
            for (int i = 0; i < _answerSlots.Count && i < answer.Length; i++)
                _answerSlots[i].Text = answer[i].ToString().ToUpper();
        }

        #endregion

        #region ==================== Image & Speech ====================

        
        
        private void RenderEmoji(string emoji)
        {
            int width = picWord.Width > 0 ? picWord.Width : 484;
            int height = picWord.Height > 0 ? picWord.Height : 331;
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.Clear(SystemColors.Control);
                using var font = new Font("Segoe UI Emoji", 100, FontStyle.Regular, GraphicsUnit.Pixel);
                var sz = g.MeasureString(emoji, font);
                g.DrawString(emoji, font, SystemBrushes.ControlText,
                    (bitmap.Width - sz.Width) / 2,
                    (bitmap.Height - sz.Height) / 2);
            }

            var oldImg = picWord.Image;
            picWord.Image = bitmap;
            picWord.SizeMode = PictureBoxSizeMode.CenterImage;
            oldImg?.Dispose();
        }

        private void KhoiTaoGiongNoi()
        {
            try
            {
                _speechSynthesizer = new SpeechSynthesizer();
                
                // Cài đặt giọng nói tiếng Anh để tránh lỗi đọc tiếng Anh bằng tiếng Việt
                var voices = _speechSynthesizer.GetInstalledVoices();
                bool foundEnglish = false;
                
                foreach (var v in voices)
                {
                    if (v.VoiceInfo.Culture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase))
                    {
                        _speechSynthesizer.SelectVoice(v.VoiceInfo.Name);
                        foundEnglish = true;
                        break;
                    }
                }

                // Nếu không có giọng nói tiếng Anh, dùng fallback
                if (!foundEnglish)
                {
                    _speechSynthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
                }
            }
            catch { _speechSynthesizer = null; }
        }

        private void DocTiengAnh()
        {
            if (_speechSynthesizer == null || _currentQuestion == null) return;
            if (string.IsNullOrEmpty(_currentQuestion.EnglishWord)) return;
            try
            {
                _speechSynthesizer.SpeakAsync(_currentQuestion.EnglishWord);
                string ph = !string.IsNullOrEmpty(_currentQuestion.Phonetic) ? $"/{_currentQuestion.Phonetic}/" : "";
                lblEnglish.Text = $"{ph}  {_currentQuestion.EnglishWord}";
                lblEnglish.Visible = true;
            }
            catch { }
        }

        #endregion

        #region ==================== Helpers ====================

        private void CapNhatTimerColor(int s)
        { 
            lblTimer.ForeColor = s <= 5
                ? Color.FromArgb(255, 100, 100)
                : s <= 10
                    ? Color.Yellow
                    : Color.White;
        }

        private string GetLivesDisplay(int lives, int maxLives)
        {
            string r = "";
            for (int i = 0; i < maxLives; i++) r += i < lives ? "❤️ " : " ";
            return r.Trim();
        }

        private void AmThanhTrLoiDung() { try { System.Media.SystemSounds.Asterisk.Play(); } catch { } }
        private void AmThanhBaoLoi() { try { System.Media.SystemSounds.Hand.Play(); } catch { } }

        private void DonDepTaiNguyen()
        {
            gameTimer?.Stop();
            _nextWordTimer?.Stop();
            _nextWordTimer?.Dispose();

            if (_speechSynthesizer != null)
            {
                try { _speechSynthesizer.Dispose(); } catch { }
                _speechSynthesizer = null;
            }
            if (picWord.Image != null)
            {
                try { picWord.Image.Dispose(); } catch { }
                picWord.Image = null;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DonDepTaiNguyen();
            base.OnFormClosing(e);
        }
        #endregion
    }
}
