using System.ComponentModel;
using Brew.ViewModels.Ingredients;
using System.Collections.Generic;

namespace Brew.ViewModels.Recipes
{
    public class DetailRecipeViewModel : RecipeViewModel
    {
        public string Style { get; set; }

        public string RecipeType { get; set; }

        [ReadOnly(true)]
        public double Abv { get { return Utilities.BrewCharacteristicAlgs.CalculateABV(OriginalGravity, FinalGravity); } }

        public float OriginalGravity { get; set; }

        public float FinalGravity { get; set; }

        public float Carbonation { get; set; }

        public float Ibu { get; set; }

        public float Color { get; set; }

        public MashProfileViewModel Mash { get; set; }

        public List<HopViewModel> HopsUsed { get; set; }

        /// <summary>
        /// For multi-post edits
        /// </summary>
        public List<string> RemovedHops { get; set; }

        /// <summary>
        /// For multi-post edits
        /// </summary>
        public HopViewModel HopToAdd { get; set; }

        public List<FermentableViewModel> FermentablesUsed { get; set; }

        /// <summary>
        /// For multi-post edits
        /// </summary>
        public List<string> RemovedFermentables { get; set; }

        /// <summary>
        /// For multi-post edits
        /// </summary>
        public FermentableViewModel FermentableToAdd { get; set; }

        public Dictionary<string, long> FlavorCounts { get; set; }
        public long CommentsCount { get; set; }   
    }
}