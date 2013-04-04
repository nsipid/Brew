using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class RecipieUtils
    {
        public static RecipieType getRecipieType(string dbValue)
        {
            switch (dbValue)
            {
                case "All Grain":
                    return Models.RecipieType.AllGrain;
                case "Extract":
                    return Models.RecipieType.Extract;
                case "Partial Mash":
                    return Models.RecipieType.PartialMash;              
                default:
                    throw new Exception(dbValue + " is not a RecipieType");
            }
        }
    }

    public enum RecipieType
    {
        AllGrain,
        Extract,
        PartialMash
    }

    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public RecipieType RecipieType { get; set; }
        [Required]
        public virtual ICollection<UserProfile> Brewers { get; set; }
        [Required]
        public virtual ICollection<Fermentable> Fermentables { get; set; }
        [Required]
        public virtual ICollection<Yeast> Yeasts { get; set; }
        public Style Style { get; set; }
        [Required]
        public float BatchSize { get; set; } // Target size of the finished batch in liters.
        [Required]
        public float BoilSize { get; set; } // Starting size for the main boil of the wort in liters.
        [Required]
        public float BoilTime { get; set; } // The total time to boil the wort in minutes.
        public float Efficiency { get; set; } // The percent brewhouse efficiency to be used for estimating the starting gravity of the beer.   Not required for “Extract” recipes, but is required for “Partial Mash” and “All Grain” recipes.
        [Required]
        public virtual ICollection<Hop> Hops { get; set; }
        [Required]
        public virtual ICollection<MashProfile> Mash { get; set; }
        public string Notes { get; set; }
        public string TasteNotes { get; set; }
        public float TasteRating { get; set; } // Number between zero and 50.0 denoting the taste rating – corresponds to the 50 point BJCP rating system.
        public float OG { get; set; } // The measured original (pre-fermentation) specific gravity of the beer.
        public float FG { get; set; } // The measured final gravity of the finished beer.
        public int FermentationStages { get; set; } // The number of fermentation stages used – typically a number between one and three
        public int PrimayAge { get; set; } // Time spent in the primary in days
        public float PrimayTemp { get; set; } // Temperature in degrees Celsius for the primary fermentation.
        public int SecondaryAge { get; set; } // Time spent in the secondary in days.
        public float SecondaryTemp { get; set; } // Temperature in degrees Celsius for the secondary fermentation.
        public int TertiaryAge { get; set; } // Time spent in the tertiary in days.
        public float TertiaryTemp { get; set; } // Temperature in degrees Celsius for the tertiary fermentation.
        public int Age { get; set; } // The time to age the beer in days after bottling.
        public float Temp { get; set; } // Temperature for aging the beer after bottling.
        public DateTime Date { get; set; } // Date brewed 
        public float Carbonation { get; set; } // Floating point value corresponding to the target volumes of CO2 used to carbonate this beer.
        public bool ForcedCarbonation { get; set; } // TRUE if the batch was force carbonated using CO2 pressure, FALSE if the batch was carbonated using a priming agent.  
        public string PrimingSugarName { get; set; } // Text describing the priming agent such as “Honey” or “Corn Sugar” 
        public float CarbonationTemp { get; set; } // The temperature for either bottling or forced carbonation.
        public float PrimingSugarEquiv { get; set; } // Factor used to convert this priming agent to an equivalent amount of corn sugar for a bottled scenario.  
        public float KegPrimingFactor { get; set; } // Used to factor in the smaller amount of sugar needed for large containers. 
    }
}