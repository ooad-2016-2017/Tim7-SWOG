using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPARK.Model;
using SPARK.Helper;
using System.Windows.Input;

namespace SPARK.ViewModel
{
    class OwnerViewModel
    {
        //U view Biniding Korpa.Stavke prvo trazi Korpa propertz a zatim pristupa njegovom Stavke property
        public Owner korisnik { get; set; }
        //Servis za navigaciju koji će preći na druge forme po potrebi
        public INavigationService NavigationService { get; set; }
        //Komande koje realiziraju Binding UnosKartice i DodavanjeKomponent
        public ICommand FillWithUserData { get; set; }

        public OwnerViewModel()
        {
            using (var db = new SPARKDbContext())
            {
                int trazeni_id = 1;
                var u = db.Owner.Where(b => b.Id == trazeni_id).FirstOrDefault();
                korisnik = new Owner();
                korisnik.Name = u.Name;
                korisnik.Surname = u.Surname;
                korisnik.Id = u.Id;
                korisnik.Password = u.Password;
                korisnik.Username = u.Username;
                korisnik.Email = u.Email;
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
