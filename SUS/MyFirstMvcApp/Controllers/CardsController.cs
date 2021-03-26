namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.ViewModels;
    using SUS.HTTP;
    using SUS.MvcFramework;
    using System.Linq;

    public class CardsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CardsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd(string attack, string health, string description, string image, string name, string keyword)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }


            if (this.Request.FormData["name"].Length < 5)
            {
                return this.Error("Name should be at least 5 characters long");
            }

            this.db.Cards.Add(new Card
                {
                    Attack = int.Parse(attack),
                    Health = int.Parse(health),
                    Description = description,
                    ImageUrl = image,
                    Name = name,
                    KeyWord = keyword,
                });

            this.db.SaveChanges();

            return this.Redirect("/Cards/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var cardsViewModel = this.db.Cards.Select(x => new CardViewModel
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
