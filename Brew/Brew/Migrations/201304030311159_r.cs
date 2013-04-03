namespace Brew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class r : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hop", "Time", c => c.Single(nullable: false));            
        }
        
        public override void Down()
        {
           
        }
    }
}
