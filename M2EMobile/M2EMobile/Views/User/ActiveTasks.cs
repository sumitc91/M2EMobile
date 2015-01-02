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

            activeTaskList.ItemSelected += async (sender, e) =>
            {
                var selectedItem = (UserProductSurveyTemplateModel)e.SelectedItem;
                //await DisplayAlert("Tapped!", e.SelectedItem + " was tapped.", "OK",null);
                var selectedItemPage = new UserActiveTaskInformation(selectedItem); // so the new page shows correct data

                new UserRootPage().PushAsyncModalPage(selectedItemPage);
            };

            var pageDetailLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                Text = "Active CITs"
                //Font = Fonts.Twitter
            };
            BoxView boxView = new BoxView();
            boxView.HeightRequest = 1;
            boxView.Color = Color.Silver;

            return new ContentPage
            {
                Padding = new Thickness(20),
                //BackgroundColor = Color.Black,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = { pageDetailLabel, boxView, activeTaskList }
                }
            };
        }

        protected async void FetchUserActiveTaskDetailFromServer()
        {

            Task<String> userDetailAsync = new UserDetailService().GetUserActiveTaskDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<List<UserProductSurveyTemplateModel>>>(userDetailString);
            userActiveTaskInfo = userDetail.Payload;
            activeTaskList.ItemsSource = userActiveTaskInfo;
            int len = userDetailString.Length;

        }
    }
}
