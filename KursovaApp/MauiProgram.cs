using Microsoft.Maui.LifecycleEvents;
namespace KursovaApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureLifecycleEvents(events =>
            {
#if MACCATALYST
                events.AddiOS(iOS =>
                    iOS.SceneWillConnect((scene, session, options) =>
                    {
                        if (scene is UIKit.UIWindowScene windowScene)
                        {
                            var window = windowScene.KeyWindow;
                            if (window != null)
                            {
                                windowScene.SizeRestrictions.MinimumSize = new CoreGraphics.CGSize(4000, 900); // Set minimum window size
                                windowScene.SizeRestrictions.MaximumSize = new CoreGraphics.CGSize(4000, 900); // Set maximum window size (optional)
                            }
                        }
                    }));
#endif
            });

        return builder.Build();
    }
}
