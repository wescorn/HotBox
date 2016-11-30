using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Managers;
using DAL;

namespace BLL
{
    public class Facade
    {
        public static StoredProjectManager StoredProjectManager;
        public static BoxModuleManager BoxModuleManager;

        public StoredProjectManager GetStoredProjectManager()
        {
            if (StoredProjectManager == null) {
                StoredProjectManager = new StoredProjectManager();
            }
            return StoredProjectManager;
        }

        public BoxModuleManager GetBoxModuleManager()
        {
            if (BoxModuleManager == null)
            {
                BoxModuleManager = new BoxModuleManager();
            }
            return BoxModuleManager;
        }
    }
}
