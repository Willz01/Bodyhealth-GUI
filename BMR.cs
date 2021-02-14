using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    class BMR
    {
        private double weight;
        private double height;
        private bool isFemale;
        private int age;

        private double bmrValue;

        public BMR() {   }



        public double GetWeight()
        {
            return weight;
        }

        public void SetWeight(double weight)
        {
            if (weight > 0.0) this.weight = weight;
        }

        public double GetHeight()
        {
            return height;
        }

        public void SetHeight( double height)
        {
            if (height > 0.0) this.height = height;
        }
      
        
        public bool IsFemaleBool()
        {
            return isFemale;
        }

        public void SetIsFemale(bool gender)
        {
            if(gender == true)
            {
                isFemale = true;
            }
            else
            {
                isFemale = false;
            }
        }

       public double GetAge()
        {
            return age;
        }

        public void SetAge(int age)
        {
            if (age > 0) this.age = age;
        }

        public double BmrValue { get => bmrValue;}

        public double CalculateBMR()
        {
            bmrValue = ((10 * weight) + (6.25 * height)) - (5 * age);
            if (isFemale)
            {
                bmrValue -= 161;
            }
            else
            {
                bmrValue += 5;
            }
            return bmrValue;
        }
    }
}
