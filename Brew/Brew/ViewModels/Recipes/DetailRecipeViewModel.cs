using Brew.ViewModels.Ingredients;
using System.Collections.Generic;

namespace Brew.ViewModels.Recipes
{
    public class DetailRecipeViewModel : RecipeViewModel
    {
        public string Style { get; set; }

        public string RecipeType { get; set; }

        public double Abv { get { return Utilities.BrewCharacteristicAlgs.CalculateABV(OriginalGravity, FinalGravity); } }

        public double OriginalGravity { get; set; }

        public double FinalGravity { get; set; }

        public double Carbonation { get; set; }

        public double Ibu { get; set; }

        public double Color { get; set; }

        public List<HopViewModel> HopsUsed { get; set; }

        /// <summary>
        /// For the NoJS use case.
        /// </summary>
        public string HopToAdd { get; set; }

        public List<FermentableViewModel> FermentablesUsed { get; set; }

        public Dictionary<string, long> FlavorCounts { get; set; }
        public long CommentsCount { get; set; }   
    }
}