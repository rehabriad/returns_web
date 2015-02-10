namespace ST.WebUI.DataContext.STMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyReturns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Returns", "rin", "dbo.Masters");
            DropIndex("dbo.Returns", new[] { "rin" });
            AlterColumn("dbo.Returns", "rin", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Returns", "docLocNumber", c => c.String(nullable: false, maxLength: 20));
            CreateIndex("dbo.Returns", "rin");
            AddForeignKey("dbo.Returns", "rin", "dbo.Masters", "rin", cascadeDelete: true);
            DropColumn("dbo.Returns", "doclocnum");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Returns", "doclocnum", c => c.String());
            DropForeignKey("dbo.Returns", "rin", "dbo.Masters");
            DropIndex("dbo.Returns", new[] { "rin" });
            AlterColumn("dbo.Returns", "docLocNumber", c => c.String());
            AlterColumn("dbo.Returns", "rin", c => c.String(maxLength: 128));
            CreateIndex("dbo.Returns", "rin");
            AddForeignKey("dbo.Returns", "rin", "dbo.Masters", "rin");
        }
    }
}
