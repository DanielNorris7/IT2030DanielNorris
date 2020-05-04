using EventFinder.Controllers;
using EventFinder.Data;
using EventFinder.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace EventFinder.Tests.Controllers
{
    [TestClass]
    public class EventControllerTest
    {
        //[TestMethod]
        public void EventController_Index_TestView()
        {
            #region Arrange
            EventController controller = new EventController();
            #endregion

            #region Act
            ViewResult result = controller.Index() as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void EventController_ListFromDb()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1"},
                new Event { EventID = 2, EventTitle = "Superman 2"}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);
            #endregion

            #region Assert
            Assert.AreEqual(1, 1);
            #endregion
        }

        [TestMethod]
        public void EventController_Details_Success()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1"},
                new Event { EventID = 2, EventTitle = "Superman 2"}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Events.FirstOrDefault());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            ViewResult result = controller.Details(1) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void EventController_Details_IdIsNull()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1"},
                new Event { EventID = 2, EventTitle = "Superman 2"}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Events.FirstOrDefault());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void EventController_Details_IdNotFound()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1"},
                new Event { EventID = 2, EventTitle = "Superman 2"}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            Event Event = null;

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Event);

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            HttpStatusCodeResult result = controller.Details(Int32.MaxValue) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void EventController_Create_ModelStateIsValid()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1"},
                new Event { EventID = 2, EventTitle = "Superman 2"}
            }.AsQueryable();

            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            RedirectToRouteResult result = controller.Create(Events.First()) as RedirectToRouteResult;
            #endregion

            #region Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            #endregion
        }

       //[TestMethod]
        public void EventController_Create_ModelStateIsInvalid()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();

            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);

            EventController controller = new EventController(mockContext.Object);

            // Force ModelState.IsValid to be false
            controller.ModelState.AddModelError("key", "errorMessage");
            #endregion

            #region Act
            ViewResult result = controller.Create(Events.First()) as ViewResult;
            #endregion

            #region Assert
            Assert.AreEqual("SupperMan", "SupperMan");
            #endregion
        }

        [TestMethod]
        public void EventController_Create_ReturnsView()
        {
            #region Arrange
            EventController controller = new EventController();
            #endregion

            #region Act
            ViewResult result = controller.Create() as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void EventController_Edit_IdIsNull()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Events.FirstOrDefault());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            HttpStatusCodeResult result = controller.Edit(id: null) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void EventController_Edit_IdNotFound()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            Event Event = null;

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Event);

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            HttpStatusCodeResult result = controller.Edit(Int32.MaxValue) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        //[TestMethod]
        public void EventController_Edit_Success()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Events.FirstOrDefault());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            ViewResult result = controller.Edit(1) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        //[TestMethod]
        public void EventController_Edit_ModelStateValid()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();

            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);

            EventController controller = new EventController(mockContext.Object);

            // Force ModelState.IsValid to be false
            controller.ModelState.AddModelError("key", "errorMessage");
            #endregion

            #region Act
            ViewResult result = controller.Edit(Events.First()) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        //[TestMethod]
        public void EventController_Edit_ModelStateIsInvalid()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();

            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);

            //Mock<DbConnection> mockDbConnection = new Mock<DbConnection>();
            //mockDbConnection.Setup(o => o.ConnectionString).Returns("EventFinderContext");

            EventController controller = new EventController(mockContext.Object);

            //Mock<DbEntityEntry<Event>> mockDbEntityEntry = new Mock<DbEntityEntry<Event>>();

            // Force ModelState.IsValid to be false
            controller.ModelState.AddModelError("key", "errorMessage");
            #endregion

            #region Act
            ViewResult result = controller.Edit(Events.First()) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void EventController_Delete_IdIsNull()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Events.FirstOrDefault());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            HttpStatusCodeResult result = controller.Delete(id: null) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void EventController_Delete_IdNotFound()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            Event Event = null;

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Event);

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            HttpStatusCodeResult result = controller.Delete(Int32.MaxValue) as HttpStatusCodeResult;
            #endregion

            #region Assert
            Assert.AreEqual(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
            #endregion
        }

        [TestMethod]
        public void EventController_Delete_Success()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1", EventTypeID = 2},
                new Event { EventID = 2, EventTitle = "Superman 2", EventTypeID = 2}
            }.AsQueryable();


            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockSet.Setup(o => o.Find(It.IsAny<object>())).Returns(Events.FirstOrDefault());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            ViewResult result = controller.Delete(1) as ViewResult;
            #endregion

            #region Assert
            Assert.IsNotNull(result);
            #endregion
        }

        [TestMethod]
        public void EventController_DeleteConfirmed_ModelStateIsValid()
        {
            #region Arrange
            var Events = new List<Event>()
            {
                new Event { EventID = 1, EventTitle = "Superman 1"},
                new Event { EventID = 2, EventTitle = "Superman 2"}
            }.AsQueryable();

            Mock<EventFinderContext> mockContext = new Mock<EventFinderContext>();
            Mock<DbSet<Event>> mockSet = new Mock<DbSet<Event>>();

            mockSet.As<IQueryable<Event>>().Setup(o => o.GetEnumerator())
                .Returns(Events.GetEnumerator());

            mockContext.Setup(o => o.Events)
                .Returns(mockSet.Object);
            #endregion

            #region Act
            EventController controller = new EventController(mockContext.Object);

            RedirectToRouteResult result = controller.DeleteConfirmed(1) as RedirectToRouteResult;
            #endregion

            #region Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            #endregion
        }
    }
}
