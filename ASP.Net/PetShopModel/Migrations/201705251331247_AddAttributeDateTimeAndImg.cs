namespace PetShopModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributeDateTimeAndImg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Alimentoes", "Img", c => c.String());
            AddColumn("dbo.Alimentoes", "BuyTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Mascotas", "BuyTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Utiles", "Img", c => c.String());
            AddColumn("dbo.Utiles", "BuyTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utiles", "BuyTime");
            DropColumn("dbo.Utiles", "Img");
            DropColumn("dbo.Mascotas", "BuyTime");
            DropColumn("dbo.Alimentoes", "BuyTime");
            DropColumn("dbo.Alimentoes", "Img");
        }
    }
}
