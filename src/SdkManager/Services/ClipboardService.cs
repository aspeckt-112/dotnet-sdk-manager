using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.Platform;
using SdkManager.Services.Abstractions;

namespace SdkManager.Services;

public class ClipboardService : IClipboardService
{
    private readonly IClipboard _clipboard;

    public ClipboardService()
    {
        // TODO Any issues with this? Could there be a desktop device without a clipboard?
        if (Application.Current is null)
        {
            throw new InvalidOperationException("Avalonia application is not initialized.");
        }

        if (Application.Current is not { ApplicationLifetime: IClassicDesktopStyleApplicationLifetime desktop })
        {
            throw new InvalidOperationException("Avalonia application is not running in a classic desktop style.");
        }

        if (desktop.MainWindow?.Clipboard is { } clipboard)
        {
            _clipboard = clipboard;
        }
        else
        {
            throw new InvalidOperationException("Avalonia application does not have a clipboard.");
        }
    }

    public Task CopyToClipboard(string text) => _clipboard.SetTextAsync(text);
}