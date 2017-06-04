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
using SPARK.ViewModel;
using SPARK.Model;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.Popups;
using Microsoft.Data.Entity;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditUserView : Page
    {

        public EditUserView()
        {
            DataContext = new UserViewModel();
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
            NavigationCacheMode = NavigationCacheMode.Required;
            this.InitializeComponent();
        }
        public void Show()
        {
            this.Show();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new SPARKDbContext())
            {
                int trazeni_id = 6;
                var u = db.User.Where(b => b.Id == trazeni_id).FirstOrDefault();
                db.Database.ExecuteSqlCommand("delete from User where id=" + trazeni_id.ToString());
                u.Name = TextBoxName.Text;
                u.Surname = TextBoxSurname.Text;
                u.Password = TextBoxPassword.Text;
                u.Username = TextBoxUsername.Text;
                u.Email = TextBoxEmail.Text;

                db.User.Add(u);
                db.SaveChanges();
            }


            var dialog = new MessageDialog("Vaš korisnički račun je uspješno izmijenjen!");
            dialog.Commands.Add(new UICommand { Label = "Ok" });
            await dialog.ShowAsync();
        }


    }
}
