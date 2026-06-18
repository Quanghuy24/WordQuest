IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_Login')
    DROP PROCEDURE sp_Login;
GO
CREATE PROCEDURE sp_Login
@Username NVARCHAR(100), @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaNguoiChoi AS PlayerID, TenNguoiChoi AS Username, MatKhau AS PasswordHash, TongDiem AS TotalScore, TongSao AS TotalStars, NgayTao AS CreatedAt FROM Players WHERE TenNguoiChoi = @Username AND MatKhau = @Password;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetPlayerByUsername')
    DROP PROCEDURE sp_GetPlayerByUsername;
GO
CREATE PROCEDURE sp_GetPlayerByUsername
@Username NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaNguoiChoi AS PlayerID, TenNguoiChoi AS Username, MatKhau AS PasswordHash, TongDiem AS TotalScore, TongSao AS TotalStars, NgayTao AS CreatedAt FROM Players WHERE TenNguoiChoi = @Username;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CheckUsernameExists')
    DROP PROCEDURE sp_CheckUsernameExists;
GO
CREATE PROCEDURE sp_CheckUsernameExists
@Username NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM Players WHERE TenNguoiChoi = @Username;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertPlayer')
    DROP PROCEDURE sp_InsertPlayer;
GO
CREATE PROCEDURE sp_InsertPlayer
@Username NVARCHAR(100), @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO Players (TenNguoiChoi, MatKhau, TongDiem, TongSao, NgayTao) VALUES (@Username, @Password, 0, 0, GETDATE());
    SELECT SCOPE_IDENTITY();
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ChangePassword')
    DROP PROCEDURE sp_ChangePassword;
GO
CREATE PROCEDURE sp_ChangePassword
@PlayerID INT, @NewPassword NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
UPDATE Players SET MatKhau = @NewPassword WHERE MaNguoiChoi = @PlayerID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdatePlayerScore')
    DROP PROCEDURE sp_UpdatePlayerScore;
CREATE PROCEDURE sp_GetAllTopics
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock, MaChuDeCha AS ParentID, ThuTuSapXep AS SortOrder FROM Topics ORDER BY ThuTuSapXep, TenChuDe;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetParentTopics')
    DROP PROCEDURE sp_GetParentTopics;
GO
CREATE PROCEDURE sp_GetParentTopics
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock FROM Topics WHERE MaChuDeCha IS NULL ORDER BY ThuTuSapXep;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetChildTopics')
    DROP PROCEDURE sp_GetChildTopics;
GO
CREATE PROCEDURE sp_GetChildTopics
@pid INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock FROM Topics WHERE MaChuDeCha = @pid ORDER BY ThuTuSapXep;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTopicByID')
    DROP PROCEDURE sp_GetTopicByID;
GO
CREATE PROCEDURE sp_GetTopicByID
@id INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock, MaChuDeCha AS ParentID, ThuTuSapXep AS SortOrder FROM Topics WHERE MaChuDe = @id;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertTopic')
    DROP PROCEDURE sp_InsertTopic;
GO
CREATE PROCEDURE sp_InsertTopic
@name NVARCHAR(100), @icon NVARCHAR(50), @stars INT, @parentID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO Topics (TenChuDe, BieuTuongChuDe, SaoDeMoKhoa, MaChuDeCha, ThuTuSapXep) VALUES (@name, @icon, @stars, @parentID, (SELECT ISNULL(MAX(ThuTuSapXep),0)+1 FROM Topics));
    SELECT SCOPE_IDENTITY();
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateTopic')
    DROP PROCEDURE sp_UpdateTopic;
GO
CREATE PROCEDURE sp_UpdateTopic
@id INT, @name NVARCHAR(100), @icon NVARCHAR(50), @stars INT, @parentID INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
UPDATE Topics SET TenChuDe = @name, BieuTuongChuDe = @icon, SaoDeMoKhoa = @stars, MaChuDeCha = @parentID WHERE MaChuDe = @id;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteTopic')
    DROP PROCEDURE sp_DeleteTopic;
GO
CREATE PROCEDURE sp_DeleteTopic
@id INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM Topics WHERE MaChuDe = @id;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_HasChildTopics')
    DROP PROCEDURE sp_HasChildTopics;
GO
CREATE PROCEDURE sp_HasChildTopics
@id INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM Topics WHERE MaChuDeCha = @id;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllWords')
    DROP PROCEDURE sp_GetAllWords;
GO
CREATE PROCEDURE sp_GetAllWords
AS
BEGIN
    SET NOCOUNT ON;
SELECT w.MaTu AS WordID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe ORDER BY t.TenChuDe, w.TiengAnh;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWordsByTopic')
    DROP PROCEDURE sp_GetWordsByTopic;
GO
CREATE PROCEDURE sp_GetWordsByTopic
@tid INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaTu AS WordID, TiengAnh AS EnglishWord, PhienAm AS Phonetic, NghiaTiengViet AS VietnameseMeaning, DoKho AS DifficultyLevel, BieuTuong AS EmojiIcon, DuongDanAnh AS ImagePath, MaAnh AS ImageID FROM Words WHERE MaChuDe = @tid ORDER BY TiengAnh;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWordsByTopicAndDifficulty')
    DROP PROCEDURE sp_GetWordsByTopicAndDifficulty;
GO
CREATE PROCEDURE sp_GetWordsByTopicAndDifficulty
@count INT, @topicID INT, @difficulty INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT TOP (@count) MaTu AS WordID, TiengAnh AS EnglishWord, NghiaTiengViet AS VietnameseMeaning, PhienAm AS Phonetic, BieuTuong AS EmojiIcon, DuongDanAnh AS ImagePath, MaAnh AS ImageID, DoKho AS DifficultyLevel FROM Words WHERE MaChuDe = @topicID AND DoKho = @difficulty ORDER BY NEWID();
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWordsByLevelID')
    DROP PROCEDURE sp_GetWordsByLevelID;
GO
CREATE PROCEDURE sp_GetWordsByLevelID
@LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT w.MaTu AS WordID, w.TiengAnh AS EnglishWord, w.NghiaTiengViet AS VietnameseMeaning, w.PhienAm AS Phonetic, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID, w.DoKho AS DifficultyLevel FROM LevelWords lw JOIN Words w ON lw.MaTu = w.MaTu WHERE lw.MaMuc = @LevelID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_SearchWords')
    DROP PROCEDURE sp_SearchWords;
GO
CREATE PROCEDURE sp_SearchWords
@Keyword NVARCHAR(200), @Difficulty NVARCHAR(50), @ImageFilter INT
AS
BEGIN
    SET NOCOUNT ON;
DECLARE @sql NVARCHAR(MAX);
    SET @sql = 'SELECT w.MaTu AS WordID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe WHERE 1=1';
    IF @Keyword IS NOT NULL AND @Keyword != '' SET @sql = @sql + ' AND (w.TiengAnh LIKE ''%'' + @Keyword + ''%'' OR w.NghiaTiengViet LIKE ''%'' + @Keyword + ''%'' OR w.PhienAm LIKE ''%'' + @Keyword + ''%'')';
    IF @Difficulty IS NOT NULL AND @Difficulty != '' SET @sql = @sql + ' AND w.DoKho = @Difficulty';
    IF @ImageFilter = 1 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NOT NULL AND w.DuongDanAnh != '''') OR w.MaAnh IS NOT NULL)';
    IF @ImageFilter = 2 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NULL OR w.DuongDanAnh = '''') AND w.MaAnh IS NULL)';
    SET @sql = @sql + ' ORDER BY t.TenChuDe, w.TiengAnh';
    EXEC sp_executesql @sql, N'@Keyword NVARCHAR(200), @Difficulty NVARCHAR(50)', @Keyword, @Difficulty;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWordByID')
    DROP PROCEDURE sp_GetWordByID;
GO
CREATE PROCEDURE sp_GetWordByID
@WordID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT w.MaTu AS WordID, w.MaChuDe AS TopicID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe WHERE w.MaTu = @WordID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertWord')
    DROP PROCEDURE sp_InsertWord;
GO
CREATE PROCEDURE sp_InsertWord
@TopicID INT, @EnglishWord NVARCHAR(200), @Phonetic NVARCHAR(200), @VietnameseMeaning NVARCHAR(200), @DifficultyLevel INT, @EmojiIcon NVARCHAR(50), @ImagePath NVARCHAR(MAX), @ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO Words (MaChuDe, TiengAnh, PhienAm, NghiaTiengViet, DoKho, BieuTuong, DuongDanAnh, MaAnh) VALUES (@TopicID, @EnglishWord, @Phonetic, @VietnameseMeaning, @DifficultyLevel, @EmojiIcon, @ImagePath, @ImageID);
    SELECT SCOPE_IDENTITY();
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateWord')
    DROP PROCEDURE sp_UpdateWord;
GO
CREATE PROCEDURE sp_UpdateWord
@WordID INT, @TopicID INT, @EnglishWord NVARCHAR(200), @Phonetic NVARCHAR(200), @VietnameseMeaning NVARCHAR(200), @DifficultyLevel INT, @EmojiIcon NVARCHAR(50), @ImagePath NVARCHAR(MAX), @ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
UPDATE Words SET MaChuDe = @TopicID, TiengAnh = @EnglishWord, PhienAm = @Phonetic, NghiaTiengViet = @VietnameseMeaning, DoKho = @DifficultyLevel, BieuTuong = @EmojiIcon, DuongDanAnh = @ImagePath, MaAnh = @ImageID WHERE MaTu = @WordID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteWord')
    DROP PROCEDURE sp_DeleteWord;
GO
CREATE PROCEDURE sp_DeleteWord
@WordID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM Words WHERE MaTu = @WordID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_IsWordInUse')
    DROP PROCEDURE sp_IsWordInUse;
GO
CREATE PROCEDURE sp_IsWordInUse
@WordID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM LevelWords WHERE MaTu = @WordID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_IsDuplicateEnglishWord')
    DROP PROCEDURE sp_IsDuplicateEnglishWord;
GO
CREATE PROCEDURE sp_IsDuplicateEnglishWord
@TopicID INT, @EnglishWord NVARCHAR(200), @ExcludeWordID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM Words WHERE MaChuDe = @TopicID AND TiengAnh = @EnglishWord AND MaTu != @ExcludeWordID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetWordsByFilter')
    DROP PROCEDURE sp_GetWordsByFilter;
GO
CREATE PROCEDURE sp_GetWordsByFilter
@TopicID INT, @Difficulty NVARCHAR(50), @ImageFilter INT, @Keyword NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
DECLARE @sql NVARCHAR(MAX);
    SET @sql = 'SELECT TOP 1000 w.MaTu AS WordID, w.MaChuDe AS TopicID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe WHERE 1=1';
    IF @TopicID != -1 SET @sql = @sql + ' AND w.MaChuDe = @TopicID';
    IF @Difficulty IS NOT NULL AND @Difficulty != '' SET @sql = @sql + ' AND w.DoKho = @Difficulty';
    IF @ImageFilter = 1 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NOT NULL AND w.DuongDanAnh != '''') OR w.MaAnh IS NOT NULL)';
    IF @ImageFilter = 2 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NULL OR w.DuongDanAnh = '''') AND w.MaAnh IS NULL)';
    IF @Keyword IS NOT NULL AND @Keyword != '' SET @sql = @sql + ' AND (w.TiengAnh LIKE ''%'' + @Keyword + ''%'' OR w.NghiaTiengViet LIKE ''%'' + @Keyword + ''%'' OR w.PhienAm LIKE ''%'' + @Keyword + ''%'')';
    SET @sql = @sql + ' ORDER BY t.TenChuDe, w.TiengAnh';
    EXEC sp_executesql @sql, N'@TopicID INT, @Difficulty NVARCHAR(50), @Keyword NVARCHAR(200)', @TopicID, @Difficulty, @Keyword;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetLevelsByTopic')
    DROP PROCEDURE sp_GetLevelsByTopic;
GO
CREATE PROCEDURE sp_GetLevelsByTopic
@TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaMuc AS LevelID, SoMuc AS LevelNum, TenMuc AS LevelName, DoKho AS DifficultyLevel, CheDo AS Mode, SoCauHoi AS QuestionCount FROM Levels WHERE MaChuDe = @TopicID ORDER BY SoMuc;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetLevelByID')
    DROP PROCEDURE sp_GetLevelByID;
GO
CREATE PROCEDURE sp_GetLevelByID
@LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT MaMuc AS LevelID, MaChuDe AS TopicID, SoMuc AS LevelNum, TenMuc AS LevelName, DoKho AS DifficultyLevel, CheDo AS Mode, SoCauHoi AS QuestionCount FROM Levels WHERE MaMuc = @LevelID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertLevel')
    DROP PROCEDURE sp_InsertLevel;
GO
CREATE PROCEDURE sp_InsertLevel
@TopicID INT, @LevelNum INT, @LevelName NVARCHAR(100), @DifficultyLevel NVARCHAR(50), @Mode NVARCHAR(50), @QuestionCount INT
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO Levels (MaChuDe, SoMuc, TenMuc, DoKho, CheDo, SoCauHoi) VALUES (@TopicID, @LevelNum, @LevelName, @DifficultyLevel, @Mode, @QuestionCount);
    SELECT SCOPE_IDENTITY();
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateLevel')
    DROP PROCEDURE sp_UpdateLevel;
GO
CREATE PROCEDURE sp_UpdateLevel
@LevelID INT, @LevelName NVARCHAR(100), @DifficultyLevel NVARCHAR(50), @Mode NVARCHAR(50), @QuestionCount INT
AS
BEGIN
    SET NOCOUNT ON;
UPDATE Levels SET TenMuc = @LevelName, DoKho = @DifficultyLevel, CheDo = @Mode, SoCauHoi = @QuestionCount WHERE MaMuc = @LevelID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteLevel')
    DROP PROCEDURE sp_DeleteLevel;
GO
CREATE PROCEDURE sp_DeleteLevel
@LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM Levels WHERE MaMuc = @LevelID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_AddWordToLevel')
    DROP PROCEDURE sp_AddWordToLevel;
GO
CREATE PROCEDURE sp_AddWordToLevel
@LevelID INT, @WordID INT
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO LevelWords (MaMuc, MaTu) VALUES (@LevelID, @WordID);
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_RemoveWordFromLevel')
    DROP PROCEDURE sp_RemoveWordFromLevel;
GO
CREATE PROCEDURE sp_RemoveWordFromLevel
@LevelID INT, @WordID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM LevelWords WHERE MaMuc = @LevelID AND MaTu = @WordID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ClearLevelWords')
    DROP PROCEDURE sp_ClearLevelWords;
GO
CREATE PROCEDURE sp_ClearLevelWords
@LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM LevelWords WHERE MaMuc = @LevelID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetFixedWordsForLevel')
    DROP PROCEDURE sp_GetFixedWordsForLevel;
GO
CREATE PROCEDURE sp_GetFixedWordsForLevel
@LevelID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT w.MaTu AS WordID, w.TiengAnh AS EnglishWord, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.PhienAm AS Phonetic, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID FROM LevelWords lw JOIN Words w ON lw.MaTu = w.MaTu WHERE lw.MaMuc = @LevelID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllImages')
    DROP PROCEDURE sp_GetAllImages;
GO
CREATE PROCEDURE sp_GetAllImages
@Keyword NVARCHAR(200) = ''
AS
BEGIN
    SET NOCOUNT ON;
DECLARE @sql NVARCHAR(MAX);
    SET @sql = 'SELECT i.MaAnh AS ImageID, i.ImageName, CASE WHEN EXISTS (SELECT 1 FROM Words w WHERE w.MaAnh = i.MaAnh) THEN 1 ELSE 0 END AS IsUsed FROM Images i';
    IF @Keyword IS NOT NULL AND @Keyword != '' SET @sql = @sql + ' WHERE i.ImageName LIKE ''%'' + @Keyword + ''%''';
    SET @sql = @sql + ' ORDER BY i.ImageName';
    EXEC sp_executesql @sql;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetImageData')
    DROP PROCEDURE sp_GetImageData;
GO
CREATE PROCEDURE sp_GetImageData
@ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT ImageData FROM Images WHERE MaAnh = @ImageID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertImage')
    DROP PROCEDURE sp_InsertImage;
GO
CREATE PROCEDURE sp_InsertImage
@ImageName NVARCHAR(100), @ImageData VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO Images (ImageName, ImageData) VALUES (@ImageName, @ImageData);
    SELECT SCOPE_IDENTITY();
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateImageData')
    DROP PROCEDURE sp_UpdateImageData;
GO
CREATE PROCEDURE sp_UpdateImageData
@ImageID INT, @ImageData VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
UPDATE Images SET ImageData = @ImageData WHERE MaAnh = @ImageID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteImage')
    DROP PROCEDURE sp_DeleteImage;
GO
CREATE PROCEDURE sp_DeleteImage
@ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM Images WHERE MaAnh = @ImageID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_IsImageInUse')
    DROP PROCEDURE sp_IsImageInUse;
GO
CREATE PROCEDURE sp_IsImageInUse
@ImageID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM Words WHERE MaAnh = @ImageID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_IsImageNameExists')
    DROP PROCEDURE sp_IsImageNameExists;
GO
CREATE PROCEDURE sp_IsImageNameExists
@ImageName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM Images WHERE ImageName = @ImageName;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetPlayerProgress')
    DROP PROCEDURE sp_GetPlayerProgress;
GO
CREATE PROCEDURE sp_GetPlayerProgress
@PlayerID INT, @TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT SoMuc AS LevelNum, Sao AS Stars, DiemCaoNhat AS BestScore, DaHoanThanh AS IsCompleted, ThoiDiemHoanThanh AS CompletedAt FROM PlayerProgress WHERE MaNguoiChoi = @PlayerID AND MaChuDe = @TopicID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAllPlayerProgress')
    DROP PROCEDURE sp_GetAllPlayerProgress;
GO
CREATE PROCEDURE sp_GetAllPlayerProgress
@PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT t.TenChuDe AS TopicName, pp.SoMuc AS LevelNum, pp.Sao AS Stars, pp.DiemCaoNhat AS BestScore, pp.DaHoanThanh AS IsCompleted, pp.ThoiDiemHoanThanh AS CompletedAt FROM PlayerProgress pp INNER JOIN Topics t ON pp.MaChuDe = t.MaChuDe WHERE pp.MaNguoiChoi = @PlayerID ORDER BY t.TenChuDe, pp.SoMuc;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_SaveOrUpdateProgress')
    DROP PROCEDURE sp_SaveOrUpdateProgress;
GO
CREATE PROCEDURE sp_SaveOrUpdateProgress
@PlayerID INT, @TopicID INT, @LevelNum INT, @Stars INT, @BestScore INT
AS
BEGIN
    SET NOCOUNT ON;
IF EXISTS (SELECT 1 FROM PlayerProgress WHERE MaNguoiChoi = @PlayerID AND MaChuDe = @TopicID AND SoMuc = @LevelNum)
    BEGIN
        UPDATE PlayerProgress
        SET Sao = CASE WHEN @Stars > Sao THEN @Stars ELSE Sao END,
            DiemCaoNhat = CASE WHEN @BestScore > DiemCaoNhat THEN @BestScore ELSE DiemCaoNhat END,
            DaHoanThanh = 1,
            ThoiDiemHoanThanh = GETDATE()
        WHERE MaNguoiChoi = @PlayerID AND MaChuDe = @TopicID AND SoMuc = @LevelNum;
    END
    ELSE
    BEGIN
        INSERT INTO PlayerProgress (MaNguoiChoi, MaChuDe, SoMuc, Sao, DiemCaoNhat, DaHoanThanh, ThoiDiemHoanThanh)
        VALUES (@PlayerID, @TopicID, @LevelNum, @Stars, @BestScore, 1, GETDATE());
    END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteProgressByPlayer')
    DROP PROCEDURE sp_DeleteProgressByPlayer;
GO
CREATE PROCEDURE sp_DeleteProgressByPlayer
@PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM PlayerProgress WHERE MaNguoiChoi = @PlayerID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteProgressByTopic')
    DROP PROCEDURE sp_DeleteProgressByTopic;
GO
CREATE PROCEDURE sp_DeleteProgressByTopic
@TopicID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM PlayerProgress WHERE MaChuDe = @TopicID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertScoreHistory')
    DROP PROCEDURE sp_InsertScoreHistory;
GO
CREATE PROCEDURE sp_InsertScoreHistory
@PlayerID INT, @TopicID INT, @LevelNum INT, @Score INT, @Stars INT, @TimeTaken INT
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO ScoreHistory (MaNguoiChoi, MaChuDe, SoMuc, Diem, Sao, ThoiGianChoi, ThoiDiemChoi) VALUES (@PlayerID, @TopicID, @LevelNum, @Score, @Stars, @TimeTaken, GETDATE());
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetScoreHistoryByPlayer')
    DROP PROCEDURE sp_GetScoreHistoryByPlayer;
GO
CREATE PROCEDURE sp_GetScoreHistoryByPlayer
@PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
SELECT t.TenChuDe AS TopicName, sh.SoMuc AS LevelNum, sh.Diem AS Score, sh.Sao AS Stars, sh.ThoiGianChoi AS TimeTaken, sh.ThoiDiemChoi AS PlayedAt FROM ScoreHistory sh INNER JOIN Topics t ON sh.MaChuDe = t.MaChuDe WHERE sh.MaNguoiChoi = @PlayerID ORDER BY sh.ThoiDiemChoi DESC;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetLeaderboard')
    DROP PROCEDURE sp_GetLeaderboard;
GO
CREATE PROCEDURE sp_GetLeaderboard
AS
BEGIN
    SET NOCOUNT ON;
SELECT ROW_NUMBER() OVER (ORDER BY TongDiem DESC) AS Hang, TenNguoiChoi AS Username, TongDiem AS TotalScore, TongSao AS TotalStars FROM Players ORDER BY TongDiem DESC;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteScoreHistoryByPlayer')
    DROP PROCEDURE sp_DeleteScoreHistoryByPlayer;
GO
CREATE PROCEDURE sp_DeleteScoreHistoryByPlayer
@PlayerID INT
AS
BEGIN
    SET NOCOUNT ON;
DELETE FROM ScoreHistory WHERE MaNguoiChoi = @PlayerID;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetTotalGames')
    DROP PROCEDURE sp_GetTotalGames;
GO
CREATE PROCEDURE sp_GetTotalGames
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(*) FROM ScoreHistory;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetGameRule')
    DROP PROCEDURE sp_GetGameRule;
GO
CREATE PROCEDURE sp_GetGameRule
AS
BEGIN
    SET NOCOUNT ON;
SELECT TOP 1 MaLuat AS RuleID, SoCauHoi AS QuestionCount, GioiHanThoiGian AS TimeLđiểmit, SoMang AS Lives, ThuongChuoiDiem AS StreakBonus, NguongSao1 AS Star1Threshold, NguongSao2 AS Star2Threshold, NguongSao3 AS Star3Threshold FROM GameRules ORDER BY MaLuat ASC;
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateGameRule')
    DROP PROCEDURE sp_UpdateGameRule;
GO
CREATE PROCEDURE sp_UpdateGameRule
@QuestionCount INT, @TimeLimit INT, @Lives INT, @StreakBonus INT, @Star1Threshold INT, @Star2Threshold INT, @Star3Threshold INT
AS
BEGIN
    SET NOCOUNT ON;
UPDATE GameRules SET SoCauHoi = @QuestionCount, GioiHanThoiGian = @TimeLimit, SoMang = @Lives, ThuongChuoiDiem = @StreakBonus, NguongSao1 = @Star1Threshold, NguongSao2 = @Star2Threshold, NguongSao3 = @Star3Threshold WHERE MaLuat = (SELECT TOP 1 MaLuat FROM GameRules ORDER BY MaLuat ASC);
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertGameRule')
    DROP PROCEDURE sp_InsertGameRule;
GO
CREATE PROCEDURE sp_InsertGameRule
@QuestionCount INT, @TimeLimit INT, @Lives INT, @StreakBonus INT, @Star1Threshold INT, @Star2Threshold INT, @Star3Threshold INT
AS
BEGIN
    SET NOCOUNT ON;
INSERT INTO GameRules (SoCauHoi, GioiHanThoiGian, SoMang, ThuongChuoiDiem, NguongSao1, NguongSao2, NguongSao3) VALUES (@QuestionCount, @TimeLimit, @Lives, @StreakBonus, @Star1Threshold, @Star2Threshold, @Star3Threshold);
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_HasAnyGameRule')
    DROP PROCEDURE sp_HasAnyGameRule;
GO
CREATE PROCEDURE sp_HasAnyGameRule
AS
BEGIN
    SET NOCOUNT ON;
SELECT COUNT(1) FROM GameRules;
END
GO

