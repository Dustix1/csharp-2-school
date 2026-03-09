namespace WebApplication1
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context, ExceptionHandler handler)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                context.Response.Headers.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("chyba lmao");

                await handler.Handle(e);
            }
        }
    }
}
