namespace NearMe.Mvvm.Models.Ui
{
   public class Poi:ModelBase
    {
        private string _longitude;
        public string Longitude
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
