using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MainWindowViewModel = SdkManager.ViewModels.Windows.MainWindowViewModel;

namespace SdkManager.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = App.Services.GetRequiredService<MainWindowViewModel>();
    }
}