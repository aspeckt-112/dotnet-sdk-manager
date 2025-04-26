using Avalonia.Controls;

namespace SdkManager.Views;

public abstract class NavigatableUserControl : UserControl
{
    public abstract void OnNavigatedTo();
}