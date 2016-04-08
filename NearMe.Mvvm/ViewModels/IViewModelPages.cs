using GalaSoft.MvvmLight.Command;

namespace NearMe.Mvvm.ViewModels
{
    public interface IViewModelPages
    {
        RelayCommand Load { get; }
    }
}