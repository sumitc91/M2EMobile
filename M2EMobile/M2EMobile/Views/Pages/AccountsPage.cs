using Xamarin.Forms;

namespace M2EMobile.Views.Pages
{
	public class AccountsPage : ContentPage
	{
		public static Page GetAccountsPage ()
		{
		    var layout = new StackLayout();
            var label = new Label
            {
                Text = "Account Page!",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
                    
            layout.Children.Add(label);

		    return new ContentPage {Content = layout};
		}
	}
	
}