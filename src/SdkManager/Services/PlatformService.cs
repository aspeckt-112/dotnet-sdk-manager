using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SdkManager.Services;

public class PlatformService
{
    private const string Windows = "Windows";
    private const string Mac = "Mac";
    private const string Linux = "Linux";

    private readonly Dictionary<string, string> _operatingSystemToDirectoryOpenProcessMap = new()
    {
        { Windows, "explorer.exe" },
        { Mac, "open" },
        { Linux, "xdg-open" }
    };

    public Task OpenDirectory(string path)
    {
        string operatingSystem = GetOperatingSystem();

        if (!_operatingSystemToDirectoryOpenProcessMap.TryGetValue(operatingSystem, out string? processName))
        {
            throw new PlatformNotSupportedException($"Unsupported operating system: {operatingSystem}");
        }

        ProcessStartInfo processStartInfo = new()
        {
            FileName = processName,
            Arguments = path,
            UseShellExecute = true
        };

        Process? process = Process.Start(processStartInfo);

        if (process is null)
        {
            throw new InvalidOperationException($"Failed to start process: {processName}");
        }

        return process.WaitForExitAsync();
    }

    private static string GetOperatingSystem()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Windows;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return Mac;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Linux;
        }

        throw new PlatformNotSupportedException("Unsupported operating system");
    }
}