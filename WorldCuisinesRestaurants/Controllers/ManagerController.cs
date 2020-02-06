using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldCuisinesRestaurants.Data_Access;
using WorldCuisinesRestaurants.Models;

namespace WorldCuisinesRestaurants.Controllers
{
    public class ManagerController : Controller
    {
        private Data_Access.WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetMembers()
        {
            return RedirectToAction("GetAllMembers");
        }

        // POST: GetAllMembers
        [HttpPost]
        public ActionResult GetAllMembers(string redirectFrom)
        {
            string role = "Member";
            //var members = db.Users.Where(x=>x.UserRole.ToString().Equals(role)).ToList();
            var members = db.Users.Where(x => x.UserRole.ToString() == role).ToList();

            List<int> percents = new List<int>();

            foreach (var m in members)
            {
                var disc = db.Discounts.FirstOrDefault(z => z.MemberID == m.ID);
                if (disc == null) percents.Add(0);
                else percents.Add(disc.Percentage);
            }
            ViewBag.Discount = percents;
            string controllerName;

            var userRole = Session["UserRole"];

            if (userRole.ToString() == "Admin")
            {
                redirectFrom = "AdminPage";
                controllerName = "Admin";
            }
            else
                controllerName = "Managers";

            string[] backpath = {redirectFrom, controllerName};
            ViewBag.BackPath = backpath;

            return View(members);            
        }
        

        // POST: Manager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
/// <summary>
/// ////////////////////////////////  НАДО  ПЕРЕМЕСТИТЬ  в ДИСКАУНТ!!!!
/// </summary>
/// <param name="id"></param>
/// <returns></returns>
        // POST: Discount/Edit/5
        [HttpPost]
         
        public ActionResult EditDiscount(int? id)
      {
            var memberID = db.Users.FirstOrDefault(x => x.ID == id).MemberID;
            var disc = db.Discounts.FirstOrDefault(z => z.MemberID == memberID);

            if (disc == null)
            {
                disc = new Discount();
                disc.ID = (int) id;
                disc.MemberID = (int) memberID;
                disc.Percentage = 0;
            }
           

            ViewBag.Discount = disc.Percentage;

            Session["Discount"] = disc;

            return View(disc);
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int? id)
        {

            return View();
        }

        // POST: Manager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
