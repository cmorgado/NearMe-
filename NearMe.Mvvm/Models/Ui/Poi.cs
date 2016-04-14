using System;

namespace NearMe.Mvvm.Models.Ui
{

    public class PoiPoint : ModelBase
    {

        private double _latitude;
        public double Latitude
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

        private double _longitude;
        public double Longitude
        {
            get { return this._longitude; }
            set
            {
                if (_longitude != value)
                {
                    _longitude = value;
                    NotifyPropertyChanged();
                }
            }
        }



    }
    public class Poi : ModelBase
    {

        private PoiPoint _point = new PoiPoint();
        public PoiPoint Center
        {
            get { return this._point; }
            set
            {
                if (_point != value)
                {
                    _point = value;
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


        private string _email;
        public string Email
        {
            get { return this._email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
