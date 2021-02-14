using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    class FuelCalculator
    {

        private double currentReading;
        private double previousReading;
        private double fuelAmount;
        private double unitPrice;

        private double distance;
        private double literPerKm;

        private const double kmToMileFactor = 0.621371192;


        public FuelCalculator()
        {

        }

        public FuelCalculator(double currentReading, double previousReading, double fuelAmount, double unitPrice)
        {
            this.currentReading = currentReading;
            this.previousReading = previousReading;
            this.fuelAmount = fuelAmount;
            this.unitPrice = unitPrice;
        }


        public double GetCurrentReading()
        {
            return currentReading;
        }

        public void SetCurrentReading(double currentReading)
        {
            if(currentReading > previousReading)
                 this.currentReading = currentReading;
        }

        public double GetPreviousReading()
        {
            return previousReading;
        }

        public void SetPreviousReading(double previousReading)
        {
            if(previousReading > 0.0) 
                this.previousReading = previousReading;
        }

        public double GetFuelAmount()
        {
            return fuelAmount;
        }

        public void SetFuelAmount(double fuelAmount)
        {
            this.fuelAmount = fuelAmount;
        }

        public double GetUnitPrice()
        {
            return unitPrice;
        }

        public void SetUnitPrice(double unitPrice)
        {
            this.unitPrice = unitPrice;
        }

        public double CalculaterKmPerLiter()
        {
            distance = currentReading - previousReading;
            return distance / fuelAmount;
        }

        public double CalculateLiterPerKm()
        {
            literPerKm = fuelAmount / distance;
            return literPerKm;
        }

        public double CalculaterLiterPerMetricMile()
        {
            return literPerKm / kmToMileFactor;
        }

        public double CalculaterLiterPerSwedishMile()
        {
            return literPerKm * 10;
        }

        public double CalculaterCostPerKm()
        {
            return literPerKm * unitPrice;
        }

    }
}
