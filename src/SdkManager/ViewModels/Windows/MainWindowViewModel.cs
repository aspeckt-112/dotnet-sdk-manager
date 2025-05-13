using Avalonia.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SdkManager.Services.Abstractions;
using SdkManager.Views.Pages;

using HomeView = SdkManager.Views.Pages.HomeView;
using SettingsView = SdkManager.Views.Pages.SettingsView;

namespace SdkManager.ViewModels.Windows;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private UserControl? _currentView;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _navigationService.OnNavigationCompleted = newView => CurrentView = newView;
    }

    [RelayCommand]
    private void NavigateToHome()
    {
        _navigationService.NavigateTo<HomeView>();
    }

    [RelayCommand]
    private void NavigateToSdkList()
    {
        _navigationService.NavigateTo<SdkListView>();
    }

    [RelayCommand]
    private void NavigateToRuntimeList()
    {
        _navigationService.NavigateTo<RuntimeListView>();
    }

    [RelayCommand]
    private void NavigateToSettings()
    {
        _navigationService.NavigateTo<SettingsView>();
    }
}