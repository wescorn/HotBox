using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class Calculations
    {
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double Areal { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double TestTimeHours { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double TestTimeSeconds { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double AverageTemp { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double PowerConsumptionStart { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double PowerConsumptionEnd { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double PowerConsumptionDifference { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public double Transmittance { get; set; }


    }
}
