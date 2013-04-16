using Brew.Models;
using Brew.ViewModels;
using Brew.ViewModels.Ingredients;
using Brew.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;


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

        public CommentsRecipeViewModel CreateCommentsRecipeViewModel(string name)
        {
            using (var context = new Models.ModelsContext())
            {
                var flavorCounts = from c in context.Comments where c.Recipe_Name == name group c by c.FlavorProfile.Name into g select new { Key = g.Key, Count = g.LongCount() };
                var recipeComments = from c in context.Comments where c.Recipe_Name == name select c.Timestamp;
                var recipeModel = context.Recipes.Find(name);

                CommentsRecipeViewModel viewModel = new CommentsRecipeViewModel
                {
                    BeerName = recipeModel.Name,
                    AvgRating = recipeModel.TasteRating,
                    SiteRating = recipeModel.SiteRating,
                    FlavorCounts = flavorCounts.ToDictionary(g => g.Key, g => g.Count),
                    CommentsCount = recipeComments.Count()                    
                };

                viewModel.Comments = context.Comments.Where(c => c.Recipe_Name == name).Select(c => new CommentViewModel
                {
                    Flavor = c.FlavorProfile.Name,
                    Text = c.Text,
                    Poster = c.UserProfile.UserName
                }).ToList();

                viewModel.Flavors = context.FlavorProfiles.Select(c => c.Name).ToList();
                viewModel.Top3Flavors = GetTop3Flavors(viewModel.FlavorCounts);

                return viewModel;
            }
        }

        private static Tuple<string, string, string> GetTop3Flavors(Dictionary<string, long> oFlavorCounts)
        {
            string strFlav1 = "";
            string strFlav2 = "";
            string strFlav3 = "";
            long lFlav1Count = 0;
            long lFlav2Count = 0;
            long lFlav3Count = 0;

            foreach(string strFlavor in oFlavorCounts.Keys)
            {
                if(oFlavorCounts[strFlavor] >= lFlav1Count)
                {
                    strFlav3 = strFlav2;
                    lFlav3Count = lFlav2Count;
                    strFlav2 = strFlav1;
                    lFlav2Count = lFlav1Count;
                    strFlav1 = strFlavor;
                    lFlav1Count = oFlavorCounts[strFlavor];
                }
                else if(oFlavorCounts[strFlavor] >= lFlav2Count)
                {
                    strFlav3 = strFlav2;
                    lFlav3Count = lFlav2Count;
                    strFlav2 = strFlavor;
                    lFlav2Count = oFlavorCounts[strFlavor];
                }
                else if(oFlavorCounts[strFlavor] >= lFlav3Count)
                {
                    strFlav3 = strFlavor;
                    lFlav3Count = oFlavorCounts[strFlavor];
                }
            }

            return new Tuple<string, string, string>(strFlav1, strFlav2, strFlav3);
        }

        private static List<HopViewModel> GetHopsUsed(Recipe recipeModel)
        {
            return
                recipeModel.RecipeHops.Select(
                    r =>
                    new HopViewModel
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
                        }).ToList();
            // TODO: IBU, Color, NumRatings, YourRating;
        }

        private static List<FermentableViewModel> GetFermentablesUsed(Recipe recipeModel)
        {
            return
                recipeModel.RecipeFermentables.Select(
                    f =>
                    new FermentableViewModel
                        {
                            IsAddedAfterBoiling = f.AddAfterBoil,
                            Amount = f.Amount,
                            Color = f.Fermentable.Color,
                            DiastaticPower = f.Fermentable.DiastaticPower,
                            CourseGrainYield = f.Fermentable.Yield + f.Fermentable.CoarseFineDiff,
                            Name = f.Fermentable_Name,
                            Yield = f.Fermentable.Yield
                        }).ToList();
        }

        public DetailRecipeViewModel CreateDetailRecipeViewModel(string name)
        {
            using (var context = new Models.ModelsContext())
            {
                var flavorCounts = from c in context.Comments where c.Recipe_Name == name group c by c.FlavorProfile.Name into g select new { Key = g.Key, Count = g.LongCount() };
                var recipeComments = from c in context.Comments where c.Recipe_Name == name select c.Timestamp;
                var recipeModel = context.Recipes.Find(name);

                if (recipeModel == null) return null;

                //Todo: compute color using color formula from grains
                var recipeViewModel = new DetailRecipeViewModel
                {
                    AvgRating = System.Math.Round(Utilities.StatisticsUtils.GetLocalAvg(name)),
                    BeerName = name,
                    Carbonation = recipeModel.Carbonation,
                    Creators = recipeModel.Brewers.Select(b => b.UserName).ToList(),
                    FinalGravity = recipeModel.FG,
                    OriginalGravity = recipeModel.OG,
                    PostedDate = recipeModel.Date,
                    RecipeType = recipeModel.RecipieType_Name,
                    SiteRating = System.Math.Round(Utilities.StatisticsUtils.GetSiteAvg(name),2),
                    Style = recipeModel.Style == null ? "Unknown" : recipeModel.Style.StyleType_Name,
                    FlavorCounts = flavorCounts.ToDictionary(g => g.Key, g => g.Count),
                    CommentsCount = recipeComments.Count(),
                    FermentablesUsed = GetFermentablesUsed(recipeModel),
                    RemovedFermentables = new List<string>(),
                    RemovedHops = new List<string>(),
                    HopsUsed = GetHopsUsed(recipeModel),
                    HopToAdd = new HopViewModel(),
                    FermentableToAdd = new FermentableViewModel()
                };

                if (recipeModel.Mash != null)
                {
                    MashProfileViewModel mash = new MashProfileViewModel
                        {
                            Name = recipeModel.Mash.Name,
                            GrainTemp = recipeModel.Mash.GrainTemp,
                            UID = recipeModel.Mash.UID,
                            EquipAdjust = recipeModel.Mash.EquipAdjust,
                            Notes = recipeModel.Mash.Notes,
                            PH = recipeModel.Mash.PH,
                            SpargeTemp = recipeModel.Mash.SpargeTemp,
                            TunSpecificHeat = recipeModel.Mash.TunSpecificHeat,
                            TunTemp = recipeModel.Mash.TunTemp,
                            TunWeight = recipeModel.Mash.TunWeight,
                            Steps = recipeModel.Mash.Steps.Select(s => new MashStepViewModel
                                {
                                    DecoctionAmount = s.DecoctionAmount,
                                    EndTempCel = s.EndTemp,
                                    InfuseAmountLiters = s.InfuseAmount,
                                    SequenceNumber = s.SequenceNumber,
                                    StepTempCel = s.StepTemp,
                                    StepTimeMin = TimeSpan.FromMinutes(s.StepTime),
                                    InfuseTempCel = s.InfuseTemp,
                                    Name = s.Name,
                                    Type = s.MashStepType_Name
                                }).ToList()
                        };

                    recipeViewModel.Mash = mash;
                }

                return recipeViewModel;
            }
        }

        public ActionResult Show(string name, string tab = "details")
        {
            ActionResult result;

            if (name == null)
            {
                result = HttpNotFound();
            }

            RecipeViewModel vm = null;
            if (tab == "details")
            {
                vm = CreateDetailRecipeViewModel(name);
            }
            else if (tab == "comments")
            {
                vm = CreateCommentsRecipeViewModel(name);
            }

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

            DetailRecipeViewModel vm = CreateDetailRecipeViewModel(name);

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

        private ActionResult PartialHopDelete(DetailRecipeViewModel vm, string name)
        {
            if (name != null)
            {
                var removal = vm.HopsUsed.FirstOrDefault(u => u.Name == name);
                vm.RemovedHops.Add(name);
                vm.HopsUsed.Remove(removal);                  
            }
            return View("Update", vm);
        }

        private ActionResult PartialHopAdd(DetailRecipeViewModel vm, ModelsContext context)
        {
            if (vm.HopToAdd != null)
            {
                var removedHop = vm.RemovedHops.FirstOrDefault(u => u == vm.HopToAdd.Name);
                if (removedHop != null)
                    vm.RemovedHops.Remove(removedHop);

                var addition = context.Hops.Find(vm.HopToAdd.Name);
                vm.HopsUsed.Add(new HopViewModel()
                    {
                        Alpha = addition.Alpha,
                        Beta = addition.Beta,
                        Name = addition.Name,
                        PercentCaryophyllene = addition.Caryophyllene,
                        PercentHumulene = addition.Humulene,
                        PercentMyrcene = addition.Myrcene,
                        PercentCohumulone = addition.Cohumulone,
                        Stability = addition.HSI,
                        UsageTimeMin = vm.HopToAdd.UsageTimeMin,
                        UsedDuring = vm.HopToAdd.UsedDuring,
                        Form = vm.HopToAdd.Form,
                        AmountKg = vm.HopToAdd.AmountKg,
                    });
            }
            
            return View("Update", vm);
        }

        private ActionResult PartialFermentableAdd(DetailRecipeViewModel vm, ModelsContext context)
        {
            if (vm.FermentableToAdd != null)
            {
                var removedFermentable = vm.RemovedFermentables.FirstOrDefault(u => u == vm.FermentableToAdd.Name);
                if (removedFermentable != null)
                    vm.RemovedHops.Remove(removedFermentable);

                var addition = context.Fermentables.Find(vm.FermentableToAdd.Name);
                vm.FermentablesUsed.Add(new FermentableViewModel()
                {
                    Name = addition.Name,
                    CourseGrainYield = addition.Yield + addition.CoarseFineDiff,
                    Color = addition.Color,
                    DiastaticPower = addition.DiastaticPower,
                    Yield = addition.Yield,
                    IsAddedAfterBoiling = vm.FermentableToAdd.IsAddedAfterBoiling,
                    Amount = vm.FermentableToAdd.Amount,
                });
            }
            return View("Update", vm);
        }

        private ActionResult PartialFermentableDelete(DetailRecipeViewModel vm, string name)
        {
            if (name != null)
            {
                var removal = vm.FermentablesUsed.FirstOrDefault(u => u.Name == name);
                vm.RemovedFermentables.Add(name);
                vm.FermentablesUsed.Remove(removal);
            }
            return View("Update", vm);
        }

        [HttpPost]
        public ActionResult CreateComment(CommentsRecipeViewModel vm, CommentViewModel cm)
        {
            using (var context = new ModelsContext())
            {
                Models.Comment comment = new Comment();
                comment.Recipe_Name = vm.BeerName;
                comment.Timestamp = DateTime.Today;
                comment.UserProfile_UserID = WebSecurity.GetUserId(User.Identity.Name);
                comment.Text = cm.Text;
                comment.FlavorProfile_Name = cm.Flavor;

                context.Comments.Add(comment);
                context.SaveChanges();
            }

            
            return RedirectToAction("Show", new { name = vm.BeerName, tab="comments" });
        }

        [HttpPost]
        public ActionResult Update(DetailRecipeViewModel vm, string submission)
        {
            using (var context = new ModelsContext())
            {
                var recipeModel = context.Recipes.Find(vm.BeerName);

                vm.HopsUsed = vm.HopsUsed ?? GetHopsUsed(recipeModel);
                vm.FermentablesUsed = vm.FermentablesUsed ?? GetFermentablesUsed(recipeModel);
                vm.RemovedHops = vm.RemovedHops ?? new List<string>();
                vm.RemovedFermentables = vm.RemovedFermentables ?? new List<string>();

                if (ModelState.IsValid && submission == "Apply")
                {
                   
                    recipeModel.Carbonation = vm.Carbonation;
                    recipeModel.FG = vm.FinalGravity;
                    recipeModel.OG = vm.OriginalGravity;

                    //add new hops
                    foreach (var hopVm in vm.HopsUsed)
                    {
                        var hopModel = context.Hops.Find(hopVm.Name);
                        var recipeHop = context.RecipeHops.Find(recipeModel.Name, hopVm.Name);

                        if (recipeHop == null)
                        {
                            RecipeHop rh = new RecipeHop();
                            rh.Recipe_Name = recipeModel.Name;
                            rh.Hop = hopModel;
                            rh.Amount = (float)hopVm.AmountKg;
                            rh.Hop_Name = hopModel.Name;
                            rh.HopUses_Name = hopVm.UsedDuring ?? context.HopUses.FirstOrDefault().Name;
                            //must add to context, not the recipe
                            context.RecipeHops.Add(rh);
                        }
                    }
       
                    //add new fermentables
                    foreach (var fermentableVm in vm.FermentablesUsed)
                    {
                        var fermentableModel = context.Fermentables.Find(fermentableVm.Name);
                        var recipeFermentable = context.RecipeFermentables.Find(recipeModel.Name, fermentableVm.Name);

                        if (recipeFermentable == null)
                        {
                            RecipeFermentable rf = new RecipeFermentable();
                            rf.Recipe_Name = recipeModel.Name;
                            rf.Fermentable_Name = fermentableVm.Name;
                            rf.Amount = fermentableVm.Amount;
                            rf.AddAfterBoil = fermentableVm.IsAddedAfterBoiling;
                            rf.IsMashed = fermentableVm.IsMashed;
                            //must add to context, not the recipe
                            context.RecipeFermentables.Add(rf);
                        }
                    }

                    //remove hops
                    foreach (var hop in vm.RemovedHops)
                    {
                        var toRemove = context.RecipeHops.Find(recipeModel.Name, hop);
                        if (toRemove != null)
                            context.RecipeHops.Remove(toRemove);
                    }

                    //remove fermentables
                    foreach (var fermentable in vm.RemovedFermentables)
                    {
                        var toRemove = context.RecipeFermentables.Find(recipeModel.Name, fermentable);
                        if (toRemove != null)
                            context.RecipeFermentables.Remove(toRemove);
                    }

                    context.SaveChanges();
                    return RedirectToAction("Show", new {name= vm.BeerName});
                }

                var errors =
                    ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new {x.Key, x.Value.Errors}).ToArray();

                //ModelState.Clear();
                //var newModelState = new DetailRecipeViewModel();
                //newModelState.BeerName = vm.BeerName;
                //newModelState.HopsUsed = vm.HopsUsed ?? GetHopsUsed(recipeModel);
                //newModelState.FermentablesUsed = vm.FermentablesUsed ?? GetFermentablesUsed(recipeModel);
                //newModelState.RemovedHops = vm.RemovedHops ?? new List<string>();
                //newModelState.RemovedFermentables = vm.RemovedFermentables ?? new List<string>();
                //newModelState.HopToAdd = vm.HopToAdd;
                //newModelState.FermentableToAdd = vm.FermentableToAdd;
                //newModelState.Carbonation = vm.Carbonation;
                //newModelState.Color = vm.Color;
                //newModelState.FinalGravity = vm.FinalGravity;
                //newModelState.OriginalGravity = vm.OriginalGravity;

                if (submission.StartsWith("Delete Hop"))
                {
                    return PartialHopDelete(vm, submission.Substring(11));
                }

                if (submission == "Add Hop")
                {
                    return PartialHopAdd(vm, context);
                }

                if (submission.StartsWith("Delete Fermentable"))
                {
                    return PartialFermentableDelete(vm, submission.Substring(19));
                }

                if (submission == "Add Fermentable")
                {
                    return PartialFermentableAdd(vm, context);
                }

 
                else
                {
                    return View("Update", vm);
                }
            }
        }
    }
}
