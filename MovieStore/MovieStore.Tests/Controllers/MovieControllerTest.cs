using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MovieStore.Controllers;
using MovieStore.Data;
using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MovieStore.Tests.Controllers
{
    [TestClass]
    public class MovieControllerTest
    {
        [TestMethod]
        public void MovieStore_Index_TestView()
        {
            #region Arrange
            MoviesController controller = new MoviesController();
            #endregion

            #region Act
            ViewResult result = controller.Index() as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_ListOfMovies()
        {
            #region Arrange
            MoviesController controller = new MoviesController();
            #endregion

            #region Act
            List<Movie> result = controller.ListOfMovies();
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Iron Man 1", result[0].Title);
            Assert.AreEqual("Iron Man 2", result[1].Title);
            Assert.AreEqual("Iron Man 3", result[2].Title);
            #endregion
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_Success()
        {
            #region Arrange
            MoviesController controller = new MoviesController();
            #endregion

            #region Act
            RedirectToRouteResult result = controller.IndexRedirect(1) as RedirectToRouteResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.RouteValues["action"]);
            Assert.AreEqual("HomeController", result.RouteValues["controller"]);
            #endregion
        }

        [TestMethod]
        public void MovieStore_IndexRedirect_Failure()
        {
            #region Arrange
            MoviesController controller = new MoviesController();
            #endregion

            #region Act
            HttpStatusCodeResult result = controller.IndexRedirect(0) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_ListFromDb()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.ListFromDb() as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Details_Success()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movies.FirstOrDefault());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.Details(1) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Details_IdIsNull()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movies.FirstOrDefault());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Details_IdNotFound()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            Movie movie = null;

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movie);

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Details(Int32.MaxValue) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Create_ModelStateIsValid()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1", YearRelease = 2019},
                new Movie { MovieId = 2, Title = "Superman 2", YearRelease = 2007}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            RedirectToRouteResult result = controller.Create(movies.First()) as RedirectToRouteResult;
            #endregion

            #region Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Create_ModelStateIsInvalid()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1", YearRelease = 2019},
                new Movie { MovieId = 2, Title = "Superman 2", YearRelease = 2007}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            // Force ModelState.IsValid to be false
            controller.ModelState.AddModelError("key", "errorMessage");
            #endregion

            #region Act
            ViewResult result = controller.Create(movies.First()) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Create_ReturnsView()
        {
            #region Arrange
            MoviesController controller = new MoviesController();
            #endregion

            #region Act
            ViewResult result = controller.Create() as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Edit_IdIsNull()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movies.FirstOrDefault());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Edit(id: null) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Edit_IdNotFound()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            Movie movie = null;

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movie);

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Edit(Int32.MaxValue) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Edit_Success()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movies.FirstOrDefault());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.Edit(1) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Edit_ModelStateValid()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1", YearRelease = 2019},
                new Movie { MovieId = 2, Title = "Superman 2", YearRelease = 2007}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);

            MoviesController controller = new MoviesController(mockContext.Object);

            // Force ModelState.IsValid to be false
            controller.ModelState.AddModelError("key", "errorMessage");
            #endregion

            #region Act
            ViewResult result = controller.Edit(movies.First()) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Edit_ModelStateIsInvalid()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1", YearRelease = 2019},
                new Movie { MovieId = 2, Title = "Superman 2", YearRelease = 2007}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);

            //Mock<DbConnection> mockDbConnection = new Mock<DbConnection>();
            //mockDbConnection.Setup(o => o.ConnectionString).Returns("MovieStoreDbContext");

            MoviesController controller = new MoviesController(mockContext.Object);

            //Mock<DbEntityEntry<Movie>> mockDbEntityEntry = new Mock<DbEntityEntry<Movie>>();

            // Force ModelState.IsValid to be false
            controller.ModelState.AddModelError("key", "errorMessage");
            #endregion

            #region Act
            ViewResult result = controller.Edit(movies.First()) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Delete_IdIsNull()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movies.FirstOrDefault());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Delete(id: null) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Delete_IdNotFound()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            Movie movie = null;

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movie);

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            HttpStatusCodeResult result = controller.Delete(Int32.MaxValue) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void MovieStore_Delete_Success()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1"},
                new Movie { MovieId = 2, Title = "Superman 2"}
            }.AsQueryable();


            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(movies.FirstOrDefault());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            ViewResult result = controller.Delete(1) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void MovieStore_DeleteConfirmed_ModelStateIsValid()
        {
            #region Arrange
            var movies = new List<Movie>()
            {
                new Movie { MovieId = 1, Title = "Superman 1", YearRelease = 2019},
                new Movie { MovieId = 2, Title = "Superman 2", YearRelease = 2007}
            }.AsQueryable();

            Mock<MovieStoreDbContext> mockContext = new Mock<MovieStoreDbContext>();
            Mock<DbSet<Movie>> mockSet = new Mock<DbSet<Movie>>();

            mockSet.As<IQueryable<Movie>>().Setup(o => o.GetEnumerator())
                .Returns(movies.GetEnumerator());

            mockContext.Setup(o => o.Movies)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            MoviesController controller = new MoviesController(mockContext.Object);

            RedirectToRouteResult result = controller.DeleteConfirmed(1) as RedirectToRouteResult;
            #endregion

            #region Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            #endregion
        }
    }
}
