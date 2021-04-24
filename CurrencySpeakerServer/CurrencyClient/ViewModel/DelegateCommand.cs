using System;
using System.Windows.Input;

namespace CurrencyClient.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteFunc;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc = null)
        {
            _executeAction = executeAction;
            _canExecuteFunc = canExecuteFunc;
        }

        public bool CanExecute(object parameter) => _canExecuteFunc is null || _canExecuteFunc(parameter);

        public void Execute(object parameter) => _executeAction(parameter);

        public event EventHandler CanExecuteChanged;
    }
}