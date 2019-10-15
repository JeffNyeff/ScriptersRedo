namespace SCRIPTERS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionTime = c.DateTime(nullable: false),
                        User = c.String(),
                        TransactionType = c.String(),
                        TransactionDetails = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToEmail = c.String(nullable: false),
                        EMailBody = c.String(),
                        EmailSubject = c.String(),
                        EmailCC = c.String(),
                        EmailBCC = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        ThemeColor = c.String(),
                        IsFullDay = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.SmsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToNo = c.String(nullable: false),
                        SmsBody = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmsModels");
            DropTable("dbo.Events");
            DropTable("dbo.EmailModels");
            DropTable("dbo.Audits");
        }
    }
}
