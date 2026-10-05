using Catalogo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //TABLAS
        public DbSet<Cosecha> Cosechas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<EstadoCosecha> EstadoCosechas { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  RELACIONES

            //Relacion Producto --> Cosecha
            modelBuilder.Entity<Producto>()
                .HasMany(p => p.Cosechas)
                .WithOne(c => c.Producto)
                .HasForeignKey(c => c.IdProducto);

            //Relacion EstadoCosehca --> Cosecha
            modelBuilder.Entity<EstadoCosecha>()
                .HasMany(e => e.Cosechas)
                .WithOne(c => c.EstadoCosecha)
                .HasForeignKey(c => c.IdEstadoCosecha);

            //Relacion Categoria --> Producto
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Productos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.IdCategoria);

            //Relacion UnidadMedida --> Producto
            modelBuilder.Entity<UnidadMedida>()
                .HasMany(u => u.Productos)
                .WithOne(p => p.UnidadMedida)
                .HasForeignKey(p => p.IdUnidadMedida);

        }

    }
}
