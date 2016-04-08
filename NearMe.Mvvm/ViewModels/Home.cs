using System;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using NearMe.Domain.Code;
using NearMe.Domain.Interfaces;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;

//using INavigationService = Cimbalino.Toolkit.Services.INavigationService;


namespace NearMe.Mvvm.ViewModels
{
    public class Home : BaseVm, IViewModelPages
    {






        public Home(INavigationService navigation, IPlatform platform, IMessageBoxService messageBoxService)
            : base(navigation, platform, messageBoxService)
        {
        }

        private RelayCommand _load;
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

                            await MessageBoxService.ShowAsync("Olá Mundo", "CAPTION");
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

        private RelayCommand _GoToDetails;
        public RelayCommand GoToDetails
        {
            get
            {
                return _GoToDetails ?? (_GoToDetails = new RelayCommand(
            async () =>
            {

                try
                {
                    LoadingCounter++;
                    NavigationService.NavigateTo(Domain.Code.PagesDefinitions.Details.ConvertToString());

                }
                catch (Exception e)
                {

                    await MessageBoxService.ShowAsync("", "");
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
