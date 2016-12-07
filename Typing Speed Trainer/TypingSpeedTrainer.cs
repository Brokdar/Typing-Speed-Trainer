using System;
using Typing_Speed_Trainer.LessonExecution;
using Typing_Speed_Trainer.LessonFactories;
using Typing_Speed_Trainer.Statistics;

namespace Typing_Speed_Trainer
{
    public enum LessonProvider
    {
        Dummy,
        Web,
        File
    }

    public enum LessonFormat
    {
        Practice,
        Programmer
    }

    public enum Mode
    {
        PractingMode,
        SpeedingMode
    }

    public class TypingSpeedTrainer
    {
        private LessonFactory _factory;
        private readonly LessonExecutionService _executor;

        public int AvailableLessons { get; internal set; }

        public Statistic LessonStatistic { get; internal set; }
        public Statistic SessionStatistic { get; internal set; }
        public Statistic OverallStatistic { get; internal set; }
        public Mode Mode { get; set; }

        public TypingSpeedTrainer()
        {
            _executor = new LessonExecutionService();
        }


        public void CreateNewLessons(Uri source, LessonProvider provider, LessonFormat format = LessonFormat.Practice)
        {
            switch (provider)
            {
                case LessonProvider.Dummy:
                    _factory = new DummyLessonFactory();
                    break;
                case LessonProvider.Web:
                    throw new NotImplementedException();
                case LessonProvider.File:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("Invalid Provider for LessonFactory");
            }

            AvailableLessons = _factory.CreateLessons(source);
        }

        public Lesson NextLesson()
        {
            var nextLesson = _factory.NextLesson();
            AvailableLessons--;
            return nextLesson;
        }

        public void Execute(Lesson lesson)
        {
            var result = _executor.Execute(lesson);
            LessonStatistic = LessonEvaluationService.Evaluate(result);
            LessonEvaluationService.AppendEvaluation(SessionStatistic, result);
            LessonEvaluationService.AppendEvaluation(OverallStatistic, result);
        }
    }
}