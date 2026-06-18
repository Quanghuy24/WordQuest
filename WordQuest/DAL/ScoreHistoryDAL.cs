using System;
using System.Data;
using Microsoft.Data.SqlClient;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class ScoreHistoryDAL
    {
        // Lưu lịch sử điểm sau mỗi lần chơi
        public bool InsertScoreHistory(ScoreHistoryDTO history)
        {
            const string sql = "sp_InsertScoreHistory";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
                {
                new SqlParameter("@PlayerID", history.PlayerID),
                new SqlParameter("@TopicID", history.TopicID),
                new SqlParameter("@LevelNum", history.LevelNum),
                new SqlParameter("@Score", history.Score),
                new SqlParameter("@Stars", history.Stars),
                new SqlParameter("@TimeTaken", history.TimeTaken)
                }, null, CommandType.StoredProcedure) > 0;
        }

        // Lấy lịch sử điểm của một người chơi
        public DataTable LayLichSuDiemNguoiChoi(int playerID)
        {
            const string sql = "sp_GetScoreHistoryByPlayer";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@PlayerID", playerID));
        }

        // Lấy bảng xếp hạng toàn server
        public DataTable LayBangXepHang()
        {
            const string sql = "sp_GetLeaderboard";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        // Xóa lịch sử điểm của người chơi
        public bool XoaLichSuDiemNguoiChoi(int playerID)
        {
            const string sql = "sp_DeleteScoreHistoryByPlayer";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@PlayerID", playerID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Lấy tổng số game đã chơi
        public int LayTongSoGame()
        {
            const string sql = "sp_GetTotalGames";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, null, null, CommandType.StoredProcedure));
        }
    }
}
