using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Brew.Filters;

namespace Brew.Controllers
{
    [InitializeSimpleMembership]
    public class BeerImageController : Controller
    {
        // GET: /BeerImage/
        public FileResult Index(string name)
        {
            using (var context = new Models.ModelsContext())
            {
                var recipe = context.Recipes.Find(name);
                if (recipe != null)
                {
                    if (recipe.Image != null)
                    {
                        return new FileContentResult(recipe.Image, "image/png");
                    }
                }
            }

            return new FilePathResult("/Images/no_beer_pic.png", "image/png");
        }
    }
}
