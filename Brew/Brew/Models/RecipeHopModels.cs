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
    public class RecipeHop
    {
        [Key, Column(Order = 0), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("Hop")]
        public string Hop_Name { get; set; }
        
        [Key, Column(Order = 2), ForeignKey("HopUses")]
        public string HopUses_Name { get; set; }

        public HopUse HopUses { get; set; }
        public virtual Hop Hop { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}