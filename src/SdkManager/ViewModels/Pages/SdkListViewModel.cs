using System.Collections.Frozen;
using System.Collections.ObjectModel;
using CliWrapper;
using CliWrapper.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using SdkManager.Extensions;
using SdkManager.ProcessManagers.Abstractions;

namespace SdkManager.ViewModels.Pages;

public partial class SdkListViewModel : ViewModelBase
{
    private readonly ILogger<SdkListViewModel> _logger;
    private readonly IDotnetCliWrapper _dotnetCliWrapper;
    private readonly IProcessManager _processManager;

    [ObservableProperty]
    private InstalledSdk? _selectedSdk;

    public SdkListViewModel(
        ILogger<SdkListViewModel> logger,
        IDotnetCliWrapper dotnetCliWrapper,
        IProcessManager processManager)
    {
        _logger = logger;
        _dotnetCliWrapper = dotnetCliWrapper;
        _processManager = processManager;
    }
    
    public ObservableCollection<InstalledSdk> Sdks { get; } = [];
    
    internal async Task OnNavigatedTo()
    {
        FrozenSet<InstalledSdk>? installedSdks =
            await _dotnetCliWrapper.GetInstalledSdks() ?? FrozenSet<InstalledSdk>.Empty;

        foreach (InstalledSdk installedSdk in installedSdks)
        {
            Sdks.Add(installedSdk);
        }
    }

    internal async Task OnSdkSelected()
    {
        if (SelectedSdk is null)
        {
            _logger.LogWarning("Selected triggered but selected SDK is null");
            return;
        }

        _processManager.OpenDirectory(SelectedSdk.GetFullPath());
    }
}