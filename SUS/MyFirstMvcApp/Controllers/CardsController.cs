namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Linq;

    public class CardsController : Controller
    {
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var dbContext = new ApplicationDbContext();

            if (this.Request.FormData["name"].Length < 5)
            {
                return this.Error("Name should be at least 5 characters long");
            }

            dbContext.Cards.Add(new Card
                {
                    Attack = int.Parse(this.Request.FormData["attack"]),
                    Health = int.Parse(this.Request.FormData["health"]),
                    Description = this.Request.FormData["description"],
                    ImageUrl = this.Request.FormData["image"],
                    Name = this.Request.FormData["name"],
                    KeyWord = this.Request.FormData["keyword"],
                });

            dbContext.SaveChanges();

            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var db = new ApplicationDbContext();
            var cardsViewModel = db.Cards.Select(x => new CardViewModel
            {
                Name = x.Name,
                Attack = x.Attack,
                Description = x.Description,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Type = x.KeyWord
            }).ToList();

            return this.View(cardsViewModel);
        }
        public HttpResponse Collection()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }
    }
}
