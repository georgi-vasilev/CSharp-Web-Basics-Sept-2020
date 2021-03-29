using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        //TODO: bug with the repo id 
        public string Create(string description, string id, string userId, string repoId)
        {
            var repo = this.db.Repositories
                .Where(x => x.Commits.Any(x => x.RepositoryId == id))
                .FirstOrDefault();

            var creator = this.db.Users.Where(x => x.Id == userId)
                .FirstOrDefault();

            var commit = new Commit
            {
                CreatedOn = DateTime.UtcNow,
                Description = description,
                CreatorId = creator.Id,
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
            return this.db.Commits.Select(x => new CommitsViewModel
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
