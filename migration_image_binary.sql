-- ============================================================
-- WORDQUEST - SQL MIGRATION: Lưu ảnh Binary vào Database
-- Chạy script này 1 lần trên SQL Server Management Studio
-- Thứ tự: Chạy từng BLOCK theo hướng dẫn bên dưới
-- ============================================================

-- ============================================================
-- BLOCK 1: Thêm cột ImageData (VARBINARY) vào bảng Images
-- ============================================================
-- Kiểm tra cột đã tồn tại chưa, nếu chưa thì thêm
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Images' AND COLUMN_NAME = 'ImageData'
)
BEGIN
    ALTER TABLE Images ADD ImageData VARBINARY(MAX) NULL;
    PRINT 'Đã thêm cột ImageData vào bảng Images.';
END
ELSE
    PRINT 'Cột ImageData đã tồn tại.';

GO

-- ============================================================
-- BLOCK 2: Thêm cột ImageID vào bảng Words (FK đến Images)
--          Dùng để thay thế cột ImagePath (string đường dẫn)
-- ============================================================
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Words' AND COLUMN_NAME = 'ImageID'
)
BEGIN
    ALTER TABLE Words ADD ImageID INT NULL;
    PRINT 'Đã thêm cột ImageID vào bảng Words.';
END
ELSE
    PRINT 'Cột ImageID đã tồn tại.';

GO

-- ============================================================
-- BLOCK 3: Liên kết ImageID trong Words với bảng Images
--          (dựa trên ImagePath hiện có)
-- ============================================================
UPDATE w
SET w.ImageID = i.ImageID
FROM Words w
INNER JOIN Images i ON w.ImagePath = i.ImagePath
WHERE w.ImagePath IS NOT NULL AND w.ImagePath != '';

PRINT 'Đã liên kết ImageID cho các từ có ảnh.';

GO

-- ============================================================
-- BLOCK 4 (TÙY CHỌN): Thêm Foreign Key constraint
-- ============================================================
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
    WHERE TABLE_NAME = 'Words' AND CONSTRAINT_NAME = 'FK_Words_Images'
)
BEGIN
    ALTER TABLE Words
    ADD CONSTRAINT FK_Words_Images
    FOREIGN KEY (ImageID) REFERENCES Images(ImageID)
    ON DELETE SET NULL;
    PRINT 'Đã thêm FK_Words_Images.';
END

GO

-- ============================================================
-- BLOCK 5 (SAU KHI ĐÃ IMPORT HẾT ẢNH QUA APP):
--         Xóa cột cũ ImagePath khỏi Words (KHÔNG CHẠY VỘI)
-- ============================================================
-- UNCOMMENT khi đã chắc chắn không cần ImagePath nữa:
-- ALTER TABLE Words DROP COLUMN ImagePath;
-- PRINT 'Đã xóa cột ImagePath khỏi Words.';

GO

-- ============================================================
-- KIỂM TRA KẾT QUẢ
-- ============================================================
SELECT
    'Images table columns' AS Info,
    COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Images'

UNION ALL

SELECT
    'Words table columns',
    COLUMN_NAME, DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Words'
ORDER BY Info, COLUMN_NAME;
