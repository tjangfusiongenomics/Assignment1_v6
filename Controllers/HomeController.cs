using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithDataLib.Model;
using ZenithWebSite.Models;

namespace ZenithWebSite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // get a list of events specific day
        public List<Event> get_event_by_day (List<Event> this_week,int start_index, int end_index)
        {

            List<Event> output = new List<Event>();

            DateTime startDay = DateTime.Today.AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + start_index);
            DateTime EndDay = DateTime.Today.AddDays(((int)(DateTime.Today.DayOfWeek) * -1) + end_index);

            // get a list of event with following condition
            //  day should be greater than start day
            //  day should be less than end day
            //  event has to be active   
            // Then order the list by ascending order 


                output = (from e in db.Events
                          where e.EventFromDateAndTime >= startDay && e.EventFromDateAndTime < EndDay && e.IsActive == true
                          select e
                            ).ToList<Event>();

            return output;
        }

        // get a list of activities on specific day
        public List<Activity> get_activities(List<Event> events)
        {
            List<Activity> output = new List<Activity>();

            foreach(var e in events)
            {
                // get a list of activities by eventId
                int id = e.EventId;
                List<Activity> activities = (from a in db.Activities
                                           where a.EventId == id
                                           select a).ToList<Activity>();
                // add activities to output list
                foreach(var a in activities)
                {
                    output.Add(a);
                }
            }

            return output;
        }


        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            string email = (from a in db.Users
                            where a.Id == userId
                            select a.UserName).FirstOrDefault();

            // get a list of all event
            List<Event> week = db.Events.ToList<Event>();
            ViewBag.week = week;


            // get a list of this event daily
            List<Event> Monday = get_event_by_day(week,1,2);
            List<Event> Tuesday = get_event_by_day(week, 2, 3);
            List<Event> Wendsday = get_event_by_day(week, 3, 4);
            List<Event> Thursday = get_event_by_day(week, 4, 5);
            List<Event> Friday = get_event_by_day(week, 5, 6);
            List<Event> Saturday = get_event_by_day(week, 6, 7);
            List<Event> Sunday = get_event_by_day(week, 7, 8);

            // pass the event lists to view
            ViewBag.Monday = Monday;
            ViewBag.Tuesday = Tuesday;
            ViewBag.Wendsday = Wendsday;
            ViewBag.Thursday = Thursday;
            ViewBag.Friday = Friday;
            ViewBag.Saturday = Saturday;
            ViewBag.Sunday = Sunday;

            // get daily activities
            List<Activity> Monday_activities = get_activities(Monday);
            List<Activity> Tuesday_activities = get_activities(Tuesday);
            List<Activity> Wendsday_activities = get_activities(Wendsday);
            List<Activity> Thursday_activities = get_activities(Thursday);
            List<Activity> Friday_activities = get_activities(Friday);
            List<Activity> Saturday_activities = get_activities(Saturday);
            List<Activity> Sunday_activities = get_activities(Sunday);

            // pass the activity lists to view
            ViewBag.Monday_activities = Monday_activities;
            ViewBag.Tuesday_activities = Tuesday_activities;
            ViewBag.Wendsday_activities = Wendsday_activities;
            ViewBag.Thursday_activities = Thursday_activities;
            ViewBag.Friday_activities = Friday_activities;
            ViewBag.Saturday_activities = Saturday_activities;
            ViewBag.Sunday_activities = Sunday_activities;


            return View(db.Events.ToList<Event>());
        }
    }
}