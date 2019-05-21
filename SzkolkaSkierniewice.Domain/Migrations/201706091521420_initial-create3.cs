namespace SzkolkaSkierniewice.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GalleryImage",
                c => new
                    {
                        GalleryImageID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.GalleryImageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GalleryImage");
        }
    }
}
