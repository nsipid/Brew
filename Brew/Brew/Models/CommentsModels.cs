using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    [Table("FlavorProfile")]
    public class FlavorProfile
    {
        [Key, StringLength(50)]
        public string Name { get; set; }
    }

    [Table("Comment")]
    public class Comment
    {
        [Key, Column(Order = 0)]//, ForeignKey["UserProfile"]
        public UserProfile UserProfile { get; set; }
        [Key, Column(Order = 1)]//, StringLength(75), ForeignKey("FlavorProfile")]
        public Recipe Recipe { get; set; }
        [Key, Column(Order = 2)]
        public System.DateTime Timestamp { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]//, StringLength(50), ForeignKey("FlavorProfile")]
        public FlavorProfile FlavorProfile { get; set; }
        [StringLength(250)]
        public string Text { get; set; }
    }
}