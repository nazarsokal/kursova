
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

        // Method to resize window without using UIWindow
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 800;
        const int newHeight = 600;
        
        window.Width = newWidth;
        window.Height = newHeight;

        return window;
    }
}
