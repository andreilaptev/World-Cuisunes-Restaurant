using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorldCuisinesRestaurants.Data_Access;
using WorldCuisinesRestaurants.Models;

namespace WorldCuisinesRestaurants.Controllers
{
    public class RestaurantSetsController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: RestaurantSets
        public ActionResult Index()
        {
            return View(db.RestaurantSets.ToList());
        }

        // GET: RestaurantSets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantSet restaurantSet = db.RestaurantSets.Find(id);
            if (restaurantSet == null)
            {
                return HttpNotFound();
            }
            return View(restaurantSet);
        }

        // GET: RestaurantSets/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TablesSetByRestaurantID()
        {
            int restaurantID = (int)Session["RestaurantID"];

            var set = db.RestaurantSets.FirstOrDefault(x => x.RestaurantID == restaurantID).HtmlSnippet;
            var restaurant = db.Restaurants.FirstOrDefault(z => z.ID == restaurantID).RestaurantName;

            ViewBag.HTML = set;
            ViewBag.RestaurantName = restaurant;

            return View();
        }


        // POST: RestaurantSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NumberOfTables,HtmlSnippet,PicturePath")] RestaurantSet restaurantSet)
        {
            if (ModelState.IsValid)
            {
                db.RestaurantSets.Add(restaurantSet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurantSet);
        }

        // GET: RestaurantSets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantSet restaurantSet = db.RestaurantSets.Find(id);
            if (restaurantSet == null)
            {
                return HttpNotFound();
            }
            return View(restaurantSet);
        }

        // POST: RestaurantSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NumberOfTables,HtmlSnippet,PicturePath")] RestaurantSet restaurantSet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantSet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurantSet);
        }

        // GET: RestaurantSets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantSet restaurantSet = db.RestaurantSets.Find(id);
            if (restaurantSet == null)
            {
                return HttpNotFound();
            }
            return View(restaurantSet);
        }

        // POST: RestaurantSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantSet restaurantSet = db.RestaurantSets.Find(id);
            db.RestaurantSets.Remove(restaurantSet);
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
