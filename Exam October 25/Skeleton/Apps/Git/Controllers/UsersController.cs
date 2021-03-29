namespace Git.Controllers
{
    using System.ComponentModel.DataAnnotations;

    using Git.Services;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class UsersController : Controller
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrWhiteSpace(username) || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Username should be between 5 and 20 characters long.");
            }
            if (!this.service.IsUsernameAvailable(username))
            {
                return this.Error("Username is not available.");
            }

            if (string.IsNullOrWhiteSpace(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email address.");
            }
            if (!this.service.IsEmailAvailable(email))
            {
                return this.Error("Email address is already taken.");
            }

            if (string.IsNullOrEmpty(password) || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters long.");
            }
            if (password != confirmPassword)
            {
                return this.Error("Passwords should match!");
            }

            this.service.CreateUser(username, email, password);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.service.GetUserId(username, password);
            if (userId == null)
            {
                return this.Error("Invalid username or password!");
            }

            this.SignIn(userId);
            return this.Redirect("/Repositories/All");
        }
        public HttpResponse Logout()
        {
            if (this.IsUserSignedIn())
            {
                this.SignOut();
                return this.Redirect("/");
            }
            else
            {
                return this.Error("Only logged in users can logout.");
            }

        }
    }
}
