using System;
using System.Data;
using Microsoft.Data.SqlClient;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class PlayerDAL
    {
        // Lấy tất cả người chơi, sắp xếp theo điểm cao nhất
        public DataTable LayTatCaNguoiChoi()
        {
            const string sql = "sp_GetAllPlayers";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        // Lấy thông tin người chơi theo ID
        public PlayerDTO LayNguoiChoiTheoID(int playerID)
        {
            const string sql = "sp_GetPlayerByID";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@id", playerID));
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new PlayerDTO
            {
                PlayerID = Convert.ToInt32(row["PlayerID"]),
                Username = row["Username"].ToString() ?? "",
                TotalScore = Convert.ToInt32(row["TotalScore"]),
                TotalStars = Convert.ToInt32(row["TotalStars"]),
                DayStreak = Convert.ToInt32(row["DayStreak"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                LastPlayed = row["LastPlayed"] == DBNull.Value ? null : Convert.ToDateTime(row["LastPlayed"])
            };
        }

        // Lấy thông tin người chơi theo tên đăng nhập
        public PlayerDTO LayNguoiChoiTheoTen(string username)
        {
            const string sql = "sp_GetPlayerByUsername";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@name", username));
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new PlayerDTO
            {
                PlayerID = Convert.ToInt32(row["PlayerID"]),
                Username = row["Username"].ToString() ?? "",
                TotalScore = Convert.ToInt32(row["TotalScore"]),
                TotalStars = Convert.ToInt32(row["TotalStars"]),
                DayStreak = Convert.ToInt32(row["DayStreak"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                LastPlayed = row["LastPlayed"] == DBNull.Value ? null : Convert.ToDateTime(row["LastPlayed"])
            };
        }

        // Thêm người chơi mới, trả về ID vừa tạo
        public int InsertPlayer(string username)
        {
            const string sql = "sp_InsertPlayer";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@name", username) }, null, CommandType.StoredProcedure));
        }

        // Cập nhật điểm và sao sau mỗi game
        public bool CapNhatChiSoNguoiChoi(int playerID, int scoreEarned, int starsEarned)
        {
            const string sql = "sp_UpdatePlayerStats";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@id", playerID),
                new SqlParameter("@score", scoreEarned),
                new SqlParameter("@stars", starsEarned)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Cập nhật chuỗi ngày chơi liên tiếp
        public bool CapNhatChuoiNgayNguoiChoi(int playerID, int newStreak)
        {
            const string sql = "sp_UpdatePlayerStreak";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@id", playerID),
                new SqlParameter("@streak", newStreak)
            }, null, CommandType.StoredProcedure) > 0;
        }

        /// <summary>
        /// Tính và cập nhật DayStreak theo ngày thực sự:
        /// - Cùng ngày hôm nay → giữ nguyên streak (không cộng 2 lần)
        /// - Ngày hôm qua     → tăng streak + 1
        /// - Bỏ 2+ ngày      → reset về 1 (bắt đầu lại)
        /// - Chưa bao giờ chơi → khởi tạo streak = 1
        /// Trả về (newStreak, isNewDay): newStreak = giá trị sau cập nhật,
        /// isNewDay = true nếu thực sự chuyển ngày (dùng để thông báo UI).
        /// </summary>
        public (int newStreak, bool isNewDay) CapNhatChuoiNgay(int playerID)
        {
            // Ly LanChoiCuoi vA NgayStreak hin ti
            const string selectSql = "sp_GetPlayerStreakInfo";
            var dt = DatabaseHelper.ExecuteQuery(selectSql, CommandType.StoredProcedure, new SqlParameter("@id", playerID));
            if (dt.Rows.Count == 0) return (0, false);

            var row = dt.Rows[0];
            int currentStreak = Convert.ToInt32(row["NgayStreak"]);
            DateTime today = DateTime.Today;

            DateTime? lastPlayed = row["LanChoiCuoi"] == DBNull.Value ? null : Convert.ToDateTime(row["LanChoiCuoi"]);
            var (newStreak, isNewDay) = CalculateNewStreak(lastPlayed, currentStreak, DateTime.Today);

            UpdateStreakInDB(playerID, newStreak);
            return (newStreak, isNewDay);
        }

        /// <summary>
        /// Hàm logic thuần (Pure logic) để tính toán streak.
        /// Đưa ra public static để dễ dàng Unit Test mà không cần DB.
        /// </summary>
        public static (int newStreak, bool isNewDay) CalculateNewStreak(DateTime? lastPlayed, int currentStreak, DateTime today)
        {
            if (lastPlayed == null)
                return (1, true);

            int daysDiff = (today.Date - lastPlayed.Value.Date).Days;

            if (daysDiff == 0) return (currentStreak, false); // Cùng ngày
            if (daysDiff == 1) return (currentStreak + 1, true); // Chơi liên tiếp

            return (1, true); // Bỏ 2+ ngày, reset
        }

        private void UpdateStreakInDB(int playerID, int newStreak)
        {
            const string sql = "sp_UpdateStreakInDB";
            DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@id", playerID),
                new SqlParameter("@streak", newStreak)
            }, null, CommandType.StoredProcedure);
        }

        // Xóa người chơi theo ID
        public bool XoaNguoiChoi(int playerID)
        {
            const string sql = "sp_DeletePlayer";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@id", playerID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Đặt lại chỉ số người chơi về 0
        public bool DatLaiChiSoNguoiChoi(int playerID)
        {
            const string sql = "sp_ResetPlayerStats";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@id", playerID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Lấy tổng số người chơi trong hệ thống
        public int LayTongSoNguoiChoi()
        {
            const string sql = "sp_GetTotalPlayers";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, null, null, CommandType.StoredProcedure));
        }

        // Lấy điểm cao nhất trong hệ thống
        public int LayDiemCaoNhat()
        {
            const string sql = "sp_GetTopScore";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, null, null, CommandType.StoredProcedure));
        }

        // Lấy chuỗi ngày cao nhất trong hệ thống
        public int LayChuoiNgayCaoNhat()
        {
            const string sql = "sp_GetTopStreak";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, null, null, CommandType.StoredProcedure));
        }
    }
}
