using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;

namespace SdkManager.Extensions;

public static class ApplicationExtensions
{
    public static IClassicDesktopStyleApplicationLifetime GetDesktopLifetime(this Application application)
    {
        if (application is null)
        {
            throw new InvalidOperationException("Avalonia application is not initialized.");
        }

        if (application.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            throw new InvalidOperationException("Avalonia application is not running in a classic desktop style.");
        }

        return desktop;
    }

    public static Window GetMainWindow(this Application application)
    {
        IClassicDesktopStyleApplicationLifetime lifetime = application.GetDesktopLifetime();

        if (lifetime.MainWindow is null)
        {
            throw new InvalidOperationException("Avalonia application does not have a main window.");
        }

        return lifetime.MainWindow;
    }

    public static IStorageProvider GetStorageProvider(this Application application)
    {
        Window mainWindow = application.GetMainWindow();

        if (mainWindow.StorageProvider is null)
        {
            throw new InvalidOperationException("Avalonia application does not have a storage provider.");
        }

        return mainWindow.StorageProvider;
    }
}