using System;
using Avalonia.Controls;

namespace SdkManager.Services.Abstractions;

public interface INavigationService
{
    void NavigateTo<TView>() where TView : UserControl;

    Action<UserControl> OnNavigated { get; set; }
}