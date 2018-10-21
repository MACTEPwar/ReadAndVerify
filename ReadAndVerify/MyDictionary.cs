using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAndVerify
{
    /// <summary>
    /// Класс для 2стронней привязке словаря
    /// </summary>
    public class MyDictionary: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _key; private double _value;

        public string Key { get { return _key; } set { _key = value; RaisePropertyChanged("Key"); } }
        public double Value { get { return _value; } set { _value = value; RaisePropertyChanged("Value"); } }

        public MyDictionary(string key, double value)
        {
            Key = key;
            Value = value;
        }
        
        protected virtual void RaisePropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        protected void RaisePropertyChanged(string propertyName)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
