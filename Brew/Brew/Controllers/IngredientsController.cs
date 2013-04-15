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
                return PartialView("~/Views/Ingredients/EditorTemplates/HopViewModel.cshtml", new HopViewModel());

            return View();
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

        public ActionResult ListJson(string type = "hop")
        {
            return Json(new[] {"hop1", "hop2", "hop3"}, JsonRequestBehavior.AllowGet);
        }

    }
}
