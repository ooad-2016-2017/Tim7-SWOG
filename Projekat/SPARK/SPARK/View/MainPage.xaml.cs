using SPARK.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            myMap.MapServiceToken = "laUq6i4377dfOIVIHYEI~T_8vyIA3sznxgRSix8_JFw~AvZvd6to90gmNls6DvTMFLuiu_ekbhwYin_dmDs9lqGpvqgeaCCf6mtqNdXkVKmP";
        }
        private void myMap_Loaded(object sender, RoutedEventArgs e)
        {
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 43.865, Longitude = 18.4131 });
            myMap.ZoomLevel = 14;
        }

        private void SeekParkingButton_Loaded(object sender, RoutedEventArgs e)
        {
            SeekParkingButton.Width = this.ActualWidth;
        }

        private void LoginButton_Loaded(object sender, RoutedEventArgs e)
        {
            LoginButton.Width = this.ActualWidth;
        }

        private void RegisterButton_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterButton.Width = this.ActualWidth;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(RegistrationTypeView));
//            Frame.Navigate(typeof(RegisterParkingView)); ako zelite testirati registraciju parkinga  onda samo ovu liniju stavite umjesto ove iznad
        }


        private void PotvrdiButton_Loaded(object sender, RoutedEventArgs e)
        {
            PotvrdiButton.Width = this.ActualWidth;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) { 
            //Frame rootFrame = Window.Current.Content as Frame;
            //Frame.Navigate(typeof(RegisterParkingView));
            ppup.IsOpen = true;
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LoginButton.Width = this.ActualWidth;
            RegisterButton.Width = this.ActualWidth;
            SeekParkingButton.Width = this.ActualWidth;
        }

        private void SeekParkingButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(UserView), new Tuple<int, int>(-1, -1));
            
        }

        private async void PotvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            String Username = TextBoxUsername1.Text;
            String Password = TextBoxPassword1.Text;
            using (var db = new SPARKDbContext())
            {
                var o = db.Owner
                        .Where(b => b.Username == Username)
                        .FirstOrDefault();
                var u = db.User
                    .Where(b => b.Username == Username)
                    .FirstOrDefault();
                if (o != null && o.Password == Password)
                {
                    Frame rootFrame = Window.Current.Content as Frame;
                    Frame.Navigate(typeof(UserView), new Tuple<int, int>(1, o.Id));
                }   
                else if (u != null && u.Password == Password)
                {
                    Frame rootFrame = Window.Current.Content as Frame;
                    Frame.Navigate(typeof(UserView), new Tuple<int, int>(0, u.Id));
                }
                else
                {
                    var dialog = new MessageDialog("Pogresni podaci");
                    dialog.Commands.Add(new UICommand { Label = "Ok" });
                    await dialog.ShowAsync();
                }
            }
        }
    }
}
