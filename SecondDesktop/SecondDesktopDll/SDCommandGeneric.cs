using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SecondDesktopDll
{
    public class SDCommand<T> : ICommand
     {
        public SDCommand(Action<T> execute) : this(execute, null)
        {

        }
        
        public SDCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        private Func<T, bool> _canExecute;
        public bool CanExecute(object param)
        {
            if (_canExecute == null) return true;
            return _canExecute((T)param);
        }
        
        private Action<T> _execute;
        public void Execute(object param)
        {
            if (_execute != null && CanExecute(param))
            {
                _execute((T)param);
            }
        }
    }
}
