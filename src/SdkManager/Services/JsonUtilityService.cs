using System.Text.Json;

using Avalonia.Input.Platform;

namespace SdkManager.Services;

public class JsonUtilityService
{
    private readonly IClipboard _clipboard;
    private readonly JsonSerializerOptions _jsonOptions;

    public JsonUtilityService(IClipboard clipboard)
    {
        _clipboard = clipboard;

        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };
    }

    public Task CopyJsonToClipboard<T>(T obj)
    {
        string json = JsonSerializer.Serialize(obj, _jsonOptions);

        return _clipboard.SetTextAsync(json);
    }
}