using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Typing_Speed_Trainer
{
    public partial class TypingSpeedTrainerViewModel : NotifiableDataBase
    {
        private readonly TypingSpeedTrainer _trainer;

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private int _caretPosition;
        public int CaretPosition
        {
            get { return _caretPosition; }
            set { SetProperty(ref _caretPosition, value); }
        }

        private Lesson _currentLesson;

        private LessonResultRepresentation _lessonResult;
        public LessonResultRepresentation LessonResult
        {
            get { return _lessonResult; }
            set { SetProperty(ref _lessonResult, value); }
        }

        private StatisticRepresentation _lessonStatistic;
        public StatisticRepresentation LessonStatistic
        {
            get { return _lessonStatistic; }
            internal set { SetProperty(ref _lessonStatistic, value); }
        }

        private StatisticRepresentation _sessionStatistic;
        public StatisticRepresentation SessionStatistic
        {
            get { return _sessionStatistic; }
            internal set { SetProperty(ref _sessionStatistic, value); }
        }

        private StatisticRepresentation _overallStatistic;
        public StatisticRepresentation OverallStatistic
        {
            get { return _overallStatistic; }
            internal set { SetProperty(ref _overallStatistic, value); }
        }


        private string _currentUri;
        public string CurrentUri
        {
            get { return _currentUri; }
            set
            {
                SetProperty(ref _currentUri, value);
                if (!_changedByCombobox)
                {
                    SelectedUriIndex = -1;
                }
                _changedByCombobox = false;
            }
        }

        private bool _changedByCombobox;

        private readonly LastVisitedUrisCollection _lastVisited;
        private List<string> _lastVisitedUris;
        public List<string> LastVisitedUris
        {
            get { return _lastVisitedUris; }
            set { SetProperty(ref _lastVisitedUris, value); }
        }

        private int _selectedUriIndex;
        public int SelectedUriIndex
        {
            get { return _selectedUriIndex; }
            set
            {
                SetProperty(ref _selectedUriIndex, value);
                if (SelectedUriIndex >= 0 && SelectedUriIndex < LastVisitedUris.Count)
                {
                    _changedByCombobox = true;
                    CurrentUri = _lastVisited.GetUri(LastVisitedUris[SelectedUriIndex]).ToString();
                }
            }
        }

        public ICommand CreateLessonCommand { get; set; }

        public TypingSpeedTrainerViewModel()
        {
            _trainer = new TypingSpeedTrainer();

            _changedByCombobox = false;
            NotifiyMessage("Welcome to Typing-Speed-Trainer");

            _lastVisited = new LastVisitedUrisCollection(10);
            _lastVisited.Append(new Uri("https://de.wikipedia.org/wiki/Spezial:Zuf%C3%A4llige_Seite"));
            _lastVisited.Append(new Uri("http://www.spiegel.de/"));
            _lastVisited.Append(new Uri("http://www.nytimes.com/"));
            _lastVisited.Append(new Uri("http://www.golem.de/"));

            LastVisitedUris = _lastVisited.Uris();
            SelectedUriIndex = -1;

            CreateLessonCommand = new RelayCommand(CreateLessons);

            _trainer.ResultsAvailable += OnResultsAvailable;
        }

        public void CreateLessons(object o)
        {
            try
            {
                var uri = new Uri(CurrentUri);
                _lastVisited.Append(uri);
                LastVisitedUris = _lastVisited.Uris();
                _trainer.CreateNewLessons(uri, LessonProvider.Dummy);
                _currentLesson = _trainer.NextLesson();
                LessonContent(_currentLesson.Content);
            }
            catch (ArgumentNullException)
            {
                NotifiyMessage("No URL: please enter a valid URL");
            }
            catch (System.UriFormatException)
            {
                NotifiyMessage("Invalid URL: please check the spelling of the URL");
            }
        }

        public void StartLesson()
        {
            _trainer.Start(_currentLesson);
            CaretPosition = _trainer.CurrentCharacter;
        }

        public void KeystrokeDetected(char key)
        {
            _trainer.OnKeystrokeDetected(key);
            CaretPosition = _trainer.CurrentCharacter;
        }

        public void OnResultsAvailable(object sender, EventArgs eventArgs)
        {
            LessonResult = new LessonResultRepresentation(_trainer.LessonResult);
            LessonStatistic = new StatisticRepresentation(_trainer.LessonStatistic);
            SessionStatistic = new StatisticRepresentation(_trainer.SessionStatistic);
            OverallStatistic = new StatisticRepresentation(_trainer.OverallStatistic);

            if (_trainer.AvailableLessons > 0)
            {
                _currentLesson = _trainer.NextLesson();
                LessonContent(_currentLesson.Content);
            }
            else
            {
                NotifiyMessage("No more Lessons available, please create new Lessons");
                _currentLesson = null;
            }
        }

        private void LessonContent(string lessonContent)
        {
            Content = lessonContent.Replace(' ', '␣').Replace('\r', '⏎');
        }

        private void NotifiyMessage(string text)
        {
            Content = text;
            CaretPosition = int.MaxValue;
        }
    }
}
