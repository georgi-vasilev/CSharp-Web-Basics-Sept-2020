namespace BattleCards.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BattleCards.Data;
    using BattleCards.ViewModels.Cards;

    public class CardsService : ICardsService
    {
        private readonly ApplicationDbContext db;

        public CardsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int AddCard(AddCardInputModel input)
        {
            var card = new Card
            {
                Attack = input.Attack,
                Health = input.Health,
                Description = input.Description,
                ImageUrl = input.Image,
                Name = input.Name,
                KeyWord = input.Keyword,
            };

            this.db.Cards.Add(card);
            this.db.SaveChanges();

            return card.Id;
        }

        public void AddCardToUserCollection(string userId, int cardId)
        {
            if (this.db.UserCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }
            this.db.UserCards.Add(new UserCard
            {
                CardId = cardId,
                UserId = userId,
            });
            this.db.SaveChanges();
        }

        public IEnumerable<CardViewModel> GetAll()
        {
            return this.db.Cards.Select(x => new CardViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Attack = x.Attack,
                Description = x.Description,
                Health = x.Health,
                ImageUrl = x.ImageUrl,
                Type = x.KeyWord
            }).ToList();
        }

        public IEnumerable<CardViewModel> GetByUserId(string userId)
        {
            return this.db.UserCards
                .Where(x => x.UserId == userId)
                .Select(x => new CardViewModel
                {
                    Attack = x.Card.Attack,
                    Description = x.Card.Description,
                    Health = x.Card.Health,
                    Name = x.Card.Name,
                    ImageUrl = x.Card.ImageUrl,
                    Type = x.Card.KeyWord,
                    Id = x.CardId
                }).ToList();
        }

        public void RemoveCardFromUserCollection(string userId, int cardId)
        {
            var userCard = this.db.UserCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);
            if (userCard == null)
            {
                return;
            }
            this.db.UserCards.Remove(userCard);
            this.db.SaveChanges();
        }
    }
}
