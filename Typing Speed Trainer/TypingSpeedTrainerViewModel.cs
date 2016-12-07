using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Typing_Speed_Trainer
{
    public class TypingSpeedTrainerViewModel : NotifiableDataBase
    {
        private TypingSpeedTrainer _trainer;

        private Lesson _currentLesson;
        public Lesson CurrentLesson
        {
            get { return _currentLesson; }
            set { SetProperty(ref _currentLesson, value); }
        }

        private Uri _sourceUri;
        public Uri SourceUri
        {
            get { return _sourceUri; }
            set { SetProperty(ref _sourceUri, value); }
        }

        private Dictionary<string, Uri> _lastVisitedUris;
        public ObservableCollection<string> LastVisitedUris { get; set; }
        private int _selectedUri;
        public int SelectedUri
        {
            get { return _selectedUri; }
            set
            {
                SetProperty(ref _selectedUri, value);
                if (_lastVisitedUris.ContainsKey(LastVisitedUris[SelectedUri]))
                {
                    SourceUri = _lastVisitedUris[LastVisitedUris[SelectedUri]];
                }
            }
        }

        public TypingSpeedTrainerViewModel()
        {
            _trainer = new TypingSpeedTrainer();

            _lastVisitedUris = new Dictionary<string, Uri>
            {
                {"Wikipedia", new Uri("https://de.wikipedia.org/wiki/Spezial:Zuf%C3%A4llige_Seite")},
                {"Spiegel", new Uri("http://www.spiegel.de/")},
                {"NY Times", new Uri("http://www.nytimes.com/")}
            };

            LastVisitedUris = new ObservableCollection<string>();
            foreach (var key in _lastVisitedUris.Keys)
            {
                LastVisitedUris.Add(key);
            }
            SelectedUri = 0;

            SourceUri = _lastVisitedUris[LastVisitedUris[SelectedUri]];
            _trainer.CreateNewLessons(SourceUri, LessonProvider.Dummy);
            CurrentLesson = _trainer.NextLesson();
        }
    }
}
