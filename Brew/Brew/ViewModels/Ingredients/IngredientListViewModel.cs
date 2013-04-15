using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class IngredientListViewModel : BaseLayoutViewModel
    {
        public PagedList<FermentableListItemViewModel> Fermentables { get; set; }
        public PagedList<HopListItemViewModel> Hops { get; set; }
        public string Type { get; set; }
    }
}