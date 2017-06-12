using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace SPARK.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditParkingView : Page
    {
        public static String IDparkinga;

        public EditParkingView()
        {

            DataContext = new ViewModel.ParkingViewModel(IDparkinga);
            this.InitializeComponent();
            TextBoxAddress.IsReadOnly = true;
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new SPARK.Model.SPARKDbContext())
                {
                   
                    IMobileServiceTable<Azure.Parking> userTableObj = App.MobileService.GetTable<Azure.Parking>();

                    List<Azure.Parking> lista = await App.MobileService.GetTable<Azure.Parking>().ToListAsync();
                    try
                    {
                        Azure.Parking obj = lista.Find(x => x.id == IDparkinga);
                        obj.Name = TextBoxName.Text.ToString();
                        obj.WorkingFrom = Convert.ToString(openingTime.Time.Hours) + ":" + Convert.ToString(openingTime.Time.Minutes);
                        obj.WorkingTo = Convert.ToString(closingTime.Time.Hours) + ":" + Convert.ToString(closingTime.Time.Minutes);

                        try
                        {
                            obj.Capacity = Convert.ToInt32(TextBoxCapacity.Text.ToString());
                        }
                        catch (Exception ee)
                        {
                            throw new IOException("Neispravan kapacitet!");
                        }
                        try
                        {
                            obj.Zone = Convert.ToInt32(TextBoxParkingZone.Text);
                        }
                        catch (Exception ee)
                        {
                            throw new IOException("Neispravna zona!");
                        }
                        try
                        {
                            obj.MinCredits = Convert.ToInt32(TextBoxParkingMinCredits.Text.ToString());
                        }
                        catch (Exception ee)
                        {
                            throw new IOException("Neispravno napisan broj minimalnih kredita!");
                        }
                        try
                        {
                            obj.Price = Convert.ToDouble(TextBoxPrice.Text);
                        }
                        catch (Exception ee)
                        {
                            throw new IOException("Neispravno napisana cijena!");
                        }
                        obj.NumTakenSpaces=0;
                        obj.TodaysProfit = 0;
                        obj.MonthlyProfit = 0;
                        await userTableObj.UpdateAsync(obj);
                        MessageDialog msgDialog = new MessageDialog("Uspješno ste ažurirali parking.");
                        await msgDialog.ShowAsync();
                    }
                    catch (IOException ex)
                    {
                        MessageDialog msgDialogError = new MessageDialog("Error : " + ex.Message);
                        await msgDialogError.ShowAsync();
                    }

                    TextBoxName.Text = string.Empty;
                    TextBoxParkingZone.Text = string.Empty;
                    TextBoxPrice.Text = string.Empty;
                    TextBoxAddress.Text = string.Empty;
                    TextBoxParkingMinCredits.Text = string.Empty;
                    TextBoxCapacity.Text = string.Empty;
                }
            }
            catch (Exception izuzetak)
            {
                var dialog1 = new MessageDialog(izuzetak.Message);
                dialog1.Commands.Add(new UICommand { Label = "Ok" });
                await dialog1.ShowAsync();
            }
        }

    }
}
