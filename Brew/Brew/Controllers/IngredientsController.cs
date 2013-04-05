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

        public ActionResult List(string type = "hop", string format = "json")
        {
            return Json(new[] {"hop1", "hop2", "hop3"}, JsonRequestBehavior.AllowGet);
        }

    }
}
