using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{    
    [Table("FermentableType")]
    public class FermentableType
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("Fermentable")]
    public class Fermentable : IEquatable<Fermentable>
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
        //[Required]
        public FermentableType FermentableType { get; set; }
        [Required]
        public float Amount { get; set; } // Weight of the fermentable, extract or sugar in Kilograms.
        [Required, Range(0.0, 100)]
        public float Yield { get; set; } // Percent dry yield (fine grain) for the grain, or the raw yield by weight if this is an extract adjunct or sugar.
        [Required]
        public float Color { get; set; } // The color of the item in Lovibond Units (SRM for liquid extracts).
        public bool AddAfterBoil { get; set; } // May be TRUE if this item is normally added after the boil.
        [StringLength(75)]
        public string Origin { get; set; } // Country or place of origin
        [StringLength(75)]
        public string Supplier { get; set; } // Supplier of the grain/extract/sugar
        public string Notes { get; set; }
        [Range(0.0, 100)]
        public float CoarseFineDiff { get; set; } // Percent difference between the coarse grain yield and fine grain yield.  Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        [Range(0.0, 100)]
        public float Moisture { get; set; } // Percent moisture in the grain.  Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        public float DiastaticPower { get; set; } // The diastatic power of the grain as measured in "Lintner" units. Only appropriate for a "Grain" or "Adjunct" type, otherwise this value is ignored.
        [Range(0.0, 100)]
        public float Protein { get; set; } // The percent protein in the grain.  Only appropriate for a "Grain" or "Adjunct" 
        [Range(0.0, 100)]
        public float MaxInBatch { get; set; } // The recommended maximum percentage (by weight) this ingredient should represent in a batch of beer.
        public bool Recommended_Mash { get; set; } // TRUE if it is recommended the grain be mashed, FALSE if it can be steeped. 
        public bool IsMashed { get; set; }
        public float IBUs { get; set; } // For hopped extracts only - an estimate of the number of IBUs per pound of extract in a gallon of water.  

        [ForeignKey("FermentableType")]
        public string FermentableType_Name { get; set; } 

        public virtual ICollection<Recipe> UsingRecipes { get; set; }

        public Fermentable()
        {
            UsingRecipes = new HashSet<Recipe>();            
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Fermentable);
        }

        public bool Equals(Fermentable other)
        {
            return (other.Name == this.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}