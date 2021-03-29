namespace Git.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Git.Data;
    using Git.ViewModels;

    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string Create(string description, string repoId, string userId)
        {
            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                Description = description,
                CreatorId = userId,
                RepositoryId = repoId,
            };

            this.db.Commits.Add(commit);
            this.db.SaveChanges();
            return commit.Id;
        }

        public void Delete(string id, string userId)
        {
            var commit = this.db.Commits
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (commit.CreatorId == userId)
            {
                this.db.Commits.Remove(commit);
                this.db.SaveChanges();
            }
        }

        public IEnumerable<CommitsViewModel> GetAll()
        {
            return this.db.Commits.Include(x => x.Repository).Select(x => new CommitsViewModel
            {
                Id = x.Id,
                Name = x.Repository.Name,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
            }).ToList();
        }



        public string GetNameById(string id)
        {
            return this.db.Repositories.Where(x => x.Id == id)
                 .Select(y => y.Name)
                 .FirstOrDefault();
        }
    }
}
