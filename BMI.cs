using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    class BMI
    {
        private string Username;
        private double height;
        private UnitTypes unit;
        private double weight;

        private double BMIValue;


        public string GetName()
        {
            return Username;
        }

        public double GetHeight()
        {
            return height;
        }

        public UnitTypes GetUnit()
        {
            return unit;
        }

        public double GetWeight()
        {
            return weight;
        }

        public void SetName(string name)
        {
            Username = name;
        }

        public void SetHeight(double heightFromUser)
        {
            if (heightFromUser > 0.0) height = heightFromUser;
        }

        public void SetUnitType(UnitTypes type)
        {
            if (type == UnitTypes.Metric)
            {
                unit = UnitTypes.Metric;
            }
            else
            {
                unit = UnitTypes.Imperical;
            }
        }

        public void SetWeight(double weightFromUser)
        {
            if (weightFromUser > 0.0) weight = weightFromUser;
        }

        public double CalculateBMI()
        {
            if(unit == UnitTypes.Metric)
            {
                BMIValue = (weight / (height * height));
            }
            else
            {
                BMIValue = ((703.0 * weight) / (height * height));
            }

            return BMIValue;
        }


        public string BMICategory()
        {
            string category = "";

            if (BMIValue < 18.5)
            {
                category = "Under weight";
            } else if (BMIValue >= 18.5 && BMIValue <= 24.9)
            {
                category = "Normal weight";
            } else if (BMIValue > 24.9 && BMIValue <= 29.9)
            {
                category = "Over weight (Pre-obesity)";
            } else if (BMIValue > 29.9 && BMIValue <= 34.9)
            {
                category = "Over weight (Pre obesity class I)";
            } else if (BMIValue > 34.9 && BMIValue <= 39.9)
            {
                category = "Over weight (Pre obesity class II)";
            } else if (BMIValue > 39.9)
            {
                category = "Over weight (Pre obesity class III)";
            }

            return category;
        }
    }
}
