using CliWrapper.Models;

namespace SdkManager.Extensions
{
    public static class InstalledSdkExtensions
    {
        public static string GetFullPath(this InstalledSdk installedSdk)
        {
            return Path.Combine(installedSdk.InstallationPath, installedSdk.SdkVersion.ToString());
        }
    }
}