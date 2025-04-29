using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SdkManager.ViewModels.Pages;

namespace SdkManager.Views.Pages;

public partial class SdkListView : NavigatableUserControl
{
    private readonly SdkListViewModel _viewModel;

    public SdkListView(SdkListViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    public override async void OnNavigatedTo()
    {
        await _viewModel.OnNavigatedTo();
    }

    private async void OnGridRowDoubleTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            await _viewModel.OnSdkSelected();
        }
        catch (Exception exception)
        {
            throw; // TODO handle exception
        }
    }
}