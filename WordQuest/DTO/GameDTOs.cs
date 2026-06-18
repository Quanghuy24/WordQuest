// DTO/GameDTOs.cs - Gộp tất cả DTO liên quan đến game session
using System;
using System.Collections.Generic;

namespace WordQuest.DTO
{
    // Luật chơi - đọc từ DB, dùng để khởi tạo GameBUS
    public class GameRuleDTO
    {
        public int RuleID { get; set; }
        public int Lives { get; set; } = 3;
        public int TimeLđiểmit { get; set; } = 30;        // Giây
        public int BaseScore { get; set; } = 10;
        public int StreakBonus { get; set; } = 5;       // Thưởng khi streak >= 3
        public int HintPenalty { get; set; } = 3;       // Điểm bị trừ khi dùng hint
        public int Star1Threshold { get; set; } = 5;
        public int Star2Threshold { get; set; } = 8;
        public int Star3Threshold { get; set; } = 10;
        public int QuestionCount { get; set; } = 15;
    }

    // Toàn bộ trạng thái game tại một thời điểm â€” GUI chỉ đọc, không tự tính
    public class GameStateDTO
    {
        // Thông tin game
        public int PlayerID { get; set; }
        public int TopicID { get; set; }
        public int LevelID { get; set; }
        public int LevelNum { get; set; }
        public string GameMode { get; set; } = "";

        // Thống kê chính
        public int Lives { get; set; }
        public int MaxLives { get; set; }
        public int Score { get; set; }
        public int Streak { get; set; }
        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }

        // Timer
        public int RemainingTimeMs { get; set; }
        public int MaxTimeMs { get; set; }
        public int TotalTimeSpentMs { get; set; }

        // Trạng thái hiện tại
        public int CurrentWordIndex { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsWaitingForNext { get; set; }

        // Dữ liệu câu hiện tại
        public List<char> CurrentInput { get; set; } = new();
        public List<char> AvailableLetters { get; set; } = new();
        public string CorrectAnswer { get; set; } = "";
        public string CurrentQuestionText { get; set; } = "";
        public string LivesDisplay { get; set; } = "";

        // Hint
        public int HintUsedCount { get; set; }

        // Helper properties
        public int RemainingSeconds => (int)Math.Ceiling(RemainingTimeMs / 1000.0);
        public int ProgressPercent => MaxTimeMs > 0 ? (RemainingTimeMs * 100 / MaxTimeMs) : 0;
        public bool IsStreaking => Streak >= 3;
        public bool IsLastChance => Lives == 1;

        public GameStateDTO Clone()
        {
            return new GameStateDTO
            {
                PlayerID = this.PlayerID,
                TopicID = this.TopicID,
                LevelID = this.LevelID,
                LevelNum = this.LevelNum,
                GameMode = this.GameMode,
                Lives = this.Lives,
                MaxLives = this.MaxLives,
                Score = this.Score,
                Streak = this.Streak,
                CorrectCount = this.CorrectCount,
                TotalQuestions = this.TotalQuestions,
                RemainingTimeMs = this.RemainingTimeMs,
                MaxTimeMs = this.MaxTimeMs,
                TotalTimeSpentMs = this.TotalTimeSpentMs,
                CurrentWordIndex = this.CurrentWordIndex,
                IsAnswered = this.IsAnswered,
                IsGameOver = this.IsGameOver,
                IsWaitingForNext = this.IsWaitingForNext,
                CurrentInput = new List<char>(this.CurrentInput),
                AvailableLetters = new List<char>(this.AvailableLetters),
                CorrectAnswer = this.CorrectAnswer,
                CurrentQuestionText = this.CurrentQuestionText,
                HintUsedCount = this.HintUsedCount
            };
        }
    }

    // Kết quả cuối game - truyền sang frmWin để hiển thị
    public class GameResultDTO
    {
        public int PlayerID { get; set; }
        public int TopicID { get; set; }
        public int LevelNum { get; set; }
        public int Score { get; set; }
        public int Stars { get; set; }
        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public int TimeTaken { get; set; }  // Giây
        public int Streak { get; set; }

        public double Accuracy => TotalQuestions > 0 ? (CorrectCount * 100.0 / TotalQuestions) : 0;
        public string Grade => Stars switch
        {
            3 => "Xuất sắc! 🏆",
            2 => "Tốt! 👍",
            1 => "Khá! 📝",
            _ => "Cần cố gắng hơn 😕"
        };
    }

    // DTO câu hỏi hiện tại để GUI render
    public class CurrentQuestionDTO
    {
        public int WordID { get; set; }
        public string QuestionText { get; set; } = "";
        public string CorrectAnswer { get; set; } = "";
        public string EnglishWord { get; set; } = "";
        public string VietnameseMeaning { get; set; } = "";
        public string Phonetic { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public int? ImageID { get; set; }
        public string EmojiIcon { get; set; } = "";
        public int CurrentIndex { get; set; }
        public int TotalQuestions { get; set; }
    }

    // Kết quả trả lời â€” GUI chỉ đọc KiemTraDung và CorrectAnswer
    public class AnswerOutcomeDTO
    {
        public bool KiemTraDung { get; set; }
        public string CorrectAnswer { get; set; } = "";
    }

    // Trạng thái ô chữ (các chữ cái đã điền / còn lại)
    public class AnswerStateDTO
    {
        public string CorrectAnswer { get; set; } = "";
        public List<string> FilledLetters { get; set; } = new();
        public List<string> AvailableLetters { get; set; } = new();
    }

    // Kết quả gợi ý â€” chứa đủ SlotIndex + Letter để GUI highlight đúng ô
    public class HintResultDTO
    {
        public int SlotIndex { get; set; }
        public char Letter { get; set; }
    }

    // Thông tin khi hết giờ â€” GUI nhận đáp án đúng từ đây, không query lại BUS
    public class TimeOutDTO
    {
        public string CorrectAnswer { get; set; } = "";
    }
}

