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


        public Statistic()
        {

        }

        public Statistic(Lesson lesson)
        {
            Evaluate(lesson);
        }

        public void Update(Lesson lesson)
        {
            Evaluate(lesson);
        }

        #region Evaluation of Lesson

        public void Evaluate(Lesson lesson)
        {
            CharactersPerMinute = CalculateCharactersPerMinute(lesson);
            WordsPerMinute = CalculateWordsPerMinute(lesson);
            ErrorRate = CalculateErrorRate(lesson);
        }

        public void AppendEvaluation(Lesson lesson)
        {
            CharactersPerMinute = GetAverage(CharactersPerMinute, CalculateCharactersPerMinute(lesson));
            WordsPerMinute = GetAverage(WordsPerMinute, CalculateWordsPerMinute(lesson));
            ErrorRate = GetAverage(ErrorRate, CalculateErrorRate(lesson));
        }

        private int CalculateCharactersPerMinute(Lesson lesson)
        {
            if (string.IsNullOrEmpty(lesson.RequestedText) || (int)lesson.Duration.TotalSeconds == 0)
                return 0;

            return (int)Math.Round((lesson.CharacterCount * 60.0) / (int)lesson.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private int CalculateWordsPerMinute(Lesson lesson)
        {
            if (string.IsNullOrEmpty(lesson.RequestedText) || (int)lesson.Duration.TotalSeconds == 0)
                return 0;

            var wordCount = lesson.CharacterCount / 5.0;
            return (int)Math.Round((wordCount * 60.0) / (int)lesson.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private double CalculateErrorRate(Lesson lesson)
        {
            if (string.IsNullOrEmpty(lesson.RequestedText) || string.IsNullOrEmpty(lesson.WrittenText))
                return 0.0;

            return Math.Round((double)lesson.ErrorCount / lesson.CharacterCount, 2, MidpointRounding.AwayFromZero);
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
                   Math.Abs(ErrorRate - other.ErrorRate) < 0.01;
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