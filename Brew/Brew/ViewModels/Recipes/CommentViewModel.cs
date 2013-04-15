using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Recipes
{
    public class CommentViewModel
    {
        public string Flavor { get; set; }
        public string Text { get; set; }
        public string Poster { get; set; }
    }
}