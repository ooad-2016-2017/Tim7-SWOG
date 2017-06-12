using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class BuyCreditsView : Page
    {

        public BuyCreditsView()
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
            comboBox.Items.Add("10");
            comboBox.Items.Add("20");
            comboBox.Items.Add("30");
            comboBox.Items.Add("50");
            comboBox.Items.Add("100");
        }

        private void BackToPrevious_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(UserView));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            int userId = UserView.userID;
            int userType = UserView.userType;
            try
            {
                if (userType == 0)
                {
                    List<Azure.User> lista = await App.MobileService.GetTable<Azure.User>().ToListAsync();
                    Azure.User u = lista.Find(x => x.id == Convert.ToString(userId));
                    if (u.stanje_kredita == null) u.stanje_kredita = "0";
                    iznos.Text = u.stanje_kredita;
                }
                else
                {
                    List<Azure.Owner> listaVlasnika = await App.MobileService.GetTable<Azure.Owner>().ToListAsync();
                    Azure.Owner u = listaVlasnika.Find(x => x.id == Convert.ToString(userId));
                    if (u.stanje_kredita == null) u.stanje_kredita = "0";
                    iznos.Text = u.stanje_kredita;
                }


            }
            catch (IOException ex)
            {
                MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                await msgDialogError.ShowAsync();
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int userId = UserView.userID;
            int userType = UserView.userType;
            try
            {
                if (userType == 0)
                {
                    IMobileServiceTable<Azure.User> userTableObj = App.MobileService.GetTable<Azure.User>();
                    List<Azure.User> lista = await App.MobileService.GetTable<Azure.User>().ToListAsync();
                    Azure.User u = lista.Find(x => x.id == Convert.ToString(userId));
                    if (u.stanje_kredita == null) u.stanje_kredita = "0";
                    u.stanje_kredita =Convert.ToString( Convert.ToDouble(u.stanje_kredita) + Convert.ToDouble(comboBox.SelectedItem));
                    iznos.Text = u.stanje_kredita;
                    await userTableObj.UpdateAsync(u);
                }
                else
                {
                    IMobileServiceTable<Azure.Owner> ownerTableObj = App.MobileService.GetTable<Azure.Owner>();
                    List<Azure.Owner> listaVlasnika = await App.MobileService.GetTable<Azure.Owner>().ToListAsync();
                    Azure.Owner u = listaVlasnika.Find(x => x.id == Convert.ToString(userId));
                    if (u.stanje_kredita == null) u.stanje_kredita = "0";
                    u.stanje_kredita = Convert.ToString(Convert.ToDouble(u.stanje_kredita) + Convert.ToDouble(comboBox.SelectedItem));
                    iznos.Text = u.stanje_kredita;
                    await ownerTableObj.UpdateAsync(u);
                }
            }
            catch (IOException ex)
            {
                MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                await msgDialogError.ShowAsync();
            }



        }
    }
}
