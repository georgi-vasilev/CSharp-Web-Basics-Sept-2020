namespace BattleCards
{
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using SUS.HTTP;
    using SUS.MvcFramework;
    using BattleCards.Data;

    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices()
        {
        }
    }
}
