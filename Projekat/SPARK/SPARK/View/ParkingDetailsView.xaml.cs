using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SPARK.Model;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Storage.Streams;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ParkingDetailsView : Page
    {
        public Parking p = new Parking();
        public ParkingDetailsView()
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
            this.InitializeComponent();
            myMap.MapServiceToken = "laUq6i4377dfOIVIHYEI~T_8vyIA3sznxgRSix8_JFw~AvZvd6to90gmNls6DvTMFLuiu_ekbhwYin_dmDs9lqGpvqgeaCCf6mtqNdXkVKmP";

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            p = e.Parameter as Parking;
            cijenaPoSatu.Text = Convert.ToString(p.Price);
            parkingZona.Text = Convert.ToString(p.Zone);
            lokacija.Text = Convert.ToString(p.Address);
            string vrijeme = Convert.ToString(p.WorkingHours);
            radnoVrijeme.Text = vrijeme.Substring(11, 5) + " AM - " + vrijeme.Substring(34, 5) + " PM";
            if (p.Capacity == p.NumTakenSpaces)
            {
                statusParkinga.Text = "Zauzet";
                //statusParkinga.Foreground = CRVENA
            }
            else
            {
                statusParkinga.Text = "Slobodan";
                //statusParkinga.Foreground = 0081AF;
            }
            Run run = new Run();
            run.Text = Convert.ToString(p.Capacity-p.NumTakenSpaces);
            brojSlobodnihMjesta.Inlines.Add(run);

            Run run1 = new Run();
            run1.Text = Convert.ToString(p.MonthlyProfit);
            mjesecnaZarada.Inlines.Add(run1);

            Run run2 = new Run();
            run2.Text = Convert.ToString(p.TodaysProfit);
            dnevnaZarada.Inlines.Add(run2);
        }
        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 43.865, Longitude = 18.4131 });
            myMap.ZoomLevel = 14;
            nadjiParking();
        }
        private void nadjiParking()
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = p.CoordX, Longitude = p.CoordY });
            myMap.ZoomLevel = 16;

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
        private void urediParking_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            //Frame.Navigate(typeof(EditParkingView));
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
        private void izmjena_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            EditParkingView.IDparkinga = Convert.ToString(p.Id);
            ViewModel.ParkingViewModel.p = this.p;
            Frame.Navigate(typeof(View.EditParkingView));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(View.EditUserView));
        }
    }
}
