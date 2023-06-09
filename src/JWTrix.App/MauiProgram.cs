using DiffPlex;
using DiffPlex.DiffBuilder;
using JWTrix.Data.Abstractions;
using JWTrix.Data.Repositories;
using JWTrix.Service;
using JWTrix.Service.Abstractions;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;

namespace JWTrix.App;

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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services
            .AddSingleton<StateContainer>()
            .AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = true;
                config.SnackbarConfiguration.HideTransitionDuration = 400;
                config.SnackbarConfiguration.ShowTransitionDuration = 400;
                config.SnackbarConfiguration.VisibleStateDuration = 8000;
                config.SnackbarConfiguration.ShowCloseIcon = false;
            });

        // Repository
        builder.Services.AddSingleton<IHistoryRepository, HistoryRepository>();

        // Service
        builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
        builder.Services.AddSingleton<IHistoryService, HistoryService>();

        // Diff
        builder.Services.AddScoped<ISideBySideDiffBuilder, SideBySideDiffBuilder>();
        builder.Services.AddScoped<IDiffer, Differ>();

        var app = builder.Build();

        app.Services.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(MauiProgram)).LogInformation("Application started.");

        return app;
    }
}