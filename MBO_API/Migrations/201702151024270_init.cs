namespace MBO_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.MainTask", new[] { "AssignedByID" });
            DropIndex("dbo.Messages", new[] { "ReceiverID" });
            AlterColumn("dbo.Category", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.MainTask", "Title", c => c.String());
            AlterColumn("dbo.MainTask", "Description", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.MainTask", "AssignedByID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "ReceiverID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Status", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Status", "Description", c => c.String(nullable: false));
            CreateIndex("dbo.MainTask", "AssignedByID");
            CreateIndex("dbo.Messages", "ReceiverID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "ReceiverID" });
            DropIndex("dbo.MainTask", new[] { "AssignedByID" });
            AlterColumn("dbo.Status", "Description", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Status", "Title", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Messages", "ReceiverID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "Description", c => c.String());
            AlterColumn("dbo.MainTask", "AssignedByID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.MainTask", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.MainTask", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Category", "Title", c => c.String(nullable: false, maxLength: 25));
            CreateIndex("dbo.Messages", "ReceiverID");
            CreateIndex("dbo.MainTask", "AssignedByID");
        }
    }
}
