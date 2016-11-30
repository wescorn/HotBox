using Domain.DomainModels;
using HOTBOXWebsite.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HOTBOXWebsite.Controllers
{
    public class HomeController : Controller
    {
        Facade facade = new Facade();
        public ActionResult Index()
        {
            List<StoredProject> model = facade.GetStoredProjectService().GetAll();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}