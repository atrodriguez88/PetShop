namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tercera : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "gender", c => c.String(maxLength: 5));
            AlterColumn("dbo.Mascotas", "gender", c => c.String(maxLength: 5));

            CreateStoredProcedure("sp_name", x => new { gender = x.String() },
                @"SELECT name FROM Cliente WHERE gender = @gender");

            //Other Form
            //Creando Resourse.resx
            //Sql(ResourcesSQL.Cear_algo);
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Mascotas", "gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Clientes", "gender", c => c.Boolean(nullable: false));

            DropStoredProcedure("sp_name");
        }
    }
}
