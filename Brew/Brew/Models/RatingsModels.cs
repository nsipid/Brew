using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{   
    [Table("Rating")]
    public class Rating
    {
        [Key, Column(Order = 0), ForeignKey("Recipe")]
        public string Recipe_Name { get; set; }
        [Key, Column(Order = 1), ForeignKey("UserProfile")]
        public int UserProfile_UserID { get; set; }

        public UserProfile UserProfile { get; set; }
        public Recipe Recipe { get; set; }

        [Required, Range(0, 10)]
        public int Rating_Score { get; set; }    
    }
}