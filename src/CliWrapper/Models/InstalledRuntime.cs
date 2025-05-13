namespace CliWrapper.Models;

public record InstalledRuntime
{
    public required Version RuntimeVersion { get; init; }

    public required string InstallationPath { get; init; }

    public Version? PreviewVersion { get; init; }
}