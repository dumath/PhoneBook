using System;
using System.Collections;
using System.ComponentModel;

namespace PhoneBook 
{
    //Определение объекта информации, над которым будут выполняться операции.
    class Contact : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        private string _name;
        private long _number;
        #endregion

        #region Propertyes
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                //this.onPropertyChanged(Name);
            }
        }

        public long Number
        {
            get => _number;
            set
            {
                _number = value;
                //this.onPropertyChanged(Number);
            }
        }
        #endregion

        #region Methods
        private void onPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
