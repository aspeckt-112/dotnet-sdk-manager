namespace SdkManager.Services;

public class CsvUtilityService
{
    public async Task SaveAsCsv<T>(IEnumerable<T> data, string path)
    {
        // TODO This is janky. Do it properly.
        await using TextWriter writer = File.CreateText(path);

        foreach (T row in data)
        {
            string csvRow = string.Join(",", row.GetType().GetProperties()
                .Select(prop => prop.GetValue(row)?.ToString() ?? string.Empty));

            await writer.WriteLineAsync(csvRow);
        }
    }
}