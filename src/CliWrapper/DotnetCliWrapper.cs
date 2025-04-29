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
                
                _logger.LogError("Process failed with exit code {ExitCode}: {Error}",
                    process.ExitCode, error);
                
                return null;
            }

            Func<string, InstalledSdk> parseSdkListLine = line =>
            {
                string[] splitLine = line.Split(' ');
                string version = splitLine[0];
                string path = splitLine[1].Trim('[', ']');
                return new InstalledSdk(version, path);
            };
            
            return output.Split(Environment.NewLine)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(parseSdkListLine)
                .ToFrozenSet();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting installed SDKs");
            return null;
        }
    }

    private Process SpawnProcess(string command)
    {
        _logger.LogInformation("Spawning process: {Command}", command);
        ProcessStartInfo startInfo = CreateProcessStartInfo(command);
        return Process.Start(startInfo) ?? throw new Exception("Failed to start process"); // TODO Better exception type
    }

    private ProcessStartInfo CreateProcessStartInfo(string command)
    {
        return new ProcessStartInfo
        {
            FileName = DotnetCli,
            Arguments = command,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
    }
}