using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        var sb = new StringBuilder();

        void AddSP(string name, string parameters, string body)
        {
            sb.AppendLine($"IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = '{name}')");
            sb.AppendLine($"    DROP PROCEDURE {name};");
            sb.AppendLine("GO");
            sb.AppendLine($"CREATE PROCEDURE {name}");
            if (!string.IsNullOrWhiteSpace(parameters))
                sb.AppendLine(parameters);
            sb.AppendLine("AS");
            sb.AppendLine("BEGIN");
            sb.AppendLine("    SET NOCOUNT ON;");
            sb.AppendLine(body);
            sb.AppendLine("END");
            sb.AppendLine("GO");
            sb.AppendLine();
        }

        // ================= PLAYER =================
        AddSP("sp_Login", "@Username NVARCHAR(100), @Password NVARCHAR(255)", 
            "SELECT MaNguoiChoi AS PlayerID, TenNguoiChoi AS Username, MatKhau AS PasswordHash, TongDiem AS TotalScore, TongSao AS TotalStars, NgayTao AS CreatedAt FROM Players WHERE TenNguoiChoi = @Username AND MatKhau = @Password;");
        
        AddSP("sp_GetPlayerByUsername", "@Username NVARCHAR(100)", 
            "SELECT MaNguoiChoi AS PlayerID, TenNguoiChoi AS Username, MatKhau AS PasswordHash, TongDiem AS TotalScore, TongSao AS TotalStars, NgayTao AS CreatedAt FROM Players WHERE TenNguoiChoi = @Username;");
            
        AddSP("sp_CheckUsernameExists", "@Username NVARCHAR(100)", 
            "SELECT COUNT(1) FROM Players WHERE TenNguoiChoi = @Username;");

        AddSP("sp_InsertPlayer", "@Username NVARCHAR(100), @Password NVARCHAR(255)", 
            "INSERT INTO Players (TenNguoiChoi, MatKhau, TongDiem, TongSao, NgayTao) VALUES (@Username, @Password, 0, 0, GETDATE());\n    SELECT SCOPE_IDENTITY();");

        AddSP("sp_ChangePassword", "@PlayerID INT, @NewPassword NVARCHAR(255)", 
            "UPDATE Players SET MatKhau = @NewPassword WHERE MaNguoiChoi = @PlayerID;");

        AddSP("sp_UpdatePlayerScore", "@PlayerID INT, @Score INT, @Stars INT", 
            "UPDATE Players SET TongDiem = TongDiem + @Score, TongSao = TongSao + @Stars WHERE MaNguoiChoi = @PlayerID;");

        AddSP("sp_GetAllPlayers", "", 
            "SELECT MaNguoiChoi AS PlayerID, TenNguoiChoi AS Username, TongDiem AS TotalScore, TongSao AS TotalStars, NgayTao AS CreatedAt FROM Players ORDER BY TongDiem DESC;");

        AddSP("sp_DeletePlayer", "@PlayerID INT", 
            "DELETE FROM Players WHERE MaNguoiChoi = @PlayerID;");

        // ================= TOPIC =================
        AddSP("sp_GetAllTopics", "", 
            "SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock, MaChuDeCha AS ParentID, ThuTuSapXep AS SortOrder FROM Topics ORDER BY ThuTuSapXep, TenChuDe;");

        AddSP("sp_GetParentTopics", "", 
            "SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock FROM Topics WHERE MaChuDeCha IS NULL ORDER BY ThuTuSapXep;");

        AddSP("sp_GetChildTopics", "@pid INT", 
            "SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock FROM Topics WHERE MaChuDeCha = @pid ORDER BY ThuTuSapXep;");

        AddSP("sp_GetTopicByID", "@id INT", 
            "SELECT MaChuDe AS TopicID, TenChuDe AS TopicName, BieuTuongChuDe AS TopicIcon, SaoDeMoKhoa AS StarsToUnlock, MaChuDeCha AS ParentID, ThuTuSapXep AS SortOrder FROM Topics WHERE MaChuDe = @id;");

        AddSP("sp_InsertTopic", "@name NVARCHAR(100), @icon NVARCHAR(50), @stars INT, @parentID INT = NULL", 
            "INSERT INTO Topics (TenChuDe, BieuTuongChuDe, SaoDeMoKhoa, MaChuDeCha, ThuTuSapXep) VALUES (@name, @icon, @stars, @parentID, (SELECT ISNULL(MAX(ThuTuSapXep),0)+1 FROM Topics));\n    SELECT SCOPE_IDENTITY();");

        AddSP("sp_UpdateTopic", "@id INT, @name NVARCHAR(100), @icon NVARCHAR(50), @stars INT, @parentID INT = NULL", 
            "UPDATE Topics SET TenChuDe = @name, BieuTuongChuDe = @icon, SaoDeMoKhoa = @stars, MaChuDeCha = @parentID WHERE MaChuDe = @id;");

        AddSP("sp_DeleteTopic", "@id INT", 
            "DELETE FROM Topics WHERE MaChuDe = @id;");

        AddSP("sp_HasChildTopics", "@id INT", 
            "SELECT COUNT(1) FROM Topics WHERE MaChuDeCha = @id;");

        // ================= WORD =================
        AddSP("sp_GetAllWords", "", 
            "SELECT w.MaTu AS WordID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe ORDER BY t.TenChuDe, w.TiengAnh;");

        AddSP("sp_GetWordsByTopic", "@tid INT", 
            "SELECT MaTu AS WordID, TiengAnh AS EnglishWord, PhienAm AS Phonetic, NghiaTiengViet AS VietnameseMeaning, DoKho AS DifficultyLevel, BieuTuong AS EmojiIcon, DuongDanAnh AS ImagePath, MaAnh AS ImageID FROM Words WHERE MaChuDe = @tid ORDER BY TiengAnh;");

        AddSP("sp_GetWordsByTopicAndDifficulty", "@count INT, @topicID INT, @difficulty INT", 
            "SELECT TOP (@count) MaTu AS WordID, TiengAnh AS EnglishWord, NghiaTiengViet AS VietnameseMeaning, PhienAm AS Phonetic, BieuTuong AS EmojiIcon, DuongDanAnh AS ImagePath, MaAnh AS ImageID, DoKho AS DifficultyLevel FROM Words WHERE MaChuDe = @topicID AND DoKho = @difficulty ORDER BY NEWID();");

        AddSP("sp_GetWordsByLevelID", "@LevelID INT", 
            "SELECT w.MaTu AS WordID, w.TiengAnh AS EnglishWord, w.NghiaTiengViet AS VietnameseMeaning, w.PhienAm AS Phonetic, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID, w.DoKho AS DifficultyLevel FROM LevelWords lw JOIN Words w ON lw.MaTu = w.MaTu WHERE lw.MaMuc = @LevelID;");

        AddSP("sp_SearchWords", "@Keyword NVARCHAR(200), @Difficulty NVARCHAR(50), @ImageFilter INT", 
            @"DECLARE @sql NVARCHAR(MAX);
    SET @sql = 'SELECT w.MaTu AS WordID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe WHERE 1=1';
    IF @Keyword IS NOT NULL AND @Keyword != '' SET @sql = @sql + ' AND (w.TiengAnh LIKE ''%'' + @Keyword + ''%'' OR w.NghiaTiengViet LIKE ''%'' + @Keyword + ''%'' OR w.PhienAm LIKE ''%'' + @Keyword + ''%'')';
    IF @Difficulty IS NOT NULL AND @Difficulty != '' SET @sql = @sql + ' AND w.DoKho = @Difficulty';
    IF @ImageFilter = 1 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NOT NULL AND w.DuongDanAnh != '''') OR w.MaAnh IS NOT NULL)';
    IF @ImageFilter = 2 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NULL OR w.DuongDanAnh = '''') AND w.MaAnh IS NULL)';
    SET @sql = @sql + ' ORDER BY t.TenChuDe, w.TiengAnh';
    EXEC sp_executesql @sql, N'@Keyword NVARCHAR(200), @Difficulty NVARCHAR(50)', @Keyword, @Difficulty;");

        AddSP("sp_GetWordByID", "@WordID INT", 
            "SELECT w.MaTu AS WordID, w.MaChuDe AS TopicID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe WHERE w.MaTu = @WordID;");

        AddSP("sp_InsertWord", "@TopicID INT, @EnglishWord NVARCHAR(200), @Phonetic NVARCHAR(200), @VietnameseMeaning NVARCHAR(200), @DifficultyLevel INT, @EmojiIcon NVARCHAR(50), @ImagePath NVARCHAR(MAX), @ImageID INT", 
            "INSERT INTO Words (MaChuDe, TiengAnh, PhienAm, NghiaTiengViet, DoKho, BieuTuong, DuongDanAnh, MaAnh) VALUES (@TopicID, @EnglishWord, @Phonetic, @VietnameseMeaning, @DifficultyLevel, @EmojiIcon, @ImagePath, @ImageID);\n    SELECT SCOPE_IDENTITY();");

        AddSP("sp_UpdateWord", "@WordID INT, @TopicID INT, @EnglishWord NVARCHAR(200), @Phonetic NVARCHAR(200), @VietnameseMeaning NVARCHAR(200), @DifficultyLevel INT, @EmojiIcon NVARCHAR(50), @ImagePath NVARCHAR(MAX), @ImageID INT", 
            "UPDATE Words SET MaChuDe = @TopicID, TiengAnh = @EnglishWord, PhienAm = @Phonetic, NghiaTiengViet = @VietnameseMeaning, DoKho = @DifficultyLevel, BieuTuong = @EmojiIcon, DuongDanAnh = @ImagePath, MaAnh = @ImageID WHERE MaTu = @WordID;");

        AddSP("sp_DeleteWord", "@WordID INT", 
            "DELETE FROM Words WHERE MaTu = @WordID;");

        AddSP("sp_IsWordInUse", "@WordID INT", 
            "SELECT COUNT(1) FROM LevelWords WHERE MaTu = @WordID;");

        AddSP("sp_IsDuplicateEnglishWord", "@TopicID INT, @EnglishWord NVARCHAR(200), @ExcludeWordID INT", 
            "SELECT COUNT(1) FROM Words WHERE MaChuDe = @TopicID AND TiengAnh = @EnglishWord AND MaTu != @ExcludeWordID;");

        AddSP("sp_GetWordsByFilter", "@TopicID INT, @Difficulty NVARCHAR(50), @ImageFilter INT, @Keyword NVARCHAR(200)", 
            @"DECLARE @sql NVARCHAR(MAX);
    SET @sql = 'SELECT TOP 1000 w.MaTu AS WordID, w.MaChuDe AS TopicID, t.TenChuDe AS TopicName, w.TiengAnh AS EnglishWord, w.PhienAm AS Phonetic, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID FROM Words w INNER JOIN Topics t ON w.MaChuDe = t.MaChuDe WHERE 1=1';
    IF @TopicID != -1 SET @sql = @sql + ' AND w.MaChuDe = @TopicID';
    IF @Difficulty IS NOT NULL AND @Difficulty != '' SET @sql = @sql + ' AND w.DoKho = @Difficulty';
    IF @ImageFilter = 1 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NOT NULL AND w.DuongDanAnh != '''') OR w.MaAnh IS NOT NULL)';
    IF @ImageFilter = 2 SET @sql = @sql + ' AND ((w.DuongDanAnh IS NULL OR w.DuongDanAnh = '''') AND w.MaAnh IS NULL)';
    IF @Keyword IS NOT NULL AND @Keyword != '' SET @sql = @sql + ' AND (w.TiengAnh LIKE ''%'' + @Keyword + ''%'' OR w.NghiaTiengViet LIKE ''%'' + @Keyword + ''%'' OR w.PhienAm LIKE ''%'' + @Keyword + ''%'')';
    SET @sql = @sql + ' ORDER BY t.TenChuDe, w.TiengAnh';
    EXEC sp_executesql @sql, N'@TopicID INT, @Difficulty NVARCHAR(50), @Keyword NVARCHAR(200)', @TopicID, @Difficulty, @Keyword;");

        // ================= LEVEL =================
        AddSP("sp_GetLevelsByTopic", "@TopicID INT", 
            "SELECT MaMuc AS LevelID, SoMuc AS LevelNum, TenMuc AS LevelName, DoKho AS DifficultyLevel, CheDo AS Mode, SoCauHoi AS QuestionCount FROM Levels WHERE MaChuDe = @TopicID ORDER BY SoMuc;");

        AddSP("sp_GetLevelByID", "@LevelID INT", 
            "SELECT MaMuc AS LevelID, MaChuDe AS TopicID, SoMuc AS LevelNum, TenMuc AS LevelName, DoKho AS DifficultyLevel, CheDo AS Mode, SoCauHoi AS QuestionCount FROM Levels WHERE MaMuc = @LevelID;");

        AddSP("sp_InsertLevel", "@TopicID INT, @LevelNum INT, @LevelName NVARCHAR(100), @DifficultyLevel NVARCHAR(50), @Mode NVARCHAR(50), @QuestionCount INT", 
            "INSERT INTO Levels (MaChuDe, SoMuc, TenMuc, DoKho, CheDo, SoCauHoi) VALUES (@TopicID, @LevelNum, @LevelName, @DifficultyLevel, @Mode, @QuestionCount);\n    SELECT SCOPE_IDENTITY();");

        AddSP("sp_UpdateLevel", "@LevelID INT, @LevelName NVARCHAR(100), @DifficultyLevel NVARCHAR(50), @Mode NVARCHAR(50), @QuestionCount INT", 
            "UPDATE Levels SET TenMuc = @LevelName, DoKho = @DifficultyLevel, CheDo = @Mode, SoCauHoi = @QuestionCount WHERE MaMuc = @LevelID;");

        AddSP("sp_DeleteLevel", "@LevelID INT", 
            "DELETE FROM Levels WHERE MaMuc = @LevelID;");

        AddSP("sp_AddWordToLevel", "@LevelID INT, @WordID INT", 
            "INSERT INTO LevelWords (MaMuc, MaTu) VALUES (@LevelID, @WordID);");

        AddSP("sp_RemoveWordFromLevel", "@LevelID INT, @WordID INT", 
            "DELETE FROM LevelWords WHERE MaMuc = @LevelID AND MaTu = @WordID;");

        AddSP("sp_ClearLevelWords", "@LevelID INT", 
            "DELETE FROM LevelWords WHERE MaMuc = @LevelID;");

        AddSP("sp_GetFixedWordsForLevel", "@LevelID INT", 
            "SELECT w.MaTu AS WordID, w.TiengAnh AS EnglishWord, w.NghiaTiengViet AS VietnameseMeaning, w.DoKho AS DifficultyLevel, w.PhienAm AS Phonetic, w.BieuTuong AS EmojiIcon, w.DuongDanAnh AS ImagePath, w.MaAnh AS ImageID FROM LevelWords lw JOIN Words w ON lw.MaTu = w.MaTu WHERE lw.MaMuc = @LevelID;");

        // ================= IMAGE =================
        AddSP("sp_GetAllImages", "@Keyword NVARCHAR(200) = ''", 
            @"DECLARE @sql NVARCHAR(MAX);
    SET @sql = 'SELECT i.MaAnh AS ImageID, i.ImageName, CASE WHEN EXISTS (SELECT 1 FROM Words w WHERE w.MaAnh = i.MaAnh) THEN 1 ELSE 0 END AS IsUsed FROM Images i';
    IF @Keyword IS NOT NULL AND @Keyword != '' SET @sql = @sql + ' WHERE i.ImageName LIKE ''%'' + @Keyword + ''%''';
    SET @sql = @sql + ' ORDER BY i.ImageName';
    EXEC sp_executesql @sql;");

        AddSP("sp_GetImageData", "@ImageID INT", 
            "SELECT ImageData FROM Images WHERE MaAnh = @ImageID;");

        AddSP("sp_InsertImage", "@ImageName NVARCHAR(100), @ImageData VARBINARY(MAX)", 
            "INSERT INTO Images (ImageName, ImageData) VALUES (@ImageName, @ImageData);\n    SELECT SCOPE_IDENTITY();");

        AddSP("sp_UpdateImageData", "@ImageID INT, @ImageData VARBINARY(MAX)", 
            "UPDATE Images SET ImageData = @ImageData WHERE MaAnh = @ImageID;");

        AddSP("sp_DeleteImage", "@ImageID INT", 
            "DELETE FROM Images WHERE MaAnh = @ImageID;");

        AddSP("sp_IsImageInUse", "@ImageID INT", 
            "SELECT COUNT(1) FROM Words WHERE MaAnh = @ImageID;");

        AddSP("sp_IsImageNameExists", "@ImageName NVARCHAR(100)", 
            "SELECT COUNT(1) FROM Images WHERE ImageName = @ImageName;");

        // ================= PLAYER PROGRESS =================
        AddSP("sp_GetPlayerProgress", "@PlayerID INT, @TopicID INT", 
            "SELECT SoMuc AS LevelNum, Sao AS Stars, DiemCaoNhat AS BestScore, DaHoanThanh AS IsCompleted, ThoiDiemHoanThanh AS CompletedAt FROM PlayerProgress WHERE MaNguoiChoi = @PlayerID AND MaChuDe = @TopicID;");

        AddSP("sp_GetAllPlayerProgress", "@PlayerID INT", 
            "SELECT t.TenChuDe AS TopicName, pp.SoMuc AS LevelNum, pp.Sao AS Stars, pp.DiemCaoNhat AS BestScore, pp.DaHoanThanh AS IsCompleted, pp.ThoiDiemHoanThanh AS CompletedAt FROM PlayerProgress pp INNER JOIN Topics t ON pp.MaChuDe = t.MaChuDe WHERE pp.MaNguoiChoi = @PlayerID ORDER BY t.TenChuDe, pp.SoMuc;");

        AddSP("sp_SaveOrUpdateProgress", "@PlayerID INT, @TopicID INT, @LevelNum INT, @Stars INT, @BestScore INT", 
            @"IF EXISTS (SELECT 1 FROM PlayerProgress WHERE MaNguoiChoi = @PlayerID AND MaChuDe = @TopicID AND SoMuc = @LevelNum)
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
    END");

        AddSP("sp_DeleteProgressByPlayer", "@PlayerID INT", 
            "DELETE FROM PlayerProgress WHERE MaNguoiChoi = @PlayerID;");

        AddSP("sp_DeleteProgressByTopic", "@TopicID INT", 
            "DELETE FROM PlayerProgress WHERE MaChuDe = @TopicID;");

        // ================= SCORE HISTORY =================
        AddSP("sp_InsertScoreHistory", "@PlayerID INT, @TopicID INT, @LevelNum INT, @Score INT, @Stars INT, @TimeTaken INT", 
            "INSERT INTO ScoreHistory (MaNguoiChoi, MaChuDe, SoMuc, Diem, Sao, ThoiGianChoi, ThoiDiemChoi) VALUES (@PlayerID, @TopicID, @LevelNum, @Score, @Stars, @TimeTaken, GETDATE());");

        AddSP("sp_GetScoreHistoryByPlayer", "@PlayerID INT", 
            "SELECT t.TenChuDe AS TopicName, sh.SoMuc AS LevelNum, sh.Diem AS Score, sh.Sao AS Stars, sh.ThoiGianChoi AS TimeTaken, sh.ThoiDiemChoi AS PlayedAt FROM ScoreHistory sh INNER JOIN Topics t ON sh.MaChuDe = t.MaChuDe WHERE sh.MaNguoiChoi = @PlayerID ORDER BY sh.ThoiDiemChoi DESC;");

        AddSP("sp_GetLeaderboard", "", 
            "SELECT ROW_NUMBER() OVER (ORDER BY TongDiem DESC) AS Hang, TenNguoiChoi AS Username, TongDiem AS TotalScore, TongSao AS TotalStars FROM Players ORDER BY TongDiem DESC;");

        AddSP("sp_DeleteScoreHistoryByPlayer", "@PlayerID INT", 
            "DELETE FROM ScoreHistory WHERE MaNguoiChoi = @PlayerID;");

        AddSP("sp_GetTotalGames", "", 
            "SELECT COUNT(*) FROM ScoreHistory;");

        // ================= GAME RULE =================
        AddSP("sp_GetGameRule", "", 
            "SELECT TOP 1 MaLuat AS RuleID, SoCauHoi AS QuestionCount, GioiHanThoiGian AS TimeLđiểmit, SoMang AS Lives, ThuongChuoiDiem AS StreakBonus, NguongSao1 AS Star1Threshold, NguongSao2 AS Star2Threshold, NguongSao3 AS Star3Threshold FROM GameRules ORDER BY MaLuat ASC;");

        AddSP("sp_UpdateGameRule", "@QuestionCount INT, @TimeLimit INT, @Lives INT, @StreakBonus INT, @Star1Threshold INT, @Star2Threshold INT, @Star3Threshold INT", 
            "UPDATE GameRules SET SoCauHoi = @QuestionCount, GioiHanThoiGian = @TimeLimit, SoMang = @Lives, ThuongChuoiDiem = @StreakBonus, NguongSao1 = @Star1Threshold, NguongSao2 = @Star2Threshold, NguongSao3 = @Star3Threshold WHERE MaLuat = (SELECT TOP 1 MaLuat FROM GameRules ORDER BY MaLuat ASC);");

        AddSP("sp_InsertGameRule", "@QuestionCount INT, @TimeLimit INT, @Lives INT, @StreakBonus INT, @Star1Threshold INT, @Star2Threshold INT, @Star3Threshold INT", 
            "INSERT INTO GameRules (SoCauHoi, GioiHanThoiGian, SoMang, ThuongChuoiDiem, NguongSao1, NguongSao2, NguongSao3) VALUES (@QuestionCount, @TimeLimit, @Lives, @StreakBonus, @Star1Threshold, @Star2Threshold, @Star3Threshold);");

        AddSP("sp_HasAnyGameRule", "", 
            "SELECT COUNT(1) FROM GameRules;");

        File.WriteAllText("CreateAllSPs_Clean.sql", sb.ToString());
        Console.WriteLine("Successfully created CreateAllSPs_Clean.sql");
    }
}
