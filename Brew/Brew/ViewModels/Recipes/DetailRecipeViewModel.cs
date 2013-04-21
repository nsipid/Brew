using System.ComponentModel;
using System.Web;
using Brew.ViewModels.Ingredients;
using System.Collections.Generic;

namespace Brew.ViewModels.Recipes
{
    public class DetailRecipeViewModel : RecipeViewModel
    {
        public HttpPostedFileBase File { get; set; }
        public bool IsNewRecipe { get; set; }
        public string Style { get; set; }

        public string RecipeType { get; set; }

        [ReadOnly(true)]
        public double Abv { get { return System.Math.Round(Utilities.BrewCharacteristicAlgs.CalculateABV(OriginalGravity, FinalGravity), 2); } }

        public float OriginalGravity { get; set; }

        public float FinalGravity { get; set; }

        public float Carbonation { get; set; }

        public float Ibu { get; set; }

        public float Color { get { return (float)System.Math.Round(Utilities.BrewCharacteristicAlgs.CalculateColor(this.FermentablesUsed), 2); } }

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

        public int Rating { get; set; }
        public int[] Ratings = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public DetailRecipeViewModel()
        {
            HopsUsed = new List<HopViewModel>();
            FermentablesUsed = new List<FermentableViewModel>();
            RemovedFermentables = new List<string>();
            RemovedHops = new List<string>();
        }
    }
}