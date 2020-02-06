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
    public class CuisinesController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Cuisines
        public ActionResult Index()
        {
            return View(db.Cuisines.ToList());
        }

        // GET: GetAllCuisines
        public ActionResult GetAllCuisines()
        {
            return View(db.Cuisines.ToList());
        }

        public ActionResult CuisineChosen (string name)
        {
            ViewBag.ChosenCuisine = name;
            //return View();

            Session["Cuisine"] = name;

            return RedirectToAction("GetRestaurantsByCuisineType", "Restaurants");
        }

        // GET: Cuisines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuisine cuisine = db.Cuisines.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }

        // GET: Cuisines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cuisines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                db.Cuisines.Add(cuisine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cuisine);
        }

        // GET: Cuisines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuisine cuisine = db.Cuisines.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }

        // POST: Cuisines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuisine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cuisine);
        }

        // GET: Cuisines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuisine cuisine = db.Cuisines.Find(id);
            if (cuisine == null)
            {
                return HttpNotFound();
            }
            return View(cuisine);
        }

        // POST: Cuisines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuisine cuisine = db.Cuisines.Find(id);
            db.Cuisines.Remove(cuisine);
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
