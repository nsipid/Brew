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
    public class RecipeFermentable
    {
        [Key, Column(Order = 0), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("Fermentable")]
        public string Fermentable_Name { get; set; }

        [Key, Column(Order = 2)]
        public bool IsMashed { get; set; }
        [Key, Column(Order = 3)]
        public bool AddAfterBoil { get; set; } // May be TRUE if this item is normally added after the boil.

        public virtual Fermentable Fermentable { get; set; }
        public virtual Recipe Recipe { get; set; }      
    }
}