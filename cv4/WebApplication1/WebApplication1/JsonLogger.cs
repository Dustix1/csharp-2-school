using System.Text.Json;

namespace WebApplication1
{
    public class JsonLogger : IMyLogger
    {
        public async Task Log(string message)
        {
            string path = "log.json";
            List<string> result;

            if (File.Exists(path))
            {
                result = JsonSerializer.Deserialize<List<string>>(
                    await File.ReadAllTextAsync(path)
                )!;
            } else
            {
                result = new List<string>();
            }
            result.Add(message);

            await File.WriteAllTextAsync(path, JsonSerializer.Serialize(result));
        }
    }
}
