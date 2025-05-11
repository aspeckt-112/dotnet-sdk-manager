namespace CliWrapper.Models;

public record InstalledSdk
{
    public required Version SdkVersion { get; init; }
    
    public required string InstallationPath { get; init; }
    
    public Version? PreviewVersion { get; init; }
}