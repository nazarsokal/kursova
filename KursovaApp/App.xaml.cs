
namespace KursovaApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

		const int newHeight = 1200;
		const int newWidth = 3000;

		window.Height = newHeight;
		window.Width = newWidth;

		return window;
    }
}
