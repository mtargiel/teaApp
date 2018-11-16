using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corealate
{
    class Tea : ITea
    {
        private double _brewingTemperature;
        private double _brewingSeconds;

        public string TeaName { get; set; }

        public double BrewingTemperature { get => _brewingTemperature; set => _brewingTemperature = value; }
        public double BrewingSeconds { get => _brewingSeconds*60; set => _brewingSeconds = value; }
        public string SpecialProperties { get; set; }
        public double UserBrewingTemp { get; set; }
        public double UserBrewingTime { get; set; }

        public Tea(string teaName)
        {
            TeaName = teaName;
        }

        public string PrepareTea()
        {
            return PrepareTea(UserBrewingTemp, UserBrewingTime);
        }

        public virtual string PrepareTea(double temp, double time)
        {

            foreach (DataRow row in DataContext.GetDataTableFromTextFile().Rows)
            {
                if (row[0].ToString() == TeaName)
                {
                    double.TryParse(row[2].ToString(), out _brewingTemperature);
                    double.TryParse(row[3].ToString(), out _brewingSeconds);

                    return CheckTempAndTime(temp, time);

                }
            }

            return "Inwalid tea name";
        }

        private string CheckTempAndTime(double temp, double time)
        {
            if ((BrewingTemperature * 1.1 >= temp && BrewingTemperature * 0.9 <= temp)
                && ((BrewingSeconds) * 1.1 >= time && (BrewingSeconds) * 0.9 <= time))
            {
                return "PERFECT!";
            }
            else if (BrewingTemperature * 1.1 < temp || (BrewingSeconds) * 1.1 < time)
            {
                return "yucky";
            }
            else if (BrewingTemperature * 0.9 > temp || (BrewingSeconds) * 0.9 > time)
            {
                return "weak";
            }
            else
            {
                return "";
            }
        }
    }
}
