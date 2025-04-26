using Avalonia.Controls;
using SdkManager.ViewModels;
using SdkManager.ViewModels.Pages;

namespace SdkManager.Views.Pages;

public partial class HomeView : UserControl
{
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}