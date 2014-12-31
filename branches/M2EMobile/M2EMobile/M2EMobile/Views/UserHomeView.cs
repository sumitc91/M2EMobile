using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataResponse;
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

        public async Task<string> GetUserDetailsAsync()
        {
            var SQLiteInfo = M2ESSOClient.GetUserInfoFromSQLite();

            var userDetailAsync =
                M2ESSOClient.MakePostRequestWithHeaders(
                    Constants.serverContextUrl + "/Client/GetClientDetails?userType=user", null, null, SQLiteInfo.UTMZK,
                    SQLiteInfo.TokenId, SQLiteInfo.UTMZV);
            return await userDetailAsync;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Task<String> userDetailAsync = GetUserDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<ClientDetailsModel>>(userDetailString);
            int len = userDetailString.Length;
        }

    }
}
