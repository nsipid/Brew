using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("RecipeFermentables")]
    public class RecipeFermentable : IEquatable<RecipeFermentable>
    {
        [Key, Column(Order = 0), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("Fermentable")]
        public string Fermentable_Name { get; set; }

        [Required]
        public float Amount { get; set; } // Weight of the fermentable, extract or sugar in Kilograms.       
        public bool IsMashed { get; set; }
        public bool AddAfterBoil { get; set; } // May be TRUE if this item is normally added after the boil.

        public virtual Fermentable Fermentable { get; set; }
        public virtual Recipe Recipe { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as RecipeFermentable);
        }

        public bool Equals(RecipeFermentable other)
        {
            return (other.Recipe_Name == this.Recipe_Name) &&
                (other.Fermentable_Name == this.Fermentable_Name) &&
                (other.IsMashed == this.IsMashed) &&
                (other.AddAfterBoil == this.AddAfterBoil);
        }

        public override int GetHashCode()
        {
            return this.Recipe_Name.GetHashCode() +
                this.Fermentable_Name.GetHashCode() +
               IsMashed.GetHashCode() + 
               AddAfterBoil.GetHashCode();
        }
    }
}