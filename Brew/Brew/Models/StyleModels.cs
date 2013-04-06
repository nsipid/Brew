using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class StyleUtils
    {        
        public static StyleType getStyleType(string dbValue)
        {
            switch (dbValue)
            {
                case "Lager":
                    return Models.StyleType.Lager;
                case "Ale":
                    return Models.StyleType.Ale;
                case "Mead":
                    return Models.StyleType.Mead;
                case "Wheat":
                    return Models.StyleType.Wheat;
                case "Mixed":
                    return Models.StyleType.Mixed;
                case "Cider":
                    return Models.StyleType.Cider;
                default:
                    throw new Exception(dbValue + " is not a StyleType");
            }
        }       
    }
       
    public enum StyleType
    {
        Lager,
        Ale,
        Mead,
        Wheat,
        Mixed,
        Cider
    }

    // The term "style" encompasses beer styles.  The beer style may be from the BJCP style guide, Australian, UK or local style guides. 
    [Table("Style")]
    public class Style
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string CategoryNumber { get; set; } // Number or identifier associated with this style category.  
        [Required]
        public string StyleLetter { get; set; } // The specific style number or subcategory letter associated with this particular style.  
        [Required]
        public string StyleGuide { get; set; } // The name of the style guide that this particular style or category belongs to
        public StyleType StyleType { get; set; }
        [Required]
        public float OGMin { get; set; } // The minimum specific gravity as measured relative to water.
        [Required]
        public float OGMax { get; set; } // The maximum specific gravity as measured relative to water.
        [Required]
        public float FGMin { get; set; } // The minimum final gravity as measured relative to water.
        [Required]
        public float FGMax { get; set; } // The maximum final gravity as measured relative to water.
        [Required]
        public float IBUMin { get; set; } // The recommended minimum bitterness for this style as measured in International Bitterness Units (IBUs)
        [Required]
        public float IBUMax { get; set; } // The recommended maximum bitterness for this style as measured in International Bitterness Units (IBUs)            
        [Required]
        public float ColorMin { get; set; } // The minimum recommended color in SRM
        [Required]
        public float ColorMax { get; set; } // The maximum recommended color in SRM
        public float CRABMin { get; set; } // Minimum recommended carbonation for this style in volumes of CO2
        public float CRABMax { get; set; } // The maximum recommended carbonation for this style in volumes of CO2
        public float ABVMin { get; set; } // The minimum recommended alcohol by volume as a percentage.
        public float ABVMax { get; set; } // The maximum recommended alcohol by volume as a percentage.
        public string Notes { get; set; }
        public string Profile { get; set; } // Flavor and aroma profile for this style
        public string Ingredients { get; set; } // Suggested ingredients for this style
        public string Eamples { get; set; } // Example beers of this style.

        public virtual ICollection<Recipe> UsingRecipes { get; set; }

        public Style()
        {
            UsingRecipes = new HashSet<Recipe>();            
        }
    }
}