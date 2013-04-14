using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("HopSubstitute")]
    public class HopSubstitute
    {
        [Key, Column(Order = 0), ForeignKey("Hop")]
        public string Hop_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        

        public virtual Hop Hop { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}