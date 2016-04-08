namespace NearMe.Mvvm.ViewModels
{
    public class BaseWithProgress : Models.ModelBase
    {
        private int _loadingCounter;
        public int LoadingCounter
        {
            get { return _loadingCounter; }
            set
            {
              
                if (value != _loadingCounter)
                {
                    _loadingCounter = value;
                
                }
                if (_loadingCounter < 0)
                    _loadingCounter = 0;

                IsLoading = _loadingCounter > 0;
            }
        }

        bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {

                _isLoading = value;
                NotifyPropertyChanged();

            }
        }


      
    }
}
