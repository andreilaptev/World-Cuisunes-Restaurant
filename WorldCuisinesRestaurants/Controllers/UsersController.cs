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
   
    public class UsersController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,UserRole")] User user, string Login, string Password)
        {
            if (ModelState.IsValid)
            {
                user.MemberID = user.ID;
                user.TotalAmountSpent = 0;
                db.Users.Add(user);
                //var iid = db.Users.Max(x => x.ID);
                
                db.SaveChanges();

                var newId = db.Users.Max(x => x.ID);
                var updatedUser = db.Users.First(x => x.ID == newId);
                updatedUser.MemberID = newId;

                Discount discount = new Discount();

                if (user.UserRole == UserRole.Member)
                {
                 
                    discount.MemberID = newId;
                    discount.Percentage = 0;
                }

                db.Discounts.Add(discount);
                db.SaveChanges();

                db.Entry(updatedUser).State = EntityState.Modified;
                db.SaveChanges();
                

                UserLogin userLogin = new UserLogin();
                userLogin.LoginName = Login;
                userLogin.Password = Password;
                userLogin.UserID = newId;
                db.UserLogins.Add(userLogin);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
