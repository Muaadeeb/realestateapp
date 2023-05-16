using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        LblUserName.Text = $"Hi {Preferences.Get(AppSettings.UserName, string.Empty)}";
        GetCategories();

    }

    private async Task GetCategories()
    {
        var categories = await ApiService.GetCategories();
        CvCategories.ItemsSource = categories;
    }
}