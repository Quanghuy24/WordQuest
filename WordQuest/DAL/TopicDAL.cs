using System.Data;
using Microsoft.Data.SqlClient;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class TopicDAL
    {
        // Lấy tất cả chủ đề
        public DataTable LayTatCaChuDe()
        {
            const string sql = "sp_GetAllTopics";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        // Lấy chủ đề gốc (không có cha)
        public DataTable LayChuDeGoc()
        {
            const string sql = "sp_GetParentTopics";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure);
        }

        // Lấy chủ đề con theo ID cha
        public DataTable LayChuDeCon(int parentID)
        {
            const string sql = "sp_GetChildTopics";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@pid", parentID));
        }

        // Lấy thông tin chi tiết chủ đề theo ID
        public TopicDTO LayChuDeTheoID(int topicID)
        {
            const string sql = "sp_GetTopicByID";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@id", topicID));
            if (dt.Rows.Count == 0) return null;

            var row = dt.Rows[0];
            return new TopicDTO
            {
                TopicID = Convert.ToInt32(row["TopicID"]),
                TopicName = row["TopicName"].ToString() ?? "",
                TopicIcon = row["TopicIcon"].ToString() ?? "",
                StarsToUnlock = Convert.ToInt32(row["StarsToUnlock"]),
                ParentID = row["ParentID"] == DBNull.Value ? null : Convert.ToInt32(row["ParentID"]),
                SortOrder = Convert.ToInt32(row["SortOrder"])
            };
        }

        // Thêm chủ đề mới
        public int ThemChuDe(TopicDTO topic)
        {
            const string sql = "sp_InsertTopic";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[]
                {
                new SqlParameter("@name", topic.TopicName),
                new SqlParameter("@icon", topic.TopicIcon),
                new SqlParameter("@stars", topic.StarsToUnlock),
                new SqlParameter("@parentID", (object)topic.ParentID ?? DBNull.Value)}, null, CommandType.StoredProcedure));
        }

        // Cập nhật thông tin chủ đề
        public bool CapNhatChuDe(TopicDTO topic)
        {
            const string sql = "sp_UpdateTopic";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
                {
                new SqlParameter("@id", topic.TopicID),
                new SqlParameter("@name", topic.TopicName),
                new SqlParameter("@icon", topic.TopicIcon),
                new SqlParameter("@stars", topic.StarsToUnlock),
                new SqlParameter("@parentID", (object)topic.ParentID ?? DBNull.Value)}, null, CommandType.StoredProcedure) > 0;
        }

        // Xóa chủ đề theo ID
        public bool XoaChuDe(int topicID)
        {
            const string sql = "sp_DeleteTopic";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@id", topicID) }, null, CommandType.StoredProcedure) > 0;
        }

        // Kiểm tra chủ đề có con không
        public bool HasChildren(int topicID)
        {
            const string sql = "sp_HasChildTopics";
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@id", topicID) }, null, CommandType.StoredProcedure)) > 0;
        }
    }
}
