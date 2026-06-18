-- ================================================================
-- FIX SCRIPT: Sửa sp_GetAllPlayers + thêm các SP còn thiếu
-- Chạy script này trực tiếp trên database WordQuest
-- ================================================================

-- 1. sp_GetAllPlayers - thêm cột NgayStreak (DayStreak) và LanChoiCuoi (LastPlayed)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllPlayers')
    DROP PROCEDURE sp_GetAllPlayers;
GO
CREATE PROCEDURE sp_GetAllPlayers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaNguoiChoi AS PlayerID,
           TenNguoiChoi AS Username,
           TongDiem AS TotalScore,
           TongSao AS TotalStars,
           NgayStreak AS DayStreak,
           LanChoiCuoi AS LastPlayed,
           NgayTao AS CreatedAt
    FROM Players
    ORDER BY TongDiem DESC;
END
GO

-- 2. sp_GetPlayerByID - lấy thông tin người chơi theo ID
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetPlayerByID')
    DROP PROCEDURE sp_GetPlayerByID;
GO
CREATE PROCEDURE sp_GetPlayerByID
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT MaNguoiChoi AS PlayerID,
           TenNguoiChoi AS Username,
           TongDiem AS TotalScore,
           TongSao AS TotalStars,
           NgayStreak AS DayStreak,
           LanChoiCuoi AS LastPlayed,
           NgayTao AS CreatedAt
    FROM Players
    WHERE MaNguoiChoi = @id;
END
GO

-- 3. sp_GetPlayerStreakInfo - lấy thông tin streak để tính toán
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetPlayerStreakInfo')
    DROP PROCEDURE sp_GetPlayerStreakInfo;
GO
CREATE PROCEDURE sp_GetPlayerStreakInfo
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT NgayStreak, LanChoiCuoi
    FROM Players
    WHERE MaNguoiChoi = @id;
END
GO

-- 4. sp_UpdateStreakInDB - cập nhật streak và lần chơi cuối
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateStreakInDB')
    DROP PROCEDURE sp_UpdateStreakInDB;
GO
CREATE PROCEDURE sp_UpdateStreakInDB
    @id INT,
    @streak INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players
    SET NgayStreak = @streak, LanChoiCuoi = GETDATE()
    WHERE MaNguoiChoi = @id;
END
GO

-- 5. sp_UpdatePlayerStreak - cập nhật streak (không cập nhật LanChoiCuoi)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdatePlayerStreak')
    DROP PROCEDURE sp_UpdatePlayerStreak;
GO
CREATE PROCEDURE sp_UpdatePlayerStreak
    @id INT,
    @streak INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players
    SET NgayStreak = @streak
    WHERE MaNguoiChoi = @id;
END
GO

-- 6. sp_UpdatePlayerStats - cập nhật điểm và sao sau mỗi game
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdatePlayerStats')
    DROP PROCEDURE sp_UpdatePlayerStats;
GO
CREATE PROCEDURE sp_UpdatePlayerStats
    @id INT,
    @score INT,
    @stars INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players
    SET TongDiem = TongDiem + @score, TongSao = TongSao + @stars
    WHERE MaNguoiChoi = @id;
END
GO

-- 7. sp_ResetPlayerStats - đặt lại chỉ số người chơi về 0
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ResetPlayerStats')
    DROP PROCEDURE sp_ResetPlayerStats;
GO
CREATE PROCEDURE sp_ResetPlayerStats
    @id INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players
    SET TongDiem = 0, TongSao = 0, NgayStreak = 0, LanChoiCuoi = NULL
    WHERE MaNguoiChoi = @id;
END
GO

-- 8. sp_GetTotalPlayers - tổng số người chơi
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTotalPlayers')
    DROP PROCEDURE sp_GetTotalPlayers;
GO
CREATE PROCEDURE sp_GetTotalPlayers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) FROM Players;
END
GO

-- 9. sp_GetTopScore - điểm cao nhất toàn hệ thống
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTopScore')
    DROP PROCEDURE sp_GetTopScore;
GO
CREATE PROCEDURE sp_GetTopScore
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(MAX(TongDiem), 0) FROM Players;
END
GO

-- 10. sp_GetTopStreak - chuỗi ngày cao nhất toàn hệ thống
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTopStreak')
    DROP PROCEDURE sp_GetTopStreak;
GO
CREATE PROCEDURE sp_GetTopStreak
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(MAX(NgayStreak), 0) FROM Players;
END
GO

PRINT N'✅ Tất cả Stored Procedure đã được cập nhật thành công!';
