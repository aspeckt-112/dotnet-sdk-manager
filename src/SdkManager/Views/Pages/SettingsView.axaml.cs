using Avalonia.Controls;
using SdkManager.ViewModels.Pages;

namespace SdkManager.Views.Pages;

public partial class SettingsView : UserControl
{
    public SettingsView(SettingsViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}