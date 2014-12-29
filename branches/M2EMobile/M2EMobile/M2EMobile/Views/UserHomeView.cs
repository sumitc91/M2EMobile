using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.ViewModels;
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

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //var client = new RestClient("http://echo.jsontest.com/key/value/otherkey/othervalue");
            //// client.Authenticator = new HttpBasicAuthenticator(username, password);

            //var request = new RestRequest("resource/{id}", HttpMethod.Get);
            
            //// easily add HTTP Headers
            //request.AddHeader("header", "value");

            //// add files to upload (works with compatible verbs)
            ////request.AddFile(path);

            //// execute the request
            //var response = client.Execute(request);
            //var content = response.Result; // raw content as string

            //var client = new RestClient("http://echo.jsontest.com");
            //var request = new RestRequest("key/value/otherkey/othervalue", HttpMethod.Get);
            //var queryResult = client.Execute<List<Dictionary<string,string>>>(request).Result;

            //var uri = "http://echo.jsontest.com/key/value/otherkey/othervalue";
            //var client = new HttpClient();
            ////var resultAsync = await client.GetStringAsync(uri); //"http://api.ihackernews.com/page"
            //var result = client.GetStringAsync(uri);


            //var request = await Task.Run(() => System.Net.WebRequest.Create("http://echo.jsontest.com/key/value/otherkey/othervalue")).ConfigureAwait(false);

            var httpClient = new HttpClient(); // Xamarin supports HttpClient!

            Task<string> contentsTask = httpClient.GetStringAsync("http://echo.jsontest.com/key/value/otherkey/othervalue"); // async method!

            // await! control returns to the caller and the task continues to run on another thread
            string contents = await contentsTask;

            string result = "";
            //result += "DownloadHomepage method continues after async call. . . . .\n";

            // After contentTask completes, you can calculate the length of the string.
            int exampleInt = contents.Length;

            //result += "Downloaded the html and found out the length.\n\n\n";

            result += contents; // just dump the entire HTML

            
            if (LoginViewModel.ShouldShowLogin(App.LastUseTime))
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
        }

    }
}
