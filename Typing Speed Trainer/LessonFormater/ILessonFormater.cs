using System.Collections.Generic;

namespace Typing_Speed_Trainer.LessonFormater
{
    public interface ILessonFormater
    {
        Queue<Lesson> Format(string content);
    }
}
