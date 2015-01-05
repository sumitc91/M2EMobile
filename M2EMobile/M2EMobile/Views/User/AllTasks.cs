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
    
    class AllTasks : ContentPage
    {
        List<UserProductSurveyTemplateModel> userTaskInfo = new List<UserProductSurveyTemplateModel>();
        ListView contactList = new ListView();
        ContentPage _allTasksPage = new ContentPage();

        public Page GetAllTasks()
        {

            contactList = new ListView
            {                
                RowHeight = 50,
                HasUnevenRows = true,
                
            };
            //userTaskInfo[0].earningPerThreads            
                       
            FetchUserAllTaskDetailFromServer();

            //contactList.ItemTemplate = new DataTemplate(typeof(TextCell));
            //contactList.ItemTemplate.SetBinding(TextCell.TextProperty, "title");
            //contactList.ItemTemplate.SetBinding(TextCell.DetailProperty, "earningPerThreads");

            contactList.ItemTemplate = new DataTemplate(typeof(AllTaskListCell));

            contactList.ItemTapped += async (sender, e) =>
            {
                var selectedItem = (UserProductSurveyTemplateModel)e.Item;
                //await DisplayAlert("Tapped!", e.SelectedItem + " was tapped.", "OK",null);
                var selectedItemPage = new UserTaskInformation(selectedItem); // so the new page shows correct data                
                new UserRootPage().PushAsyncModalPage(selectedItemPage);                
            };
            

            _allTasksPage = new ContentPage
            {
                Padding = new Thickness(20),
                Title = "All CITs",
			    Icon = "Leads.png",
                IsBusy = true,
                //BackgroundColor = Color.Black,
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {contactList }
                }
            };

            return _allTasksPage;
        }

        protected async void FetchUserAllTaskDetailFromServer()
        {

            Task<String> userDetailAsync = new UserDetailService().GetUserAllTaskDetailsAsync();
            String userDetailString = await userDetailAsync;
            var userDetail = JsonConvert.DeserializeObject<ResponseModel<List<UserProductSurveyTemplateModel>>>(userDetailString);
            userTaskInfo = userDetail.Payload;
            contactList.ItemsSource = userTaskInfo;
            int len = userDetailString.Length;
            await HideBusyIcon();

        }

        protected async Task HideBusyIcon()
        {
            _allTasksPage.IsBusy = false;
        }
    }

    class AllTaskListCell : ViewCell
    {
        public AllTaskListCell()
        {
            var image = new Image
            {
                HorizontalOptions = LayoutOptions.Start
            };
            //image.SetBinding(Image.SourceProperty, new Binding("ImageUri"));
            image.Source = ImageSource.FromUri(new Uri("http://i.imgur.com/sn0Croys.jpg"));
            image.WidthRequest = image.HeightRequest = 80;

            var nameLayout = CreateNameLayout();
            BoxView boxView = new BoxView();
            boxView.HeightRequest = 1;
            boxView.Color = Color.Silver;

            var viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { image, nameLayout }
            };

            var stackLayout = new StackLayout()
            {
                Children = { viewLayout, boxView }
            };
            View = stackLayout;
        }

        static StackLayout CreateNameLayout()
        {

            var titleLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Small)
            };
            titleLabel.SetBinding(Label.TextProperty, "title");

            var detailLabel = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Font = Font.SystemFontOfSize(NamedSize.Micro)
                //Font = Fonts.Twitter
            };

            var totalThreadLabel = new Label
            {
                Text = "CITs Total:",
                Font = Font.SystemFontOfSize(NamedSize.Micro)
            };

            var totalThreadData = new Label
            {
                TextColor = Color.Yellow,
                Font = Font.SystemFontOfSize(NamedSize.Micro)
            };
            totalThreadData.SetBinding(Label.TextProperty, "totalThreads");

            var remainingThreadLabel = new Label
            {
                Text = " Remaining:",
                Font = Font.SystemFontOfSize(NamedSize.Micro)
            };
            var remainingThreadData = new Label { 
                TextColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Micro) 
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
                Font = Font.SystemFontOfSize(NamedSize.Micro)
            };

            var earningCurrencyData = new Label
            {
                TextColor = Color.Yellow,
                Font = Font.SystemFontOfSize(NamedSize.Micro)
            };
            earningCurrencyData.SetBinding(Label.TextProperty, "currency");

            var maxEarningData = new Label
            {
                TextColor = Color.Yellow,
                Font = Font.SystemFontOfSize(NamedSize.Micro)
            };
            maxEarningData.SetBinding(Label.TextProperty, "earningPerThreads");

            var paymentInfoLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children = { maxEarningLabel, earningCurrencyData, maxEarningData }
            };

            detailLabel.SetBinding(Label.TextProperty, "title");

            var nameLayout = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, detailLabel, taskInfoLayout, paymentInfoLayout }
            };
            return nameLayout;
        }
    }
}
