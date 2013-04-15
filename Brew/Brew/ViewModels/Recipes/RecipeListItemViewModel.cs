using Brew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brew.ViewModels.Recipes
{
    public class RecipeListItemViewModel
    {
        public double SiteRating { get; set; }
        public double AvgRating { get; set; }
        public double Abv { get { return Utilities.BrewCharacteristicAlgs.CalculateABV(OG, FG); } }
        public double FG { get; set; }
        public string Name { get; set; }
        public double OG { get; set; }
        public string User { get; set; }
        public string Style { get; set; }
        public double Color { get; set; }
        public DateTime Date { get; set; }
    }
}
