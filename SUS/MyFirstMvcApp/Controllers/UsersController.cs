namespace MyFirstMvcApp.Controllers
{
    using System.Text;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class UsersController : Controller
    {
        public HttpResponse Register(HttpRequest arg)
        {
            var responseHtml = "<h1>Register...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }

        public HttpResponse Login(HttpRequest request)
        {
            var responseHtml = "<h1>Login...</h1>";
            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }
    }
}
