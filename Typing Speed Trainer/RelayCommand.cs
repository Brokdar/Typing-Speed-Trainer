﻿using System;
using System.Windows.Input;

namespace Typing_Speed_Trainer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _executeHandler;
        private readonly Predicate<object> _canExecuteHandler;

        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _executeHandler = execute;
            _canExecuteHandler = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null) return true;
            return _canExecuteHandler(parameter);
        }

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}