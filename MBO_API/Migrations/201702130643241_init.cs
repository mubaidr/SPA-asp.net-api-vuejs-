namespace MBO_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropColumn("dbo.Messages", "ReceiverID");
            DropColumn("dbo.Messages", "SenderID");
            RenameColumn(table: "dbo.Messages", name: "Receiver_Id", newName: "ReceiverID");
            RenameColumn(table: "dbo.Messages", name: "Sender_Id", newName: "SenderID");
            AlterColumn("dbo.Messages", "SenderID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "ReceiverID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "SenderID");
            CreateIndex("dbo.Messages", "ReceiverID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "ReceiverID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            AlterColumn("dbo.Messages", "ReceiverID", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "SenderID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Messages", name: "SenderID", newName: "Sender_Id");
            RenameColumn(table: "dbo.Messages", name: "ReceiverID", newName: "Receiver_Id");
            AddColumn("dbo.Messages", "SenderID", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "ReceiverID", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "Sender_Id");
            CreateIndex("dbo.Messages", "Receiver_Id");
        }
    }
}
