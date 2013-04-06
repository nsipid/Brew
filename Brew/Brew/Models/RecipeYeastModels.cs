using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("RecipeYeasts")]
    public class RecipeYeast
    {
        [Key, Column(Order = 0), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("Yeast")]
        public string Yeast_Name { get; set; }

        [Key, Column(Order = 2)]
        public bool AddToSecondary { get; set; } // Flag denoting that this yeast was added for a secondary (or later) fermentation as opposed to the primary fermentation   

        public virtual Recipe Recipe { get; set; }
        public virtual Yeast Yeast { get; set; }
    }
}