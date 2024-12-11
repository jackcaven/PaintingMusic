using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SketchpadServer.Classes;

class Program
{
    static async Task Main(string[] args)
    {
       IServiceProvider serviceProvider = ConfigureServices;

        WebsocketServer sketchpadServer = serviceProvider.GetRequiredService<WebsocketServer>();

        await sketchpadServer.Run();
    }

    private static IServiceProvider ConfigureServices
    {
        get
        {
            ServiceCollection services = new();

            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.AddConsole();
            });

            services.AddSingleton<WebsocketServer>();

            return services.BuildServiceProvider();
        }
    }
}
