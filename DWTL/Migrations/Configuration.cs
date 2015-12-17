namespace DWTL.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DWTL.Models.DownContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DWTL.Models.DownContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var down_users = new List<DownUser>
            {
            new DownUser { FirstName ="Tina", LastName="Belcher", Handle = "unicornLover" },
            new DownUser { FirstName ="Luiz", LastName="Belcher", Handle = "lovelyLinda" },
            new DownUser { FirstName ="Jimmy", LastName="Pesto Sr.", Handle = "pizzaJim" },
            new DownUser { FirstName ="Teddy", LastName="Francisco", Handle = "tedster" },
            new DownUser { FirstName ="Bob", LastName="Belcher", Handle = "burgerBob" },
            };

            down_users.ForEach(user => context.DownUsers.AddOrUpdate(user));
            context.SaveChanges();

            var comps = new List<Competition>
            {
                new Competition{ Name = "Belcher Gang", CompType = CompType.Weight, Bet = 25, DownUserId = 1 },
                new Competition{ Name = "Run, Run, Run!", CompType = CompType.Dist_Ran, Bet = 10, DownUserId = 3 },
                new Competition{ Name = "Dropping pounds", CompType = CompType.Weight, Bet = 30, DownUserId = 2 },
                new Competition{ Name = "Distance Challenge", CompType = CompType.Dist_Ran, Bet = 5, DownUserId = 2 },
                new Competition{ Name = "New Years Resolution", CompType = CompType.Weight, Bet = 25, DownUserId = 4 },
            };
            comps.ForEach(comp => context.Competitions.AddOrUpdate(comp));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post{ CompetitionId = 1, Content = "Yay", Date = DateTime.Parse("2015-09-01")},
                new Post{ CompetitionId = 1, Content = "Hello", Date = DateTime.Parse("2015-10-01")},
                new Post{ CompetitionId = 1, Content = "Bye!", Date = DateTime.Parse("2015-11-01")},
                new Post{ CompetitionId = 2, Content = "Toodles!", Date = DateTime.Parse("2015-11-11")},
                new Post{ CompetitionId = 2, Content = "Great Job!", Date = DateTime.Parse("2015-11-13")},
                new Post{ CompetitionId = 3, Content = "Keep it Up!", Date = DateTime.Parse("2015-11-21")},
            };
            posts.ForEach(post => context.Posts.AddOrUpdate(post));
            context.SaveChanges();
        }
    }
}
