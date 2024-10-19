using System.Diagnostics;
using Foundation;

namespace KursovaApp;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp()
    {
        Debug.WriteLine("Creating Maui App");
        return MauiProgram.CreateMauiApp();
    }
}
