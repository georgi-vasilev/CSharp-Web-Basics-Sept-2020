using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService service;

        public RepositoriesController(IRepositoriesService service)
        {
            this.service = service;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string type)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 10)
            {
                return this.Error("Name should be between 3 and 10 characters long.");
            }
            var isPublic = false;
            if (type == "Public")
            {
                isPublic = true;
            }
            this.service.Create(name, isPublic, this.GetUserId());

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            var repositoriesViewModel = this.service.GetAll();
            return this.View(repositoriesViewModel);
        }
    }
}
