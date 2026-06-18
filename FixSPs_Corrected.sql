CREATE OR ALTER PROCEDURE sp_GetGameRule
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 RuleID, QuestionCount, TimeLimit, Lives, StreakBonus,
           Star1Threshold, Star2Threshold, Star3Threshold
    FROM GameRules;
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllImages
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

CREATE OR ALTER PROCEDURE sp_GetAllPlayerProgress
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TopicName, pp.LevelNum, pp.Stars, pp.BestScore,
           pp.IsCompleted, pp.CompletedAt
    FROM PlayerProgress pp
    INNER JOIN Topics t ON pp.TopicID = t.TopicID
    WHERE pp.PlayerID = @PlayerID
    ORDER BY t.TopicName, pp.LevelNum;
END
GO

CREATE OR ALTER PROCEDURE sp_GetAllPlayers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PlayerID, Username, TotalScore, TotalStars, DayStreak, LastPlayed, CreatedAt
    FROM Players
    ORDER BY TotalScore DESC;
END
GO

CREATE OR ALTER PROCEDURE sp_GetScoreHistoryByPlayer
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
