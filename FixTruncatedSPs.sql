-- ================================================================
-- FIX SCRIPT: Sửa các Stored Procedure bị cắt cụt trong database
-- Chạy script này trên database WordQuest để sửa lỗi
-- ================================================================

-- ================================================================
--  1. sp_GetAllPlayers - Thiếu cột TotalStars, DayStreak, LastPlayed, CreatedAt
-- ================================================================
IF OBJECT_ID('sp_GetAllPlayers', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllPlayers;
GO

CREATE PROCEDURE sp_GetAllPlayers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PlayerID, Username, TotalScore, TotalStars, DayStreak, LastPlayed, CreatedAt
    FROM Players
    ORDER BY TotalScore DESC;
END
GO

-- ================================================================
--  2. sp_GetGameRule - Bị cắt cụt sau "QuestionCount,"
-- ================================================================
IF OBJECT_ID('sp_GetGameRule', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetGameRule;
GO

CREATE PROCEDURE sp_GetGameRule
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 RuleID, QuestionCount, TimeLimit, LivesCount, StreakBonus,
           Star1Threshold, Star2Threshold, Star3Threshold
    FROM GameRules;
END
GO

-- ================================================================
--  3. sp_GetAllImages - Bị cắt cụt sau "SELECT"
-- ================================================================
IF OBJECT_ID('sp_GetAllImages', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllImages;
GO

CREATE PROCEDURE sp_GetAllImages
    @Keyword NVARCHAR(200) = ''
AS
BEGIN
    SET NOCOUNT ON;
    SELECT i.ImageID, i.ImageName, i.UploadedAt, 
           DATALENGTH(i.ImageData) AS FileSize,
           CASE WHEN EXISTS(SELECT 1 FROM Words w WHERE w.ImageID = i.ImageID) 
                THEN 1 ELSE 0 END AS IsUsed
    FROM Images i
    WHERE @Keyword = '' OR i.ImageName LIKE '%' + @Keyword + '%'
    ORDER BY i.ImageID DESC;
END
GO

-- ================================================================
--  4. sp_GetAllPlayerProgress - Bị cắt cụt
-- ================================================================
IF OBJECT_ID('sp_GetAllPlayerProgress', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllPlayerProgress;
GO

CREATE PROCEDURE sp_GetAllPlayerProgress
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TopicName, pp.CompletedLevel, pp.Stars, pp.HighestScore,
           pp.IsCompleted, pp.CompletedAt
    FROM PlayerProgress pp
    INNER JOIN Topics t ON pp.TopicID = t.TopicID
    WHERE pp.PlayerID = @PlayerID
    ORDER BY t.TopicName, pp.CompletedLevel;
END
GO

-- ================================================================
--  5. sp_GetScoreHistoryByPlayer - Kiểm tra và sửa nếu cần
-- ================================================================
IF OBJECT_ID('sp_GetScoreHistoryByPlayer', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetScoreHistoryByPlayer;
GO

CREATE PROCEDURE sp_GetScoreHistoryByPlayer
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TopicName, sh.LevelNum, sh.Score, sh.Stars, sh.TimeTaken, sh.PlayedAt
    FROM ScoreHistory sh
    INNER JOIN Topics t ON sh.TopicID = t.TopicID
    WHERE sh.PlayerID = @PlayerID
    ORDER BY sh.PlayedAt DESC;
END
GO

PRINT N'✅ Tất cả Stored Procedure đã được sửa thành công!';
