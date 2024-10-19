using System.Diagnostics;

namespace KursovaApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Debug.WriteLine("AppShell initialized");
	}
}
