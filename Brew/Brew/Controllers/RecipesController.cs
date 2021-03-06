﻿using System.IO;
using Brew.Filters;
using Brew.Models;
using Brew.ViewModels;
using Brew.ViewModels.Ingredients;
using Brew.ViewModels.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web;


namespace Brew.Controllers
{
    [InitializeSimpleMembership]
    public class RecipesController : Controller
    {
        public ActionResult Create()
        {
            using (var context = new Models.ModelsContext())
            {
                var styles =
                    ((from s in context.Styles select new SelectListItem {Text = s.Name, Value = s.Name}).ToList());
                var types =
                    ((from s in context.RecipieTypes select new SelectListItem {Text = s.Name, Value = s.Name}).ToList());

                return View(new DetailRecipeViewModel { IsNewRecipe = true, Styles = styles, RecipeTypes = types, Style = styles.FirstOrDefault().Value, RecipeType = types.FirstOrDefault().Value });
            }       
        }

        [HttpPost]
        public ActionResult Create(DetailRecipeViewModel viewModel, string submission = "Apply")
        {
            return HandleRecipeEditor(viewModel, true, submission);
        }

        [HttpPost]
        public ActionResult Delete(DetailRecipeViewModel vm)
        {
            using (var context = new Models.ModelsContext())
            {
                var removal = context.Recipes.Find(vm.BeerName);
                context.Recipes.Remove(removal);
                context.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult UpdateRating(DetailRecipeViewModel vm)
        {
            using (var context = new Models.ModelsContext())
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                var rating = context.Ratings.Where(x => x.Recipe_Name == vm.BeerName && x.UserProfile_UserID == userID).Select(x => x);
                if (rating.Count() == 0)
                {
                    Models.Rating newRating = new Rating();
                    newRating.Recipe_Name = vm.BeerName;
                    newRating.UserProfile_UserID = WebSecurity.GetUserId(User.Identity.Name);
                    newRating.Rating_Score = vm.Rating;
                    context.Ratings.Add(newRating);
                }
                else
                {
                    rating.Single().Rating_Score = vm.Rating;
                }
                context.SaveChanges();
            }

            return RedirectToAction("Show", new { name = vm.BeerName, tab = "details" });
        }

        public ActionResult List(string sortby = "hoppin", int pageno = 0)
        {           
            using (var context = new Models.ModelsContext())
            {
                var siteAverages = Utilities.StatisticsUtils.GetSiteAvg();
                var localAverages = Utilities.StatisticsUtils.GetLocalAvg();

                var random = new Random();
                IOrderedEnumerable<RecipeListItemViewModel> recipes;

                List<RecipeListItemViewModel> allRecipes = new List<RecipeListItemViewModel>();
                foreach (var r in context.Recipes.Include("Style").Include("Brewers").Include("RecipeFermentables").ToList())
                {
                    var vm = new RecipeListItemViewModel
                        {                       
                            Name = r.Name,
                            Style = r.Style == null ? "Unknown" : r.Style.StyleType_Name,
                            User = r.Brewers.Count == 0 ? "Unknown" : r.Brewers.FirstOrDefault().UserName,
                            OG = r.OG,
                            FG = r.FG,
                            Date = r.Date
                        };

                    double siteRating = 0;
                    siteAverages.TryGetValue(r.Name, out siteRating);

                    double avgRating = 0;
                    localAverages.TryGetValue(r.Name, out avgRating);

                    vm.SiteRating = Math.Round(siteRating, 2);
                    vm.AvgRating = Math.Round(avgRating, 2);
                    vm.Color = Math.Round(Utilities.BrewCharacteristicAlgs.CalculateColor(GetFermentablesUsed(r) ?? new List<FermentableViewModel>()),2);
                    allRecipes.Add(vm);
                }

                if (sortby == "hoppin")
                {
                    recipes = allRecipes.OrderByDescending(r => r.SiteRating);
                }
                else if (sortby == "fresh")
                {
                    recipes = allRecipes.OrderByDescending(r => r.Date);
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

                var siteAverages = Utilities.StatisticsUtils.GetSiteAvg();
                var localAverages = Utilities.StatisticsUtils.GetLocalAvg();

                double siteRating = 0;
                siteAverages.TryGetValue(name, out siteRating);

                double avgRating = 0;
                localAverages.TryGetValue(name, out avgRating);

                CommentsRecipeViewModel viewModel = new CommentsRecipeViewModel
                {
                    BeerName = recipeModel.Name,
                    AvgRating = Math.Round(avgRating, 2),
                    SiteRating = Math.Round(siteRating, 2),
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
                var siteAverages = Utilities.StatisticsUtils.GetSiteAvg();
                var localAverages = Utilities.StatisticsUtils.GetLocalAvg();

                var flavorCounts = from c in context.Comments where c.Recipe_Name == name group c by c.FlavorProfile.Name into g select new { Key = g.Key, Count = g.LongCount() };
                var recipeComments = from c in context.Comments where c.Recipe_Name == name select c.Timestamp;
                var recipeModel = context.Recipes.Include("Style").Include("RecipieType").Where(r => r.Name == name).FirstOrDefault();
                var styles = ((from s in context.Styles select new SelectListItem { Text = s.Name, Value = s.Name }).ToList());
                var types = ((from s in context.RecipieTypes select new SelectListItem { Text = s.Name, Value = s.Name }).ToList());
                var styleModel = recipeModel.Style;
                var typeModel = recipeModel.RecipieType;

                int ratingScore = 0;
                if (WebSecurity.IsAuthenticated)
                {
                    int userID = WebSecurity.GetUserId(User.Identity.Name);
                    var rating = context.Ratings.Where(x => x.Recipe_Name == name && x.UserProfile_UserID == userID).Select(x => x);
                    ratingScore = rating.Count() != 0 ? rating.FirstOrDefault().Rating_Score : 0;
                }

                if (recipeModel == null) return null;

                double siteRating = 0;
                siteAverages.TryGetValue(name, out siteRating);

                double avgRating = 0;
                localAverages.TryGetValue(name, out avgRating);

                var styleString = styleModel == null ? context.Styles.FirstOrDefault().Name : styleModel.Name;
                var typeString = typeModel == null ? context.RecipieTypes.FirstOrDefault().Name : typeModel.Name;

                var recipeViewModel = new DetailRecipeViewModel
                {
                    Rating = ratingScore,
                    AvgRating = Math.Round(avgRating, 2),
                    BeerName = name,
                    Carbonation = recipeModel.Carbonation,
                    Creators = recipeModel.Brewers.Select(b => b.UserName).ToList(),
                    FinalGravity = recipeModel.FG,
                    OriginalGravity = recipeModel.OG,
                    PostedDate = recipeModel.Date,
                    SiteRating = Math.Round(siteRating, 2),
                    FlavorCounts = flavorCounts.ToDictionary(g => g.Key, g => g.Count),
                    CommentsCount = recipeComments.Count(),
                    FermentablesUsed = GetFermentablesUsed(recipeModel),
                    RemovedFermentables = new List<string>(),
                    RemovedHops = new List<string>(),
                    HopsUsed = GetHopsUsed(recipeModel),
                    HopToAdd = new HopViewModel(),
                    FermentableToAdd = new FermentableViewModel(),
                    Styles = styles,
                    RecipeTypes = types,
                    Style = styleString,
                    RecipeType = typeString
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
                                    UID = s.UID,
                                    DecoctionAmount = s.DecoctionAmount,
                                    EndTempCel = s.EndTemp,
                                    InfuseAmountLiters = s.InfuseAmount,
                                    SequenceNumber = s.SequenceNumber,
                                    StepTempCel = s.StepTemp,
                                    StepTimeMin = TimeSpan.FromMinutes(s.StepTime),
                                    RampTimeMin = TimeSpan.FromMinutes(s.RampTime),
                                    InfuseTempCel = s.InfuseTemp,
                                    Name = s.Name,
                                    Type = s.MashStepType_Name
                                }).OrderBy(s => s.SequenceNumber).ToList()
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

        private ActionResult PartialHopDelete(DetailRecipeViewModel vm, string name, bool isNewRecipe)
        {
            if (name != null)
            {
                var removal = vm.HopsUsed.FirstOrDefault(u => u.Name == name);
                vm.RemovedHops.Add(name);
                vm.HopsUsed.Remove(removal);                  
            }
            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialHopAdd(DetailRecipeViewModel vm, ModelsContext context, bool isNewRecipe)
        {
            if (vm.HopToAdd != null)
            {
                var removedHop = vm.RemovedHops.FirstOrDefault(u => u == vm.HopToAdd.Name);
                if (removedHop != null)
                    vm.RemovedHops.Remove(removedHop);

                var addition = context.Hops.Find(vm.HopToAdd.Name);
                if (addition != null)
                {
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
            }

            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialFermentableAdd(DetailRecipeViewModel vm, ModelsContext context, bool isNewRecipe)
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
            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialFermentableDelete(DetailRecipeViewModel vm, string name, bool isNewRecipe)
        {
            if (name != null)
            {
                var removal = vm.FermentablesUsed.FirstOrDefault(u => u.Name == name);
                vm.RemovedFermentables.Add(name);
                vm.FermentablesUsed.Remove(removal);
            }
            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialMashStepAdd(DetailRecipeViewModel vm, bool isNewRecipe)
        {
            if ( vm.Mash != null)
            {
                vm.Mash.Steps.Add(new MashStepViewModel());
            }

            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialMashStepDelete(DetailRecipeViewModel vm, int uid, bool isNewRecipe)
        {

            var removal = vm.Mash.Steps.FirstOrDefault(u => u.UID == uid);
            vm.Mash.Steps.Remove(removal);
            ModelState.Clear();
            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialMashProfileAdd(DetailRecipeViewModel vm, bool isNewRecipe)
        {
            vm.Mash = new MashProfileViewModel();
            ModelState.Clear();
            return isNewRecipe ? View("Create", vm) : View("Update", vm);
        }

        private ActionResult PartialMashProfileDelete(DetailRecipeViewModel vm, bool isNewRecipe)
        {
            vm.Mash = null;
            ModelState.Clear();

            return isNewRecipe ? View("Create",vm) : View("Update", vm);
        }

        [HttpPost]
        public ActionResult CreateComment(CommentsRecipeViewModel vm)
        {
            using (var context = new ModelsContext())
            {
                try
                {

                    Models.Comment comment = new Comment();
                    comment.Recipe_Name = vm.BeerName;
                    comment.Timestamp = DateTime.Now;
                    comment.UserProfile_UserID = WebSecurity.GetUserId(User.Identity.Name);
                    comment.Text = vm.Text;
                    comment.FlavorProfile_Name = vm.Flavor;

                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return RedirectToAction("Show", new { name = vm.BeerName, tab = "comments" });
                }
            }

            
            return RedirectToAction("Show", new { name = vm.BeerName, tab="comments" });
        }

        [HttpPost]
        public ActionResult Update(DetailRecipeViewModel vm, string submission = "Apply")
        {
            return HandleRecipeEditor(vm, false, submission);
        }

        public ActionResult HandleRecipeEditor(DetailRecipeViewModel vm, bool isNewRecipe, string submission = "Apply")
        {
            try
            {
                using (var context = new ModelsContext())
                {
                    Recipe recipeModel;
                    recipeModel = !isNewRecipe
                                      ? context.Recipes.Find(vm.BeerName)
                                      : new Recipe() {Name = vm.BeerName, Date = DateTime.Now};

                    var styles = ((from s in context.Styles select new SelectListItem {Text = s.Name, Value = s.Name}).ToList());
                    var types =  ((from s in context.RecipieTypes select new SelectListItem { Text = s.Name, Value = s.Name }).ToList());

                    vm.HopsUsed = vm.HopsUsed ?? GetHopsUsed(recipeModel);
                    vm.FermentablesUsed = vm.FermentablesUsed ?? GetFermentablesUsed(recipeModel);
                    vm.RemovedHops = vm.RemovedHops ?? new List<string>();
                    vm.RemovedFermentables = vm.RemovedFermentables ?? new List<string>();
                    vm.PostedDate = isNewRecipe ? DateTime.Now : recipeModel.Date;
                    vm.Styles = styles;
                    vm.RecipeTypes = types;

                    var styleModel = context.Styles.Find(vm.Style);
                    var typeModel = context.RecipieTypes.Find(vm.RecipeType);

                    if (ModelState.IsValid && submission == "Apply")
                    {
                        recipeModel.Style_Name = styleModel.Name ?? context.Styles.FirstOrDefault().Name;
                        recipeModel.RecipieType_Name = typeModel.Name ?? context.RecipieTypes.FirstOrDefault().Name;
                        recipeModel.Carbonation = vm.Carbonation;
                        recipeModel.FG = vm.FinalGravity;
                        recipeModel.OG = vm.OriginalGravity;
                        recipeModel.Date = vm.PostedDate;
                        if (vm.File != null && vm.File.ContentLength > 0)
                        {
                            BinaryReader reader = new BinaryReader(vm.File.InputStream);
                            vm.File.InputStream.Seek(0, SeekOrigin.Begin);
                            var bytes = reader.ReadBytes((int) vm.File.InputStream.Length);

                            recipeModel.Image = bytes;
                        }

                        //replace mash profile
                        if (recipeModel.Mash != null)
                        {
                            List<MashStep> toRemove = recipeModel.Mash.Steps.ToList();
                            foreach (var step in toRemove)
                            {
                                context.MashSteps.Remove(step);
                            }
                            context.MashProfiles.Remove(recipeModel.Mash);
                        }
                        if (vm.Mash != null)
                        {

                            recipeModel.Mash = new MashProfile();
                            recipeModel.Mash.EquipAdjust = vm.Mash.EquipAdjust;
                            recipeModel.Mash.Name = vm.Mash.Name;
                            recipeModel.Mash.GrainTemp = vm.Mash.GrainTemp;
                            recipeModel.Mash.PH = vm.Mash.PH;
                            recipeModel.Mash.SpargeTemp = vm.Mash.SpargeTemp;
                            recipeModel.Mash.TunSpecificHeat = vm.Mash.TunSpecificHeat;
                            recipeModel.Mash.TunTemp = vm.Mash.TunTemp;
                            recipeModel.Mash.Notes = vm.Mash.Notes;
                            recipeModel.Mash.TunWeight = vm.Mash.TunWeight;
                            vm.Mash.Steps = vm.Mash.Steps ?? new List<MashStepViewModel>();
                            for (int i = 0; i < vm.Mash.Steps.Count; i++)
                            {
                                var stepVm = vm.Mash.Steps[i];
                                var stepModel = new MashStep
                                    {
                                        Name = stepVm.Name,
                                        EndTemp = stepVm.EndTempCel,
                                        DecoctionAmount = stepVm.DecoctionAmount,
                                        InfuseAmount = stepVm.InfuseAmountLiters,
                                        InfuseTemp = stepVm.InfuseTempCel,
                                        MashStepType_Name = stepVm.Type,
                                        RampTime = (float) stepVm.RampTimeMin.TotalMinutes,
                                        SequenceNumber = stepVm.SequenceNumber,
                                        StepTemp = stepVm.StepTempCel,
                                        StepTime = (float) stepVm.StepTimeMin.TotalMinutes,
                                    };

                                recipeModel.Mash.Steps.Add(stepModel);
                            }
                            context.MashProfiles.Add(recipeModel.Mash);
                        }

                        if (isNewRecipe)
                        {
                            context.Recipes.Add(recipeModel);
                        }

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
                                rh.Amount = (float) hopVm.AmountKg;
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
                        return RedirectToAction("Show", new {name = vm.BeerName});
                    }

                    var errors =
                        ModelState.Where(x => x.Value.Errors.Count > 0)
                            .Select(x => new {x.Key, x.Value.Errors})
                            .ToArray();

                    if (submission.StartsWith("Delete Hop"))
                    {
                        return PartialHopDelete(vm, submission.Substring(11), isNewRecipe);
                    }

                    if (submission == "Add Hop")
                    {
                        return PartialHopAdd(vm, context, isNewRecipe);
                    }

                    if (submission.StartsWith("Delete Fermentable"))
                    {
                        return PartialFermentableDelete(vm, submission.Substring(19), isNewRecipe);
                    }

                    if (submission == "Add Fermentable")
                    {
                        return PartialFermentableAdd(vm, context, isNewRecipe);
                    }

                    if (submission == "Add Mash Step")
                    {
                        return PartialMashStepAdd(vm, isNewRecipe);
                    }

                    if (submission.StartsWith("Delete Mash Step"))
                    {
                        return PartialMashStepDelete(vm, int.Parse(submission.Substring(17)), isNewRecipe);
                    }

                    if (submission == "Delete Mash Profile")
                    {
                        return PartialMashProfileDelete(vm, isNewRecipe);
                    }

                    if (submission == "Add Mash Profile")
                    {
                        return PartialMashProfileAdd(vm, isNewRecipe);
                    }

                    return isNewRecipe ? View("Create", vm) : View("Update", vm);

                }
            } 
            catch (Exception e)
            {
                return RedirectToAction("Update", new {name = vm.BeerName});
            }
        }
    }
}
