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
            a_competition.Author = null;

            Assert.AreEqual("Pizza Lovers!", a_competition.Name);
            Assert.AreEqual(30, a_competition.Bet);
            Assert.AreEqual(30, a_competition.Pot);
            Assert.AreEqual(null, a_competition.Author);
        }

        //[TestMethod]
        //public void GroupEnsureGroupHasTeams()
        //{
        //    List<Team> list_of_teams = new List<Team>
        //    {
        //        new Team { Name = "The best!" },
        //        new Team { Name = "We try!" }
        //    };
        //    Competition a_group = new Competition { Name = "Pizza Lovers!", Teams = list_of_teams };
        //    List<Team> actual_teams = a_group.Teams;
        //    CollectionAssert.AreEqual(list_of_teams, actual_teams);
        //}

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
