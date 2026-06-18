using System;
using System.Data;
using WordQuest.DAL;
using WordQuest.DTO;

namespace WordQuest.BUS
{
    // ──────────────────────────────────────────────────────────────────────────────
    // Quản lý tài khoản người chơi (đăng nhập, thống kê, streak)
    // ──────────────────────────────────────────────────────────────────────────────
    public class PlayerBUS
    {
        private readonly PlayerDAL _playerDAL = new();

        public DataTable LayTatCaNguoiChoi() => _playerDAL.LayTatCaNguoiChoi();

        public PlayerDTO LayNguoiChoiTheoID(int playerID) => _playerDAL.LayNguoiChoiTheoID(playerID);

        public PlayerDTO LayNguoiChoiTheoTen(string username) => _playerDAL.LayNguoiChoiTheoTen(username);

        public int LayHoacTaoNguoiChoi(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Tên người chơi không được để trống!");
            if (username.Length < 2)
                throw new Exception("Tên phải có ít nhất 2 ký tự!");

            var existing = _playerDAL.LayNguoiChoiTheoTen(username);
            if (existing != null)
                return existing.PlayerID;

            return _playerDAL.InsertPlayer(username);
        }

        public bool CapNhatChiSoNguoiChoi(int playerID, int scoreEarned, int starsEarned)
        {
            if (playerID <= 0)
                throw new Exception("ID người chơi không hợp lệ!");

            return _playerDAL.CapNhatChiSoNguoiChoi(playerID, scoreEarned, starsEarned);
        }

        public bool CapNhatChuoiNgayNguoiChoi(int playerID, int newStreak) => _playerDAL.CapNhatChuoiNgayNguoiChoi(playerID, newStreak);

        /// <summary>
        /// Gọi ngay khi người chơi đăng nhập thành công.
        /// Tự động tính streak dựa vào ngày thực tế:
        ///   - Cùng ngày -> không đổi
        ///   - Hôm qua -> streak++
        ///   - Bỏ 2+ ngày -> reset = 1
        /// Trả về (newStreak, isNewDay) để GUI hiển thị thông báo phù hợp.
        /// </summary>
        public (int newStreak, bool isNewDay) CapNhatChuoiNgay(int playerID)
        {
            if (playerID <= 0) return (0, false);
            return _playerDAL.CapNhatChuoiNgay(playerID);
        }

        public bool XoaNguoiChoi(int playerID)
        {
            if (playerID <= 0)
                throw new Exception("ID người chơi không hợp lệ!");

            return _playerDAL.XoaNguoiChoi(playerID);
        }

        public bool DatLaiChiSoNguoiChoi(int playerID)
        {
            if (playerID <= 0)
                throw new Exception("ID người chơi không hợp lệ!");

            return _playerDAL.DatLaiChiSoNguoiChoi(playerID);
        }

        public int LayTongSoNguoiChoi() => _playerDAL.LayTongSoNguoiChoi();
        public int LayDiemCaoNhat() => _playerDAL.LayDiemCaoNhat();
        public int LayChuoiNgayCaoNhat() => _playerDAL.LayChuoiNgayCaoNhat();
    }

    // ──────────────────────────────────────────────────────────────────────────────
    // Quản lý tiến độ người chơi trong từng màn (lưu/đọc stars, best score)
    // ──────────────────────────────────────────────────────────────────────────────
    public class PlayerProgressBUS
    {
        private readonly PlayerProgressDAL _playerProgressDAL = new();

        public DataTable LayTienTrinhNguoiChoi(int playerID, int topicID)
            => _playerProgressDAL.LayTienTrinhNguoiChoi(playerID, topicID);

        public DataTable LayTatCaTienTrinh(int playerID)
            => _playerProgressDAL.LayTatCaTienTrinh(playerID);

        public bool LuuTienTrinh(int playerID, int topicID, int levelNum, int score, int stars)
        {
            if (playerID <= 0) throw new Exception("ID người chơi không hợp lệ!");
            if (topicID <= 0) throw new Exception("ID chủ đề không hợp lệ!");
            if (levelNum <= 0) throw new Exception("Số màn không hợp lệ!");

            return _playerProgressDAL.SaveOrUpdateProgress(playerID, topicID, levelNum, stars, score);
        }

        public bool XoaTienTrinhNguoiChoi(int playerID) => _playerProgressDAL.XoaTienTrinhNguoiChoi(playerID);
        public bool XoaTienTrinhChuDe(int topicID) => _playerProgressDAL.XoaTienTrinhChuDe(topicID);
    }

    // ──────────────────────────────────────────────────────────────────────────────
    // Quản lý lịch sử điểm số và bảng xếp hạng
    // ──────────────────────────────────────────────────────────────────────────────
    public class ScoreHistoryBUS
    {
        private readonly ScoreHistoryDAL _scoreHistoryDAL = new();

        public bool LuuLichSuDiem(int playerID, int topicID, int levelNum, int score, int stars, int timeTaken)
        {
            if (playerID <= 0) throw new Exception("ID người chơi không hợp lệ!");
            if (topicID <= 0) throw new Exception("ID chủ đề không hợp lệ!");

            var history = new ScoreHistoryDTO
            {
                PlayerID = playerID,
                TopicID = topicID,
                LevelNum = levelNum,
                Score = score,
                Stars = stars,
                TimeTaken = timeTaken
            };
            return _scoreHistoryDAL.InsertScoreHistory(history);
        }

        public DataTable LayLichSuDiemNguoiChoi(int playerID) => _scoreHistoryDAL.LayLichSuDiemNguoiChoi(playerID);

        public DataTable LayBangXepHang() => _scoreHistoryDAL.LayBangXepHang();

        public bool XoaLichSuDiemNguoiChoi(int playerID) => _scoreHistoryDAL.XoaLichSuDiemNguoiChoi(playerID);

        public int LayTongSoGame() => _scoreHistoryDAL.LayTongSoGame();
    }
}
