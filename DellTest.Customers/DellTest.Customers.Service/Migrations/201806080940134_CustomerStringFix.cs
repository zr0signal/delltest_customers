namespace DellTest.Customers.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerStringFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Email", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.Int(nullable: false));
        }
    }
}
