using Filantroplanta.Views;
using Filantroplanta.Views.Produtor;
using Microsoft.Maui.Controls;

namespace Filantroplanta;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        var nav = new NavigationPage(new Login());

        nav.BarBackgroundColor = Colors.DarkGreen;
        nav.BarTextColor = Colors.White;

        MainPage = nav;
    }
}
