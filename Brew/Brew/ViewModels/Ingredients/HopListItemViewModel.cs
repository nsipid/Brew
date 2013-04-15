using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class HopListItemViewModel
    {
        public string Name { get; set; }
        public double Alpha { get; set; }
        public double Beta { get; set; }
        public string Type { get; set; }
    }
}