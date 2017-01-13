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


        public List<BoxModule> GetAll(StoredProject project)
        {
            List<string> pointList = new List<string>(new string[] { "P10", "P17", "P20", "P21", "P100", "P200" });
            List<BoxModule> modules = new List<BoxModule>();
            foreach (var point in pointList)
            {
                modules.AddRange(EFSafePointQuery(point, project.StartTime, project.EndTime));
            }
            return modules;
        }

        private List<BoxModule> EFSafePointQuery(string point, DateTime startTime, DateTime endTime)
        {
            using (var ctx = new DBContext())
            {
                if (ctx.Database.Exists())
                {
                    string sql = "SELECT DISTINCT Point, theLabel, DataTime, DataValue, theUnit FROM tblStrategy JOIN tblPointValue ON tblStrategy.theIndex = tblPointValue.theIndex WHERE (DataTime > TRY_PARSE('" + startTime.ToString() + "' AS DATETIME USING 'en-gb') AND DataTime < TRY_PARSE('" + endTime.ToString() + "' AS DATETIME USING 'en-gb')) AND (Point like '" + point + "')";
                    return ctx.BoxModule.SqlQuery(sql).ToList();
                }
                else {
                    List<BoxModule> emptyList = new List<BoxModule>();
                    return emptyList;
                }
                }
        }


    }
}   
