using Typing_Speed_Trainer.Statistics;

namespace Typing_Speed_Trainer
{
    public partial class TypingSpeedTrainerViewModel
    {
        public class StatisticRepresentation : NotifiableDataBase
        {
            private string _wpm;

            public string WPM
            {
                get { return _wpm; }
                set { SetProperty(ref _wpm, value); }
            }

            private string _cpm;

            public string CPM
            {
                get { return _cpm; }
                set { SetProperty(ref _cpm, value); }
            }

            private string _errorRate;

            public string ErrorRate
            {
                get { return _errorRate; }
                set { SetProperty(ref _errorRate, value); }
            }

            private string _score;

            public string Score
            {
                get { return _score; }
                set { SetProperty(ref _score, value); }
            }


            public StatisticRepresentation(Statistic statistic)
            {
                WPM = statistic.WordsPerMinute.ToString();
                CPM = statistic.CharactersPerMinute.ToString();
                ErrorRate = (statistic.ErrorRate).ToString("F2") + "%";
                Score = statistic.Score.ToString();
            }
        }
    }
}