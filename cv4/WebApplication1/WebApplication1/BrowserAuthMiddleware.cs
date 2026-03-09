namespace WebApplication1
{
    public class BrowserAuthMiddleware
    {
        private readonly RequestDelegate next;

        public BrowserAuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string ua = context.Request.Headers.UserAgent.First();

            if (ua.Contains("Chrome"))
            {
                await next(context);
            } else
            {
                context.Response.Headers.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("použij chrome!");
            }
        }
    }
}
