using System;
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
using Windows.Storage;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
    public sealed partial class BookParkingView : Page
    {

        Model.Parking p;
        public BookParkingView()
        {
            p = UserView.choosenParking;
            this.InitializeComponent();
       
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            Frame.Navigate(typeof(UserView));
        }

        private async void BookParkingButton_Click(object sender, RoutedEventArgs e)
        {

             
            if (p != null)
            {
                int userId = UserView.userID;
                int userType = UserView.userType;

                DateTime odD = OdDate.Date.Date;
                DateTime doD = DoDate.Date.Date;

                double hours = (doD - odD).TotalHours + (DoTime.Time - OdTime.Time).TotalHours;
                double valuta = p.Price;
                try
                {
                    if (userType == 0)
                    {
                        List<Azure.User> lista = await App.MobileService.GetTable<Azure.User>().ToListAsync();
                        Azure.User u = lista.Find(x => x.id == Convert.ToString(userId));
                        if (Convert.ToDouble(u.stanje_kredita) < hours * valuta) throw new IOException("Nemate dovoljno kredita!");
                        else
                        {
                            u.stanje_kredita = Convert.ToString(Convert.ToDouble(u.stanje_kredita) - hours * valuta);
                        }
                        IMobileServiceTable<Azure.User> userTableObj = App.MobileService.GetTable<Azure.User>();
                        await userTableObj.UpdateAsync(u);
                    }
                    else
                    {
                        List<Azure.Owner> listaVlasnika = await App.MobileService.GetTable<Azure.Owner>().ToListAsync();
                        Azure.Owner u = listaVlasnika.Find(x => x.id == Convert.ToString(userId));
                        if (Convert.ToDouble(u.stanje_kredita) < hours * valuta) throw new IOException("Nemate dovoljno kredita!");
                        else
                        {
                            u.stanje_kredita = Convert.ToString(Convert.ToDouble(u.stanje_kredita) - hours * valuta);
                        }
                        IMobileServiceTable<Azure.Owner> ownerTableObj = App.MobileService.GetTable<Azure.Owner>();
                        await ownerTableObj.UpdateAsync(u);
                    }
                    IMobileServiceTable<Azure.Parking> parkingTableObj = App.MobileService.GetTable<Azure.Parking>();
                    List<Azure.Parking> listaParkinga = await App.MobileService.GetTable<Azure.Parking>().ToListAsync();
                    Azure.Parking i = listaParkinga.Find(x => x.id == Convert.ToString(p.Id));
                    i.MonthlyProfit = i.MonthlyProfit + hours * valuta;
                    i.TodaysProfit = i.TodaysProfit + hours * valuta;
                    i.NumTakenSpaces = i.NumTakenSpaces + 1;
                    await parkingTableObj.UpdateAsync(i);


                }
                catch (IOException ex)
                {
                    MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                    await msgDialogError.ShowAsync();
                }
            }
        }

        private void OdDate_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            if (p != null)
            {
                DateTime odD = OdDate.Date.Date;
                DateTime doD = DoDate.Date.Date;

                double hours = (doD - odD).TotalHours + (DoTime.Time - OdTime.Time).TotalHours;
                double valuta = p.Price;
                numberOfCredits.Text = valuta * hours + " SPARK KREDITA";
            }
        }


        private void OdTime_TimeChanged(object sender, TimePickerValueChangedEventArgs e)
        {
            if (p != null)
            {
                DateTime odD = OdDate.Date.Date;
                DateTime doD = DoDate.Date.Date;

                double hours = (doD - odD).TotalHours + (DoTime.Time - OdTime.Time).TotalHours;
                double valuta = p.Price;
                numberOfCredits.Text = valuta * hours + " SPARK KREDITA";
            }
        }
    }
}
