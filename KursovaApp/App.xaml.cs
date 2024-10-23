
using System.Diagnostics;

namespace KursovaApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Debug.WriteLine("App initialized");
		MainPage = new AppShell();

		ResizeWindow(2000, 800);
	}

        // Method to resize window without using UIWindow
	public void ResizeWindow(double width, double height)
	{
		var window = Application.Current?.Windows.FirstOrDefault();

		if (window != null)
		{
			window.Width = width;
			window.Height = height;
		}
	}
}
