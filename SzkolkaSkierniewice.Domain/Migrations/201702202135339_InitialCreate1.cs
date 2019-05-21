namespace SzkolkaSkierniewice.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotion", "EndDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Product", "Block");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Block", c => c.String());
            DropColumn("dbo.Promotion", "EndDate");
        }
    }
}
