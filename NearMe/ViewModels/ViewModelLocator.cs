using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using NearMe.Domain.Interfaces;
using NearMe.Mvvm.ViewModels;
using NearMe.Services;
using GalaSoft.MvvmLight;
using NearMe.Domain.Code;
using NearMe.Views;


namespace NearMe.ViewModels
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);



            SimpleIoc.Default.Register<IMessageBoxService, MessageBoxService>();
            SimpleIoc.Default.Register<IPlatform, Platform>();

            Cimbalino.Toolkit.Services.NavigationService cimbalinoNavigationService = new NavigationService();

            SimpleIoc.Default.Register<Cimbalino.Toolkit.Services.INavigationService>(() => cimbalinoNavigationService);

            GalaSoft.MvvmLight.Views.NavigationService mvvmLightNavigationService = new GalaSoft.MvvmLight.Views.NavigationService();
            mvvmLightNavigationService.Configure(PagesDefinitions.HomePage.ConvertToString(), typeof(Views.Home));
            mvvmLightNavigationService.Configure(PagesDefinitions.About.ConvertToString(), typeof(Views.About));
            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.INavigationService>(() => mvvmLightNavigationService);

            SimpleIoc.Default.Register<Mvvm.ViewModels.Home>();
            SimpleIoc.Default.Register<Mvvm.ViewModels.About>();

        }


        public Mvvm.ViewModels.Home Home => ServiceLocator.Current.GetInstance<Mvvm.ViewModels.Home>();
        public Mvvm.ViewModels.About About => ServiceLocator.Current.GetInstance<Mvvm.ViewModels.About>();

    }
}
