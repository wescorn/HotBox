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


        public PowerConsumption pc = new PowerConsumption(0,0);

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
                if (module.Point.Equals(RT02_BOX1_Top) || module.Point.Equals(RT01_BOX1_Bund)) 
                {
                    box1Average += module.DataValue;
                    box1Entries += 1;
                }
                if (module.Point.Equals(RT02_BOX2_Top) || module.Point.Equals(RT02_BOX2_Bund))
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
            BoxModule startModule = null;
            BoxModule endModule = null;

            foreach (var module in BoxModules)
            {
                if (module.Point.Equals(Forb_El_ialt_EL01))
                {
                    if (startModule == null) { startModule = module; }
                    if (module.DataTime <= startModule.DataTime && module.DataValue != 0)
                    {
                        startModule = module;
                    } else if (startModule.DataValue == 0) { startModule = module; }
                    if (endModule == null) { endModule = module; }
                    if (module.DataTime >= endModule.DataTime)
                    {
                        endModule = module;
                    }
                    else if (endModule.DataValue == 0) { endModule = module; }
                }
            }
            if (startModule != null && endModule != null)
            {
                pc = new PowerConsumption(startModule.DataValue, endModule.DataValue);
            }
            return (pc);
        }

        public void CalculateProjectCalculations()
        {
            ProjectCalculations.StartTime = this.StartTime;
            ProjectCalculations.EndTime = this.EndTime;
            ProjectCalculations.TestTimeHours = (EndTime - StartTime).TotalHours;
            ProjectCalculations.TestTimeSeconds = (EndTime - StartTime).TotalSeconds;
            ProjectCalculations.AverageTemp = GetAverageTempDifference();
            GetPowerConsumption();
            ProjectCalculations.PowerConsumptionStart = pc.Box1Start;
            ProjectCalculations.PowerConsumptionEnd = pc.Box1End;
            ProjectCalculations.PowerConsumptionDifference = pc.Box1Difference;
        }


    }



}