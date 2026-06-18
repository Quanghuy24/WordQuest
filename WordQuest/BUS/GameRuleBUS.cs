using System;
using WordQuest.DAL;
using WordQuest.DTO;

namespace WordQuest.BUS
{
    public class GameRuleBUS
    {
        private readonly GameRuleDAL _gameRuleDAL = new();

        public GameRuleDTO LayLuatGame() => _gameRuleDAL.LayLuatGame();

        public bool LuuLuatGame(GameRuleDTO rule)
        {
            if (rule.QuestionCount < 5 || rule.QuestionCount > 30)
                throw new Exception("Số câu hỏi phải từ 5 đến 30!");
            if (rule.TimeLđiểmit < 10 || rule.TimeLđiểmit > 120)
                throw new Exception("Thời gian mỗi câu phải từ 10 đến 120 giây!");
            if (rule.Lives < 1 || rule.Lives > 10)
                throw new Exception("Số mạng phải từ 1 đến 10!");
            if (rule.Star1Threshold >= rule.Star2Threshold)
                throw new Exception("Ngưỡng sao 1 phải nhỏ hơn ngưỡng sao 2!");
            if (rule.Star2Threshold >= rule.Star3Threshold)
                throw new Exception("Ngưỡng sao 2 phải nhỏ hơn ngưỡng sao 3!");

            if (_gameRuleDAL.HasAnyRule())
                return _gameRuleDAL.UpdateGameRule(rule);
            else
                return _gameRuleDAL.InsertGameRule(rule);
        }

        public void DatLaiMacDinh()
        {
            var defaultRule = new GameRuleDTO
            {
                QuestionCount = 15,
                TimeLđiểmit = 60,
                Lives = 3,
                StreakBonus = 5,
                Star1Threshold = 5,
                Star2Threshold = 10,
                Star3Threshold = 15
            };

            if (_gameRuleDAL.HasAnyRule())
                _gameRuleDAL.UpdateGameRule(defaultRule);
            else
                _gameRuleDAL.InsertGameRule(defaultRule);
        }
    }
}
