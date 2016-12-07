using System;
using Typing_Speed_Trainer.Statistics;

namespace Typing_Speed_Trainer.LessonExecution
{
    public class LessonExecutionService
    {
        private const int TimerIntervalInMilliseconds = 10;
        //private Timer _timer;

        public virtual LessonResult Execute(Lesson lesson)
        {
            throw new NotImplementedException();
        }
    }
}
