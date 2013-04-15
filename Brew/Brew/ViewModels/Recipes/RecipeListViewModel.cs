using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

using Brew.Utilities;

namespace Brew.ViewModels.Recipes
{
    public class RecipeListViewModel : BaseLayoutViewModel
    {
        public string SortBy { get; set; }

        public PagedList<RecipeListItemViewModel> Beers;

    
    }
}