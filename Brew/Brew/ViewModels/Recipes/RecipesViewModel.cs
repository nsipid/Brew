using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            for (int i = 1; i < 1000; i++)
            {
                allBeers.Add(new BeerSummaryViewModel
                    {
                        Abv = 10,
                        AvgRating = 9.9m,
                        SiteRating = 9.8m,
                        Color = 71,
                        Id = 1,
                        Name = "McGroober's Finest <script>ruhroh!</script>",
                        Style = "Irish Stout",
                        User = "McGroober"
                    });
                allBeers.Add(new BeerSummaryViewModel
                    {
                        Abv = 10,
                        AvgRating = 9.9m,
                        SiteRating = 9.8m,
                        Color = 71,
                        Id = 2,
                        Name = "McGroober's Finest",
                        Style = "Irish Stout",
                        User = "McGroober"
                    });
                allBeers.Add(new BeerSummaryViewModel
                    {
                        Abv = 10,
                        AvgRating = 9.9m,
                        SiteRating = 9.8m,
                        Color = 71,
                        Id = 3,
                        Name = "McGroober's Finest",
                        Style = "Irish Stout",
                        User = "McGroober"
                    });
            }
            Beers = new PagedList<BeerSummaryViewModel>(allBeers.GetRange((int) (pageNo*30), (int) Math.Min(allBeers.Count, pageNo*30 + 30)), (uint) Math.Ceiling(allBeers.Count/30d), pageNo, 30);
        }

       
    }
}