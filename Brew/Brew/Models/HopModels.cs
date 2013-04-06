using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class HopUtils
    {
        public static HopType getHopType(string dbValue)
        {
            switch (dbValue)
            {
                case "Both":
                    return Models.HopType.Both;
                case "Aroma":
                    return Models.HopType.Aroma;
                case "Bittering":
                    return Models.HopType.Bittering;

                default:
                    throw new Exception(dbValue + " is not a HopType");
            }
        }

        public static HopForm getHopForm(string dbValue)
        {
            switch (dbValue)
            {
                case "Plug":
                    return Models.HopForm.Plug;
                case "Pellet":
                    return Models.HopForm.Pellet;
                case "Leaf":
                    return Models.HopForm.Leaf;
                default:
                    throw new Exception(dbValue + " is not a HopForm");
            }
        }

        public static HopUse getHopUse(string dbValue)
        {           
            switch (dbValue)
            {
                case "Boil":
                    return Models.HopUse.Boil;
                case "Aroma":
                    return Models.HopUse.Aroma;
                case "DryHop":
                    return Models.HopUse.DryHop;
                case "First Wort":
                    return Models.HopUse.FirstWort;
                case "Mash":
                    return Models.HopUse.Mash;
                default:
                    throw new Exception(dbValue + " is not a HopUse");
            }
        }
    }

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

    // System.ComponentModel.DataAnnotations.MaxLengthAttribute
    //[StringLength(250)]
   
    [Table("Hop")]
    public class Hop : IEquatable<Hop>
    {
        [Key, StringLength(75)]        
        public string Name { get; set; }
        [Required, Range(0.0, 100)]
        public float Alpha { get; set; } // Percent alpha of hops - for example "5.5" represents 5.5% alpha
        [Required]
        public float Amount { get; set; } // Weight in Kilograms of the hops used in the recipe.
        [Required, EnumDataType(typeof(HopUse))]
        public HopUse HopUses { get; set; }
        [Required]
        public float Time { get; set; } // Meaning is dependent on the “USE” 
        public string Notes { get; set; }
        [EnumDataType(typeof(HopType))]
        public HopType HopType { get; set; }
        [EnumDataType(typeof(HopForm))]
        public HopForm HopForm { get; set; }
        [Range(0.0, 100)]
        public float Beta { get; set; } // Hop beta percentage - for example "4.4" denotes 4.4 % beta
        [Range(0.0, 100)]
        public float HSI { get; set; } // Hop Stability Index - defined as the percentage of hop alpha lost in 6 months of storage
        [StringLength(75)]
        public string Origin { get; set; } // Place of origin for the hops
        public string Substitutes { get; set; } // Substitutes that can be used for this hops
        [Range(0.0, 100)]
        public float Humulene { get; set; } // Humulene level in percent
        [Range(0.0, 100)]
        public float Caryophyllene { get; set; } // Caryophyllene level in percent
        [Range(0.0, 100)]
        public float Cohumulone { get; set; } // Cohumulone level in percent
        [Range(0.0, 100)]
        public float Myrcene { get; set; } // Myrcene level in percent

        public virtual ICollection<Recipe> UsingRecipes { get; set; }

        public Hop()
        {
            UsingRecipes = new HashSet<Recipe>();            
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Hop);
        }

        public bool Equals(Hop other)
        {
            return (other.Name == this.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}