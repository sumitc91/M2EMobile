using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.DataWrapper;
using Newtonsoft.Json;

namespace M2EMobile.SSO
{
    public class M2ESSOClient
    {
        const int TimeoutSeconds = 30;
        const string user_agent = "M2ESSOClient .NET v2.0";
        static Encoding encoding = Encoding.UTF8;        

        public async Task<string> HttpGetAsync(string url)
        {
            var httpClient = new HttpClient(); // Xamarin supports HttpClient!

            Task<string> contentsTask = httpClient.GetStringAsync(url); // async method!

            // await! control returns to the caller and the task continues to run on another thread
            //string contents = await contentsTask;
            return await contentsTask;;
        }

        public async Task<String> HttpPostLoginAsync(string username, string password, string kmsi, string url)
        {
            var comment = "hello world";
            var questionId = 1;

            LoginRequest loginData = new LoginRequest
            {
                KeepMeSignedInCheckBox = "true",
                Password = "password",
                Type = "web",
                UserName = "sumitchourasia91@gmail.com"
            };

            var postJson = JsonConvert.SerializeObject(loginData);

            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            StringContent queryString = new StringContent(postJson.ToString());

            HttpResponseMessage response = await client.PostAsync(new Uri(url), queryString);

            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            response.EnsureSuccessStatusCode();
            string responseBody = "";
            try
            {
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
            

            return responseBody;
        }

        public async static Task<String> Post(string url, Dictionary<string, string> headers = null, string body = "", string contentType = "application/json")
        {
            HttpClientHandler handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            // add headers
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            // set the content
            request.Content = new StringContent(body, Encoding.UTF8, contentType);
            // set the content length
            request.Content.Headers.ContentLength = body.Length;

            // set transfer-enconding 
            if (handler.SupportsTransferEncodingChunked())
            {
                bool chuncked = false;
                request.Headers.TransferEncodingChunked = chuncked;
                httpClient.DefaultRequestHeaders.TransferEncodingChunked = chuncked;
            }
            // await and return response
            HttpResponseMessage response = await httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
