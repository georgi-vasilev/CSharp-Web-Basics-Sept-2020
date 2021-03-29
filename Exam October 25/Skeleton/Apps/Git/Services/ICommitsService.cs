namespace Git.Services
{
    using System.Collections.Generic;

    using Git.ViewModels;

    public interface ICommitsService
    {
        string Create(string description, string id, string userId);

        void Delete(string id, string userId);

        IEnumerable<CommitsViewModel> GetAll();

        string GetNameById(string id);
    }
}
