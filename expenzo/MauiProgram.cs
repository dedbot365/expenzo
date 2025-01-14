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
            builder.Services.AddSingleton<DebtDao>();
            builder.Services.AddSingleton<TransactionDao>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<AuthenticationStateService>();
            builder.Services.AddSingleton<EncryptionService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // Initialize database tables
            var categoryDao = app.Services.GetService<CategoryDao>();
            categoryDao?.CreateTable();
            var userDao = app.Services.GetService<UserDao>();
            userDao?.CreateTable();
            var debtDao = app.Services.GetService<DebtDao>();
            debtDao?.CreateTable();
            var transactionDao = app.Services.GetService<TransactionDao>();
            transactionDao?.CreateTable();

            return app;
        }
    }
}