using System;
using System.Collections.Generic;
using Typing_Speed_Trainer.LessonFormater;

namespace Typing_Speed_Trainer.LessonFactories
{
    public abstract class LessonFactory
    {
        protected ILessonFormater Formater;
        public Uri Source { get; protected set; }
        protected Queue<Lesson> Lessons { get; set; }


        public abstract int CreateLessons(Uri source);

        protected LessonFactory(ILessonFormater formater)
        {
            Formater = formater;
            Lessons = new Queue<Lesson>();
        }

        public Lesson NextLesson()
        {
            if (Lessons.Count == 0)
            {
                throw new OutOfLessonsException("Error: No more Lessons in Queue available");
            }

            return Lessons.Dequeue();
        }
    }
}
