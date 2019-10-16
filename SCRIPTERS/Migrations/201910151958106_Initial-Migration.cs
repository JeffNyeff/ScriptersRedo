namespace SCRIPTERS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Image", c => c.Binary(nullable: false));
        }
    }
}
