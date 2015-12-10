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
        public void DownRepositoryEnsureICanSearchByHandle()
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
        public void DownRepositoryEnsureICanSearchByName()
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
        public void DownRepositoryEnsureICanGetCompetitionsByUserHandle()
        {
            var list_of_comps = new List<Competition>
            {
                new Competition { Name = "Horses Rock" },
                new Competition { Name = "Belcher Gang" }
            };

            var list_of_users = new List<DownUser>
            {
                new DownUser { Handle = "unicornLover", Competition = list_of_comps },
            new DownUser { Handle = "burgerBob", Competition = list_of_comps }
            };

            mock_user_set.Object.AddRange(list_of_users);
            ConnectMocksToDataStore(list_of_users);

            string handle = "unicornLover";
            List<Competition> actual_user_comps = repository.GetUserCompetitions(handle);

            //Assert.AreEqual("Horses Rock", actual_user_comps.First().Name);
            CollectionAssert.AreEqual(list_of_comps, actual_user_comps);
        }
    }
}
