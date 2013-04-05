using Brew.ViewModels;
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
        public ActionResult List(string sortby, uint? pageno)
        {
            var vm = new RecipesViewModel(pageno ?? 0)
                {
                    SortBy = String.IsNullOrWhiteSpace(sortby) ? "hoppin" : sortby,
                };

            return View(vm);
        }

        public ActionResult Show(ulong id, string tab)
        {
            var vm = new DetailRecipeViewModel() {BeerId = id};

            return View(vm);
        }

        public ActionResult Update(ulong id)
        {
            var vm = new DetailRecipeViewModel { BeerId = id };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Update(DetailRecipeViewModel vm)
        {
            return View(vm);
        }
    }
}
