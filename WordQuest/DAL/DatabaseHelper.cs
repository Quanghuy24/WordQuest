using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using NLog;

namespace WordQuest.DAL
{
    public static class DatabaseHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Connection string đọc từ appsettings.json (qua AppConfig).
        /// Fallback tự động về localhost nếu file config không tồn tại.
        /// </summary>
        private static string ConnectionString => AppConfig.ConnectionString;

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();
                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "TestConnection failed.");
                errorMessage = ex.Message;
                return false;
            }
        }

        public static bool TestConnection()
        {
            return TestConnection(out _);
        }

        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null, SqlTransaction transaction = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                if (transaction != null)
                {
                    using var cmd = new SqlCommand(sql, transaction.Connection, transaction);
                    cmd.CommandType = commandType;
                    AddParameters(cmd, parameters);
                    return cmd.ExecuteNonQuery();
                }

                using var conn = GetConnection();
                conn.Open();
                using var cmd2 = new SqlCommand(sql, conn);
                cmd2.CommandType = commandType;
                AddParameters(cmd2, parameters);
                return cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ExecuteNonQuery failed. SQL: {sql}");
                throw;
            }
        }

        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null, SqlTransaction transaction = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                if (transaction != null)
                {
                    using var cmd = new SqlCommand(sql, transaction.Connection, transaction);
                    cmd.CommandType = commandType;
                    AddParameters(cmd, parameters);
                    return cmd.ExecuteScalar();
                }

                using var conn = GetConnection();
                conn.Open();
                using var cmd2 = new SqlCommand(sql, conn);
                cmd2.CommandType = commandType;
                AddParameters(cmd2, parameters);
                return cmd2.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ExecuteScalar failed. SQL: {sql}");
                throw;
            }
        }

        public static async Task<object> ExecuteScalarAsync(string sql, SqlParameter[] parameters = null, SqlTransaction transaction = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                if (transaction != null)
                {
                    using var cmd = new SqlCommand(sql, transaction.Connection, transaction);
                    cmd.CommandType = commandType;
                    AddParameters(cmd, parameters);
                    return await cmd.ExecuteScalarAsync();
                }

                using var conn = GetConnection();
                await conn.OpenAsync();
                using var cmd2 = new SqlCommand(sql, conn);
                cmd2.CommandType = commandType;
                AddParameters(cmd2, parameters);
                return await cmd2.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ExecuteScalarAsync failed. SQL: {sql}");
                throw;
            }
        }

        public static DataTable ExecuteQuery(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();
                using var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = commandType;
                AddParameters(cmd, parameters);
                using var adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ExecuteQuery failed. SQL: {sql}");
                throw;
            }
        }

        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            return ExecuteQuery(sql, CommandType.Text, parameters);
        }

        public static void ExecuteInTransaction(Action<SqlTransaction> action)
        {
            using var conn = GetConnection();
            conn.Open();
            using var transaction = conn.BeginTransaction();
            try
            {
                action(transaction);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private static void AddParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);
        }
    }
}//szgrgẻgẻgẻg