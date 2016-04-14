using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NearMe.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        private Mvvm.ViewModels.Home vm;
        public Home()
        {
            this.InitializeComponent();

            vm = DataContext as Mvvm.ViewModels.Home;

            vm.PropertyChanged += Item_PropertyChanged;

        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName !="Item") return;

            MyMapControl.Center = new Geopoint(
                new BasicGeoposition
                {
                    Latitude = vm.Item.Center.Latitude
                    ,
                    Longitude = vm.Item.Center.Longitude
                });

        }
    }
}
