using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HOTBOXWebsite.BLL
{
    public class Facade
    {
        public static StoredProjectService StoredProjectService;
        public static BoxModuleService BoxModuleService;

        public StoredProjectService GetStoredProjectService()
        {
            if (StoredProjectService == null)
            {
                StoredProjectService = new StoredProjectService();
            }
            return StoredProjectService;
        }

        public BoxModuleService GetBoxModuleService()
        {
            if (BoxModuleService == null)
            {
                BoxModuleService = new BoxModuleService();
            }
            return BoxModuleService;
        }
    }
}