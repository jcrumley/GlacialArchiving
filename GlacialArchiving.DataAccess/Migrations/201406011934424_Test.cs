namespace GlacialArchiving.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ClientIdTag = c.String(nullable: false, maxLength: 20),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        NameFirst = c.String(maxLength: 100),
                        NameLast = c.String(maxLength: 100),
                        EmailAddress = c.String(maxLength: 200),
                        Password = c.String(maxLength: 100),
                        LastLoginDate = c.DateTime(),
                        LastLockoutDate = c.DateTime(),
                        LastPasswordChangedDate = c.DateTime(),
                        IsVerified = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        NavigationMode = c.Int(nullable: false),
                        PageLayout = c.Int(nullable: false),
                        PageTheme = c.String(maxLength: 200),
                        Address = c.String(maxLength: 50),
                        City = c.String(maxLength: 200),
                        State = c.String(maxLength: 2),
                        Zipcode = c.String(maxLength: 10),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "dbo.Membership",
                c => new
                    {
                        MembershipId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MembershipId)
                .ForeignKey("dbo.UserGroup", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        UserGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserGroupId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "datastorage.CustomerCoverage",
                c => new
                    {
                        CustomerCoverageId = c.Int(nullable: false, identity: true),
                        UploadedItemId = c.Int(nullable: false),
                        Value = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerCoverageId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .ForeignKey("datastorage.UploadedItem", t => t.UploadedItemId, cascadeDelete: true)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId)
                .Index(t => t.UploadedItemId);
            
            CreateTable(
                "datastorage.UploadedItem",
                c => new
                    {
                        UploadedItemId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        UniqueIdentifier = c.String(nullable: false, maxLength: 100),
                        PayloadTypeId = c.Int(nullable: false),
                        PayloadFilename = c.String(nullable: false, maxLength: 200),
                        PayloadChecksum = c.String(nullable: false, maxLength: 100),
                        PayloadFilesize = c.String(nullable: false, maxLength: 100),
                        TradingDateStart = c.DateTime(nullable: false),
                        TradingDateEnd = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        GenericField1 = c.String(maxLength: 100),
                        GenericField2 = c.String(maxLength: 100),
                        GlacierZone1ID = c.String(maxLength: 400),
                        GlacierZone2ID = c.String(maxLength: 400),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UploadedItemId)
                .ForeignKey("dbo.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .ForeignKey("datastorage.PayloadType", t => t.PayloadTypeId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId)
                .Index(t => t.PayloadTypeId);
            
            CreateTable(
                "datastorage.DataTag",
                c => new
                    {
                        DataTagId = c.Int(nullable: false, identity: true),
                        UploadedItemId = c.Int(nullable: false),
                        Value = c.String(maxLength: 100),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DataTagId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .ForeignKey("datastorage.UploadedItem", t => t.UploadedItemId, cascadeDelete: true)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId)
                .Index(t => t.UploadedItemId);
            
            CreateTable(
                "datastorage.PayloadType",
                c => new
                    {
                        PayloadTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        MonthsToKeep = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.PayloadTypeId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "datastorage.Replacement",
                c => new
                    {
                        ReplacementId = c.Int(nullable: false, identity: true),
                        UploadedItemId = c.Int(nullable: false),
                        UniqueIdentifier = c.String(nullable: false, maxLength: 100),
                        Reason = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ReplacementId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .ForeignKey("datastorage.UploadedItem", t => t.UploadedItemId, cascadeDelete: true)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId)
                .Index(t => t.UploadedItemId);
            
            CreateTable(
                "datastorage.TradingUnitCoverage",
                c => new
                    {
                        TradingUnitCoverageId = c.Int(nullable: false, identity: true),
                        UploadedItemId = c.Int(nullable: false),
                        Value = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TradingUnitCoverageId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .ForeignKey("datastorage.UploadedItem", t => t.UploadedItemId, cascadeDelete: true)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId)
                .Index(t => t.UploadedItemId);
            
            CreateTable(
                "dbo.FileData",
                c => new
                    {
                        FileDataId = c.Int(nullable: false, identity: true),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.FileDataId);
            
            CreateTable(
                "enum.FileType",
                c => new
                    {
                        FileTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Extension = c.String(nullable: false, maxLength: 10),
                        ContextType = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.FileTypeId);
            
            CreateTable(
                "audit.HistoryLog",
                c => new
                    {
                        HistoryLogId = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 250),
                        ModelId = c.Int(nullable: false),
                        PropertyName = c.String(nullable: false, maxLength: 250),
                        OldValue = c.String(),
                        OldModelId = c.Int(),
                        NewValue = c.String(),
                        NewModelId = c.Int(),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.HistoryLogId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "nav.NavIcon",
                c => new
                    {
                        NavIconId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CssClassName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.NavIconId);
            
            CreateTable(
                "nav.Navigation",
                c => new
                    {
                        NavigationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        InConstruction = c.Boolean(nullable: false),
                        SystemGenerated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NavigationId);
            
            CreateTable(
                "nav.NavItem",
                c => new
                    {
                        NavItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        TopLevelId = c.Int(nullable: false),
                        URL = c.String(maxLength: 100),
                        Sequence = c.Int(nullable: false),
                        IconId = c.Int(),
                        SystemGenerated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NavItemId)
                .ForeignKey("nav.NavIcon", t => t.IconId)
                .ForeignKey("nav.Navigation", t => t.TopLevelId, cascadeDelete: true)
                .Index(t => t.IconId)
                .Index(t => t.TopLevelId);
            
            CreateTable(
                "nav.NavSubItem",
                c => new
                    {
                        NavSubItemId = c.Int(nullable: false, identity: true),
                        NavItemId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        URL = c.String(maxLength: 100),
                        Sequence = c.Int(nullable: false),
                        IconId = c.Int(),
                        SystemGenerated = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.NavSubItemId)
                .ForeignKey("nav.NavIcon", t => t.IconId)
                .ForeignKey("nav.NavItem", t => t.NavItemId, cascadeDelete: true)
                .Index(t => t.IconId)
                .Index(t => t.NavItemId);
            
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        SettingId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Data = c.String(maxLength: 500),
                        Type = c.String(maxLength: 100),
                        IsStatic = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SettingId);
            
            CreateTable(
                "dbo.SimpleHTMLPage",
                c => new
                    {
                        SimpleHTMLPageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        BodyHTML = c.String(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.SimpleHTMLPageId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.ModifiedBy_UserId);
            
            CreateTable(
                "dbo.StoredFile",
                c => new
                    {
                        StoredFileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(nullable: false, maxLength: 50),
                        FileTypeId = c.Int(nullable: false),
                        FileDataId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy_UserId = c.Int(),
                        ModifiedOn = c.DateTime(),
                        ModifiedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.StoredFileId)
                .ForeignKey("dbo.User", t => t.CreatedBy_UserId)
                .ForeignKey("dbo.FileData", t => t.FileDataId, cascadeDelete: true)
                .ForeignKey("enum.FileType", t => t.FileTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.ModifiedBy_UserId)
                .Index(t => t.CreatedBy_UserId)
                .Index(t => t.FileDataId)
                .Index(t => t.FileTypeId)
                .Index(t => t.ModifiedBy_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoredFile", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.StoredFile", "FileTypeId", "enum.FileType");
            DropForeignKey("dbo.StoredFile", "FileDataId", "dbo.FileData");
            DropForeignKey("dbo.StoredFile", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("dbo.SimpleHTMLPage", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.SimpleHTMLPage", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("nav.NavItem", "TopLevelId", "nav.Navigation");
            DropForeignKey("nav.NavSubItem", "NavItemId", "nav.NavItem");
            DropForeignKey("nav.NavSubItem", "IconId", "nav.NavIcon");
            DropForeignKey("nav.NavItem", "IconId", "nav.NavIcon");
            DropForeignKey("audit.HistoryLog", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("audit.HistoryLog", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.TradingUnitCoverage", "UploadedItemId", "datastorage.UploadedItem");
            DropForeignKey("datastorage.TradingUnitCoverage", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.TradingUnitCoverage", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.Replacement", "UploadedItemId", "datastorage.UploadedItem");
            DropForeignKey("datastorage.Replacement", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.Replacement", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.UploadedItem", "PayloadTypeId", "datastorage.PayloadType");
            DropForeignKey("datastorage.PayloadType", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.PayloadType", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.UploadedItem", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.DataTag", "UploadedItemId", "datastorage.UploadedItem");
            DropForeignKey("datastorage.DataTag", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.DataTag", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.CustomerCoverage", "UploadedItemId", "datastorage.UploadedItem");
            DropForeignKey("datastorage.UploadedItem", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.UploadedItem", "ClientId", "dbo.Client");
            DropForeignKey("datastorage.CustomerCoverage", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("datastorage.CustomerCoverage", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("dbo.Client", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.Client", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("dbo.User", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.Membership", "MemberId", "dbo.User");
            DropForeignKey("dbo.UserGroup", "ModifiedBy_UserId", "dbo.User");
            DropForeignKey("dbo.Membership", "GroupId", "dbo.UserGroup");
            DropForeignKey("dbo.UserGroup", "CreatedBy_UserId", "dbo.User");
            DropForeignKey("dbo.User", "CreatedBy_UserId", "dbo.User");
            DropIndex("dbo.StoredFile", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.StoredFile", new[] { "FileTypeId" });
            DropIndex("dbo.StoredFile", new[] { "FileDataId" });
            DropIndex("dbo.StoredFile", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.SimpleHTMLPage", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.SimpleHTMLPage", new[] { "CreatedBy_UserId" });
            DropIndex("nav.NavItem", new[] { "TopLevelId" });
            DropIndex("nav.NavSubItem", new[] { "NavItemId" });
            DropIndex("nav.NavSubItem", new[] { "IconId" });
            DropIndex("nav.NavItem", new[] { "IconId" });
            DropIndex("audit.HistoryLog", new[] { "ModifiedBy_UserId" });
            DropIndex("audit.HistoryLog", new[] { "CreatedBy_UserId" });
            DropIndex("datastorage.TradingUnitCoverage", new[] { "UploadedItemId" });
            DropIndex("datastorage.TradingUnitCoverage", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.TradingUnitCoverage", new[] { "CreatedBy_UserId" });
            DropIndex("datastorage.Replacement", new[] { "UploadedItemId" });
            DropIndex("datastorage.Replacement", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.Replacement", new[] { "CreatedBy_UserId" });
            DropIndex("datastorage.UploadedItem", new[] { "PayloadTypeId" });
            DropIndex("datastorage.PayloadType", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.PayloadType", new[] { "CreatedBy_UserId" });
            DropIndex("datastorage.UploadedItem", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.DataTag", new[] { "UploadedItemId" });
            DropIndex("datastorage.DataTag", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.DataTag", new[] { "CreatedBy_UserId" });
            DropIndex("datastorage.CustomerCoverage", new[] { "UploadedItemId" });
            DropIndex("datastorage.UploadedItem", new[] { "CreatedBy_UserId" });
            DropIndex("datastorage.UploadedItem", new[] { "ClientId" });
            DropIndex("datastorage.CustomerCoverage", new[] { "ModifiedBy_UserId" });
            DropIndex("datastorage.CustomerCoverage", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.Client", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.Client", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.User", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.Membership", new[] { "MemberId" });
            DropIndex("dbo.UserGroup", new[] { "ModifiedBy_UserId" });
            DropIndex("dbo.Membership", new[] { "GroupId" });
            DropIndex("dbo.UserGroup", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.User", new[] { "CreatedBy_UserId" });
            DropTable("dbo.StoredFile");
            DropTable("dbo.SimpleHTMLPage");
            DropTable("dbo.Setting");
            DropTable("nav.NavSubItem");
            DropTable("nav.NavItem");
            DropTable("nav.Navigation");
            DropTable("nav.NavIcon");
            DropTable("audit.HistoryLog");
            DropTable("enum.FileType");
            DropTable("dbo.FileData");
            DropTable("datastorage.TradingUnitCoverage");
            DropTable("datastorage.Replacement");
            DropTable("datastorage.PayloadType");
            DropTable("datastorage.DataTag");
            DropTable("datastorage.UploadedItem");
            DropTable("datastorage.CustomerCoverage");
            DropTable("dbo.UserGroup");
            DropTable("dbo.Membership");
            DropTable("dbo.User");
            DropTable("dbo.Client");
        }
    }
}
