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
            return (int)Math.Round(result.Lesson.Count * 60.0 / result.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private static int CalculateWordsPerMinute(LessonResult result)
        {
            var wordCount = result.Lesson.Count / 5;
            return (int)Math.Round(wordCount * 60.0 / result.Duration.TotalSeconds, MidpointRounding.AwayFromZero);
        }

        private static double CalculateErrorRate(LessonResult result)
        {
            return (double)result.ErrorCount / result.Lesson.Count * 100.0;
        }

        private static int CalculateScore(LessonResult result)
        {
            var wordsPerMinute = CalculateWordsPerMinute(result);
            var score = result.Lesson.Count * wordsPerMinute / (1.0 + result.ErrorCount * result.ErrorCount);

            return (int)Math.Round(score, 0, MidpointRounding.AwayFromZero);
        }
    }
}