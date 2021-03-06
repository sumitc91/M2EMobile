﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.DataResponse;
using M2EMobile.Views;
using M2EMobile.Views.Pages;
using Newtonsoft.Json;
using Xamarin.Forms;
using Button = Xamarin.Forms.Button;
using Image = Xamarin.Forms.Image;

namespace M2EMobile.ViewModels
{
    public class LoginView : ContentPage
    {
        private LoginViewModel loginViewModel;

        private LoginViewModel Model
        {
            get
            {
                if (loginViewModel == null)
                    loginViewModel = new LoginViewModel(App.Service);

                return loginViewModel;
            }
        }

        public LoginView()
        {
            BindingContext = Model;
            BackgroundColor = Color.FromRgb(173, 216, 230);

            var logo = new Image
            {
                Source = ImageSource.FromResource("M2EMobile.Resources.Images.cautom_logo.png"),
                HeightRequest = 33,
                WidthRequest = 186
            };
                        
            
            var usernameEntry = new Entry { Placeholder = "Username",TextColor = Color.Gray};
            usernameEntry.SetBinding(Entry.TextProperty, "Username");

            var passwordEntry = new Entry { IsPassword = true, Placeholder = "Password", TextColor = Color.Gray };
            passwordEntry.SetBinding(Entry.TextProperty, "Password");

            var loginButton = new Button { Text = "Login" };
            loginButton.Clicked += OnLoginClicked;

            var helpButton = new Button { Text = "Help" };
            helpButton.Clicked += OnHelpClicked;

            var facebookLoginButton = new Button {Text = "Facebook"};
            facebookLoginButton.Clicked += facebookLoginButton_Clicked;

            var grid = new Grid()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            if (Device.OS == TargetPlatform.iOS)
            {
                grid.Children.Add(loginButton, 0, 0);
                grid.Children.Add(helpButton, 1, 0);

                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Padding = new Thickness(30),
                    Children = { logo, usernameEntry, passwordEntry, grid }
                };

                BackgroundImage = "login_box";

            }
            else
            {
                grid.Children.Add(logo, 0, 0);
                grid.Children.Add(helpButton, 1, 0);

                Content = new StackLayout()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(30),
                    Children = { grid, usernameEntry, passwordEntry, loginButton, facebookLoginButton },
                };
            }

        }

        async void facebookLoginButton_Clicked(object sender, EventArgs e)
        {
            //App.HardwareBackPressed = () => Task.FromResult<bool?>(true);
            new LoginPage();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.HardwareBackPressed = () => Task.FromResult<bool?>(false);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (loginViewModel.CanLogin)
            {
                Task<String> response = loginViewModel
                    .LoginAsync(loginViewModel.Username, loginViewModel.Password,
                        System.Threading.CancellationToken.None);
                String userData = await response;
                
                var res = App.Database.GetItems();
                                
                //var resAll = App.Database.GetItems();
                //Navigation.PushModalAsync(new UserHomeView());
                var authInfo = JsonConvert.DeserializeObject<ResponseModel<LoginResponse>>(userData);
                if (authInfo.Status == 200)
                {
                    if (res.Count() != 1)
                    {
                        App.Database.SaveItem(
                            new TodoItem
                            {
                                Done = false,
                                IsLoggedIn = true,
                                Username = loginViewModel.Username,
                                Notes = "Notes",
                                Name = "Sumit Chourasia",
                                FirstName = "Sumit",
                                LastName = "Chourasia",
                                Password = loginViewModel.Password,
                                TokenId = authInfo.Payload.UTMZT,
                                UTMZK = authInfo.Payload.UTMZK,
                                UTMZV = authInfo.Payload.UTMZV,
                                UserType = loginViewModel.UserType
                            });
                    }
                    //await DisplayAlert("Success", "Succesfully logged in!!!", "OK", null);
                    await Navigation.PopModalAsync();
                    
                    await Navigation.PushModalAsync(new UserHomeView().GetUserHomeView());
                }
                else if (authInfo.Status == 401)
                {
                    await DisplayAlert("Unauthorized", "Username/Password Incorrect !!!", "OK", null);
                }
                else
                {
                    await DisplayAlert("Error", "Internal Server Error Occured!!!", "OK", null);
                }
                
            }
            else
            {
                await DisplayAlert("Error", loginViewModel.ValidationErrors, "OK", null);
            }
        }

        private void OnHelpClicked(object sender, EventArgs e)
        {
            DisplayAlert("Help", "Enter any username and password", "OK", null);
        }


    }
}
