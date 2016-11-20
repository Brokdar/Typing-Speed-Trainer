using System;
using System.Diagnostics;

namespace Typing_Speed_Trainer.StatisicsData
{
    public class LessonResult : NotifiableDataBase
    {
        private Lesson _lesson;

        public Lesson Lesson
        {
            get { return _lesson; }
            set { SetProperty(ref _lesson, value); }
        }


        public int CharacterCount => _lesson.Count;
        public Difficulty Difficulty => _lesson.Difficulty;

        private string _writtenText;

        public string WrittenText
        {
            get { return _writtenText; }
            internal set { SetProperty(ref _writtenText, value); }
        }

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get { return _duration; }
            internal set { SetProperty(ref _duration, value); }
        }

        private int _errorCount;

        public int ErrorCount
        {
            get { return _errorCount; }
            internal set { SetProperty(ref _errorCount, value); }
        }

        public LessonResult(Lesson lesson, string written, TimeSpan duration)
        {
            Update(lesson, written, duration);
        }

        public void Update(Lesson lesson, string written, TimeSpan duration)
        {
            if (string.IsNullOrEmpty(lesson.Content) || string.IsNullOrEmpty(written) || duration.Seconds == 0)
                return;

            _lesson = lesson;
            WrittenText = written;
            Duration = duration;
            GetErrorCount();
        }

        private void GetErrorCount()
        {
            if (string.IsNullOrEmpty(_lesson.Content) || string.IsNullOrEmpty(WrittenText))
                return;

            var errorCount = WrittenText.Length - _lesson.Content.Length;

            Debug.Assert(errorCount >= 0, "errorCount negative");

            ErrorCount = errorCount;
        }
    }
}