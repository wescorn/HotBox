using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.DomainModels
{
    public class StoredProject
    {
        public string ProjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}