using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
