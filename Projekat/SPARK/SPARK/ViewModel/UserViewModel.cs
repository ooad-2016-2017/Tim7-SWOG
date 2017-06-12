using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPARK.Model;
using System.Windows.Input;
using SPARK.Helper;

namespace SPARK.ViewModel
{
     class UserViewModel {

        public static User korisnik { get; set; }
        //Servis za navigaciju koji će preći na druge forme po potrebi
        public INavigationService NavigationService { get; set; }
        //Komande koje realiziraju Binding UnosKartice i DodavanjeKomponent
        public ICommand FillWithUserData { get; set; }
        public static List<Parking> Parkings { get; set; }
        public static Parking ClickedParking { get; set; }
        
        public UserViewModel()
        {
            LoadaData();
            ClickedParking = null;
            
        /*       using (var db = new SPARKDbContext()) {

                   int trazeni_id = UserView.userID;
                   if (trazeni_id != -1)
                   {

                       var u = db.User.Where(b => b.Id == trazeni_id).FirstOrDefault();
                       if (u != null)
                       {
                           korisnik = new User();
                           korisnik.Name = u.Name;
                           korisnik.Surname = u.Surname;
                           korisnik.Id = u.Id;
                           korisnik.Password = u.Password;
                           korisnik.Username = u.Username;
                           korisnik.Email = u.Email;
                       }

                   }
                   Parkings.Add(db.Parkings.Where(p => p.Id==1).FirstOrDefault()); //= db.Parkings.ToList<Parking>();
               }*/
            NavigationService = new NavigationService();
            FillWithUserData = new RelayCommand<object>(dodavanjeKomponente, showUserData);
        }
        public bool showUserData(object parameter)
        {

            return true;
        }
        public void dodavanjeKomponente(object parameter)
        {
            NavigationService.GoBack();

        }

        private async void LoadaData()
        {
           if (UserView.userType == 0)
            {
                List<Azure.User> lista = await App.MobileService.GetTable<Azure.User>().Where(x => x.id == Convert.ToString(UserView.userID)).ToListAsync();
                if(lista.Count != 0)
                {
                    Azure.User u = new Azure.User();
                    u = lista.Find(x => x.id== Convert.ToString(UserView.userID));
                    korisnik = new User();
                    korisnik.Name = u.Name;
                    korisnik.Surname = u.Surname;
                    korisnik.Id = Convert.ToInt32(u.id);
                    korisnik.Password = u.Password;
                    korisnik.Username = u.Username;
                    korisnik.Email = u.Email;
                }
            }
            else if (UserView.userType == 1)
            {
                List<Azure.Owner> listaVlasnika = await App.MobileService.GetTable<Azure.Owner>().Where(x => x.id == Convert.ToString(UserView.userID)).ToListAsync();
                if (listaVlasnika.Count != 0)
                {
                    Azure.Owner u = new Azure.Owner();
                    u = listaVlasnika.Find(x => x.id == Convert.ToString(UserView.userID));
                    korisnik = new Owner();
                    korisnik.Name = u.Name;
                    korisnik.Surname = u.Surname;
                    korisnik.Id = Convert.ToInt32(u.id);
                    korisnik.Password = u.Password;
                    korisnik.Username = u.Username;
                    korisnik.Email = u.Email;

                    List<Azure.Parking> listaParkinga = await App.MobileService.GetTable<Azure.Parking>().Where(x => x.id_vlasnika == (UserView.userID)).ToListAsync();
                    if(listaParkinga.Count != 0)
                    {
                        Parkings = new List<Parking>();

                        foreach (Azure.Parking p in listaParkinga)
                        {
                            Parking novi = new Parking();
                            novi.Id = Convert.ToInt32(p.id);
                            novi.Name = p.Name;
                            novi.Address = p.Address;
                            novi.Capacity = p.Capacity;
                            novi.CoordX = p.CoordX;
                            novi.CoordY = p.CoordY;
                            novi.MinCredits = Convert.ToInt32(p.MinCredits);
                            novi.MinCredits = Convert.ToInt32(p.MonthlyProfit);
                            novi.NumTakenSpaces = p.NumTakenSpaces;
                            novi.Price= Convert.ToInt32(p.Price);
                            novi.MonthlyProfit= Convert.ToInt32(p.MonthlyProfit);
                            novi.TodaysProfit= Convert.ToInt32(p.TodaysProfit);
                            novi.WorkingHours = new KeyValuePair<DateTime, DateTime>(Convert.ToDateTime(p.WorkingFrom), Convert.ToDateTime(p.WorkingTo));
                            novi.Zone = p.Zone;
                            Parkings.Add(novi);
                        }
                    }
                }
            }
        }



    }
}
