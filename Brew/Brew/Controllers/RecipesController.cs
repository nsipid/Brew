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

        public ActionResult Show(string strBeerName)//ulong id, string tab)
        {
            //retrieve counts of comment flavor profiles for this recipe
            long lCount;
            Dictionary<string, long> oFlavorCounts = new Dictionary<string, long>();
            using (var context = new Models.ModelsContext())
            {
                var comments = from c in context.Comments
                               where c.Recipe.Name == strBeerName 
                               select c.FlavorProfile.Name;

                //accumulate flavor profile counts
                lCount = comments.Count();
                string strName;                
                foreach (var x in comments)
                {
                    strName = (string)x;
                    if (!oFlavorCounts.ContainsKey(strName))
                        oFlavorCounts[strName] = 1;
                    else
                        oFlavorCounts[strName]++;
                }
            }
            
            var vm = new DetailRecipeViewModel() {BeerName = strBeerName, FlavorCounts = oFlavorCounts, CommentsCount = lCount};

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
