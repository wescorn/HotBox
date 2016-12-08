using Domain.DomainModels;
using HOTBOXWebsite.BLL;
using HOTBOXWebsite.Models;
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
        List<DropdownModel> ddlistItems = new List<DropdownModel>();
        public ActionResult Index(int? id)
        {
            List<StoredProject> projectList = facade.GetStoredProjectService().GetAll();
            
            int idCounter = 0;
            foreach (var item in projectList)
            {
                DropdownModel ddm = new DropdownModel();
                ddm.Id = idCounter;
                ddm.project = item;
                ddlistItems.Add(ddm);
                idCounter++;
            }
            var selectedProject = ddlistItems.FirstOrDefault(x => x.Id == id);
            var model = new ProjectOverviewViewModel() { projects = ddlistItems, SelectedProject = selectedProject };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateProject(FormCollection collection)
        {
            StoredProject newProject = new StoredProject();
            newProject.ProjectName = collection["projektnavn"];
            newProject.StartTime = DateTime.Parse(collection["startDag"] + " " + collection["startTime"] + ":" + collection["startMinut"] + ":" + collection["startSekund"]);
            newProject.EndTime = DateTime.Parse(collection["slutDag"] + " " + collection["slutTime"] + ":" + collection["slutMinut"] + ":" + collection["slutSekund"]);


            facade.GetStoredProjectService().CreateProjectXML(newProject);
            return RedirectToAction("Index");
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