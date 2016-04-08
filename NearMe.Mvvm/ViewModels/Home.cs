using System;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using NearMe.Domain.Interfaces;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;

//using INavigationService = Cimbalino.Toolkit.Services.INavigationService;


namespace NearMe.Mvvm.ViewModels
{
    public class Home : BaseVm, IViewModelPages
    {




        private RelayCommand _load;

        public Home(INavigationService navigation, IPlatform platform, IMessageBoxService messageBoxService)
            : base(navigation, platform, messageBoxService)
        {
        }

        public RelayCommand Load
        {
            get
            {
                return _load ?? (_load = new RelayCommand(
                    async () =>
                    {

                        try
                        {
                            LoadingCounter++;


                        }
                        catch (Exception e)
                        {

                            await MessageBoxService.ShowAsync(e.Message, "CAPTION");
                        }
                        finally
                        {
                            LoadingCounter--;
                        }



                    }));
            }

        }



    }
}
