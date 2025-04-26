using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SdkManager.Services.Abstractions;
using SdkManager.Views;

namespace SdkManager.Services;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    public void NavigateTo<TView>() where TView : NavigatableUserControl
    {
        var view = serviceProvider.GetRequiredService<TView>();
        view.OnNavigatedTo();
        OnNavigationCompleted(view);
    }

    public Action<UserControl> OnNavigationCompleted { get; set; } = null!;
}