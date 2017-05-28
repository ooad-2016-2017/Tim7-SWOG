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
using SPARK.ViewModel;
using Windows.UI.Core;
using System.Diagnostics;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using SPARK.Model;
using Windows.Devices.Geolocation;
using System.Threading;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserView : Page
    {
        protected int userID;
        protected int userType = -1;
        public UserView()
        {
            this.InitializeComponent();
            DataContext = new UserViewModel();

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };

            myMap.MapServiceToken = "laUq6i4377dfOIVIHYEI~T_8vyIA3sznxgRSix8_JFw~AvZvd6to90gmNls6DvTMFLuiu_ekbhwYin_dmDs9lqGpvqgeaCCf6mtqNdXkVKmP";
            loadPinsToMap();
            getUserLocation();

        }
        private async void getUserLocation()
        {
            var _cts = new CancellationTokenSource();

            CancellationToken token = _cts.Token;

            Geolocator geolocator = new Geolocator();

            // Make the request for the current position
            Geoposition pos = await geolocator.GetGeopositionAsync().AsTask(token);
            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.ZIndex = 0;
            mapIcon1.Image =
                RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/crveniPin.png"));
            Geopoint snPoint = pos.Coordinate.Point;
            mapIcon1.Location = snPoint;
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = "VASA LOKACIJA";
            myMap.MapElements.Add(mapIcon1);
            
            /*
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:

                    // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                    Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 0 }; 
                    Geoposition pos = await geolocator.GetGeopositionAsync();

                    MapIcon mapIcon1 = new MapIcon();
                    mapIcon1.ZIndex = 0;
                    mapIcon1.Image =
                        RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/crveniPin.png"));
                    Geopoint snPoint = pos.Coordinate.Point;
                    mapIcon1.Location = snPoint;
                    mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIcon1.Title = "VASA LOKACIJA";
                    myMap.MapElements.Add(mapIcon1);
                    break;
            }
            */
        }
        
        private void loadPinsToMap()
        {
            using (var db = new SPARKDbContext())
            {
                var parkings = db.Parkings;
                foreach (var p in parkings)
                {
                    MapIcon mapIcon1 = new MapIcon();
                    mapIcon1.ZIndex = 0;
                    mapIcon1.Image =
                        RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/crveniPin.png"));        
                    BasicGeoposition snPosition = new BasicGeoposition() { Latitude = p.CoordX, Longitude = p.CoordY };
                    Geopoint snPoint = new Geopoint(snPosition);
                    mapIcon1.Location = snPoint;
                    mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                    mapIcon1.Title = p.Name;
                    myMap.MapElements.Add(mapIcon1);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameters = e.Parameter as Tuple<int, int>;
            userType = parameters.Item1;
            userID = parameters.Item2;
            if (userType == 1)
                Debug.WriteLine("Ulogovani ste kao vlasnik " + userID.ToString());
            else if(userType == 0)
            {
                Debug.WriteLine("Ulogovani ste kao korisnik " + userID.ToString());
                PinButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                Debug.WriteLine("Niste ulogovani " + userID.ToString());
                PinButton.Visibility = Visibility.Collapsed;
                EditProfile.Visibility = Visibility.Collapsed;
                rezervisiMjesto.Visibility = Visibility.Collapsed;
                kupiKredite.Visibility = Visibility.Collapsed;
                registrujSe.Visibility = Visibility.Visible;
            }
        }



        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 43.865, Longitude = 18.4131 });
            myMap.ZoomLevel = 14;
            //ovisno o validaciji treba ovdje da se postavi visibility redom: registrujSe, rezervisiMjesto, kupiKredite, izmjena
        }
        private void rezervisiMjesto_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void registrujSe_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void rezervisiMjesto_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(BookParkingView));
        }
        private void kupiKredite_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(BuyCreditsView));
        }


        private void PinButton_Click(object sender, RoutedEventArgs e)
        {
            
            MySplitView.IsPaneOpen = true;
            
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(View.EditUserView));
        }

    }
}
