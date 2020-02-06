using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WorldCuisinesRestaurants.Data_Access;
using WorldCuisinesRestaurants.Models;

namespace WorldCuisinesRestaurants.Controllers
{
    public class UserLoginsController : Controller
    {
        private WorldCuisinesRestaurantsDBContext db = new WorldCuisinesRestaurantsDBContext();
        private static int verificationCode;

        // GET: UserLogins
        public ActionResult Index()
        {
            return View(db.UserLogins.ToList());
        }

        // GET: UserLogins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogins.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View(userLogin);
        }

        // GET Loggong in
        public ActionResult UserLogin()
        {

            return View();
        }

        // GET: UserLogins/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST Check Logging in
        public ActionResult CheckUserLogin(string login, string pass)
        {
            var request = db.UserLogins.FirstOrDefault(x => x.LoginName == login);            

            if (request == null)
            {
                ViewBag.UserNotFound = "User not found";
                return View("UserLogin");
            }
            else if (request.Password != pass)
            {
                ViewBag.IncorrectPassword = "Password is incorrect";
                return View("UserLogin");
            }
            else
            {
                Session["User"] = request;

                // Defining user's role
                var id = request.ID;
                var currentUser = db.Users.FirstOrDefault(x => x.ID == id);
                var role = currentUser.UserRole;
                Session["UserRole"] = role;

                Session["CurrentUser"] = currentUser;

                UserRole r = UserRole.Manager;
                UserRole a = UserRole.Admin;
        
                if (role.Equals(r)) return RedirectToAction("ManagerPage", "Managers");
                else
                    if (role.Equals(a)) return RedirectToAction("AdminPage", "Admin");
                else return RedirectToAction("getAllCuisines", "Cuisines");


            }                

            return View();
        }
        
        // POST: UserLogins/Create
        // THIS METHOD IS ABOUT REGISTERING NEW USER REGARDLESS OF IT'S ROLE
        // ROLE IS ASSIGNED LATER BE ADMIN
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginName,Password,UserID")] UserLogin userLogin, string Email, string FirstName, string LastName)
        {
            if (ModelState.IsValid)
            {

                string newLogin = userLogin.LoginName;
                
                 var loginExists = db.UserLogins.FirstOrDefault(X=>X.LoginName == newLogin);
                if (loginExists != null)
                {
                  ViewBag.LoginExistsMessage = "This login already exists";
                    return View(userLogin);
                };

                Session["UserLogin"] = userLogin;
                Session["Email"] = Email;
                Session["FirstName"] = FirstName;
                Session["LastName"] = LastName;

                SendEmail(Email);
                
            }

            //return View(userLogin);
            return RedirectToAction("EnterVerificationCode");
        }


        // GET: UserLogins/EnterVerificationCode
        public ActionResult EnterVerificationCode()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CheckVerificationCode(int code)
        {
            if (verificationCode == code)
            {
                User newUser = new User();
                newUser.FirstName = Session["FirstName"].ToString();
                newUser.LastName = Session["LastName"].ToString();
                newUser.Email = Session["Email"].ToString();
                newUser.UserRole = UserRole.Member;
                newUser.TotalAmountSpent = 0;

                Random random = new Random();
                int memberID = random.Next(0, 1000);

                newUser.MemberID = memberID;// db.Members.Max(x => x.ID);

                db.Users.Add(newUser);
                db.SaveChanges();

                Session["CurrentUser"] = newUser;

                UserLogin newLogin = Session["UserLogin"] as UserLogin;
                //var newID = newUser.ID;

                var s = db.Users.FirstOrDefault(x=>x.ID == newUser.ID);

                newLogin.UserID = s.ID;

                db.UserLogins.Add(newLogin);
                db.SaveChanges();
                   return RedirectToAction("getAllCuisines", "Cuisines");               
            }            

            return RedirectToAction("Index", "Home");
        }

/// <summary>
/// /////////////////////////////////////////// FORGOTTEN PASSWORD PROCESSING /////////////////////////////////////
/// </summary>
/// <returns></returns>

        public ActionResult ForgottenPasswordRequest()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgottenPasswordCode(string email)
        {
            var address = db.Users.FirstOrDefault(x => x.Email == email);

            if (address == null)
            {
                ViewBag.Error = "This email address does not exist";
                return View("ForgottenPasswordRequest");
            }

            Session["UserForgottenPassID"] = address.ID;
            SendEmail(email);

            //return View();
            return RedirectToAction("ForgottenPasswordVerificationCode");
        }

        public ActionResult ForgottenPasswordVerificationCode()
        {

            return View();
        }

        // VIEW NewPasswordForm ///////////////////
        [HttpPost]
        public ActionResult CheckForgottenPasswordVerificationCode(int code)
        {
            if (verificationCode != code)
            {

                ViewBag.Error = "Code incorrect. Try one more time";
                return  View("ForgottenPasswordRequest");

            }


              return View("NewPasswordForm");//RedirectToAction("UserLogin", "UserLogins");
        }

        [HttpPost]
        public ActionResult NewPasswordSave (string password1, string password2)
        {
            if (password1 != password2)
            {
                ViewBag.Error = "Password fields do not match";
                return View("NewPasswordForm");
            }

            var userID = Session["UserForgottenPassID"];

            var userLogin = db.UserLogins.FirstOrDefault(x => x.UserID == (int) userID);
            userLogin.Password = password1;

            db.Entry(userLogin).State = EntityState.Modified;
            db.SaveChanges();

            //UserLogin ul = new UserLogin();
            //ul.ID = user.ID;
            //ul.LoginName = user.



            return RedirectToAction("UserLogin", "UserLogins");
        }


/// <summary>
/// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
/// <param name="id"></param>
/// <returns></returns>

        // GET: UserLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogins.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View(userLogin);
        }

        // POST: UserLogins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LoginName,Password,UserID")] UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userLogin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userLogin);
        }

        // GET: UserLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserLogin userLogin = db.UserLogins.Find(id);
            if (userLogin == null)
            {
                return HttpNotFound();
            }
            return View(userLogin);
        }

        // POST: UserLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserLogin userLogin = db.UserLogins.Find(id);
            db.UserLogins.Remove(userLogin);
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

     
        public void SendEmail(string email)
        {
            Random random = new Random();
            verificationCode = random.Next(0, 1000);

            var body = "<p> Please enter this code  </p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(email)); //replace with valid value
            message.From = new MailAddress("noreply@gmail.com"); //("bokken2008@gmail.com");
            message.Subject = "Your registration confirmation code";
            message.Body = string.Format(body + verificationCode, "World Cuisines Restaurants", "New Member");
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "bokken2008@gmail.com",  // replace with valid value
                    Password = "bokken1111"  // replace with valid value
                };

                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
                //return code;
            }

        }
    }
}
