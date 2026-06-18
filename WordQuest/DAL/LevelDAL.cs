using System.Data;
using Microsoft.Data.SqlClient;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class LevelDAL
    {
        // Lấy tất cả màn chơi theo chủ đề
        public DataTable LayMucDoTheoChuDe(int topicID)
        {
            const string sql = "sp_GetLevelsByTopic";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@TopicID", topicID));
        }

        // Lấy thông tin màn chơi theo ID
        public LevelDTO LayMucDoTheoID(int levelID)
        {
            const string sql = "sp_GetLevelByID";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@LevelID", levelID));
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new LevelDTO
            {
                LevelID = Convert.ToInt32(row["LevelID"]),
                TopicID = Convert.ToInt32(row["TopicID"]),
                LevelNum = Convert.ToInt32(row["LevelNum"]),
                LevelName = row["LevelName"].ToString() ?? "",
                DifficultyLevel = row["DifficultyLevel"].ToString() ?? "",
                Mode = row["Mode"].ToString() ?? "",
                QuestionCount = Convert.ToInt32(row["QuestionCount"])
            };
        }

        // Thêm màn chơi mới, trả về ID vừa tạo
        public int ThemMucDo(LevelDTO level)
        {
            const string sql = "sp_InsertLevel";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[]
            {
                new SqlParameter("@TopicID", level.TopicID),
                new SqlParameter("@LevelNum", level.LevelNum),
                new SqlParameter("@LevelName", level.LevelName),
                new SqlParameter("@DifficultyLevel", level.DifficultyLevel),
                new SqlParameter("@Mode", level.Mode),
                new SqlParameter("@QuestionCount", level.QuestionCount)
            }, null, CommandType.StoredProcedure));
        }

        // Cập nhật thông tin màn chơi
        public bool CapNhatMucDo(LevelDTO level)
        {
            const string sql = "sp_UpdateLevel";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@LevelID", level.LevelID),
                new SqlParameter("@LevelName", level.LevelName),
                new SqlParameter("@DifficultyLevel", level.DifficultyLevel),
                new SqlParameter("@Mode", level.Mode),
                new SqlParameter("@QuestionCount", level.QuestionCount)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa màn chơi theo ID
        public bool XoaMucDo(int levelID)
        {
            const string sql = "sp_DeleteLevel";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@LevelID", levelID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Thêm từ vào danh sách cố định của màn chơi
        public bool ThemTuVaoMucDo(int levelID, int wordID)
        {
            const string sql = "sp_AddWordToLevel";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@LevelID", levelID),
                new SqlParameter("@WordID", wordID)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa một từ khỏi danh sách cố định của màn chơi
        public bool XoaTuKhoiMucDo(int levelID, int wordID)
        {
            const string sql = "sp_RemoveWordFromLevel";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@LevelID", levelID),
                new SqlParameter("@WordID", wordID)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa tất cả từ khỏi danh sách cố định của màn chơi
        public bool XoaTatCaTuMucDo(int levelID)
        {
            const string sql = "sp_ClearLevelWords";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@LevelID", levelID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Lấy danh sách từ cố định của màn chơi
        public DataTable LayTuCoDinhMucDo(int levelID)
        {
            const string sql = "sp_GetFixedWordsForLevel";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter[] { new SqlParameter("@LevelID", levelID) });
        }
    }
}
