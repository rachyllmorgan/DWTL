using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DWTL.Models;
using System.Collections.Generic;

namespace DWTL.Tests.Models
{
    [TestClass]
    public class CompetitionTests
    {
        [TestMethod]
        public void CompetitionEnsureICanCreateAnInstance()
        {
            Competition a_comp = new Competition();
            Assert.IsNotNull(a_comp);
        }
        [TestMethod]
        public void CompetitionEnsureCompetitionHasAllProperties()
        {
            Competition a_competition = new Competition();
            a_competition.Name = "Pizza Lovers!";
            a_competition.Bet = 30;
            a_competition.Pot = 30;
            a_competition.DownUserId = 1;
            a_competition.CompType = CompType.Weight;

            Assert.AreEqual("Pizza Lovers!", a_competition.Name);
            Assert.AreEqual(30, a_competition.Bet);
            Assert.AreEqual(30, a_competition.Pot);
            Assert.AreEqual(1, a_competition.DownUserId);
            Assert.AreEqual(CompType.Weight, a_competition.CompType);
        }

        [TestMethod]
        public void CompetitionEnsureCompetitionHasMembers()
        {
            List<DownUser> list_of_members = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" }
            };
            Competition a_competition = new Competition { Name = "Belcher Gang", Members = list_of_members};
            List<DownUser> actual_members = a_competition.Members;
            CollectionAssert.AreEqual(list_of_members, actual_members);
        }

        [TestMethod]
        public void CompetitionEnsureCompetitionHasPosts()
        {
            DateTime expected_time = DateTime.Now;
            List<Post> list_of_posts = new List<Post>
            {
                new Post { Content = "Yo!", Date = expected_time},
                new Post { Content = "Nice try!", Date = expected_time }
            };
            Competition a_competition = new Competition { Name = "Pizza Lovers!", Posts = list_of_posts };
            List<Post> actual_posts = a_competition.Posts;
            CollectionAssert.AreEqual(list_of_posts, actual_posts);
        }
    }
}
