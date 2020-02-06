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

    public class RestaurantsController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurants/Cuisine
        public ActionResult GetRestaurantsByCuisineType()
        {
            string cuisine = Session["Cuisine"].ToString();

            var restaurants = db.Restaurants.Where(x => x.CuisineType.ToString().Equals(cuisine));
            if (restaurants == null)
            {
                return HttpNotFound();
            }

            ViewBag.Cuisine = cuisine;
            return View(restaurants);
        }

        [HttpPost]
        public ActionResult RestaurantChosen (int? restaurantID)
        {
            Session["RestaurantID"] = restaurantID;

            var name = db.Restaurants.FirstOrDefault(x => x.ID == restaurantID);
            ViewBag.ChosenRestaurant = name.RestaurantName;

            return View();
        }


        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RestaurantName,CuisineType")] Restaurant restaurant, Address address)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();

                var newId = db.Restaurants.Max(x => x.ID);
                Address ad = new Address();
                ad.RestaurantID = newId;
                ad.Street = address.Street;
                ad.City = address.City;
                ad.ZipCode = address.ZipCode;

                db.Addresses.Add(ad);
                db.SaveChanges();


                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RestaurantName,CuisineType")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
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
