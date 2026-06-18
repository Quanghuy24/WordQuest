using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordQuest.DAL;

namespace WordQuest.BUS
{
    public class ImageBUS
    {
        private readonly ImageDAL _imageDAL = new();

        public DataTable LayTatCaHinh(string keyword = "") => _imageDAL.LayTatCaHinh(keyword);

        //Chuyển đổi: Image <=> byte[] <=> file
        /// Đọc file ảnh từ địa chỉ trả về byte[] để lưu vào DB.
        public static byte[] ChuyenFileAnhSangByte(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File ảnh không tồn tại: {filePath}");
            return File.ReadAllBytes(filePath);
        }

        /// Chuyển byte[] từ DB thành đối tượng Image để hiển thị PictureBox.
        /// Trả về null nếu dữ liệu rỗng hoặc lỗi.
        public static Image ChuyenByteSangAnh(byte[] data)
        {
            if (data == null || data.Length == 0) return null;
            try
            {
                using var ms = new MemoryStream(data);
                using var tmp = Image.FromStream(ms);
                return new Bitmap(tmp);
            }
            catch { return null; }
        }

        /// Chuyển đối tượng Image thành byte[] (PNG) để lưu DB.
        public static byte[] ChuyenAnhSangByte(Image ảnh)
        {
            using var ms = new MemoryStream();
            ảnh.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        // Thêm ảnh từ file (đọc file → byte[] → lưu DB)
        /// Đọc file ảnh từ đường dẫn, lưu binary vào DB.
        /// Trả về ImageID của bản ghi vừa tạo.
        public int ThemHinhTuFile(string sourceFilePath)
        {
            if (!File.Exists(sourceFilePath))
                throw new Exception("File ảnh không tồn tại!");

            string imageName = Path.GetFileNameWithoutExtension(sourceFilePath);

            // Tránh trùng tên: thêm số thứ tự nếu trùng
            string uniqueName = imageName;
            int suffix = 1;
            while (_imageDAL.KiemTraTenHinhTonTai(uniqueName))
                uniqueName = $"{imageName}_{suffix++}";

            byte[] imageData = ChuyenFileAnhSangByte(sourceFilePath);
            return _imageDAL.ThemHinh(uniqueName, imageData);
        }

        //Thêm ảnh từ byte[] (tải từ Pixabay, v.v.)
        /// Lưu byte[] ảnh vào DB với tên đề xuất.
        /// Trả về ImageID của bản ghi vừa tạo.
        public int ThemHinhTuByte(byte[] imageData, string suggestedName)
        {
            string imageName = Path.GetFileNameWithoutExtension(suggestedName);

            string uniqueName = imageName;
            int suffix = 1;
            while (_imageDAL.KiemTraTenHinhTonTai(uniqueName))
                uniqueName = $"{imageName}_{suffix++}";

            return _imageDAL.ThemHinh(uniqueName, imageData);
        }

        public byte[] LayDuLieuHinh(int imageID) => _imageDAL.LayDuLieuHinh(imageID);

        public Image LayHinh(int imageID)
        {
            var data = _imageDAL.LayDuLieuHinh(imageID);
            return ChuyenByteSangAnh(data);
        }

        //Lấy Image để hiển thị trực tiếp (Async)
        public async Task<Image> LayHinhAsync(int imageID)
        {
            var data = await _imageDAL.LayDuLieuHinhAsync(imageID);
            return ChuyenByteSangAnh(data);
        }

        //Xóa ảnh
        public bool XoaHinh(int imageID)
        {
            if (_imageDAL.KiemTraHinhDangDung(imageID))
                throw new Exception("Ảnh đang được dùng bởi một từ vựng! Không thể xóa.");

            return _imageDAL.XoaHinh(imageID);
        }
    }
}
