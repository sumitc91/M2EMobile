using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.DataResponse.UserResponse;
using M2EMobile.Services;
using Xamarin.Forms;

namespace M2EMobile.Views.User
{
    public class UserTaskInformation:ContentPage
    {
        private UserProductSurveyTemplateModel globalTaskInfo= new UserProductSurveyTemplateModel();
        public UserTaskInformation(UserProductSurveyTemplateModel taskInfo)
        {
            globalTaskInfo = taskInfo;
            var titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BindingContext = taskInfo
            };
            titleLabel.SetBinding(Label.TextProperty, "title");

            var detailLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BindingContext = taskInfo
                //Font = Fonts.Twitter
            };

            var totalThreadLabel = new Label
            {
                Text = "CITs Total:",
                Font = Font.SystemFontOfSize(NamedSize.Medium)
            };

            var totalThreadData = new Label
            {
                TextColor = Color.Yellow,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BindingContext = taskInfo
            };
            totalThreadData.SetBinding(Label.TextProperty, "totalThreads");

            var remainingThreadLabel = new Label
            {
                Text = " Remaining:",
                Font = Font.SystemFontOfSize(NamedSize.Medium)
            };
            var remainingThreadData = new Label
            {
                TextColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BindingContext = taskInfo
            };
            remainingThreadData.SetBinding(Label.TextProperty, "remainingThreads");

            var taskInfoLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { totalThreadLabel, totalThreadData, remainingThreadLabel, remainingThreadData }
            };

            var maxEarningLabel = new Label
            {
                Text = "Max Earning:",
                Font = Font.SystemFontOfSize(NamedSize.Medium)
            };

            var earningCurrencyData = new Label
            {
                TextColor = Color.Yellow,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BindingContext = taskInfo
            };
            earningCurrencyData.SetBinding(Label.TextProperty, "currency");

            var maxEarningData = new Label
            {
                TextColor = Color.Yellow,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                BindingContext = taskInfo
            };
            maxEarningData.SetBinding(Label.TextProperty, "earningPerThreads");

            var paymentInfoLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { maxEarningLabel, earningCurrencyData, maxEarningData },
            };

            detailLabel.SetBinding(Label.TextProperty, "title");

            var warningInfo = new Label
            {
                TextColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                Text = "After accepting you have to complete the job within 10 min, failing to do so will result in decrease in your reputation level. only accept if you will be able to complete the job within 10 mins of time span."
            };

            var iWillDoItButton = new Button
            {
                Text = "I Will Do It",
                Font = Font.SystemFontOfSize(NamedSize.Small),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Green,
                TextColor = Color.White
            };
            iWillDoItButton.Clicked += iWillDoItButton_Clicked;
            var thisIsSpam = new Button
            {
                Text = "This is Spam",
                Font = Font.SystemFontOfSize(NamedSize.Small),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Red,
                TextColor = Color.White
            };

            var buttonInfoLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = {iWillDoItButton,thisIsSpam },
            };

            var taskImage = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Source = ImageSource.FromUri(new Uri("http://i.imgur.com/sn0Croys.jpg")),
                WidthRequest = 200,
                HeightRequest = 200,
            };

            var nameLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { taskImage, titleLabel, detailLabel, taskInfoLayout, paymentInfoLayout,warningInfo, buttonInfoLayout }
            };
            Content = nameLayout;
        }

        async void iWillDoItButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Are You Sure ?", "Failing to complete this task within 10 mins will lead to reputation penalty !!", "Yes", "No");
            if (response)
            {
                Task<String> AcceptUserTaskByRefKeyString = new UserDetailService().AcceptUserTaskByRefIdAsync(globalTaskInfo.refKey);
                String userDetailString = await AcceptUserTaskByRefKeyString;
                int len = userDetailString.Length;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            App.HardwareBackPressed = () => Task.FromResult<bool?>(true);
        }

        protected async override void OnDisappearing()
        {
            base.OnAppearing();
            App.HardwareBackPressed = () => Task.FromResult<bool?>(false);
        }
    }
}
