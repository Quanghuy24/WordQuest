// DTO/CatalogDTOs.cs - Gộp tất cả DTO dữ liệu danh mục (Topic, Level, Word)
namespace WordQuest.DTO
{
    // Chủ đề từ vựng
    public class TopicDTO
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; } = string.Empty;
        public string TopicIcon { get; set; } = string.Empty;
        public int StarsToUnlock { get; set; }
        public int? ParentID { get; set; }
        public int SortOrder { get; set; }
    }

    
    // Màn (Level) trong một chủ đề
    public class LevelDTO
    {
        public int LevelID { get; set; }
        public int TopicID { get; set; }
        public int LevelNum { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public string DifficultyLevel { get; set; } = string.Empty; // Dễ, Trung bình, Khó
        public string Mode { get; set; } = string.Empty;            // Random hoặc Fixed
        public int QuestionCount { get; set; }
    }

    // Từ vựng
    public class WordDTO
    {
        public int WordID { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; } = "";
        public string EnglishWord { get; set; } = string.Empty;
        public string Phonetic { get; set; } = string.Empty;
        public string VietnameseMeaning { get; set; } = string.Empty;
        public int DifficultyLevel { get; set; } // 1: Dễ, 2: TB, 3: Khó
        public string EmojiIcon { get; set; } = string.Empty;

        /// <summary>ID ảnh trong bảng Images (VARBINARY). Null = chưa có ảnh.</summary>
        public int? ImageID { get; set; }

        /// <summary>Giữ lại để tương thích ngược trong giai đoạn migration.</summary>
        public string ImagePath { get; set; } = string.Empty;
    }

    // Bộ lọc từ vựng (dùng trong Admin)
    public class WordFilterDTO
    {
        public int TopicID { get; set; } = -1;
        public string Difficulty { get; set; } = "Tất cả";
        public string ImageFilter { get; set; } = "Tất cả";
        public string Keyword { get; set; } = "";
    }
}

