namespace PetShopModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ContextModel : DbContext
    {
        // Your context has been configured to use a 'ContextModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'PetShopModel.ContextModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ContextModel' 
        // connection string in the application configuration file.
        public ContextModel()
            : base("name=ContextModel")
        {
        }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Mascota> Mascotas { get; set; }
        public virtual DbSet<Alimento> Alimentos { get; set; }
        public virtual DbSet<Utiles> UtilesS { get; set; }

        public virtual System.Data.Entity.DbSet<PetShopModel.Type.TipoDeMascota> TipoDeMascotas { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}