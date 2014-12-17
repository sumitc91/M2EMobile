using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using M2EMobile.Services;

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

        public Task LoginAsync(CancellationToken cancellationToken)
        {
            IsBusy = true;
            return service
                .LoginAsync(Username, Password,"user", cancellationToken)
                .ContinueWith(t =>
                {
                    IsBusy = false;
                    if (t.IsFaulted)
                        throw new AggregateException(t.Exception);
                }, cancellationToken, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static bool ShouldShowLogin(DateTime? lastUseTime)
        {
            if (!lastUseTime.HasValue)
                return true;

            return (DateTime.UtcNow - lastUseTime) > ForceLoginTimespan;
        }
    }
}
