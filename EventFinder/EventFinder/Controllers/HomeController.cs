using EventFinder.Data;
using EventFinder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace EventFinder.Controllers
{
    public class HomeController : Controller
    {
        EventFinderContext db = new EventFinderContext();

        public ActionResult LastMinDeals()
        {
            List<Event> lastMinEvents = getLastMinEvent();
            return PartialView("_getLastMinDeals", lastMinEvents);
        }

        private List<Event> getLastMinEvent()
        {
            return db.Events.Where(o => o.StartDate <= DbFunctions.AddDays(DateTime.Today, 2)).ToList();
        }

        public ActionResult EventSearch(string eventinfo, string location)
        {
            return PartialView("_eventSearch", getEvents(eventinfo, location));
        }

        private List<Event> getEvents(string eventinfo, string location)
        {
            return db.Events
                .Where(o => o.EventType.Type.Contains(eventinfo) || o.EventTitle.Contains(eventinfo) || o.Location.Contains(location))
                .OrderBy(o => o.StartDate)
                .ToList();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}