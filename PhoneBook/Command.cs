using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace PhoneBook
{
    /* Тестовый класс */
    //TODO: Удалить?
    class Command : ICommand
    {
        private Action methodToExecute = null;
        private Func<bool> methodToDetectCanExecute = null;
        private DispatcherTimer canExecuteChangedEventTimer = null;

        public event EventHandler CanExecuteChanged;

        public Command(Action methodToExecute, Func<bool>methodToDetectCanExecute)
        {
            this.methodToExecute = methodToExecute;
            this.methodToDetectCanExecute = methodToDetectCanExecute;
            this.canExecuteChangedEventTimer = new DispatcherTimer();
            this.canExecuteChangedEventTimer.Tick += canExecuteChangedEventTimer_Tick;
            this.canExecuteChangedEventTimer.Interval = new TimeSpan(0, 0, 1);
            this.canExecuteChangedEventTimer.Start();
        }

        public bool CanExecute(object parameter)
        {
            if(this.methodToDetectCanExecute != null)
                return true;
            else
                return this.methodToDetectCanExecute();
        }

        public void Execute(object parameter)
        {
            this.methodToExecute();
        }


        void canExecuteChangedEventTimer_Tick(object sender, object e)
        {
            if(this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
