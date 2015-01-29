namespace ST.WebUI.DataContext.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FullUsername", c => c.String(nullable: false, maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FullUsername");
        }
    }
}
