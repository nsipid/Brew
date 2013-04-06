using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using Brew.Utilities;

namespace Brew.ViewModels.Recipes
{
    public class RecipesViewModel : BaseLayoutViewModel
    {
        public string SortBy { get; set; }

        public readonly PagedList<BeerSummaryViewModel> Beers;

        public RecipesViewModel(uint pageNo)
        {
            SortBy = "hoppin";
            var allBeers = new List<BeerSummaryViewModel>();
            using (var context = new Models.ModelsContext())
            {
                var dbModel = (from r in context.Recipes.Include(p => p.Style) select r);
                foreach (var recipie in dbModel)
                {                     
                    allBeers.Add(new BeerSummaryViewModel
                    {
                        Abv = BrewCharacteristicAlgs.CalculateABV(recipie.OG, recipie.FG),                                 // from equation
                        AvgRating = (decimal)recipie.TasteRating, // average of all votes
                        SiteRating = (decimal)recipie.SiteRating, 
                        Color = recipie.Efficiency,
                        Id = 1,
                        Name = recipie.Name,
                        Style = recipie.Style.Name,
                        User = "McGroober"
                    });
                }
            }

            Beers = new PagedList<BeerSummaryViewModel>(allBeers.GetRange((int)(pageNo * 30), (int)Math.Min(allBeers.Count, pageNo * 30 + 30)), allBeers.Count < 30 ? 0 : (uint)Math.Ceiling(allBeers.Count / 30d), pageNo, 30);
        }     
    }
}