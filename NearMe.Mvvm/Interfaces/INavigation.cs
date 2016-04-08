namespace NearMe.Mvvm.Interfaces
{
    public interface INavigationService : GalaSoft.MvvmLight.Views.INavigationService
    {
        void NavigateToModal(string pageKey);
        void NavigateToModal(string pageKey, object parameter);

        void NavigateToNative(string pageKey, object parameter);

        object Parameters();



    }
}
