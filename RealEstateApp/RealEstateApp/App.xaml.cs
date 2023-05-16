using RealEstateApp.Pages;

namespace RealEstateApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        var accessToken = Preferences.Get(AppSettings.AccessToken, string.Empty);
        if (string.IsNullOrEmpty(accessToken))
        {
            MainPage = new RegisterPage();
        }
        else
        {
            MainPage = new CustomTabbedPage();
        }

		// From this:
		//MainPage = new AppShell();

		//To this:
        //MainPage = new RegisterPage();

        //To this due to Android bug issue.:
        MainPage = new NavigationPage(new RegisterPage());
	}
}
