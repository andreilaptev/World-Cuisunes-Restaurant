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
    public class DiscountsController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Discounts
        public ActionResult Index(string redirectFrom)
        {
            return View(db.Discounts.ToList());
        }

        // GET: Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // GET: Discounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Discounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Percentage,MemberID")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discount);
        }

        // GET: Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Percentage,MemberID")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMemberDiscount(int Discount, string redirectFrom)
        {

            Discount disc = Session["Discount"] as Discount;


            Discount discount = new Discount();
            discount.ID = disc.ID;
            discount.MemberID = disc.MemberID;
            discount.Percentage = Discount;

            disc.Percentage = Discount;


            //var existingDisc = db.Discounts.FirstOrDefault(z => z.MemberID == discount.MemberID);

            //if (existingDisc == null) db.Discounts.Add(discount);
            //else
            //{
            //    db.Discounts.Add(disc);
            //   // db.Entry(disc).State = EntityState.Modified;
            //}
            db.Entry(disc).State = EntityState.Modified;
            db.SaveChanges();

            if (redirectFrom.Equals("editDiscount"))
                return RedirectToAction("ManagerPage", "Managers");
            else
                return RedirectToAction("Index", "Discounts");
        }
        





        // GET: Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
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
