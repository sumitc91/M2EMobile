using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.Constants;
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
            Content = new Label
            {
                Text = "You are not Logged in yet !",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
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
 
            }
            
            
        }
    }
}
