using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BoxModuleManager
    {
        DBContext context = new DBContext();

        public IEnumerable<BoxModule> GetAll(StoredProject project)
        { 
            return context.BoxModule.SqlQuery("SELECT Point, theLabel, "+ 
                "OutstationHostname, DataTime, DataValue, theUnit FROM tblStrategy JOIN tblOutstation ON tblStrategy.OutstationNo = tblOutstation.OutstationId JOIN tblPointValue ON tblStrategy.theIndex = tblPointValue.theIndex WHERE DataTime > {0} AND DataTime < {1}",project.StartTime,project.EndTime);
        }
    }
}
