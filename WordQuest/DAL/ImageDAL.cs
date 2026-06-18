using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace WordQuest.DAL
{
    public class ImageDAL
    {
        //Lấy danh sách ảnh (KHÃ”NG kèm binary để tránh tải nặng)
        public DataTable LayTatCaHinh(string keyword = "")
        {
            const string sql = "sp_GetAllImages";
            return DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure, new SqlParameter("@Keyword", keyword ?? ""));
        }

        //Lấy binary của một ảnh theo ImageID
        public byte[] LayDuLieuHinh(int imageID)
        {
            const string sql = "sp_GetImageData";
            var result = DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@ImageID", imageID) }, null, CommandType.StoredProcedure);
            if (result == null || result == DBNull.Value) return null;
            return (byte[])result;
        }

        //Lấy binary của một ảnh theo ImageID (Async)
        public async Task<byte[]> LayDuLieuHinhAsync(int imageID)
        {
            const string sql = "sp_GetImageData";
            var result = await DatabaseHelper.ExecuteScalarAsync(sql, new SqlParameter[] { new SqlParameter("@ImageID", imageID) }, null, CommandType.StoredProcedure);
            if (result == null || result == DBNull.Value) return null;
            return (byte[])result;
        }

        //Thêm ảnh mới (lưu binary trực tiếp)
        public int ThemHinh(string imageName, byte[] imageData)
        {
            const string sql = "sp_InsertImage";
            var result = DatabaseHelper.ExecuteScalar(sql, new SqlParameter[]
            {
                new SqlParameter("@ImageName", imageName),
                new SqlParameter("@ImageData", imageData)     // byte[] → VARBINARY(MAX)
            }, null, CommandType.StoredProcedure);
            return Convert.ToInt32(result);
        }

        //Cập nhật binary của ảnh đã có
        public bool CapNhatDuLieuHinh(int imageID, byte[] imageData)
        {
            const string sql = "sp_UpdateImageData";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@ImageID", imageID),
                new SqlParameter("@ImageData", imageData)
            }, null, CommandType.StoredProcedure) > 0;
        }

        //Xóa ảnh
        public bool XoaHinh(int imageID)
        {
            const string sql = "sp_DeleteImage";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] { new SqlParameter("@ImageID", imageID) }, null, CommandType.StoredProcedure) > 0;
        }

        //Kiểm tra ảnh có đang dùng không (theo ImageID trên Words)
        public bool KiemTraHinhDangDung(int imageID)
        {
            const string sql = "sp_IsImageInUse";
            var result = DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@ImageID", imageID) }, null, CommandType.StoredProcedure);
            return Convert.ToInt32(result ?? 0) > 0;
        }

        //Kiểm tra tên ảnh đã tồn tại chưa
        public bool KiemTraTenHinhTonTai(string imageName)
        {
            const string sql = "sp_IsImageNameExists";
            var result = DatabaseHelper.ExecuteScalar(sql, new SqlParameter[] { new SqlParameter("@ImageName", imageName) }, null, CommandType.StoredProcedure);
            return Convert.ToInt32(result ?? 0) > 0;
        }
    }
}
