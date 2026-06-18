using System.Data;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class WordDAL
    {
        // Lấy tất cả từ vựng kèm tên chủ đề
        public DataTable LayTatCaTu()
        {
            const string sql = "sp_GetAllWords";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        // Lấy từ vựng theo chủ đề
        public DataTable LayTuTheoChuDe(int topicID)
        {
            const string sql = "sp_GetWordsByTopic";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@tid", topicID));
        }

        // Lấy từ vựng theo chủ đề và độ khó, ngẫu nhiên
        public DataTable LayTuTheoChuDeVaDoKho(int topicID, int difficultyLevel, int count)
        {
            const string sql = "sp_GetWordsByTopicAndDifficulty";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure,
                new SqlParameter("@count", count),
                new SqlParameter("@topicID", topicID),
                new SqlParameter("@difficulty", difficultyLevel));
        }

        // Lấy từ vựng theo ID màn chơi (từ cố định)
        public DataTable LayTuTheoIDMucDo(int levelID)
        {
            const string sql = "sp_GetWordsByLevelID";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@LevelID", levelID));
        }

        // Tìm kiếm từ vựng với bộ lọc từ khóa, độ khó, hình ảnh
        public DataTable TimKiemTu(string keyword, string difficulty, string imageFilter)
        {
            int imgFilter = 0;
            if (imageFilter == "Có ảnh") imgFilter = 1;
            else if (imageFilter == "Chưa có ảnh") imgFilter = 2;

            const string sql = "sp_SearchWords";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure,
                new SqlParameter("@Keyword", keyword ?? ""),
                new SqlParameter("@Difficulty", string.IsNullOrEmpty(difficulty) || difficulty == "Tất cả" ? "" : difficulty),
                new SqlParameter("@ImageFilter", imgFilter));
        }

        // Lấy thông tin chi tiết một từ vựng theo ID
        public WordDTO LayTuTheoID(int wordID)
        {
            const string sql = "sp_GetWordByID";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@WordID", wordID));
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new WordDTO
            {
                WordID = Convert.ToInt32(row["WordID"]),
                TopicID = Convert.ToInt32(row["TopicID"]),
                TopicName = row["TopicName"].ToString() ?? "",
                EnglishWord = row["EnglishWord"].ToString() ?? "",
                Phonetic = row["Phonetic"].ToString() ?? "",
                VietnameseMeaning = row["VietnameseMeaning"].ToString() ?? "",
                DifficultyLevel = Convert.ToInt32(row["DifficultyLevel"]),
                EmojiIcon = row["EmojiIcon"].ToString() ?? "",
                ImagePath = row["ImagePath"].ToString() ?? "",
                ImageID = row["ImageID"] == DBNull.Value ? null : Convert.ToInt32(row["ImageID"])
            };
        }

        // Thêm từ vựng mới, trả về ID vừa tạo
        public int ThemTu(WordDTO word)
        {
            const string sql = "sp_InsertWord";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[]
                {
                new SqlParameter("@TopicID", word.TopicID),
                new SqlParameter("@EnglishWord", word.EnglishWord),
                new SqlParameter("@Phonetic", word.Phonetic),
                new SqlParameter("@VietnameseMeaning", word.VietnameseMeaning),
                new SqlParameter("@DifficultyLevel", word.DifficultyLevel),
                new SqlParameter("@EmojiIcon", word.EmojiIcon),
                new SqlParameter("@ImagePath", (object)word.ImagePath ?? DBNull.Value),
                new SqlParameter("@ImageID", (object)word.ImageID ?? DBNull.Value)}, null, CommandType.StoredProcedure));
        }

        // Cập nhật thông tin từ vựng
        public bool CapNhatTu(WordDTO word)
        {
            const string sql = "sp_UpdateWord";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
                {
                new SqlParameter("@WordID", word.WordID),
                new SqlParameter("@TopicID", word.TopicID),
                new SqlParameter("@EnglishWord", word.EnglishWord),
                new SqlParameter("@Phonetic", word.Phonetic),
                new SqlParameter("@VietnameseMeaning", word.VietnameseMeaning),
                new SqlParameter("@DifficultyLevel", word.DifficultyLevel),
                new SqlParameter("@EmojiIcon", word.EmojiIcon),
                new SqlParameter("@ImagePath", (object)word.ImagePath ?? DBNull.Value),
                new SqlParameter("@ImageID", (object)word.ImageID ?? DBNull.Value)}, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa từ vựng theo ID
        public bool XoaTu(int wordID)
        {
            const string sql = "sp_DeleteWord";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@WordID", wordID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Kiểm tra từ vựng có đang được dùng trong màn chơi không
        public bool IsWordInUse(int wordID)
        {
            const string sql = "sp_IsWordInUse";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@WordID", wordID) }, null, CommandType.StoredProcedure)) > 0;
        }

        // Kiểm tra từ tiếng Anh có bị trùng trong cùng chủ đề không
        public bool KiemTraTuTiengAnhTrung(int topicID, string englishWord, int excludeWordID = 0)
        {
            const string sql = "sp_IsDuplicateEnglishWord";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[]
                {
                new SqlParameter("@TopicID", topicID),
                new SqlParameter("@EnglishWord", englishWord),
                new SqlParameter("@ExcludeWordID", excludeWordID)}, null, CommandType.StoredProcedure)) > 0;
        }

        // Lấy danh sách từ vựng với bộ lọc đa tiêu chí
        public List<WordDTO> LayDanhSachTu(WordFilterDTO filter)
        {
            int imgFilter = 0;
            if (filter.ImageFilter == "Có ảnh") imgFilter = 1;
            else if (filter.ImageFilter == "Chưa có ảnh") imgFilter = 2;

            const string sql = "sp_GetWordsByFilter";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure,
                new SqlParameter("@TopicID", filter.TopicID),
                new SqlParameter("@Difficulty", filter.Difficulty == "Tất cả" ? "" : filter.Difficulty),
                new SqlParameter("@ImageFilter", imgFilter),
                new SqlParameter("@Keyword", filter.Keyword ?? ""));

            // Map DataTable sang danh sách DTO
            var list = new List<WordDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new WordDTO
                {
                    WordID = Convert.ToInt32(row["WordID"]),
                    TopicID = Convert.ToInt32(row["TopicID"]),
                    TopicName = row["TopicName"].ToString() ?? "",
                    EnglishWord = row["EnglishWord"].ToString() ?? "",
                    Phonetic = row["Phonetic"].ToString() ?? "",
                    VietnameseMeaning = row["VietnameseMeaning"].ToString() ?? "",
                    DifficultyLevel = Convert.ToInt32(row["DifficultyLevel"]),
                    EmojiIcon = row["EmojiIcon"].ToString() ?? "",
                    ImagePath = row["ImagePath"].ToString() ?? "",
                    ImageID = row["ImageID"] == DBNull.Value ? null : Convert.ToInt32(row["ImageID"])
                });
            }

            return list;
        }
    }
}
