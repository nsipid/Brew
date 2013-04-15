using Brew.Models;
using Brew.ViewModels.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brew.Controllers
{
    public class IngredientsController : Controller
    {
        public ActionResult Create(string type = "hop")
        {
            if (Request.IsAjaxRequest())
            {
                if (type == "hop")
                    return PartialView("~/Views/Ingredients/EditorTemplates/HopViewModel.cshtml", new HopViewModel());
                else if(type =="fermentable")
                    return PartialView("~/Views/Ingredients/EditorTemplates/FermentableViewModel.cshtml", new FermentableViewModel());
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateFermentable(FermentableViewModel vm)
        {
            using (var context = new Models.ModelsContext())
            {
                Fermentable newFermentable = new Fermentable();
                newFermentable.CoarseFineDiff = (float) (vm.CourseGrainYield - vm.Yield);
                newFermentable.Color = vm.Color;
                newFermentable.DiastaticPower = (float) vm.DiastaticPower;
                newFermentable.Name = vm.Name;
                newFermentable.Yield = (float) vm.Yield;

                context.Fermentables.Add(newFermentable);
                context.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/Ingredients/DisplayTemplates/FermentableViewModel.cshtml", vm);                   
                }
                else
                {
                    return RedirectToAction("Show", new { name = vm.Name, type = "fermentable" });
                }
            }
        }

        [HttpPost]
        public ActionResult CreateHop(HopViewModel vm)
        {
            using (var context = new Models.ModelsContext())
            {
                Hop newHop = new Hop();

                newHop.Name = vm.Name;
                newHop.Alpha = (float) vm.Alpha;
                newHop.Beta = (float) vm.Beta;
                newHop.Caryophyllene = (float) vm.PercentCaryophyllene;
                newHop.Cohumulone = (float) vm.PercentCohumulone;
                newHop.HSI = (float) vm.Stability;
                newHop.Humulene = (float) vm.PercentHumulene;

                context.Hops.Add(newHop);
                context.SaveChanges();
                if (Request.IsAjaxRequest())
                {
                    return PartialView("~/Views/Ingredients/DisplayTemplates/HopViewModel.cshtml", vm);
                }
                else
                {
                    return RedirectToAction("Show", new { name = vm.Name, type = "hop" });
                }
            }
        }

        public ActionResult List(string type = "hop", int pageno = 0)
        {
            IngredientListViewModel vm = new IngredientListViewModel { Type = type };

            using (var context = new Models.ModelsContext())
            {
                if (type == "hop")
                {
                    var hopsModel = context.Hops.Select(p => new HopListItemViewModel 
                        {
                            Alpha = p.Alpha,
                            Beta = p.Beta,
                            Name = p.Name,
                            Type = p.HopType_Name
                        }).OrderBy(p => p.Name);

                    vm.Hops = new ViewModels.PagedList<HopListItemViewModel>(hopsModel, pageno);
                }
                else if (type == "fermentable")
                {
                    var fermentablesModel = context.Fermentables.Select(p => new FermentableListItemViewModel
                        {
                            Name = p.Name,
                            Color = p.Color,
                            IBUs = p.IBUs
                        }).OrderBy(p => p.Name);

                    vm.Fermentables = new ViewModels.PagedList<FermentableListItemViewModel>(fermentablesModel, pageno);
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return View(vm); 
        }

        public ActionResult Show(string name, string type = "hop")
        { 
            using(var context = new Models.ModelsContext())
            {
                if (type == "hop")
                {
                    var hopModel = context.Hops.Find(name);

                    if (hopModel == null) return HttpNotFound();

                    var hopViewModel = new HopViewModel     
                        {
                            Alpha = hopModel.Alpha,
                            Beta = hopModel.Beta,
                            Name = hopModel.Name,
                            Type = hopModel.HopType_Name,
                            PercentCaryophyllene = hopModel.Caryophyllene,
                            PercentCohumulone = hopModel.Cohumulone,
                            PercentHumulene = hopModel.Humulene,
                            PercentMyrcene = hopModel.Myrcene,
                            Form = hopModel.HopForm_Name,
                            Stability = hopModel.HSI,
                        };

                    return View(hopViewModel);
                }
                else if (type == "fermentable")
                {
                    var fermentableModel = context.Fermentables.Find(name);

                    if (fermentableModel == null) return HttpNotFound();

                    var fermentableViewModel = new FermentableViewModel
                        {
                            Color = fermentableModel.Color,
                            DiastaticPower = fermentableModel.DiastaticPower,
                            CourseGrainYield = fermentableModel.Yield + fermentableModel.CoarseFineDiff,
                            Name = fermentableModel.Name,
                            Yield = fermentableModel.Yield
                        };

                    return View(fermentableViewModel);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        public ActionResult ListJson(string type = "hop", string Term = "")
        {
            using (var context = new Models.ModelsContext())
            {
                if (type == "hop")
                {
                    var hopNames = from hop in context.Hops where hop.Name.StartsWith(Term) select hop.Name;
                    var hopArray = hopNames.ToArray();
                    return Json(hopArray, JsonRequestBehavior.AllowGet);
                }
                else if (type == "fermentable")
                {
                    var fermentableNames = from f in context.Fermentables where f.Name.StartsWith(Term) select f.Name;
                    return Json(fermentableNames.ToArray(), JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new string[0], JsonRequestBehavior.AllowGet);
        }

    }
}
