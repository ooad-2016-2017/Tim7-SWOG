﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationWindow : Page
    {
        public RegistrationWindow()
        {
            this.InitializeComponent();
        }

        public void Show()
        {
            this.Show();
        }

        private void StackPanelRegistration_Loaded(object sender, RoutedEventArgs e)
        {
            //   StackPanelRegistration.Width = this.ActualWidth - 100;
            //   StackPanelRegistration.Height = this.ActualHeight;
        }

        private void RegistrationDetailsButton_Loaded(object sender, RoutedEventArgs e)
        {
            RegistrationDetailsButton.Width = this.ActualWidth;
        }



        private void RegistrationDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(RegistrationWindowDetails));
        }
    }
}