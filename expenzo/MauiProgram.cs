using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using expenzo.Database;
using expenzo.Services;
using expenzo.Services.Interfaces;
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
            builder.Services.AddSingleton<UserDao>();
            builder.Services.AddSingleton<IUserService, UserService>();
            

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize database tables for Category
            var categoryDao = app.Services.GetService<CategoryDao>();
            categoryDao?.CreateTable();
            var userDao = app.Services.GetService<UserDao>();
            userDao?.CreateTable();
            return app;
        }
    }
}