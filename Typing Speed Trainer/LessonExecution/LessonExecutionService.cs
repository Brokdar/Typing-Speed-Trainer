using System;
using System.Diagnostics;
using System.Text;
using Typing_Speed_Trainer.Statistics;

namespace Typing_Speed_Trainer.LessonExecution
{
    public class LessonExecutionService
    {
        private Lesson _currentLesson;
        private readonly Stopwatch _stopwatch;
        private readonly StringBuilder _keystrokes;
        public int CurrentCharacterIndex { get; internal set; }
        private bool _wrongKeyPressed;
        public bool LessonExecuting { get; internal set; }

        public event EventHandler<LessonResult> LessonCompleted;


        public LessonExecutionService()
        {
            _stopwatch = new Stopwatch();
            LessonExecuting = false;
            _keystrokes = new StringBuilder();
            CurrentCharacterIndex = int.MaxValue;
        }

        public virtual void Start(Lesson lesson)
        {
            if (lesson == null || LessonExecuting) return;

            _currentLesson = lesson;
            CurrentCharacterIndex = 0;
            _wrongKeyPressed = false;
            _keystrokes.Clear();
            LessonExecuting = true;

            _stopwatch.Restart();
        }

        private void Stop()
        {
            _stopwatch.Stop();
            LessonExecuting = false;
            CurrentCharacterIndex = int.MaxValue;
            OnLessonCompleted(new LessonResult(_currentLesson, _keystrokes.ToString(), _stopwatch.Elapsed));
        }

        public void OnKeystrokeDetected(char detectedCharacter)
        {
            if (!LessonExecuting)
                return;

            if (CharacterChecker.Check(_currentLesson.Content[CurrentCharacterIndex], detectedCharacter))
            {
                AppendKeystroke(detectedCharacter);

                if (IsLessonComplete())
                    Stop();
            }
            else
            {
                if (_wrongKeyPressed)
                    return;

                AppendKeystroke(detectedCharacter, true);
            }
        }

        private void AppendKeystroke(char key, bool wrongKey = false)
        {
            _keystrokes.Append(key);
            _wrongKeyPressed = wrongKey;
            if (!wrongKey)
                CurrentCharacterIndex++;
        }

        private bool IsLessonComplete()
        {
            return LessonExecuting && CurrentCharacterIndex == _currentLesson.Count;
        }

        protected virtual void OnLessonCompleted(LessonResult result)
        {
            LessonCompleted?.Invoke(this, result);
        }
    }
}
