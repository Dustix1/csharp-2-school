using System.Security.Cryptography.X509Certificates;

namespace WebApplication1
{
    public class FirstMiddleware
    {
        public FirstMiddleware(RequestDelegate next)
        {
        }
        public async Task Invoke(HttpContext context)
        {
            //string path = context.Request.Path.Value;

            //throw new NotImplementedException();

            context.Response.Headers.ContentType = "text/html; charset=utf-8";

            await context.Response.WriteAsync(@"
                <html>
                    <head>
                        <title>Název strány</title>
                    </head>
                    <body>
                        <h1>Nadpis stránky</h1>
                    </body>
                </html>
            ");
        }
    }
}
