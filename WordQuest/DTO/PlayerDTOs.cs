// DTO/PlayerDTOs.cs - Gộp tất cả DTO liên quan đến người chơi
using System;

namespace WordQuest.DTO
{
    // Thông tin người chơi
    public class PlayerDTO
    {
        public int PlayerID { get; set; }
        public string Username { get; set; } = string.Empty;
        public int TotalScore { get; set; }
        public int TotalStars { get; set; }
        public int DayStreak { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastPlayed { get; set; }
    }

    // Tiến độ người chơi trong từng màn
    public class PlayerProgressDTO
    {
        public int ProgressID { get; set; }
        public int PlayerID { get; set; }
        public int TopicID { get; set; }
        public int LevelNum { get; set; }
        public int Stars { get; set; }
        public int BestScore { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    // Lịch sử điểm số sau mỗi lần chơi
    public class ScoreHistoryDTO
    {
        public int HistoryID { get; set; }
        public int PlayerID { get; set; }
        public int TopicID { get; set; }
        public int LevelNum { get; set; }
        public int Score { get; set; }
        public int Stars { get; set; }
        public int TimeTaken { get; set; }
        public DateTime PlayedAt { get; set; }
    }
}

