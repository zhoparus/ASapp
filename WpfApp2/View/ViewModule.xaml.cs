using System;
using MapControl;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2.View
{
    /// <summary>
    /// Логика взаимодействия для ViewModule.xaml
    /// </summary>
    public partial class ViewModule : Window
    {
        private readonly ViewModel viewModel = new ViewModel();
        public ViewModule()
        {
            InitializeComponent();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow but = new MainWindow();
            but.Show();
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.Projections.Add(new WebMercatorProjection());
            viewModel.Projections.Add(new Etrs89UtmProjection(32));

            viewModel.Layers.Add(
                "OpenStreetMap WMS",
                new WmsImageLayer
                {
                    ServiceUri = new Uri("http://ows.terrestris.de/osm/service"),
                    Layers = "OSM-WMS"
                });

            viewModel.Layers.Add(
                "TopPlusOpen WMS",
                new WmsImageLayer
                {
                    ServiceUri = new Uri("https://sgx.geodatenzentrum.de/wms_topplus_open"),
                    Layers = "web"
                });

            viewModel.Layers.Add(
                "Orthophotos Wiesbaden",
                new WmsImageLayer
                {
                    ServiceUri = new Uri("https://geoportal.wiesbaden.de/cgi-bin/mapserv.fcgi?map=d:/openwimap/umn/map/orthophoto/orthophotos.map"),
                    Layers = "orthophoto2017"
                });

            viewModel.CurrentProjection = viewModel.Projections[0];
            viewModel.CurrentLayer = viewModel.Layers.First().Value;

            DataContext = viewModel;
        }

        private void Map_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var map = (MapBase)sender;
            var pos = e.GetPosition(map);

            viewModel.PushpinLocation = map.ViewToLocation(pos);
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private MapProjection currentProjection;
        private IMapLayer currentLayer;
        Location pushpinLocation = new Location();
        public event PropertyChangedEventHandler PropertyChanged;

        public List<MapProjection> Projections { get; } = new List<MapProjection>();

        public Dictionary<string, IMapLayer> Layers { get; } = new Dictionary<string, IMapLayer>();

        public MapProjection CurrentProjection
        {
            get => currentProjection;
            set
            {
                currentProjection = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentProjection)));
            }
        }

        public IMapLayer CurrentLayer
        {
            get => currentLayer;
            set
            {
                currentLayer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLayer)));
            }
        }

        public Location PushpinLocation
        {
            get => pushpinLocation;
            set
            {
                pushpinLocation = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PushpinLocation)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PushpinText)));
            }
        }

        public string PushpinText
        {
            get
            {
                var latitude = (int)Math.Round(PushpinLocation.Latitude * 36000);
                var longitude = (int)Math.Round(Location.NormalizeLongitude(PushpinLocation.Longitude) * 36000);
                var latHemisphere = 'N';
                var lonHemisphere = 'E';

                if (latitude < 0)
                {
                    latitude = -latitude;
                    latHemisphere = 'S';
                }

                if (longitude < 0)
                {
                    longitude = -longitude;
                    lonHemisphere = 'W';
                }

                return string.Format(CultureInfo.InvariantCulture,
                    "{0}  {1:00} {2:00} {3:00.0}\n{4} {5:000} {6:00} {7:00.0}",
                    latHemisphere, latitude / 36000, (latitude / 600) % 60, (latitude % 600) / 10d,
                    lonHemisphere, longitude / 36000, (longitude / 600) % 60, (longitude % 600) / 10d);
            }
        }
       
    }
}
