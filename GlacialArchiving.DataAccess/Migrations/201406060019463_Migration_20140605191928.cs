namespace GlacialArchiving.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_20140605191928 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.fred",
                c => new
                    {
                        fredId = c.Int(nullable: false, identity: true),
                        foo = c.Int(nullable: false),
                        bar = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.fredId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "dbo.UploadedItemValidation",
                c => new
                    {
                        UploadedItemValidationId = c.Int(nullable: false, identity: true),
                        UniqueIdentifier = c.String(nullable: false, maxLength: 100),
                        Succeeded = c.Boolean(nullable: false),
                        InputData = c.String(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UploadedItemValidationId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "dbo.ValidationError",
                c => new
                    {
                        ValidationErrorId = c.Int(nullable: false, identity: true),
                        UploadedItemValidationId = c.Int(nullable: false),
                        Error = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ValidationErrorId)
                .ForeignKey("dbo.UploadedItemValidation", t => t.UploadedItemValidationId, cascadeDelete: true)
                .Index(t => t.UploadedItemValidationId);
            
            AddColumn("datastorage.UploadedItem", "GlacierRegion1", c => c.String(maxLength: 400));
            AddColumn("datastorage.UploadedItem", "GlacierRegion2", c => c.String(maxLength: 400));
            AddColumn("datastorage.UploadedItem", "GlacierVault1", c => c.String(maxLength: 400));
            AddColumn("datastorage.UploadedItem", "GlacierVault2", c => c.String(maxLength: 400));
            AddColumn("datastorage.UploadedItem", "GlacierStorageId1", c => c.String(maxLength: 400));
            AddColumn("datastorage.UploadedItem", "GlacierStorageId2", c => c.String(maxLength: 400));
            AlterColumn("datastorage.UploadedItem", "ExpirationDate", c => c.DateTime());
            DropColumn("datastorage.UploadedItem", "GlacierZone1ID");
            DropColumn("datastorage.UploadedItem", "GlacierZone2ID");
        }
        
        public override void Down()
        {
            AddColumn("datastorage.UploadedItem", "GlacierZone2ID", c => c.String(maxLength: 400));
            AddColumn("datastorage.UploadedItem", "GlacierZone1ID", c => c.String(maxLength: 400));
            DropForeignKey("dbo.ValidationError", "UploadedItemValidationId", "dbo.UploadedItemValidation");
            DropForeignKey("dbo.UploadedItemValidation", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.UploadedItemValidation", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("dbo.fred", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.fred", "CreatedBy_UserId", "dbo.User");
            DropIndex("dbo.ValidationError", new[] { "UploadedItemValidationId" });
            DropIndex("dbo.UploadedItemValidation", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.UploadedItemValidation", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.fred", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.fred", new[] { "CreatedBy_UserId" });
            AlterColumn("datastorage.UploadedItem", "ExpirationDate", c => c.DateTime(nullable: false));
            DropColumn("datastorage.UploadedItem", "GlacierStorageId2");
            DropColumn("datastorage.UploadedItem", "GlacierStorageId1");
            DropColumn("datastorage.UploadedItem", "GlacierVault2");
            DropColumn("datastorage.UploadedItem", "GlacierVault1");
            DropColumn("datastorage.UploadedItem", "GlacierRegion2");
            DropColumn("datastorage.UploadedItem", "GlacierRegion1");
            DropTable("dbo.ValidationError");
            DropTable("dbo.UploadedItemValidation");
            DropTable("dbo.fred");
        }
    }
}
