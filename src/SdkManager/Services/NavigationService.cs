using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SdkManager.Services.Abstractions;
using SdkManager.ViewModels;

namespace SdkManager.Services;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public void NavigateTo<TView>() where TView : UserControl
    {
        var view = serviceProvider.GetRequiredService<TView>();
        OnNavigated(view);
    }

    public Action<UserControl> OnNavigated { get; set; } = null!;
}