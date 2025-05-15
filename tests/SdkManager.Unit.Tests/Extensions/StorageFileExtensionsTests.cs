using Avalonia.Platform.Storage;

using SdkManager.Extensions;

namespace SdkManager.Unit.Tests.Extensions;

public class StorageFileExtensionsTests
{
    [Fact]
    public void GetCsvFilePath_WhenFileHasNoExtension_ReturnsPathWithCsvExtension()
    {
        string testFile = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        string testFilePath = Path.Combine(Path.GetTempPath(), testFile);

        // Arrange
        var file = new Mock<IStorageFile>();

        file.Setup(f => f.Path)
            .Returns(new Uri(testFilePath));

        // Act
        string result = file.Object.GetCsvFilePath();

        // Assert
        Assert.Equal($"{testFilePath}.csv", result);
    }
    
    [Fact]
    public void GetCsvFilePath_WhenFileHasCsvExtension_ReturnsSamePath()
    {
        string testFile = Path.GetRandomFileName();
        string testFilePath = Path.Combine(Path.GetTempPath(), testFile + ".csv");

        // Arrange
        var file = new Mock<IStorageFile>();

        file.Setup(f => f.Path)
            .Returns(new Uri(testFilePath));

        // Act
        string result = file.Object.GetCsvFilePath();

        // Assert
        Assert.Equal(testFilePath, result);
    }
}