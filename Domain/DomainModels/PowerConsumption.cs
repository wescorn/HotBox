using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainModels
{
    public class PowerConsumption
    {
        public PowerConsumption(double m_Box1Start, double m_Box1End)
        {
            this.Box1Start = m_Box1Start;
            this.Box1End = m_Box1End;
            this.Box1Difference = Box1Start - Box1End;
        }

        public double Box1Start { get; set; }
        public double Box1End { get; set; }
        public double Box1Difference { get; set; }
    }
}
