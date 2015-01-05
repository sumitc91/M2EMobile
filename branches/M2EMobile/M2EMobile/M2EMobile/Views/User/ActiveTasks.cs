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
    class ActiveTasks : ContentPage
    {
        List<UserProductSurveyTemplateModel> userActiveTaskInfo = new List<UserProductSurveyTemplateModel>();
        ListView activeTaskList = new ListView();

        ContentPage _activeTasksPage = new ContentPage();
        public Page GetActiveTasks()
        {

            activeTaskList = new ListView
            {
                RowHeight = 50,
                HasUnevenRows = true,

            };
            //userTaskInfo[0].earningPerThreads            

            FetchUserActiveTaskDetailFromServer();

            //activeTaskList.ItemTemplate = new DataTemplate(typeof(TextCell));
            //activeTaskList.ItemTemplate.SetBinding(TextCell.TextProperty, "title");
            //activeTaskList.ItemTemplate.SetBinding(TextCell.DetailProperty, "earningPerThreads");

            activeTaskList.ItemTemplate = new DataTemplate(typeof(AllTaskListCell));

            activeTaskList.ItemTapped += async (sender, e) =>
            {
                var selectedItem = (UserProductSurveyTemplateModel)e.Item;
                //await DisplayAlert("Tapped!", e.SelectedItem + " was tapped.", "OK",null);
                var selectedItemPage = new UserActiveTaskInformation(selectedItem); // so the new page shows correct data

                new UserRootPage().PushAsyncModalPage(selectedItemPage);
            };
            

            _activeTasksPage = new ContentPage
            {
                Padding = new Thickness(20),
                Title = "Active CITs",
                Icon = "Leads.png",
                IsBusy = true,
                //BackgroundColor = Color.Black,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { activeTaskList }
                }
            };

            return _activeTasksPage;
        }

        protected async void FetchUserActiveTaskDetailFromServer()
        {

            Task<String> userDetailAsync = new UserDetailService().GetUserActiveTaskDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<List<UserProductSurveyTemplateModel>>>(userDetailString);
            userActiveTaskInfo = userDetail.Payload;
            activeTaskList.ItemsSource = userActiveTaskInfo;
            int len = userDetailString.Length;
            await HideBusyIcon();

        }

        protected async Task HideBusyIcon()
        {
            _activeTasksPage.IsBusy = false;
        }
    }
}
