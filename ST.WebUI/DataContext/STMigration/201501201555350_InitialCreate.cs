namespace ST.WebUI.DataContext.STMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Masters",
                c => new
                    {
                        rin = c.String(nullable: false, maxLength: 128),
                        officeid = c.Int(nullable: false),
                        regname = c.String(),
                        tradename = c.String(),
                        regdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.rin);
            
            CreateTable(
                "dbo.Retpurches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        officeid = c.Int(nullable: false),
                        returnsid = c.Guid(nullable: false),
                        taxrate = c.Int(nullable: false),
                        purchval = c.Decimal(nullable: false, precision: 18, scale: 2),
                        purchtax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        targetoffid = c.Int(nullable: false),
                        moddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Returns", t => t.returnsid, cascadeDelete: true)
                .Index(t => t.returnsid);
            
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        officeid = c.Int(nullable: false),
                        rin = c.String(maxLength: 128),
                        returncode = c.Int(nullable: false),
                        taxyrmo = c.DateTime(nullable: false),
                        transdate = c.DateTime(nullable: false),
                        saleltc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        purctdt2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        nettaxpy = c.Decimal(nullable: false, precision: 18, scale: 2),
                        targetoffid = c.Int(nullable: false),
                        doclocnum = c.String(),
                        moddate = c.DateTime(nullable: false),
                        status = c.Int(nullable: false),
                        docLocNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Masters", t => t.rin)
                .Index(t => t.rin);
            
            CreateTable(
                "dbo.Retsales",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        officeid = c.Int(nullable: false),
                        returnsid = c.Guid(nullable: false),
                        taxrateid = c.Int(nullable: false),
                        saleval = c.Decimal(nullable: false, precision: 18, scale: 2),
                        saletax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        targetoffid = c.Int(nullable: false),
                        moddate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Returns", t => t.returnsid, cascadeDelete: true)
                .Index(t => t.returnsid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Retsales", "returnsid", "dbo.Returns");
            DropForeignKey("dbo.Retpurches", "returnsid", "dbo.Returns");
            DropForeignKey("dbo.Returns", "rin", "dbo.Masters");
            DropIndex("dbo.Retsales", new[] { "returnsid" });
            DropIndex("dbo.Returns", new[] { "rin" });
            DropIndex("dbo.Retpurches", new[] { "returnsid" });
            DropTable("dbo.Retsales");
            DropTable("dbo.Returns");
            DropTable("dbo.Retpurches");
            DropTable("dbo.Masters");
        }
    }
}
