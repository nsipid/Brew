namespace Brew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.YeastForm",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.YeastFlocculation",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.YeastType",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.StyleType",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.RecipieType",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.MashStepType",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.HopForm",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.HopType",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.HopUse",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.FermentableType",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.Hop",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                    Alpha = c.Single(nullable: false),
                    Amount = c.Single(nullable: false),
                    Time = c.Single(nullable: false),
                    Notes = c.String(),
                    Beta = c.Single(nullable: false),
                    HSI = c.Single(nullable: false),
                    Origin = c.String(maxLength: 75),
                    Substitutes = c.String(),
                    Humulene = c.Single(nullable: false),
                    Caryophyllene = c.Single(nullable: false),
                    Cohumulone = c.Single(nullable: false),
                    Myrcene = c.Single(nullable: false),
                    HopUses_Name = c.String(nullable: false, maxLength: 75),
                    HopType_Name = c.String(maxLength: 75),
                    HopForm_Name = c.String(maxLength: 75),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.HopUse", t => t.HopUses_Name, cascadeDelete: true)
                .ForeignKey("dbo.HopType", t => t.HopType_Name)
                .ForeignKey("dbo.HopForm", t => t.HopForm_Name)
                .Index(t => t.HopUses_Name)
                .Index(t => t.HopType_Name)
                .Index(t => t.HopForm_Name);

            CreateTable(
                "dbo.Recipe",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                    BatchSize = c.Single(nullable: false),
                    BoilSize = c.Single(nullable: false),
                    BoilTime = c.Single(nullable: false),
                    Efficiency = c.Single(nullable: false),
                    Notes = c.String(),
                    TasteNotes = c.String(),
                    TasteRating = c.Single(nullable: false),
                    OG = c.Single(nullable: false),
                    FG = c.Single(nullable: false),
                    FermentationStages = c.Int(nullable: false),
                    PrimayAge = c.Single(nullable: false),
                    PrimayTemp = c.Single(nullable: false),
                    SecondaryAge = c.Single(nullable: false),
                    SecondaryTemp = c.Single(nullable: false),
                    TertiaryAge = c.Single(nullable: false),
                    TertiaryTemp = c.Single(nullable: false),
                    Age = c.Single(nullable: false),
                    AgeTemp = c.Single(nullable: false),
                    Date = c.String(),
                    Carbonation = c.Single(nullable: false),
                    ForcedCarbonation = c.Boolean(nullable: false),
                    PrimingSugarName = c.String(maxLength: 75),
                    CarbonationTemp = c.Single(nullable: false),
                    PrimingSugarEquiv = c.Single(nullable: false),
                    KegPrimingFactor = c.Single(nullable: false),
                    SiteRating = c.Single(nullable: false),
                    RecipieType_Name = c.String(nullable: false, maxLength: 75),
                    Style_Name = c.String(maxLength: 75),
                    Mash_UID = c.Int(),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.RecipieType", t => t.RecipieType_Name, cascadeDelete: true)
                .ForeignKey("dbo.Style", t => t.Style_Name)
                .ForeignKey("dbo.MashProfile", t => t.Mash_UID)
                .Index(t => t.RecipieType_Name)
                .Index(t => t.Style_Name)
                .Index(t => t.Mash_UID);

            CreateTable(
                "dbo.Fermentable",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                    Amount = c.Single(nullable: false),
                    Yield = c.Single(nullable: false),
                    Color = c.Single(nullable: false),
                    AddAfterBoil = c.Boolean(nullable: false),
                    Origin = c.String(maxLength: 75),
                    Supplier = c.String(maxLength: 75),
                    Notes = c.String(),
                    CoarseFineDiff = c.Single(nullable: false),
                    Moisture = c.Single(nullable: false),
                    DiastaticPower = c.Single(nullable: false),
                    Protein = c.Single(nullable: false),
                    MaxInBatch = c.Single(nullable: false),
                    Recommended_Mash = c.Boolean(nullable: false),
                    IsMashed = c.Boolean(nullable: false),
                    IBUs = c.Single(nullable: false),
                    FermentableType_Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.FermentableType", t => t.FermentableType_Name, cascadeDelete: true)
                .Index(t => t.FermentableType_Name);

            CreateTable(
                "dbo.Yeast",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                    Amount = c.Single(nullable: false),
                    AmoutIsWeight = c.Boolean(),
                    Laboratory = c.String(maxLength: 75),
                    ProductID = c.Int(),
                    MinTemperature = c.Single(nullable: false),
                    MaxTemperature = c.Single(nullable: false),
                    Attenuation = c.Single(nullable: false),
                    Notes = c.String(),
                    BestFor = c.String(),
                    TimesCultured = c.Int(nullable: false),
                    MaxReuse = c.Int(nullable: false),
                    AddToSecondary = c.Boolean(nullable: false),
                    YeastType_Name = c.String(nullable: false, maxLength: 75),
                    YeastForm_Name = c.String(nullable: false, maxLength: 75),
                    Focculation_Name = c.String(maxLength: 75),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.YeastType", t => t.YeastType_Name, cascadeDelete: true)
                .ForeignKey("dbo.YeastForm", t => t.YeastForm_Name, cascadeDelete: true)
                .ForeignKey("dbo.YeastFlocculation", t => t.Focculation_Name)
                .Index(t => t.YeastType_Name)
                .Index(t => t.YeastForm_Name)
                .Index(t => t.Focculation_Name);

            CreateTable(
                "dbo.Style",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 75),
                    Category = c.String(nullable: false, maxLength: 75),
                    CategoryNumber = c.String(nullable: false),
                    StyleLetter = c.String(nullable: false, maxLength: 1),
                    StyleGuide = c.String(nullable: false),
                    OGMin = c.Single(nullable: false),
                    OGMax = c.Single(nullable: false),
                    FGMin = c.Single(nullable: false),
                    FGMax = c.Single(nullable: false),
                    IBUMin = c.Single(nullable: false),
                    IBUMax = c.Single(nullable: false),
                    ColorMin = c.Single(nullable: false),
                    ColorMax = c.Single(nullable: false),
                    CRABMin = c.Single(nullable: false),
                    CRABMax = c.Single(nullable: false),
                    ABVMin = c.Single(nullable: false),
                    ABVMax = c.Single(nullable: false),
                    Notes = c.String(),
                    Profile = c.String(),
                    Ingredients = c.String(),
                    Eamples = c.String(),
                    StyleType_Name = c.String(maxLength: 75),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.StyleType", t => t.StyleType_Name)
                .Index(t => t.StyleType_Name);

            CreateTable(
                "dbo.MashProfile",
                c => new
                {
                    UID = c.Int(nullable: false, identity: true),
                    Name = c.String(maxLength: 75),
                    GrainTemp = c.Single(nullable: false),
                    Notes = c.String(),
                    TunTemp = c.Single(nullable: false),
                    SpargeTemp = c.Single(nullable: false),
                    PH = c.Single(nullable: false),
                    TunWeight = c.Single(nullable: false),
                    TunSpecificHeat = c.Single(nullable: false),
                    EquipAdjust = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.UID);

            CreateTable(
                "dbo.MashStep",
                c => new
                {
                    UID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 75),
                    InfuseAmount = c.Single(nullable: false),
                    StepTemp = c.Single(nullable: false),
                    StepTime = c.Single(nullable: false),
                    RampTime = c.Single(nullable: false),
                    EndTemp = c.Single(nullable: false),
                    InfuseTemp = c.Single(nullable: false),
                    DecoctionAmount = c.Single(nullable: false),
                    SequenceNumber = c.Int(nullable: false),
                    MashProfile_UID = c.Int(),
                })
                .PrimaryKey(t => t.UID)
                .ForeignKey("dbo.MashStepType", t => t.Name, cascadeDelete: true)
                .ForeignKey("dbo.MashProfile", t => t.MashProfile_UID)
                .Index(t => t.Name)
                .Index(t => t.MashProfile_UID);

            CreateTable(
                "dbo.RecipeFermentables",
                c => new
                {
                    Recipe_Name = c.String(nullable: false, maxLength: 75),
                    Fermentable_Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => new { t.Recipe_Name, t.Fermentable_Name })
                .ForeignKey("dbo.Recipe", t => t.Recipe_Name, cascadeDelete: true)
                .ForeignKey("dbo.Fermentable", t => t.Fermentable_Name, cascadeDelete: true)
                .Index(t => t.Recipe_Name)
                .Index(t => t.Fermentable_Name);

            CreateTable(
                "dbo.RecipeYeasts",
                c => new
                {
                    Recipe_Name = c.String(nullable: false, maxLength: 75),
                    Yeast_Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => new { t.Recipe_Name, t.Yeast_Name })
                .ForeignKey("dbo.Recipe", t => t.Recipe_Name, cascadeDelete: true)
                .ForeignKey("dbo.Yeast", t => t.Yeast_Name, cascadeDelete: true)
                .Index(t => t.Recipe_Name)
                .Index(t => t.Yeast_Name);

            CreateTable(
                "dbo.RecipeHops",
                c => new
                {
                    Recipe_Name = c.String(nullable: false, maxLength: 75),
                    Hop_Name = c.String(nullable: false, maxLength: 75),
                })
                .PrimaryKey(t => new { t.Recipe_Name, t.Hop_Name })
                .ForeignKey("dbo.Recipe", t => t.Recipe_Name, cascadeDelete: true)
                .ForeignKey("dbo.Hop", t => t.Hop_Name, cascadeDelete: true)
                .Index(t => t.Recipe_Name)
                .Index(t => t.Hop_Name);

            AddColumn("dbo.UserProfile", "Recipe_Name", c => c.String(maxLength: 75));
            AddForeignKey("dbo.UserProfile", "Recipe_Name", "dbo.Recipe", "Name");
            CreateIndex("dbo.UserProfile", "Recipe_Name");
        }

        public override void Down()
        {
            DropIndex("dbo.RecipeHops", new[] { "Hop_Name" });
            DropIndex("dbo.RecipeHops", new[] { "Recipe_Name" });
            DropIndex("dbo.RecipeYeasts", new[] { "Yeast_Name" });
            DropIndex("dbo.RecipeYeasts", new[] { "Recipe_Name" });
            DropIndex("dbo.RecipeFermentables", new[] { "Fermentable_Name" });
            DropIndex("dbo.RecipeFermentables", new[] { "Recipe_Name" });
            DropIndex("dbo.MashStep", new[] { "MashProfile_UID" });
            DropIndex("dbo.MashStep", new[] { "Name" });
            DropIndex("dbo.Style", new[] { "StyleType_Name" });
            DropIndex("dbo.Yeast", new[] { "Focculation_Name" });
            DropIndex("dbo.Yeast", new[] { "YeastForm_Name" });
            DropIndex("dbo.Yeast", new[] { "YeastType_Name" });
            DropIndex("dbo.Fermentable", new[] { "FermentableType_Name" });
            DropIndex("dbo.UserProfile", new[] { "Recipe_Name" });
            DropIndex("dbo.Recipe", new[] { "Mash_UID" });
            DropIndex("dbo.Recipe", new[] { "Style_Name" });
            DropIndex("dbo.Recipe", new[] { "RecipieType_Name" });
            DropIndex("dbo.Hop", new[] { "HopForm_Name" });
            DropIndex("dbo.Hop", new[] { "HopType_Name" });
            DropIndex("dbo.Hop", new[] { "HopUses_Name" });
            DropForeignKey("dbo.RecipeHops", "Hop_Name", "dbo.Hop");
            DropForeignKey("dbo.RecipeHops", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.RecipeYeasts", "Yeast_Name", "dbo.Yeast");
            DropForeignKey("dbo.RecipeYeasts", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.RecipeFermentables", "Fermentable_Name", "dbo.Fermentable");
            DropForeignKey("dbo.RecipeFermentables", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.MashStep", "MashProfile_UID", "dbo.MashProfile");
            DropForeignKey("dbo.MashStep", "Name", "dbo.MashStepType");
            DropForeignKey("dbo.Style", "StyleType_Name", "dbo.StyleType");
            DropForeignKey("dbo.Yeast", "Focculation_Name", "dbo.YeastFlocculation");
            DropForeignKey("dbo.Yeast", "YeastForm_Name", "dbo.YeastForm");
            DropForeignKey("dbo.Yeast", "YeastType_Name", "dbo.YeastType");
            DropForeignKey("dbo.Fermentable", "FermentableType_Name", "dbo.FermentableType");
            DropForeignKey("dbo.UserProfile", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.Recipe", "Mash_UID", "dbo.MashProfile");
            DropForeignKey("dbo.Recipe", "Style_Name", "dbo.Style");
            DropForeignKey("dbo.Recipe", "RecipieType_Name", "dbo.RecipieType");
            DropForeignKey("dbo.Hop", "HopForm_Name", "dbo.HopForm");
            DropForeignKey("dbo.Hop", "HopType_Name", "dbo.HopType");
            DropForeignKey("dbo.Hop", "HopUses_Name", "dbo.HopUse");
            DropColumn("dbo.UserProfile", "Recipe_Name");
            DropTable("dbo.RecipeHops");
            DropTable("dbo.RecipeYeasts");
            DropTable("dbo.RecipeFermentables");
            DropTable("dbo.MashStep");
            DropTable("dbo.MashProfile");
            DropTable("dbo.Style");
            DropTable("dbo.Yeast");
            DropTable("dbo.Fermentable");
            DropTable("dbo.Recipe");
            DropTable("dbo.Hop");
            DropTable("dbo.FermentableType");
            DropTable("dbo.HopUse");
            DropTable("dbo.HopType");
            DropTable("dbo.HopForm");
            DropTable("dbo.MashStepType");
            DropTable("dbo.RecipieType");
            DropTable("dbo.StyleType");
            DropTable("dbo.YeastType");
            DropTable("dbo.YeastFlocculation");
            DropTable("dbo.YeastForm");
        }
    }
}
