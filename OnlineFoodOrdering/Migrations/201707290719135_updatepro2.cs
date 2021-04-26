namespace OnlineFoodOrdering.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatepro2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
