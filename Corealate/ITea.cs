using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corealate
{
    public interface ITea
    {
        string TeaName { get; set; }
        double BrewingTemperature { get; set; }
        double BrewingSeconds { get; set; }
        string SpecialProperties { get; set; }
        string PrepareTea();
        string PrepareTea(double temp, double time);
        double UserBrewingTemp { get; set; }
        double UserBrewingTime { get; set; }


    }
}
