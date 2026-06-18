using System;
using System.Threading;
using System.Windows.Forms;
using NLog;
using WordQuest.DAL;
using WordQuest.GUI;

namespace WordQuest
{
    internal static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Setup global exception handling
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Logger.Info("==================================================");
            Logger.Info($"WordQuest App Started. Version: {AppConfig.AppVersion}");

            // kiểm tra file cấu hình appsettings.json tồn tại hay không
            if (!AppConfig.ConfigFileExists)
            {
                Logger.Warn("appsettings.json not found! Using defaults.");
                MessageBox.Show(
                    "⚠️ Không tìm thấy file appsettings.json bên cạnh WordQuest.exe!\n\n" +
                    "App sẽ dùng cấu hình mặc định (localhost).\n" +
                    "Hãy tạo file appsettings.json để cấu hình kết nối database.",
                    "Cảnh báo cấu hình",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            // Kiểm tra kết nối DB trước khi chạy
            if (!DatabaseHelper.TestConnection(out string dbError))
            {
                Logger.Error($"Database connection failed on startup: {dbError}");
                var result = MessageBox.Show(
                    $"❌ Không thể kết nối đến database!\n\n" +
                    $"Lỗi: {dbError}\n\n" +
                    $"Connection string hiện tại:\n{AppConfig.ConnectionString}\n\n" +
                    "Bạn có muốn tiếp tục không? (App có thể bị lỗi)",
                    "Lỗi kết nối Database",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (result == DialogResult.No)
                {
                    Logger.Info("User chose to exit app due to DB connection failure.");
                    return;
                }
            }

            Application.Run(new frmLogin());
            Logger.Info("WordQuest App Closed.");
            LogManager.Shutdown();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Fatal(e.Exception, "Unhandled ThreadException occurred.");
            MessageBox.Show($"Đã xảy ra lỗi nghiêm trọng:\n{e.Exception.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal(e.ExceptionObject as Exception, "Unhandled AppDomain Exception occurred.");
        }
    }
}
