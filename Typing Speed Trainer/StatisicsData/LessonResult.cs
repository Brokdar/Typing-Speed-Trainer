using System;
using System.Diagnostics;

namespace Typing_Speed_Trainer.StatisicsData
{
    public class LessonResult : NotifiableDataBase
    {
        private string _requestedText;

        public string RequestedText
        {
            get { return _requestedText; }
            internal set { SetProperty(ref _requestedText, value); }
        }

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

        private int _characterCount;

        public int CharacterCount
        {
            get { return _characterCount; }
            internal set { SetProperty(ref _characterCount, value); }
        }

        private int _errorCount;

        public int ErrorCount
        {
            get { return _errorCount; }
            internal set { SetProperty(ref _errorCount, value); }
        }

        public LessonResult()
        {

        }

        public LessonResult(string requested, string written, TimeSpan duration)
        {
            Update(requested, written, duration);
        }

        public void Update(string requested, string written, TimeSpan duration)
        {
            if (string.IsNullOrEmpty(requested) || string.IsNullOrEmpty(written) || duration.Seconds == 0)
                return;

            RequestedText = requested;
            WrittenText = written;
            Duration = duration;
            CharacterCount = requested.Length;
            GetErrorCount();
        }

        private void GetErrorCount()
        {
            if (string.IsNullOrEmpty(RequestedText) || string.IsNullOrEmpty(WrittenText))
                return;

            var errorCount = WrittenText.Length - RequestedText.Length;

            Debug.Assert(errorCount >= 0, "errorCount negative");

            ErrorCount = errorCount;
        }
    }
}