using SdkManager.ViewModels.Pages;

namespace SdkManager.Views.Pages;

public partial class HomeView : NavigatableUserControl
{
    private readonly HomeViewModel _viewModel;
    
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        DataContext = _viewModel;
    }

    public override async void OnNavigatedTo()
    {
        try
        {
            await _viewModel.OnNavigatedTo();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw; // TODO Handle this better
        }
    }
}