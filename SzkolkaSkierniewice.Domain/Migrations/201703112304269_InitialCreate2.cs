namespace SzkolkaSkierniewice.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Promotion", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Promotion", "Description", c => c.String(nullable: false));
        }
    }
}
