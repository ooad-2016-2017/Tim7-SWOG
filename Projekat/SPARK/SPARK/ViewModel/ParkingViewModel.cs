using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPARK.Model;
using SPARK.Helper;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SPARK.ViewModel
{
    class ParkingViewModel
    {
        Parking p { get; set; }
        public INavigationService NavigationService { get; set; }
        //Komande koje realiziraju Binding UnosKartice i DodavanjeKomponent
        public ICommand FillWithUserData { get; set; }

        public ParkingViewModel()
        {

            using (var db = new SPARKDbContext())
            {
                int trazeni_id = 1;
                var u = db.Parkings.Where(b => b.Id == trazeni_id).FirstOrDefault();
                p = new Parking();
                p.Address = u.Address;
                p.Name = u.Name;
                p.Id = u.Id;
                p.Zone = u.Zone;
                p.Price = u.Price;
                p.CoordX = u.CoordX;
                p.CoordY = u.CoordY;
                p.Capacity = u.Capacity;
                p.NumTakenSpaces = u.NumTakenSpaces;
                p.WorkingHours = u.WorkingHours;
                p.TodaysProfit = u.TodaysProfit;
                p.NumTakenSpaces = u.NumTakenSpaces;
                p.MonthlyProfit = u.MonthlyProfit;
                p.NumTakenSpaces = u.NumTakenSpaces;
                p.MinCredits = u.MinCredits;

                NavigationService = new NavigationService();
                FillWithUserData = new RelayCommand<object>(dodavanjeKomponente, showUserData);

            }
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

    }
}
