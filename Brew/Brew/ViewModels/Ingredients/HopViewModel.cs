﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class HopViewModel : IngredientViewModel
    {
        public string Type { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public double Amount { get; set; }
        public double PercentHumulene { get; set; }
        public double PercentCaryophyllene { get; set; }
        public double PercentCohumulone { get; set; }
        public double PercentMyrcene { get; set; }
        public double UsedDuring { get; set; }
        public TimeSpan UsageTime { get; set; }
        public string Form { get; set; }
        public double Stability { get; set; }
    }
}