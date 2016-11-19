using System;
using System.Collections.Generic;

namespace Typing_Speed_Trainer
{
    public abstract class LessonCreator
    {
        protected Uri Source { get; set; }
        protected Queue<Lesson> Lessons { get; set; }

        public event EventHandler LoadContentSuccessfully;
        public event EventHandler LoadContentFailed;
        public event EventHandler<int> LessonsCreated;

        public abstract void CreateLessons(Uri source);

        public Lesson NextLesson()
        {
            if (Lessons.Count == 0)
            {
                throw new OutOfLessonsException("Error: No more Lessons in Queue available");
            }

            return Lessons.Dequeue();
        }

        protected virtual void OnLoadContentSuccessfully()
        {
            LoadContentSuccessfully?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnLoadContentFailed()
        {
            LoadContentFailed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnLessonsCreated(int e)
        {
            LessonsCreated?.Invoke(this, e);
        }
    }
}
