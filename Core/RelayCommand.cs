using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasySave.Core
{
    // Implementation of ICommand interface for RelayCommand pattern
    class RelayCommand : ICommand
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        // Event handler for CanExecuteChanged event, used to update the enabled state of the command
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Constructor for RelayCommand, takes an Action to execute and a Func to determine if it can be executed
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Determines if the command can be executed with the given parameter
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Executes the command with the given parameter
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
