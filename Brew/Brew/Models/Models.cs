using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class StyleCategory
    {
        public static readonly string STOUT = "Stout";
        public static readonly string ENGLISH_ALES = "English Ales";
        public static readonly string AMERICAN_LAGERS = "Amercian Lagers";
    }

    public class StyleType
    {
        public static readonly string LAGER = "Lager";
        public static readonly string ALE = "Ale";
        public static readonly string MEAD = "Mead";
        public static readonly string WHEAT = "Wheat";
        public static readonly string MIXED = "Mixed";
        public static readonly string CIDER = "Cider";
    }

    // The term "style" encompasses beer styles.  The beer style may be from the BJCP style guide, Australian, UK or local style guides. 
    public class Style
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public StyleCategory Category { get; set; }
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
        public string Note { get; set; }
        public string Profile { get; set; } // Flavor and aroma profile for this style
        public string Ingredients { get; set; } // Suggested ingredients for this style
        public string Eamples { get; set; } // Example beers of this style.
    }

    public class MashStepType
    {
        public static readonly string INFUSION = "Infusion";
        public static readonly string TEMPERATURE = "Temperature";
        public static readonly string DECOCTION = "Decoction";

    }
    //A mash step is an internal record used within a mash profile to denote a separate step in a multi-step mash. 
    public class MashStep
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public MashStepType MashStepType { get; set; }
        public float InfuseAmount { get; set; } // The volume of water in liters to infuse in this step
        [Required]
        public float StepTemp { get; set; } // The target temperature for this step in degrees Celsius.
        [Required]
        public DateTime StepTime { get; set; } // The number of minutes to spend at this step
        public float RampTemp { get; set; } // Time in minutes to achieve the desired step temperature 
        public float EndTemp { get; set; } // Temperature you can expect the mash to fall to after a long mash step.  Measured in degrees Celsius.
    }

    public class MashProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float GrainTemp { get; set; } // The temperature of the grain before adding it to the mash in degrees Celsius.
        [Required]
        public virtual ICollection<MashStep> Steps { get; set; }
        public string Note { get; set; }
        public float TunTemp { get; set; } // Grain tun temperature – may be used to adjust the infusion temperature for equipment if the program supports it.  Measured in degrees C.
        public float SpargeTemp { get; set; } // Temperature of the sparge water used in degrees Celsius.
        public float PH { get; set; } // The PH of the sparge.
        public float TunWeight { get; set; } // Weight of the mash tun in kilograms
        public float TunSpecificHeat { get; set; } // Specific heat of the tun material in calories per gram-degree C.
        public bool EquipAdjust { get; set; } // If TRUE, mash infusion and decoction calculations should take into account the temperature effects of the equipment (tun specific heat and tun weight).  If FALSE, the tun is assumed to be pre-heated. 
    }

    public class MiscType
    {
        public static readonly string SPACE = "Spice";
        public static readonly string FINING = "Fining";
        public static readonly string WATER_AGENT = "Water Agent";
        public static readonly string HERB = "Herb";
        public static readonly string FLAVOR = "Flavor";
        public static readonly string OTHER = "Other";
    }

    public class MiscUse
    {
        public static readonly string BOIL = "Boil";
        public static readonly string MASH = "Mash";
        public static readonly string PRIMARY = "Primary";
        public static readonly string SECONDARY = "Secondary";
        public static readonly string BOTTLING = "Bottling";
    }

    // The term "misc" encompasses all non-fermentable miscellaneous ingredients that are not hops or yeast and do not significantly change the gravity of the beer.
    public class Misc
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public MiscType MiscType { get; set; }
        [Required]
        public MiscUse MiscUse { get; set; }
        [Required]
        public DateTime Time { get; set; } // Amount of time the misc was boiled, steeped, mashed, etc in minutes.
        [Required]
        public float Amount { get; set; } // Amount of item used.  
        public bool? AmoutIsWeight { get; set; } // TRUE if the amount measurement is a weight measurement and FALSE if the amount is a volume measurement. 
        public string UseFor { get; set; } // Short description of what the ingredient is used for in text
        public string Note { get; set; }
    }

    // The term "water" encompasses water profiles. 
    public class Water
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Amount { get; set; } // Volume of water to use in a recipe in liters.
        [Required]
        public float Calcium { get; set; } // The amount of calcium (Ca) in parts per million.
        [Required]
        public float Bicarbonate { get; set; } // The amount of bicarbonate (HCO3) in parts per million.
        [Required]
        public float Sulfate { get; set; } // The amount of Sulfate (SO4) in parts per million.
        [Required]
        public float Chloride { get; set; } // The amount of Chloride (Cl) in parts per million.
        [Required]
        public float Sodium { get; set; } // The amount of Sodium (Na) in parts per million.
        [Required]
        public float Magnesium { get; set; } // The amount of Magnesium (Mg) in parts per million.
        public float PH { get; set; } // The PH of the water.
        public string Notes { get; set; }
    }

    public class RecipieType
    {
        public static readonly string ALL_GRAIN = "All Grain";
        public static readonly string EXTRACT = "Extract";
        public static readonly string PARTIAL_MASH = "Partial Mash";
    }

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
        public virtual ICollection<Misc> Miscs { get; set; }
        [Required]
        public virtual ICollection<Water> Waters { get; set; }
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