using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using M2EMobile.Models;
using M2EMobile.Models.Binding;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataResponse;
using M2EMobile.Services;
using M2EMobile.SSO;
using M2EMobile.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using Page = Xamarin.Forms.Page;

namespace M2EMobile.Views.Pages
{
    public class UserDetailPage:ContentPage
    {
        UserDetailPageBinding _userDetailPageData = new UserDetailPageBinding();
        private  Image userImage;
        public  Page GetAccountsPage()
        {
            var layout = new StackLayout();            
            _userDetailPageData.FirstName = "First Name : loading...";
            _userDetailPageData.LastName = "Last Name : loading...";
            _userDetailPageData.Reputation = "Reputation : loading...";
            _userDetailPageData.TotalEarning = "TotalEarning : loading...";
            _userDetailPageData.UserProfilePicImageSource = "";
            FetchUserDetailFromServer();


            userImage = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BindingContext = _userDetailPageData,
                WidthRequest = 200,
                HeightRequest = 200,
            };
            userImage.SetBinding(Image.SourceProperty, "UserProfilePicImageSource");
            //userImage.Source = ImageSource.FromUri(new Uri("http://i.imgur.com/Y5DauNCm.jpg"));

            var firstName = new Label
            {                
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BindingContext = _userDetailPageData
            };
            firstName.SetBinding(Label.TextProperty,"FirstName");

            var lastName = new Label
            {                
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BindingContext = _userDetailPageData
            };
            lastName.SetBinding(Label.TextProperty, "LastName");

            var reputationName = new Label
            {               
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BindingContext = _userDetailPageData
            };
            reputationName.SetBinding(Label.TextProperty, "Reputation");

            var totalEarning = new Label
            {                
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BindingContext = _userDetailPageData
            };
            totalEarning.SetBinding(Label.TextProperty, "TotalEarning");

            var logoutButton = new Button
            {
                Text = "Logout",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            logoutButton.Clicked += logoutButton_Clicked;

            layout.Children.Add(userImage);
            layout.Children.Add(firstName);
            layout.Children.Add(lastName);
            layout.Children.Add(reputationName);
            layout.Children.Add(totalEarning);
            layout.Children.Add(logoutButton);
            return new ContentPage { Content = layout };
        }

        protected async void logoutButton_Clicked(object sender, EventArgs e)
        {
            App.Database.DeleteItems();
            await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
        }
        
        protected async void FetchUserDetailFromServer()
        {
            
            Task<String> userDetailAsync =new UserDetailService().GetUserDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<ClientDetailsModel>>(userDetailString);
            int len = userDetailString.Length;
            _userDetailPageData.FirstName = "First Name : "+userDetail.Payload.FirstName;
            _userDetailPageData.LastName = "Last Name : "+userDetail.Payload.LastName;
            _userDetailPageData.Reputation = "Reputation : "+userDetail.Payload.totalReputation;
            _userDetailPageData.TotalEarning = "TotalEarning : "+userDetail.Payload.availableBalance;
            _userDetailPageData.UserProfilePicImageSource = ImageSource.FromUri(new Uri(userDetail.Payload.imageUrl));
        }
    }
}
