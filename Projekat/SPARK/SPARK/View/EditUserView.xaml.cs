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
using Microsoft.WindowsAzure.MobileServices;

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
            TextBoxUsername.IsReadOnly = true;
        }
        public void Show()
        {
            this.Show();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Validator.RegistrationDetailsEmpty(TextBoxName.Text.ToString(), TextBoxSurname.Text.ToString(), TextBoxPassword.Password.ToString(), TextBoxUsername.Text.ToString(), TextBoxEmail.Text.ToString()))
            {
                throw new IOException("Nijedno polje ne smije biti prazno!");
            }
            if (!Model.Validator.EmailIsValid(TextBoxEmail.Text.ToString())) throw new IOException("Neispravan Email format!");
            try
            {
                using (var db = new SPARKDbContext())
                {
                    List<Azure.User> lista = await App.MobileService.GetTable<Azure.User>().ToListAsync();
                    List<Azure.Owner> listaVlasnika = await App.MobileService.GetTable<Azure.Owner>().ToListAsync();
                    var user = new User();
                    if (UserView.userType == 0)
                    {
                        IMobileServiceTable<Azure.User> userTableObj = App.MobileService.GetTable<Azure.User>();
                        
                        try
                        {
                            
                            Azure.User obj = lista.Find(x => x.id == Convert.ToString(UserView.userID));
                        //    await userTableObj.DeleteAsync(obj);
                            obj.Name = TextBoxName.Text.ToString();
                            obj.Surname = TextBoxSurname.Text.ToString();
                            obj.id = Convert.ToString(UserView.userID);
                            obj.Username = TextBoxUsername.Text.ToString();
                            obj.Password = TextBoxPassword.Password.ToString();
                            obj.Email = TextBoxEmail.Text.ToString();
                            await userTableObj.UpdateAsync(obj);
                            MessageDialog msgDialog = new MessageDialog("Uspješno ste ažurirali svoj profil.");
                            await msgDialog.ShowAsync();
                        }
                        catch (IOException ex)
                        {
                            MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                            await msgDialogError.ShowAsync();
                        }
                    }
                    else if (UserView.userType == 1)
                    {
                        IMobileServiceTable<Azure.Owner> userTableObj = App.MobileService.GetTable<Azure.Owner>();
                        try
                        {
                            Azure.Owner obj = listaVlasnika.Find(x => x.id == Convert.ToString(UserView.userID));
                         //   await userTableObj.DeleteAsync(obj);
                            obj.Name = TextBoxName.Text.ToString();
                            obj.Surname = TextBoxSurname.Text.ToString();
                            obj.id = Convert.ToString(UserView.userID);
                            obj.Username = TextBoxUsername.Text.ToString();
                            obj.Password = TextBoxPassword.Password.ToString();
                            obj.Email = TextBoxEmail.Text.ToString();
                            await userTableObj.UpdateAsync(obj);

                            MessageDialog msgDialog = new MessageDialog("Uspješno ste ažurirali svoj profil.");
                            await msgDialog.ShowAsync();
                        }
                        catch (IOException ex)
                        {
                            MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                            await msgDialogError.ShowAsync();
                        }
                    }
                }
            }catch(IOException ee)
            {
                MessageDialog msgDialogError = new MessageDialog("Greška : " + ee.Message);
                await msgDialogError.ShowAsync();
            }
          /*  using (var db = new SPARKDbContext())
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
            }*/




            var dialog = new MessageDialog("Vaš korisnički račun je uspješno izmijenjen!");
            dialog.Commands.Add(new UICommand { Label = "Ok" });
            await dialog.ShowAsync();
        }


    }
}
