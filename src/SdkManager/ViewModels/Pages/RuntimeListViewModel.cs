using System.Collections.Frozen;

using CliWrapper;
using CliWrapper.Models;

using Microsoft.Extensions.Logging;

namespace SdkManager.ViewModels.Pages;

public class RuntimeListViewModel
{
    private readonly ILogger<RuntimeListViewModel> _logger;
    private readonly IDotnetCliWrapper _dotnetCliWrapper;

    public RuntimeListViewModel(
        ILogger<RuntimeListViewModel> logger,
        IDotnetCliWrapper dotnetCliWrapper)
    {
        _logger = logger;
        _dotnetCliWrapper = dotnetCliWrapper;
    }

    internal async Task OnNavigatedTo()
    {
        FrozenSet<InstalledRuntime> installedRuntimes =
            await _dotnetCliWrapper.GetInstalledRuntimes() ?? FrozenSet<InstalledRuntime>.Empty;
    }
}