namespace Git.ViewModels
{
    using System;

    public class RepositoriesViewModel
    {
        public string  Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommitsCount { get; set; }
    }
}
