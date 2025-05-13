using Avalonia;
using Avalonia.Input.Platform;
using Avalonia.Platform.Storage;

using CliWrapper;

using Microsoft.Extensions.DependencyInjection;

using SdkManager.ProcessManagers;
using SdkManager.ProcessManagers.Abstractions;
using SdkManager.Services;
using SdkManager.Services.Abstractions;
using SdkManager.ViewModels.Pages;
using SdkManager.ViewModels.Windows;
using SdkManager.Views.Pages;

namespace SdkManager.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<JsonUtilityService>();
        services.AddSingleton<CsvUtilityService>();

        return services;
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<HomeView>();
        services.AddTransient<SdkListView>();
        services.AddTransient<SettingsView>();

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<SdkListViewModel>();
        services.AddTransient<SettingsViewModel>();

        return services;
    }

    public static IServiceCollection AddDotnetCliWrapper(this IServiceCollection services)
    {
        services.AddSingleton<IDotnetCliWrapper, DotnetCliWrapper>();

        return services;
    }

    public static IServiceCollection AddProcessManager(this IServiceCollection services)
    {
        if (OperatingSystem.IsMacOS())
        {
            services.AddSingleton<IProcessManager, MacOsProcessManager>();
        }
        else
        {
            throw new NotImplementedException("Other operating systems are not supported yet.");
        }

        return services;
    }

    public static IServiceCollection AddAvaloniaComponents(this IServiceCollection services)
    {
        services.AddSingleton<IStorageProvider>(_ => Application.Current.GetStorageProvider());
        services.AddSingleton<IClipboard>(_ => Application.Current.GetClipboard());

        return services;
    }
}