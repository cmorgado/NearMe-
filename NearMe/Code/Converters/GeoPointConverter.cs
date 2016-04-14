using System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Data;

namespace NearMe.Code.Converters
{
    public class GeoPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {


            if (value == null) return new Geopoint(new BasicGeoposition { Latitude = 41.178039, Longitude = -8.608079 });

            var p = value as Mvvm.Models.Ui.PoiPoint;
            if (p == null) return new Geopoint(new BasicGeoposition { Latitude = 41.178039, Longitude = -8.608079 });
            var center = new BasicGeoposition
            {
                Latitude = p.Latitude,
                Longitude = p.Longitude
            };
            var result = new Geopoint(center);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
