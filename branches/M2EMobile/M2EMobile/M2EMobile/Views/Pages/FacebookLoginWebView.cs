using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
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

            var user = new M2EMobile.Models.User();
            {
                user.Username = result.data[0].username + "@facebook.com";
                        //Password = EncryptionClass.Md5Hash(Guid.NewGuid().ToString()),
                        user.Source = "facebook";
                        user.isActive = "true";
                        user.Type = "user";
                        user.guid = Guid.NewGuid().ToString();
                        user.fixedGuid = Guid.NewGuid().ToString();
                        user.FirstName = result.data[0].first_name;
                        user.LastName = result.data[0].last_name;
                        user.gender = result.data[0].sex;
                        user.ImageUrl = result.data[0].pic_big_with_log;
                    };
        }
    }
}
