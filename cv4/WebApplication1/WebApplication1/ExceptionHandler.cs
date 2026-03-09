namespace WebApplication1
{
    public class ExceptionHandler
    {
        private readonly IMyLogger logger;

        public ExceptionHandler(IMyLogger logger) {
            this.logger = logger;
        }

        public async Task Handle(Exception exception)
        {
            string message = exception.Message + "\n" + exception.StackTrace;
            await logger.Log(message);
            //await File.AppendAllTextAsync("log.txt", exception.Message + "\n" + exception.StackTrace + "\n\n");
        }
    }
}
 