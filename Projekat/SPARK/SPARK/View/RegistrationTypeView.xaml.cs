using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationTypeView : Page
    {
        public RegistrationTypeView()
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
        }
        public void Show()
        {
            this.Show();
        }


        private void RegistrationDetailsButton_Loaded(object sender, RoutedEventArgs e)
        {
            RegistrationDetailsButton.Width = this.ActualWidth;
        }

        private async void RegistrationDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            bool x = (bool)CreateUser.IsChecked;
            if (CreateUser.IsChecked == false && CreateOwner.IsChecked == false)
            {
                var dialog = new MessageDialog("Niste odabrali nijednu od ponuđenih opcija");
                dialog.Commands.Add(new UICommand { Label = "Ok" });
                await dialog.ShowAsync();
            }
            else
            {
                Frame rootFrame = Window.Current.Content as Frame;
                Frame.Navigate(typeof(RegistrationDetailsView), x);
            }
        }
    }
}
