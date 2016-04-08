using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using NearMe.Domain.Interfaces;

namespace NearMe.Mvvm.ViewModels
{
    public class Details : BaseVm, IViewModelPages
    {
        public Details(GalaSoft.MvvmLight.Views.INavigationService navigation, IPlatform platform, IMessageBoxService messageBoxService) : base(navigation, platform, messageBoxService)
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
                    await MessageBoxService.ShowAsync("CHegEI", "");

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
