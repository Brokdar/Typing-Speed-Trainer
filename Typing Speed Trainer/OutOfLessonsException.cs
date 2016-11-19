using System;

namespace Typing_Speed_Trainer
{
    public class OutOfLessonsException : Exception
    {
        public OutOfLessonsException(string message) : base(message)
        {

        }
    }
}
