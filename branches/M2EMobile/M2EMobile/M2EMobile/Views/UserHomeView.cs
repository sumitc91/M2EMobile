using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.ViewModels;
using Xamarin.Forms;

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
            if (LoginViewModel.ShouldShowLogin(App.LastUseTime))
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
        }
    }
}
