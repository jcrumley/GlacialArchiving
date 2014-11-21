namespace GlacialArchiving.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_20140807200640 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.GlacierStorage", newSchema: "datastorage");
            MoveTable(name: "dbo.UploadedItemStatus", newSchema: "datastorage");
            DropForeignKey("datastorage.UploadedItem", "StatusId", "dbo.UploadedItemStatus");
            DropIndex("datastorage.UploadedItem", new[] { "StatusId" });
            AddColumn("datastorage.GlacierStorage", "CreatedOn", c => c.DateTime());
            AddColumn("datastorage.GlacierStorage", "CreatedBy_UserId", c => c.Int());
            AddColumn("datastorage.GlacierStorage", "ModifiedOn", c => c.DateTime());
            AddColumn("datastorage.GlacierStorage", "ModifiedBy_UserId", c => c.Int());
            AlterColumn("datastorage.UploadedItem", "StatusId", c => c.Int(nullable: false));
            CreateIndex("datastorage.GlacierStorage", "CreatedBy_UserId");
            CreateIndex("datastorage.GlacierStorage", "ModifiedBy_UserId");
            CreateIndex("datastorage.UploadedItem", "StatusId");
            AddForeignKey("datastorage.GlacierStorage", "CreatedBy_UserId", "dbo.User", "UserId");
            AddForeignKey("datastorage.GlacierStorage", "ModifiedBy_UserId", "dbo.User", "UserId");
            AddForeignKey("datastorage.UploadedItem", "StatusId", "datastorage.UploadedItemStatus", "UploadedItemStatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("datastorage.UploadedItem", "StatusId", "datastorage.UploadedItemStatus");
            DropForeignKey("datastorage.GlacierStorage", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.GlacierStorage", "CreatedBy_UserId", "dbo.User");
            DropIndex("datastorage.UploadedItem", new[] { "StatusId" });
            DropIndex("datastorage.GlacierStorage", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.GlacierStorage", new[] { "CreatedBy_UserId" });
            AlterColumn("datastorage.UploadedItem", "StatusId", c => c.Int());
            DropColumn("datastorage.GlacierStorage", "ModifiedBy_UserId");
            DropColumn("datastorage.GlacierStorage", "ModifiedOn");
            DropColumn("datastorage.GlacierStorage", "CreatedBy_UserId");
            DropColumn("datastorage.GlacierStorage", "CreatedOn");
            CreateIndex("datastorage.UploadedItem", "StatusId");
            AddForeignKey("datastorage.UploadedItem", "StatusId", "dbo.UploadedItemStatus", "UploadedItemStatusId");
            MoveTable(name: "datastorage.UploadedItemStatus", newSchema: "dbo");
            MoveTable(name: "datastorage.GlacierStorage", newSchema: "dbo");
        }
    }
}
