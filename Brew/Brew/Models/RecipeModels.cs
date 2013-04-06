using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("RecipieType")]
    public class RecipieType
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("Recipe")]
    public class Recipe
    {   
        [Key, StringLength(75)]
        public string Name { get; set; }
        //[Required]
        public RecipieType RecipieType { get; set; }
        [Required]
        public virtual ICollection<UserProfile> Brewers { get; set; }
        [Required]
        public virtual ICollection<RecipeFermentable> RecipeFermentables { get; set; }
        [Required]
        public virtual ICollection<RecipeYeast> RecipeYeasts { get; set; }
        public Style Style { get; set; }
        [Required]
        public float BatchSize { get; set; } // Target size of the finished batch in liters.
        [Required]
        public float BoilSize { get; set; } // Starting size for the main boil of the wort in liters.
        [Required]
        public float BoilTime { get; set; } // The total time to boil the wort in minutes.
        public float Efficiency { get; set; } // The percent brewhouse efficiency to be used for estimating the starting gravity of the beer.   Not required for “Extract” recipes, but is required for “Partial Mash” and “All Grain” recipes.
        [Required]
        public virtual ICollection<RecipeHop> RecipeHops { get; set; }
        public virtual MashProfile Mash { get; set; }
        public string Notes { get; set; }
        public string TasteNotes { get; set; }
        public float TasteRating { get; set; } // Number between zero and 50.0 denoting the taste rating – corresponds to the 50 point BJCP rating system.
        public float OG { get; set; } // The measured original (pre-fermentation) specific gravity of the beer.
        public float FG { get; set; } // The measured final gravity of the finished beer.
        public int FermentationStages { get; set; } // The number of fermentation stages used – typically a number between one and three
        public float PrimayAge { get; set; } // Time spent in the primary in days
        [Range(-50, 110)] 
        public float PrimayTemp { get; set; } // Temperature in degrees Celsius for the primary fermentation.
        public float SecondaryAge { get; set; } // Time spent in the secondary in days.
        [Range(-50, 110)] 
        public float SecondaryTemp { get; set; } // Temperature in degrees Celsius for the secondary fermentation.
        public float TertiaryAge { get; set; } // Time spent in the tertiary in days.
        [Range(-50, 110)] 
        public float TertiaryTemp { get; set; } // Temperature in degrees Celsius for the tertiary fermentation.
        public float Age { get; set; } // The time to age the beer in days after bottling.
        [Range(-50, 110)] 
        public float AgeTemp { get; set; } // Temperature for aging the beer after bottling.
        public string Date { get; set; } // Date brewed 
        public float Carbonation { get; set; } // Floating point value corresponding to the target volumes of CO2 used to carbonate this beer.
        public bool ForcedCarbonation { get; set; } // TRUE if the batch was force carbonated using CO2 pressure, FALSE if the batch was carbonated using a priming agent.  
        [StringLength(75)]
        public string PrimingSugarName { get; set; } // Text describing the priming agent such as “Honey” or “Corn Sugar” 
        [Range(-50, 110)] 
        public float CarbonationTemp { get; set; } // The temperature for either bottling or forced carbonation.
        public float PrimingSugarEquiv { get; set; } // Factor used to convert this priming agent to an equivalent amount of corn sugar for a bottled scenario.  
        public float KegPrimingFactor { get; set; } // Used to factor in the smaller amount of sugar needed for large containers. 
        public float SiteRating { get; set; } // ADDED for website
        public byte[] Image { get; set; }

        [ForeignKey("RecipieType")]
        public string RecipieType_Name { get; set; }

        public Recipe()
        {
            RecipeFermentables = new HashSet<RecipeFermentable>();
            Brewers = new HashSet<UserProfile>();
            RecipeHops = new HashSet<RecipeHop>();
            RecipeYeasts = new HashSet<RecipeYeast>();      
        }
    }
}