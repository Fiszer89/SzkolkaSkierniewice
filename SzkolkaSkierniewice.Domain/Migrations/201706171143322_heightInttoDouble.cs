namespace SzkolkaSkierniewice.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class heightInttoDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "HeightMin", c => c.Double(nullable: false));
            AlterColumn("dbo.Product", "HeightMax", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "HeightMax", c => c.Int());
            AlterColumn("dbo.Product", "HeightMin", c => c.Int(nullable: false));
        }
    }
}
