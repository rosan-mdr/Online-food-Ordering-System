namespace OnlineFoodOrdering.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepro5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductCode", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductCode");
        }
    }
}
