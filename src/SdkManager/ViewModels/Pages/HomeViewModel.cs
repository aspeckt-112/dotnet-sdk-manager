using CliWrapper;
using Microsoft.Extensions.Logging;

namespace SdkManager.ViewModels.Pages;

public partial class HomeViewModel : ViewModelBase
{
    private readonly ILogger<HomeViewModel> _logger;
    private readonly IDotnetCliWrapper _dotnetCliWrapper;

    public HomeViewModel(
        ILogger<HomeViewModel> logger, 
        IDotnetCliWrapper dotnetCliWrapper)
    {
        _logger = logger;
        _dotnetCliWrapper = dotnetCliWrapper;
    }

    internal async Task OnNavigatedTo()
    {
        bool isAnyVersionInstalled = await _dotnetCliWrapper.IsAnyVersionInstalled();
    }
}