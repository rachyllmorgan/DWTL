using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DWTL.Models;
using System.Collections.Generic;

namespace DWTL.Tests.Models
{
    [TestClass]
    public class DownUserTests
    {
        [TestMethod]
        public void DownUserEnsureICanCreateAnInstance()
        {
            DownUser a_user = new DownUser();
            Assert.IsNotNull(a_user);
        }

        [TestMethod]
        public void DownUserEnsureUserHasAllProperties()
        {
            DownUser a_user = new DownUser();
            a_user.FirstName = "Tina";
            a_user.LastName = "Belcher";
            a_user.Handle = "unicornLover";
            a_user.Weight = 110;
            a_user.Picture = "http://placecorgi.com/260/180";

            Assert.AreEqual("Tina", a_user.FirstName);
            Assert.AreEqual("Belcher", a_user.LastName);
            Assert.AreEqual("unicornLover", a_user.Handle);
            Assert.AreEqual(110, a_user.Weight);
            Assert.AreEqual("http://placecorgi.com/260/180", a_user.Picture);
        }

        [TestMethod]
        public void DownUserEnsureUserHasCompetitions()
        {
            List<Competition> list_of_competitions = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Dropping pounds" }
            };
            DownUser a_user = new DownUser { Handle = "unicornLover", Competition = list_of_competitions };
            List<Competition> actual_competitions = a_user.Competition;
            CollectionAssert.AreEqual(actual_competitions, list_of_competitions);
        }

        [TestMethod]
        public void DownUserEnsureUserCanJoinCompetitions()
        {
            List<Competition> list_of_competitions = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Dropping Pounds" }
            };
            DownUser a_user = new DownUser { Handle = "unicornLover", Competition = list_of_competitions };
            List<Competition> actual_competitions = a_user.Competition;
            CollectionAssert.AreEqual(list_of_competitions, actual_competitions);
        }
    }
}
