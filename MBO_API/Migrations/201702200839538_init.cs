namespace MBO_API.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MainTask", "DateCompleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MainTask", "DateCompleted");
        }
    }
}
