namespace Brew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fermentable",
                c => new
                    {
                        UID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                .PrimaryKey(t => t.UID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fermentable");
        }
    }
}
