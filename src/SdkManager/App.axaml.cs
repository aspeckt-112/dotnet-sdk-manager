using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SdkManager.Extensions;
using SdkManager.Services;
using SdkManager.Services.Abstractions;
using SdkManager.ViewModels;
using SdkManager.ViewModels.Pages;
using SdkManager.Views;
using SdkManager.Views.Windows;
using HomeView = SdkManager.Views.Pages.HomeView;
using MainWindowViewModel = SdkManager.ViewModels.Windows.MainWindowViewModel;
using SettingsView = SdkManager.Views.Pages.SettingsView;

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
            .AddServices()
            .AddViews()
            .AddViewModels();

        return services.BuildServiceProvider();
    }
}