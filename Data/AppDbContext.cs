using CrudMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudMVCApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }
        public AppDbContext()
        {
        }

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Direccion> Direccion { get; set; }
        public DbSet<Producto> Producto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Persona>()
                .HasMany(p => p.Direcciones)
                .WithOne(d => d.Persona)
                .HasForeignKey(d => d.PersonaId);

            modelBuilder.Entity<Persona>()
                .HasMany(p => p.Pedidos)
                .WithOne(p => p.Persona)
                .HasForeignKey(p => p.PersonaId);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Pedidos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.DetallePedidos)
                .WithOne(dp => dp.Pedido)
                .HasForeignKey(dp => dp.PedidoId);

            modelBuilder.Entity<Producto>()
                .HasMany(p => p.DetallePedidos)
                .WithOne(dp => dp.Producto)
                .HasForeignKey(dp => dp.ProductoId);
        }




    }
}
