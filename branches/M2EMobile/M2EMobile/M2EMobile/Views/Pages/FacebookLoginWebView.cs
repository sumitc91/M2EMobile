using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using M2EMobile.Models.DataWrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Portable.Deserializers;
using Xamarin.Forms;

namespace M2EMobile.Views.Pages
{
    public class FacebookLoginWebView : ContentPage
    {
        public void fetchUserInfoFromAccessToken(string accessToken)
        {
            var fb = new FacebookClient(accessToken);
                    dynamic result = fb.Get("fql",
                                new { q = "SELECT uid, first_name, last_name, sex, pic_big_with_logo, username FROM user WHERE uid=me()" });
            String temp = Convert.ToString(result);
            dynamic dynamicJsonResult = JObject.Parse(temp);
            var userDetail = JsonConvert.DeserializeObject<FacebookUserDetailAPIResponseWrapper>(temp);
            DisplayAlert(userDetail.data[0].username, userDetail.data[0].first_name + userDetail.data[0].first_name,"OK",null);
        }
    }
}
