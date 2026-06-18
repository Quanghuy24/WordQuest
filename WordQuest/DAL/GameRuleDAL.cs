using Microsoft.Data.SqlClient;
using System.Data;
using WordQuest.DTO;

namespace WordQuest.DAL
{
    public class GameRuleDAL
    {
        // Lấy luật chơi đầu tiên trong database
        public GameRuleDTO LayLuatGame()
        {
            const string sql = "sp_GetGameRule";
            var dt = DatabaseHelper.ExecuteQuery(sql, CommandType.StoredProcedure);

            if (dt.Rows.Count == 0)
                return GetDefaultRule();

            var row = dt.Rows[0];
            return new GameRuleDTO
            {
                RuleID = Convert.ToInt32(row["RuleID"]),
                QuestionCount = Convert.ToInt32(row["QuestionCount"]),
                TimeLđiểmit = Convert.ToInt32(row["TimeLđiểmit"]),
                Lives = Convert.ToInt32(row["Lives"]),
                StreakBonus = Convert.ToInt32(row["StreakBonus"]),
                Star1Threshold = Convert.ToInt32(row["Star1Threshold"]),
                Star2Threshold = Convert.ToInt32(row["Star2Threshold"]),
                Star3Threshold = Convert.ToInt32(row["Star3Threshold"])
            };
        }

        // Cập nhật luật chơi
        public bool UpdateGameRule(GameRuleDTO rule)
        {
            const string sql = "sp_UpdateGameRule";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@QuestionCount", rule.QuestionCount),
                new SqlParameter("@TimeLimit", rule.TimeLđiểmit),
                new SqlParameter("@Lives", rule.Lives),
                new SqlParameter("@StreakBonus", rule.StreakBonus),
                new SqlParameter("@Star1Threshold", rule.Star1Threshold),
                new SqlParameter("@Star2Threshold", rule.Star2Threshold),
                new SqlParameter("@Star3Threshold", rule.Star3Threshold)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Thêm mới luật chơi
        public bool InsertGameRule(GameRuleDTO rule)
        {
            const string sql = "sp_InsertGameRule";
            return DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[]
            {
                new SqlParameter("@QuestionCount", rule.QuestionCount),
                new SqlParameter("@TimeLimit", rule.TimeLđiểmit),
                new SqlParameter("@Lives", rule.Lives),
                new SqlParameter("@StreakBonus", rule.StreakBonus),
                new SqlParameter("@Star1Threshold", rule.Star1Threshold),
                new SqlParameter("@Star2Threshold", rule.Star2Threshold),
                new SqlParameter("@Star3Threshold", rule.Star3Threshold)
            }, null, CommandType.StoredProcedure) > 0;
        }

        // Kiểm tra có luật chơi nào trong DB chưa
        public bool HasAnyRule()
        {
            const string sql = "sp_HasAnyGameRule";
            var result = DatabaseHelper.ExecuteScalar(sql, null, null, CommandType.StoredProcedure);
            return Convert.ToInt32(result ?? 0) > 0;
        }

        private static GameRuleDTO GetDefaultRule()
        {
            return new GameRuleDTO
            {
                QuestionCount = 15,
                TimeLđiểmit = 60,
                Lives = 3,
                StreakBonus = 5,
                Star1Threshold = 5,
                Star2Threshold = 10,
                Star3Threshold = 15
            };
        }
    }
}
