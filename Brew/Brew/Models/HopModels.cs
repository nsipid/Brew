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

    // [StringLength(250)]
    // [Key]
    // [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    // public int UID { get; set; }

    [Table("Hop")]
    public class Hop : IEquatable<Hop>
    {        
        [Key]        
        public string Name { get; set; }
        [Required]
        public float Alpha { get; set; } // Percent alpha of hops - for example "5.5" represents 5.5% alpha
        [Required]
        public float Amount { get; set; } // Weight in Kilograms of the hops used in the recipe.
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