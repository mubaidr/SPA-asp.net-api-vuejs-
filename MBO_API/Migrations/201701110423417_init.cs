namespace MBO_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.MainTask",
                c => new
                    {
                        MainTaskID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Progress = c.Int(nullable: false),
                        DateAssigned = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        StatusID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        AssignedByID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MainTaskID)
                .ForeignKey("dbo.AspNetUsers", t => t.AssignedByID)
                .ForeignKey("dbo.Category", t => t.CategoryID)
                .ForeignKey("dbo.Status", t => t.StatusID)
                .Index(t => t.StatusID)
                .Index(t => t.CategoryID)
                .Index(t => t.AssignedByID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                        LogTime = c.DateTime(nullable: false),
                        MainTaskID = c.Int(nullable: false),
                        ApplicationUserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.LogID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.MainTask", t => t.MainTaskID)
                .Index(t => t.MainTaskID)
                .Index(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ProgressHistories",
                c => new
                    {
                        ProgressHistoryID = c.Int(nullable: false, identity: true),
                        MainTaskID = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProgressHistoryID)
                .ForeignKey("dbo.MainTask", t => t.MainTaskID)
                .Index(t => t.MainTaskID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 25),
                        Description = c.String(nullable: false, maxLength: 250),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.MainTaskApplicationUsers",
                c => new
                    {
                        MainTask_MainTaskID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MainTask_MainTaskID, t.ApplicationUser_Id })
                .ForeignKey("dbo.MainTask", t => t.MainTask_MainTaskID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.MainTask_MainTaskID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MainTask", "StatusID", "dbo.Status");
            DropForeignKey("dbo.ProgressHistories", "MainTaskID", "dbo.MainTask");
            DropForeignKey("dbo.MainTask", "CategoryID", "dbo.Category");
            DropForeignKey("dbo.MainTaskApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MainTaskApplicationUsers", "MainTask_MainTaskID", "dbo.MainTask");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Log", "MainTaskID", "dbo.MainTask");
            DropForeignKey("dbo.Log", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.MainTask", "AssignedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MainTaskApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.MainTaskApplicationUsers", new[] { "MainTask_MainTaskID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProgressHistories", new[] { "MainTaskID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Log", new[] { "ApplicationUserID" });
            DropIndex("dbo.Log", new[] { "MainTaskID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.MainTask", new[] { "AssignedByID" });
            DropIndex("dbo.MainTask", new[] { "CategoryID" });
            DropIndex("dbo.MainTask", new[] { "StatusID" });
            DropTable("dbo.MainTaskApplicationUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Status");
            DropTable("dbo.ProgressHistories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Log");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.MainTask");
            DropTable("dbo.Category");
        }
    }
}
