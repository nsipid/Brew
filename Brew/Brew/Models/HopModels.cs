using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("HopForm")]
    public class HopForm
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("HopType")]
    public class HopType
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
    }

    [Table("HopUse")]
    public class HopUse
    {
        [Key, StringLength(75)]
        public string Name { get; set; }
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
        public HopType HopType { get; set; }
        public HopForm HopForm { get; set; }
        [Range(0.0, 100)]
        public float Beta { get; set; } // Hop beta percentage - for example "4.4" denotes 4.4 % beta
        [Range(0.0, 100)]
        public float HSI { get; set; } // Hop Stability Index - defined as the percentage of hop alpha lost in 6 months of storage
        [StringLength(75)]
        public string Origin { get; set; } // Place of origin for the hops
        [Range(0.0, 100)]
        public float Humulene { get; set; } // Humulene level in percent
        [Range(0.0, 100)]
        public float Caryophyllene { get; set; } // Caryophyllene level in percent
        [Range(0.0, 100)]
        public float Cohumulone { get; set; } // Cohumulone level in percent
        [Range(0.0, 100)]
        public float Myrcene { get; set; } // Myrcene level in percent
              
        [ForeignKey("HopForm")]
        public string HopForm_Name { get; set; }

        [ForeignKey("HopType")]
        public string HopType_Name { get; set; }

        public virtual ICollection<RecipeHop> RecipeHops { get; set; }

        public virtual ICollection<HopSubstitute> Substitutes { get; set; }

        public Hop()
        {
            RecipeHops = new HashSet<RecipeHop>();
            Substitutes = new HashSet<HopSubstitute>();      
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