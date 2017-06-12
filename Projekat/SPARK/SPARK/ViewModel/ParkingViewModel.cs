using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPARK.Model;
using SPARK.Helper;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;

namespace SPARK.ViewModel
{
    class ParkingViewModel
    {
        public static Parking p { get; set; }
        public INavigationService NavigationService { get; set; }
        //Komande koje realiziraju Binding UnosKartice i DodavanjeKomponent
        public ICommand FillWithUserData { get; set; }
        String IDparkinga;

        public ParkingViewModel(String id)
        {
            IDparkinga = id;
            LoadParking();

            NavigationService = new NavigationService();
            FillWithUserData = new RelayCommand<object>(dodavanjeKomponente, showUserData);


        }

        public bool showUserData(object parameter)
        {
            return true;
        }

        public void dodavanjeKomponente(object parameter)
        {
            //NavigationService.Navigate(typeof(RegistrationDetailsView), user);
            NavigationService.GoBack();
        }

        public async void LoadParking()
        {
            try
            {
                using (var db = new SPARK.Model.SPARKDbContext())
                {

                    IMobileServiceTable<Azure.Parking> userTableObj = App.MobileService.GetTable<Azure.Parking>();
                    List<Azure.Parking> lista = await App.MobileService.GetTable<Azure.Parking>().ToListAsync();
                    Azure.Parking u = lista.Find(x => x.id == IDparkinga);
                    p = new Parking();
                    p.Address = u.Address;
                    p.Name = u.Name;
                    p.Id = Convert.ToInt32(u.id);
                    p.Zone = u.Zone;
                    p.Price = u.Price;
                    p.CoordX = u.CoordX;
                    p.CoordY = u.CoordY;
                    p.Capacity = u.Capacity;
                    p.NumTakenSpaces = u.NumTakenSpaces;
                    p.WorkingHours = new KeyValuePair<DateTime, DateTime>(Convert.ToDateTime(u.WorkingFrom), Convert.ToDateTime(u.WorkingTo));
                    p.TodaysProfit = u.TodaysProfit;
                    p.NumTakenSpaces = u.NumTakenSpaces;
                    p.MonthlyProfit = u.MonthlyProfit;
                    p.NumTakenSpaces = u.NumTakenSpaces;
                    p.MinCredits = Convert.ToInt32(u.MinCredits);
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
