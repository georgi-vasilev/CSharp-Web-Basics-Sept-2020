namespace Git.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Git.Data;
    using Git.ViewModels;

    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string name, bool type, string userId)
        {         
            var repository = new Repository
            {
                Name = name,
                CreateOn = DateTime.UtcNow,
                IsPublic = type,
                OwnerId = userId,
                
            };
            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
            return repository?.Id;
        }

        public IEnumerable<RepositoriesViewModel> GetAll()
        {
            return this.db.Repositories.Select(x => new RepositoriesViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = x.CreateOn,
                CommitsCount = x.Commits.Count()
            }).ToList();
        }
    }
}
