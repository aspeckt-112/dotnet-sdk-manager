namespace SdkManager.Services.Abstractions;

public interface IClipboardService
{
    public Task CopyToClipboard(string text);
}