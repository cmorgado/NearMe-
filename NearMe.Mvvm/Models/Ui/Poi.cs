using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMe.Mvvm.Models.Ui
{
   public class Poi:ModelBase
    {
        private string _latitude;
        public string Latitude
        {
            get { return this._latitude; }
            set
            {
                if (_latitude != value)
                {
                    _latitude = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return this._name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
