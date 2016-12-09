namespace MBO_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainTask",
                c => new
                    {
                        MainTask_Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                        DateAssigned = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        Status_Id = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MainTask_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Category_Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Category_Id);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Log_Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                        LogTime = c.DateTime(nullable: false),
                        MainTask_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Log_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.MainTask", t => t.MainTask_Id)
                .Index(t => t.MainTask_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Status_Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Status_Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.ProviderKey })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainTaskApplicationUsers",
                c => new
                    {
                        MainTask_MainTask_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MainTask_MainTask_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.MainTask", t => t.MainTask_MainTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.MainTask_MainTask_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.MainTask", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.Log", "MainTask_Id", "dbo.MainTask");
            DropForeignKey("dbo.Log", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.MainTask", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.MainTaskApplicationUsers", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.MainTaskApplicationUsers", "MainTask_MainTask_Id", "dbo.MainTask");
            DropForeignKey("dbo.MainTask", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.MainTaskApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MainTaskApplicationUsers", new[] { "MainTask_MainTask_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Log", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Log", new[] { "MainTask_Id" });
            DropIndex("dbo.MainTask", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MainTask", new[] { "Category_Id" });
            DropIndex("dbo.MainTask", new[] { "Status_Id" });
            DropTable("dbo.MainTaskApplicationUsers");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Status");
            DropTable("dbo.Log");
            DropTable("dbo.Category");
            DropTable("dbo.MainTask");
            DropTable("dbo.ApplicationUsers");
        }
    }
}
