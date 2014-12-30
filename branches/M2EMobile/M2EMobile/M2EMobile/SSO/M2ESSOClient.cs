using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public static async Task<string> MakeGetRequest(string url, string cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/html";
            request.Method = "GET";
            request.Headers["Cookie"] = cookie;

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();
            respStream.Flush();

            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                respStream = null;
                return strContent;
            }
        }

        public static async Task<string> MakePostRequest(string url, string data, string cookie, bool isJson = true)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            if (isJson)
                request.ContentType = "application/json";
            else
                request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Headers["Cookie"] = cookie;
            var stream = await request.GetRequestStreamAsync();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(data);
                writer.Flush();
                writer.Dispose();
            }

            var response = await request.GetResponseAsync();
            var respStream = response.GetResponseStream();


            using (StreamReader sr = new StreamReader(respStream))
            {
                //Need to return this response 
                return sr.ReadToEnd();
            }
        }

        public static async Task<string> PostMultiPartForm(string url, byte[] file, string paramName, string contentType, Dictionary<String, string> nvc,
        string cookie)
        {
            // log.Debug(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.Headers["Cookie"] = cookie;
            //wr.KeepAlive = true;
            //wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = await wr.GetRequestStreamAsync();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            rs.Write(file, 0, file.Length);


            byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            //rs.Close();
            string responseString = String.Empty;
            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream respStream = wresp.GetResponseStream();
                StreamReader respReader = new StreamReader(respStream);
                responseString = respReader.ReadToEnd();
                //log.Debug(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                //log.Error("Error uploading file", ex);
                if (wresp != null)
                {
                    //wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
            return responseString;

        }

        //sample....................
        //public static async Task<ResultModel> PostMultiPartInfoAsync(MyModel model, byte[] image, string cookie)
        //{
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();

        //    parameters.Add("Name", model.Name);
        //    parameters.Add("Age", model.Age);
        //    parameters.Add("Info", model.Info);


        //    string resp = await WebControl.PostMultiPartForm(Constants.url + Constants.Path, image, "file", "image/jpg", parameters, cookie);

        //    ResultModel model = JsonConvert.DeserializeObject<ResultModel>(resp);

        //    return model;

        //}
    }
}
