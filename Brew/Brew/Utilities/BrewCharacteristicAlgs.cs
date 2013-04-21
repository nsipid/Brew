using Brew.ViewModels.Ingredients;
using Brew.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.Utilities
{
    public class BrewCharacteristicAlgs
    {
        public static double CalculateABV(double dOG, double dFG)
        {
            return (dOG - dFG) * 131.25;
        }

        //http://brewwiki.com/index.php/Estimating_Color
        public static double CalculateColor(List<FermentableViewModel> grains)
        {
            const double kgtolbs = 2.20462;
            const double gallons = 10;
            double color = 0;
            double mcu = 0;
            foreach (var grain in grains)
            {
                mcu += grain.Amount * kgtolbs + grain.Color / gallons;
            }

            return 1.4922 * Math.Pow(mcu, 0.6859);
        }
    }
}