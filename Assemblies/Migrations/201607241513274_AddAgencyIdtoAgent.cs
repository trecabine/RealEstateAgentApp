namespace Assemblies.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAgencyIdtoAgent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Agents", "Agency_Id", "dbo.Agencies");
            DropIndex("dbo.Agents", new[] { "Agency_Id" });
            AddColumn("dbo.Agents", "Agency_Id1", c => c.Int());
            AlterColumn("dbo.Agents", "Agency_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Agents", "Agency_Id1");
            AddForeignKey("dbo.Agents", "Agency_Id1", "dbo.Agencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Agents", "Agency_Id1", "dbo.Agencies");
            DropIndex("dbo.Agents", new[] { "Agency_Id1" });
            AlterColumn("dbo.Agents", "Agency_Id", c => c.Int());
            DropColumn("dbo.Agents", "Agency_Id1");
            CreateIndex("dbo.Agents", "Agency_Id");
            AddForeignKey("dbo.Agents", "Agency_Id", "dbo.Agencies", "Id");
        }
    }
}
