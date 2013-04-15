using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class FermentableListItemViewModel
    {
        public double Color { get; set; }
        public string Name { get; set; }
        public double IBUs { get; set; }
    }
}