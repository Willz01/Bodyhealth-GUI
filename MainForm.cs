using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMICalculator
{
    public partial class activityGB : Form
    {
        private const string Format = "f2";
        BMI bmi = new BMI();
        double bmiValue;

        FuelCalculator fuelCalculator = new FuelCalculator();

        BMR bmr = new BMR();

        // for bmr calculation
        private double weight;
        private double height;

        private bool isInch = false;
        double bmrValue;
        double activeLevel;

        bool flowOKay = false;

        public activityGB()
        {
            InitializeComponent();
            Initializer();
        }

        private void Initializer()
        {
            // set as default valus/units/selected radio buttons
            metricRadio.Checked = true;
            maleRadioBtn.Checked = true;
            nameTB.Text = "No name";
            moderateRadioBtn.Checked = true;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void metricRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (metricRadio.Checked)
            {
                heightLabel.Text = "Height (cm)";
                weightLabel.Text = "Weight (kg)";
                bmi.SetUnitType(UnitTypes.Metric);
                heightTB2.Visible = false;
            }
        }

        private void imperialRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imperialRadio.Checked)
            {
                heightLabel.Text = "Height (ft,in)";
                weightLabel.Text = "Weight (lbs)";
                bmi.SetUnitType(UnitTypes.Imperical);
                heightTB2.Visible = true;
            }
        }

        private void ReadName()
        {
            string name = nameTB.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                bmi.SetName(name);
                flowOKay = true;
            }
            else
            {
                MessageBox.Show("Enter a name","Error");
                flowOKay = false;
            }
        }


        private void ReadHeight()
        {
            if (flowOKay)
            {
            double height;
            double heightValue;
            bool ok = double.TryParse(heightTB1.Text, out heightValue);

            if (ok)
            {
                if(heightValue > 0.0)
                {
                     if (bmi.GetUnit().Equals(UnitTypes.Imperical))
                     {
                          double inchValue;
                          bool InchOkay = double.TryParse(heightTB2.Text, out inchValue);
                          // covert feet value to inch and add to inch value
                          height = (heightValue * 12) + inchValue;
                          bmi.SetHeight(height);
                          isInch = true;
                            flowOKay = true;
                     }
                     else
                      {
                          bmi.SetHeight(heightValue / 100);
                        isInch = false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid height", "Error");
                    flowOKay = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid height", "Error");
                flowOKay = false;
            }
            }
        }

        private void ReadWeight()
        {
            if (flowOKay)
            {
            double weight;
            bool ok = double.TryParse(weightTB.Text.Trim(), out weight);


            if (ok)
            {
                if (weight > 0.0)
                {
                    bmi.SetWeight(weight);
                    flowOKay = true;
                }
                else
                {
                    MessageBox.Show("Invalid weight", "Error");
                    flowOKay = false;
                }
            }
            else
            {
                MessageBox.Show("Invalid weight", "Error");
                flowOKay = false;
            }
            }
           

        }

        private void calculateButton_Click(object sender, EventArgs e)
        {

            ReadName();
            ReadHeight();
            ReadWeight();

            resultGroup.Text = "Results for " + bmi.GetName();
            

            weight = bmi.GetWeight();
            height = bmi.GetHeight();
            CalculateResult();
            DisplayResults();

            weightDisplay.Text = "";

            if (flowOKay)
            {
            StringBuilder stringBuilder = new StringBuilder(weightDisplay.Text);
            stringBuilder.Append("Normal BMI is between 18.50 and 24.9" + "\n");

            if (metricRadio.Checked)
            {
                double tHeight = bmi.GetHeight();
                double tWeight = tHeight * tHeight / 1;
                stringBuilder.Append("Normal weight should be between " + (tWeight * 18.50).ToString(Format) + " and " + (tWeight * 24.9).ToString(Format) + " kg");
                weightDisplay.Text = stringBuilder.ToString();
            }
            else
            {
                Console.WriteLine(bmi.GetHeight());
                double tWeight = bmi.GetHeight() * bmi.GetHeight() / 703;
                stringBuilder.Append("Normal weight should be between " + (tWeight * 18.50).ToString(Format) + " and " + (tWeight * 24.9).ToString(Format) + " lbs");
                weightDisplay.Text = stringBuilder.ToString();
            }
            }
            
           
        }

        private void CalculateResult()
        {
            if (flowOKay)
            {
                bmiValue = bmi.CalculateBMI();
                Console.WriteLine(bmiValue);
            }
            
        }

        private void DisplayResults()
        {
            if (flowOKay)
            {
                BMIResult.Text = bmiValue.ToString(Format);
                weightCat.Text = bmi.BMICategory();
            }
            
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void fuelCalButton_Click(object sender, EventArgs e)
        {
            BackgroundFuelMethodsCaller();
            DisplayFuelResults();  
        }


        private void BackgroundFuelMethodsCaller()
        {
            GetCurrentReading();
            GetPreviousReading();
            GetFuelReading();
            GetPricePerLiter();
        }

        private void DisplayFuelResults()
        {
            fuelConKmPerLitLabel.Text = fuelCalculator.CalculaterKmPerLiter().ToString(Format);
            fuelConLitPerKmLabel.Text = fuelCalculator.CalculateLiterPerKm().ToString(Format);
            fuelConLitPerMileLabel.Text = fuelCalculator.CalculaterLiterPerMetricMile().ToString(Format);
            fuelConLitPerSweMileLabel.Text = fuelCalculator.CalculaterLiterPerSwedishMile().ToString(Format);
            costPerKiloLabel.Text = fuelCalculator.CalculaterCostPerKm().ToString(Format);

        }


        private void GetCurrentReading()
        {
            if(odometerTB.Text == "")
            {
                MessageBox.Show("Enter a valid value", "Error");
            }
            else
            {
                if (double.TryParse(odometerTB.Text.Trim(), out double currentOdoReading))
                {
                    fuelCalculator.SetCurrentReading(currentOdoReading);
                }
                else
                {
                    MessageBox.Show("Enter a valid value", "Error");
                }
            }
        }

        private void GetPreviousReading()
        {
            if (preOdoReading.Text == "")
            {
                MessageBox.Show("Enter a valid value", "Error");
            }
            else
            {
                if (double.TryParse(preOdoReading.Text.Trim(), out double previousOdoReading))
                {
                    fuelCalculator.SetPreviousReading(previousOdoReading);
                }
                else
                {
                    MessageBox.Show("Enter a valid value", "Error");
                }
            }
        }

        private void GetFuelReading()
        {
            if (fuelTB.Text == "")
            {
                MessageBox.Show("Enter a valid value", "Error");
            }
            else
            {
                if (double.TryParse(fuelTB.Text.Trim(), out double fuelAmount))
                {
                    fuelCalculator.SetFuelAmount(fuelAmount);
                }
                else
                {
                    MessageBox.Show("Enter a valid value", "Error");
                }
            }
        }

        private void GetPricePerLiter()
        {
            if (price.Text == "")
            {
                MessageBox.Show("Enter a valid value", "Error");
            }
            else
            {
                if (double.TryParse(price.Text.Trim(), out double priceValue))
                {
                    fuelCalculator.SetUnitPrice(priceValue);
                }
                else
                {
                    MessageBox.Show("Enter a valid value", "Error");
                }
            }
        }

        private void sedentaryRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            activeLevel = 1.2;
        }

        private void lighlyActiveRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            activeLevel = 1.375;
        }

        private void moderateRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            activeLevel = 1.550;
        }

        private void extraActiveRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            activeLevel = 1.9;
        }

        private void veryActiveRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            activeLevel = 1.725;
        }

        private void CalculateBMR()
        {
            if (weight != 0 && height != 0)
            {
                     height = bmi.GetHeight();
                     weight = bmi.GetWeight();
                     if (isInch)
                     {
                         height *= 2.54;
                         bmr.SetHeight(height);
                         bmr.SetWeight(weight/2.2046);
                    }
                     else
                     {
                         height *= 100;
                         bmr.SetHeight(height);
                         bmr.SetWeight(weight);
                    }
                    bmrValue = bmr.CalculateBMR();          
            }
            else
            {
                MessageBox.Show("Provide valid height and weight", "Error");
            }
        }


        private double CaloriesMaintainer()
        {
            return bmrValue * activeLevel;
            
        }

        private void DisplayBmrResults()
        {
            bmrResults.Text = "";
            StringBuilder stringBuilder = new StringBuilder(bmrResults.Text);
            double maintainWeightCalories = CaloriesMaintainer();
            stringBuilder.Append("BMR RESULTS FOR " + bmi.GetName().ToUpper() + "\n");
            stringBuilder.Append("\n");
            stringBuilder.Append("Your BMR (Calories/Day)                                         " + bmrValue.ToString("f1") + "\n");
            stringBuilder.Append("Calories to maintain your weight                              " + maintainWeightCalories.ToString("f1") + "\n");
            stringBuilder.Append("Calories to lose 0,5kg per week                               " + (maintainWeightCalories - 500).ToString("f1") + "\n");
            stringBuilder.Append("Calories to lose 1kg per week                                  " + (maintainWeightCalories - 1000).ToString("f1") + "\n");
            stringBuilder.Append("Calories to add 0,5kg per week                                " + (maintainWeightCalories + 500).ToString("f1") + "\n");
            stringBuilder.Append("Calories to lose 1kg per week                                  " + (maintainWeightCalories + 1000).ToString("f1") + "\n");
            stringBuilder.Append("\n");
            stringBuilder.Append("Losing more than 1000 calories per day is to avoided." + "\n");
            stringBuilder.Append("\n");
            bmrResults.Text = stringBuilder.ToString();
        }

        private void bmrCalculateButton_Click(object sender, EventArgs e)
        {
          if(Convert.ToInt32(bmr.GetAge()) > 0)
          {
                CalculateBMR();
                DisplayBmrResults();    
          }
          else
          {
                MessageBox.Show("Invalid age", "Error");
          }

        }

        private void femaleRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            bmr.SetIsFemale(true);
        }

        private void maleRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            bmr.SetIsFemale(false);
        }

        private void ageSelector_ValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine(ageSelector.Value);
            bmr.SetAge(Convert.ToInt32(ageSelector.Value));
        }  
    }
}
