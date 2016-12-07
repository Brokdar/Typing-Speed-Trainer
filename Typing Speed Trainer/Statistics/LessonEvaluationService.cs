using System;

namespace Typing_Speed_Trainer.Statistics
{
    public class LessonEvaluationService
    {
        public static Statistic Evaluate(LessonResult result)
        {
            var cpm = CalculateCharactersPerMinute(result);
            var wpm = CalculateWordsPerMinute(result);
            var errorRate = CalculateErrorRate(result);
            var score = CalculateScore(result);

            return new Statistic(cpm, wpm, errorRate, score);
        }

        public static void AppendEvaluation(Statistic statistic, LessonResult result)
        {
            var cpm = CalculateCharactersPerMinute(result);
            var wpm = CalculateWordsPerMinute(result);
            var errorRate = CalculateErrorRate(result);
            var score = CalculateScore(result);

            statistic.Append(cpm, wpm, errorRate, score);
        }

        private static int CalculateCharactersPerMinute(LessonResult result)
        {
            return (int)Math.Round(result.CharacterCount * 60.0 / result.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private static int CalculateWordsPerMinute(LessonResult result)
        {
            var wordCount = result.CharacterCount / 5;
            return (int)Math.Round(wordCount * 60.0 / result.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private static double CalculateErrorRate(LessonResult result)
        {
            return Math.Round((double)result.ErrorCount / result.CharacterCount, 2, MidpointRounding.AwayFromZero);
        }

        private static int CalculateScore(LessonResult result)
        {
            var wordsPerMinute = CalculateWordsPerMinute(result);
            var score = result.CharacterCount * wordsPerMinute / (1.0 + result.ErrorCount * result.ErrorCount);

            return (int)Math.Round(score, 0, MidpointRounding.AwayFromZero);
        }
    }
}