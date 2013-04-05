using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brew.ViewModels.Recipes
{
    public class BeerSummaryViewModel
    {
        public int Id { get; set; }
        public decimal SiteRating { get; set; }
        public decimal AvgRating { get; set; }
        public double Abv { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Style { get; set; }
        public double Color { get; set; }
    }
}
