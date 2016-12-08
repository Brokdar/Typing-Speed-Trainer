using System.Text;
using Typing_Speed_Trainer.Statistics;

namespace Typing_Speed_Trainer
{
    public partial class TypingSpeedTrainerViewModel
    {
        public class LessonResultRepresentation : NotifiableDataBase
        {
            private string _characterCount;

            public string CharacterCount
            {
                get { return _characterCount; }
                set { SetProperty(ref _characterCount, value); }
            }

            private string _errorCount;

            public string ErrorCount
            {
                get { return _errorCount; }
                set { SetProperty(ref _errorCount, value); }
            }

            private string _duration;

            public string Duration
            {
                get { return _duration; }
                set { SetProperty(ref _duration, value); }
            }

            private string _writtenText;

            public string WrittenText
            {
                get { return _writtenText; }
                set { SetProperty(ref _writtenText, value); }
            }

            private string _source;

            public string Source
            {
                get { return _source; }
                set { SetProperty(ref _source, value); }
            }

            private string _difficulty;

            public string Difficulty
            {
                get { return _difficulty; }
                set { SetProperty(ref _difficulty, value); }
            }


            public LessonResultRepresentation(LessonResult result)
            {
                CharacterCount = result.Lesson.Count.ToString();
                ErrorCount = result.ErrorCount.ToString();
                Duration = result.Duration.ToString(@"mm\:ss\.ff");
                WrittenText = HightlightErrors(result);
                Source = result.Lesson.Source.ToString();
                Difficulty = GetDifficultry(result.Lesson.Difficulty);
            }

            private static string GetDifficultry(Difficulty lessonDifficulty)
            {
                switch (lessonDifficulty)
                {
                    case Typing_Speed_Trainer.Difficulty.VeryEasy:
                        return "Very Easy";
                    case Typing_Speed_Trainer.Difficulty.Easy:
                        return "Easy";
                    case Typing_Speed_Trainer.Difficulty.Medium:
                        return "Medium";
                    case Typing_Speed_Trainer.Difficulty.Hard:
                        return "Hard";
                    case Typing_Speed_Trainer.Difficulty.VeryHard:
                        return "Very Hard";
                    default:
                        return "Invalid";
                }
            }

            private static string HightlightErrors(LessonResult result)
            {
                var lesson = result.Lesson.Content.Replace(' ', '␣').Replace('\r', '⏎');
                var written = result.WrittenText.Replace(' ', '␣').Replace('\r', '⏎');

                if (result.ErrorCount == 0)
                    return written;

                var highlightErrorBuilder = new StringBuilder();
                const string beginHighlight = "<Span Foreground=\"Red\">";
                const string endHighlight = "</Span>";

                for (int i = 0, j = 0; i < lesson.Length; i++, j++)
                {
                    if (lesson[i] == written[j])
                    {
                        highlightErrorBuilder.Append(written[j]);
                    }
                    else
                    {
                        highlightErrorBuilder.Append(beginHighlight);
                        highlightErrorBuilder.Append(written[j++]);
                        highlightErrorBuilder.Append(endHighlight);
                        highlightErrorBuilder.Append(written[j]);
                    }
                }

                return highlightErrorBuilder.ToString();
            }
        }
    }
}