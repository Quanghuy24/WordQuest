definition                                                                                                                                                                                                                                                      
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE   PROCEDURE sp_UpdateLevel
    @LevelID         INT,
    @LevelName       NVARCHAR(100),
    @DifficultyLevel NVARCHAR(50),
    @Mode            NVARCHAR(50),
    @QuestionCount   INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Levels
    SET LevelName

CREATE   PROCEDURE sp_DeleteLevel
    @LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Levels WHERE LevelID = @LevelID;
END
                                                                                                                          

CREATE   PROCEDURE sp_AddWordToLevel
    @LevelID INT,
    @WordID  INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LevelWords (LevelID, WordID) VALUES (@LevelID, @WordID);
END
                                                                             

CREATE   PROCEDURE sp_RemoveWordFromLevel
    @LevelID INT,
    @WordID  INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LevelWords WHERE LevelID = @LevelID AND WordID = @WordID;
END
                                                                       

CREATE   PROCEDURE sp_ClearLevelWords
    @LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LevelWords WHERE LevelID = @LevelID;
END
                                                                                                                  

CREATE   PROCEDURE sp_GetFixedWordsForLevel
    @LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT w.WordID, w.EnglishWord, w.VietnameseMeaning, w.DifficultyLevel,
           w.Phonetic, w.EmojiIcon, w.ImagePath, w.ImageID
    FROM LevelWords lw
    JOI

-- ================================================================
--  PLAYERS
-- ================================================================

CREATE   PROCEDURE sp_GetAllPlayers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PlayerID, Username, TotalScore

CREATE   PROCEDURE sp_GetPlayerByID
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PlayerID, Username, TotalScore, TotalStars, DayStreak, LastPlayed, CreatedAt
    FROM Players WHERE PlayerID = @PlayerID;
END
                                   

CREATE   PROCEDURE sp_GetPlayerByUsername
    @Username NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT PlayerID, Username, TotalScore, TotalStars, DayStreak, LastPlayed, CreatedAt
    FROM Players WHERE Username = @Username;
END
                   

CREATE   PROCEDURE sp_InsertPlayer
    @Username NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Players (Username) OUTPUT INSERTED.PlayerID VALUES (@Username);
END
                                                                               

CREATE   PROCEDURE sp_UpdatePlayerStats
    @PlayerID    INT,
    @ScoreEarned INT,
    @StarsEarned INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players
    SET TotalScore = TotalScore + @ScoreEarned,
        TotalStars = TotalStars + @StarsEarned,
      

CREATE   PROCEDURE sp_UpdatePlayerStreak
    @PlayerID  INT,
    @NewStreak INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players SET DayStreak = @NewStreak WHERE PlayerID = @PlayerID;
END
                                                                    

CREATE   PROCEDURE sp_GetPlayerStreakInfo
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DayStreak, LastPlayed FROM Players WHERE PlayerID = @PlayerID;
END
                                                                                        

CREATE   PROCEDURE sp_UpdateStreakInDB
    @PlayerID  INT,
    @NewStreak INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players
    SET DayStreak  = @NewStreak,
        LastPlayed = GETDATE()
    WHERE PlayerID = @PlayerID;
END
                             

CREATE   PROCEDURE sp_DeletePlayer
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Players WHERE PlayerID = @PlayerID;
END
                                                                                                                     

CREATE   PROCEDURE sp_ResetPlayerStats
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Players SET TotalScore = 0, TotalStars = 0, DayStreak = 0
    WHERE PlayerID = @PlayerID;
END
                                                                

CREATE   PROCEDURE sp_GetTotalPlayers
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS Total FROM Players;
END
                                                                                                                                             

CREATE   PROCEDURE sp_GetTopScore
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(MAX(TotalScore), 0) AS TopScore FROM Players;
END
                                                                                                                            

CREATE   PROCEDURE sp_GetTopStreak
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ISNULL(MAX(DayStreak), 0) AS TopStreak FROM Players;
END
                                                                                                                           

-- ================================================================
--  PLAYER PROGRESS
-- ================================================================

CREATE   PROCEDURE sp_GetPlayerProgress
    @PlayerID INT,
    @TopicID  INT
AS
BEGIN
    SET NOCO

CREATE   PROCEDURE sp_GetAllPlayerProgress
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TopicName, pp.LevelNum, pp.Stars, pp.BestScore, pp.IsCompleted, pp.CompletedAt
    FROM PlayerProgress pp
    INNER JOIN Topics t ON pp.TopicID = t.Topi

CREATE   PROCEDURE sp_SaveOrUpdateProgress
    @PlayerID  INT,
    @TopicID   INT,
    @LevelNum  INT,
    @Stars     INT,
    @BestScore INT
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM PlayerProgress
               WHERE PlayerID = @PlayerI

CREATE   PROCEDURE sp_DeleteProgressByPlayer
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM PlayerProgress WHERE PlayerID = @PlayerID;
END
                                                                                                    

CREATE   PROCEDURE sp_DeleteProgressByTopic
    @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM PlayerProgress WHERE TopicID = @TopicID;
END
                                                                                                        

-- ================================================================
--  SCORE HISTORY
-- ================================================================

CREATE   PROCEDURE sp_InsertScoreHistory
    @PlayerID  INT,
    @TopicID   INT,
    @LevelNum  INT,

CREATE   PROCEDURE sp_GetScoreHistoryByPlayer
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT t.TopicName, sh.LevelNum, sh.Score, sh.Stars, sh.TimeTaken, sh.PlayedAt
    FROM ScoreHistory sh
    INNER JOIN Topics t ON sh.TopicID = t.TopicID
    

CREATE   PROCEDURE sp_GetLeaderboard
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ROW_NUMBER() OVER (ORDER BY TotalScore DESC) AS Hang,
           Username, TotalScore, TotalStars
    FROM Players ORDER BY TotalScore DESC;
END
                                 

CREATE   PROCEDURE sp_DeleteScoreHistoryByPlayer
    @PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM ScoreHistory WHERE PlayerID = @PlayerID;
END
                                                                                                  

CREATE   PROCEDURE sp_GetTotalGames
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(*) AS Total FROM ScoreHistory;
END
                                                                                                                                          

-- ================================================================
--  TOPICS
-- ================================================================

CREATE   PROCEDURE sp_GetAllTopics
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TopicID, TopicName, TopicIcon, S

CREATE   PROCEDURE sp_GetParentTopics
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TopicID, TopicName, TopicIcon, StarsToUnlock
    FROM Topics WHERE ParentID IS NULL ORDER BY SortOrder;
END
                                                                     

CREATE   PROCEDURE sp_GetChildTopics
    @ParentID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TopicID, TopicName, TopicIcon, StarsToUnlock
    FROM Topics WHERE ParentID = @ParentID ORDER BY SortOrder;
END
                                                

CREATE   PROCEDURE sp_GetTopicByID
    @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TopicID, TopicName, TopicIcon, StarsToUnlock, ParentID, SortOrder
    FROM Topics WHERE TopicID = @TopicID;
END
                                                   

CREATE   PROCEDURE sp_InsertTopic
    @TopicName    NVARCHAR(100),
    @TopicIcon    NVARCHAR(100),
    @StarsToUnlock INT,
    @ParentID     INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Topics (TopicName, TopicIcon, StarsToUnlock, ParentID, So

CREATE   PROCEDURE sp_UpdateTopic
    @TopicID      INT,
    @TopicName    NVARCHAR(100),
    @TopicIcon    NVARCHAR(100),
    @StarsToUnlock INT,
    @ParentID     INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Topics
    SET TopicName     = @TopicNa

CREATE   PROCEDURE sp_DeleteTopic
    @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Topics WHERE TopicID = @TopicID;
END
                                                                                                                          

CREATE   PROCEDURE sp_HasChildTopics
    @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) AS Total FROM Topics WHERE ParentID = @TopicID;
END
                                                                                                    

-- ================================================================
--  WORDS
-- ================================================================

CREATE   PROCEDURE sp_GetAllWords
AS
BEGIN
    SET NOCOUNT ON;
    SELECT w.WordID, t.TopicName, w.EnglishWo

CREATE   PROCEDURE sp_GetWordsByTopic
    @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT WordID, EnglishWord, Phonetic, VietnameseMeaning,
           DifficultyLevel, EmojiIcon, ImagePath, ImageID
    FROM Words WHERE TopicID = @TopicID ORDER BY Eng

CREATE   PROCEDURE sp_GetWordsByTopicAndDifficulty
    @TopicID    INT,
    @Difficulty INT,
    @Count      INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP (@Count) WordID, EnglishWord, VietnameseMeaning,
           Phonetic, EmojiIcon, ImagePath, ImageI

CREATE   PROCEDURE sp_GetWordsByLevelID
    @LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT w.WordID, w.EnglishWord, w.VietnameseMeaning,
           w.Phonetic, w.EmojiIcon, w.ImagePath, w.ImageID, w.DifficultyLevel
    FROM LevelWords lw
    JOIN Wo

-- SP tìm ki?m v?i b? l?c d?ng
-- @ImageFilter: 0 = t?t c?, 1 = có ?nh, 2 = chua có ?nh
CREATE   PROCEDURE sp_SearchWords
    @Keyword     NVARCHAR(200) = '',
    @Difficulty  NVARCHAR(50)  = '',
    @ImageFilter INT           = 0     -- 0=t?t c?, 1=có ?n

CREATE   PROCEDURE sp_GetWordByID
    @WordID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT w.WordID, w.TopicID, t.TopicName, w.EnglishWord, w.Phonetic,
           w.VietnameseMeaning, w.DifficultyLevel, w.EmojiIcon, w.ImagePath, w.ImageID
    FROM Words w 

CREATE   PROCEDURE sp_InsertWord
    @TopicID          INT,
    @EnglishWord      NVARCHAR(100),
    @Phonetic         NVARCHAR(100),
    @VietnameseMeaning NVARCHAR(200),
    @DifficultyLevel  INT,
    @EmojiIcon        NVARCHAR(10),
    @ImagePath      

CREATE   PROCEDURE sp_UpdateWord
    @WordID           INT,
    @TopicID          INT,
    @EnglishWord      NVARCHAR(100),
    @Phonetic         NVARCHAR(100),
    @VietnameseMeaning NVARCHAR(200),
    @DifficultyLevel  INT,
    @EmojiIcon        NVARCHA

CREATE   PROCEDURE sp_DeleteWord
    @WordID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Words WHERE WordID = @WordID;
END
                                                                                                                               

CREATE   PROCEDURE sp_IsWordInUse
    @WordID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) AS Total FROM LevelWords WHERE WordID = @WordID;
END
                                                                                                       

CREATE   PROCEDURE sp_IsDuplicateEnglishWord
    @TopicID      INT,
    @EnglishWord  NVARCHAR(100),
    @ExcludeWordID INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) AS Total
    FROM Words
    WHERE TopicID = @TopicID AND EnglishWord = @Englis

-- SP l?c t? theo b? l?c (thay th? GetWords v?i WordFilterDTO)
CREATE   PROCEDURE sp_GetWordsByFilter
    @TopicID     INT          = -1,
    @Difficulty  NVARCHAR(50) = '',
    @ImageFilter INT          = 0,   -- 0=t?t c?, 1=có ?nh, 2=chua có ?nh
    @Ke

	CREATE PROCEDURE dbo.sp_upgraddiagrams
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			

	CREATE PROCEDURE dbo.sp_helpdiagrams
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboL

	CREATE PROCEDURE dbo.sp_helpdiagramdefinition
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDF

-- ================================================================
--  GAME RULES
-- ================================================================

CREATE   PROCEDURE sp_GetGameRule
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 RuleID, QuestionCount, 

	CREATE PROCEDURE dbo.sp_creatediagram
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int

CREATE   PROCEDURE sp_UpdateGameRule
    @QuestionCount  INT,
    @TimeLimit      INT,
    @Lives          INT,
    @StreakBonus    INT,
    @Star1Threshold INT,
    @Star2Threshold INT,
    @Star3Threshold INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Game

	CREATE PROCEDURE dbo.sp_renamediagram
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UI

CREATE   PROCEDURE sp_InsertGameRule
    @QuestionCount  INT,
    @TimeLimit      INT,
    @Lives          INT,
    @StreakBonus    INT,
    @Star1Threshold INT,
    @Star2Threshold INT,
    @Star3Threshold INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO

	CREATE PROCEDURE dbo.sp_alterdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int

CREATE   PROCEDURE sp_HasAnyGameRule
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) AS Total FROM GameRules;
END
                                                                                                                                            

	CREATE PROCEDURE dbo.sp_dropdiagram
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			in

-- ================================================================
--  IMAGES
-- ================================================================

CREATE   PROCEDURE sp_GetAllImages
    @Keyword NVARCHAR(200) = ''
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 

CREATE   PROCEDURE sp_GetImageData
    @ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ImageData FROM Images WHERE ImageID = @ImageID;
END
                                                                                                               

CREATE   PROCEDURE sp_InsertImage
    @ImageName NVARCHAR(200),
    @ImageData VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Images (ImageName, ImageData) VALUES (@ImageName, @ImageData);
    SELECT SCOPE_IDENTITY() AS NewID;
END
           

CREATE   PROCEDURE sp_UpdateImageData
    @ImageID   INT,
    @ImageData VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Images SET ImageData = @ImageData WHERE ImageID = @ImageID;
END
                                                               

CREATE   PROCEDURE sp_DeleteImage
    @ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Images WHERE ImageID = @ImageID;
END
                                                                                                                          

CREATE   PROCEDURE sp_IsImageInUse
    @ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) AS Total FROM Words WHERE ImageID = @ImageID;
END
                                                                                                        

CREATE   PROCEDURE sp_IsImageNameExists
    @ImageName NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT COUNT(1) AS Total FROM Images WHERE ImageName = @ImageName;
END
                                                                                  

-- ================================================================
--  LEVELS
-- ================================================================

CREATE   PROCEDURE sp_GetLevelsByTopic
    @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT LevelID, Le

CREATE   PROCEDURE sp_GetLevelByID
    @LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT LevelID, TopicID, LevelNum, LevelName, DifficultyLevel, Mode, QuestionCount
    FROM Levels WHERE LevelID = @LevelID;
END
                                         

CREATE   PROCEDURE sp_InsertLevel
    @TopicID        INT,
    @LevelNum       INT,
    @LevelName      NVARCHAR(100),
    @DifficultyLevel NVARCHAR(50),
    @Mode           NVARCHAR(50),
    @QuestionCount  INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INT

(70 rows affected)
