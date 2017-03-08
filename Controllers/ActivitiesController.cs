using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZenithDataLib.Model;
using ZenithWebSite.Models;

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            //using user id, find all related events
            string userId = User.Identity.GetUserId();

            string email = (from a in db.Users
                            where a.Id == userId
                            select a.UserName).FirstOrDefault();

            List<Event> Events = (from e in db.Events
                                  where e.EnteredByUserName == email
                                  select e).ToList<Event>();

            List<Activity> Activities = new List<Activity>();


            // using the event id, get all activiteis and populate activity list
            foreach(var e in Events)
            {
                int eventId = e.EventId;
                List<Activity> activity_in_events = (from a in db.Activities
                                                    where a.EventId == eventId
                                                    select a).ToList<Activity>();
                foreach(var a in activity_in_events)
                {
                    Activities.Add(a);
                }
            }
            return View(Activities);
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            //ViewBag.EventId = new SelectList(db.Events, "EventId", "EnteredByUserName");

            //using user id, find all related events
            string userId = User.Identity.GetUserId();

            string email = (from a in db.Users
                            where a.Id == userId
                            select a.UserName).FirstOrDefault();

            List<Event> Events = (from e in db.Events
                                  where e.EnteredByUserName == email
                                  select e).ToList<Event>();

            ViewBag.EventId = new SelectList(Events, "EventId", "EventId");
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,ActivityDescription,CreationData,EventId")] Activity activity)
        {
            if (ModelState.IsValid)
            {

                activity.CreationData = DateTime.Now;
                db.Activities.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "EventId", "EnteredByUserName", activity.EventId);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EnteredByUserName", activity.EventId);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,ActivityDescription,CreationData,EventId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "EventId", "EnteredByUserName", activity.EventId);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
