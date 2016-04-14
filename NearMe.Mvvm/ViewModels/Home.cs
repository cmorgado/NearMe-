using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cimbalino.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using NearMe.Domain.Code;
using NearMe.Domain.Interfaces;
using NearMe.Mvvm.Models.Ui;
using NearMe.Rest.Service;
using INavigationService = GalaSoft.MvvmLight.Views.INavigationService;

//using INavigationService = Cimbalino.Toolkit.Services.INavigationService;


namespace NearMe.Mvvm.ViewModels
{
    public class Home : BaseVm, IViewModelPages
    {
        public string BingKey { get; set; } =
            ""
            ;
        private Poi _item = new Poi { Center = new PoiPoint { Latitude = 41.178039, Longitude = -8.608079 } };

        public Poi Item
        {
            get { return _item; }
            set
            {
                if (_item != value)
                {
                    _item = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private ObservableCollection<Poi> _items = new ObservableCollection<Poi>();
        public ObservableCollection<Poi> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                  //  NotifyPropertyChanged();
                }
            }
        }






        public Home(INavigationService navigation, IPlatform platform, IMessageBoxService messageBoxService)
            : base(navigation, platform, messageBoxService)
        {
            this.Item.PropertyChanged += Item_PropertyChanged;

        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           // NotifyPropertyChanged("Item");


        }

        private RelayCommand<Poi> _selectMe;
        public RelayCommand<Poi> SelectMe
        {
            get
            {
                return _selectMe ?? (_selectMe = new RelayCommand<Poi>(
            async (m) =>
            {

                try
                {
                    LoadingCounter++;
                    this.Item = m;

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


        private RelayCommand _remove;
        public RelayCommand Remove
        {
            get
            {
                return _remove ?? (_remove = new RelayCommand(
            async () =>
            {

                try
                {
                    LoadingCounter++;
                    this.Items.Remove(this.Item);
                    this.Item= new Poi();

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

                          await  Task.Delay(3000);
                            ServiceBroker broker = new ServiceBroker();

                            var csv = await broker.GetPois();


                            if (!csv.Error.HasError)
                            {
                                var linhas = csv.Raw.Split('\n');
                                foreach (var linha in linhas)
                                {
                                    var p = new Poi
                                    {
                                        Name = linha.Split(',')[1]
                                        ,
                                        Email = linha.Split(',')[5],
                                        Center =
                                        {
                                            Latitude = double.Parse(linha.Split(',')[2]),
                                            Longitude = double.Parse(linha.Split(',')[3])
                                        }
                                    };



                                    Items.Add(p);
                                }
                                ;
                                Item = Items[0];

                                ;
                            }
                            else
                            {
                                await MessageBoxService.ShowAsync("Error a carregar os pois via REST", "CAPTION");

                            }

                            //await MessageBoxService.ShowAsync("Olá Mundo", "CAPTION");
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
                    NavigationService.NavigateTo(PagesDefinitions.Details.ConvertToString());

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
