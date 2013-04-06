using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class YeastUtils
    {
        public static YeastType getYeastType(string dbValue)
        {
            switch (dbValue)
            {
                case "Ale":
                    return Models.YeastType.Ale;
                case "Lager":
                    return Models.YeastType.Lager;
                case "Wheat":
                    return Models.YeastType.Wheat;
                case "Wine":
                    return Models.YeastType.Wine;
                case "Champagne":
                    return Models.YeastType.Champagne;
                default:
                    throw new Exception(dbValue + " is not a YeastType");
            }
        }

        public static YeastForm getYeastForm(string dbValue)
        {
            switch (dbValue)
            {
                case "Liquid":
                    return Models.YeastForm.Liquid;
                case "Dry":
                    return Models.YeastForm.Dry;
                case "Slant":
                    return Models.YeastForm.Slant;
                case "Culture":
                    return Models.YeastForm.Culture;              
                default:
                    throw new Exception(dbValue + " is not a YeastForm");
            }
        }

        public static YeastFlocculation getYeastFlocculation(string dbValue)
        {
            switch (dbValue)
            {
                case "Low":
                    return Models.YeastFlocculation.Low;
                case "Medium":
                    return Models.YeastFlocculation.Medium;
                case "High":
                    return Models.YeastFlocculation.High;
                case "Very High":
                    return Models.YeastFlocculation.VeryHigh;
                default:
                    throw new Exception(dbValue + " is not a YeastFlocculation");
            }
        }
    }

    public enum YeastType
    {
        Ale,
        Lager,
        Wheat,
        Wine,
        Champagne
    }

    public enum YeastForm
    {
        Liquid,
        Dry,
        Slant,
        Culture
    }

    public enum YeastFlocculation
    {
        Low,
        Medium,
        High,
        VeryHigh
    }

    [Table("Yeast")]
    public class Yeast : IEquatable<Yeast>
    {
        [Key]
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
        public string Notes { get; set; }
        public string BestFor { get; set; } // Styles or types of beer this yeast strain is best suited for.
        public int? TimesCultured { get; set; } // Number of times this yeast has been reused as a harvested culture.  
        public int? MaxReuse { get; set; }  // Recommended of times this yeast can be reused (recultured from a previous batch)
        public bool? AddToSecondary { get; set; }  // Flag denoting that this yeast was added for a secondary (or later) fermentation as opposed to the primary fermentation   

        public virtual ICollection<Recipe> UsingRecipes { get; set; }

        public Yeast()
        {
            UsingRecipes = new HashSet<Recipe>();            
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Yeast);
        }

        public bool Equals(Yeast other)
        {
            return (other.Name == this.Name);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}