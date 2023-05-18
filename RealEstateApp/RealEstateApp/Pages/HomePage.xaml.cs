using RealEstateApp.Models;
using RealEstateApp.Services;
using System.ComponentModel;



namespace RealEstateApp.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
        LblUserName.Text = $"Hi {Preferences.Get(AppSettings.UserName, string.Empty)}";
        GetCategories();
        GetTrendingProperties();
    }

    private async void GetCategories()
    {
        var categories = await ApiService.GetCategories();
        CvCategories.ItemsSource = categories;
    }

    private async void GetTrendingProperties()
    {
        var properties = await ApiService.GetTrendingProperties();
        CvTopPicks.ItemsSource = properties;

    }

    //private void CvCategories_SelectionChanged(System.Object sender, SelectionChangedEventArgs e)
    //{
    //    var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
    //    if (currentSelection is null) return;

    //    // Using PushAsync because this will automatically create a navigation bar for us.
    //    // With PushModalAsync - we would need to design the navbar ourselves.
    //    Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
    //    ((CollectionView)sender).SelectedItem = null;
    //}

    private void CvCategories_SelectionChanged(Object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if (currentSelection is null) return;

        // Using PushAsync because this will automatically create a navigation bar for us.
        // With PushModalAsync - we would need to design the navbar ourselves.
        Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
        ((CollectionView)sender).SelectedItem = null;
    }

    private void CvTopPicks_SelectionChanged(System.Object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;
        if (currentSelection is null) return;

        // Using PushAsync because this will automatically create a navigation bar for us.
        // With PushModalAsync - we would need to design the navbar ourselves.
        Navigation.PushModalAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
        ((CollectionView)sender).SelectedItem = null;
    }

    private void TapSearch_Tapped(System.Object sender, TappedEventArgs e)
    {
        Navigation.PushModalAsync(new SearchPage());
    }
}