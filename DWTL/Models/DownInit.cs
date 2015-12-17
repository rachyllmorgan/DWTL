using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace DWTL.Models
//{
//    public class DownInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<DownContext>
//    {
//        protected override void Seed(DownContext context)
//        {
//            var down_users = new List<DownUser>
//            {
//            new DownUser { FirstName ="Tina", LastName="Belcher" },
//            new DownUser { FirstName ="Luiz", LastName="Belcher" },
//            new DownUser { FirstName ="Jimmy", LastName="Pesto Sr." },
//            new DownUser { FirstName ="Teddy", LastName="Francisco" },
//            new DownUser { FirstName ="Bob", LastName="Belcher" },
//            };

//            down_users.ForEach(user => context.DownUsers.Add(user));
//            context.SaveChanges();
//            var competitions = new List<Competition>
//            {
//                new Competition{ Name = "Belcher Gang", CompType = CompType.Weight, Bet = 25, DownUserId = 1 },
//                new Competition{ Name = "Run, Run, Run!", CompType = CompType.Dist_Ran, Bet = 10, DownUserId = 3 },
//                new Competition{ Name = "Dropping pounds", CompType = CompType.Weight, Bet = 30, DownUserId = 2 },
//                new Competition{ Name = "Distance Challenge", CompType = CompType.Dist_Ran, Bet = 5, DownUserId = 2 },
//                new Competition{ Name = "New Years Resolution", CompType = CompType.Weight, Bet = 25, DownUserId = 4 },
//            };
//            competitions.ForEach(comp => context.Competitions.Add(comp));
//            context.SaveChanges();
//        }
//    }
//}