using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using M2EMobile.Droid.Page;
using M2EMobile.Views.Pages;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace M2EMobile.Droid.Page
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnModelChanged(VisualElement oldModel, VisualElement newModel)
        {
            base.OnModelChanged(oldModel, newModel);

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "574134802616730", // your OAuth2 client id
                scope: "email", // the scopes for the particular API you're accessing, delimited by "+" symbols
                authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), // the auth URL for the service
                redirectUrl: new Uri("https://www.cautom.com/SocialAuth/FBLogin/")); // the redirect URL for the service

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    //App.SuccessfulLoginAction.Invoke();
                    // Use eventArgs.Account to do wonderful things
                    //App.SaveToken(eventArgs.Account.Properties["access_token"]);
                    string accessToken = eventArgs.Account.Properties["access_token"];
                    new FacebookLoginWebView().fetchUserInfoFromAccessToken(accessToken);
                }
                else
                {
                    // The user cancelled
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}