using System;
using System.Collections.ObjectModel;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using NearMe.Domain.Code;
using NearMe.Domain.Interfaces;
using NearMe.Mvvm.Models.Ui;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;

//using INavigationService = Cimbalino.Toolkit.Services.INavigationService;


namespace NearMe.Mvvm.ViewModels
{
    public class Home : BaseVm, IViewModelPages
    {

        private Models.Ui.Poi _item;
        public Models.Ui.Poi Item
        {
            get { return this._item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ObservableCollection<Models.Ui.Poi  > _items = new ObservableCollection<Poi>();
        public ObservableCollection<Models.Ui.Poi> Items
        {
            get { return this._items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    NotifyPropertyChanged();
                }
            }
        }






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

        private RelayCommand _goToDetails;
        public RelayCommand GoToDetails
        {
            get
            {
                return _goToDetails ?? (_goToDetails = new RelayCommand(
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
