using WordQuest.DTO;

namespace WordQuest.BUS
{
    public class AnswerManagerBUS
    {
        private AnswerStateDTO _state = new();

        public void Initialize(string answer)
        {
            _state = new AnswerStateDTO
            {
                CorrectAnswer = answer.Replace(" ", "").ToUpper(),
                FilledLetters = new List<string>()
            };

            GenerateLetters();
        }

        private void GenerateLetters()
        {
            var rnd = new Random();
            var letters = _state.CorrectAnswer
                .Select(c => c.ToString())
                .ToList();

            const string extra = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            while (letters.Count < 12)
                letters.Add(extra[rnd.Next(extra.Length)].ToString());

            _state.AvailableLetters = letters.OrderBy(x => rnd.Next()).ToList();
        }

        public List<string> LayKyTuCoSan() => _state.AvailableLetters;

        public List<string> LayKyTuDaDien() => _state.FilledLetters;

        public bool ThemKyTu(string letter)
        {
            if (_state.FilledLetters.Count >= _state.CorrectAnswer.Length)
                return false;

            _state.FilledLetters.Add(letter);
            return true;
        }

        public void XoaKyTuTuViTri(int index)
        {
            if (index < 0 || index >= _state.FilledLetters.Count) return;
            _state.FilledLetters.RemoveRange(index, _state.FilledLetters.Count - index);
        }

        public bool KiemTraDay()
        {
            return _state.FilledLetters.Count == _state.CorrectAnswer.Length;
        }

        public bool KiemTraDung()
        {
            return string.Join("", _state.FilledLetters) == _state.CorrectAnswer;
        }

        // Gợi ý auto điền chữ tiếp theo đúng, trả về chữ đó để GUI có thể disable tương ứng
        public string SuDungGoiY()
        {
            int nextIndex = _state.FilledLetters.Count;
            if (nextIndex >= _state.CorrectAnswer.Length) return null;

            string letter = _state.CorrectAnswer[nextIndex].ToString();
            _state.FilledLetters.Add(letter);

            // remove 1 chữ tương ứng khỏi available
            var btn = _state.AvailableLetters.FirstOrDefault(x => x == letter);
            if (btn != null)
                _state.AvailableLetters.Remove(btn);

            return letter;
        }

        public string LayDapAnDung() => _state.CorrectAnswer;
    }
}
