using SdkManager.ViewModels.Pages;

namespace SdkManager.Views.Pages
{
    public partial class SettingsView : NavigatableUserControl
    {
        public SettingsView(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public override void OnNavigatedTo()
        {
        
        }
    }
}