using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("YeastType")]
    public class YeastType
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("YeastForm")]
    public class YeastForm
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("YeastFlocculation")]
    public class YeastFlocculation
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("Yeast")]
    public class Yeast : IEquatable<Yeast>
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
        //[Required]
        public YeastType YeastType { get; set; }
        //[Required]
        public YeastForm YeastForm { get; set; }
        [Required]
        public float Amount { get; set; } // The amount of yeast, measured in liters.  For a starter this is the size of the starter. If the flag AMOUNT_IS_WEIGHT is set to TRUE then this measurement is in kilograms and not liters.
        public bool? AmoutIsWeight { get; set; } // TRUE if the amount measurement is a weight measurement and FALSE if the amount is a volume measurement. 
        [StringLength(75)]
        public string Laboratory { get; set; } // The name of the laboratory that produced the yeast.
        public int? ProductID { get; set; } // The manufacturer’s product ID label or number that identifies this particular strain of yeast. 
        [Range(-50, 110)] 
        public float MinTemperature { get; set; } // The minimum recommended temperature for fermenting this yeast strain in degrees Celsius.
        [Range(-50, 110)] 
        public float MaxTemperature { get; set; } // The maximum recommended temperature for fermenting this yeast strain in Celsius.
        public YeastFlocculation YeastFlocculation { get; set; }
        public float Attenuation { get; set; } // Average attenuation for this yeast strain.
        public string Notes { get; set; }
        public string BestFor { get; set; } // Styles or types of beer this yeast strain is best suited for.
        public int TimesCultured { get; set; } // Number of times this yeast has been reused as a harvested culture.  
        public int MaxReuse { get; set; }  // Recommended of times this yeast can be reused (recultured from a previous batch)
        public bool AddToSecondary { get; set; }  // Flag denoting that this yeast was added for a secondary (or later) fermentation as opposed to the primary fermentation   

        public virtual ICollection<Recipe> UsingRecipes { get; set; }

        [ForeignKey("YeastType")]
        public string YeastType_Name { get; set; }

        [ForeignKey("YeastFlocculation")]
        public string YeastFlocculation_Name { get; set; }

        [ForeignKey("YeastForm")]
        public string YeastForm_Name { get; set; }

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