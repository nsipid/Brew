using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Recipes
{
    public class CommentsRecipeViewModel : RecipeViewModel
    {
        public List<CommentViewModel> Comments { get; set; }
        public List<string> Flavors { get; set; }
        public string Flavor { get; set; }
        public string Text { get; set; }
    }
}