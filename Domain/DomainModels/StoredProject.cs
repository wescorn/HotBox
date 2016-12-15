using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.DomainModels
{
    public class StoredProject
    {

        public string RT02_BOX1_Top = "P17";
        public string RT01_BOX1_Bund = "P10";

        public string RT02_BOX2_Top = "P21";
        public string RT02_BOX2_Bund = "P20";

        public string Forb_El_ialt_EL01 = "P100";
        public string Forb_El_ialt_EL02 = "P200";


        public PowerConsumption pc;

        //Average Temp Difference between box 1 and 2
        public double atd = 0;

        //Time difference between the Start and End dates(in seconds).
        public double td = 0;

        public StoredProject()
        {
            this.ProjectCalculations = new Calculations();
            this.BoxModules = new List<BoxModule>();
        }
        [Key]
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public string ProjectName { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DisplayFormat(NullDisplayText = "", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        public IEnumerable<BoxModule> BoxModules { get; set; }
        public Calculations ProjectCalculations { get; set; }


        public double GetTimeDifference()
        {
            var ts = (StartTime - EndTime).TotalSeconds;
            td = ts;
            return td;
        }

        public double GetAverageTempDifference()
        {
            double box1Average = 0; int box1Entries = 0;
            double box2Average = 0; int box2Entries = 0;
            double averageDifference = 0;

            foreach (var module in BoxModules)
            {
                if (module.Point == RT02_BOX1_Top || module.Point == RT01_BOX1_Bund)
                {
                    box1Average += module.DataValue;
                    box1Entries += 1;
                }
                if (module.Point == RT02_BOX2_Top || module.Point == RT02_BOX2_Bund)
                {
                    box2Average += module.DataValue;
                    box2Entries += 1;
                }

            }
            box1Average = box1Average / box1Entries;
            box2Average = box2Average / box2Entries;
            averageDifference = box2Average - box1Average;
            atd = averageDifference;
            return atd;
        }

        public double GetTransmittance(double Areal)
        {

            if (pc == null)
            {
                GetPowerConsumption();
            }
            if (atd == 0)
            {
                GetAverageTempDifference();
            }
            if (td == 0)
            {
                GetTimeDifference();
            }
            return (pc.Box1Difference) / (td * atd * Areal);
        }

        public PowerConsumption GetPowerConsumption()
        {
            double Box1Start = 0;
            double Box1End = 0;

            foreach (var module in BoxModules)
            {
                DateTime previousDate = StartTime;
                if (module.Point == Forb_El_ialt_EL01)
                {
                    if (module.DataTime <= previousDate)
                    {
                        Box1Start = module.DataValue;
                    }
                    if (module.DataTime >= previousDate)
                    {
                        Box1End = module.DataValue;
                    }
                    previousDate = module.DataTime;
                }
            }
            pc = new PowerConsumption(Box1Start, Box1End);
            return (pc);
        }

        public void CalculateProjectCalculations()
        {
            ProjectCalculations.StartTime = this.StartTime;
            ProjectCalculations.EndTime = this.EndTime;
            ProjectCalculations.TestTimeHours = (EndTime - StartTime).TotalHours;
            ProjectCalculations.TestTimeSeconds = (EndTime - StartTime).TotalSeconds;
            ProjectCalculations.AverageTemp = GetAverageTempDifference();
            ProjectCalculations.PowerConsumptionStart = GetPowerConsumption().Box1Start;
            ProjectCalculations.PowerConsumptionEnd = GetPowerConsumption().Box1End;
            ProjectCalculations.PowerConsumptionDifference = GetPowerConsumption().Box1Difference;
        }


    }



}