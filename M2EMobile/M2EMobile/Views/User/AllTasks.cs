using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.DataResponse.UserResponse;
using M2EMobile.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace M2EMobile.Views.User
{
    public class TaskInfo
    {
        public string Title { get; set; }
        public string Info { get; set; }
    }
    
    class AllTasks : ContentPage
    {
        List<UserProductSurveyTemplateModel> userTaskInfo = new List<UserProductSurveyTemplateModel>();
        ListView contactList = new ListView();
        public Page GetAllTasks()
        {

            contactList = new ListView
            {                
                RowHeight = 50,
                HasUnevenRows = true,                
            };
            //userTaskInfo[0].earningPerThreads            
                       
            FetchUserAllTaskDetailFromServer();

            contactList.ItemTemplate = new DataTemplate(typeof(TextCell));
            contactList.ItemTemplate.SetBinding(TextCell.TextProperty, "title");
            contactList.ItemTemplate.SetBinding(TextCell.DetailProperty, "earningPerThreads");

            return new ContentPage
            {
                Padding = new Thickness(20),
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { contactList }
                }
            };
        }

        protected async void FetchUserAllTaskDetailFromServer()
        {

            Task<String> userDetailAsync = new UserDetailService().GetUserAllTaskDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<List<UserProductSurveyTemplateModel>>>(userDetailString);
            userTaskInfo = userDetail.Payload;
            contactList.ItemsSource = userTaskInfo;
            int len = userDetailString.Length;
            
        }

    }
}
