using Brew.ViewModels.Ingredients;
using System.Collections.Generic;

namespace Brew.ViewModels.Recipes
{
    public class DetailRecipeViewModel : RecipeViewModel
    {
        public string Style { get; set; }

        public string RecipeType { get; set; }

        public double Abv { get; set; }

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

        public DetailRecipeViewModel()
        {
            HopsUsed = new List<HopViewModel>()
                {
                    new HopViewModel()
                        {
                            Name = "Hop1",
                            Alpha = 0.23,
                            Beta = 0.33,
                            PercentCaryophyllene = 20,
                            PercentCohumulone = 10,
                            PercentHumulene = 50,
                            PercentMyrcene = 20,
                            Amount = 123
                        },
                     new HopViewModel()
                        {
                            Name = "Hop2",
                            Alpha = 0.23,
                            Beta = 0.33,
                            PercentCaryophyllene = 20,
                            PercentCohumulone = 10,
                            PercentHumulene = 50,
                            PercentMyrcene = 20,
                            Amount = 123
                        },
                };
            FermentablesUsed = new List<FermentableViewModel>();
        }
    }
}