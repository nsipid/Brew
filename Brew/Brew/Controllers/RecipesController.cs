using Brew.Models;
using Brew.ViewModels;
using Brew.ViewModels.Ingredients;
using Brew.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brew.Controllers
{
    public class RecipesController : Controller
    {
        public ActionResult List(string sortby = "hoppin", int pageno = 0)
        {
            using (var context = new Models.ModelsContext())
            {
                IOrderedQueryable<RecipeListItemViewModel> recipes;

                if (sortby == "hoppin")
                {
                    recipes = context.Recipes.Select(r => new RecipeListItemViewModel 
                        {
                            AvgRating = r.TasteRating,
                            Color = 33, // TODO: Using fake color
                            Name = r.Name,
                            SiteRating = r.SiteRating,
                            Style = r.Style.StyleType_Name,
                            User = r.Brewers.FirstOrDefault().UserName,
                            OG = r.OG,
                            FG = r.FG
                        }).OrderBy(r => r.SiteRating);
                }
                else if (sortby == "fresh")
                {
                    recipes = context.Recipes.Select(r => new RecipeListItemViewModel
                        {
                            AvgRating = r.TasteRating,
                            Color = 33, // TODO: Using fake color
                            Name = r.Name,
                            SiteRating = r.SiteRating,
                            Style = r.Style.StyleType_Name,
                            User = r.Brewers.FirstOrDefault().UserName,
                            OG = r.OG,
                            Date = r.Date,
                            FG = r.FG
                        }).OrderBy(p => p.Date);
                }
                else
                {
                    return HttpNotFound();
                }

                var recipePage = new PagedList<RecipeListItemViewModel>(recipes, pageno);

                var viewModel = new RecipeListViewModel 
                    {
                        Beers = recipePage,
                        SortBy = sortby
                    };

                return View(viewModel);
            }
        }

        public DetailRecipeViewModel CreateRecipeViewModel(string name)
        {
            using (var context = new Models.ModelsContext())
            {
                var flavorCounts = from c in context.Comments where c.Recipe_Name == name group c by c.FlavorProfile.Name into g select new { Key = g.Key, Count = g.LongCount() };
                var recipeModel = context.Recipes.Find(name);

                if (recipeModel == null) return null;

                //Todo: compute color using color formula from grains
                var recipeViewModel = new DetailRecipeViewModel
                {
                    AvgRating = recipeModel.TasteRating,
                    BeerName = name,
                    Carbonation = recipeModel.Carbonation,
                    Creators = recipeModel.Brewers.Select(b => b.UserName).ToList(),
                    FinalGravity = recipeModel.FG,
                    OriginalGravity = recipeModel.OG,
                    PostedDate = recipeModel.Date,
                    RecipeType = recipeModel.RecipieType_Name,
                    SiteRating = recipeModel.SiteRating,
                    Style = recipeModel.Style == null ? "Unknown" : recipeModel.Style.StyleType_Name,
                    FlavorCounts = flavorCounts.ToDictionary(g => g.Key, g => g.Count),
                    CommentsCount = context.Comments.Count(),
                    FermentablesUsed = recipeModel.RecipeFermentables.Select(f => new FermentableViewModel
                    {
                        IsAddedAfterBoiling = f.AddAfterBoil,
                        Amount = f.Amount,
                        Color = f.Fermentable.Color,
                        DiastaticPower = f.Fermentable.DiastaticPower,
                        CourseGrainYield = f.Fermentable.Yield + f.Fermentable.CoarseFineDiff,
                        Name = f.Fermentable_Name,
                        Yield = f.Fermentable.Yield
                    }).ToList(),
                    HopsUsed = recipeModel.RecipeHops.Select(r => new HopViewModel
                    {
                        Alpha = r.Hop.Alpha,
                        AmountKg = r.Amount,
                        Beta = r.Hop.Beta,
                        Name = r.Hop_Name,
                        UsedDuring = r.HopUses_Name,
                        Form = r.Hop.HopForm_Name,
                        PercentCaryophyllene = r.Hop.Caryophyllene,
                        PercentCohumulone = r.Hop.Cohumulone,
                        PercentHumulene = r.Hop.Humulene,
                        PercentMyrcene = r.Hop.Myrcene,
                        Stability = r.Hop.HSI,
                        Type = r.Hop.HopType_Name,
                        UsageTimeMin = TimeSpan.FromMinutes(r.Time)
                    }).ToList(),
                    // TODO: IBU, Color, NumRatings, YourRating
                };

                return recipeViewModel;
            }
        }

        public ActionResult Show(string name)
        {
            ActionResult result;

            if (name == null)
            {
                result = HttpNotFound();
            }

            DetailRecipeViewModel vm = CreateRecipeViewModel(name);

            if (vm == null)
            {
                result = HttpNotFound();
            }
            else
            {
                result = View(vm);
            }

            return result;
        }
        
        public ActionResult Update(string name)
        {
            ActionResult result;
            
            if (name == null)
            {
                result = HttpNotFound();
            }

            DetailRecipeViewModel vm = CreateRecipeViewModel(name);

            if (vm == null)
            {
                result = HttpNotFound();
            }
            else
            {
                result = View(vm);
            }
            
            return result;
        }

        [HttpPost]
        public ActionResult Update(DetailRecipeViewModel vm)
        {
            // TODO: Update database
            return View(vm);
        }
    }
}
