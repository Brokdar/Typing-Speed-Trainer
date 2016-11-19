using System;

namespace Typing_Speed_Trainer
{
    public class DummyLessonCreator : LessonCreator
    {
        private const string VeryEasyContent = "This is a very easy example";
        private const string EasyContent = "This is an easy Example";
        private const string MediumContent = "this is a medium example 1234";
        private const string HardContent = "This is a hard Example 1234";
        private const string VeryHardContent = "This is a very hard Example 1234";

        public override void CreateLessons(Uri source)
        {
            Source = source;
            OnLoadContentSuccessfully();

            Lessons.Enqueue(new Lesson(VeryEasyContent, source));
            Lessons.Enqueue(new Lesson(EasyContent, source));
            Lessons.Enqueue(new Lesson(MediumContent, source));
            Lessons.Enqueue(new Lesson(HardContent, source));
            Lessons.Enqueue(new Lesson(VeryHardContent, source));

            OnLessonsCreated(Lessons.Count);
        }
    }
}
