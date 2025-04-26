using Microsoft.Extensions.DependencyInjection;
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
        return services;
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<HomeView>();
        services.AddTransient<SettingsView>();
        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<SettingsViewModel>();
        return services;
    }
}