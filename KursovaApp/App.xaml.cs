
using System.Diagnostics;

namespace KursovaApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		Debug.WriteLine("App initialized");
		MainPage = new AppShell();
	}
}
