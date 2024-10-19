using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using Microsoft.Maui.LifecycleEvents;

namespace KursovaApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp() =>
        MauiApp.CreateBuilder()
            .UseMauiApp<App>()  // Use your App class
            .ConfigureFonts(fonts =>
            {
                // Register your fonts here
            })
            .ConfigureLifecycleEvents(events =>
            {
                events.AddiOS(iOS => iOS
                    .OnActivated(app =>
                    {
                        Debug.WriteLine("App activated (iOS/Mac Catalyst)");
                    }));
            })
            .Build();
}
