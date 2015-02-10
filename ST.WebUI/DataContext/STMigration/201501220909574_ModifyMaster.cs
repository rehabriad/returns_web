namespace ST.WebUI.DataContext.STMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyMaster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Masters", "regstatuscode", c => c.Int(nullable: false));
            AddColumn("dbo.Masters", "fillingcode", c => c.Int(nullable: false));
            AlterColumn("dbo.Masters", "regname", c => c.String(maxLength: 512));
            AlterColumn("dbo.Masters", "tradename", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Masters", "tradename", c => c.String());
            AlterColumn("dbo.Masters", "regname", c => c.String());
            DropColumn("dbo.Masters", "fillingcode");
            DropColumn("dbo.Masters", "regstatuscode");
        }
    }
}
