﻿using SPARK.Model;
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
            //isUser = true;
            this.InitializeComponent();
        }
        public void Show()
        {
            this.Show();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            isUser = (bool)e.Parameter;
            this.InitializeComponent();
        }

        private void SubmitButton_Loaded(object sender, RoutedEventArgs e)
        {
            SubmitButton.Width = this.ActualWidth;
        }



        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new SPARKDbContext())
            {
                var user = new User();
                if (isUser)
                {
                    user = new User
                    {
                        Name = TextBoxName.Text.ToString(),
                        Surname = TextBoxSurname.Text.ToString(),
                        Username = TextBoxUsername.Text.ToString(),
                        Password = TextBoxPassword.Text.ToString(),
                        Email = TextBoxEmail.Text.ToString()
                    };
                }
                else
                {
                    user = new Owner
                    {
                        Name = TextBoxName.Text.ToString(),
                        Surname = TextBoxSurname.Text.ToString(),
                        Username = TextBoxUsername.Text.ToString(),
                        Password = TextBoxPassword.Text.ToString(),
                        Email = TextBoxEmail.Text.ToString()
                    };
                }

                db.User.Add(user);
                db.SaveChanges();
            }
            TextBoxName.Text = string.Empty;
            TextBoxSurname.Text = string.Empty;
            TextBoxUsername.Text = string.Empty;
            TextBoxPassword.Text = string.Empty;
            TextBoxEmail.Text = string.Empty;

            var dialog = new MessageDialog("Vaš korisnički račun je uspješno kreiran!");
            dialog.Commands.Add(new UICommand { Label = "Ok" });
            await dialog.ShowAsync();
        }
    }
}
