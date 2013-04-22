using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brew.ViewModels.Ingredients
{
    public class FermentableViewModel : IngredientViewModel
    {      
        public float Amount { get; set; }
        [Required]
        public float Color { get; set; }
        public bool IsAddedAfterBoiling { get; set; }

        /// <summary>
        /// Fine grain/Extract Yield
        /// </summary>
        [Required, Range(0.0, 100)]
        public double Yield { get; set; }

        /// <summary>
        /// Course grain yield.
        /// </summary>
        /// <remarks>
        /// BeerXML records Course - Fine.. which is not
        /// fine for a model, but not for a view of the model.
        /// </remarks>
        [Range(0.0, 100)]
        public double? CourseGrainYield { get; set; }

        public double DiastaticPower { get; set; }

        public bool IsMashed { get; set; }

        public double IBU { get; set; }
    }
}