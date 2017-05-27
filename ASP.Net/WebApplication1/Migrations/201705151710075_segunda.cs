namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class segunda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mascotas", "Cliente_id", c => c.Int());
            CreateIndex("dbo.Mascotas", "Cliente_id");
            AddForeignKey("dbo.Mascotas", "Cliente_id", "dbo.Clientes", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mascotas", "Cliente_id", "dbo.Clientes");
            DropIndex("dbo.Mascotas", new[] { "Cliente_id" });
            DropColumn("dbo.Mascotas", "Cliente_id");
        }
    }
}
