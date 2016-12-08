using System;
using System.Diagnostics;

namespace Typing_Speed_Trainer.Statistics
{
    public class LessonResult : EventArgs
    {
        public Lesson Lesson { get; internal set; }
        public string WrittenText { get; internal set; }
        public TimeSpan Duration { get; internal set; }
        public int ErrorCount { get; internal set; }

        public LessonResult(Lesson lesson, string written, TimeSpan duration)
        {
            if (string.IsNullOrEmpty(lesson.Content) || string.IsNullOrEmpty(written) || duration.TotalSeconds < 0.0001)
                throw new ArgumentException("LessonResult: Invalid arguments");

            Lesson = lesson;
            WrittenText = written;
            Duration = duration;
            ErrorCount = GetErrorCount();
        }

        private int GetErrorCount()
        {
            var errorCount = WrittenText.Length - Lesson.Content.Length;

            Debug.Assert(errorCount >= 0, "errorCount negative");

            return errorCount;
        }
    }
}