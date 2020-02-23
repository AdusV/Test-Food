namespace FoodBase.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FoodBase.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FoodBase.Data.FoodBaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FoodBase.Data.FoodBaseContext context)
        {
            context.Prods.AddOrUpdate(x => x.Id,
                new Prod() { Id = 1, Name = "Jogurt" },
                new Prod() { Id = 2, Name = "Kurczak" },
                new Prod() { Id = 3, Name = "Ryba" }
                );
        }
    }
}
