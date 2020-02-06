using System;
using System.Collections;
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
    public class DishesController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Dishes
        public ActionResult Index()
        {
            return View(db.Dishes.ToList());
        }

        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            ViewBag.Restaurants = new SelectList(db.Restaurants, "ID", "RestaurantName");

           // var ingredients = db.Ingredients;

            //ViewBag.Ingredients = new SelectList(ingredients, "ID", "Name");

            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Cost")] Dish dish, int RestaurantID)
        {
            if (dish.Name == null || dish.Cost.ToString() == null)
            {
                ViewBag.Error = "Provide all fields";
                ViewBag.Restaurants = new SelectList(db.Restaurants, "ID", "RestaurantName");
                return View("Create");
            }

            if (ModelState.IsValid)
            {
                // Adding dish to DB
                db.Dishes.Add(dish);
                db.SaveChanges();

                DishRestaurant dr = new DishRestaurant();
                var d = db.Dishes.FirstOrDefault(x=>x.Name == dish.Name);

                dr.DishID = d.ID;
                dr.RestaurantID = RestaurantID;
                
                db.DishRestaurants.Add(dr);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            Session["DishID"] = id;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }


        public ActionResult ViewAllDishesByRestaurantID()
        {
            var currentUser = Session["User"];
           

            int restaurantID = (int) Session["RestaurantID"];
            var md = db.MenuDIshes.Where(x => x.Menu_ID == restaurantID).ToList();

            List<Dish> dishes = new List<Dish>();

            foreach (var i in md )
            {
                var dname = db.Dishes.FirstOrDefault(x => x.ID == i.Dish_ID);

                dishes.Add(dname);
            }

            ViewBag.Restaurant = db.Restaurants.FirstOrDefault(x => x.ID == restaurantID).RestaurantName;
            var l = new List<int> { 0,1, 2, 3, 4, 5, 6, 7, 8, 9};
            ViewBag.List = new SelectList(l, "quantity");

            return View(dishes);
        }

        [HttpPost]
        public ActionResult DishesChosen (FormCollection collection, List<int> item)
        {
            string collData = collection["quantity"];

            List<double> quantities = new List<double>();

            foreach (var cd in collData)
            {
                var number = (int)Char.GetNumericValue(cd);

                if (number >= 0) quantities.Add(number);
            }
        

            ///////////////////////////////////////////////////////////////////////////////////////////////
            ///                                    CALCULAING ORDER               /////////////////////////
            ///                                    
            double totalSum = 0;

            for (int i=0; i < item.Count(); i++)
            {
                var ii = item[i];

                var zz = db.Dishes.FirstOrDefault(x => x.ID == ii).Cost;

               totalSum += zz * quantities[i];                 

            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            Order order = new Order();
            order.Date = DateTime.Today;
            order.Restaurant_ID = (int)Session["RestaurantID"];
            order.TotalSum = totalSum;

            User u = Session["CurrentUser"] as User;

            if (u == null) order.User_ID = 0;

            else
            {
                order.User_ID = u.ID;

                ////// CALCULATING DISCOUNT /////////////
                var perc = db.Discounts.FirstOrDefault(x => x.MemberID == u.ID);//.Percentage;
                int disc;
                if (perc == null) disc = 0;
                    else disc = perc.Percentage;

                double d = 1 - disc / 100.0;
                totalSum = totalSum * d;

                ////// ADDING SUM TO THE ACCOUNT /////////////
                u.TotalAmountSpent += (float)totalSum;

                order.TotalSum = totalSum;

                db.Entry(u).State = EntityState.Modified;
                db.SaveChanges();
            }
            

            Session["Order"] = order;


            return RedirectToAction("SaveNewOrder", "Orders");
        }



        public ActionResult SeeIngredientsList(int id)
        {
            var dish = db.Dishes.FirstOrDefault(x => x.ID == id);
            return RedirectToAction("GetIngredientsByDishID", "Ingredients", new { id = dish.ID });
        }

        public ActionResult AddIngredient(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dish = db.Dishes.FirstOrDefault(x=>x.ID == id);
            
            if (dish == null)
            {
                return HttpNotFound();
            }
            Session["Dish"] = dish;

            return RedirectToAction ("AddIngredientsByDishID", "Ingredients", new { id=dish.ID });
        }
        


        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Cost")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
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
