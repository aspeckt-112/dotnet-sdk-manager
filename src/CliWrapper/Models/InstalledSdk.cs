namespace CliWrapper.Models;

public record InstalledSdk
{
    public required string Version { get; init; }

    public required string InstallationPath { get; init; }
}