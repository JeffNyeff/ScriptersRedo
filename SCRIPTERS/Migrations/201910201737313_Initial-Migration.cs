namespace SCRIPTERS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "PersonDetails");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "PersonDetails", c => c.String());
        }
    }
}
