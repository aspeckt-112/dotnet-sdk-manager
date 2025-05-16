namespace CliWrapper.Models;

public record InstalledRuntime
{
    public required string Name { get; init; }

    public required string Version { get; init; }

    public required string InstallationPath { get; init; }
}