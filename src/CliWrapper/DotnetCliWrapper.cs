using System.Collections.Frozen;
using System.Diagnostics;

using CliWrapper.Constants;
using CliWrapper.Models;

using Microsoft.Extensions.Logging;

namespace CliWrapper;

public class DotnetCliWrapper : IDotnetCliWrapper
{
    private const string DotnetCli = "dotnet";

    private readonly ILogger<DotnetCliWrapper> _logger;

    public DotnetCliWrapper(ILogger<DotnetCliWrapper> logger)
    {
        _logger = logger;
    }

    public async Task<bool> IsAnyVersionInstalled()
    {
        _logger.LogInformation("Checking if any version is installed");

        try
        {
            using Process process = SpawnProcess(CommandConstants.Version);
            await process.WaitForExitAsync();
            int exitCode = process.ExitCode;

            return exitCode == 0;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error checking if any version is installed");

            return false;
        }
    }

    public async Task<FrozenSet<InstalledSdk>?> GetInstalledSdks()
    {
        _logger.LogInformation("Getting installed SDKs");

        try
        {
            using Process process = SpawnProcess(CommandConstants.ListSdks);
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();

                _logger.LogError(
                    "Process failed with exit code {ExitCode}: {Error}",
                    process.ExitCode,
                    error);

                return null;
            }

            return output.Split(Environment.NewLine)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseSdkListLine)
                .OrderByDescending(sdk => sdk.SdkVersion)
                .ToFrozenSet();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting installed SDKs");

            return null;
        }
    }

    public async Task<FrozenSet<InstalledRuntime>?> GetInstalledRuntimes()
    {
        _logger.LogInformation("Getting installed runtimes");

        try
        {
            using Process process = SpawnProcess(CommandConstants.ListRuntimes);
            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
            {
                string error = await process.StandardError.ReadToEndAsync();

                _logger.LogError(
                    "Process failed with exit code {ExitCode}: {Error}",
                    process.ExitCode,
                    error);

                return null;
            }

            return output.Split(Environment.NewLine)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseRuntimeListLine)
                .OrderByDescending(sdk => sdk.RuntimeVersion)
                .ToFrozenSet();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting installed runtimes");

            throw;
        }
    }

    private Process SpawnProcess(string command)
    {
        _logger.LogInformation("Spawning process: {Command}", command);
        ProcessStartInfo startInfo = CreateProcessStartInfo(command);

        return Process.Start(startInfo) ?? throw new Exception("Failed to start process"); // TODO Better exception type
    }

    private ProcessStartInfo CreateProcessStartInfo(string command) => new()
    {
        FileName = DotnetCli,
        Arguments = command,
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        CreateNoWindow = true
    };

    private InstalledSdk ParseSdkListLine(string line)
    {
        string[] splitLine = line.Split(' ');

        if (splitLine.Length != 2)
        {
            throw new FormatException($"Invalid SDK list line format: {line}");
        }

        string versionString = splitLine[0].Trim();
        string installationPath = splitLine[1].Trim('[', ']');

        bool isPreview = !Version.TryParse(versionString, out Version? version);

        if (!isPreview)
        {
            return new InstalledSdk
            {
                SdkVersion = version!,
                InstallationPath = installationPath
            };
        }

        string[] splitVersion = versionString.ToLower().Replace("preview.", string.Empty).Split('-');

        if (splitVersion.Length != 2)
        {
            throw new FormatException($"Invalid preview version format: {versionString}");
        }

        if (!Version.TryParse(splitVersion[0], out Version? sdkVersion) ||
            !Version.TryParse(splitVersion[1], out Version? previewVersion))
        {
            throw new FormatException($"Invalid version format: {versionString}");
        }

        return new InstalledSdk
        {
            SdkVersion = sdkVersion,
            PreviewVersion = previewVersion,
            InstallationPath = installationPath
        };
    }

    private InstalledRuntime ParseRuntimeListLine(string line) => throw new NotImplementedException();
}