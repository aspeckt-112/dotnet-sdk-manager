using System.Text.Json;

namespace SdkManager.Services;

public class JsonUtilityService
{
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ClipboardService _clipboardService;

    public JsonUtilityService(ClipboardService clipboardService)
    {
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        _clipboardService = clipboardService;
    }

    public Task CopyJsonToClipboard<T>(T obj)
    {
        string json = JsonSerializer.Serialize(obj, _jsonOptions);
        return _clipboardService.CopyToClipboard(json);
    }
}