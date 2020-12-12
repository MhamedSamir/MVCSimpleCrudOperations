using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLab04.Models;

namespace MVCLab04.Controllers
{
    public class HomeController : Controller
    {
        NewsDctx NewsDctx = new NewsDctx();
        
        // GET: Home
        public ActionResult Index()
        {
            Session["catogery"] = NewsDctx.catogeries.ToList();
            if (Request.Cookies["NewsUser"] != null)
            {
                Session["id"] = Request.Cookies["NewsUser"].Values["id"];
                return RedirectToAction("Add", "News", new { id = Session["id"].ToString() });
            }
            return RedirectToAction("SelectAllNews","News");
        }
    }
}