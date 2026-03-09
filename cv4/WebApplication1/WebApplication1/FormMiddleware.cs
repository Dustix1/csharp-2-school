using System.Net;

namespace WebApplication1
{
    public class FormMiddleware
    {
        private readonly RequestDelegate next;

        public FormMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/form"))
            {
                await next(context);
                return;
            }

            context.Response.Headers.ContentType = "text/html";

            if (context.Request.Method == "POST")
            {
                var data = context.Request.Form;
                string name = data["jmeno"];

                await context.Response.WriteAsync(WebUtility.HtmlEncode(name));     // html encode aby nemohl uživatel dát skriptík
            }

            await context.Response.WriteAsync(@"
                <form method=""post"">
                    <input name=""jmeno"" />
                    <button type=""submit"">odeslat</button>
                </form>
            ");
        }
    }
}
