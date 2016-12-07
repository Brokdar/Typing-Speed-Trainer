using System;

namespace Typing_Speed_Trainer.LessonFactories
{
    public class OutOfLessonsException : Exception
    {
        public OutOfLessonsException(string message) : base(message)
        {

        }
    }
}
