EXEC sp_rename 'Topics.TopicID', 'MaChuDe', 'COLUMN';
EXEC sp_rename 'Topics.TopicName', 'TenChuDe', 'COLUMN';
EXEC sp_rename 'Topics.TopicIcon', 'BieuTuongChuDe', 'COLUMN';
EXEC sp_rename 'Topics.StarsToUnlock', 'SaoDeMoKhoa', 'COLUMN';
EXEC sp_rename 'Topics.ParentID', 'MaChuDeCha', 'COLUMN';
EXEC sp_rename 'Topics.SortOrder', 'ThuTuSapXep', 'COLUMN';

EXEC sp_rename 'Levels.LevelID', 'MaMuc', 'COLUMN';
EXEC sp_rename 'Levels.TopicID', 'MaChuDe', 'COLUMN';
EXEC sp_rename 'Levels.LevelNum', 'SoMuc', 'COLUMN';
EXEC sp_rename 'Levels.LevelName', 'TenMuc', 'COLUMN';
EXEC sp_rename 'Levels.Difficulty', 'DoKho', 'COLUMN';
EXEC sp_rename 'Levels.GameMode', 'CheDo', 'COLUMN';
EXEC sp_rename 'Levels.TotalQuestions', 'SoCauHoi', 'COLUMN';

EXEC sp_rename 'LevelWords.LevelID', 'MaMuc', 'COLUMN';
EXEC sp_rename 'LevelWords.WordID', 'MaTu', 'COLUMN';

EXEC sp_rename 'Words.WordID', 'MaTu', 'COLUMN';
EXEC sp_rename 'Words.TopicID', 'MaChuDe', 'COLUMN';
EXEC sp_rename 'Words.EnglishWord', 'TiengAnh', 'COLUMN';
EXEC sp_rename 'Words.VietnameseMeaning', 'NghiaTiengViet', 'COLUMN';
EXEC sp_rename 'Words.Phonetic', 'PhienAm', 'COLUMN';
EXEC sp_rename 'Words.DifficultyLevel', 'DoKho', 'COLUMN';
EXEC sp_rename 'Words.ImagePath', 'DuongDanAnh', 'COLUMN';
EXEC sp_rename 'Words.ImageID', 'MaAnh', 'COLUMN';
EXEC sp_rename 'Words.EmojiIcon', 'BieuTuong', 'COLUMN';

EXEC sp_rename 'Images.ImageID', 'MaAnh', 'COLUMN';
EXEC sp_rename 'Images.ThumbnailUrl', 'UrlThuNho', 'COLUMN';
EXEC sp_rename 'Images.OriginalUrl', 'UrlGoc', 'COLUMN';
EXEC sp_rename 'Images.Tags', 'Tags', 'COLUMN';
EXEC sp_rename 'Images.ImageBinary', 'NhiPhanAnh', 'COLUMN';

EXEC sp_rename 'Players.PlayerID', 'MaNguoiChoi', 'COLUMN';
EXEC sp_rename 'Players.Username', 'TenNguoiChoi', 'COLUMN';
EXEC sp_rename 'Players.TotalScore', 'TongDiem', 'COLUMN';
EXEC sp_rename 'Players.TotalStars', 'TongSao', 'COLUMN';
EXEC sp_rename 'Players.DayStreak', 'NgayStreak', 'COLUMN';
EXEC sp_rename 'Players.LastPlayed', 'LanChoiCuoi', 'COLUMN';
EXEC sp_rename 'Players.CreatedAt', 'NgayTao', 'COLUMN';

EXEC sp_rename 'PlayerProgress.ProgressID', 'MaTienTrinh', 'COLUMN';
EXEC sp_rename 'PlayerProgress.PlayerID', 'MaNguoiChoi', 'COLUMN';
EXEC sp_rename 'PlayerProgress.TopicID', 'MaChuDe', 'COLUMN';
EXEC sp_rename 'PlayerProgress.LevelNum', 'SoMuc', 'COLUMN';
EXEC sp_rename 'PlayerProgress.Stars', 'Sao', 'COLUMN';
EXEC sp_rename 'PlayerProgress.BestScore', 'DiemCaoNhat', 'COLUMN';
EXEC sp_rename 'PlayerProgress.IsCompleted', 'DaHoanThanh', 'COLUMN';
EXEC sp_rename 'PlayerProgress.CompletedAt', 'ThoiDiemHoanThanh', 'COLUMN';

EXEC sp_rename 'ScoreHistory.ScoreID', 'MaDiem', 'COLUMN';
EXEC sp_rename 'ScoreHistory.PlayerID', 'MaNguoiChoi', 'COLUMN';
EXEC sp_rename 'ScoreHistory.TopicID', 'MaChuDe', 'COLUMN';
EXEC sp_rename 'ScoreHistory.LevelNum', 'SoMuc', 'COLUMN';
EXEC sp_rename 'ScoreHistory.Score', 'Diem', 'COLUMN';
EXEC sp_rename 'ScoreHistory.Stars', 'Sao', 'COLUMN';
EXEC sp_rename 'ScoreHistory.TimeTaken', 'ThoiGianChoi', 'COLUMN';
EXEC sp_rename 'ScoreHistory.PlayedAt', 'ThoiDiemChoi', 'COLUMN';

EXEC sp_rename 'GameRules.RuleID', 'MaLuat', 'COLUMN';
EXEC sp_rename 'GameRules.QuestionCount', 'SoCauHoi', 'COLUMN';
EXEC sp_rename 'GameRules.TimeLimit', 'GioiHanThoiGian', 'COLUMN';
EXEC sp_rename 'GameRules.Lives', 'SoMang', 'COLUMN';
EXEC sp_rename 'GameRules.StreakBonus', 'ThuongChuoiDiem', 'COLUMN';
EXEC sp_rename 'GameRules.Star1Threshold', 'NguongSao1', 'COLUMN';
EXEC sp_rename 'GameRules.Star2Threshold', 'NguongSao2', 'COLUMN';
EXEC sp_rename 'GameRules.Star3Threshold', 'NguongSao3', 'COLUMN';
