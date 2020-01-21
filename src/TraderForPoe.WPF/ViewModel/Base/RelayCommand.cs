using System;
using System.Windows.Input;

namespace TraderForPoe.WPF.ViewModel.Base
{
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecuteEvaluator;

        private readonly Action _methodToExecute;

        public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
        {
            _methodToExecute = methodToExecute;
            _canExecuteEvaluator = canExecuteEvaluator;
        }

        public RelayCommand(Action methodToExecute) : this(methodToExecute, null)
        {
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteEvaluator == null)
            {
                return true;
            }

            var result = _canExecuteEvaluator.Invoke();
            return result;
        }

        public void Execute(object parameter)
        {
            _methodToExecute.Invoke();
        }
    }
}
