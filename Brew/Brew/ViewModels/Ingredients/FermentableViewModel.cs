using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class FermentableViewModel : IngredientViewModel
    {      
        public float Amount { get; set; }
        public float Color { get; set; }
        public bool IsAddedAfterBoiling { get; set; }

        /// <summary>
        /// Fine grain/Extract Yield
        /// </summary>
        public double Yield { get; set; }

        /// <summary>
        /// Course grain yield.
        /// </summary>
        /// <remarks>
        /// BeerXML records Course - Fine.. which is not
        /// fine for a model, but not for a view of the model.
        /// </remarks>
        public double? CourseGrainYield { get; set; }

        public double DiastaticPower { get; set; }
    }
}