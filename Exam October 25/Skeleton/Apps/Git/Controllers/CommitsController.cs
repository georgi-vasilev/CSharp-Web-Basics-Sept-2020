namespace Git.Controllers
{
    using Git.Services;
    using Git.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;

    public class CommitsController : Controller
    {
        private readonly ICommitsService service;

        public CommitsController(ICommitsService service)
        {
            this.service = service;

        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repoName = this.service.GetNameById(id);
            var viewModel = new CreateCommitInputModel
            {
                Id = id,
                Name = repoName,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(description) || description.Length < 5)
            {
                return this.Error("Commit description length should be more than 5 characters long.");
            }

            var userId = this.GetUserId();
            this.service.Create(description, id, userId);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var commitsViewModel = this.service.GetAll();
            return this.View(commitsViewModel);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var userId = this.GetUserId();
            this.service.Delete(id, userId);
            return this.Redirect("/Commits/All");
        }
    }
}
