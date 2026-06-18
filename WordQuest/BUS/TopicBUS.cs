using System;
using System.Data;
using WordQuest.DAL;
using WordQuest.DTO;

namespace WordQuest.BUS
{
    public class TopicBUS
    {
        private readonly TopicDAL _topicDAL = new();

        public DataTable LayTatCaChuDe() => _topicDAL.LayTatCaChuDe();

        public DataTable LayChuDeGoc() => _topicDAL.LayChuDeGoc();

        public DataTable LayChuDeCon(int parentID) => _topicDAL.LayChuDeCon(parentID);

        public TopicDTO LayChuDeTheoID(int topicID) => _topicDAL.LayChuDeTheoID(topicID);

        public int ThemChuDe(TopicDTO topic)
        {
            if (string.IsNullOrWhiteSpace(topic.TopicName))
                throw new Exception("Tên chủ đề không được để trống!");
            if (topic.StarsToUnlock < 0)
                throw new Exception("Số sao yêu cầu không hợp lệ!");

            return _topicDAL.ThemChuDe(topic);
        }

        public bool CapNhatChuDe(TopicDTO topic)
        {
            if (topic.TopicID <= 0)
                throw new Exception("ID chủ đề không hợp lệ!");
            if (string.IsNullOrWhiteSpace(topic.TopicName))
                throw new Exception("Tên chủ đề không được để trống!");

            return _topicDAL.CapNhatChuDe(topic);
        }

        public bool XoaChuDe(int topicID)
        {
            if (topicID <= 0)
                throw new Exception("ID chủ đề không hợp lệ!");
            if (_topicDAL.HasChildren(topicID))
                throw new Exception("Chủ đề này có chủ đề con! Vui lòng xóa chủ đề con trước.");

            return _topicDAL.XoaChuDe(topicID);
        }
    }
}
