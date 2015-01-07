using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.Binding;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataResponse;
using M2EMobile.Services;
using M2EMobile.SSO;
using M2EMobile.ViewModels;
using M2EMobile.Views.Pages;
using M2EMobile.Views.User;
using Newtonsoft.Json;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

namespace M2EMobile.Views
{
    public class UserRootPage:MasterDetailPage
    {
        static MasterDetailPage MDPage;
        static UserDetailPageBinding _userDetailPageData = new UserDetailPageBinding();
        public Page GetUserRootPage ()
		{
            var masterMenuAbsLayout = new AbsoluteLayout();
            App.HardwareBackPressed = () => Task.FromResult<bool?>(false);

            _userDetailPageData.FirstName = "loading...";
            _userDetailPageData.LastName = "Last Name : loading...";
            _userDetailPageData.Reputation = "Reputation : loading...";
            _userDetailPageData.TotalEarning = "TotalEarning : loading...";
            _userDetailPageData.UserProfilePicImageSource = "";
            FetchUserDetailFromServer();
            var userImage = new Image
            {                
                BindingContext = _userDetailPageData,                
                WidthRequest = 60,
                HeightRequest = 60,
            };
            userImage.SetBinding(Image.SourceProperty, "UserProfilePicImageSource");
            //userImage.Source = ImageSource.FromUri(new Uri("http://i.imgur.com/Y5DauNCm.jpg"));

            var firstName = new Label
            {                
                BindingContext = _userDetailPageData,
                TextColor = Color.White
            };
            firstName.SetBinding(Label.TextProperty, "FirstName");
            var lastName = new Label
            {
                BindingContext = _userDetailPageData,
                TextColor = Color.White
            };
            lastName.SetBinding(Label.TextProperty, "LastName");

            masterMenuAbsLayout.Children.Add(userImage, new Point(20, 5));
            masterMenuAbsLayout.Children.Add(firstName, new Point(90, 30));
            masterMenuAbsLayout.Children.Add(lastName, new Point(90, 45));

            return MDPage = new MasterDetailPage
            {
                Master = new ContentPage
                {
                    Title = "Master",
                    BackgroundColor = Color.FromRgb(5, 99, 172),
                    Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null,
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, 50),
                        Children =
                        {
                            masterMenuAbsLayout, 
                            Link(Constants.pageName_Tasks), 
                            Link(Constants.pageName_ActiveTasks), 
                            Link(Constants.pageName_CompletedTasks),
                            Link(Constants.pageName_FacebookLike),
                            Link(Constants.pageName_Referrals),
                            Link(Constants.logoutButtonText)
                        }
                    },
                },
                Detail = new NavigationPage(new ContentPage
                {
                    Content = new Label
                    {
                        Text = "Loading please wait...",
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                    }
                })
                ,
            };
		}

        Button Link(string name)
        {
            var button = new Button
            {
                Text = name,
                TextColor = Color.Black,
                BackgroundColor = Color.White
            };
            button.Clicked += delegate
            {
                switch (name)
                {
                    case Constants.pageName_Tasks:
                        MDPage.Detail =new NavigationPage(new AllTasks().GetAllTasks());
                        MDPage.IsPresented = false;
                        break;
                    case Constants.pageName_ActiveTasks:
                        MDPage.Detail = new NavigationPage(new ActiveTasks().GetActiveTasks());
                        MDPage.IsPresented = false;
                        break;
                    case Constants.pageName_CompletedTasks:
                        MDPage.Detail = new NavigationPage(new LeadsPage());
                        MDPage.IsPresented = false;
                        break;
                    case Constants.pageName_FacebookLike:
                        MDPage.Detail = new LoginPage();
                        MDPage.IsPresented = false;
                        break;
                    case Constants.pageName_Referrals:
                        MDPage.Detail = new NavigationPage(new LeadsPage());
                        MDPage.IsPresented = false;
                        break;
                    case Constants.logoutButtonText:
                        App.Database.DeleteItems();
                        //Navigation.PopModalAsync();
                        new UserRootPage().PushAsyncModalLoginPage();
                        break;
                    default: MDPage.Detail = new NavigationPage(new OpportunitiesPage());
                        MDPage.IsPresented = false;
                        break;
                }                
               
            };
            return button;
        }
        
        public void PushAsyncModalPage(Page selectedItemPage)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await MDPage.Navigation.PushModalAsync(new NavigationPage(selectedItemPage));
            });
        }

        public void PushAsyncModalLoginPage()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                await MDPage.Navigation.PushModalAsync(new LoginView());
            });
        }

        protected async void FetchUserDetailFromServer()
        {

            Task<String> userDetailAsync = new UserDetailService().GetUserDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<ClientDetailsModel>>(userDetailString);
            int len = userDetailString.Length;
            _userDetailPageData.FirstName = userDetail.Payload.FirstName;
            _userDetailPageData.LastName = userDetail.Payload.LastName;
            _userDetailPageData.Reputation = "Reputation : " + userDetail.Payload.totalReputation;
            _userDetailPageData.TotalEarning = "TotalEarning : " + userDetail.Payload.availableBalance;
            _userDetailPageData.UserProfilePicImageSource = ImageSource.FromUri(new Uri(userDetail.Payload.imageUrl));
            
            MDPage.Detail = new NavigationPage(new AllTasks().GetAllTasks());
            MDPage.IsPresented = false;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();            
        }
    }
}
