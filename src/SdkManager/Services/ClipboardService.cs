using Avalonia;
using Avalonia.Input.Platform;

using SdkManager.Extensions;

namespace SdkManager.Services;

public class ClipboardService
{
    private readonly IClipboard _clipboard;

    public ClipboardService()
    {
        if (Application.Current?.GetDesktopLifetime().MainWindow?.Clipboard is { } clipboard)
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