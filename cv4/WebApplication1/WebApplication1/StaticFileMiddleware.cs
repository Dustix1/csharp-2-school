namespace WebApplication1
{
    public class StaticFileMiddleware
    {
        private readonly RequestDelegate next;

        public StaticFileMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string dir = @"C:\Users\KAN0316\Desktop";

            string path = context.Request.Path.Value.TrimStart(new char[] { '/', '.' });

            string filePath = Path.Combine(dir, path);

            if (File.Exists(filePath))
            {
                context.Response.Headers.ContentType = "image/jpeg";
                await context.Response.SendFileAsync(filePath);
            }

            await next(context);
        }
    }
}
