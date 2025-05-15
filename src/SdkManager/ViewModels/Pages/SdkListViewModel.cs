using System.Collections.Frozen;
using System.Collections.ObjectModel;

using Avalonia.Platform.Storage;

using CliWrapper;
using CliWrapper.Models;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Logging;

using SdkManager.Extensions;
using SdkManager.Services;

namespace SdkManager.ViewModels.Pages;

public partial class SdkListViewModel : ViewModelBase
{
    private readonly ILogger<SdkListViewModel> _logger;
    private readonly IDotnetCliWrapper _dotnetCliWrapper;
    private readonly PlatformService _platformService;
    private readonly JsonUtilityService _jsonUtilityService;
    private readonly CsvUtilityService _csvUtilityService;
    private readonly IStorageProvider _storageProvider;

    [ObservableProperty]
    private InstalledSdk? _selectedSdk;

    public SdkListViewModel(
        ILogger<SdkListViewModel> logger,
        IDotnetCliWrapper dotnetCliWrapper,
        PlatformService platformService,
        JsonUtilityService jsonUtilityService,
        CsvUtilityService csvUtilityService,
        IStorageProvider storageProvider)
    {
        _logger = logger;
        _dotnetCliWrapper = dotnetCliWrapper;
        _platformService = platformService;
        _jsonUtilityService = jsonUtilityService;
        _csvUtilityService = csvUtilityService;
        _storageProvider = storageProvider;
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

        await _platformService.OpenDirectory(SelectedSdk.InstallationPath);
    }

    [RelayCommand(AllowConcurrentExecutions = false)]
    private Task CopyAsJson() => _jsonUtilityService.CopyJsonToClipboard(Sdks);

    [RelayCommand(AllowConcurrentExecutions = false)]
    private async Task ExportAsCsv()
    {
        IStorageFile? stoageFile = await _storageProvider.SaveFilePickerAsync(
            new FilePickerSaveOptions
            {
                DefaultExtension = ".csv",
                FileTypeChoices = [new FilePickerFileType("Comma Separated Values")],
                Title = "Save CSV File"
            });

        if (stoageFile is null)
        {
            _logger.LogWarning("CSV file save operation was cancelled.");

            return;
        }

        await _csvUtilityService.SaveAsCsv(Sdks, stoageFile.GetCsvFilePath());
    }
}