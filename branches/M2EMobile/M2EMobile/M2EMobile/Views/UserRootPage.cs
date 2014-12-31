using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataResponse;
using M2EMobile.SSO;
using M2EMobile.Views.Pages;
using Newtonsoft.Json;
using Xamarin.Forms;
using Label = Xamarin.Forms.Label;

namespace M2EMobile.Views
{
    public class UserRootPage
    {
        static MasterDetailPage MDPage;
        public static Page GetUserRootPage ()
		{
            return MDPage = new MasterDetailPage
            {
                Master = new ContentPage
                {
                    Title = "Master",
                    BackgroundColor = Color.Silver,
                    Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null,
                    Content = new StackLayout
                    {
                        Padding = new Thickness(5, 50),
                        Children = { Link("A"), Link("B"), Link("C") }
                    },
                },
                Detail =new UserDetailPage().GetAccountsPage()
                ,
            };
		}

        static Button Link(string name)
        {
            var button = new Button
            {
                Text = name,
                BackgroundColor = Color.FromRgb(0.9, 0.9, 0.9)
            };
            button.Clicked += delegate
            {
                switch (name)
                {
                    case "A":
                        MDPage.Detail =new UserDetailPage().GetAccountsPage();
                        MDPage.IsPresented = false;
                        break;
                    case "B":
                        MDPage.Detail = new ContractsPage();
                        MDPage.IsPresented = false;
                        break;
                    case "C":
                        MDPage.Detail = new LeadsPage();
                        MDPage.IsPresented = false;
                        break;
                    default: MDPage.Detail = new OpportunitiesPage();
                        MDPage.IsPresented = false;
                        break;
                }                
               
            };
            return button;
        }
        //public async Task<string> GetUserDetailsAsync()
        //{
        //    var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

        //    var userDetailAsync =
        //        M2ESSOClient.MakePostRequestWithHeaders(
        //            Constants.serverContextUrl + "/Client/GetClientDetails?userType=user", null, null, SQLiteInfo.UTMZK,
        //            SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
        //    return await userDetailAsync;
        //}
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    Task<String> userDetailAsync = GetUserDetailsAsync();
        //    String userDetailString = await userDetailAsync;
        //    var userDetail = JsonConvert.DeserializeObject<ResponseModel<ClientDetailsModel>>(userDetailString);
        //    int len = userDetailString.Length;
        //}
    }
}
