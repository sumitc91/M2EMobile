using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.Constants;
using M2EMobile.Models.DataResponse;
using M2EMobile.Models.DataWrapper;
using M2EMobile.SSO;
using M2EMobile.ViewModels;
using Newtonsoft.Json;
using RestSharp.Portable;
using Xamarin.Forms;
using WebRequest = System.Net.WebRequest;

namespace M2EMobile.Views
{
    class UserHomeView 
    {
        public Page GetUserHomeView()
        {            
            return new UserRootPage().GetUserRootPage();
        }

    }
}
