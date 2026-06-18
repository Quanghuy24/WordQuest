using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WordQuest.DAL;
using WordQuest.DTO;

namespace WordQuest.BUS
{
    public class GameBUS
    {
        #region ==================== Events ====================

        public event Action<GameStateDTO> OnStateChanged;
        public event Action<AnswerOutcomeDTO> OnAnswerCompleted;
        public event Action<TimeOutDTO> OnTimeOut;
        public event Action<GameResultDTO> OnGameEnded;
        public event Action OnTuTiepTheoRequested;
        public event Action<int, int> OnScoreChanged;

        #endregion

        #region ==================== Fields ====================

        private GameStateDTO _state;
        private GameRuleDTO _rule;
        private DataTable _wordsTable;

        private System.Threading.Timer _logicTimer;
        private readonly object _lockObject = new object();

        private readonly WordDAL _wordDAL;
        private readonly LevelDAL _levelDAL;
        private readonly GameRuleDAL _ruleDAL;
        private readonly PlayerProgressDAL _progressDAL;
        private readonly ScoreHistoryDAL _scoreDAL;
        private readonly PlayerDAL _playerDAL;

        private int _currentWordIndex;

        private const int TIMER_INTERVAL_MS = 50; 

        #endregion

        #region ==================== Constructor ====================

        public GameBUS()
        {
            _wordDAL = new WordDAL();
            _levelDAL = new LevelDAL();
            _ruleDAL = new GameRuleDAL();
            _progressDAL = new PlayerProgressDAL();
            _scoreDAL = new ScoreHistoryDAL();
            _playerDAL = new PlayerDAL();
            _state = new GameStateDTO();
        }

        #endregion

        #region ==================== Public Methods ====================

        //Khởi tạo game mới.
        public async Task KhoiTaoGameAsync(int levelID, string gameMode)
        {
            try
            {
                _rule = await Task.Run(() => _ruleDAL.LayLuatGame());
                _wordsTable = await Task.Run(() => GetWordsForLevel(levelID));

                if (_wordsTable == null || _wordsTable.Rows.Count == 0)
                    throw new Exception("Không có câu hỏi cho màn chơi này!");

                _state = new GameStateDTO
                {
                    PlayerID = 0,
                    TopicID = 0,
                    LevelID = levelID,
                    LevelNum = 0,
                    GameMode = gameMode,

                    Lives = _rule.Lives,
                    MaxLives = _rule.Lives,
                    Score = 0,
                    Streak = 0,
                    CorrectCount = 0,
                    TotalQuestions = _wordsTable.Rows.Count,
                    HintUsedCount = 0,

                    RemainingTimeMs = _rule.TimeLđiểmit * 1000,
                    MaxTimeMs = _rule.TimeLđiểmit * 1000,
                    TotalTimeSpentMs = 0,

                    CurrentWordIndex = 0,
                    IsAnswered = false,
                    IsGameOver = false,
                    IsWaitingForNext = false,

                    CurrentInput = new List<char>(),
                    AvailableLetters = new List<char>(),
                    CorrectAnswer = "",
                    CurrentQuestionText = ""
                };

                _currentWordIndex = 0;
                LoadCurrentWord();
                StartLogicTimer();
                OnStateChanged?.Invoke(_state);
            }
            catch (Exception ex)
            {
                throw new Exception($"Khởi tạo game thất bại: {ex.Message}", ex);
            }
        }

        /// Thêm chữ cái vào câu trả lời.
        public AnswerResult ThemKyTu(char letter)
        {
            lock (_lockObject)
            {
                if (_state.IsAnswered || _state.IsGameOver || _state.IsWaitingForNext)
                    return AnswerResult.Invalid;

                if (_state.CurrentInput.Count >= _state.CorrectAnswer.Length)
                    return AnswerResult.Full;

                _state.CurrentInput.Add(letter);
                XoaChuCaiCoSan(letter);
                OnStateChanged?.Invoke(_state);

                if (_state.CurrentInput.Count == _state.CorrectAnswer.Length)
                    return CheckAnswer();

                return AnswerResult.Added;
            }
        }

        /// Xóa chữ cái từ vị trí index trở đi.
        public void XoaKyTuTu(int startIndex)
        {
            lock (_lockObject)
            {
                if (_state.IsAnswered || _state.IsGameOver || _state.IsWaitingForNext) return;

                for (int i = _state.CurrentInput.Count - 1; i >= startIndex; i--)
                {
                    char c = _state.CurrentInput[i];
                    _state.CurrentInput.RemoveAt(i);
                    TraLaiChuCai(c);
                }

                OnStateChanged?.Invoke(_state);
            }
        }

        /// Sử dụng gợi ý.
        /// Trả về <see cref="HintResultDTO"/> chứa SlotIndex + Letter để GUI highlight đúng ô,
        /// hoặc null nếu không thể dùng gợi ý.
        public HintResultDTO SuDungGoiY()
        {
            lock (_lockObject)
            {
                if (_state.IsAnswered || _state.IsGameOver || _state.IsWaitingForNext)
                    return null;

                int nextIndex = _state.CurrentInput.Count;
                if (nextIndex >= _state.CorrectAnswer.Length)
                    return null;

                char hintChar = _state.CorrectAnswer[nextIndex];

                _state.CurrentInput.Add(hintChar);
                XoaChuCaiCoSan(hintChar);
                _state.Score = Math.Max(0, _state.Score - _rule.HintPenalty);
                _state.HintUsedCount++;

                OnScoreChanged?.Invoke(_state.Score, 0);
                OnStateChanged?.Invoke(_state);

                if (_state.CurrentInput.Count == _state.CorrectAnswer.Length)
                    CheckAnswer();

                // Trả về DTO — GUI không cần tự tính index
                return new HintResultDTO
                {
                    SlotIndex = nextIndex,
                    Letter = hintChar
                };
            }
        }

        /// Chuyển sang câu tiếp theo.
        public void TuTiepTheo()
        {
            lock (_lockObject)
            {
                if (!_state.IsWaitingForNext) return;

                _currentWordIndex++;
                _state.IsWaitingForNext = false;

                if (_currentWordIndex >= _wordsTable.Rows.Count)
                {
                    EndGame();
                }
                else
                {
                    LoadCurrentWord();
                    ResetTimer();
                    OnStateChanged?.Invoke(_state);
                    OnTuTiepTheoRequested?.Invoke();
                }
            }
        }

        /// Lấy thông tin câu hỏi hiện tại để GUI render. //
        public CurrentQuestionDTO LayCauHoiHienTai()
        {
            if (_wordsTable == null || _currentWordIndex >= _wordsTable.Rows.Count)
                return null;

            var row = _wordsTable.Rows[_currentWordIndex];
            return new CurrentQuestionDTO
            {
                WordID = Convert.ToInt32(row["WordID"]),
                QuestionText = GetQuestionText(row),
                CorrectAnswer = _state.CorrectAnswer,
                EnglishWord = row["EnglishWord"]?.ToString() ?? "",
                VietnameseMeaning = row["VietnameseMeaning"]?.ToString() ?? "",
                Phonetic = row["Phonetic"]?.ToString() ?? "",
                ImagePath = row["ImagePath"]?.ToString() ?? "",
                ImageID = row.Table.Columns.Contains("ImageID") && row["ImageID"] != DBNull.Value ? Convert.ToInt32(row["ImageID"]) : (int?)null,
                EmojiIcon = row["EmojiIcon"]?.ToString() ?? "📝",
                CurrentIndex = _currentWordIndex + 1,
                TotalQuestions = _state.TotalQuestions
            };
        }

        public GameStateDTO LayTrangThaiHienTai()
        {
            lock (_lockObject) { return _state.Clone(); }
        }

        public List<char> LayKyTuCoSan()
        {
            lock (_lockObject) { return new List<char>(_state.AvailableLetters); }
        }

        public List<char> LayNhapLieuHienTai()
        {
            lock (_lockObject) { return new List<char>(_state.CurrentInput); }
        }

        public string LayDapAnDung() => _state.CorrectAnswer;

        public async Task<bool> LuuKetQuaGameAsync(int playerID, int topicID, int levelNum)
        {
            if (playerID <= 0) return false;

            try
            {
                int score = _state.Score;
                int stars = CalculateStars();
                int timeTaken = _state.TotalTimeSpentMs / 1000;

                return await Task.Run(() =>
                {
                    // 1. Lưu/cập nhật tiến độ (chỉ ghi đè nếu sao hoặc điểm cao hơn)
                    _progressDAL.SaveOrUpdateProgress(playerID, topicID, levelNum, stars, score);

                    // 2. Ghi lịch sử điểm (mỗi lần chơi một bản ghi)
                    _scoreDAL.InsertScoreHistory(new DTO.ScoreHistoryDTO
                    {
                        PlayerID = playerID,
                        TopicID = topicID,
                        LevelNum = levelNum,
                        Score = score,
                        Stars = stars,
                        TimeTaken = timeTaken
                    });

                    // 3. Cộng dồn điểm và sao vào thống kê người chơi
                    _playerDAL.CapNhatChiSoNguoiChoi(playerID, score, stars);

                    return true;
                });
            }
            catch { return false; }
        }

        public void HuyGame()
        {
            StopLogicTimer();
            _state.IsGameOver = true;
        }

        #endregion

        #region ==================== Private Logic ====================

        private DataTable GetWordsForLevel(int levelID)
        {
            var level = _levelDAL.LayMucDoTheoID(levelID);
            if (level == null) return null;

            _state.LevelNum = level.LevelNum;
            _state.TopicID = level.TopicID;

            if (level.Mode == "Fixed")
                return _levelDAL.LayTuCoDinhMucDo(levelID);

            int difficulty = 1;
            if (int.TryParse(level.DifficultyLevel, out int diffNum))
                difficulty = diffNum; // DB lưu số
            else
                difficulty = level.DifficultyLevel switch
                {
                    "Dễ" => 1,
                    "Trung bình" => 2,
                    "Khó" => 3,
                    _ => 1
                };
            return _wordDAL.LayTuTheoChuDeVaDoKho(level.TopicID, difficulty, level.QuestionCount);
        }

        private void LoadCurrentWord()
        {
            var row = _wordsTable.Rows[_currentWordIndex];

            _state.CurrentWordIndex = _currentWordIndex;
            _state.CorrectAnswer = LayDapAnDung(row).Trim().ToUpper().Replace(" ", ""); 
            _state.CurrentQuestionText = GetQuestionText(row);
            _state.CurrentInput = new List<char>();
            _state.IsAnswered = false;
            _state.IsWaitingForNext = false;
            _state.AvailableLetters = TaoChuNgauNhien(_state.CorrectAnswer);

            // Cập nhật LivesDisplay mỗi khi load câu mới
            _state.LivesDisplay = BuildLivesDisplay(_state.Lives, _state.MaxLives);
        }

        private string GetQuestionText(DataRow row)
            => _state.GameMode == "VI"
                ? row["EnglishWord"]?.ToString() ?? ""
                : row["VietnameseMeaning"]?.ToString() ?? "";

        private string LayDapAnDung(DataRow row)
            => _state.GameMode == "VI"
                ? row["VietnameseMeaning"]?.ToString() ?? ""
                : row["EnglishWord"]?.ToString() ?? "";

        private List<char> TaoChuNgauNhien(string correctAnswer)
        {
            var random = new Random();
            var letters = correctAnswer.Where(c => c != ' ').ToList();

            const string extra = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int target = Math.Min(12, correctAnswer.Length + 6);

            while (letters.Count < target)
                letters.Add(extra[random.Next(extra.Length)]);

            return letters.OrderBy(_ => random.Next()).ToList();
        }

        private void XoaChuCaiCoSan(char letter)
        {
            int idx = _state.AvailableLetters.IndexOf(letter);
            if (idx >= 0) _state.AvailableLetters.RemoveAt(idx);
        }

        private void TraLaiChuCai(char letter)
        {
            _state.AvailableLetters.Add(letter);
            _state.AvailableLetters = _state.AvailableLetters.OrderBy(_ => Guid.NewGuid()).ToList();
        }

        private AnswerResult CheckAnswer()
        {
            _state.IsAnswered = true;
            bool KiemTraDung = new string(_state.CurrentInput.ToArray()) == _state.CorrectAnswer;

            if (KiemTraDung) TinhDiemTrLoiDung();
            else TinhDiemTrLoiSai();

            // Đóng gói kết quả vào DTO — GUI không cần tự suy luận
            OnAnswerCompleted?.Invoke(new AnswerOutcomeDTO
            {
                KiemTraDung = KiemTraDung,
                CorrectAnswer = _state.CorrectAnswer
            });

            OnStateChanged?.Invoke(_state);
            return KiemTraDung ? AnswerResult.Correct : AnswerResult.Wrong;
        }

        private void TinhDiemTrLoiDung()
        {
            int streakBonus = (_state.Streak + 1 >= 3) ? _rule.StreakBonus : 0;
            _state.Score += _rule.BaseScore + streakBonus;
            _state.Streak++;
            _state.CorrectCount++;
            _state.IsWaitingForNext = true;

            OnScoreChanged?.Invoke(_state.Score, streakBonus);
        }

        private void TinhDiemTrLoiSai()
        {
            _state.Streak = 0;
            _state.Lives--;
            _state.LivesDisplay = BuildLivesDisplay(_state.Lives, _state.MaxLives);

            OnScoreChanged?.Invoke(_state.Score, 0);

            if (_state.Lives <= 0) EndGame();
            else _state.IsWaitingForNext = true;
        }

        private void XuLyHetGio()
        {
            lock (_lockObject)
            {
                if (_state.IsAnswered || _state.IsGameOver || _state.IsWaitingForNext) return;

                _state.IsAnswered = true;
                _state.Lives--;
                _state.LivesDisplay = BuildLivesDisplay(_state.Lives, _state.MaxLives);

                // Đóng gói đáp án đúng vào DTO — GUI không cần query lại
                OnTimeOut?.Invoke(new TimeOutDTO { CorrectAnswer = _state.CorrectAnswer });

                if (_state.Lives <= 0) EndGame();
                else
                {
                    _state.IsWaitingForNext = true;
                    OnStateChanged?.Invoke(_state);
                }
            }
        }

        private void EndGame()
        {
            StopLogicTimer();
            _state.IsGameOver = true;

            OnGameEnded?.Invoke(new GameResultDTO
            {
                Score = _state.Score,
                Stars = CalculateStars(),
                CorrectCount = _state.CorrectCount,
                TotalQuestions = _state.TotalQuestions,
                TimeTaken = _state.TotalTimeSpentMs / 1000
            });
        }

        private int CalculateStars()
        {
            return TinhSoSao(_state.CorrectCount, _rule.Star1Threshold, _rule.Star2Threshold, _rule.Star3Threshold);
        }

        // Hàm logic thuần để tính toán số sao dựa trên ngưỡng của GameRule.
        // Public static để Unit Test mà không cần khởi tạo GameBUS phức tạp.
        public static int TinhSoSao(int correctCount, int t1, int t2, int t3)
        {
            if (correctCount >= t3) return 3;
            if (correctCount >= t2) return 2;
            if (correctCount >= t1) return 1;
            return 0;
        }

        private void ResetTimer()
        {
            _state.RemainingTimeMs = _rule.TimeLđiểmit * 1000;
            _state.IsAnswered = false;
        }

        // Tạo chuỗi hiển thị mạng sống - BUS tính sẵn, GUI chỉ gán Text.
        private static string BuildLivesDisplay(int lives, int maxLives)
        {
            var filled = string.Concat(Enumerable.Repeat("❤", Math.Max(0, lives)));
            var empty = string.Concat(Enumerable.Repeat(" ", Math.Max(0, maxLives - lives)));
            return filled + empty;
        }

        #endregion

        #region ==================== Timer Logic ====================

        private void StartLogicTimer()
            => _logicTimer = new System.Threading.Timer(DemThoiGian, null, 0, TIMER_INTERVAL_MS);

        private void StopLogicTimer()
        {
            _logicTimer?.Dispose();
            _logicTimer = null;
        }

        private void DemThoiGian(object state)
        {
            lock (_lockObject)
            {
                if (_state.IsAnswered || _state.IsGameOver || _state.IsWaitingForNext) return;

                if (_state.RemainingTimeMs > 0) 
                {
                    _state.RemainingTimeMs -= TIMER_INTERVAL_MS; 
                    _state.TotalTimeSpentMs += TIMER_INTERVAL_MS;
                    OnStateChanged?.Invoke(_state);
                }

                if (_state.RemainingTimeMs <= 0)
                    XuLyHetGio();
            }
        }

        #endregion
    }

    #region ==================== Enums ====================

    //Kết quả của việc thêm chữ cái
    public enum AnswerResult
    {
        Added,      // Thêm thành công, chưa đủ
        Full,       // Slot đã đầy
        Correct,    // Đúng
        Wrong,      // Sai
        Invalid     // Không hợp lệ
    }

    #endregion
}
