using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLab04.Models;

namespace MVCLab04.Controllers
{
    public class NewsController : Controller
    {
        NewsDctx NewsDctx = new NewsDctx();
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SelectAllNews()
        {
            List<news> lst = NewsDctx.news.OrderByDescending(e=>e.date).ToList();
           
            return View(lst);
        }
        public ActionResult Add()
        {
           List<catogery> s = NewsDctx.catogeries.ToList();
            SelectList ss = new SelectList(s,"id","name");
            ViewData["aa"] = ss;
            ViewBag.cat = s;
            return View();
        }
        [HttpPost]
        public ActionResult Add(news news,HttpPostedFileBase httpPosted)
        {
            
            
            if (ModelState.IsValid)

            {
                if (httpPosted.FileName!="")
                {
                    httpPosted.SaveAs(Server.MapPath("~/img/") + httpPosted.FileName);
                    news.photo = httpPosted.FileName;
                }
                if (Session["id"]!=null)
                {
                    news.user_id = int.Parse(Session["id"].ToString());
                }
                NewsDctx.news.Add(news);
                NewsDctx.SaveChanges();
            }
            return RedirectToAction("Add");
        }
        public ActionResult SelectByUser()
        {
            int id = int.Parse(Session["id"].ToString());
            List<news> n = NewsDctx.news.Where(e => e.user_id == id).ToList();
            return View(n);
        }
        public ActionResult SelectByCatogery(int id)
        {
            List<news> n = NewsDctx.news.Where(e => e.catogery_id == id).OrderByDescending(e=>e.date).ToList();
            return View("SelectAllNews",n);
        }
        public ActionResult MoreInfo(int id)
        {
            news n = NewsDctx.news.Where(i => i.id == id).SingleOrDefault();

            return View(n); 
        }

        public ActionResult Edit(int id)
        {
            news news = NewsDctx.news.Where(n => n.id == id).SingleOrDefault();
            if (news!=null)
            {
                List<catogery> s = NewsDctx.catogeries.ToList();
                SelectList ss = new SelectList(s, "id", "name");
                ViewBag.cat = ss;
                return View(news);
            }
            return RedirectToAction("SelectByUser");
        }
        [HttpPost]
        public ActionResult Edit (news news, HttpPostedFileBase httpPosted)
        {
            news ne = NewsDctx.news.Where(n => n.id == news.id).SingleOrDefault();

            if (ModelState.IsValid)
            {
                if (httpPosted != null)
                {
                    httpPosted.SaveAs(Server.MapPath("~/img/") + httpPosted.FileName);
                    ne.photo = httpPosted.FileName;
                }
               
                
            ne.title = news.title;
            ne.pref = news.pref;
            ne.desc = news.desc;
            ne.catogery_id = news.catogery_id;
            ne.date = news.date;
            
            NewsDctx.SaveChanges();
            }
            return RedirectToAction("SelectByUser");
        }
        public ActionResult Delete(int id)
        {
            if (id>0)
            {
                NewsDctx.news.Remove(NewsDctx.news.Where(n => n.id == id).SingleOrDefault());
                NewsDctx.SaveChanges();
            }
           
            return RedirectToAction("SelectByUser"); 
        }
        

    }
}