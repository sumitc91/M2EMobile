using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Data;
using M2EMobile.Services;
using M2EMobile.ViewModels;
using M2EMobile.Views;
using Xamarin.Forms;

namespace M2EMobile
{
    public enum UIImplementation
    {
        CSharp = 0,
        Xaml
    }

    public class App :ContentPage
    {
        //Change the following line to switch between XAML and C# versions
        private static UIImplementation uiImplementation = UIImplementation.CSharp;

        public static IDirectoryService Service { get; set; }

        public static IPhoneFeatureService PhoneFeatureService { get; set; }

        public static DateTime LastUseTime { get; set; }

        public static Page GetMainPage()
        {
            var preLoginPage = new ContentPage();
            if (uiImplementation == UIImplementation.CSharp)
            {
                //userHomeView = new UserHomeView();
                preLoginPage = new PleaseLoginMessagePage();
            }
            else if (uiImplementation == UIImplementation.Xaml)
            {
                //employeeList = new EmployeeListXaml();
            }

            return new NavigationPage(preLoginPage);
        }
        
        static TodoItemDatabase database;
        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase();
                }
                return database;
            }
        }

        /// <summary>
        /// This Function is assigned to when the hardware button needs to be handled in code
        /// by the Views or ViewModels. It should mirror what the back/cancel button does in
        /// iOS.
        /// </summary>
        /// <returns>
        /// Three state (nullable) boolean:
        ///  - true  => Complete navigation as normal
        ///  - false => Do not navigate
        ///  - null  => Exit application (Used on screens that limit access to the app)
        /// </returns>
        public static Func<Task<bool?>> HardwareBackPressed
        {
            private get;
            set;
        }

        /// <summary>
        /// This function is used at the platform level when a hardware back button is pressed.
        /// On occasion we want to prevent/confirm backwards navigation or have the views/viewmodels
        /// perform an action on exit. Since iOS handles this without a hardware button, iOS will not
        /// use this method, but Android and WinPhone may both have hardware buttons that need to be
        /// handled.
        /// </summary>
        /// <returns>
        /// Three state (nullable) boolean:
        ///  - true  => Complete navigation as normal
        ///  - false => Do not navigate
        ///  - null  => Exit application (Used primarily on SignInScreen)
        /// </returns>
        public static async Task<bool?> CallHardwareBackPressed()
        {
            Func<Task<bool?>> backPressed = HardwareBackPressed;
            if (backPressed != null)
            {
                return await backPressed();
            }

            return true;
        }

    }
}
