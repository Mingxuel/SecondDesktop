using System;
using System.Windows.Input;

namespace SecondDesktopDll
{
    public class SDCommand : ICommand
    {
        public SDCommand(Action<object> execute) : this(execute, null)
        {

        }

        public SDCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        private Func<object, bool> canExecute;
        public bool CanExecute(object param)
        {
            if (canExecute == null) return true;
            return canExecute(param);
        }

        private Action<object> execute;
        public void Execute(object param)
        {
            if (execute != null && CanExecute(param))
            {
                execute(param);
            }
        }
    }
}
