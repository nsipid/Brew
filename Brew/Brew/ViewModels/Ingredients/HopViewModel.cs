using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class HopViewModel : IngredientViewModel
    {
        public string Type { get; set; }
        [Range(0.0, 100)]
        public double Alpha { get; set; }
        [Range(0.0, 100)]
        public double Beta { get; set; }
        public double AmountKg { get; set; }
        [Range(0.0, 100)]
        public double PercentHumulene { get; set; }
        [Range(0.0, 100)]
        public double PercentCaryophyllene { get; set; }
        [Range(0.0, 100)]
        public double PercentCohumulone { get; set; }
        [Range(0.0, 100)]
        public double PercentMyrcene { get; set; }
        public string UsedDuring { get; set; }
        public TimeSpan UsageTimeMin { get; set; }
        public string Form { get; set; }
        [Range(0.0, 100)]
        public double Stability { get; set; }
    }
}