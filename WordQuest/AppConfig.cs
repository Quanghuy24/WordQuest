using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace WordQuest
{
    /// <summary>
    /// Helper tĩnh đọc appsettings.json một lần khi app khởi động.
    /// Nếu file không tồn tại hoặc thiếu key â†’ dùng giá trị mặc định (fallback)
    /// để app không crash khi deploy sang máy khác.
    /// </summary>
    public static class AppConfig
    {
        private static readonly IConfiguration _config;

        // Giá trị mặc định nếu file config không tồn tại hoặc thiếu key
        private const string DefaultConnectionString =
            "Server=localhost;Database=WordQuestDB;Trusted_Connection=True;TrustServerCertificate=True;";

        private const string DefaultPixabayKey = "";

        static AppConfig()
        {
            try
            {
                // Tìm appsettings.json cạnh file .exe (Application.StartupPath)
                string basePath = AppContext.BaseDirectory;

                _config = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                    .Build();
            }
            catch
            {
                // Nếu có lỗi parse - dùng config rỗng, sẽ fallback về default
                _config = new ConfigurationBuilder().Build();
            }
        }

        // Connection String
        /// <summary>
        /// Trả về connection string đọc từ appsettings.json.
        /// Fallback về localhost nếu file không tồn tại hoặc thiếu key.
        /// </summary>
        public static string ConnectionString =>
            _config.GetConnectionString("WordQuestDB")
            ?? DefaultConnectionString;

        // Pixabay API Key
        /// <summary>
        /// Trả về Pixabay API key đọc từ appsettings.json.
        /// Trả về chuỗi rỗng nếu chưa cấu hình.
        /// </summary>
        public static string PixabayApiKey =>
            _config["Pixabay:ApiKey"] ?? DefaultPixabayKey;

        // Kiểm tra file config có tồn tại không
        /// <summary>
        /// Trả về true nếu file appsettings.json tồn tại bên cạnh .exe.
        /// Dùng để cảnh báo người dùng khi chạy lần đầu.
        /// </summary>
        public static bool ConfigFileExists =>
            File.Exists(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

        // Kiểm tra App version & Admin Auth
        public static string AppVersion =>
            _config["App:Version"] ?? "1.0.0";

        /// <summary>
        /// Mật khẩu đăng nhập quyền Admin, mặc định là "123" nếu không tìm thấy.
        /// </summary>
        public static string AdminPassword =>
            _config["App:AdminPassword"] ?? "123";
    }
}

