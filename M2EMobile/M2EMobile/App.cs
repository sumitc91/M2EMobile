using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using M2EMobile.Services;
using Xamarin.Forms;

namespace M2EMobile
{
    public enum UIImplementation
    {
        CSharp = 0,
        Xaml
    }

    public class App
    {
        //Change the following line to switch between XAML and C# versions
        private static UIImplementation uiImplementation = UIImplementation.CSharp;

        public static IDirectoryService Service { get; set; }

        public static IPhoneFeatureService PhoneFeatureService { get; set; }

        public static DateTime LastUseTime { get; set; }

        public static Page GetMainPage()
        {
            var employeeList = new ContentPage();
            if (uiImplementation == UIImplementation.CSharp)
            {
                employeeList = new UserHomeView();
            }
            else if (uiImplementation == UIImplementation.Xaml)
            {
                //employeeList = new EmployeeListXaml();
            }

            return new NavigationPage(employeeList);
        }
    }
}
