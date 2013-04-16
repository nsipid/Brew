using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Brew.ViewModels.Recipes
{
    public class RecipeViewModel : BaseLayoutViewModel
    {
        [DisplayName("Name")]
        public string BeerName { get; set; }

        public List<string> Creators { get; set; }

        public double YourRating { get; set; }

        public double SiteRating { get; set; }

        public double AvgRating { get; set; }

        public ulong NumRatings { get; set; }

        public DateTime PostedDate { get; set; }

        public Dictionary<string, long> FlavorCounts { get; set; }
        public long CommentsCount { get; set; } 
    }
}