namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<ExceptionHandler>();
            //builder.Services.AddTransient<ExceptionHandler>();        // lmao prý že podívejte se do přednášky na vysvětlení
            //builder.Services.AddSingleton<ExceptionHandler>();

            builder.Services.AddScoped<IMyLogger, TxtLogger>();

            var app = builder.Build();

            //app.UseDeveloperExceptionPage();

            app.UseMiddleware<ErrorMiddleware>();
            app.UseMiddleware<BrowserAuthMiddleware>();
            app.UseMiddleware<FormMiddleware>();
            app.UseMiddleware<StaticFileMiddleware>();
            app.UseMiddleware<FirstMiddleware>();

            //app.MapGet("/", () => ":3");

            app.Run();
        }
    }
}
