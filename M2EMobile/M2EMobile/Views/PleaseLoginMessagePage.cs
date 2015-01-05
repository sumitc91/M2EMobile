using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataResponse;
using M2EMobile.Models.DataWrapper;
using M2EMobile.SSO;
using M2EMobile.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace M2EMobile.Views
{
    public class PleaseLoginMessagePage : ContentPage
    {
        public PleaseLoginMessagePage()
        {

            var layout = new StackLayout();
            var label = new Label
            {
                Text = "Please wait...",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            var logoImage = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            logoImage.HeightRequest = 50;
            logoImage.WidthRequest = 200;
            logoImage.Source = ImageSource.FromResource("M2EMobile.Resources.Images.cautom_logo.png");
            //userImage.SetBinding(Image.SourceProperty, "UserProfilePicImageSource");
            
            layout.Children.Add(logoImage);
            layout.Children.Add(label);
            BackgroundColor = Color.White;
            //layout.Children.Add(logoutButton);
            Content = layout;
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (LoginViewModel.ShouldShowLogin(App.LastUseTime))
            {
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));   
            }                
            else
            {
                var res = App.Database.GetItems();
                var loginData = new LoginRequest
                {
                    KeepMeSignedInCheckBox = "true",                    
                    Type = "web",                    
                };
                foreach (var userInfo in res)
                {
                    loginData.UserName = userInfo.Username;
                    loginData.Password = userInfo.Password;                    
                }
                
                var postJson = JsonConvert.SerializeObject(loginData);                
                Task<String> response = M2ESSOClient.MakePostRequest(Constants.serverContextUrl + "/Auth/Login", postJson,
                    null);
                String result = await response;
                int len = result.Length;
                var authInfo = JsonConvert.DeserializeObject<ResponseModel<LoginResponse>>(result);
                if (authInfo.Status == 200)
                {
                    App.Database.UpdateItemFromUsername(loginData.UserName,authInfo.Payload.UTMZT,authInfo.Payload.UTMZK,authInfo.Payload.UTMZV);
                    //await DisplayAlert("Success", "Succesfully logged in!!!", "OK", null);
                    
                    await Navigation.PushModalAsync(new UserHomeView().GetUserHomeView());
                }
                else if(authInfo.Status == 401)
                {
                    App.Database.DeleteItems();
                    await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
                }
                else
                {
                    await DisplayAlert("Error", "Internal Server Error Occured!!!", "OK", null);
                }
            }
            
            
        }
    }
}
