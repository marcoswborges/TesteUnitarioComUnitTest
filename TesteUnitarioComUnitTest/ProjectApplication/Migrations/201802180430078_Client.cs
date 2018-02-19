namespace ProjectApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Client : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        iClientID = c.Int(nullable: false, identity: true),
                        sName = c.String(),
                        sEmail = c.String(),
                        sPhone = c.String(),
                        dtBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.iClientID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clients");
        }
    }
}
