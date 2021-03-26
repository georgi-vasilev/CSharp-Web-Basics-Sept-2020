namespace BattleCards.Controllers
{
    using System.Text.RegularExpressions;
    using System.ComponentModel.DataAnnotations;

    using SUS.HTTP;
    using SUS.MvcFramework;
    using BattleCards.Services;

    public class UsersController : Controller
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            //username validation
            if (username == null || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Invalid username. The username should be between 5 and 20 characters.");
            }
            if (!Regex.IsMatch(username, "^[a-zA-Z0-9.]+$"))
            {
                return this.Error("Invalid username. Only alphanumeric chracters are allowed.");
            }
            if (!this.userService.IsUsernameAvailable(username))
            {
                return this.Error("Username already taken.");
            }

            //email validation
            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email.");
            }
            if (!this.userService.IsEmailAvailable(email))
            {
                return this.Error("Email already taken.");
            }

            //password validation
            if (password == null || password.Length < 6 || password.Length > 20)
            {
                return this.Error("Invalid password. The password should be between 6 and 20 characters.");
            }
            if (password != confirmPassword)
            {
                return this.Error("Password should be the same.");
            }


            this.userService.CreateUser(username, email, password);
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

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.userService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Error("Invalid username or password");
            }

            this.SignIn(userId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse Logout()
        {
            if (this.IsUserSignedIn())
            {
                this.SingOut();
                return this.Redirect("/");
            }
            else
            {
                return this.Error("Only logged in users can logout.");
            }

        }
    }
}
