using System;

using GalaSoft.MvvmLight.Command;
using NearMe.Domain.Interfaces;
using Cimbalino.Toolkit.Services;
using NearMe.Domain.Code;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;

//using INavigationService = Cimbalino.Toolkit.Services.INavigationService;

namespace NearMe.Mvvm.ViewModels
{
    public class BaseVm : BaseWithProgress
    {

        public readonly INavigationService NavigationService;

        public readonly IMessageBoxService MessageBoxService;
        public IPlatform Platform;

        bool _internetConnection = true;
        public bool InternetConnection
        {
            get { return _internetConnection; }
            set
            {
                if (_internetConnection == value) return;
                _internetConnection = value;
                NotifyPropertyChanged();
            }
        }


        bool _isLogged;
        public bool IsLogged
        {
            get { return _isLogged; }
            set
            {
                if (_isLogged == value) return;
                _isLogged = value;
                NotifyPropertyChanged();
            }
        }





        string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                if (_pageTitle == value) return;
                _pageTitle = value;
                NotifyPropertyChanged();
            }
        }

        RelayCommand _goBack;
        public RelayCommand GoBack
        {
            get
            {
                return _goBack ?? (_goBack = new RelayCommand(
                   async () =>
                     {

                         try
                         {
                             LoadingCounter++;

                             NavigationService.GoBack();

                         }
                         catch (Exception e)
                         {

                             await MessageBoxService.ShowAsync(e.Message, "");
                         }
                         finally
                         {
                             LoadingCounter--;
                         }



                     }));
            }

        }

        private RelayCommand _goToAbout;
        public RelayCommand GoToAbout
        {
            get
            {
                return _goToAbout ?? (_goToAbout = new RelayCommand(
            async () =>
            {

                try
                {
                    LoadingCounter++;
                    NavigationService.NavigateTo(Domain.Code.PagesDefinitions.About.ConvertToString());

                    //NavigationService.Navigate("NearMe.Views.About");

                }
                catch (Exception e)
                {

                    await MessageBoxService.ShowAsync(e.Message, "");
                }
                finally
                {
                    LoadingCounter--;
                }



            }));
            }

        }


        RelayCommand _unload;
        public RelayCommand Unload
        {
            get
            {
                return _unload ?? (_unload = new RelayCommand(
                     async () =>
                     {
                         try
                         {
                             LoadingCounter++;

                             NavigationService.GoBack();

                         }
                         catch (Exception e)
                         {

                             await MessageBoxService.ShowAsync(e.Message, "");
                         }
                         finally
                         {
                             LoadingCounter--;
                         }


                     }));
            }

        }


        public BaseVm(INavigationService navigation, IPlatform platform, IMessageBoxService messageBoxService)
        {
            NavigationService = navigation;

            Platform = platform;
            MessageBoxService = messageBoxService;
        }


    }
}
