using Newtonsoft.Json;
using System;
using System.IO;

namespace Typing_Speed_Trainer.StatisicsData
{
    public class Statistic : NotifiableDataBase, IEquatable<Statistic>
    {
        private int _wordsPerMinute;

        [JsonProperty]
        public int WordsPerMinute
        {
            get { return _wordsPerMinute; }
            internal set { SetProperty(ref _wordsPerMinute, value); }
        }

        private int _charactersPerMinute;

        [JsonProperty]
        public int CharactersPerMinute
        {
            get { return _charactersPerMinute; }
            internal set { SetProperty(ref _charactersPerMinute, value); }
        }

        private double _errorRate;

        [JsonProperty]
        public double ErrorRate
        {
            get { return _errorRate; }
            internal set { SetProperty(ref _errorRate, value); }
        }

        private int _score;

        [JsonProperty]
        public int Score
        {
            get { return _score; }
            internal set { SetProperty(ref _score, value); }
        }


        public Statistic()
        {

        }

        public Statistic(LessonResult result)
        {
            Evaluate(result);
        }

        public void Update(LessonResult result)
        {
            Evaluate(result);
        }

        #region Evaluation of Lesson

        public void Evaluate(LessonResult result)
        {
            if (result == null)
            {
                return;
            }

            CharactersPerMinute = CalculateCharactersPerMinute(result);
            WordsPerMinute = CalculateWordsPerMinute(result);
            ErrorRate = CalculateErrorRate(result);
            Score = CalculateScore(result);
        }

        private int CalculateScore(LessonResult result)
        {
            var wordsPerMinute = CalculateWordsPerMinute(result);
            var score = result.CharacterCount * wordsPerMinute / (1.0 + result.ErrorCount * result.ErrorCount);

            return (int)Math.Round(score, 0, MidpointRounding.AwayFromZero);
        }

        public void AppendEvaluation(LessonResult result)
        {
            CharactersPerMinute = GetAverage(CharactersPerMinute, CalculateCharactersPerMinute(result));
            WordsPerMinute = GetAverage(WordsPerMinute, CalculateWordsPerMinute(result));
            ErrorRate = GetAverage(ErrorRate, CalculateErrorRate(result));
            Score = GetAverage(Score, CalculateScore(result));
        }

        private int CalculateCharactersPerMinute(LessonResult result)
        {
            if (string.IsNullOrEmpty(result.Lesson.Content) || (int)result.Duration.TotalSeconds == 0)
                return 0;

            return (int)Math.Round((result.CharacterCount * 60.0) / (int)result.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private int CalculateWordsPerMinute(LessonResult result)
        {
            if (string.IsNullOrEmpty(result.Lesson.Content) || (int)result.Duration.TotalSeconds == 0)
                return 0;

            var wordCount = result.CharacterCount / 5.0;
            return (int)Math.Round((wordCount * 60.0) / (int)result.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private double CalculateErrorRate(LessonResult result)
        {
            if (string.IsNullOrEmpty(result.Lesson.Content) || string.IsNullOrEmpty(result.WrittenText))
                return 0.0;

            return Math.Round((double)result.ErrorCount / result.CharacterCount, 2, MidpointRounding.AwayFromZero);
        }

        private int GetAverage(int value1, int value2)
        {
            return (int)Math.Round((value1 + value2) / 2.0, MidpointRounding.AwayFromZero);
        }

        private double GetAverage(double value1, double value2)
        {
            return Math.Round((value1 + value2) / 2.0, 2, MidpointRounding.AwayFromZero);
        }

        #endregion

        #region Serialization

        public static void SaveAsJson(Statistic statistic, string filename)
        {
            var json = JsonConvert.SerializeObject(statistic, Formatting.Indented);
            File.WriteAllText(filename, json);
        }

        public static Statistic LoadFromJson(string filename)
        {
            var json = File.ReadAllText(filename);
            return JsonConvert.DeserializeObject<Statistic>(json);
        }

        #endregion

        #region IEquatable

        public bool Equals(Statistic other)
        {
            if (other == null)
                return false;

            return WordsPerMinute == other.WordsPerMinute && CharactersPerMinute == other.CharactersPerMinute &&
                   Score == other.Score && Math.Abs(ErrorRate - other.ErrorRate) < 0.01;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Statistic);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _wordsPerMinute;
                hashCode = (hashCode * 397) ^ _charactersPerMinute;
                hashCode = (hashCode * 397) ^ _errorRate.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}