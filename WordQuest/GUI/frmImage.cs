using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordQuest.BUS;

namespace WordQuest.GUI
{
    public partial class frmImage : Form
    {
        // Trả về MaAnh thay vì đường dẫn file
        public int SelectedImageID { get; private set; } = -1;

        private readonly ImageBUS _imageBUS = new();
        private static readonly HttpClient _httpClient = new();
        private static string PIXABAY_API_KEY => AppConfig.PixabayApiKey;

        public frmImage()
        {
            InitializeComponent();
        }

        private async void frmImage_Load(object sender, EventArgs e)
        {
            KhoiTaoControls();
            HienThiTab(1);
            await LoadLibrary();
        }

        private void KhoiTaoControls()
        {
            cboFilterImage.Items.Clear();
            cboFilterImage.Items.AddRange(new[] { "Tất cả", "Đang dùng", "Chưa dùng" });
            cboFilterImage.SelectedIndex = 0;

            điểmgListLarge.ImageSize = new Size(80, 80);
            điểmgListPixabay.ImageSize = new Size(80, 80);

            lvImages.LargeImageList = điểmgListLarge;
            lvPixResult.LargeImageList = điểmgListPixabay;

            try
            {
                var prop = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop?.SetValue(lvImages, true, null);
                prop?.SetValue(lvPixResult, true, null);
            }
            catch { }

            picPreview.Image = null;
        }

        private void HienThiTab(int tab)
        {
            pnlLibrary.Visible = (tab == 1);
            pnlPixabay.Visible = (tab == 2);
            pnlPixabay.Location = pnlLibrary.Location;
            pnlPixabay.Size = pnlLibrary.Size;

            btnTab1.BackColor = tab == 1 ? Color.FromArgb(37, 99, 176) : Color.FromArgb(220, 220, 230);
            btnTab2.BackColor = tab == 2 ? Color.FromArgb(37, 99, 176) : Color.FromArgb(220, 220, 230);
            btnTab1.ForeColor = tab == 1 ? Color.White : Color.FromArgb(80, 80, 80);
            btnTab2.ForeColor = tab == 2 ? Color.White : Color.FromArgb(80, 80, 80);
        }

        private async Task LoadLibrary(string tuKhoa = "", string filter = "Tất cả")
        {
            lvImages.BeginUpdate();
            lvImages.Items.Clear();
            điểmgListLarge.Images.Clear();

            var bangDuLieu = await Task.Run(() => _imageBUS.LayTatCaHinh(tuKhoa));

            int index = 0;
            foreach (DataRow dong in bangDuLieu.Rows)
            {
                bool isUsed = Convert.ToInt32(dong["IsUsed"]) == 1;
                int idHinh = Convert.ToInt32(dong["ImageID"]);
                string imageName = dong["ImageName"].ToString() ?? "";

                if (filter == "Đang dùng" && !isUsed) continue;
                if (filter == "Chưa dùng" && isUsed) continue;

                // Tải thumbnail binary từ DB (lazy load mỗi ảnh)
                Image điểmg = await Task.Run(() =>
                {
                    var data = _imageBUS.LayDuLieuHinh(idHinh);
                    return LoadHinhThuNho(data, 80, 80);
                });

                string key = idHinh.ToString();
                điểmgListLarge.Images.Add(key, điểmg);

                var item = new ListViewItem(imageName) { ImageKey = key, Tag = dong };
                item.ForeColor = isUsed ? Color.FromArgb(29, 78, 216) : Color.FromArgb(120, 120, 120);
                lvImages.Items.Add(item);

                index++;
                if (index % 20 == 0)
                    await Task.Delay(1);
            }

            lvImages.EndUpdate();
            lblStatusBar.Text = $"Tổng: {lvImages.Items.Count} ảnh";
        }

        /// <summary>
        /// Chuyển byte[] → Bitmap thu nhỏ (thumbnail) để hiển thị trong ListView.
        /// </summary>
        private Image LoadHinhThuNho(byte[] data, int width, int height)
        {
            if (data == null || data.Length == 0)
                return SystemIcons.Question.ToBitmap();
            try
            {
                using var ms = new MemoryStream(data);
                using var tmp = Image.FromStream(ms);
                var bmp = new Bitmap(width, height);
                using var g = Graphics.FromImage(bmp);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Transparent);
                g.DrawImage(tmp, 0, 0, width, height);
                return bmp;
            }
            catch { return SystemIcons.Question.ToBitmap(); }
        }

        private async void btnAddImage_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";
            if (dlg.ShowDialog() != DialogResult.OK) return;

            int newImageID = await Task.Run(() => _imageBUS.ThemHinhTuFile(dlg.FileName));
            SelectedImageID = newImageID;
            await LoadLibrary();
            MessageBox.Show("Thêm ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Highlight ảnh vừa thêm
            ChonHangDanhSachTheoID(newImageID);
        }

        private async void btnXoaHinh_Click(object sender, EventArgs e)
        {
            if (lvImages.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ảnh cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dong = (DataRow)lvImages.SelectedItems[0].Tag;
            bool isUsed = Convert.ToInt32(dong["IsUsed"]) == 1;
            int idHinh = Convert.ToInt32(dong["ImageID"]);

            if (isUsed)
            {
                MessageBox.Show("Ảnh này đang được dùng! Không thể xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Xóa ảnh này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            await Task.Run(() => _imageBUS.XoaHinh(idHinh));
            DatAnhXemTruoc(null);
            await LoadLibrary();
        }

        private async void btnSearchImage_Click(object sender, EventArgs e)
        {
            await LoadLibrary(txtSearchImage.Text.Trim(), cboFilterImage.SelectedItem?.ToString() ?? "Tất cả");
        }

        private async void cboFilterImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadLibrary(txtSearchImage.Text.Trim(), cboFilterImage.SelectedItem?.ToString() ?? "Tất cả");
        }

        private async void lvImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvImages.SelectedItems.Count == 0) return;
            var dong = (DataRow)lvImages.SelectedItems[0].Tag;
            int idHinh = Convert.ToInt32(dong["ImageID"]);
            bool isUsed = Convert.ToInt32(dong["IsUsed"]) == 1;
            string imageName = dong["ImageName"].ToString() ?? "";

            var data = await Task.Run(() => _imageBUS.LayDuLieuHinh(idHinh));
            var previewImg = ImageBUS.ChuyenByteSangAnh(data);
            DatAnhXemTruoc(previewImg);

            lblFileName.Text = $"Tên: {imageName}";
            lblFilePath.Text = $"ID: {idHinh}";
            lblFileStatus.Text = isUsed ? "Trạng thái: Đang dùng" : "Trạng thái: Chưa dùng";
        }

        private async void btnPixSearch_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtPixSearch.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                MessageBox.Show("Vui lòng nhập từ tiếng Anh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnPixSearch.Enabled = false;
            btnPixSearch.Text = "Đang tìm...";
            lvPixResult.Items.Clear();
            điểmgListPixabay.Images.Clear();

            try
            {
                string url = $"https://pixabay.com/api/?key={PIXABAY_API_KEY}&q={Uri.EscapeDataString(tuKhoa)}&image_type=illustration&per_page=15&safesearch=true";
                string json = await _httpClient.GetStringAsync(url);
                using var doc = JsonDocument.Parse(json);
                var hits = doc.RootElement.GetProperty("hits");

                int index = 0;
                foreach (var hit in hits.EnumerateArray())
                {
                    string previewUrl = hit.GetProperty("previewURL").GetString() ?? "";
                    string tags = hit.GetProperty("tags").GetString() ?? "";
                    int pixID = hit.GetProperty("id").GetInt32();

                    byte[] điểmgBytes = await _httpClient.GetByteArrayAsync(previewUrl);
                    using var ms = new MemoryStream(điểmgBytes);
                    using var tmp = Image.FromStream(ms);
                    var điểmg = new Bitmap(tmp);

                    string key = $"pix_{index}";
                    điểmgListPixabay.Images.Add(key, điểmg);

                    var item = new ListViewItem(tags.Length > 15 ? tags[..15] + "..." : tags)
                    {
                        ImageKey = key,
                        Tag = new PixabayResult { PixID = pixID, Tags = tags, PreviewURL = previewUrl, FullURL = hit.GetProperty("largeImageURL").GetString() ?? previewUrl }
                    };
                    lvPixResult.Items.Add(item);
                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPixSearch.Enabled = true;
                btnPixSearch.Text = "🔍 Tìm ảnh trên Pixabay";
            }
        }

        private void lvPixResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPixResult.SelectedItems.Count == 0) return;
            var ketQua = (PixabayResult)lvPixResult.SelectedItems[0].Tag;
            string key = lvPixResult.SelectedItems[0].ImageKey;
            if (điểmgListPixabay.Images.ContainsKey(key))
                DatAnhXemTruoc(new Bitmap(điểmgListPixabay.Images[key]));

            lblFileName.Text = $"Tags: {ketQua.Tags}";
            lblFilePath.Text = $"Nguồn: Pixabay #{ketQua.PixID}";
            lblFileStatus.Text = "Trạng thái: Chưa tải về";
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (lvPixResult.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ảnh cần tải!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ketQua = (PixabayResult)lvPixResult.SelectedItems[0].Tag;
            string fileName = $"pixabay_{ketQua.PixID}";

            btnDownload.Enabled = false;
            btnDownload.Text = "Đang tải...";

            try
            {
                // Tải byte[] từ Pixabay
                byte[] điểmgBytes = await _httpClient.GetByteArrayAsync(ketQua.FullURL);

                // Lưu trực tiếp vào DB (không cần file vật lý)
                int newImageID = await Task.Run(() => _imageBUS.ThemHinhTuByte(điểmgBytes, fileName));
                SelectedImageID = newImageID;

                string imageName = ketQua.Tags.Split(',')[0].Trim();
                MessageBox.Show($"Đã lưu ảnh '{imageName}' vào database!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                HienThiTab(1);
                await LoadLibrary();
                ChonHangDanhSachTheoID(newImageID);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnDownload.Enabled = true;
                btnDownload.Text = "Tải về & lưu vào database";
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            if (pnlLibrary.Visible)
            {
                if (lvImages.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ảnh trong thư viện!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var dong = (DataRow)lvImages.SelectedItems[0].Tag;
                SelectedImageID = Convert.ToInt32(dong["ImageID"]);
            }
            else // Tab Pixabay
            {
                if (SelectedImageID <= 0)
                {
                    MessageBox.Show("Vui lòng tải ảnh Pixabay về database trước!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ChonHangDanhSachTheoID(int idHinh)
        {
            foreach (ListViewItem it in lvImages.Items)
            {
                if (it.Tag is DataRow r && Convert.ToInt32(r["ImageID"]) == idHinh)
                {
                    it.Selected = true;
                    lvImages.EnsureVisible(it.Index);
                    break;
                }
            }
        }

        private void DatAnhXemTruoc(Image newImage)
        {
            var old = picPreview.Image;
            picPreview.Image = newImage;
            old?.Dispose();
        }

        private void btnTab1_Click(object sender, EventArgs e) => HienThiTab(1);
        private void btnTab2_Click(object sender, EventArgs e) => HienThiTab(2);

        private class PixabayResult
        {
            public int PixID { get; set; }
            public string Tags { get; set; } = "";
            public string PreviewURL { get; set; } = "";
            public string FullURL { get; set; } = "";
        }
    }
}