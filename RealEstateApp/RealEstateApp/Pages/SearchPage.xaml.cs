using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
	}

    void ImgBackBtn_Clicked(Object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    async void SbProperty_TextChanged(Object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue == null)
        {
            return;
        }

        var propertiesResult = await ApiService.FindProperties(e.NewTextValue);
        CvSearch.ItemsSource = propertiesResult;
    }

    private void CvSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as SearchProperty;
        if (currentSelection is null) return;

        // Using PushAsync because this will automatically create a navigation bar for us.
        // With PushModalAsync - we would need to design the navbar ourselves.
        Navigation.PushModalAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));
        ((CollectionView)sender).SelectedItem = null;
    }
}