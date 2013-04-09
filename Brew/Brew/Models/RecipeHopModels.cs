using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("RecipeHops")]
    public class RecipeHop : IEquatable<RecipeHop>
    {
        [Key, Column(Order = 0), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("Hop")]
        public string Hop_Name { get; set; }
        
        public string HopUses_Name { get; set; }

        public HopUse HopUses { get; set; }
        public virtual Hop Hop { get; set; }
        public virtual Recipe Recipe { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as RecipeHop);
        }

        public bool Equals(RecipeHop other)
        {
            return (other.Recipe_Name == this.Recipe_Name) &&
                (other.Hop_Name == this.Hop_Name) &&
                (other.HopUses_Name == this.HopUses_Name);
        }

        public override int GetHashCode()
        {
            return this.Recipe_Name.GetHashCode() +
                this.Hop_Name.GetHashCode() +
               HopUses_Name.GetHashCode();
        }
    }
}