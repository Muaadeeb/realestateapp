using RealEstateApp.Pages;

namespace RealEstateApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		// From this:
		//MainPage = new AppShell();

		//To this:
        MainPage = new RegisterPage();
	}
}
