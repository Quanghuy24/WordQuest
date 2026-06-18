using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Fixer {
    public static void Main() {
        var map = new Dictionary<string, string> {
            {"", "đ"}, {"?", "Đ"}, {"?", "Đ"}, {"+", "ế"}, {"A", "á"}, {"A", "à"}, {"A", "ả"}, {"A", "ã"}, {"A", "ạ"},
            {"A", "ă"}, {"A", "ắ"}, {"A", "ằ"}, {"A", "ẳ"}, {"A", "ẵ"}, {"A", "ặ"},
            {"A", "â"}, {"A", "ấ"}, {"A", "ầ"}, {"A", "ẩ"}, {"A", "ẫ"}, {"A-", "ậ"},
            {"A", "é"}, {"A", "è"}, {"A", "ẻ"}, {"A", "ẽ"}, {"A", "ẹ"},
            {"A", "ê"}, {"A", "ế"}, {"A", "ề"}, {"A", "ể"}, {"A", "ễ"}, {"A", "ệ"},
            {"A-", "í"}, {"A", "ì"}, {"A", "ỉ"}, {"A", "ĩ"}, {"A", "ị"},
            {"A3", "ó"}, {"A", "ò"}, {"A", "ỏ"}, {"A", "õ"}, {"A", "ọ"},
            {"A", "ô"}, {"A", "ố"}, {"A", "ồ"}, {"A", "ổ"}, {"A", "ỗ"}, {"A", "ộ"},
            {"A", "ơ"}, {"A", "ớ"}, {"A", "ờ"}, {"A", "ở"}, {"A", "ỡ"}, {"A", "ợ"},
            {"A1", "ú"}, {"A", "ù"}, {"A", "ủ"}, {"A", "ũ"}, {"A", "ụ"},
            {"A", "ư"}, {"A", "ứ"}, {"A", "ừ"}, {"A", "ử"}, {"A", "ữ"}, {"A", "ự"},
            {"A", "ý"}, {"A", "ỳ"}, {"A", "ỷ"}, {"A", "ỹ"}, {"A", "ỵ"}
        };
        
        var files = Directory.GetFiles(".", "*.cs", SearchOption.AllDirectories);
        foreach(var f in files) {
            if(f.Contains("\\obj\\") || f.Contains("\\bin\\")) continue;
            var text = File.ReadAllText(f);
            
            // Hardcoded full string replacements for safety based on the previous grep output
            text = text.Replace("Xóa chủ đề", "Xóa chủ đề");
            text = text.Replace("Tt c d_ liu", "Tất cả dữ liệu");
            text = text.Replace("liên quan", "liên quan");
            text = text.Replace("sẽ bị xóa", "sẽ bị xóa");
            text = text.Replace("THA\"NG TIN CH ??", "THÔNG TIN CHỦ ĐỀ");
            text = text.Replace("Lỗi xóa chủ đề", "Lỗi xóa chủ đề");
            text = text.Replace("Vui lòng", "Vui lòng");
            text = text.Replace("nhập tên chủ đề", "nhập tên chủ đề");
            text = text.Replace("Thiếu thông tin", "Thiếu thông tin");
            text = text.Replace("Đọc ParentID từ ComboBox", "Đọc ParentID từ ComboBox");
            text = text.Replace("không thể làm con", "không thể làm con");
            text = text.Replace("của chính nó", "của chính nó");
            text = text.Replace("THÊM MỚI", "THÊM MỚI");
            text = text.Replace("Lưu chủ đề thành công", "Lưu chủ đề thành công");
            text = text.Replace("Cập nhật chủ đề thành công", "Cập nhật chủ đề thành công");
            text = text.Replace("Lỗi lưu", "Lỗi lưu");
            text = text.Replace("Chọn node trên TreeView", "Chọn node trên TreeView");
            text = text.Replace("Duyệt đệ quy", "Duyệt đệ quy");
            text = text.Replace("toàn bộ", "toàn bộ");
            text = text.Replace("tìm và select", "tìm và select");
            text = text.Replace("Dễ", "Dễ");
            text = text.Replace("Trả về MaAnh", "Trả về MaAnh");
            text = text.Replace("đường dẫn file", "đường dẫn file");
            text = text.Replace("Nạp danh sách ảnh từ DB", "Nạp danh sách ảnh từ DB");
            text = text.Replace("khA'ng kA\"m binary", "không kèm binary");
            text = text.Replace("để tránh chậm", "để tránh chậm");
            text = text.Replace("Tải thumbnail binary", "Tải thumbnail binary");
            text = text.Replace("mỗi ảnh", "mỗi ảnh");
            text = text.Replace("giữ UI", "giữ UI");
            text = text.Replace("Tổng:", "Tổng:");
            text = text.Replace("Chuyển byte[] thành Bitmap thu nhỏ", "Chuyển byte[] thành Bitmap thu nhỏ");
            text = text.Replace("hiển thị", "hiển thị");
            text = text.Replace("Thêm ảnh từ file", "Thêm ảnh từ file");
            text = text.Replace("ảnh vừa thêm", "ảnh vừa thêm");
            text = text.Replace("chọn ảnh cần xóa", "chọn ảnh cần xóa");
            text = text.Replace("Thông báo", "Thông báo");
            text = text.Replace("Ảnh này đang được dùng! Không thể xóa.", "Ảnh này đang được dùng! Không thể xóa.");
            text = text.Replace("TAm kđiểm / L?c", "Tìm kiếm / Lọc");
            text = text.Replace("Chọn ảnh trong ListView", "Chọn ảnh trong ListView");
            text = text.Replace("từ tiếng Anh", "từ tiếng Anh");
            text = text.Replace("Lỗi tìm ảnh", "Lỗi tìm ảnh");
            text = text.Replace("Ngu\"n:", "Nguồn:");
            text = text.Replace("Trạng thái: Chưa tải về", "Trạng thái: Chưa tải về");
            text = text.Replace("chọn ảnh cần tải", "chọn ảnh cần tải");
            text = text.Replace("Tải byte[] từ Pixabay", "Tải byte[] từ Pixabay");
            text = text.Replace("Lưu trực tiếp vào DB", "Lưu trực tiếp vào DB");
            text = text.Replace("không cần file vật lý", "không cần file vật lý");
            text = text.Replace("Lỗi tải ảnh", "Lỗi tải ảnh");
            text = text.Replace("Tải về & lưu vào database", "Tải về & lưu vào database");
            text = text.Replace("Chọn ảnh và đóng form", "Chọn ảnh và đóng form");
            text = text.Replace("chọn ảnh trong thư viện", "chọn ảnh trong thư viện");
            text = text.Replace("tải ảnh Pixabay về database trước", "tải ảnh Pixabay về database trước");
            text = text.Replace("Thêm ảnh từ máy", "Thêm ảnh từ máy");
            text = text.Replace("dY- XA3a nh A ch\u008dn", "Xóa ảnh đã chọn");
            text = text.Replace("điểm", "điểm");
            text = text.Replace("Lỗi tải bảng xếp hạng", "Lỗi tải bảng xếp hạng");
            text = text.Replace("Chọn màn chơi", "Chọn màn chơi");
            text = text.Replace("Lỗi tải màn chơi", "Lỗi tải màn chơi");
            text = text.Replace("Lấy sao và trạng thái hoàn thành", "Lấy sao và trạng thái hoàn thành");
            text = text.Replace("Kđiểm tra mY khA3a: mAn 1 luA'n mY, mAn sau c n hoAn thAnh mAn tr>c", "Kiểm tra mở khóa: màn 1 luôn mở, màn sau cần hoàn thành màn trước");
            text = text.Replace("Cập nhật UI", "Cập nhật UI");
            text = text.Replace("HoAn thAnh Màn", "Hoàn thành Màn");
            text = text.Replace("Lưu levelNum vào tag của nút để StartGame biết", "Lưu levelNum vào tag của nút để StartGame biết");
            text = text.Replace("Vui lòng nhập tên của bạn!", "Vui lòng nhập tên của bạn!");
            text = text.Replace("Tên phải có ít nhất 2 ký tự!", "Tên phải có ít nhất 2 ký tự!");
            text = text.Replace("Streak của bạn đã bị reset", "Streak của bạn đã bị reset");
            text = text.Replace("bỏ lỡ hôm qua", "bỏ lỡ hôm qua");
            text = text.Replace("Hãy quay lại mỗi ngày", "Hãy quay lại mỗi ngày");
            text = text.Replace("để giữ streak", "để giữ streak");
            text = text.Replace("dY\" TUY+T VoI!", "TUYỆT VỜI!");
            text = text.Replace("ngày liên tiếp!", "ngày liên tiếp!");
            text = text.Replace("đang trên đà rất tốt, tiếp tục nhé!", "đang trên đà rất tốt, tiếp tục nhé!");
            text = text.Replace("Chào mừng trở lại", "Chào mừng trở lại");
            text = text.Replace("Mở màn hình chính", "Mở màn hình chính");
            text = text.Replace("Không thể tạo/tìm người chơi!", "Không thể tạo/tìm người chơi!");
            text = text.Replace("Xác thực Admin", "Xác thực Admin");
            text = text.Replace("Chọn chủ đề", "Chọn chủ đề");
            text = text.Replace("Hiển thị streak trên label", "Hiển thị streak trên label");
            text = text.Replace("thêm ngọn lửa nếu có streak", "thêm ngọn lửa nếu có streak");
            text = text.Replace("Lỗi tải chủ đề", "Lỗi tải chủ đề");
            text = text.Replace("Cần", "Cần");
            text = text.Replace("để mở chủ đề này!", "để mở chủ đề này!");
            text = text.Replace("Bạn có:", "Bạn có:");
            text = text.Replace("Chưa mở khóa", "Chưa mở khóa");
            text = text.Replace("Gán sự kiện", "Gán sự kiện");
            text = text.Replace("Tốt l_m!", "Tốt lắm!");
            text = text.Replace("Cố gắng hơn!", "Cố gắng hơn!");
            text = text.Replace("Thử lại nào!", "Thử lại nào!");
            text = text.Replace("Tốt", "Tốt");
            text = text.Replace("Cần c g_ng", "Cần cố gắng");
            text = text.Replace("Chủ đề:", "Chủ đề:");
            text = text.Replace("Màn", "Màn");
            text = text.Replace("Số câu đúng:", "Số câu đúng:");
            text = text.Replace("?điểm s:", "Điểm số:");
            text = text.Replace("\\u0090điểm s:", "Điểm số:");
            text = text.Replace("ID chủ đề không hợp lệ!", "ID chủ đề không hợp lệ!");
            text = text.Replace("Tên màn không được để trống!", "Tên màn không được để trống!");
            text = text.Replace("Độ khó không được để trống!", "Độ khó không được để trống!");
            text = text.Replace("Số câu hỏi phải từ 1 đến 50!", "Số câu hỏi phải từ 1 đến 50!");
            text = text.Replace("ID màn không hợp lệ!", "ID màn không hợp lệ!");
            text = text.Replace("Xóa tất cả từ liên kết trước", "Xóa tất cả từ liên kết trước");
            
            // Re-check and replace generic tokens if they are still remaining (very cautious!)
            // We just use the known phrases to be super safe and avoid breaking code strings.
            
            File.WriteAllText(f, text);
        }
        Console.WriteLine("Done replacing phrases in .cs files.");
    }
}

