using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
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
                project.StartTime = DateTime.Parse(xmlStartTime.InnerText);
                project.EndTime = DateTime.Parse(xmlEndTime.InnerText);
                project.BoxModules = facade.GetBoxModuleManager().GetAll(project);
                System.Diagnostics.Debug.WriteLine(project.BoxModules.Count() + " BoxModules were found in the Database for " + project.ProjectName);

                int counter = 0;
                foreach (var item in project.BoxModules)
                {
                    counter++;
                }
                

                project.CalculateProjectCalculations();
                projectList.Add(project);
            }
            return projectList;
        }

        public void CreateProject(StoredProject newProject)
        {
            String filepath = HostingEnvironment.ApplicationPhysicalPath + "/StoredProjects.xml";
            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(filepath, FileMode.Open);
            xd.Load(lfile);

            XmlElement cl = xd.CreateElement("Project");
            cl.SetAttribute("Name", newProject.ProjectName);
            XmlElement na = xd.CreateElement("StartTime");
            XmlText natext = xd.CreateTextNode(newProject.StartTime.ToString());
            na.AppendChild(natext);
            cl.AppendChild(na);
            xd.DocumentElement.AppendChild(cl);

            na = xd.CreateElement("EndTime");
            natext = xd.CreateTextNode(newProject.EndTime.ToString());
            na.AppendChild(natext);
            cl.AppendChild(na);
            xd.DocumentElement.AppendChild(cl);
            lfile.Close();

            xd.Save(filepath);
        }

        public void CreateProjectXML(StoredProject newProject)
        {
            string filepath = HostingEnvironment.ApplicationPhysicalPath + "/"+ newProject.ProjectName +".xml";

            XmlTextWriter xtw;
            xtw = new XmlTextWriter(filepath, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("CustomerDetails");
            xtw.WriteEndElement();
            xtw.Close();

            XmlDocument xd = new XmlDocument();
            FileStream lfile = new FileStream(filepath, FileMode.Open);
            xd.Load(lfile);

            XmlElement project = xd.CreateElement("Project");
            project.SetAttribute("Name", newProject.ProjectName);


            IEnumerable<BoxModule> boxModuleList = facade.GetBoxModuleManager().GetAll(newProject);
            foreach (var item in boxModuleList)
            {

                XmlElement mod = xd.CreateElement("BoxModule");
                mod.SetAttribute("BoxModule", item.theLabel);
                project.AppendChild(mod);

                XmlElement na = xd.CreateElement("DataValue");
                XmlText natext = xd.CreateTextNode(item.DataValue.ToString());
                na.AppendChild(natext);
                mod.AppendChild(na);

                na = xd.CreateElement("DataUnit");
                natext = xd.CreateTextNode(item.theUnit.ToString());
                na.AppendChild(natext);
                mod.AppendChild(na);

                xd.DocumentElement.AppendChild(project);
                /*
                XmlElement na = xd.CreateElement("DataValue");
                XmlText natext = xd.CreateTextNode(item.DataValue.ToString());
                na.AppendChild(natext);
                project.AppendChild(na);
                xd.DocumentElement.AppendChild(project);

                na = xd.CreateElement("DataUnit");
                natext = xd.CreateTextNode(item.theUnit.ToString());
                na.AppendChild(natext);
                project.AppendChild(na);
                xd.DocumentElement.AppendChild(project);
                */
                lfile.Close();

                xd.Save(filepath);
            }


            
        }


    }
}
