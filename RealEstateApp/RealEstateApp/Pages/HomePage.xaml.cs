using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class HomePage : ContentPage
{
	//public HomePage()
	//{
	//	InitializeComponent();
 //       LblUserName.Text = $"Hi {Preferences.Get(AppSettings.UserName, string.Empty)}";
 //       GetCategories();
 //       GetTrendingProperties();

 //   }

 //   private void GetTrendingProperties()
 //   {
 //       throw new NotImplementedException();
 //   }

 //   private async void GetCategories()
 //   {
 //       var categories = await ApiService.GetCategories();
 //       CvCategories.ItemsSource = categories;
 //   }

 
     public HomePage()
     {
         InitializeComponent();
     }

     protected override async void OnAppearing()
     {
         base.OnAppearing();
         LblUserName.Text = $"Hi {Preferences.Get(AppSettings.UserName, string.Empty)}";
         await LoadCategories();
         await LoadTrendingProperties();
     }

     private async Task LoadCategories()
     {
         var categories = await ApiService.GetCategories();
         CvCategories.ItemsSource = categories;
     }

     private async Task LoadTrendingProperties()
     {
         var properties = await ApiService.GetTrendingProperties();
         CvTopPicks.ItemsSource = properties;
         
     }

}