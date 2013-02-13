using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Brew.Models
{
    public class Models
    {
        public class Brewer
        {
            [Key]
            public int UID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
        }

        public class RecipieType
        {
            public static readonly string ALL_GRAIN = "All Grain";
        }

        public class Recipe
        {
            [Key]
            public int UID { get; set; }
            public string Name { get; set; }
            public int Version { get; set; }
            public RecipieType Type { get; set; }
            public virtual ICollection<Brewer> Brewers { get; set; }
            public virtual ICollection<Fermentable> Fermentables { get; set; }
            public virtual ICollection<Yeast> Yeasts { get; set; }
            public BeerStyle Style { get; set; }
        }

        public class YeastType
        {
            public static readonly string ALE = "Ale";
        }

        public class YeastForm
        {
            public static readonly string LIQUID = "Liquid";
        }

        public class YeastFocculation
        {
            public static readonly string MEDIUM = "Medium";
        }

        public class Yeast
        {
            [Key]
            public int UID { get; set; }
            public YeastType Type { get; set; }
            public int Version { get; set; }
            public YeastForm Form { get; set; }
            public float Amount { get; set; }
            public string Laboratory { get; set; }
            public int ProductID { get; set; }
            public float MinTemperature { get; set; }
            public float MaxTemperature { get; set; }
            public float Attenuation { get; set; }
            public string Note { get; set; }
            public string BestFor { get; set; }
            public YeastFocculation Focculation { get; set; }
        }

        public class FermentableType
        {
            public static readonly string GRAIN = "Grain";
        }

        public class Fermentable
        {
            [Key]
            public int UID { get; set; }
            public string Name { get; set; }
            public FermentableType Type { get; set; }
            public float Yield { get; set; }
            public float Color { get; set; }
            public int Version { get; set; }
            public string Origin { get; set; }
            public string Supplier { get; set; }
            public string Note { get; set; }
            public float CoarseFineDiff { get; set; }
            public float Moisture { get; set; }
            public float DiastaticPower { get; set; }
            public float MaxInBatch { get; set; }
        }

        public class BeerStyleCategory
        {
            public static readonly string STOUT = "Stout";
        }

        public class BeerStyle
        {
            [Key]
            public int UID { get; set; }
            public string Name { get; set; }
            public BeerStyleCategory Category { get; set; }
        }

        public class MashProfiles
        {
            [Key]
            public int UID { get; set; }
        }
    }
}