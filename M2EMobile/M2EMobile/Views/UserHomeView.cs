using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataWrapper;
using M2EMobile.SSO;
using M2EMobile.ViewModels;
using Newtonsoft.Json;
using RestSharp.Portable;
using Xamarin.Forms;
using WebRequest = System.Net.WebRequest;

namespace M2EMobile.Views
{
    class UserHomeView : ContentPage
    {

        public UserHomeView()
        {
            Content = new Label
            {
                Text = "Hello,New Forms !",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Task<String> response = new M2ESSOClient().HttpPostLoginAsync("", "", "", "http://www.cautom.com/Auth/Login");
            String result = await response;
            int len = result.Length;
            
            if (LoginViewModel.ShouldShowLogin(App.LastUseTime))
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
        }

    }
}
