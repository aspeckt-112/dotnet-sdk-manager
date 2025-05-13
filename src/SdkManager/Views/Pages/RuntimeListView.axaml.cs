using SdkManager.ViewModels.Pages;

namespace SdkManager.Views.Pages;

public partial class RuntimeListView : NavigatableUserControl
{
    private readonly RuntimeListViewModel _viewModel;

    public RuntimeListView(RuntimeListViewModel viewModel)
    {
        _viewModel = viewModel;
        DataContext = _viewModel;
        
        InitializeComponent();
    }

    public override void OnNavigatedTo()
    {
        throw new NotImplementedException();
    }
}