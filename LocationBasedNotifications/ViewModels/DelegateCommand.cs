using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LocationBasedNotifications.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        #region Constructors
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion Constructors

        #region ICommand

        #region Event Handlers
        public event EventHandler CanExecuteChanged;
        #endregion Event Handlers                      

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            var helper = CanExecuteChanged;
            if (helper != null)
            {
                helper(this, EventArgs.Empty);
            }
        }
        #endregion ICommand
    }
}
