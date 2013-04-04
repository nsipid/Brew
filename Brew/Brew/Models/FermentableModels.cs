using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class FermentableUtils
    {
        public static FermentableType getFermentableType(string dbValue)
        {
            switch (dbValue)
            {
                case "Grain":
                    return Models.FermentableType.Grain;
                case "Sugar":
                    return Models.FermentableType.Sugar;
                case "Extract":
                    return Models.FermentableType.Extract;
                case "Dry Extract":
                    return Models.FermentableType.DryExtract;
                case "Adjunct":
                    return Models.FermentableType.Adjunct;
                default:
                    throw new Exception(dbValue + " is not a FermentableType");
            }
        }
    }

    public enum FermentableType
    {
        Grain,
        Sugar,
        Extract,
        DryExtract,
        Adjunct
    }

    [Table("Fermentable")]
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
        public string Notes { get; set; }
        public float? CoarseFineDiff { get; set; } // Percent difference between the coarse grain yield and fine grain yield.  Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float? Moisture { get; set; } // Percent moisture in the grain.  Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float? DiastaticPower { get; set; } // The diastatic power of the grain as measured in "Lintner" units. Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float? Protein { get; set; } // The percent protein in the grain.  Only appropriate for a "Grain" or "Adjunct" 
        public float? MaxInBatch { get; set; } // The recommended maximum percentage (by weight) this ingredient should represent in a batch of beer.
        public bool? Recommended_Mash { get; set; } // TRUE if it is recommended the grain be mashed, FALSE if it can be steeped. 
        public bool? IsMashed { get; set; }
        public float? IBUs { get; set; } // For hopped extracts only - an estimate of the number of IBUs per pound of extract in a gallon of water.  
    }
}