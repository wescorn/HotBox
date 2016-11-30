using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml;

namespace DAL.Managers
{
    public class StoredProjectManager
    {
        string filepath = HostingEnvironment.ApplicationPhysicalPath + "/StoredProjects.xml";

        public XmlDocument GetAll()
        {

            XmlDocument xdoc = new XmlDocument();
            FileStream rfile = new FileStream(filepath, FileMode.Open);
            xdoc.Load(rfile);
            rfile.Close();
            return xdoc;
        }

    }
}
