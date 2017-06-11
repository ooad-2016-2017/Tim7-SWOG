using Windows.System;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SPARK.ViewModel;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;
using SPARK.Model;
using System.Collections.Generic;
using System;
using Windows.Foundation;
using System.Diagnostics;
using System.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserView : Page
    {
        public static int userID;
        public static int userType = -1;
        public static bool spremno=false;
        private Parking choosenParking = null;
        public static Model.User trenutni; //OVDJE SAM DODAO Model PRIJE User JER MI JE IZBACIVALO ERRORRRRRRRR GRRRRR
        List<Parking> Parkings = new List<Parking>();

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
        }
        private async void getUserLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            Geolocator geolocator = new Geolocator { DesiredAccuracyInMeters = 1 };

            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                Geoposition pos = await geolocator.GetGeopositionAsync();
                MapIcon mapIcon1 = new MapIcon();
                mapIcon1.ZIndex = 0;
                mapIcon1.Image =
                RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/trenutnaLokacija.png"));
                Geopoint snPoint = pos.Coordinate.Point;
                mapIcon1.Location = snPoint;
                mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                mapIcon1.Title = "VASA LOKACIJA";
                myMap.MapElements.Add(mapIcon1);
                myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = pos.Coordinate.Latitude , Longitude = pos.Coordinate.Longitude });
            }          
        }

        public async void loadPinsToMap()
        {
            /*using (var db = new SPARKDbContext())
              {
                  var parkings = db.Parkings;
                  foreach (var p in parkings)
                  {
                      MapIcon mapIcon1 = new MapIcon();
                      mapIcon1.ZIndex = 0;
                      mapIcon1.Image =
                      RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/crveniPinmali.png"));        
                      BasicGeoposition snPosition = new BasicGeoposition() { Latitude = p.CoordX, Longitude = p.CoordY };
                      Geopoint snPoint = new Geopoint(snPosition);
                      mapIcon1.Location = snPoint;
                      mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
                      mapIcon1.Title = p.Name;
                    myMap.MapElements.Add(mapIcon1);          
                  }
              }*/
            //List<Parking> Parkings;
            List<Azure.Parking> listaParkinga = await App.MobileService.GetTable<Azure.Parking>().ToListAsync();
            if (listaParkinga.Count != 0)
            {
                //Parkings = new List<Parking>();

                foreach (Azure.Parking p in listaParkinga)
                {
                    Parking novi = new Parking();
                    novi.Id = Convert.ToInt32(p.id);
                    novi.Name = p.Name;
                    novi.Address = p.Address;
                    novi.Capacity = p.Capacity;
                    novi.CoordX = p.CoordX;
                    novi.CoordY = p.CoordY;
                    novi.MinCredits = Convert.ToInt32(p.MinCredits);
                    novi.MinCredits = Convert.ToInt32(p.MonthlyProfit);
                    novi.NumTakenSpaces = p.NumTakenSpaces;
                    novi.Price = Convert.ToInt32(p.Price);
                    novi.TodaysProfit = Convert.ToInt32(p.TodaysProfit);
                    novi.WorkingHours = new KeyValuePair<DateTime, DateTime>(Convert.ToDateTime(p.WorkingFrom), Convert.ToDateTime(p.WorkingTo));
                    novi.Zone = p.Zone;
                    Parkings.Add(novi);
                }
                foreach (var p in Parkings)
                {
                    MapIcon mapIcon1 = new MapIcon();
                    mapIcon1.ZIndex = 0;
                    mapIcon1.Image =
                    RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/crveniPinmali.png"));
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
            spremno = true;
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
            DataContext = new UserViewModel();
        }



        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 43.865, Longitude = 18.4131 });
            myMap.ZoomLevel = 14;
            getUserLocation();
        }
        private void rezervisiMjesto_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void registrujSe_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(RegistrationTypeView));
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
            ParkingsListView.ItemsSource = UserViewModel.Parkings;
            MySplitView.IsPaneOpen = true;
            
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(View.EditUserView));

        }

        private void AddParkingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(RegisterParkingView),userID);

        }

        private void myMap_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            MapIcon myClickedIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            
            //Debug.WriteLine("Kliknuli ste na parking: " + myClickedIcon.Title);
            using (var db = new SPARKDbContext())
            {
                /*choosenParking = db.Parkings
                        .Where(b => b.Name == myClickedIcon.Title)
                        .FirstOrDefault();
                cijenaPoSatu.Text = myClickedIcon.Title;*/
                foreach(Parking p in Parkings)
                {
                    if(p.Name == myClickedIcon.Title)
                    {
                        choosenParking = p;
                        globalNaziv.Text = choosenParking.Name;
                        cijenaPoSatu.Text = Convert.ToString(choosenParking.Price);
                        parkingZona.Text = Convert.ToString(choosenParking.Zone);
                        lokacija.Text = Convert.ToString(choosenParking.Address);
                        string vrijeme = Convert.ToString(choosenParking.WorkingHours);
                        radnoVrijeme.Text = vrijeme.Substring(11,5) + " AM - " + vrijeme.Substring(34, 5) + " PM";
                        if (choosenParking.Capacity == choosenParking.NumTakenSpaces)
                        {
                            statusParkinga.Text = "Zauzet";
                            //statusParkinga.Foreground = CRVENA
                        }
                        else
                        {
                            statusParkinga.Text = "Slobodan";
                            //statusParkinga.Foreground = 0081AF;
                        }
                        break;
                    }
                }
                //Debug.WriteLine(kliknuti.Name + kliknuti.Id);
                /*cijenaPoSatu.Text = Convert.ToString(choosenParking.Price);
                parkingZona.Text = Convert.ToString(choosenParking.Zone);
                lokacija.Text = Convert.ToString(choosenParking.Address);
                radnoVrijeme.Text = Convert.ToString(choosenParking.WorkingHours);
                if (choosenParking.Capacity == choosenParking.NumTakenSpaces)
                    statusParkinga.Text = "Zauzet";
                else statusParkinga.Text = "Slobodan";*/
            }
            UserViewModel.ClickedParking = choosenParking;
        }

        private void pretrazi(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (pretraga.QueryText == "")
            {
                myMap.ZoomLevel = 14;
                getUserLocation();
            }
            else
            {
                //pretraga.QueryText;
                foreach (Parking p in Parkings)
                {
                    if (p.Name.ToLower().Contains(pretraga.QueryText.ToLower()))
                    {
                        myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = p.CoordX, Longitude = p.CoordY });
                        myMap.ZoomLevel = 16;
                        break;
                    }
                }
            }
        }
    }
}
