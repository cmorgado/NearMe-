using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NearMe.Mvvm.Models
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName]String propertyName = null)
        {
            var handler = PropertyChanged;
            if (null == handler) return;
            try
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
            catch
            {
                // ignored
            }
        }
    }
}
