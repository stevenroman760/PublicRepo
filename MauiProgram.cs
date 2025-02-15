using CSE.Maui.CustomControls.Consumer.Helpers;
using CSE.Maui.CustomControls.Consumer.Services;
using CSE.Maui.CustomControls.Consumer.Views;
using Microsoft.Extensions.Logging;

namespace PublicRepo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DataService>();
            builder.Services.AddSingleton<CompanyTreeViewBuilder>();
            builder.Services.AddTransient<TreeContainer>();

            return builder.Build();
        }
    }
}
