using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BLL
{
    public class StoredProjectService
    {
        Facade facade = new Facade();

        public List<StoredProject> GetAll()
        {
            XmlDocument xdoc = facade.GetStoredProjectManager().GetAll();
            List<StoredProject> projectList = new List<StoredProject>();
            XmlNodeList list = xdoc.GetElementsByTagName("Project");
            for (int i = 0; i < list.Count; i++)
            {
                StoredProject project = new StoredProject();
                XmlElement xmlProject = (XmlElement)xdoc.GetElementsByTagName("Project")[i];
                XmlElement xmlStartTime = (XmlElement)xdoc.GetElementsByTagName("StartTime")[i];
                XmlElement xmlEndTime = (XmlElement)xdoc.GetElementsByTagName("EndTime")[i];
                project.ProjectName = xmlProject.GetAttribute("Name");

                string[] StartTime = xmlStartTime.InnerText.Split('/', ' ', ':');
                int[] convStartTime = Array.ConvertAll(StartTime, s => int.Parse(s));

                string[] EndTime = xmlStartTime.InnerText.Split('/', ' ', ':');
                int[] convEndTime = Array.ConvertAll(EndTime, s => int.Parse(s));


                project.StartTime = new DateTime(convStartTime[0], convStartTime[1], convStartTime[2], convStartTime[3], convStartTime[4], convStartTime[5]);
                project.EndTime = new DateTime(convEndTime[0], convEndTime[1], convEndTime[2], convEndTime[3], convEndTime[4], convEndTime[5]);
                projectList.Add(project);
            }
            return projectList;
        }
    }
}
