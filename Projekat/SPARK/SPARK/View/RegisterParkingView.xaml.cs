using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
        int userID;
        MapIcon lokacijaParkinga;
        public RegisterParkingView()
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

        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 43.865, Longitude = 18.4131 });
            myMap.ZoomLevel = 12;
            lokacijaParkinga = new MapIcon { Location = myMap.Center, NormalizedAnchorPoint = new Point(0.5, 1.0), Title = "Lokacija Vašeg Parkinga", ZIndex = 0 };
            myMap.MapElements.Add(lokacijaParkinga);
        }


        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new SPARK.Model.SPARKDbContext())
                {
                    /*  var parking = new SPARK.Model.Parking
                      {
                          Name = TextBoxName.Text,
                          Zone = Convert.ToInt16(TextBoxParkingZone.Text),
                          Price = Convert.ToDouble(TextBoxPrice.Text),
                          Address = TextBoxAddress.Text,
                          //working hours ???
                          WorkingHours = new KeyValuePair<DateTime, DateTime>(Convert.ToDateTime("1.1.2017"), Convert.ToDateTime("5.2.2017")),
                          CoordX = (double)lokacijaParkinga.Location.Position.Longitude,
                          CoordY = (double)lokacijaParkinga.Location.Position.Longitude,
                      };
                      db.Parkings.Add(parking);
                      db.SaveChanges();

                      var dialog1 = new MessageDialog("Uspješno registrovan parking '" + TextBoxName.Text + "'");
                      dialog1.Commands.Add(new UICommand { Label = "Ok" });
                      await dialog1.ShowAsync();*/

                    IMobileServiceTable<Azure.Parking> userTableObj = App.MobileService.GetTable<Azure.Parking>();

                    List<Azure.Parking> lista = await App.MobileService.GetTable<Azure.Parking>().ToListAsync();
                    try
                    {
                        Azure.Parking obj = new Azure.Parking();
                        obj.Name = TextBoxName.Text.ToString();
                        obj.Zone = Convert.ToInt32(TextBoxParkingZone.Text);
                        obj.Zone = 2;
                        obj.id = Convert.ToString(lista.Count + 1); 
                        obj.Price = Convert.ToDouble(TextBoxPrice.Text);
                        obj.Address = TextBoxAddress.Text;
                        obj.WorkingFrom= Convert.ToString(openingTime.Time.Hours) + ":" + Convert.ToString(openingTime.Time.Minutes);
                        obj.WorkingTo = Convert.ToString(closingTime.Time.Hours) + ":" + Convert.ToString(closingTime.Time.Minutes);
                        obj.CoordX = (double)lokacijaParkinga.Location.Position.Longitude;
                        obj.CoordY = (double)lokacijaParkinga.Location.Position.Latitude;
                        obj.id_vlasnika = userID;
                        await userTableObj.InsertAsync(obj);
                        MessageDialog msgDialog = new MessageDialog("Uspješno ste unijeli novi parking.");
                        await msgDialog.ShowAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageDialog msgDialogError = new MessageDialog("Error : " +
                        ex.ToString());
                        // msgDialogError.ShowAsync();
                    }

                    TextBoxName.Text = string.Empty;
                    TextBoxParkingZone.Text = string.Empty;
                    TextBoxPrice.Text = string.Empty;
                    TextBoxAddress.Text = string.Empty;
                }
            }
            catch (Exception izuzetak)
            {
                var dialog1 = new MessageDialog(izuzetak.Message);
                dialog1.Commands.Add(new UICommand { Label = "Ok" });
                await dialog1.ShowAsync();
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
        private void myMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            var tappedGeoPosition = args.Location.Position;
            lokacijaParkinga.Location = new Geopoint(tappedGeoPosition);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameter= e.Parameter;
            userID = Convert.ToInt32(parameter);

        }
    }
}
