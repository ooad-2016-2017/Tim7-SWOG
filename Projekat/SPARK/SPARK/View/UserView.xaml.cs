﻿using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UserView : Page
    {
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
