namespace SUS.MvcFramework
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using SUS.HTTP;

    public static class Host
    {
        public static async Task CreateHostAsync(IMvcApplication application, int port=80)
        {
            List<Route> routeTable = new List<Route>();
            application.Configure(routeTable);
            application.ConfigureServices();

            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }
    }
}
