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
        //U view Biniding Korpa.Stavke prvo trazi Korpa propertz a zatim pristupa njegovom Stavke property
        public User korisnik { get; set; }
        //Servis za navigaciju koji će preći na druge forme po potrebi
        public INavigationService NavigationService { get; set; }
        //Komande koje realiziraju Binding UnosKartice i DodavanjeKomponent
        public ICommand FillWithUserData { get; set; }
        public List<Parking> Parkings { get; set; }

        public UserViewModel()
        {
            Parkings = new List<Parking>();
            using (var db = new SPARKDbContext()) {
                int trazeni_id = 6;
                var u = db.User.Where(b => b.Id == trazeni_id).FirstOrDefault();
                korisnik = new User();
                korisnik.Name = u.Name;
                korisnik.Surname = u.Surname;
                korisnik.Id = u.Id;
                korisnik.Password = u.Password;
                korisnik.Username = u.Username;
                korisnik.Email = u.Email;

                Parkings.Add(db.Parkings.Where(p => p.Id==1).FirstOrDefault()); //= db.Parkings.ToList<Parking>();
            }
            
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



    }
}
