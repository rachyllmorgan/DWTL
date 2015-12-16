using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DWTL.Models;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace DWTL.Tests.Models
{
    [TestClass]
    public class DownRepositoryTests
    {
        private Mock<DownContext> mock_context;
        private Mock<DbSet<DownUser>> mock_user_set;
        private Mock<DbSet<Post>> mock_post_set;
        private Mock<DbSet<Competition>> mock_comp_set;
        private DownRepository repository;

        private void ConnectMocksToDataStore(IEnumerable<DownUser> data_store)
        {
            var data_source = data_store.AsQueryable<DownUser>();

            mock_user_set.As<IQueryable<DownUser>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_user_set.As<IQueryable<DownUser>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_user_set.As<IQueryable<DownUser>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_user_set.As<IQueryable<DownUser>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the DownUsers property getter
            mock_context.Setup(a => a.DownUsers).Returns(mock_user_set.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Post> data_store)
        {
            var data_source = data_store.AsQueryable<Post>();
            // HINT HINT: var data_source = (data_store as IEnumerable<Jot>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_post_set.As<IQueryable<Post>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_post_set.As<IQueryable<Post>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_post_set.As<IQueryable<Post>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_post_set.As<IQueryable<Post>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Post property getter
            mock_context.Setup(a => a.Posts).Returns(mock_post_set.Object);
        }

        private void ConnectMocksToDataStore(IEnumerable<Competition> data_store)
        {
            var data_source = data_store.AsQueryable<Competition>();
            // HINT HINT: var data_source = (data_store as IEnumerable<Jot>).AsQueryable();
            // Convince LINQ that our Mock DbSet is a (relational) Data store.
            mock_comp_set.As<IQueryable<Competition>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_comp_set.As<IQueryable<Competition>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_comp_set.As<IQueryable<Competition>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_comp_set.As<IQueryable<Competition>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());

            // This is Stubbing the Competition property getter
            mock_context.Setup(a => a.Competitions).Returns(mock_comp_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<DownContext>();
            mock_user_set = new Mock<DbSet<DownUser>>();
            mock_post_set = new Mock<DbSet<Post>>();
            mock_comp_set = new Mock<DbSet<Competition>>();
            repository = new DownRepository(mock_context.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_user_set = null;
            mock_post_set = null;
            mock_comp_set = null;
            repository = null;
        }

        [TestMethod]
        public void DownContextEnsureICanCreateInstance()
        {
            DownContext context = mock_context.Object;
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanCreateInstance()
        {
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanGetAllUsers()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = repository.GetAllUsers();

            Assert.AreEqual("unicornLover", actual.First().Handle);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DownRepositoryEnsureIHaveAContext()
        {
            var actual = repository.Context;
            Assert.IsInstanceOfType(actual, typeof(DownContext));
        }

        [TestMethod]
        public void DownRepositoryEnsureICanGetUserByHandle()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "burgerBob";
            DownUser actual_user = repository.GetUserByHandle(handle);

            Assert.AreEqual(handle, actual_user.Handle);
        }

        [TestMethod]
        public void DownRepositoryGetUserByHandleThatDoesNotExist()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "jimmyPizza";
            DownUser actual_user = repository.GetUserByHandle(handle);

            Assert.IsNull(actual_user);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DownRepositoryGetUserByHandlesFailsWithMultipleUsers()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "unicornLover" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "unicornLover";
            DownUser actual_user = repository.GetUserByHandle(handle);
        }

        [TestMethod]
        public void DownRepositoryEnsureHandleIsAvailable()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "jimmyPizza";
            bool is_available = repository.IsHandleAvailable(handle);
            Assert.IsTrue(is_available);
        }

        [TestMethod]
        public void DownRepositoryEnsureHandleIsNotAvailable()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "burgerBob";
            bool is_available = repository.IsHandleAvailable(handle);
            Assert.IsFalse(is_available);
        }

        [TestMethod]
        public void DownRepositoryEnsureHandleIsNotAvailableMultipleUsers()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "burgerBob" },
                new DownUser { Handle = "burgerBob" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "burgerBob";
            bool is_available = repository.IsHandleAvailable(handle);
            Assert.IsFalse(is_available);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanSearchUserByHandle()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover" },
                new DownUser { Handle = "burgerBob" },
                new DownUser { Handle = "lovelyLinda" },
                new DownUser { Handle = "bobs_fav_kid" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string handle = "bob";

            var expected_users = new List<DownUser>
            {
                new DownUser { Handle = "bobs_fav_kid" },
                new DownUser { Handle = "burgerBob" }
            };

            List<DownUser> actual = repository.SearchByHandle(handle);

            Assert.AreEqual(expected_users[0].Handle, actual[0].Handle);
            Assert.AreEqual(expected_users[1].Handle, actual[1].Handle);
            
        }

        [TestMethod]
        public void DownRepositoryEnsureICanSearchUserByName()
        {
            var expected = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover", FirstName = "Tina", LastName = "Belcher" },
                new DownUser { Handle = "burgerBob", FirstName = "Bob", LastName = "Belcher" },
                new DownUser { Handle = "jimmyPizza", FirstName = "Jimmy", LastName = "Pesto" },
                new DownUser { Handle = "burgerLova", FirstName = "Teddy", LastName = "Francisco" }
            };
            mock_user_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string name_search = "belch";

            var expected_users = new List<DownUser>
            {
                new DownUser { Handle = "burgerBob", FirstName = "Bob", LastName = "Belcher" },
                new DownUser { Handle = "unicornLover", FirstName = "Tina", LastName = "Belcher" }
            };

            List<DownUser> actual = repository.SearchByName(name_search);

            Assert.AreEqual(expected_users[0].Handle, actual[0].Handle);
            Assert.AreEqual(expected_users[1].Handle, actual[1].Handle);

        }

        [TestMethod]
        public void DownRepositoryEnsureICanGetAllCompetitions()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Belcher Gang" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            var actual = repository.GetAllCompetitions();

            Assert.AreEqual("Pizza Lovers!", actual.First().Name);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanGetCompetitionByName()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Down with pounds" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string comp_name = "Pizza Lovers!";
            Competition actual_comp = repository.GetCompetitionByName(comp_name);

            Assert.AreEqual(comp_name, actual_comp.Name);
        }

        [TestMethod]
        public void DownRepositoryGetCompetitionByNameThatDoesNotExist()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Down with pounds" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string comp_name = "The Blah's";
            Competition actual_comp = repository.GetCompetitionByName(comp_name);

            Assert.IsNull(actual_comp);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DownRepositoryGetCompetitionByNameFailsWithMultipleCompetitions()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Pizza Lovers!" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string comp_name = "Pizza Lovers!";
            Competition actual_comp = repository.GetCompetitionByName(comp_name);
        }

        [TestMethod]
        public void DownRepositoryEnsureCompetitonNameIsAvailable()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Down With the Pounds" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string comp_name = "The Blah's";
            bool is_available = repository.IsCompetitionNameAvailable(comp_name);
            Assert.IsTrue(is_available);
        }

        [TestMethod]
        public void DownRepositoryEnsureCompetitionNameIsNotAvailable()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Down With the Pounds" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string comp_name = "Down With the Pounds";
            bool is_available = repository.IsCompetitionNameAvailable(comp_name);
            Assert.IsFalse(is_available);
        }

        [TestMethod]
        public void DownRepositoryEnsureCompetitionNameisNotAvailableWithMultipleCompetitions()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Pizza Lovers!" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string comp_name = "Pizza Lovers!";
            bool is_available = repository.IsCompetitionNameAvailable(comp_name);
            Assert.IsFalse(is_available);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanSearchCompetitionByName()
        {
            var expected = new List<Competition>
            {
                new Competition { Name = "Pizza Lovers!" },
                new Competition { Name = "Heavy Weights" },
                new Competition { Name = "Put down the pizza" },
                new Competition { Name = "Dropping pounds" }
            };
            mock_comp_set.Object.AddRange(expected);
            ConnectMocksToDataStore(expected);

            string name_search = "pizz";

            var expected_comps = new List<Competition>
            {
                 new Competition { Name = "Pizza Lovers!" },
                 new Competition { Name = "Put down the pizza" }
            };

            List<Competition> actual = repository.SearchCompetitionByName(name_search);

            Assert.AreEqual(expected_comps[0].Name, actual[0].Name);
            Assert.AreEqual(expected_comps[1].Name, actual[1].Name);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanGetAllCompetitionsByUserHandle()
        {
            var list_of_comps = new List<Competition>
            {
                new Competition { Name = "Horses Rock" },
                new Competition { Name = "Belcher Gang" }
            };
            var list_of_other_comps = new List<Competition>
            {
                new Competition { Name = "Blah" },
                new Competition { Name = "Blah blah" }
            };

            var list_of_users = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover", Competition = list_of_comps },
                new DownUser { Handle = "burgerBob", Competition = list_of_other_comps }
            };

            mock_user_set.Object.AddRange(list_of_users);
            ConnectMocksToDataStore(list_of_users);

            string handle = "unicornLover";
            List<Competition> actual_user_comps = repository.GetUserCompetitions(handle);

            Assert.AreEqual("Horses Rock", actual_user_comps.First().Name);
            CollectionAssert.AreEqual(list_of_comps, actual_user_comps);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanCreateACompetition()
        {
            List<Competition> expected_comps = new List<Competition>();

            ConnectMocksToDataStore(expected_comps);

            DownUser down_user1 = new DownUser { Handle = "unicornLover" };
            String comp_name = "The Belchers";

            mock_comp_set.Setup(p => p.Add(It.IsAny<Competition>())).Callback((Competition s) => expected_comps.Add(s));

            bool successful = repository.CreateCompetition(down_user1, comp_name);

            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void DownRepositoryEnsureICanGetAllPostByCompetitionName()
        {
            DateTime expected_time = DateTime.Now;

            List<Post> list_of_posts = new List<Post>
            {
                new Post { Content = "Yo!", Date = expected_time},
                new Post { Content = "Nice try!", Date = expected_time }
            };
            mock_post_set.Object.AddRange(list_of_posts);
            ConnectMocksToDataStore(list_of_posts);

            Competition a_competition = new Competition { Name = "Pizza Lovers!", Posts = list_of_posts };
            List<Post> actual_posts = a_competition.Posts;

            CollectionAssert.AreEqual(list_of_posts, actual_posts); ;
        }

        [TestMethod]
        public void DownRepositoryEnsureICanCreateAPost()
        {
            DateTime base_time = DateTime.Now;
            List<Post> expected_posts = new List<Post>();

            ConnectMocksToDataStore(expected_posts);

            DownUser down_user1 = new DownUser { Handle = "unicornLover" };
            Competition comp_name = new Competition { Name = "Belchers" };
            string content = "Hello DWTL World!";

            mock_post_set.Setup(p => p.Add(It.IsAny<Post>())).Callback((Post s) => expected_posts.Add(s));
            
            bool successful = repository.CreatePost(down_user1, comp_name, content);

            Assert.IsTrue(successful);
        }
    }
}
