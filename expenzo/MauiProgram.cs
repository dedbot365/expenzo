using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using expenzo.Database;

namespace expenzo
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();

            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddSingleton<CategoryDao>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize database tables
            var categoryDao = app.Services.GetService<CategoryDao>();
            categoryDao?.CreateTable();

            return app;
        }
    }
}