using System;
using System.Linq;

namespace Typing_Speed_Trainer
{
    public enum Difficulty
    {
        Invalid = 0,
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard
    }

    public class Lesson
    {
        public string Content { get; internal set; }

        public Uri Source { get; internal set; }

        public Difficulty Difficulty { get; internal set; }

        public int Count { get; internal set; }

        public Lesson(string content, Uri source)
        {
            Content = content;
            Source = source;
            Count = content.Length;
            Difficulty = GetDifficulty(content);
        }

        private Difficulty GetDifficulty(string content)
        {
            var difficultyScore = 0;

            if (content.Any(char.IsLower))
            {
                difficultyScore += 1;
            }
            if (content.Any(char.IsUpper))
            {
                difficultyScore += 2;
            }
            if (content.Any(char.IsDigit))
            {
                difficultyScore += 4;
            }
            if (content.Any(char.IsPunctuation) | content.Any(char.IsSymbol))
            {
                difficultyScore += 8;
            }

            return ConvertDifficultScore(difficultyScore);
        }

        private Difficulty ConvertDifficultScore(int difficultyScore)
        {
            if (difficultyScore == 1)
            {
                return Difficulty.VeryEasy;
            }
            else if (difficultyScore > 1 && difficultyScore <= 3)
            {
                return Difficulty.Easy;
            }
            else if (difficultyScore > 3 && difficultyScore <= 6)
            {
                return Difficulty.Medium;
            }
            else if (difficultyScore > 6 && difficultyScore <= 12)
            {
                return Difficulty.Hard;
            }
            else if (difficultyScore > 12)
            {
                return Difficulty.VeryHard;
            }
            else
            {
                return Difficulty.Invalid;
            }
        }
    }
}
