using System;

namespace Typing_Speed_Trainer.LessonFactories
{
    public class DummyLessonFactory : LessonFactory
    {
        private const string VeryEasyContent = "this is a very easy example";
        private const string EasyContent = "This is an easy Example";
        private const string MediumContent = "this is a medium example 1234";
        private const string HardContent = "This is a hard Example 1234";
        private const string VeryHardContent = "This is a very hard Example 1234!";

        public DummyLessonFactory() : base(null)
        {

        }

        public override int CreateLessons(Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Source = source;

            Lessons.Enqueue(new Lesson(VeryEasyContent, Source));
            Lessons.Enqueue(new Lesson(EasyContent, Source));
            Lessons.Enqueue(new Lesson(MediumContent, Source));
            Lessons.Enqueue(new Lesson(HardContent, Source));
            Lessons.Enqueue(new Lesson(VeryHardContent, Source));

            return Lessons.Count;
        }
    }
}
