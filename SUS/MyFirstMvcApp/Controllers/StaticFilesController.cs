namespace MyFirstMvcApp.Controllers
{
    using System.IO;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class StaticFilesController : Controller
    {
        public HttpResponse Favicon(HttpRequest arg)
        {
            var fileBytes = File.ReadAllBytes("wwwroot/monitor.ico");
            var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);

            return response;
        }
    }
}
