using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public enum HopForm
    {
        Pellet,
        Plug,
        Leaf
    }

    public enum HopType
    {
        Bittering,
        Aroma,
        Both
    }

    public enum HopUse
    {
        Boil,
        DryHop,
        Mash,
        FirstWort,
        Aroma
    }

    [Table("Hop")]
    public class Hop
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Alpha { get; set; } // Percent alpha of hops - for example "5.5" represents 5.5% alpha
        [Required]
        public float Weight { get; set; } // Weight in Kilograms of the hops used in the recipe.
        [Required]
        public HopUse HopUses { get; set; }
        [Required]
        public float Time { get; set; } // Meaning is dependent on the “USE” 
        public string Notes { get; set; }
        public HopType HopType { get; set; }
        public HopForm HopForm { get; set; }
        public float? Beta { get; set; } // Hop beta percentage - for example "4.4" denotes 4.4 % beta
        public float? HSI { get; set; } // Hop Stability Index - defined as the percentage of hop alpha lost in 6 months of storage
        public string Origin { get; set; } // Place of origin for the hops
        public string Substitutes { get; set; } // Substitutes that can be used for this hops
        public float? Humulene { get; set; } // Humulene level in percent
        public float? Caryophyllene { get; set; } // Caryophyllene level in percent
        public float? Cohumulone { get; set; } // Cohumulone level in percent
        public float? Myrcene { get; set; } // Myrcene level in percent
    }

    //[ForeignKey("StatusId")]
    public class YeastType
    {
        public static readonly string ALE = "Ale";
        public static readonly string LAGER = "Lager";
        public static readonly string WHEAT = "Wheat";
        public static readonly string WINE = "Wine";
        public static readonly string CHAMPAGNE = "Champagne";
    }

    public class YeastForm
    {
        public static readonly string LIQUID = "Liquid";
        public static readonly string DRY = "Dry";
        public static readonly string SLANT = "Slant";
        public static readonly string CULTURE = "Culture";
    }

    public class YeastFlocculation
    {
        public static readonly string LOW = "Low";
        public static readonly string MEDIUM = "Medium";
        public static readonly string HIGH = "High";
        public static readonly string VERY_HIGH = "Very High";
    }

    public class Yeast
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public YeastType YeastType { get; set; }
        [Required]
        public YeastForm YeastForm { get; set; }
        [Required]
        public float Amount { get; set; } // The amount of yeast, measured in liters.  For a starter this is the size of the starter. If the flag AMOUNT_IS_WEIGHT is set to TRUE then this measurement is in kilograms and not liters.
        public bool? AmoutIsWeight { get; set; } // TRUE if the amount measurement is a weight measurement and FALSE if the amount is a volume measurement. 
        public string Laboratory { get; set; } // The name of the laboratory that produced the yeast.
        public int? ProductID { get; set; } // The manufacturer’s product ID label or number that identifies this particular strain of yeast. 
        public float? MinTemperature { get; set; } // The minimum recommended temperature for fermenting this yeast strain in degrees Celsius.
        public float? MaxTemperature { get; set; } // The maximum recommended temperature for fermenting this yeast strain in Celsius.
        public YeastFlocculation Focculation { get; set; }
        public float? Attenuation { get; set; } // Average attenuation for this yeast strain.
        public string Note { get; set; }
        public string BestFor { get; set; } // Styles or types of beer this yeast strain is best suited for.
        public int? TimesCultured { get; set; } // Number of times this yeast has been reused as a harvested culture.  
        public int? MaxReuse { get; set; }  // Recommended of times this yeast can be reused (recultured from a previous batch)
        public bool? AddToSecondary { get; set; }  // Flag denoting that this yeast was added for a secondary (or later) fermentation as opposed to the primary fermentation   
    }

    public class FermentableType
    {
        public static readonly string GRAIN = "Grain";
        public static readonly string SUGAR = "Sugar";
        public static readonly string EXTRACT = "Extract";
        public static readonly string DRY_EXTRACT = "Dry Extract";
        public static readonly string ADJUNCT = "Adjunct";
    }

    public class Fermentable
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public FermentableType FermentableType { get; set; }
        [Required]
        public float Amount { get; set; } // Weight of the fermentable, extract or sugar in Kilograms.
        [Required]
        public float Yield { get; set; } // Percent dry yield (fine grain) for the grain, or the raw yield by weight if this is an extract adjunct or sugar.
        [Required]
        public float Color { get; set; } // The color of the item in Lovibond Units (SRM for liquid extracts).
        public bool? AddAfterBoil { get; set; } // May be TRUE if this item is normally added after the boil.
        public string Origin { get; set; } // Country or place of origin
        public string Supplier { get; set; } // Supplier of the grain/extract/sugar
        public string Note { get; set; }
        public float? CoarseFineDiff { get; set; } // Percent difference between the coarse grain yield and fine grain yield.  Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float? Moisture { get; set; } // Percent moisture in the grain.  Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float? DiastaticPower { get; set; } // The diastatic power of the grain as measured in "Lintner" units. Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float? Protein { get; set; } // The percent protein in the grain.  Only appropriate for a "Grain" or "Adjunct" 
        public float? MaxInBatch { get; set; } // The recommended maximum percentage (by weight) this ingredient should represent in a batch of beer.
        public bool? Recommended_Mash { get; set; } // TRUE if it is recommended the grain be mashed, FALSE if it can be steeped. 
        public float? IBUs { get; set; } // For hopped extracts only - an estimate of the number of IBUs per pound of extract in a gallon of water.  
    }

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