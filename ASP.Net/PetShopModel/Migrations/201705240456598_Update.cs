namespace PetShopModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("sp_name", x => new { gender = x.String() },
                @"SELECT name FROM Clientes WHERE gender = @gender");

            //Other Form
            //Creando Resourse.resx
            //Sql(ResourcesSQL.Cear_algo);

            CreateTable(
                "dbo.Alimentoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        IdTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDeMascotas", t => t.IdTipo, cascadeDelete: true)
                .Index(t => t.IdTipo);
            
            CreateTable(
                "dbo.TipoDeMascotas",
                c => new
                    {
                        IdTipo = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.IdTipo);
            
            CreateTable(
                "dbo.Mascotas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Img = c.String(),
                        Gender = c.Boolean(nullable: false),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        IdTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDeMascotas", t => t.IdTipo, cascadeDelete: true)
                .Index(t => t.IdTipo);
            
            CreateTable(
                "dbo.Utiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        IdTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoDeMascotas", t => t.IdTipo, cascadeDelete: true)
                .Index(t => t.IdTipo);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Email = c.String(),
                        ConfirmEmail = c.String(),
                        Gender = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("sp_name");

            DropForeignKey("dbo.Utiles", "IdTipo", "dbo.TipoDeMascotas");
            DropForeignKey("dbo.Mascotas", "IdTipo", "dbo.TipoDeMascotas");
            DropForeignKey("dbo.Alimentoes", "IdTipo", "dbo.TipoDeMascotas");
            DropIndex("dbo.Utiles", new[] { "IdTipo" });
            DropIndex("dbo.Mascotas", new[] { "IdTipo" });
            DropIndex("dbo.Alimentoes", new[] { "IdTipo" });
            DropTable("dbo.Clientes");
            DropTable("dbo.Utiles");
            DropTable("dbo.Mascotas");
            DropTable("dbo.TipoDeMascotas");
            DropTable("dbo.Alimentoes");
        }
    }
}
