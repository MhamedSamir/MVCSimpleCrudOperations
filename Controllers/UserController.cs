using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLab04.Models;

namespace MVCLab04.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        NewsDctx dctx = new NewsDctx();
        
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(user user)
        {
            if (ModelState.IsValid) { 
            dctx.users.Add(user);
                dctx.SaveChanges();
                                    }
            return RedirectToAction("Add");
        }

        public ActionResult login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult login(user user, bool remb)
        {
   user u = dctx.users.Where(uu => uu.email == user.email && uu.password == user.password).FirstOrDefault();    
            if (u!=null)
            {
                if (remb)
                {
                    HttpCookie co = new HttpCookie("NewsUser");
                    co.Values.Add("id", u.id.ToString());
                    co.Values.Add("name", u.name);
                    co.Expires = DateTime.Now.AddDays(5);
                    Response.Cookies.Add(co);
                }
                Session["id"] = u.id;
                return RedirectToAction("Index", "Home",new {id=u.id });
            }
            else
            {
                ViewBag.mess = "incorrect username or password";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["id"] = null;
            HttpCookie c = new HttpCookie("NewsUser");
            c.Expires = DateTime.Now.AddDays(-30);
            Response.Cookies.Add(c);
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Edit()
        {
            int id = int.Parse(Session["id"].ToString());
              user u = dctx.users.Where(i => i.id == id).Single();
            
            return View(u);
        }
        [HttpPost]
        public ActionResult Edit(user user)
        {
            if (ModelState.IsValid)
            {

                user u = dctx.users.Where(i => i.id == user.id).Single();
                u.name = user.name;
                u.email = user.email;
                dctx.SaveChanges();
            }

           return Logout();
            //return RedirectToAction("login");
        }
        public ActionResult ChangePassword()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(user user,string pass)
        {

            int id = int.Parse(Session["id"].ToString());
                user u = dctx.users.Where(i => i.id == id).SingleOrDefault();
                if (user.password!=u.password)
                {
                    ViewBag.message = "Old Pass Not Correct";
                    return ChangePassword();
                }
            if (pass=="")
            {
                ViewBag.pass = "Invalid Password";
                return ChangePassword();
            }
                u.password = pass;
                u.confirm = pass;
                dctx.SaveChanges();     
                return Logout();
        }
        public ActionResult checkEmail(string email)
        {
            user u = dctx.users.Where(i => i.email == email).FirstOrDefault();
            if (u != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}