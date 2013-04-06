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
    }
}