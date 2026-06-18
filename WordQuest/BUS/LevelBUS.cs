using System;
using System.Data;
using WordQuest.DAL;
using WordQuest.DTO;

namespace WordQuest.BUS
{
    public class LevelBUS
    {
        private readonly LevelDAL _levelDAL = new();

        public DataTable LayMucDoTheoChuDe(int topicID) => _levelDAL.LayMucDoTheoChuDe(topicID);

        public LevelDTO LayMucDoTheoID(int levelID) => _levelDAL.LayMucDoTheoID(levelID);

        public int ThemMucDo(LevelDTO level)
        {
            if (level.TopicID <= 0)
                throw new Exception("ID chủ đề không hợp lệ!");
            if (string.IsNullOrWhiteSpace(level.LevelName))
                throw new Exception("Tên màn không được để trống!");
            if (string.IsNullOrWhiteSpace(level.DifficultyLevel))
                throw new Exception("Độ khó không được để trống!");
            if (level.QuestionCount < 1 || level.QuestionCount > 50)
                throw new Exception("Số câu hỏi phải từ 1 đến 50!");

            return _levelDAL.ThemMucDo(level);
        }

        public bool CapNhatMucDo(LevelDTO level)
        {
            if (level.LevelID <= 0)
                throw new Exception("ID màn không hợp lệ!");
            if (string.IsNullOrWhiteSpace(level.LevelName))
                throw new Exception("Tên màn không được để trống!");

            return _levelDAL.CapNhatMucDo(level);
        }

        public bool XoaMucDo(int levelID)
        {
            if (levelID <= 0)
                throw new Exception("ID màn không hợp lệ!");

            _levelDAL.XoaTatCaTuMucDo(levelID);
            return _levelDAL.XoaMucDo(levelID);
        }

        public bool ThemTuVaoMucDo(int levelID, int wordID) => _levelDAL.ThemTuVaoMucDo(levelID, wordID);
        public bool XoaTuKhoiMucDo(int levelID, int wordID) => _levelDAL.XoaTuKhoiMucDo(levelID, wordID);
        public bool XoaTatCaTuMucDo(int levelID) => _levelDAL.XoaTatCaTuMucDo(levelID);
        public DataTable LayTuCoDinhMucDo(int levelID) => _levelDAL.LayTuCoDinhMucDo(levelID);
    }
}
