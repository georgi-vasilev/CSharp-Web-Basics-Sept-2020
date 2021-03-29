namespace Git.Services
{
    using System.Collections.Generic;

    using Git.ViewModels;

    public interface IRepositoriesService
    {
        string Create(string name, bool type, string userId);

        IEnumerable<RepositoriesViewModel> GetAll();
    }
}
