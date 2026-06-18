using System;
using System.Data;
using Microsoft.Data.SqlClient;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class PlayerProgressDAL
    {
        // Lấy tiến độ người chơi trong một chủ đề
        public DataTable LayTienTrinhNguoiChoi(int playerID, int topicID)
        {
            const string sql = "sp_GetPlayerProgress";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure,
                new SqlParameter("@PlayerID", playerID),
                new SqlParameter("@TopicID", topicID));
        }

        // Lấy tất cả tiến độ của người chơi kèm tên chủ đề
        public DataTable LayTatCaTienTrinh(int playerID)
        {
            const string sql = "sp_GetAllPlayerProgress";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@PlayerID", playerID));
        }

        // Lưu hoặc cập nhật tiến độ sau khi hoàn thành màn chơi
        public bool SaveOrUpdateProgress(int playerID, int topicID, int levelNum, int stars, int bestScore)
        {
            const string sql = "sp_SaveOrUpdateProgress";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@PlayerID", playerID),
                new SqlParameter("@TopicID", topicID),
                new SqlParameter("@LevelNum", levelNum),
                new SqlParameter("@Stars", stars),
                new SqlParameter("@BestScore", bestScore)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa tất cả tiến độ của người chơi
        public bool XoaTienTrinhNguoiChoi(int playerID)
        {
            const string sql = "sp_DeleteProgressByPlayer";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@PlayerID", playerID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa tiến độ theo chủ đề (dùng khi xóa chủ đề)
        public bool XoaTienTrinhChuDe(int topicID)
        {
            const string sql = "sp_DeleteProgressByTopic";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@TopicID", topicID) }, null, CommandType.StoredProcedure) > 0;
        }
    }
}
