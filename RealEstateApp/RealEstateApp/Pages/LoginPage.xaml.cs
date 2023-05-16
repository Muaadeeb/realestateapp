using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);
        if (response)
        {
            // Prevent user from pressing back button and going back to login page.
            // User must logout first.
            Application.Current!.MainPage = new CustomTabbedPage();
        }
        else
        {
            await DisplayAlert("", "Oops, something went wrong.", "Cancel");
        }

    }

    private async void TapJoinNow_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}