using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataWrapper;
using M2EMobile.Services;
using M2EMobile.SSO;
using Newtonsoft.Json;

namespace M2EMobile.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private IDirectoryService service;

        private static TimeSpan ForceLoginTimespan
        {
            get
            {
                return TimeSpan.FromMinutes(5);
            }
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string UserType { get; set; }
        public string ValidationErrors { get; private set; }

        public bool CanLogin
        {
            get
            {
                ValidationErrors = string.Empty;

                if (string.IsNullOrEmpty(Username))
                    ValidationErrors = "Please enter a username.";

                if (string.IsNullOrEmpty(Password))
                    ValidationErrors += "Please enter a password.";

                return string.IsNullOrEmpty(ValidationErrors);
            }
        }

        public LoginViewModel(IDirectoryService service)
        {
            this.service = service;

            Username = "";
            Password = "";
        }

        public async Task<String> LoginAsync(String username,String password,CancellationToken cancellationToken)
        {
            IsBusy = true;
            LoginRequest loginData = new LoginRequest
            {
                KeepMeSignedInCheckBox = "true",
                Password = password,
                Type = "web",
                UserName = username
            };

            var postJson = JsonConvert.SerializeObject(loginData);
            Task<String> response = M2ESSOClient.MakePostRequest(Constants.serverContextUrl + "/Auth/Login", postJson,
                null);
            String result = await response;
            //int len = result.Length;
            return result;
        }

        public static bool ShouldShowLogin(DateTime? lastUseTime)
        {
            //if (!lastUseTime.HasValue)
            //    return true;

            //return (DateTime.UtcNow - lastUseTime) > ForceLoginTimespan;
            var res = App.Database.GetItems();
            if(res.Count()>1) App.Database.DeleteItems();
            //res = App.Database.GetItems();
            return (res.Count() != 1);
        }
    }
}
