using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Typing_Speed_Trainer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly TypingSpeedTrainerViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new TypingSpeedTrainerViewModel();
            DataContext = _viewModel;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                _viewModel.KeystrokeDetected(' ');
                e.Handled = true;
            }
            else if (e.Key == Key.Tab)
            {
                _viewModel.KeystrokeDetected('\t');
                e.Handled = true;
            }
        }

        private void OnTextInput(object sender, TextCompositionEventArgs eventArgs)
        {
            var character = eventArgs.Text.FirstOrDefault();
            if (character == '\0')
                return;

            _viewModel.KeystrokeDetected(character);
            eventArgs.Handled = true;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            _viewModel.SaveStatistic();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadStatistic();
        }
    }
}
