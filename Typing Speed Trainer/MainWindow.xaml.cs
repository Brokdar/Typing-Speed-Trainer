using System;
using System.Windows;
using System.Windows.Input;

namespace Typing_Speed_Trainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextInput += OnCharacterOccured;
        }

        private void OnCharacterOccured(object sender, TextCompositionEventArgs args)
        {
            Console.WriteLine(args.Text);
            Console.WriteLine(args.ControlText);
            Console.WriteLine(args.SystemText);
            Console.WriteLine(Keyboard.IsKeyDown(Key.Enter) ? "Enter" : "");
        }
    }
}
