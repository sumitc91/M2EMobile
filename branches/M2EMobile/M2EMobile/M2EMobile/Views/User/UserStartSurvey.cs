using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models;
using M2EMobile.Models.DataResponse.UserResponse;
using M2EMobile.Models.DataResponse.UserResponse.Survey;
using M2EMobile.Services;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace M2EMobile.Views.User
{
    public class UserStartSurvey:CarouselPage
    {
        public UserStartSurvey(UserProductSurveyTemplateModel taskInfo)
        {
            FetchUserActiveSurveyDetailByRefKeyFromServer(taskInfo.refKey);
            var layout = new StackLayout();
            var label = new Label
            {
                Text = "start survey page",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };            
            layout.Children.Add(label);
            //layout.Children.Add(logoutButton);
            //Content = layout;            
             Children.Add(new ContentPage {Content = new BoxView {Color = new Color (1, 0, 0)}, Title = "Page 1"});
            Children.Add(new ContentPage {Content = new BoxView {Color = new Color(1, 0, 0)}, Title = "Page 1"});
            Children.Add(new ContentPage {Content = new BoxView {Color = new Color(0, 1, 0)}, Title = "Page 2"});
            Children.Add(new ContentPage {Content = new BoxView {Color = new Color (0, 0, 1)}, Title = "Page 3"});            

        }

        protected async void FetchUserActiveSurveyDetailByRefKeyFromServer(string refKey)
        {

            Task<String> userDetailAsync = new UserDetailService().GetUserActiveSurveyDetailsUsingRefKeyAsync(refKey);
            String userDetailString = await userDetailAsync;
           
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<UserSurveyResponse>>(userDetailString);
            
            //userActiveTaskInfo = userDetail.Payload;
            //activeTaskList.ItemsSource = userActiveTaskInfo;
            int len = userDetailString.Length;

        }
        
        
    }
}
