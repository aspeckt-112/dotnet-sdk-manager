using Avalonia.Controls;
using SdkManager.Views;

namespace SdkManager.Services.Abstractions
{
    public interface INavigationService
    {
        void NavigateTo<TView>() where TView : NavigatableUserControl;

        Action<UserControl> OnNavigationCompleted { get; set; }
    }
}