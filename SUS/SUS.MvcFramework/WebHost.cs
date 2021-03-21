namespace SUS.MvcFramework
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using SUS.HTTP;

    public static class Host
    {
        public static async Task CreateHostAsync(List<Route> routeTable, int port=80)
        {
            IHttpServer server = new HttpServer(routeTable);

            await server.StartAsync(port);
        }
    }
}
