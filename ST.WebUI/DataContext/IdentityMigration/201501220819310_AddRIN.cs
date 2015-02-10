namespace ST.WebUI.DataContext.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRIN : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "rin", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "rin");
        }
    }
}
