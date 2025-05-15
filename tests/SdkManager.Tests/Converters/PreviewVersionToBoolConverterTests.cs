using CliWrapper.Models;

using SdkManager.Converters;

namespace SdkManager.Tests.Converters;

public class PreviewVersionToBoolConverterTests
{
    private readonly PreviewVersionToBoolConverter _converter = new();

    [Fact]
    public void Convert_ValueIsNotInstalledSdk_ReturnsNull()
    {
        // Arrange
        object? value = "Not an InstalledSdk";

        // Act
        object? result = _converter.Convert(value, null!, null, null!);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Convert_ValueIsInstalledSdkWithPreviewVersion_ReturnsTrue()
    {
        // Arrange
        InstalledSdk installedSdk = CreateInstalledSdk() with { PreviewVersion = new Version(1, 1) };

        // Act
        object? result = _converter.Convert(installedSdk, null!, null, null!);

        // Assert
        Assert.True((bool)result!);
    }

    [Fact]
    public void Convert_ValueIsInstalledSdkWithoutPreviewVersion_ReturnsFalse()
    {
        // Arrange
        InstalledSdk installedSdk = CreateInstalledSdk();

        // Act
        object? result = _converter.Convert(installedSdk, null!, null, null!);

        // Assert
        Assert.False((bool)result!);
    }

    [Fact]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        // Arrange
        object? value = null;

        // Act & Assert
        Assert.Throws<NotImplementedException>(() => _converter.ConvertBack(value, null!, null, null!));
    }

    private InstalledSdk CreateInstalledSdk() => new()
    {
        SdkVersion = new Version(1, 1),
        InstallationPath = @"C:\Path\To\Sdk"
    };
}