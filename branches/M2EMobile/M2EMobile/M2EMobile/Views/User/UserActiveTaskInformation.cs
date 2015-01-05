using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M2EMobile.Models.DataResponse.UserResponse;
using Xamarin.Forms;

namespace M2EMobile.Views.User
{
    internal class UserActiveTaskInformation : ContentPage
    {
        private UserProductSurveyTemplateModel globalTaskInfo = new UserProductSurveyTemplateModel();

        public UserActiveTaskInformation(UserProductSurveyTemplateModel taskInfo)
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
                Children = {totalThreadLabel, totalThreadData, remainingThreadLabel, remainingThreadData}
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
                Children = {maxEarningLabel, earningCurrencyData, maxEarningData},
            };

            detailLabel.SetBinding(Label.TextProperty, "title");

            var warningInfo = new Label
            {
                TextColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Medium),
                Text =
                    "After accepting you have to complete the job within 10 min, failing to do so will result in decrease in your reputation level. only accept if you will be able to complete the job within 10 mins of time span."
            };

            var startTaskButton = new Button
            {
                Text = "Start this CIT",
                Font = Font.SystemFontOfSize(NamedSize.Micro),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Green,
                TextColor = Color.White
            };
            startTaskButton.Clicked += startTaskButton_Clicked;
            ;

            var iCantDoThis = new Button
            {
                Text = "I won't be able to do it",
                Font = Font.SystemFontOfSize(NamedSize.Micro),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.Red,
                TextColor = Color.White
            };

            var buttonInfoLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = {startTaskButton, iCantDoThis},
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
                Children =
                {
                    taskImage,
                    titleLabel,
                    detailLabel,
                    taskInfoLayout,
                    paymentInfoLayout,
                    warningInfo,
                    buttonInfoLayout
                }
            };
            Content = nameLayout;
        }

        private void startTaskButton_Clicked(object sender, EventArgs e)
        {
            var startSurveyPage = new UserStartSurvey(globalTaskInfo); // so the new page shows correct data

            new UserRootPage().PushAsyncModalPage(startSurveyPage);
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
