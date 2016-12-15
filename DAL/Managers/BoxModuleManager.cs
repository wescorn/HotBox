using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BoxModuleManager
    {


        public IEnumerable<BoxModule> GetAll(StoredProject project)
        {
                using (var ctx = new DBContext())
                {
                string sql = "SELECT Point, theLabel, OutstationHostname, DataTime, DataValue, theUnit FROM tblStrategy JOIN tblOutstation ON tblStrategy.OutstationNo = tblOutstation.OutstationId JOIN tblPointValue ON tblStrategy.theIndex = tblPointValue.theIndex WHERE DataTime > @StartTime AND DataTime < @EndTime";

                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@StartTime", project.StartTime));
                sqlParams.Add(new SqlParameter("@EndTime", project.EndTime));

                var BoxModules = ctx.BoxModule.SqlQuery(sql, sqlParams.ToArray()).ToList();
                return BoxModules;
                
            }
        }
    }
}   
