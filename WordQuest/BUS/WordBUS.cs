using System;
using System.Data;
using WordQuest.DAL;
using WordQuest.DTO;

namespace WordQuest.BUS
{
    public class WordBUS
    {
        private readonly WordDAL _wordDAL = new();
        public DataTable LayTatCaTu() => _wordDAL.LayTatCaTu(); 
        public DataTable LayTuTheoChuDe(int topicID) => _wordDAL.LayTuTheoChuDe(topicID); 
        public DataTable LayTuTheoChuDeVaDoKho(int topicID, int difficultyLevel, int count) 
            => _wordDAL.LayTuTheoChuDeVaDoKho(topicID, difficultyLevel, count);
        public DataTable LayTuTheoIDMucDo(int levelID) => _wordDAL.LayTuTheoIDMucDo(levelID);
        public DataTable TimKiemTu(string keyword, string difficulty, string imageFilter) 
            => _wordDAL.TimKiemTu(keyword, difficulty, imageFilter);
        public WordDTO LayTuTheoID(int wordID) => _wordDAL.LayTuTheoID(wordID);

        public int ThemTu(WordDTO word)
        {
            if (string.IsNullOrWhiteSpace(word.EnglishWord))
                throw new Exception("Từ tiếng Anh không được để trống!");
            if (string.IsNullOrWhiteSpace(word.VietnameseMeaning))
                throw new Exception("Nghĩa tiếng Việt không được để trống!");
            if (word.DifficultyLevel < 1 || word.DifficultyLevel > 3)
                throw new Exception("Độ khó phải từ 1 đến 3!");
            if (_wordDAL.KiemTraTuTiengAnhTrung(word.TopicID, word.EnglishWord, 0))
                throw new Exception($"Từ '{word.EnglishWord}' đã tồn tại trong chủ đề này!");

            return _wordDAL.ThemTu(word);
        }

        public bool CapNhatTu(WordDTO word)
        {
            if (word.WordID <= 0)
                throw new Exception("ID từ vựng không hợp lệ!");
            if (string.IsNullOrWhiteSpace(word.EnglishWord))
                throw new Exception("Từ tiếng Anh không được để trống!");
            if (string.IsNullOrWhiteSpace(word.VietnameseMeaning))
                throw new Exception("Nghĩa tiếng Việt không được để trống!");
            if (_wordDAL.KiemTraTuTiengAnhTrung(word.TopicID, word.EnglishWord, word.WordID))
                throw new Exception($"Từ '{word.EnglishWord}' đã tồn tại trong chủ đề này!");

            return _wordDAL.CapNhatTu(word);
        }

        public bool XoaTu(int wordID)
        {
            if (wordID <= 0)
                throw new Exception("ID từ vựng không hợp lệ!");
            if (_wordDAL.IsWordInUse(wordID))
                throw new Exception("Từ này đang được dùng trong màn chơi! Không thể xóa.");

            return _wordDAL.XoaTu(wordID);
        }

        public bool KiemTraTuTiengAnhTrung(int topicID, string englishWord, int excludeWordID = 0)
            => _wordDAL.KiemTraTuTiengAnhTrung(topicID, englishWord, excludeWordID);

        public List<WordDTO> LayDanhSachTu(WordFilterDTO filter)
        {
            return _wordDAL.LayDanhSachTu(filter);
        }
    }
}
