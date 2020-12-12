using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLab04.Models;

namespace MVCLab04.Controllers
{
    public class CatogeryController : Controller
    {
        NewsDctx NewsDctx = new NewsDctx();
        // GET: Catogery
        public ActionResult Index()
        {
           SelectList c =new SelectList( NewsDctx.catogeries.ToList(),"id","name");

            return View(c);
        }
        public ActionResult selectNewsByCatogery(int id)
        {
            List<news> lst = NewsDctx.news.Where(i => i.catogery_id == id).ToList();

            return PartialView(lst);

        }
    }
}