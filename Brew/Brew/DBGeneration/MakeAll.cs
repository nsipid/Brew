namespace Brew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class MakeAll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hop",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128),
                    Alpha = c.Single(nullable: false),
                    Amount = c.Single(nullable: false),
                    HopUses = c.Int(nullable: false),
                    Time = c.Single(nullable: false),
                    Notes = c.String(),
                    HopType = c.Int(nullable: false),
                    HopForm = c.Int(nullable: false),
                    Beta = c.Single(),
                    HSI = c.Single(),
                    Origin = c.String(),
                    Substitutes = c.String(),
                    Humulene = c.Single(),
                    Caryophyllene = c.Single(),
                    Cohumulone = c.Single(),
                    Myrcene = c.Single(),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.Recipe",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128),
                    RecipieType = c.Int(nullable: false),
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
                    PrimayAge = c.Int(nullable: false),
                    PrimayTemp = c.Single(nullable: false),
                    SecondaryAge = c.Int(nullable: false),
                    SecondaryTemp = c.Single(nullable: false),
                    TertiaryAge = c.Int(nullable: false),
                    TertiaryTemp = c.Single(nullable: false),
                    Age = c.Int(nullable: false),
                    AgeTemp = c.Single(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Carbonation = c.Single(nullable: false),
                    ForcedCarbonation = c.Boolean(nullable: false),
                    PrimingSugarName = c.String(),
                    CarbonationTemp = c.Single(nullable: false),
                    PrimingSugarEquiv = c.Single(nullable: false),
                    KegPrimingFactor = c.Single(nullable: false),
                    Style_Name = c.String(maxLength: 128),
                    Mash_UID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Style", t => t.Style_Name)
                .ForeignKey("dbo.MashProfile", t => t.Mash_UID, cascadeDelete: true)
                .Index(t => t.Style_Name)
                .Index(t => t.Mash_UID);

            CreateTable(
                "dbo.Fermentable",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128),
                    FermentableType = c.Int(nullable: false),
                    Amount = c.Single(nullable: false),
                    Yield = c.Single(nullable: false),
                    Color = c.Single(nullable: false),
                    AddAfterBoil = c.Boolean(),
                    Origin = c.String(),
                    Supplier = c.String(),
                    Notes = c.String(),
                    CoarseFineDiff = c.Single(),
                    Moisture = c.Single(),
                    DiastaticPower = c.Single(),
                    Protein = c.Single(),
                    MaxInBatch = c.Single(),
                    Recommended_Mash = c.Boolean(),
                    IsMashed = c.Boolean(),
                    IBUs = c.Single(),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.Yeast",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128),
                    YeastType = c.Int(nullable: false),
                    YeastForm = c.Int(nullable: false),
                    Amount = c.Single(nullable: false),
                    AmoutIsWeight = c.Boolean(),
                    Laboratory = c.String(),
                    ProductID = c.Int(),
                    MinTemperature = c.Single(),
                    MaxTemperature = c.Single(),
                    Focculation = c.Int(nullable: false),
                    Attenuation = c.Single(),
                    Notes = c.String(),
                    BestFor = c.String(),
                    TimesCultured = c.Int(),
                    MaxReuse = c.Int(),
                    AddToSecondary = c.Boolean(),
                    Recipe_Name = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Recipe", t => t.Recipe_Name)
                .Index(t => t.Recipe_Name);

            CreateTable(
                "dbo.Style",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 128),
                    Category = c.String(nullable: false),
                    CategoryNumber = c.String(nullable: false),
                    StyleLetter = c.String(nullable: false),
                    StyleGuide = c.String(nullable: false),
                    StyleType = c.Int(nullable: false),
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
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.MashProfile",
                c => new
                {
                    UID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
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
                    Name = c.String(),
                    MashStepType = c.Int(nullable: false),
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
                .ForeignKey("dbo.MashProfile", t => t.MashProfile_UID)
                .Index(t => t.MashProfile_UID);

            CreateTable(
                "dbo.RecipeFermentables",
                c => new
                {
                    Recipe_Name = c.String(nullable: false, maxLength: 128),
                    Fermentable_Name = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.Recipe_Name, t.Fermentable_Name })
                .ForeignKey("dbo.Recipe", t => t.Recipe_Name, cascadeDelete: true)
                .ForeignKey("dbo.Fermentable", t => t.Fermentable_Name, cascadeDelete: true)
                .Index(t => t.Recipe_Name)
                .Index(t => t.Fermentable_Name);

            CreateTable(
                "dbo.HopRecipes",
                c => new
                {
                    Hop_Name = c.String(nullable: false, maxLength: 128),
                    Recipe_Name = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.Hop_Name, t.Recipe_Name })
                .ForeignKey("dbo.Hop", t => t.Hop_Name, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.Recipe_Name, cascadeDelete: true)
                .Index(t => t.Hop_Name)
                .Index(t => t.Recipe_Name);

            AddColumn("dbo.UserProfile", "Recipe_Name", c => c.String(maxLength: 128));
            AddForeignKey("dbo.UserProfile", "Recipe_Name", "dbo.Recipe", "Name");
            CreateIndex("dbo.UserProfile", "Recipe_Name");
        }

        public override void Down()
        {
            DropIndex("dbo.HopRecipes", new[] { "Recipe_Name" });
            DropIndex("dbo.HopRecipes", new[] { "Hop_Name" });
            DropIndex("dbo.RecipeFermentables", new[] { "Fermentable_Name" });
            DropIndex("dbo.RecipeFermentables", new[] { "Recipe_Name" });
            DropIndex("dbo.MashStep", new[] { "MashProfile_UID" });
            DropIndex("dbo.Yeast", new[] { "Recipe_Name" });
            DropIndex("dbo.Recipe", new[] { "Mash_UID" });
            DropIndex("dbo.Recipe", new[] { "Style_Name" });
            DropIndex("dbo.UserProfile", new[] { "Recipe_Name" });
            DropForeignKey("dbo.HopRecipes", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.HopRecipes", "Hop_Name", "dbo.Hop");
            DropForeignKey("dbo.RecipeFermentables", "Fermentable_Name", "dbo.Fermentable");
            DropForeignKey("dbo.RecipeFermentables", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.MashStep", "MashProfile_UID", "dbo.MashProfile");
            DropForeignKey("dbo.Yeast", "Recipe_Name", "dbo.Recipe");
            DropForeignKey("dbo.Recipe", "Mash_UID", "dbo.MashProfile");
            DropForeignKey("dbo.Recipe", "Style_Name", "dbo.Style");
            DropForeignKey("dbo.UserProfile", "Recipe_Name", "dbo.Recipe");
            DropColumn("dbo.UserProfile", "Recipe_Name");
            DropTable("dbo.HopRecipes");
            DropTable("dbo.RecipeFermentables");
            DropTable("dbo.MashStep");
            DropTable("dbo.MashProfile");
            DropTable("dbo.Style");
            DropTable("dbo.Yeast");
            DropTable("dbo.Fermentable");
            DropTable("dbo.Recipe");
            DropTable("dbo.Hop");
        }
    }
}
