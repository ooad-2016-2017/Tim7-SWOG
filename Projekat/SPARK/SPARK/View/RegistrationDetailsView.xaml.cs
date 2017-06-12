using SPARK.Model;
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
using SPARK.ViewModel;
using Windows.UI.Core;
using Microsoft.WindowsAzure.MobileServices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SPARK
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationDetailsView : Page
    {
        bool isUser;
        public RegistrationDetailsView()
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
            NavigationCacheMode = NavigationCacheMode.Required;
            this.InitializeComponent();
        }
        public void Show()
        {
            this.Show();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            isUser = (bool)e.Parameter;
            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            this.InitializeComponent();
        }



        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Model.Validator.RegistrationDetailsEmpty(TextBoxName.Text.ToString(), TextBoxSurname.Text.ToString(), TextBoxPassword.Password.ToString(), TextBoxUsername.Text.ToString(), TextBoxEmail.Text.ToString())){
                    throw new IOException("Nijedno polje ne smije biti prazno!");
                }
                if (!Model.Validator.EmailIsValid(TextBoxEmail.Text.ToString())) throw new IOException("Neispravan Email format!");
                using (var db = new SPARKDbContext())
                {
                    List<Azure.User> lista = await App.MobileService.GetTable<Azure.User>().ToListAsync();
                    List<Azure.Owner> listaVlasnika = await App.MobileService.GetTable<Azure.Owner>().ToListAsync();
                    var user = new User();
                    if (isUser)
                    {
                        /*   user = new User
                           {
                               Name = TextBoxName.Text.ToString(),
                               Surname = TextBoxSurname.Text.ToString(),
                               Username = TextBoxUsername.Text.ToString(),
                               Password = TextBoxPassword.Text.ToString(),
                               Email = TextBoxEmail.Text.ToString()
                           };*/

                        IMobileServiceTable<Azure.User> userTableObj = App.MobileService.GetTable<Azure.User>();

                        try
                        {
                            Azure.User obj = new Azure.User();
                            obj.Name = TextBoxName.Text.ToString();
                            obj.Surname = TextBoxSurname.Text.ToString();
                            obj.id = Convert.ToString(lista.Count+1);
                            obj.Password = TextBoxPassword.Password.ToString();
                            obj.stanje_kredita = "0";
                            obj.Username = TextBoxUsername.Text.ToString();
                            if (lista.Find(x => x.Username == obj.Username) != null || listaVlasnika.Find(x => x.Username == obj.Username) != null) throw new IOException("Korisničko ime već postoji!");
                            obj.Email = TextBoxEmail.Text.ToString();
                            await userTableObj.InsertAsync(obj);
                            MessageDialog msgDialog = new MessageDialog("Uspješno ste se registrovali na SPARK.");
                            await msgDialog.ShowAsync();
                        }
                        catch (IOException ex)
                        {
                            MessageDialog msgDialogError = new MessageDialog("Greška : " +ex.Message);
                            await msgDialogError.ShowAsync();
                        }

                    }
                    else
                    {
                        /*    user = new Owner
                            {
                                Name = TextBoxName.Text.ToString(),
                                Surname = TextBoxSurname.Text.ToString(),
                                Username = TextBoxUsername.Text.ToString(),
                                Password = TextBoxPassword.Text.ToString(),
                                Email = TextBoxEmail.Text.ToString()
                            };*/
                        

                        IMobileServiceTable<Azure.Owner> userTableObj = App.MobileService.GetTable<Azure.Owner>();
                        try
                        {
                            Azure.Owner obj = new Azure.Owner();
                            obj.Name = TextBoxName.Text.ToString();
                            obj.Surname = TextBoxSurname.Text.ToString();
                            obj.id = Convert.ToString(listaVlasnika.Count + 1);
                            obj.Password = TextBoxPassword.Password.ToString();
                            obj.Username = TextBoxUsername.Text.ToString();
                            if (lista.Find(x => x.Username == obj.Username) != null || listaVlasnika.Find(x => x.Username == obj.Username) != null) throw new IOException("Korisničko ime već postoji!");
                            obj.Email = TextBoxEmail.Text.ToString();
                            
                            await userTableObj.InsertAsync(obj);
                            MessageDialog msgDialog = new MessageDialog("Uspješno ste unijeli novog vlasnika.");
                            await msgDialog.ShowAsync();
                        }
                        catch (IOException ex)
                        {
                            MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                            await msgDialogError.ShowAsync();
                        }
                    }

                    db.User.Add(user);
                    db.SaveChanges();
                }
                TextBoxName.Text = string.Empty;
                TextBoxSurname.Text = string.Empty;
                TextBoxUsername.Text = string.Empty;
                TextBoxPassword.Password = string.Empty;
                TextBoxEmail.Text = string.Empty;


            }
            catch (IOException ex)
            {
                MessageDialog msgDialogError = new MessageDialog("Greška : " + ex.Message);
                msgDialogError.Commands.Add(new UICommand { Label = "Ok" });
                await msgDialogError.ShowAsync();

            }
        }

    }
}
