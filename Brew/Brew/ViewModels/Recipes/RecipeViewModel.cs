using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Recipes
{
    public abstract class RecipeViewModel : BaseLayoutViewModel
    {
        public ulong BeerId { get; set; }

        [DisplayName("Name")]
        public string BeerName { get; set; }

        public List<string> Creators { get; set; }

        public decimal YourRating { get; set; }

        public decimal SiteRating { get; set; }

        public decimal AvgRating { get; set; }

        public ulong NumFavorites { get; set; }

        public bool IsYourFavorite { get; set; }

        public DateTime PostedDate { get; set; }

        public ulong VisitedCount { get; set; }
    }
}