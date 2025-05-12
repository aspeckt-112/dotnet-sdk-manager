using Avalonia.Platform.Storage;

namespace SdkManager.Extensions;

public static class StorageFileExtensions
{
    public static string GetCsvFilePath(this IStorageFile file)
    {
        string path = file.Path.AbsolutePath;

        if (!Path.HasExtension(path))
        {
            path += ".csv";
        }

        return path;
    }
}