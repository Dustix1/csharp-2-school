namespace WebApplication1
{
    public class TxtLogger : IMyLogger
    {
        public async Task Log(string message)
        {
            await File.AppendAllTextAsync("txtlog.txt", message + "\n\n");
        }
    }
}
