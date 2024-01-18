using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Clases.Flujo;
using FerreteriaJuanito.Clases.Utils;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<TipoUsuario> TipoUsuarios => Set<TipoUsuario>();
        public DbSet<Proveedor> Proveedors => Set<Proveedor>();
        public DbSet<Producto> Productos=> Set<Producto>();
        public DbSet<CarroCompra> CarroCompras => Set<CarroCompra>();
        public DbSet<MedioPago> MedioPagos => Set<MedioPago>();
        public DbSet<Despacho> Despachos => Set<Despacho>();
        public DbSet<Logs> Logss => Set<Logs>();
        public DbSet<Login> Logins => Set<Login>();
        
        internal object Where(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
