using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Brew.Models
{
    public class ModelsContext : DbContext
    {
        public ModelsContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<RecipeHop> RecipeHops { get; set; }
        public DbSet<RecipeYeast> RecipeYeasts { get; set; }
        public DbSet<RecipeFermentable> RecipeFermentables { get; set; }
        public DbSet<YeastForm> YeastForms { get; set; }
        public DbSet<YeastFlocculation> YeastFlocculations { get; set; }
        public DbSet<YeastType> YeastTypes { get; set; }
        public DbSet<StyleType> StyleTypes { get; set; }
        public DbSet<RecipieType> RecipieTypes { get; set; }
        public DbSet<MashStepType> MashStepTypes { get; set; }
        public DbSet<HopForm> HopForms { get; set; }
        public DbSet<HopType> HopTypes { get; set; }
        public DbSet<HopUse> HopUses { get; set; }
        public DbSet<FermentableType> FermentableTypes { get; set; }       
        public DbSet<Hop> Hops { get; set; }
        public DbSet<Yeast> Yeasts { get; set; }
        public DbSet<Fermentable> Fermentables { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<MashStep> MashSteps { get; set; }
        public DbSet<MashProfile> MashProfiles { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<FlavorProfile> FlavorProfiles { get; set; }
    }
}