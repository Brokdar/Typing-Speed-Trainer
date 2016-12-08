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
        public int CurrentCharacter => _executor.CurrentCharacterIndex;

        public int AvailableLessons { get; internal set; }

        public LessonResult LessonResult { get; internal set; }
        public Statistic LessonStatistic { get; internal set; }
        public Statistic SessionStatistic { get; internal set; }
        public Statistic OverallStatistic { get; internal set; }
        public Mode Mode { get; set; }

        public event EventHandler ResultsAvailable;

        public TypingSpeedTrainer()
        {
            _executor = new LessonExecutionService();
            _executor.LessonCompleted += OnLessonCompleted;

            SessionStatistic = null;
            OverallStatistic = null;
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

        public void Start(Lesson lesson)
        {
            _executor.Start(lesson);
        }

        public void OnKeystrokeDetected(char detectedCharacter)
        {
            _executor.OnKeystrokeDetected(detectedCharacter);
        }

        public void OnLessonCompleted(object sender, LessonResult result)
        {
            LessonResult = result;
            LessonStatistic = LessonEvaluationService.Evaluate(result);

            if (SessionStatistic == null)
                SessionStatistic = LessonStatistic;
            else
                LessonEvaluationService.AppendEvaluation(SessionStatistic, result);

            if (OverallStatistic == null)
                OverallStatistic = LessonStatistic;
            else
                LessonEvaluationService.AppendEvaluation(OverallStatistic, result);

            OnResultsAvailable();
        }

        protected virtual void OnResultsAvailable()
        {
            ResultsAvailable?.Invoke(this, EventArgs.Empty);
        }

        public bool IsLessonExecuting()
        {
            return _executor.LessonExecuting;
        }
    }
}