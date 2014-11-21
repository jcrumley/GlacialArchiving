namespace GlacialArchiving.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("datastorage.UploadedItem", "StatusId", "dbo.UploadedItemStatus");
            DropIndex("datastorage.UploadedItem", new[] { "StatusId" });
            AlterColumn("datastorage.UploadedItem", "StatusId", c => c.Int());
            CreateIndex("datastorage.UploadedItem", "StatusId");
            AddForeignKey("datastorage.UploadedItem", "StatusId", "dbo.UploadedItemStatus", "UploadedItemStatusId");
        }
        
        public override void Down()
        {
            DropForeignKey("datastorage.UploadedItem", "StatusId", "dbo.UploadedItemStatus");
            DropIndex("datastorage.UploadedItem", new[] { "StatusId" });
            AlterColumn("datastorage.UploadedItem", "StatusId", c => c.Int(nullable: false));
            CreateIndex("datastorage.UploadedItem", "StatusId");
            AddForeignKey("datastorage.UploadedItem", "StatusId", "dbo.UploadedItemStatus", "UploadedItemStatusId", cascadeDelete: true);
        }
    }
}
