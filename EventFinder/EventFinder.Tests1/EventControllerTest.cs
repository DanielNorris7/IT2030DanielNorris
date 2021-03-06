// <copyright file="EventControllerTest.cs">Copyright ©  2020</copyright>
using System;
using System.Web.Mvc;
using EventFinder.Controllers;
using EventFinder.Data;
using EventFinder.Models;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventFinder.Controllers.Tests
{
    /// <summary>This class contains parameterized unit tests for EventController</summary>
    [PexClass(typeof(EventController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class EventControllerTest
    {
        /// <summary>Test stub for .ctor()</summary>
        [PexMethod]
        public EventController ConstructorTest()
        {
            EventController target = new EventController();
            return target;
            // TODO: add assertions to method EventControllerTest.ConstructorTest()
        }

        /// <summary>Test stub for .ctor(EventFinderContext)</summary>
        [PexMethod]
        public EventController ConstructorTest01(EventFinderContext dbContext)
        {
            EventController target = new EventController(dbContext);
            return target;
            // TODO: add assertions to method EventControllerTest.ConstructorTest01(EventFinderContext)
        }

        /// <summary>Test stub for Create()</summary>
        [PexMethod]
        public ActionResult CreateTest([PexAssumeUnderTest]EventController target)
        {
            ActionResult result = target.Create();
            return result;
            // TODO: add assertions to method EventControllerTest.CreateTest(EventController)
        }

        /// <summary>Test stub for Create(Event)</summary>
        [PexMethod]
        public ActionResult CreateTest01([PexAssumeUnderTest]EventController target, Event @event)
        {
            ActionResult result = target.Create(@event);
            return result;
            // TODO: add assertions to method EventControllerTest.CreateTest01(EventController, Event)
        }

        /// <summary>Test stub for DeleteConfirmed(Int32)</summary>
        [PexMethod]
        public ActionResult DeleteConfirmedTest([PexAssumeUnderTest]EventController target, int id)
        {
            ActionResult result = target.DeleteConfirmed(id);
            return result;
            // TODO: add assertions to method EventControllerTest.DeleteConfirmedTest(EventController, Int32)
        }

        /// <summary>Test stub for Delete(Nullable`1&lt;Int32&gt;)</summary>
        [PexMethod]
        public ActionResult DeleteTest(
            [PexAssumeUnderTest]EventController target,
            int? id
        )
        {
            ActionResult result = target.Delete(id);
            return result;
            // TODO: add assertions to method EventControllerTest.DeleteTest(EventController, Nullable`1<Int32>)
        }

        /// <summary>Test stub for Details(Nullable`1&lt;Int32&gt;)</summary>
        [PexMethod]
        public ActionResult DetailsTest(
            [PexAssumeUnderTest]EventController target,
            int? id
        )
        {
            ActionResult result = target.Details(id);
            return result;
            // TODO: add assertions to method EventControllerTest.DetailsTest(EventController, Nullable`1<Int32>)
        }

        /// <summary>Test stub for Edit(Nullable`1&lt;Int32&gt;)</summary>
        [PexMethod]
        public ActionResult EditTest(
            [PexAssumeUnderTest]EventController target,
            int? id
        )
        {
            ActionResult result = target.Edit(id);
            return result;
            // TODO: add assertions to method EventControllerTest.EditTest(EventController, Nullable`1<Int32>)
        }

        /// <summary>Test stub for Edit(Event)</summary>
        [PexMethod]
        public ActionResult EditTest01([PexAssumeUnderTest]EventController target, Event @event)
        {
            ActionResult result = target.Edit(@event);
            return result;
            // TODO: add assertions to method EventControllerTest.EditTest01(EventController, Event)
        }

        /// <summary>Test stub for FindEvent()</summary>
        [PexMethod]
        public ActionResult FindEventTest([PexAssumeUnderTest]EventController target)
        {
            ActionResult result = target.FindEvent();
            return result;
            // TODO: add assertions to method EventControllerTest.FindEventTest(EventController)
        }

        /// <summary>Test stub for Index()</summary>
        [PexMethod]
        public ActionResult IndexTest([PexAssumeUnderTest]EventController target)
        {
            ActionResult result = target.Index();
            return result;
            // TODO: add assertions to method EventControllerTest.IndexTest(EventController)
        }
    }
}
