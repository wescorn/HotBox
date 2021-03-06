﻿using Domain.DomainModels;
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
        
        public ActionResult Index(int? id)
        {
            List<DropdownModel> ddlistItems = new List<DropdownModel>();
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
            newProject.StartTime = DateTime.Parse(collection["startTime"]);
            newProject.EndTime = DateTime.Parse(collection["endTime"]);


            facade.GetStoredProjectService().CreateProject(newProject);
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