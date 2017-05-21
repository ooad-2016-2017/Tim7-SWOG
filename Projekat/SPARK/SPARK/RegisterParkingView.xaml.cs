using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterParkingView : Page
    {
        public RegisterParkingView()
        {
            this.InitializeComponent();
            myMap.MapServiceToken = "laUq6i4377dfOIVIHYEI~T_8vyIA3sznxgRSix8_JFw~AvZvd6to90gmNls6DvTMFLuiu_ekbhwYin_dmDs9lqGpvqgeaCCf6mtqNdXkVKmP";

        }

        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 43.865, Longitude = 18.4131 });
            myMap.ZoomLevel = 14;
        }

        private void SubmitButton_Loaded(object sender, RoutedEventArgs e)
        {
            SubmitButton.Width = this.ActualWidth;
        }



        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SPARK.Model.SPARKDbContext())
            {
                var parking = new SPARK.Model.Parking
                {
                    Name = TextBoxName.Text,
                    Zone = Convert.ToInt16(TextBoxParkingZone.Text),
                    Price = Convert.ToDouble(TextBoxPrice.Text),
                    Address = TextBoxAddress.Text,
                    WorkingHours = new KeyValuePair<DateTime, DateTime>(Convert.ToDateTime("1.1.2017"), Convert.ToDateTime("5.2.2017"))
            };
                db.Parkings.Add(parking);
                db.SaveChanges();

                TextBoxName.Text = string.Empty;
                TextBoxParkingZone.Text = string.Empty;
                TextBoxPrice.Text = string.Empty;
                TextBoxAddress.Text = string.Empty;


            }
        }

        private void PickLocationButton_Loaded(object sender, RoutedEventArgs e)
        {
            PickLocationButton.Width = this.ActualWidth;
        }
        private void ReturnToMainPage()
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog("Uspješno registrovan parking. Povratak na početnu stranicu.");

            messageDialog.Commands.Add(new UICommand("Yes", (command) =>
            {
                Frame rootFrame = Window.Current.Content as Frame;
                Frame.Navigate(typeof(RegistrationDetailsView));
            }));
        }
    }
}
