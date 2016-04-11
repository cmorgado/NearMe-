using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using NearMe.Domain.Interfaces;
using NearMe.Services;
using NearMe.Domain.Code;


namespace NearMe.ViewModels
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);



            SimpleIoc.Default.Register<IMessageBoxService, MessageBoxService>();
            SimpleIoc.Default.Register<IPlatform, Platform>();

            NavigationService cimbalinoNavigationService = new NavigationService();

            SimpleIoc.Default.Register<INavigationService>(() => cimbalinoNavigationService);

            GalaSoft.MvvmLight.Views.NavigationService mvvmLightNavigationService = new GalaSoft.MvvmLight.Views.NavigationService();
            mvvmLightNavigationService.Configure(PagesDefinitions.HomePage.ConvertToString(), typeof(Views.Home));
            mvvmLightNavigationService.Configure(PagesDefinitions.About.ConvertToString(), typeof(Views.About));
            mvvmLightNavigationService.Configure(PagesDefinitions.Details.ConvertToString(), typeof(Views.Details));
            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.INavigationService>(() => mvvmLightNavigationService);

           SimpleIoc.Default.Register<IStorageServiceHandler, StorageServiceHandler>();
            SimpleIoc.Default.Register<IStorageService, StorageService>();

            SimpleIoc.Default.Register<Mvvm.ViewModels.Home>();
            SimpleIoc.Default.Register<Mvvm.ViewModels.About>();
            SimpleIoc.Default.Register<Mvvm.ViewModels.Details>();

        }


        public Mvvm.ViewModels.Home Home => ServiceLocator.Current.GetInstance<Mvvm.ViewModels.Home>();
        public Mvvm.ViewModels.About About => ServiceLocator.Current.GetInstance<Mvvm.ViewModels.About>();

        public Mvvm.ViewModels.Details Details => ServiceLocator.Current.GetInstance<Mvvm.ViewModels.Details>();

    }
}
