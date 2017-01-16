using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.DomainModels
{

    public class BoxModule
    {
        public string Point { get; set; }

        public string theLabel { get; set; }

        [Key]
        public DateTime DataTime { get; set; }

        public string theUnit { get; set; }

        public double DataValue { get; set; }
    }
}