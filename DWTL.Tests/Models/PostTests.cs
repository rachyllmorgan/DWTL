using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DWTL.Models;

namespace DWTL.Tests.Models
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void PostEnsureICanCreateAnInstance()
        {
            Post a_post = new Post();
            Assert.IsNotNull(a_post);
        }
        [TestMethod]
        public void PostEnsurePostHasAllProperties()
        {
            Post a_post = new Post();
            DateTime expected_time = DateTime.Now;
            a_post.PostId = 1;
            a_post.Author = null;
            a_post.Content = "You got this!";
            a_post.Date = expected_time;
            a_post.Picture = "http://placecorgi.com/260/180";
            a_post.Competition = null;

            Assert.AreEqual(1, a_post.PostId);
            Assert.AreEqual(null, a_post.Author);
            Assert.AreEqual("You got this!", a_post.Content);
            Assert.AreEqual(expected_time, a_post.Date);
            Assert.AreEqual("http://placecorgi.com/260/180", a_post.Picture);
            Assert.AreEqual(null, a_post.Competition);
        }
        [TestMethod]
        public void PostEnsureICanUseObjectInitializerSyntax()
        {
            DateTime expected_time = DateTime.Now;
            Post a_post = new Post { PostId = 1, Author = null, Content = "You got this!", Date = expected_time, Picture = "http://placecorgi.com/260/180", Competition = null };

            Assert.AreEqual(1, a_post.PostId);
            Assert.AreEqual(null, a_post.Author);
            Assert.AreEqual("You got this!", a_post.Content);
            Assert.AreEqual(expected_time, a_post.Date);
            Assert.AreEqual("http://placecorgi.com/260/180", a_post.Picture);
            Assert.AreEqual(null, a_post.Competition);
        }
    }
}
