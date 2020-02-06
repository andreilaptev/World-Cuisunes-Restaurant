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
    public class IngredientsController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Ingredients
        public ActionResult Index()
        {
            return View(db.Ingredients.ToList());
        }

        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // Getting Ingredients List by Dish ID
        public ActionResult GetIngredientsByDishID(int id)
        {
            var ing = db.DishIngredients.Where(x => x.Dish_ID == id).ToList();

            List<Ingredient> list = new List<Ingredient>();

            foreach (var i in ing)
            {
                list.Add(db.Ingredients.FirstOrDefault(x => x.ID == i.Ingredient_ID));
            }

            var objects = db.DishIngredients.Where(x => x.Dish_ID == id);

            List<double> quantitiesList = new List<double>();

            foreach (var x in objects)
            {
                quantitiesList.Add(x.Quantity);
            }

            ViewBag.Quantities = quantitiesList;

            var currentDish = Session["DishID"];

            ViewBag.DishID = currentDish;

            Console.WriteLine(quantitiesList);

            return View(list);
        }

        public ActionResult AddIngredientsByDishID(int id)
        {
            var ingredients = db.Ingredients;

            ViewBag.Ingredients = new SelectList(ingredients, "ID", "Name");

            //var ing = db.DishIngredients.Where(x => x.Dish_ID == id).ToList();

            //List<Ingredient> list = new List<Ingredient>();

            //foreach (var i in ing)
            //{
            //    list.Add(db.Ingredients.FirstOrDefault(x => x.ID == i.Ingredient_ID));
            //}



            // Console.WriteLine(list);

            return View();
        }


        public ActionResult AddIngredientToDish(int Name, double Quantity)
        {
            DishIngredient di = new DishIngredient();

            Dish currentDish = Session["Dish"] as Dish;
            di.Dish_ID = currentDish.ID;
            di.Ingredient_ID = Name;
            di.Quantity = Quantity;

            db.DishIngredients.Add(di);
            db.SaveChanges();

            return RedirectToAction("Edit", "Dishes", new { currentDish.ID });
        }



        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Price,Measure")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Measure")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            db.Ingredients.Remove(ingredient);
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
