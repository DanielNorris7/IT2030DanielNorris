namespace MVCMusicStoreApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shoppingCart1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        AlbumID = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Albums", t => t.AlbumID, cascadeDelete: true)
                .Index(t => t.AlbumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "AlbumID", "dbo.Albums");
            DropIndex("dbo.Carts", new[] { "AlbumID" });
            DropTable("dbo.Carts");
        }
    }
}
