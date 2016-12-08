using Newtonsoft.Json;
using System;
using System.IO;

namespace Typing_Speed_Trainer.Statistics
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

        public Statistic(int cpm, int wpm, double errorRate, int score)
        {
            CharactersPerMinute = cpm;
            WordsPerMinute = wpm;
            ErrorRate = errorRate;
            Score = score;
        }

        public void Append(int cpm, int wpm, double errorRate, int score)
        {
            CharactersPerMinute = GetAverage(CharactersPerMinute, cpm);
            WordsPerMinute = GetAverage(WordsPerMinute, wpm);
            ErrorRate = GetAverage(ErrorRate, errorRate);
            Score = GetAverage(Score, score);
        }

        private static int GetAverage(int value1, int value2)
        {
            return (int)Math.Round((value1 + value2) / 2.0, MidpointRounding.AwayFromZero);
        }

        private static double GetAverage(double value1, double value2)
        {
            return (value1 + value2) / 2.0;
        }

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