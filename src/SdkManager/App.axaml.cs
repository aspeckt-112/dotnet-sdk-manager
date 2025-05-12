using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SdkManager.Extensions;
using SdkManager.Services.Abstractions;
using SdkManager.Views.Windows;

using HomeView = SdkManager.Views.Pages.HomeView;

namespace SdkManager;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Services = CreateServiceProvider();
    }

    public static IServiceProvider Services { get; private set; } = null!;

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow();
            var navigationService = Services.GetRequiredService<INavigationService>();
            navigationService.NavigateTo<HomeView>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        DataAnnotationsValidationPlugin[] dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (DataAnnotationsValidationPlugin plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private static IServiceProvider CreateServiceProvider()
    {
        IServiceCollection services = new ServiceCollection()
            .AddLogging(builder => { builder.AddConsole(); })
            .AddServices()
            .AddViews()
            .AddViewModels()
            .AddDotnetCliWrapper()
            .AddProcessManager()
            .AddAvaloniaComponents();

        return services.BuildServiceProvider();
    }
}