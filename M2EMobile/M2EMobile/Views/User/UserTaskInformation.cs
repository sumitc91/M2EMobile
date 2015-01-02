using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.DataResponse.UserResponse;
using Xamarin.Forms;

namespace M2EMobile.Views.User
{
    public class UserTaskInformation:ContentPage
    {
        public UserTaskInformation(UserProductSurveyTemplateModel taskInfo)
        {
            var layout = new StackLayout();
            var label = new Label
            {
                Text = taskInfo.title,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };            
            
            layout.Children.Add(label);
            //layout.Children.Add(logoutButton);
            Content = layout;
        }
    }
}
