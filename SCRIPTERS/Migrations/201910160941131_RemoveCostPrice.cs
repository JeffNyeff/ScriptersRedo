namespace SCRIPTERS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCostPrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Books", "CostPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
