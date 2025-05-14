using CliWrapper.Models;

namespace SdkManager.Extensions;

public static class InstalledSdkExtensions
{
    private const string Preview = "-preview.";

    public static string GetFullPath(this InstalledSdk installedSdk)
    {
        ArgumentNullException.ThrowIfNull(installedSdk, nameof(installedSdk));

        if (!installedSdk.IsPreview)
        {
            return Path.Combine(installedSdk.InstallationPath, installedSdk.SdkVersion.ToString());
        }

        var previewPath = $"{installedSdk.SdkVersion}{Preview}{installedSdk.PreviewVersion}";

        return Path.Combine(
            installedSdk.InstallationPath,
            previewPath);
    }
}