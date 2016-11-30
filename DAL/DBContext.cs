
using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class DBContext : DbContext
    {

        public DBContext() : base("name=BoxDataDBContext")

        {
            Database.SetInitializer<DBContext>(null);
        }

        public DbSet<BoxModule> BoxModule { get; set; }

    }

}
