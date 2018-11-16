using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corealate
{
    class Touareg : ITea, ITouareg
    {
        public ITea BaseOfTea { get; set; }
        public string TeaName { get; set; }
        public double BrewingTemperature { get; set; }
        public double BrewingSeconds { get; set; }
        public string SpecialProperties { get; set; }
        public double UserBrewingTemp { get; set; }
        public double UserBrewingTime { get; set; }

        public Touareg(string teaName)
        {
            TeaName = teaName;
        }
        public string PrepareTea(double temp, double time)
        {
            if (BaseOfTea != null)
            {
                if (BaseOfTea.PrepareTea() == "PERFECT!")
                {
                    Tea touareg = new Tea(TeaName);
                    if (touareg.PrepareTea(temp, time) == "PERFECT!")
                    {
                        return "Congratulations, perfect Touareg";
                    }
                    else
                    {
                        return "Sadly, your Touareg is ruined";
                    }
                }
                else
                {
                    return "Sadly, your Touareg is ruined";
                }
            }
            else
            {
                return BaseOfTea.PrepareTea(temp, time);
            }

            return "";
        }

        public string PrepareTea()
        {
            return PrepareTea(UserBrewingTemp, UserBrewingTemp);
        }
    }
}
